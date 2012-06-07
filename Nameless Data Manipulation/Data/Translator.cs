using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace NSE_Framework.Data
{
    public static class Translator
    {
        public static GBAcolor ByteToPalette(byte byte1, byte byte2)
        {
            int val = (byte2 << 8) + byte1;

            GBAcolor pal = new GBAcolor();
            pal.Red = (byte)((val & 0x1f) << 3);
            pal.Green = (byte)(((val >> 5) & 0x1f) << 3);
            pal.Blue = (byte)(((val >> 10) & 0x1f) << 3);
            return pal;
            
        }

        public static byte[] PaletteToByte(GBAcolor Palette)
        {
            //int val = Palette.
            byte[] b = new byte[2];
            b[0] = (byte)( (byte)(Palette.Red / 8) + ( (byte)( (Palette.Green /8) & 0x7 )<< 5 ) ) ;
            b[1] = (byte)( ( ( (byte)(Palette.Blue / 8) ) << 2) + ( (byte)(Palette.Green / 8) >> 3 ) );

            return b;

        }

        public static SpritePalette.PaletteType SpriteToPaletteType(Sprite.SpriteType SpriteType)
        {
            if (SpriteType == Sprite.SpriteType.Color16)
            {
                return SpritePalette.PaletteType.Color16;
            }
            else if (SpriteType == Sprite.SpriteType.Color256)
            {
                return SpritePalette.PaletteType.Color256;
            }
            else
            {
                throw new Exception("Unknown type");
            }
        }

        public static Sprite.SpriteType PaletteToSpriteType(SpritePalette.PaletteType PaletteType)
        {
            if (PaletteType == SpritePalette.PaletteType.Color16)
            {
                return Sprite.SpriteType.Color16;
            }
            else if (PaletteType == SpritePalette.PaletteType.Color256)
            {
                return Sprite.SpriteType.Color256;
            }
            else
            {
                throw new Exception("Unknown type");
            }
        }

        public static int Position2Index(Size Size, Point Position, NSE_Framework.Data.Sprite.SpriteType Type)
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

    }
}
