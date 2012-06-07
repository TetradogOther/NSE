using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE_Framework.Data
{
    [Serializable]
    public class Sprite
    {

        int imageOffset;
        public int ImageOffset
        {
            get { return imageOffset; }
            set
            {
                if (value >= 0)
                    imageOffset = value;
            }
        }

        int paletteOffset;
        public int PaletteOffset
        {
            get { return paletteOffset; }
            set
            {
                if (value >= 0)
                    paletteOffset = value;
            }
        }

        public readonly int Width;
        public readonly int Height;
        public byte[] ImageData;
        public SpritePalette Palette;
        public readonly SpriteType Type;

        [Serializable]
        public enum SpriteType
        {
            Color16,
            Color256
        }

        public bool UniqueImage
        {
            get
            {
                if (imageOffset >= 0)
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
                if (paletteOffset >= 0)
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
                this.imageOffset = ImageOffset;
                this.paletteOffset = PaletteOffset;
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
            this.imageOffset = ImageOffset;
            this.paletteOffset = PaletteOffset;
            this.Width = Width;
            this.Height = Height;
            if (Type == SpriteType.Color16)
            {
                //Checks for regular and compressed data
                #region Color16
                if (Lz77.CheckLz77(Read, ImageOffset, CheckLz77Type.Sprite) == -1)
                {
                    this.ImageData = Read.ReadBytes(ImageOffset, Width * Height * 32);
                }
                else
                {
                    this.ImageData = Read.ReadLz77Bytes(ImageOffset, out this.CompressedSprite);
                }



                if (Lz77.CheckLz77(Read, PaletteOffset, CheckLz77Type.Palette) == 32)
                {
                    this.Palette = new SpritePalette(SpritePalette.PaletteType.Color16,
                        Read.ReadLz77Bytes(PaletteOffset, out this.CompressedPalette));
                }
                else
                {
                    this.Palette = new SpritePalette(SpritePalette.PaletteType.Color16, Read.ReadBytes(PaletteOffset, 32));
                }
               
                this.Type = Type;
                #endregion
            }
            else if (Type == SpriteType.Color256)
            {
                //I'm not sure why, but I never completed this part
                //Fixed - 12/21/2011
                #region Color256
                if (Lz77.CheckLz77(Read, ImageOffset, CheckLz77Type.Sprite) == -1)
                {
                    this.ImageData = Read.ReadBytes(ImageOffset, Width * Height * 64);
                }
                else
                {
                    this.ImageData = Read.ReadLz77Bytes(ImageOffset, out this.CompressedSprite);
                }


                if (Lz77.CheckLz77(Read, PaletteOffset, CheckLz77Type.Palette) == 256)
                {
                    this.Palette = new SpritePalette(SpritePalette.PaletteType.Color256,
                        Read.ReadLz77Bytes(PaletteOffset, out this.CompressedPalette));
                }
                else
                {
                    this.Palette = new SpritePalette(SpritePalette.PaletteType.Color256, Read.ReadBytes(PaletteOffset, 512));
                }
                this.Type = Type;
                #endregion
            }



        }

        public Sprite(int Width, int Height, SpriteType Type)
        {
            this.imageOffset = -1;
            this.paletteOffset = -1;
            this.Width = Width;
            this.Height = Height;
            if (Type == SpriteType.Color16)
            {
                this.ImageData = new byte[Width * Height *32];
                this.Palette = new SpritePalette(SpritePalette.PaletteType.Color16, new byte[32]);
                this.Type = Type;
            }
            else if (Type == SpriteType.Color256)
            {
                this.ImageData = new byte[Width * Height * 64];
                this.Palette = new SpritePalette(SpritePalette.PaletteType.Color256, new byte[512]);
                this.Type = Type;
            }
        }
    }
}
