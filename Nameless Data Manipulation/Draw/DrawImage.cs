using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;


namespace NSE_Framework
{
    public static class Draw
    {
        public static void DrawSprite(ref Bitmap bitmap, NSE_Framework.Data.Sprite Sprite, int scale = 2, int Length = -1)
        {
            DrawSprite(ref bitmap, Sprite, Point.Empty, scale, Length);
        }

        public static void DrawSprite(ref Bitmap bitmap, NSE_Framework.Data.Sprite Sprite, Point Position, int scale = 1, int Length = -1)
        {
            DrawData(ref bitmap, Sprite.ImageData, Sprite.Palette, new Size(Sprite.Width, Sprite.Height), Position, scale, -1);
        }

        public static void DrawData(ref Bitmap bitmap, byte[] Data, NSE_Framework.Data.SpritePalette Palette, Size size, int scale = 1, int Length = -1)
        {
            DrawData(ref bitmap, Data, Palette, size, Point.Empty, scale, Length);
        }

        public static void DrawData(ref Bitmap bitmap, byte[] Data, NSE_Framework.Data.SpritePalette Palette, Size size, Point Position, int scale = 1, int Length = -1)
        {

            // Lock the bitmap's bits.

            Bitmap refbitmap = new Bitmap(size.Width * 8, size.Height * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            #region lockbits
            Rectangle rect = new Rectangle(0, 0, size.Width, size.Height);

            System.Drawing.Imaging.BitmapData bmpData =
                refbitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                refbitmap.PixelFormat);
 

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes  = Math.Abs(bmpData.Stride) * refbitmap.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            //System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            # endregion

            size.Width *= 8;
            size.Height *= 8;
            int iLength = 0;
            if (Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color16)
            {
                iLength = size.Width * size.Height / 2;
            }
            else if (Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color256)
            {
                iLength = size.Width * size.Height;
            }

            Point mpos = new Point(8 , 8);

            #region OldDrawCode
            if (Data.Length != 0)
            {
                for (int i = 0; i < iLength; i++)
                {
                    if ((i + 1 < Data.Length && i < Length + 1) || (Length == -1 && i < Data.Length))
                    {
                        if (Data[i] != 0)
                        {
                            if (Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color16)
                            {

                                int r = Data[i] & 0x0F;
                                int l = Data[i] >> 4;
                                //int l = Data[i] >> 4;
                                //int r = Data[i] - (l << 4);

                                if (l != 0)
                                {
                                    //g.FillRectangle(new SolidBrush(Palette.Colors[l].Color), Position.X + 1, Position.Y, 1, 1);
                                    rgbValues[4 * (Position.Y * size.Width + Position.X + 1)] = Palette.Colors[l].Blue;
                                    rgbValues[4 * (Position.Y * size.Width + Position.X + 1) + 1] = Palette.Colors[l].Green;
                                    rgbValues[4 * (Position.Y * size.Width + Position.X + 1) + 2] = Palette.Colors[l].Red;
                                    rgbValues[4 * (Position.Y * size.Width + Position.X + 1) + 3] = 0xff;
                                }

                                if (r != 0)
                                {
                                    rgbValues[4 * (Position.Y * size.Width + Position.X)] = Palette.Colors[r].Blue;
                                    rgbValues[4 * (Position.Y * size.Width + Position.X) + 1] = Palette.Colors[r].Green;
                                    rgbValues[4 * (Position.Y * size.Width + Position.X) + 2] = Palette.Colors[r].Red;
                                    rgbValues[4 * (Position.Y * size.Width + Position.X) + 3] = 0xff;
                                    //g.FillRectangle(new SolidBrush(Palette.Colors[r].Color), Position.X, Position.Y,  1, 1);
                                }
                            }
                            else if (Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color256)
                            {
                                rgbValues[4 * (Position.Y * size.Width + Position.X)] = Palette.Colors[Data[i]].Blue;
                                rgbValues[4 * (Position.Y * size.Width + Position.X) + 1] = Palette.Colors[Data[i]].Green;
                                rgbValues[4 * (Position.Y * size.Width + Position.X) + 2] = Palette.Colors[Data[i]].Red;
                                rgbValues[4 * (Position.Y * size.Width + Position.X) + 3] = 0xff;
                            }

                        }
                    }
                    if (Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color16)
                    {
                        Position.X = Position.X + 2;
                    }
                    else if (Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color256)
                    {
                        Position.X++;
                    }
                    if (Position.X == mpos.X)
                    {
                        Position.X = mpos.X - 8;
                        Position.Y = Position.Y + 1;
                    }

                    if (Position.Y >= mpos.Y)
                    {
                        if (mpos.X >= (size.Width))
                        {
                            mpos.Y = mpos.Y + 8;
                            mpos.X = 8;
                            Position.Y = mpos.Y - 8;
                            Position.X = mpos.X - 8;
                        }
                        else
                        {
                            mpos.X = mpos.X + 8;
                            Position.Y = mpos.Y - 8;
                            Position.X = mpos.X - 8;
                        }
                    }

                }
            }
            #endregion

            
            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            refbitmap.UnlockBits(bmpData);

            RectangleF sourceRectangle = new RectangleF(-0.5f, -0.5f, (float)size.Width, (float)size.Height);

            // This rectangle defines where we want to draw on the control

            RectangleF destinationRectangle;
            if (scale == 1 || scale % 2 == 0)
            {
                destinationRectangle = new RectangleF(0.0f, 0.0f, (float)bitmap.Width, (float)bitmap.Height);
            }
            else
            {
                destinationRectangle = new RectangleF(-0.5f, -0.5f, (float)bitmap.Width, (float)bitmap.Height);
            }

            if (scale != 1)
            {
                bitmap = new Bitmap(bitmap.Width, bitmap.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    //g.DrawImage(refbitmap, scale / 2.0f, scale / 2.0f, size.Width * scale, size.Height * scale,GraphicsUnit.Pixel);
                    g.DrawImage(refbitmap, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);

                }
            }
            else
            {
                bitmap = refbitmap;
            }





        }

