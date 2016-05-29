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
   
    public static class Import
    {
        public static BookMarkTree ImportBookMarkTree(string Filename)
        {
            BookMarkTree Tree;
            Stream stream = File.Open(Filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            BinaryFormatter bFormatter = new BinaryFormatter();
            Tree = (BookMarkTree)bFormatter.Deserialize(stream);
            stream.Close();
            return Tree;
        }

        public static SpriteLibrary ImportSpriteLibrary(string Filename)
        {
            SpriteLibrary Library;
            Stream stream = File.Open(Filename, FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
            BinaryFormatter bFormatter = new BinaryFormatter();
            Library = (SpriteLibrary)bFormatter.Deserialize(stream);
            stream.Close();
            return Library;
 
        }

        public static Data.Sprite ImportImage(string Filename)
        {
            
            if (System.IO.File.Exists(Filename) )
            {
                
                Bitmap bitmap = new Bitmap(Filename);
                if (bitmap.Width % 8 == 0 && bitmap.Height % 8 == 0)
                {
                    Data.Sprite Sprite;
                    System.Drawing.Imaging.ColorPalette palette = bitmap.Palette;
                    
                    if (palette.Entries.Length <= 16)
                    {
                        if (palette.Entries.Length > 1)
                        {
                            Sprite = new Data.Sprite(bitmap.Width / 8, bitmap.Height / 8, Data.Sprite.SpriteType.Color16);
                        }
                        else
                        {
                            throw new Exception("The image loaded was not indexed or used an undefined alpha channel.");
                        }
                    }
                    else
                    {
                        bool r = false;
                        int i = 16;
                        while( i < palette.Entries.Length & !r)
                        {
                            if (palette.Entries[i].ToArgb() != Color.Black.ToArgb())
                                r = true;

                            i++;
                        }

                        if (r )
                        {
                            Sprite = new Data.Sprite(bitmap.Width / 8, bitmap.Height / 8, Data.Sprite.SpriteType.Color256);
                        }
                        else
                        {
                            Sprite = new Data.Sprite(bitmap.Width / 8, bitmap.Height / 8, Data.Sprite.SpriteType.Color16);
                        }
                    }

                    for (int p = 0; p < Sprite.Palette.Colors.Length; p++)
                    {
                        if (p < palette.Entries.Length)
                        {
                            Sprite.Palette.Colors[p] = new Data.GBAcolor(palette.Entries[p]);
                        }
                    }
                    
                    Sprite.ImageData = GetDataFromBitmap(bitmap, Sprite.Palette.Type);
                    return Sprite;

                }
                else
                {
                    throw new Exception("The image's dimensions were not multiples of eight.");
                }

            }
            else
            {
                throw new Exception("The file does not exist.");
            }
        }

        public static byte[] GetDataFromBitmap(Bitmap bitmap, Data.SpritePalette.PaletteType Type)
        {
            byte[] sprite = new byte[1];
            if (Type == Data.SpritePalette.PaletteType.Color16)
            {
                sprite = new byte[bitmap.Width * bitmap.Height / 2];
            }
            else if (Type == Data.SpritePalette.PaletteType.Color256)
            {
                sprite = new byte[bitmap.Width * bitmap.Height];
            }

            #region lockbits
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            System.Drawing.Imaging.BitmapData bmpData =
                bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bitmap.PixelFormat);


            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            # endregion



            if (Type == Data.SpritePalette.PaletteType.Color16)
            {
                //Wierd format with PNG's from Unlz
                // -for some strange reason unlz exports 16 color sprites in 256 color format
                // -this doubles the size of the internal data and makes it hard to decode properly
                if (rgbValues.Length > bitmap.Width * bitmap.Height / 2)
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        for (int x = 0; x + 1 < bitmap.Width; x++)
                        {
                            byte i = (byte)(rgbValues[y * (bitmap.Width) + x] + (rgbValues[y * (bitmap.Width) + x + 1] << 4));
                            if (i != 0)
                            {
                                SetByte(ref sprite, Data.Sprite.SpriteType.Color16, bitmap.Width, bitmap.Height, x, y, i);
                            }
                        }

                    }
                }
                else
                {
                    for (int y = 0; y < bitmap.Height; y++)
                    {
                        for (int x = 0; x < bitmap.Width / 2; x++)
                        {
                            byte i = FlipByte(rgbValues[y * (bitmap.Width / 2) + x]);
                            if (i != 0)
                            {
                                SetByte(ref sprite, Data.Sprite.SpriteType.Color16, bitmap.Width, bitmap.Height, x * 2, y, i);
                            }
                        }

                    }
                }


            }
            else if (Type == Data.SpritePalette.PaletteType.Color256)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    for (int x = 0; x < bitmap.Width; x++)
                    {
                        byte i = rgbValues[y * (bitmap.Width) + x];
                        if (i != 0)
                        {
                            SetByte(ref sprite, Data.Sprite.SpriteType.Color256, bitmap.Width, bitmap.Height, x, y, i);
                        }
                    }

                }
            }
            
            bitmap.UnlockBits(bmpData);

            return sprite;
        }

        //public unsafe static byte[] uGetDataFromBitmap(Bitmap bitmap, Data.SpritePalette.PaletteType Type)
        //{
        //    byte[] sprite = new byte[1];
        //    if (Type == Data.SpritePalette.PaletteType.Color16)
        //    {
        //        sprite = new byte[bitmap.Width * bitmap.Height / 2];
        //    }
        //    else if (Type == Data.SpritePalette.PaletteType.Color256)
        //    {
        //        sprite = new byte[bitmap.Width * bitmap.Height];
        //    }

        //    #region lockbits
        //    Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

        //    System.Drawing.Imaging.BitmapData bmpData =
        //        bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
        //        bitmap.PixelFormat);


        //    // Get the address of the first line.
        //    IntPtr ptr = bmpData.Scan0;

        //    // Declare an array to hold the bytes of the bitmap.
        //    int bytes = Math.Abs(bmpData.Stride) * bitmap.Height;
        //    //byte[] rgbValues = new byte[bytes];

        //    // Copy the RGB values into the array.
        //    //System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

        //    # endregion



        //    if (Type == Data.SpritePalette.PaletteType.Color16)
        //    {
        //        //Wierd format with PNG's from Unlz
        //        // -for some strange reason unlz exports 16 color sprites in 256 color format
        //        // -this doubles the size of the internal data and makes it hard to decode properly
        //        if (bytes > bitmap.Width * bitmap.Height / 2)
        //        {
        //            for (int y = 0; y < bitmap.Height; y++)
        //            {
        //                for (int x = 0; x + 1 < bitmap.Width; x++)
        //                {
        //                    //*((byte*)ptr) = 1;

        //                    byte i = (byte) ( *((byte*)ptr + y * (bitmap.Width) + x) + *((byte*)ptr  + y * (bitmap.Width) + x + 1) << 4 );

        //                    SetByte(ref sprite, Data.Sprite.SpriteType.Color16, bitmap.Width, bitmap.Height, x, y, i);
        //                }

        //            }
        //        }
        //        else
        //        {
        //            for (int y = 0; y < bitmap.Height; y++)
        //            {
        //                for (int x = 0; x < bitmap.Width / 2; x++)
        //                {
        //                    byte i = FlipByte(*((byte*)ptr + y * (bitmap.Width / 2) + x));
        //                    if (i != 0)
        //                    {
        //                        SetByte(ref sprite, Data.Sprite.SpriteType.Color16, bitmap.Width, bitmap.Height, x * 2, y, i);

        //                    }
        //                }

        //            }
        //        }


        //    }
        //    else if (Type == Data.SpritePalette.PaletteType.Color256)
        //    {
        //        for (int y = 0; y < bitmap.Height; y++)
        //        {
        //            for (int x = 0; x < bitmap.Width; x++)
        //            {
        //                byte i = *((byte*)ptr + y * (bitmap.Width) + x);
        //                SetByte(ref sprite, Data.Sprite.SpriteType.Color256, bitmap.Width, bitmap.Height, x, y, i);
        //            }

        //        }
        //    }

        //    bitmap.UnlockBits(bmpData);

        //    return sprite;
        //}

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

        static void SetByte(ref byte[] Data, NSE_Framework.Data.Sprite.SpriteType Type, int Width, int Height, int x, int y, byte val)
        {
            int pos = Position2Index(new Size(Width, Height), new Point(x, y), Type);

            Data[pos] = val;
        }

        static byte FlipByte(byte Byte)
        {
            return (byte)((Byte >> 4) + ((Byte << 4) & 0xF0));
        }

    }
}
