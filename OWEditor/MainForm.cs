using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Plugins;

namespace OWEditor
{
    public partial class MainForm : Form
    {
        IPluginHost Host;
        public MainForm(IPluginHost Host)
        {
            InitializeComponent();
            this.Host = Host;

            read = new NSE_Framework.Read(Host.Filename);
            write = new NSE_Framework.Write(Host.Filename);
        }

        NSE_Framework.Read read;
        NSE_Framework.Write write;

        int Index = 0;
        int Frame = 0;

        public string RomHeader;
        List<byte> foundPalettes;

        int SpriteTable = 0x39FDB0;
        int PaletteTable = 0x3A5158;

        public byte sPaletteIndex;
        public int sHeaderOffset = 0;
        public int sWidth = 0;
        public int sHeight = 0;

        public int sAnimPointer = 0;
        public int sPointer1 = 0;
        public int sPointer2 = 0;
        public int sPointer3 = 0;
        public int ImageOffset = 0;
        public int PaletteOffset = 0;
        public byte[] cFrameSize;

        public Bitmap Sprite;
        public byte[] byteSprite;
        public NSE_Framework.Data.SpritePalette SpritePalette;

        byte    EditColorIndex
        {
            get { return EditColorIndex; }
            set
            {
                if (value < 16)
                {
                    iSelectedColor.BackColor = SpritePalette.Colors[value].Color;
                    iSelectedIndex.Text = "Index: 0" + value.ToString("X");
                    editColorIndex = value;
                }
            }
        }
        int     EditBlock
        {
            get { return EditBlock; }
            set
            {
                if (value >= 0)
                {                    
                    editBlock = value;
                    fillCanvas();
                }
            }
        }
        byte    editColorIndex;
        int     editBlock;



        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.opened = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.RomHeader = read.ReadString(0xAC, 4);
            findTables(this.RomHeader);
            if (this.SpriteTable == 0x0)
            {
                this.Close();
                return;
            }

            #region FindPalette
            foundPalettes = new List<byte>();


            byte indicator = 0x1;
            int take = 0;
            byte b;
            while (indicator != 0x0)
            {
                b = read.ReadByte(PaletteTable + 8 * take + 4) ;

                if (!foundPalettes.Contains(b))
                    foundPalettes.Add(b);

                indicator = read.ReadByte(PaletteTable + 8 * take + 5);
                take++;
            }
            #endregion


            iEditBox.Image = new Bitmap(128, 128);
            iPaletteBox.Image = new Bitmap(32, 128);
            editColorIndex = 0;
           
            navigate();
            