        public static void nDrawSprite(ref Bitmap bitmap, NSE_Framework.Data.Sprite Sprite)
        {
            nDrawData(ref bitmap, Sprite.ImageData, Sprite.Palette, new Size(Sprite.Width, Sprite.Height));
        }

        public static void nDrawData(ref Bitmap bitmap, byte[] Data, NSE_Framework.Data.SpritePalette Palette, Size size)
        {
            Bitmap refbitmap = new Bitmap(size.Width * 8, size.Height * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            #region lockbits
            Rectangle rect = new Rectangle(0, 0, size.Width, size.Height);

            System.Drawing.Imaging.BitmapData bmpData =
                refbitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                refbitmap.PixelFormat);


            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * refbitmap.Height;
            byte[] rgbValues = new byte[bytes];

            # endregion

            size.Width *= 8;
            size.Height *= 8;
            int iLength = 0;
            if (Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color16)
            {
                iLength = size.Width * size.Height / 2;
            }
            else if (Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color256)
            {
                iLength = size.Width * size.Height;
            }

            Point mpos = new Point(8, 8);


            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    int i = NSE_Framework.Data.Translator.Position2Index(size, new Point(x, y), NSE_Framework.Data.Translator.PaletteToSpriteType(Palette.Type));

                    if (Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color16)
                    {

                        int r = Data[i] & 0x0F;
                        int l = Data[i] >> 4;

                        if (l != 0)
                        {
                            //g.FillRectangle(new SolidBrush(Palette.Colors[l].Color), x + 1, y, 1, 1);
                            rgbValues[4 * (y * size.Width + x + 1)] = Palette.Colors[l].Blue;
                            rgbValues[4 * (y * size.Width + x + 1) + 1] = Palette.Colors[l].Green;
                            rgbValues[4 * (y * size.Width + x + 1) + 2] = Palette.Colors[l].Red;
                            rgbValues[4 * (y * size.Width + x + 1) + 3] = 0xff;
                        }

                        if (r != 0)
                        {
                            rgbValues[4 * (y * size.Width + x)] = Palette.Colors[r].Blue;
                            rgbValues[4 * (y * size.Width + x) + 1] = Palette.Colors[r].Green;
                            rgbValues[4 * (y * size.Width + x) + 2] = Palette.Colors[r].Red;
                            rgbValues[4 * (y * size.Width + x) + 3] = 0xff;
                            //g.FillRectangle(new SolidBrush(Palette.Colors[r].Color), x, y,  1, 1);
                        }
                        x++;
                    }
                    else if (Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color256)
                    {
                        rgbValues[4 * (y * size.Width + x)] = Palette.Colors[Data[i]].Blue;
                        rgbValues[4 * (y * size.Width + x) + 1] = Palette.Colors[Data[i]].Green;
                        rgbValues[4 * (y * size.Width + x) + 2] = Palette.Colors[Data[i]].Red;
                        rgbValues[4 * (y * size.Width + x) + 3] = 0xff;
                    }
                }
            }


            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            refbitmap.UnlockBits(bmpData);

