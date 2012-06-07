using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE2.Data
{
    public class Sprite
    {
        public readonly int ImageOffset;
        public readonly int PaletteOffset;
        public readonly int Width;
        public readonly int Height;
        public byte[] ImageData;
        public SpritePalette Palette;
        public readonly SpriteType Type;

        public enum SpriteType
        {
            Color16,
            Color256
        }

        public bool UniqueImage
        {
            get
            {
                if (ImageOffset >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool UniquePalette
        {
            get
            {
                if (PaletteOffset >= 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public int CompressedSprite = -1;
        public int CompressedPalette = -1;

        public Sprite(int ImageOffset, int PaletteOffset, int Width, int Height, SpriteType Type, byte[] ImageData, SpritePalette Palette)
        {
            if ((int)Type == (int)Palette.Type)
            {
                this.ImageOffset = ImageOffset;
                this.PaletteOffset = PaletteOffset;
                this.Width = Width;
                this.Height = Height;
                this.ImageData = ImageData;
                this.Palette = Palette;
                this.Type = Type;
            }
            else
            {
                throw new Exception("Sprite and Palette types must match.");
            }

        }

        public Sprite(int ImageOffset, int PaletteOffset, int Width, int Height, SpriteType Type, Read Read)
        {
            this.ImageOffset = ImageOffset;
            this.PaletteOffset = PaletteOffset;
            this.Width = Width;
            this.Height = Height;
            if (Type == SpriteType.Color16)
            {
                this.ImageData = Read.ReadBytes(ImageOffset, Width * Height * 32);
                this.Palette = new SpritePalette(SpritePalette.PaletteType.Color16, Read.ReadBytes(PaletteOffset, 32));
                this.Type = Type;
            }



        }

        public Sprite(int Width, int Height, SpriteType Type)
        {
            this.ImageOffset = -1;
            this.PaletteOffset = -1;
            this.Width = Width;
            this.Height = Height;
            if (Type == SpriteType.Color16)
            {
                this.ImageData = new byte[Width * Height / 2];
                this.Palette = new SpritePalette(SpritePalette.PaletteType.Color16, new byte[32]);
                this.Type = Type;
            }
        }
    }
}
