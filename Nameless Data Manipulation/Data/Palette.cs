using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE_Framework.Data
{
    [Serializable()]
    public class SpritePalette
    {
        public GBAcolor[] Colors;
        public readonly PaletteType Type;

        [Serializable()]
        public enum PaletteType
        {
            Color16,
            Color256
        }
       

        public SpritePalette(PaletteType Type)
        {
            if (Type == PaletteType.Color16)
            {
                this.Colors = new GBAcolor[16];
            }

            if (Type == PaletteType.Color256)
            {
                this.Colors = new GBAcolor[256];
            }
            this.Type = Type;
        }

        public SpritePalette(PaletteType Type, byte[] PaletteData)
        {
            if (Type == PaletteType.Color16)
            {
                this.Colors = new GBAcolor[16];
            }

            if (Type == PaletteType.Color256)
            {
                this.Colors = new GBAcolor[256];
            }
            this.Type = Type;

            for (int i = 0; i < PaletteData.Length && i / 2 < this.Colors.Length; i += 2)
            {
                this.Colors[i / 2] = Translator.ByteToPalette(PaletteData[i], PaletteData[i + 1]);
            }
        }

        public byte[] GetGBABytes
        {
            get
            {
                byte[] r = new byte[this.Colors.Length * 2];

                int index = 0;
                foreach (GBAcolor c in Colors)
                {
                    NSE_Framework.Data.Translator.PaletteToByte(c).CopyTo(r, index);
                    index += 2;
                }

                return r;
            }
        }


    }

    [Serializable()]
    public struct GBAcolor
    {
        //public byte[] Bytes;
        public byte Red;
        public byte Green;
        public byte Blue;

        public System.Drawing.Color Color
        {
            get { return System.Drawing.Color.FromArgb(Red, Green, Blue); }

            set
            {
                Red = value.R;
                Green = value.G;
                Blue = value.B;
            }
        }

        //public byte[] Red
        //{
        //    get { return Bytes[0]; }

        //    set { Bytes[0] = value; }
        //}
        //public byte Green
        //{
        //    get { return Bytes[1]; }

        //    set { Bytes[1] = value; }
        //}
        //public byte Blue
        //{
        //    get { return Bytes[2]; }

        //    set { Bytes[2] = value; }
        //}

        public GBAcolor(System.Drawing.Color Color)
        {
            this.Red = Color.R;
            this.Green = Color.G;
            this.Blue = Color.B;
        }

        public GBAcolor(byte Red, byte Green, byte Blue)
        {
            this.Red = Red;
            this.Green = Green;
            this.Blue = Blue;
        }

    }
}