            bitmap = refbitmap;
        }

        public unsafe static void uDrawData(ref Bitmap bitmap, byte[] Data, NSE_Framework.Data.SpritePalette Palette, Size size)
        {
            if (bitmap.Size.Width != size.Width * 8 || bitmap.Size.Height != size.Height * 8 || bitmap.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                bitmap = new Bitmap(size.Width * 8, size.Height * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            }

            Rectangle rect = new Rectangle(0, 0, size.Width * 8, size.Height * 8);


            System.Drawing.Imaging.BitmapData bmpData =
                bitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bitmap.PixelFormat);

            IntPtr ptr = bmpData.Scan0;

            size.Width *= 8;
            size.Height *= 8;

            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    int i = NSE_Framework.Data.Translator.Position2Index(size, new Point(x, y), NSE_Framework.Data.Translator.PaletteToSpriteType(Palette.Type));

                    if (i < Data.Length)
                    {
                        if (Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color16)
                        {
                            #region 16color
                            int r = Data[i] & 0x0F;
                            int l = Data[i] >> 4;

                            if (l != 0)
                            {
                                *((byte*)ptr + 4 * (y * size.Width + x + 1)) = Palette.Colors[l].Blue;
                                *((byte*)ptr + 4 * (y * size.Width + x + 1) + 1) = Palette.Colors[l].Green;
                                *((byte*)ptr + 4 * (y * size.Width + x + 1) + 2) = Palette.Colors[l].Red;
                                *((byte*)ptr + 4 * (y * size.Width + x + 1) + 3) = 0xFF;
                            }
                            else
                            {
                                *((byte*)ptr + 4 * (y * size.Width + x + 1)) = 0;
                                *((byte*)ptr + 4 * (y * size.Width + x + 1) + 1) = 0;
                                *((byte*)ptr + 4 * (y * size.Width + x + 1) + 2) = 0;
                                *((byte*)ptr + 4 * (y * size.Width + x + 1) + 3) = 0;
                            }

                            if (r != 0)
                            {
                                *((byte*)ptr + 4 * (y * size.Width + x)) = Palette.Colors[r].Blue;
                                *((byte*)ptr + 4 * (y * size.Width + x) + 1) = Palette.Colors[r].Green;
                                *((byte*)ptr + 4 * (y * size.Width + x) + 2) = Palette.Colors[r].Red;
                                *((byte*)ptr + 4 * (y * size.Width + x) + 3) = 0xFF;
                            }
                            else
                            {
                                *((byte*)ptr + 4 * (y * size.Width + x)) = 0;
                                *((byte*)ptr + 4 * (y * size.Width + x) + 1) = 0;
                                *((byte*)ptr + 4 * (y * size.Width + x) + 2) = 0;
                                *((byte*)ptr + 4 * (y * size.Width + x) + 3) = 0;
                            }

                            x++;
                            #endregion
                        }
                        else if (Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color256)
                        {
                            #region 256color
                            if (Data[i] != 0)
                            {
                                *((byte*)ptr + 4 * (y * size.Width + x)) = Palette.Colors[Data[i]].Blue;
                                *((byte*)ptr + 4 * (y * size.Width + x) + 1) = Palette.Colors[Data[i]].Green;
                                *((byte*)ptr + 4 * (y * size.Width + x) + 2) = Palette.Colors[Data[i]].Red;
                                *((byte*)ptr + 4 * (y * size.Width + x) + 3) = 0xFF;
                            }
                            else
                            {
                                *((byte*)ptr + 4 * (y * size.Width + x)) = 0;
                                *((byte*)ptr + 4 * (y * size.Width + x) + 1) = 0;
                                *((byte*)ptr + 4 * (y * size.Width + x) + 2) = 0;
                                *((byte*)ptr + 4 * (y * size.Width + x) + 3) = 0;
                            }


                            //rgbValues[4 * (y * size.Width + x)] = Palette.Colors[Data[i]].Blue;
                            //rgbValues[4 * (y * size.Width + x) + 1] = Palette.Colors[Data[i]].Green;
                            //rgbValues[4 * (y * size.Width + x) + 2] = Palette.Colors[Data[i]].Red;
                            //rgbValues[4 * (y * size.Width + x) + 3] = 0xff;

                            #endregion
                        }
                    }
                }
            }


            bitmap.UnlockBits(bmpData);

            //bitmap = refbitmap;
        }

        public static void uDrawSprite(ref Bitmap bitmap, NSE_Framework.Data.Sprite Sprite)
        {
            uDrawData(ref bitmap, Sprite.ImageData, Sprite.Palette, new Size(Sprite.Width, Sprite.Height));
        }

    }
}
