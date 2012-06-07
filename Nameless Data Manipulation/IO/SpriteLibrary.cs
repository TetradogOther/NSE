using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NSE_Framework.IO
{
    [Serializable()]
     public class SpriteLibrary 
    {
        byte[] nslx = {1,0,0,0};

        public byte[] NSLx
        {
            get { return nslx; }
        }

        public List<SpriteSet> Sprites;
        string origin = "GBA";
        public string Origin { get { return origin; } }

        public SpriteLibrary(string Origin)
        {
            this.origin = Origin;
            Sprites = new List<SpriteSet>();
        }

    }

    [Serializable()]
     public class SpriteSet 
    {
        public NSE_Framework.Data.SpritePalette Palette;

        int width;
        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        int height;
        public int Height
        {
            get { return height; }
            set{height = value;}
        }

        public string Name = "Sprite Set";

        public List<SpriteData> SpriteData;

        public SpriteSet(string Name, NSE_Framework.Data.SpritePalette Palette, int Width, int Height)
        {
            this.Name = Name;
            this.Palette = Palette;
            this.width = Width;
            this.height = Height;
            SpriteData = new List<SpriteData>();
        }

    }
    [Serializable()]
     public class SpriteData
    {
        public string Name;
        public byte[] Data;
        public bool Compressed = false;

        public SpriteData(string Name, byte[] Data, bool Compressed = false)
        {
            this.Name = Name;
            this.Data = Data;
            this.Compressed = Compressed;
        }
    }
}
