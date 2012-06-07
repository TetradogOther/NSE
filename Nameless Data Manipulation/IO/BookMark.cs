using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE_Framework.IO
{
    [Serializable()]
    public class BookMark
    {
        public string Name = "";
        public int ImageOffset = 0;
        public int PaletteOffset = 0;

        public bool Lz77 = false;

        public int Width = 1;
        public int Height = 1;

        public Data.Sprite.SpriteType SpriteType = Data.Sprite.SpriteType.Color16;

        public List<byte[]> Commands { get; set; }

        public BookMarkTree Parent = null;
        

        public BookMark(string Name, int ImageOffset, int PaletteOffset, int Width, int Height, Data.Sprite.SpriteType Type, bool Lz77 = false)
        {
            Commands = new List<byte[]>();

            this.Name = Name;
            this.ImageOffset = ImageOffset;
            this.PaletteOffset = PaletteOffset;
            this.Width = Width;
            this.Height = Height;
            this.SpriteType = Type;
            this.Lz77 = Lz77;
        }

    }
}