            LabelInfo.Text = this.RomHeader + " - " + read.ReadString(0xA0, 12);
            //this.Edit.Zoom = 2;
            
        }
        void findTables(string header)
        {
            string headershort = header.Substring(0, 3);
            if (header == "BPRE")
            {
                SpriteTable = 0x39FDB0;
                PaletteTable = 0x3A5158;
            }
            else if (header == "BPRS")
            {
                SpriteTable = 0x39B518;
                PaletteTable = 0x3A08C0;
            }
            else if (header == "AXVE")
            {
                SpriteTable = 0x36DC58;
                PaletteTable = 0x37377C;
            }
            else if (header == "BPEE")
            {
                SpriteTable = 0x505620;
                PaletteTable = 0x50BBC8;
            }
            else if (headershort == "BPR" || headershort == "BPG" || headershort == "AXV" || headershort == "AXP" || headershort == "BPE")
            {
                this.SpriteTable = 0x0;
                MessageBox.Show(this, "You've tried to open a pokemon rom that's not yet supported.\nTo add support yourself, check the readme included with this plugin.\n\nIf you want to help get this header officialy supported, open up this rom in NSE [classic],\n In NSE [classic], click View>>HexEditor, then from the drop-down menu at the top of the hexeditor choose SpriteTable, record the offset, do the same for the PaletteTable.\n Once you have these offsets, post them and your rom header: " + header + " in the NSE 2.X project page online at http://www.lastlink.tk\n\n-Thanks",
                    "This ROM is not supported... Yet!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.SpriteTable = 0x0;
                MessageBox.Show(this, "This is either not a supported Pokemon rom, or you've changed the rom header.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void navigate()
        {
            sHeaderOffset = BitConverter.ToInt32(read.ReadBytes(SpriteTable + 4 * Index, 4), 0) - 0x8000000;

            byte[] HeaderBytes = read.ReadBytes(sHeaderOffset, 36);

            sWidth = BitConverter.ToInt32(new byte[4] { HeaderBytes[8], HeaderBytes[9], 0, 0 }, 0);
            sHeight = BitConverter.ToInt32(new byte[4] { HeaderBytes[10], HeaderBytes[11], 0, 0 }, 0);

            sPaletteIndex = HeaderBytes[2];

            sPointer1 = BitConverter.ToInt32(new byte[4] { HeaderBytes[16], HeaderBytes[17], HeaderBytes[18], HeaderBytes[19] }, 0) - 0x8000000;
            sPointer2 = BitConverter.ToInt32(new byte[4] { HeaderBytes[20], HeaderBytes[21], HeaderBytes[22], HeaderBytes[23] }, 0) - 0x8000000;
            sPointer3 = BitConverter.ToInt32(new byte[4] { HeaderBytes[28], HeaderBytes[29], HeaderBytes[30], HeaderBytes[31] }, 0) - 0x8000000;

            ImageOffset = read.ReadPointer(sPointer3 + 8 * Frame);
            cFrameSize = read.ReadBytes(sPointer3 + 8 * Frame + 4, 2);

            #region FindPalette
            PaletteOffset = read.ReadPointer(PaletteTable);
            
            byte indicator = 0x1;
            int take = 0;
            while (indicator != 0x0)
            {
                if (read.ReadByte(PaletteTable + 8 * take + 4) == sPaletteIndex)
                {
                    PaletteOffset = read.ReadPointer(PaletteTable + 8 * take);
                    indicator = 0x0;
                }
                else
                {
                    indicator = read.ReadByte(PaletteTable + 8 * take + 5);
                    take++;
                }
            }
            #endregion

            NSE_Framework.Data.Sprite OW = new NSE_Framework.Data.Sprite(ImageOffset, PaletteOffset, sWidth / 8, sHeight / 8, NSE_Framework.Data.Sprite.SpriteType.Color16, read);

            Sprite = new Bitmap(sWidth, sHeight);//new Bitmap(sWidth * 2, sHeight * 2);
            byteSprite = read.ReadBytes(ImageOffset, sWidth * sHeight / 2);

            SpritePalette = new NSE_Framework.Data.SpritePalette(NSE_Framework.Data.SpritePalette.PaletteType.Color16, read.ReadBytes(PaletteOffset, 32));

            NSE_Framework.Draw.uDrawData(ref Sprite, byteSprite, SpritePalette, new Size(sWidth / 8, sHeight / 8));
            PictureBoxSprite.Image = Sprite;
            PictureBoxSprite.BackColor = SpritePalette.Colors[0].Color;
            PictureBoxSprite.Size = new System.Drawing.Size(sWidth * 2, sHeight * 2);
            PictureBoxSprite.Invalidate();

            #region Labels
            LabelPaletteOffset.Text = "Palette "+ sPaletteIndex.ToString("X2") + " Offset:";

            dLabelStarterBytes.Text     = HeaderBytes[0].ToString("X2") + HeaderBytes[1].ToString("X2");
            // Byte 2 is the palette index
            // Byte 3 is always 0x11
            dLabelTypeSize.Text         = HeaderBytes[4].ToString("X2") + HeaderBytes[5].ToString("X2");
            dLabelFrameSize.Text        = HeaderBytes[7].ToString("X2") + HeaderBytes[6].ToString("X2");
            // Bytes 8,9,10,11 are width and height
            dLabelWidth.Text            = sWidth.ToString();
            dLabelHeight.Text           = sHeight.ToString();
            
            dLabelPalReg.Text           = HeaderBytes[12].ToString("X2") + "-" + HeaderBytes[13].ToString("X2") + " "  +  HeaderBytes[14].ToString("X2") + HeaderBytes[15].ToString("X2");
            dLabelPointer1.Text         = HeaderBytes[19].ToString("X2") + HeaderBytes[18].ToString("X2") + HeaderBytes[17].ToString("X2") + HeaderBytes[16].ToString("X2");
            dLabelPointer2.Text         = HeaderBytes[23].ToString("X2") + HeaderBytes[22].ToString("X2") + HeaderBytes[21].ToString("X2") + HeaderBytes[20].ToString("X2");
            dLabelAnim.Text             = HeaderBytes[27].ToString("X2") + HeaderBytes[26].ToString("X2") + HeaderBytes[25].ToString("X2") + HeaderBytes[24].ToString("X2");
            dLabelPointer3.Text         = HeaderBytes[31].ToString("X2") + HeaderBytes[30].ToString("X2") + HeaderBytes[29].ToString("X2") + HeaderBytes[28].ToString("X2");

            dLabelSpriteOffset.Text     = "0x" + sHeaderOffset.ToString("X2");
            dLabelImageOffset.Text      = "0x" + ImageOffset.ToString("X2");
            dLabelPaletteOffset.Text    = "0x" + PaletteOffset.ToString("X2");
            dLabelCFrameSize.Text       = cFrameSize[1].ToString("X2") + cFrameSize[0].ToString("X2");

            TextBoxPalette.Text = sPaletteIndex.ToString("X2");
            #endregion

            editBlock = 0;
            drawBlock();
            fillCanvas();
            fillPalette();

            iSelectedColor.BackColor = SpritePalette.Colors[editColorIndex].Color;

        }

        void fillCanvas()
        {
            int s = editBlock * 32;
            int x = 0;
            int y = 0;
            Graphics g = Graphics.FromImage(iEditBox.Image);
            g.FillRectangle(Brushes.Black, 0, 0, 128, 128);
            for (int i = 0; i < 32; i++)
            {
                if (!MenuItemGridCanvas.Checked)
                {
                    g.FillRectangle(new SolidBrush(SpritePalette.Colors[byteSprite[s + i] & 0xf].Color), x * 16, y * 16, 16, 16);
                    g.FillRectangle(new SolidBrush(SpritePalette.Colors[byteSprite[s + i] >> 4].Color), x * 16 + 16, y * 16, 16, 16);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(SpritePalette.Colors[byteSprite[s + i] & 0xf].Color), x * 16, y * 16, 15, 15);
                    g.FillRectangle(new SolidBrush(SpritePalette.Colors[byteSprite[s + i] >> 4].Color), x * 16 + 16, y * 16, 15, 15);
                }
                if (x > 4)
                {
                    x = 0;
                    y++;
                }
                else
                {
                    x+= 2;
                }
            }

            iEditBox.Refresh();
        }
        void fillPalette()
        {
            Graphics g = Graphics.FromImage(iPaletteBox.Image);
            int x = 0;
            int y = 0;
            g.FillRectangle(Brushes.Black, 0, 0, 32, 128);
            for (int i = 0; i < 16; i++)
            {
                if (!MenuItemGridPalette.Checked)
                {
                    g.FillRectangle(new SolidBrush(SpritePalette.Colors[i].Color), x, y, 16, 16);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(SpritePalette.Colors[i].Color), x, y, 15, 15);
                }

                if (x > 1)
                {
                    x = 0;
                    y += 16;
                }
                else
                {
                    x = 16;
                }
            }

            iPaletteBox.Refresh();
        }
        void drawBlock()
        {
            int x = editBlock % (sWidth / 8) * 16;
            int y = editBlock / (sWidth / 8) * 16;
            PictureBoxSprite.Refresh();
            Graphics g = PictureBoxSprite.CreateGraphics();
            g.DrawRectangle(Pens.Red, x, y, 15, 15);
        }

        private void ButtonIndexNext_Click(object sender, EventArgs e)
        {
            Index++;
            IndexText.Text = Index.ToString();
            
            Frame = 0;
            FrameText.Text = "0";

            navigate();
        }
        private void ButtonIndexPrev_Click(object sender, EventArgs e)
        {
            if (Index > 0)
            {
                Index--;
            }
            IndexText.Text = Index.ToString();

            Frame = 0;
            FrameText.Text = "0";

            navigate();
        }
        private void ButtonFrameNext_Click(object sender, EventArgs e)
        {
            Frame++;
            FrameText.Text = Frame.ToString();
            navigate();
        }
        private void ButtonFramePrev_Click(object sender, EventArgs e)
        {
            if(Frame > 0)
                Frame--;

            FrameText.Text = Frame.ToString();
            navigate();
        }
        private void ButtonGo_Click(object sender, EventArgs e)
        {

        }

        private void MenuItemGridPalette_Click(object sender, EventArgs e)
        {
            if (MenuItemGridPalette.Checked)
                MenuItemGridPalette.Checked = false;
            else
                MenuItemGridPalette.Checked = true;

            fillPalette();
        }
        private void MenuItemGridCanvas_Click(object sender, EventArgs e)
        {
            if (MenuItemGridCanvas.Checked)
                MenuItemGridCanvas.Checked = false;
            else
                MenuItemGridCanvas.Checked = true;

            fillCanvas();
        }

        private void iPaletteBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int x = iPaletteBox.PointToClient(Cursor.Position).X;
                int y = iPaletteBox.PointToClient(Cursor.Position).Y;
                EditColorIndex = (byte)(x / 16 + (y - y % 16) / 8);
            }
        }
        private void iEditBoxMain(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int x = iEditBox.PointToClient(Cursor.Position).X;
                int y = iEditBox.PointToClient(Cursor.Position).Y;

                if (x > 127)
                    x = 127;
                if (y > 127)
                    y = 127;

                if (x >= 0 && y >= 0)
                {

                    int index = editBlock * 32 + (x - x % 32) / 32 + (y - y % 16) / 4;

                    if ((x / 16) % 2 == 0)
                    {
                        byteSprite[index] = (byte)((byteSprite[index] & 0xf0) + editColorIndex);
                    }
                    else
                    {
                        byteSprite[index] = (byte)((byteSprite[index] & 0x0f) + (editColorIndex << 4));
                    }
                    iOverColor.BackColor = SpritePalette.Colors[editColorIndex].Color;

                    Graphics g = Graphics.FromImage(iEditBox.Image);
                    if (!MenuItemGridCanvas.Checked)
                    {
                        g.FillRectangle(new SolidBrush(SpritePalette.Colors[editColorIndex].Color),
                            x - x % 16, y - y % 16, 16, 16);
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(SpritePalette.Colors[editColorIndex].Color),
                            x - x % 16, y - y % 16, 15, 15);
                    }

                    g = Graphics.FromImage(PictureBoxSprite.Image);
                    g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;

                    int gx = (index % 4) * 4;
                    if ((x / 16) % 2 == 1)
                        gx += 2;
                    gx += 16 * (editBlock % (sWidth / 8));

                    int gy = index % 32;
                    gy = (gy - gy % 4) / 2;
                    gy += 16 * (editBlock - (editBlock % (sWidth / 8))) / (sWidth / 8);

                    if (editColorIndex == 0)
                    {
                        g.FillRectangle(Brushes.Transparent,
                            gx, gy, 2, 2);
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(SpritePalette.Colors[editColorIndex].Color),
                            gx, gy, 2, 2);
                            
                    }

