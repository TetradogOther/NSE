using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NSE2
{
    static class Draw
    {
        public static void DrawData(ref Bitmap bitmap, byte[] Data, NSE2.Data.SpritePalette Palette, Size size, Point Position, int scale = 2, int Length = -1)
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
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            # endregion

            size.Width *= 8;
            size.Height *= 8;

            Point mpos = new Point(8 , 8);

            if (Data.Length != 0)
            {
                for (int i = 0; i < (size.Width * size.Height / 2); i++)
                {
                    if ((i + 1 < Data.Length && i < Length + 1) || (Length == -1 && i <= Data.Length))
                    {
                        if (Data[i] != 0)
                        {
                            int l = Data[i] >> 4;
                            int r = Data[i]- (l << 4);

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
                    }

                    Position.X = Position.X + 2;
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


            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            refbitmap.UnlockBits(bmpData);

            RectangleF sourceRectangle = new RectangleF(-0.5f, -0.5f, (float)size.Width, (float)size.Height);

            // This rectangle defines where we want to draw on the control

            RectangleF destinationRectangle = new RectangleF(0.0f, 0.0f, (float)bitmap.Width, (float)bitmap.Height);


            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                //g.DrawImage(refbitmap, scale / 2.0f, scale / 2.0f, size.Width * scale, size.Height * scale,GraphicsUnit.Pixel);
                g.DrawImage(refbitmap, destinationRectangle, sourceRectangle,GraphicsUnit.Pixel);

            }





        }

    }
}
