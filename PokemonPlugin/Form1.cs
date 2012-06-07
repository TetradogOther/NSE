using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Plugins;
using NSE_Framework.Data;

namespace PokemonPlugin
{
    public partial class Form1 : Form
    {
            // PLUGIN HOST
            IPluginHost Host;

            // READ CLASS
            NSE_Framework.Read Read;
            // WRITE CLASS
            NSE_Framework.Write Write;

            bool CanNavigate;     // CAN WE NAVIGATE
            bool Navigating;      // NAVIGATE IS RUNNING

            #region ROM_SPECIFIC

        string Header;          // ROM HEADER

        int FrontTable;
        int BackTable;
        int NormalTable;
        int ShinyTable;

        int MiniSpriteTable;
        int MiniPaletteIndex;
        int MiniPalettes;

        int MAX;                // MAX SPRITES
        #endregion

            #region SPRITE STUFF
        // The sprites!
        NSE_Framework.Data.Sprite FN;
        NSE_Framework.Data.Sprite BN;
        NSE_Framework.Data.Sprite BS;
        NSE_Framework.Data.Sprite FS;

        NSE_Framework.Data.Sprite M1;
        NSE_Framework.Data.Sprite M2;

        Bitmap bFN = new Bitmap(64, 64);
        Bitmap bBN = new Bitmap(64, 64);
        Bitmap bFS = new Bitmap(64, 64);
        Bitmap bBS = new Bitmap(64, 64);
        Bitmap bM1 = new Bitmap(32, 32);
        Bitmap bM2 = new Bitmap(32, 32);
        #endregion