                    drawBlock();
                    iEditBox.Refresh();
                }
                
                
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                int x = iEditBox.PointToClient(Cursor.Position).X;
                int y = iEditBox.PointToClient(Cursor.Position).Y;

                if (x > 127)
                    x = 127;
                if (y > 127)
                    y = 127;
                if (x >= 0 && y >= 0)
                {
                    int index = editBlock * 32 + (x - x % 32) / 32 + (y - y % 16) / 4;

                    if ((x / 16) % 2 == 0)
                    {
                        EditColorIndex = (byte)(byteSprite[index] & 0x0f);
                    }
                    else
                    {
                        EditColorIndex = (byte)((byteSprite[index] & 0xf0) >> 4);
                    }
                }
            }
            else
            {
                int x = iEditBox.PointToClient(Cursor.Position).X;
                int y = iEditBox.PointToClient(Cursor.Position).Y;

                if (x > 127)
                    x = 127;
                if (y > 127)
                    y = 127;
                if (x >= 0 && y >= 0)
                {
                    int index = editBlock * 32 + (x - x % 32) / 32 + (y - y % 16) / 4;

                    if ((x / 16) % 2 == 0)
                    {
                        iOverColor.BackColor = SpritePalette.Colors[(byteSprite[index] & 0x0f)].Color;
                    }
                    else
                    {
                        iOverColor.BackColor = SpritePalette.Colors[((byteSprite[index] & 0xf0) >> 4)].Color;
                    }
                }
            }
        }
        private void PictureBoxSprite_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int x = PictureBoxSprite.PointToClient(Cursor.Position).X;
                int y = PictureBoxSprite.PointToClient(Cursor.Position).Y;

                EditBlock = (x - x % 16) / 16 + (y - y % 16) / 16 * sWidth / 8;
                drawBlock();
            }
        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            byte newPalette = byte.Parse(TextBoxPalette.Text, System.Globalization.NumberStyles.HexNumber);

            if (foundPalettes.Contains(newPalette) || !MenuItemBoundPalette.Checked)
            {
                //Write the palette byte
                write.WriteByte(newPalette, sHeaderOffset + 2);

                //Adjust the palette reg
                byte a = CalculatePalReg(newPalette);
                if(a != 0xFF)
                    write.WriteByte(a, sHeaderOffset + 12);
                navigate();
            }
            else
            {
                //generate a list of palettes in a string
                string palettes = foundPalettes[0].ToString("X");

                for (int i = 1; i < foundPalettes.Count; i++)
                {
                    palettes += ", " + foundPalettes[i].ToString("X");
                }
                
                //Yep, there is no palette "9000!" but here's a list of actual palettes:
                MessageBox.Show(this, "Palette selected is not a valid palette for the current ROM.\n\nValid Palettes:\n" + palettes, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                TextBoxPalette.Text = sPaletteIndex.ToString("X2");
            }
        }
        byte CalculatePalReg(byte pal)
        {
            //I'm not exactly sure why these need to be changed.
            //All I know is they do, and there is some pattern to it.
            //This is where we change it.
            string shortHeader = this.RomHeader.Substring(0, 3);
            if (shortHeader == "BPR" || shortHeader == "BPG")
            {
                #region FrLg
                byte st = read.ReadByte(sHeaderOffset + 13);
                //0 item
                //1 normal person
                //2 special person


                // The small item-type OW have a 0 as the second byte in their palette-reg
                if (st == 0 && Width < 64)
                {
                    if (pal == 0)
                        return 0x10;
                    else if (pal == 0x0A)
                        return 0x19;
                    else if (pal == 0x0B)
                        return 0x1A;
                    else if (pal == 0x0F)
                        return 0x48;
                    else if (pal == 0x13)
                        return 0x4A;
                    else
                    {
                        //This is a pattern for smaller sprites
                        return (byte)(0x3F + pal);
                    }
                }
                else
                {
                    if (pal == 0)
                        return 0x10;
                    else if (pal == 1 || pal == 0x11)
                        return 0x01;
                    else if (pal == 2)
                        return 0x0A;
                    else if (pal == 0x15 || pal == 0x14)
                        return 0x1A;
                    else
                    {
                        if (sWidth < 64)
                        {
                            //This is the pattern for regular sprites
                            return (byte)(0xF + pal);
                        }
                        else
                        {
                            //This is the pattern for Large boat-like sprites
                            return (byte)(0x6 + pal);
                        }
                    }
                }
                #endregion
            }
            else if (shortHeader == "AXV" || shortHeader == "AXP" || shortHeader == "BPE")
            {
                #region RSE
                byte st = read.ReadByte(sHeaderOffset + 13);
                //0 item
                //1 normal person
                //2 special person


                // The small item-type OW have a 0 as the second byte in their palette-reg
                if (st == 0 && Width < 64)
                {
                    #region small
                    if (pal == 0x01)
                        return 0x11;
                    else if (pal == 0x02)
                        return 0x0A;
                    else if (pal == 0x06)
                        return 0x15;
                    else if (pal == 0x07)
                        return 0x16;
                    else if (pal == 0x08)
                        return 0x17;
                    else if (pal == 0x09)
                        return 0x18;
                    else if (pal == 0x0A)
                        return 0x19;
                    else if (pal == 0x0B)
                        return 0x2A;
                    else if (pal == 0x0B)
                        return 0x6A;
                    else if (pal == 0x0D)
                        return 0x5A;
                    else if (pal == 0x0E)
                        return 0x1A;
                    else if (pal == 0x0F)
                        return 0x1A;
                    else if (pal == 0x10)
                        return 0x10;
                    else if (pal == 0x11)
                        return 0x3A;
                    else if (pal == 0x12)
                        return 0x4A;
                    else if (pal == 0x13)
                        return 0x5A;
                    else if (pal == 0x15)
                        return 0x1A;
                    else
                    {
                        //This is a pattern for smaller sprites
                        return (byte)(0x3F + pal);
                    }
                    #endregion
                }
                else
                {
                    #region normal
                    if (pal == 0x00)
                        return 0x10;
                    else if (pal == 0x01)
                        return 0x11;
                    else if (pal == 0x02)
                        return 0x0A;
                    else if (pal == 0x03)
                        return 0x12;
                    else if (pal == 0x04)
                        return 0x13;
                    else if (pal == 0x05)
                        return 0x14;
                    else if (pal == 0x06)
                        return 0x15;
                    else if (pal == 0x07)
                        return 0x16;
                    else if (pal == 0x08)
                        return 0x17;
                    else if (pal == 0x09)
                        return 0x18;
                    else if (pal == 0x0A)
                        return 0x19;
                    else if (pal == 0x0B)
                        return 0x2A;
                    else if (pal == 0x0C)
                        return 0x6A;
                    else if (pal == 0x0D)
                        return 0x5A;
                    else if (pal == 0x0E)
                        return 0x1A;
                    else if (pal == 0x0F)
                        return 0x7A;
                    else if (pal == 0x10)
                        return 0x10;
                    else if (pal == 0x11)
                        return 0x3A;
                    else if (pal == 0x12)
                        return 0x4A;
                    else if (pal == 0x13)
                        return 0x5A;
                    else if (pal == 0x14)
                        return 0x1A;
                    else if (pal == 0x15)
                        return 0x90;
                    #endregion
                }
                #endregion
            }

            //Nope, IDK what could have happend, time to bail!
            return 0xFF;
        }

        private void HexKeyPress(object sender, KeyPressEventArgs e)
        {
            if (
                !(
                Char.IsDigit(e.KeyChar) ||
                Char.IsControl(e.KeyChar) ||
                (e.KeyChar == ("a")[0]) ||
                (e.KeyChar == ("b")[0]) ||
                (e.KeyChar == ("c")[0]) ||
                (e.KeyChar == ("d")[0]) ||
                (e.KeyChar == ("e")[0]) ||
                (e.KeyChar == ("f")[0]) ||
                (e.KeyChar == ("A")[0]) ||
                (e.KeyChar == ("B")[0]) ||
                (e.KeyChar == ("C")[0]) ||
                (e.KeyChar == ("D")[0]) ||
                (e.KeyChar == ("E")[0]) ||
                (e.KeyChar == ("F")[0])
                )
                )
            {
                e.Handled = true;
            }
        }
        private void DecKeyPress(object sender, KeyPressEventArgs e)
        {
            if (
                !(
                Char.IsDigit(e.KeyChar) ||
                Char.IsControl(e.KeyChar)
                )
                )
            {
                e.Handled = true;
            }
        }

        private void MenuItemSave_Click(object sender, EventArgs e)
        {
            write.WriteBytes(byteSprite, ImageOffset);
        }
        private void MenuItemRefresh_Click(object sender, EventArgs e)
        {
            navigate();
        }
        private void MenuItemBackColor_Click(object sender, EventArgs e)
        {
            if (MenuItemBackColor.Checked)
            {
                MenuItemBackColor.Checked = false;
                PictureBoxSprite.BackgroundImage = OWEditor.Properties.Resources.transparent;
            }
            else
            {
                MenuItemBackColor.Checked = true;
                PictureBoxSprite.BackgroundImage = null;
            }

            PictureBoxSprite.Refresh();
            drawBlock();


        }
        private void MenuItemOpenInNSE_Click(object sender, EventArgs e)
        {
            Host.NewEditorForm(new NSE_Framework.Data.Sprite(
                this.ImageOffset,
                this.PaletteOffset,
                sWidth / 8, sHeight / 8,
                NSE_Framework.Data.Sprite.SpriteType.Color16,
                (byte[])byteSprite.Clone(),
                SpritePalette));



        }

        private void MainForm_Enter(object sender, EventArgs e)
        {
            if (byteSprite != null)
            {
                fillCanvas();
                fillPalette();

                NSE_Framework.Draw.uDrawData(ref Sprite, byteSprite, SpritePalette, new Size(sWidth / 8, sHeight / 8));
                PictureBoxSprite.Image = Sprite;
                PictureBoxSprite.BackColor = SpritePalette.Colors[0].Color;
                PictureBoxSprite.Refresh();

                drawBlock();
            }
        }

        private void MenuItemExportBitmap_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Bitmap|*.bmp;";
            saveDialog.FileName = Index.ToString() + "_" + Frame.ToString();

            if (saveDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                NSE_Framework.IO.Export.ExportBitmap(saveDialog.FileName, new NSE_Framework.Data.Sprite(
                    this.ImageOffset,
                    this.PaletteOffset,
                    sWidth / 8, sHeight / 8,
                    NSE_Framework.Data.Sprite.SpriteType.Color16,
                    (byte[])byteSprite.Clone(),
                    SpritePalette));
            }            
        }
    }
}
