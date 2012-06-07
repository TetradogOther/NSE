using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE2.Data
{
    public class SpritePalette
    {
        public Palette[] Colors;
        public readonly PaletteType Type;

        public enum PaletteType
        {
            Color16,
            Color256
        }

        public SpritePalette(PaletteType Type)
        {
            if (Type == PaletteType.Color16)
            {
                this.Colors = new Palette[16];
            }

            if (Type == PaletteType.Color256)
            {
                this.Colors = new Palette[256];
            }
            this.Type = Type;
        }

        public SpritePalette(PaletteType Type, byte[] PaletteData)
        {
            if (Type == PaletteType.Color16)
            {
                this.Colors = new Palette[16];
            }

            if (Type == PaletteType.Color256)
            {
                this.Colors = new Palette[256];
            }
            this.Type = Type;

            for (int i = 0; i < PaletteData.Length; i += 2)
            {
                this.Colors[i / 2] = Translator.GBAToPalette(PaletteData[i], PaletteData[i + 1]);
            }
        }


    }

    public class Palette
    {
        public byte[] Bytes;

        public System.Drawing.Color Color
        {
            get { return System.Drawing.Color.FromArgb(Red, Green, Blue); }
        }

        public byte Red
        {
            get { return Bytes[0]; }

            set { Bytes[0] = value; }
        }
        public byte Green
        {
            get { return Bytes[1]; }

            set { Bytes[1] = value; }
        }

        public byte Blue
        {
            get { return Bytes[2]; }

            set { Bytes[2] = value; }
        }

        public Palette(byte Red, byte Green, byte Blue)
        {
            Bytes = new byte[3];
            this.Red = Red;
            this.Green = Green;
            this.Blue = Blue;
        }

        public Palette(byte[] Bytes)
        {
            this.Bytes = Bytes;
        }

        public Palette()
        {
            Bytes = new byte[3];
        }
    }
}
