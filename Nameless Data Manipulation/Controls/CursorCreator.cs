using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace NSE_Framework.Controls
{
    public static class CursorCreator
    {
        public struct IconInfo
        {
            public bool fIcon;
            public int xHotspot;
            public int yHotspot;
            public IntPtr hbmMask;
            public IntPtr hbmColor;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr CreateIconIndirect(ref IconInfo icon);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

        public static Cursor MakeImage(System.Drawing.Bitmap bmp, int xHotSpot, int yHotSpot)
        {
            IconInfo tmp = new IconInfo();
            GetIconInfo(bmp.GetHicon(), ref tmp);
            tmp.xHotspot = xHotSpot;
            tmp.yHotspot = yHotSpot;
            tmp.fIcon = false;
            return new Cursor(CreateIconIndirect(ref tmp));
        }

        public static Cursor iMakeBrush(int brushstroke, int zoom)
        {


            int w = zoom * (2 * brushstroke + 3);
            int h = zoom * (2 * brushstroke + 3);

            bool[,] xorMask = new bool[w, h];
            bool[,] andMask = new bool[w, h];

            for (int y = 0; y < xorMask.GetUpperBound(1) + 1; y++)
            {
                for (int x = 0; x < xorMask.GetUpperBound(0) + 1; x++)
                {
                    //xorMask[x, y] = (x % 16 < 8) == (y % 16 < 8) ? true : false;
                    if (x == 0 || y == 0 ||
                        x == xorMask.GetUpperBound(0) || y == xorMask.GetUpperBound(1))
                    {
                        xorMask[x, y] = true;
                    }
                    
                    andMask[x, y] = true;
                }
            }

            if (zoom > 2)
            {
                xorMask[w / 2, h / 2] = true;
                xorMask[w / 2, h / 2 + 1] = true;
                xorMask[w / 2, h / 2 - 1] = true;
                xorMask[w / 2 + 1, h / 2] = true;
                xorMask[w / 2 - 1, h / 2] = true;
            }

            return MakeCursor(xorMask, andMask);
        }

        public static Cursor iMakePencil(int zoom)
        {


            int w = zoom;
            int h = zoom;

            bool[,] xorMask = new bool[w, h];
            bool[,] andMask = new bool[w, h];

            for (int y = 0; y < xorMask.GetUpperBound(1) + 1; y++)
            {
                for (int x = 0; x < xorMask.GetUpperBound(0) + 1; x++)
                {
                    //xorMask[x, y] = (x % 16 < 8) == (y % 16 < 8) ? true : false;
                    if (x == 0 || y == 0 ||
                        x == xorMask.GetUpperBound(0) || y == xorMask.GetUpperBound(1))
                    {
                        xorMask[x, y] = true;
                    }
                    andMask[x, y] = true;
                }
            }

            return MakeCursor(xorMask, andMask);
        }

        public static Cursor MakeCursor(bool[,] xorMask, bool[,] andMask)
        {
            int w = xorMask.GetUpperBound(0) + 1;
            int h = xorMask.GetUpperBound(1) + 1;

            MemoryStream ms = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(ms);

            bw.Write((ushort)0); // Reserved
            bw.Write((ushort)2); // Cursor
            bw.Write((ushort)1); // Number of images

            WriteImageDescriptor(bw, w, h);
            WriteBitmapHeader(bw, w, h * 2); // * 2 due to XOR and AND data
            WriteBitmapData(bw, xorMask);
            WriteBitmapData(bw, andMask);

            bw.Flush();

            ms.Position = 0;

            return new Cursor(ms);
        }
        private static void WriteImageDescriptor(BinaryWriter bw, int w, int h)
        {
            bw.Write((byte)(w % 256));
            bw.Write((byte)(h % 256));
            bw.Write((byte)2); // Monochrome
            bw.Write((byte)0); // Reserved
            bw.Write((ushort)((w - 1) / 2)); // Hotspot x
            bw.Write((ushort)((h - 1) / 2)); // Hotspot y
            bw.Write((uint)(40 + 8 + DivCeiling(w, 32) * 4 * h * 2)); // Bitmap size, * 2 due to XOR and AND
            bw.Flush();
            bw.Write((uint)bw.BaseStream.Position + 4); // Offset
        }
        private static void WriteBitmapHeader(BinaryWriter bw, int w, int h)
        {
            bw.Write((uint)40); // Sizeof
            bw.Write((int)w);
            bw.Write((int)h);
            bw.Write((short)1); // Planes
            bw.Write((short)1); // Bpp
            bw.Write((uint)0); // Compression
            bw.Write((uint)(DivCeiling(w, 32) * 4 * h)); // Bitmap data size
            bw.Write((int)0); // Pels/meter X (unused)
            bw.Write((int)0); // Pels/meter Y (unused)
            bw.Write((uint)2); // Colors used
            bw.Write((uint)0); // Colors important (all)
            bw.Write(0x00000000U); // Palette: Black
            bw.Write(0x00FFFFFFU); // Palette: White
        }
        private static void WriteBitmapData(BinaryWriter bw, bool[,] data)
        {
            int w = data.GetUpperBound(0) + 1;
            int h = data.GetUpperBound(1) + 1;

            uint dword = 0U;
            int shift = 31;

            for (int y = h - 1; y >= 0; y--) // Bitmaps are upside down.
            {
                for (int x = 0; x < w; x++)
                {
                    dword |= (data[x, y] ? 1U : 0U) << shift--;
                    if (shift < 0)
                    {
                        bw.Write(ChangeEndian(dword));
                        dword = 0U;
                        shift = 31;
                    }
                }

                // Write any remainder to finish the line.
                // Lines are DWORD aligned.
                if (shift != 31)
                {
                    bw.Write(ChangeEndian(dword));
                    dword = 0U;
                    shift = 31;
                }
            }
        }
        private static int DivCeiling(int a, int b)
        {
            int c = a / b;
            if (a % b != 0)
                c++;
            return c;
        }
        private static uint ChangeEndian(uint x)
        {
            return (x >> 24) |
              ((x << 8) & 0x00FF0000) |
              ((x >> 8) & 0x0000FF00) |
              (x << 24);
        }
    }
}