        #region FORM_FUNCTIONS_LOAD
        public Form1(Plugins.IPluginHost Host)
        {
            // LOADS THE HOST
            this.Host = Host;
            InitializeComponent();
        }
        private void LoadForm(object sender, EventArgs e)
        {        
            if (Host.Filename != null && Host.Filename != null)
            {
                //Creates a new read object
                Read = new NSE_Framework.Read(Host.Filename);

                //Creates a new write object
                Write = new NSE_Framework.Write(Host.Filename);

                //see if were eddting a pokemon game
                if (Read.ReadString(0xA0, 7).ToLower() == "pokemon")
                {
                    //gets the header
                    Header = Read.ReadString(0xAC, 4);

                    if (Header.Substring(0, 2) == "BP")
                    {
                        #region FIRERED_LEAFGREEN_EMERALD
                        FrontTable = Read.ReadPointer(0x128);

                        BackTable = Read.ReadPointer(0x12C);

                        NormalTable = Read.ReadPointer(0x130);

                        ShinyTable = Read.ReadPointer(0x134);

                        MiniSpriteTable = Read.ReadPointer(0x138);

                        MiniPaletteIndex = Read.ReadPointer(0x13C);

                        MiniPalettes = Read.ReadPointer(0x140);


                        MAX = 439;

                        if (NSE2.Program.MainForm.SafetyRepointing == false)
                        {
                            if (MessageBox.Show(this, "It seems that SafetyRepointing is disabled:\nIt's suggested that you enable SafetyRepointing for best results.\nEnable SafetyRepointing?", "Notice:", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                            {
                                NSE2.Program.MainForm.SafetyRepointing = true;
                            }
                        }

                        CanNavigate = true;
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("Currently this plugin only supports Firered, Leafgreen and Emerald.\n-Link12552", "Notice:", MessageBoxButtons.OK);                       
                    }
                    Navigate();
                    NumericUpDownPalette.Enabled = true;

                    // Wires the EDITBOX to NSE
                    Host.SetEditor(ref EditBox);
                }

            }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Tells the plugin that it has been closed
            // This is done to make sure only one pokemon plugin is open at a time
            PokemonPlugin.Program.opened = false;
        }
        #endregion

        // The NAVIGATE function
        void Navigate()
        {
            // Can we Navigate?
            if (Header!= null && Header != "" && FrontTable != null)
            {
                // Say we're currently naviagting
                Navigating = true;

                // Create a new list of sprites in our EditBox
                EditBox.Sprites = new List<NSE_Framework.Data.Sprite>(6);

                #region Variables
                // The current POKEMON index
                int INDEX = int.Parse(IndexText.Text);

                // Define Variables
                int heightFront, heightBack;                                    // The height of the front sprite and back sprite 
                int lzSprite, normalLzPalette, shinyLzPalette;                  // Stores length of Lz77 compressed data ! RETURNS -1 IF NOT LZ77 COMPRESSED !                                                                              
                int spriteOffset, normalPaletteOffset, shinyPaletteOffset;      // Temp sprite offset and palette offset variables                                                                                
                byte[] spriteData, normalPaletteData, shinyPaletteData;         // Temp sprite data and palette data byte[]
                #endregion

                #region FRONT_NORMAL
                // Offsets of front sprite and palette
                spriteOffset = Read.ReadPointer(FrontTable + 8 * INDEX);
                normalPaletteOffset = Read.ReadPointer(NormalTable + 8 * INDEX);

                // Front sprite's height
                heightFront = Lz77.CheckLz77(Read, spriteOffset, CheckLz77Type.Sprite) / 256;
                
                // Read and decompress data from offsets, output the lengths
                spriteData = Read.ReadLz77Bytes(spriteOffset,out lzSprite);
                normalPaletteData = Read.ReadLz77Bytes(normalPaletteOffset, out normalLzPalette);

                // Build FRONT_NORMAL sprite
                FN = new Sprite(spriteOffset,normalPaletteOffset,8,heightFront, Sprite.SpriteType.Color16, spriteData, new SpritePalette(SpritePalette.PaletteType.Color16, normalPaletteData));
                FN.CompressedSprite = lzSprite;
                FN.CompressedPalette = normalLzPalette;
                #endregion

                #region FRONT_SHINY
                // RE-USE LAST SPRITE_DATA

                // Offsets of shiny palette, RE-USE last sprite offset
                shinyPaletteOffset = Read.ReadPointer(ShinyTable + 8 * INDEX);

                // Read and decompress data from offset, output the length
                shinyPaletteData = Read.ReadLz77Bytes(shinyPaletteOffset, out shinyLzPalette);

                // Build FRONT_SHINY sprite
                FS = new Sprite(spriteOffset, shinyPaletteOffset, 8, heightFront, Sprite.SpriteType.Color16, spriteData, new SpritePalette( SpritePalette.PaletteType.Color16, shinyPaletteData));
                FS.CompressedSprite = lzSprite;
                FS.CompressedPalette = shinyLzPalette;
                #endregion

                #region BACK_SHINY
                // Offsets of back sprite, RE-USE last palette offset
                spriteOffset = Read.ReadPointer(BackTable + 8 * INDEX);

                // Back sprite's height
                heightBack = Lz77.CheckLz77(Read, spriteOffset, CheckLz77Type.Sprite) / 256;

                // Read and decompress data from offset, output the length
                spriteData = Read.ReadLz77Bytes(spriteOffset, out lzSprite);

                // Build BACK_SHINY sprite
                BS = new NSE_Framework.Data.Sprite(spriteOffset, shinyPaletteOffset, 8, heightBack, Sprite.SpriteType.Color16, spriteData, FS.Palette);
                BS.CompressedSprite = lzSprite;
                BS.CompressedPalette = shinyLzPalette;
                #endregion

                #region BACK_NORMAL

                // Build BACK_NORMAL sprite RE-USING all data
                BN = new Sprite(spriteOffset, normalPaletteOffset, 8, heightBack, Sprite.SpriteType.Color16, spriteData, FN.Palette);
                BN.CompressedSprite = lzSprite;
                BN.CompressedPalette = normalLzPalette;
                #endregion

                #region MINI
                // Read sprite pointer
                spriteOffset = Read.ReadPointer(MiniSpriteTable + 4 * INDEX);

                // Palette index 0 - 2
                byte pi = Read.ReadByte(MiniPaletteIndex + INDEX);

                // Read palette pointer
                normalPaletteOffset = Read.ReadPointer(MiniPalettes + 8 * pi);

                // Build both MINI frames
                M1 = new Sprite(spriteOffset,           normalPaletteOffset, 4, 4, Sprite.SpriteType.Color16, Read);
                M2 = new Sprite(spriteOffset + 0x200,   normalPaletteOffset, 4, 4, Sprite.SpriteType.Color16, Read);
                M2.Palette = M1.Palette;

                // Add the value to the numeric up down control
                NumericUpDownPalette.Value = pi;
                #endregion

                #region EDITBOX
                EditBox.Sprites.Add(FN);
                EditBox.Sprites.Add(BN);
                EditBox.Sprites.Add(FS);
                EditBox.Sprites.Add(BS);
                EditBox.Sprites.Add(M1);
                EditBox.Sprites.Add(M2);

                // MUST BE SET!
                EditBox.CurrentIndex = 0;
                #endregion

                #region PICTURES
                NSE_Framework.Draw.uDrawSprite(ref bFN, FN);
                NSE_Framework.Draw.uDrawSprite(ref bBN, BN);
                NSE_Framework.Draw.uDrawSprite(ref bFS, FS);
                NSE_Framework.Draw.uDrawSprite(ref bBS, BS);
                NSE_Framework.Draw.uDrawSprite(ref bM1, M1);
                NSE_Framework.Draw.uDrawSprite(ref bM2, M2);

                PictureboxFN.Image = bFN;
                PictureboxBN.Image = bBN;
                PictureboxFS.Image = bFS;
                PictureboxBS.Image = bBS;
                PictureboxA1.Image = bM1;
                PictureboxA2.Image = bM2;
                #endregion
                              
                // Say we're done navigating
                Navigating = false;

            }
        }

        // RE-WIRE EDITBOX TO NSE
        private void Form1_Enter(object sender, EventArgs e)
        {
            if (Header != null)
            {
                Host.SetEditor(ref EditBox);
            }
        }

        #region SPRITE_SWITCH
        private void PictureboxFN_Click(object sender, EventArgs e)
        {
            EditBox.CurrentIndex = 0;
        }
        private void PictureboxFS_Click(object sender, EventArgs e)
        {
            EditBox.CurrentIndex = 2;
        }
        private void PictureboxBN_Click(object sender, EventArgs e)
        {
            EditBox.CurrentIndex = 1;
        }
        private void PictureboxBS_Click(object sender, EventArgs e)
        {
            EditBox.CurrentIndex = 3;
        }
        private void PictureboxA1_Click(object sender, EventArgs e)
        {
            EditBox.CurrentIndex = 4;
        }
        private void PictureboxA2_Click(object sender, EventArgs e)
        {
            EditBox.CurrentIndex = 5;
        }
        #endregion

        #region NAVIGATOR_BOX
        private void ButtonNext_Click(object sender, EventArgs e)
        {
            if (int.Parse(IndexText.Text) < MAX)
            {
                IndexText.Text = (int.Parse(IndexText.Text) + 1).ToString();
            }
        }
        private void ButtonPrev_Click(object sender, EventArgs e)
        {
            if (int.Parse(IndexText.Text) > 0)
            {
                IndexText.Text = (int.Parse(IndexText.Text) - 1).ToString();
            }
        }
        private void IndexText_TextChanged(object sender, EventArgs e)
        {
            if (CanNavigate == true)
            {
                CanNavigate = false;


                if (IndexText.Text.Length == 0)
                {
                    IndexText.Text = "0";
                }
                else if (int.Parse(IndexText.Text) > MAX)
                {
                    IndexText.Text = MAX.ToString();
                }
                
                Navigate();

                CanNavigate = true;
            }
        }
        private void DigitKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)))
            {
                e.Handled = true;
            }
        }
        #endregion

        private void NumericUpDownPalette_ValueChanged(object sender, EventArgs e)
        {
            if (!Navigating)
            {
                int INDEX = int.Parse(IndexText.Text);
                byte P_INDEX = (byte)NumericUpDownPalette.Value;

                Write.WriteByte(P_INDEX, MiniPaletteIndex + INDEX);

                 // Read palette pointer
                int paletteOffset = Read.ReadPointer(MiniPalettes + 8 * P_INDEX);

                // Fix both MINI frames
                EditBox.Sprites[4].Palette = new SpritePalette(SpritePalette.PaletteType.Color16, Read.ReadBytes(paletteOffset, 32));
                EditBox.Sprites[5].Palette = EditBox.Sprites[4].Palette;

                NSE_Framework.Draw.uDrawSprite(ref bM1, EditBox.Sprites[4]);
                NSE_Framework.Draw.uDrawSprite(ref bM2, EditBox.Sprites[5]);

                PictureboxA1.Image = bM1;
                PictureboxA2.Image = bM2;

                if (EditBox.CurrentIndex == 4 || EditBox.CurrentIndex == 5)
                    EditBox.Redraw();
            }
        }
    }
}
