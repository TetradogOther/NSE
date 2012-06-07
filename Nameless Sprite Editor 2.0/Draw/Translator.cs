using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NSE2.Data
{
    public static class Translator
    {
        public static Palette GBAToPalette(byte byte1, byte byte2)
        {
            int val = (byte2 << 8) + byte1;

            Palette pal = new Palette();
            pal.Red = (byte)((val & 0x1f) << 3);
            pal.Green = (byte)(((val >> 5) & 0x1f) << 3);
            pal.Blue = (byte)(((val >> 10) & 0x1f) << 3);
            return pal;
            
        }


    }
}
