using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

 

namespace NSE_Framework.IO
{
    public static class Export
    {

        public static T[] SubArray<T>(this T[] data, int index, int length) 
        { 
            T[] result = new T[length]; 
            Array.Copy(data, index, result, 0, length); 
            return result; 
        }
        
        public static void ExportSpriteLibrary(string Filename, SpriteLibrary Library)
        {
           
            Stream stream = File.Open(Filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, Library);
            stream.Close();
 
        }

        public static void ExportBitmap(string Filename, NSE_Framework.Data.Sprite Sprite)
        {
            if (Sprite.Type == Data.Sprite.SpriteType.Color16 || Sprite.Type == Data.Sprite.SpriteType.Color256)
            {
                //4bb bitmap http://en.wikipedia.org/wiki/BMP_file_format
                //header creation
                byte[] data; //= new byte[54 + 4 * Sprite.Palette.Colors.Length + (32 * (Sprite.Width * Sprite.Height))];
                
                byte[] ilength;
                if(Sprite.Type == Data.Sprite.SpriteType.Color16)
                {
                    data = new byte[118 + (32 * (Sprite.Width * Sprite.Height))];

                    //1
                        //0xF at offset 0x9 will mean 16 colors (this is not part of the bitmap format)
                    data[9] = 0XF;
                    data[0xA] = 0x76;

                    //2
                    data[0x1C] = 4;
                    ilength = BitConverter.GetBytes(32 * (Sprite.Width * Sprite.Height));

                }
                else if (Sprite.Type == Data.Sprite.SpriteType.Color256)
                {
                    data = new byte[1114 + (64 * (Sprite.Width * Sprite.Height))];
                    //1
                        //0xFF at offset 0x9 will mean 256 colors (this is not part of the bitmap format)
                    data[9] = 0xFF;
                    data[0xA] = 0x5A;
                    data[0xB] = 0x4;

                    //2
                    data[0x1C] = 8;
                    ilength = BitConverter.GetBytes(64 * (Sprite.Width * Sprite.Height));
                }
                else
                {
                    throw new Exception("Unsupported sprite.");
                }

                data[0] = 0x42;
                data[1] = 0x4D;

                //Write Length                 
                byte[] flength = BitConverter.GetBytes(data.Length);
                flength.CopyTo(data, 2);

                //Add "NSE" to the bitmap
                data[6] = 0x4E;
                data[7] = 0x53;
                data[8] = 0x45;

                //1
                data[0xE] = 40;

                byte[] width = BitConverter.GetBytes(Sprite.Width * 8);
                width.CopyTo(data, 0x12);

                byte[] height = BitConverter.GetBytes(Sprite.Height* 8);
                height.CopyTo(data, 0x16);

                data[0x1A] = 1;

                //2
                ilength.CopyTo(data,0x22);                 

                // Write Palette Table
                int i = 0;
                foreach (NSE_Framework.Data.GBAcolor color in Sprite.Palette.Colors)
                {
                    data[0x36 + i * 4] = color.Blue;
                    data[0x37 + i * 4] = color.Green;
                    data[0x38 + i * 4] = color.Red;

                    i++;
                }

                if (Sprite.Type == Data.Sprite.SpriteType.Color16)
                {
                    for (int y = 0; y < Sprite.Height * 8; y++)
                    {
                        for (int x = 0; x < Sprite.Width * 4; x++)
                        {
                            data[118 + ((Sprite.Height * 8) - y - 1) * (Sprite.Width * 4) + x] = FlipByte(Sprite.ImageData[Position2Index(new Size(Sprite.Width * 8, Sprite.Height * 8), new Point(x * 2, y), Sprite.Type)]);
                        }                       
                    }
                }
                else if (Sprite.Type == Data.Sprite.SpriteType.Color256)
                {
                    for (int y = 0; y < Sprite.Height * 8; y++)
                    {
                        for (int x = 0; x < Sprite.Width * 8; x++)
                        {
                            data[1114 + ((Sprite.Height * 8) - y - 1) * (Sprite.Width * 8) + x] = Sprite.ImageData[Position2Index(new Size(Sprite.Width * 8, Sprite.Height * 8), new Point(x, y), Sprite.Type)];
                        }
                    }
                }




                Stream stream = File.Open(Filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
                BinaryWriter bw = new BinaryWriter(stream);
               
                bw.Write(data);
                bw.Close();
                stream.Close();

            }


        }

        public static void ExportBookMarkTree(string Filename, BookMarkTree Tree)
        {
            if (!Directory.Exists(Filename))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Filename));
            }

            Stream stream = File.Open(Filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, Tree);
            stream.Close();
        }

        static int Position2Index(Size Size, Point Position, NSE_Framework.Data.Sprite.SpriteType Type)
        {

            if (Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
            {

                int r = (Position.Y - (Position.Y % 8)) / 2 * Size.Width;
                r += (Position.X - (Position.X % 8)) * 4;
                r += (Position.Y % 8) * 4;
                r += (Position.X % 8) / 2;

                return r;
            }
            else if (Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
            {
                int r = (Position.Y - (Position.Y % 8)) * Size.Width;
                r += (Position.X - (Position.X % 8)) * 8;
                r += (Position.Y % 8) * 8;
                r += (Position.X % 8);

                return r;
            }
            else
            {
                return -1;
            }

        }

        static byte FlipByte(byte Byte)
        {
            return (byte)((Byte >> 4) + ((Byte << 4) & 0xF0));
        }

    }
}
