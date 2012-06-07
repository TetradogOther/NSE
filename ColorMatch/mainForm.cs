using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ColorMatch
{
    public partial class mainForm : Form
    {
        Plugins.IPluginHost Host;

        NSE_Framework.Data.GBAcolor[] Colors;

        NSE_Framework.Data.Sprite NewSprite;

        string oldName;

        public mainForm(Plugins.IPluginHost iHost)
        {
            InitializeComponent();
            this.Host = iHost;
        }

        private void ButtonImportPalette_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog iDialog = new OpenFileDialog();
                iDialog.Title = "Select a file to import:";
                iDialog.CheckFileExists = true;
                iDialog.Filter = "Importable Palettes|*.act;*.bmp;*.png*";

                if (iDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK & iDialog.FileName != "")
                {
                    if (System.IO.File.Exists(iDialog.FileName) == true)
                    {
                        string extension = Path.GetExtension(iDialog.FileName).Substring(1).ToLower();

                        if (extension == "png" || extension == "bmp")
                        {


                            Bitmap bitmap = new Bitmap(iDialog.FileName);
                            System.Drawing.Imaging.ColorPalette palette = bitmap.Palette;


                            if (palette.Entries.Length > 1)
                            {
                                Colors = new NSE_Framework.Data.GBAcolor[Math.Min(palette.Entries.Length, 16)]; //should be 256
                            }
                            else
                            {
                                throw new Exception("The image loaded was not indexed or used an undefined alpha channel.");
                            }

                            for (int p = 0; p < Colors.Length; p++)
                            {
                                if (p < palette.Entries.Length)
                                {
                                    Colors[p] = new NSE_Framework.Data.GBAcolor(palette.Entries[p]);
                                }
                            }

                            DrawPalette();

                            ButtonColorMatch.Enabled = true;
                            ButtonSave.Enabled = false;
                        }
                        else if (extension == "act")
                        {
                            FileStream stream = new FileStream(iDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            BinaryReader br = new BinaryReader(stream);
                            byte[] b = br.ReadBytes((int)stream.Length);

                            Colors = new NSE_Framework.Data.GBAcolor[Math.Min((b.Length - b.Length % 3) / 3, 16)]; // Should be 256

                            for (int x = 0; x < Colors.Length; x++)
                            {
                                Colors[x] = new NSE_Framework.Data.GBAcolor(b[x * 3], b[x * 3 + 1], b[x * 3 + 2]);
                            }

                            DrawPalette();

                            ButtonColorMatch.Enabled = true;
                            ButtonSave.Enabled = false;

                        }

                        else
                        {
                            throw new Exception("The file does not exist.");
                        }


                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DrawPalette()
        {
            if (PictureBoxPalette.Image == null)
            {
                PictureBoxPalette.Image = new Bitmap(32, 128);
            }
            Graphics g = Graphics.FromImage(PictureBoxPalette.Image);
            int x = 0;
            int y = 0;
            for (int i = 0; i < 16; i++)
            {
                g.FillRectangle(new SolidBrush(Colors[i].Color), x, y, 16, 16);

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

            PictureBoxPalette.Refresh();
        }

        private void ButtonColorMatch_Click(object sender, EventArgs e)
        {
            OpenFileDialog iDialog = new OpenFileDialog();
            iDialog.Title = "Select a file to import:";
            iDialog.CheckFileExists = true;
            iDialog.Filter = "Importable Images|*.bmp;*.png*";

            if (iDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK && !String.IsNullOrEmpty(iDialog.FileName))
            {
                if (System.IO.File.Exists(iDialog.FileName) == true)
                {
                    oldName = Path.GetFileNameWithoutExtension(iDialog.FileName);
                    Bitmap bm = new Bitmap(iDialog.FileName);

                    NewSprite = new NSE_Framework.Data.Sprite(bm.Width / 8, bm.Height / 8, NSE_Framework.Data.Sprite.SpriteType.Color16);

                    NewSprite.Palette = new NSE_Framework.Data.SpritePalette(NSE_Framework.Data.SpritePalette.PaletteType.Color16);
                    NewSprite.Palette.Colors = Colors;

                    Color getColor;
                    byte index = 0;

                    for (int x = 0; x < NewSprite.Width * 8; x++)
                    {
                        for (int y = 0; y < NewSprite.Height * 8; y++)
                        {
                            index = 0;
                            getColor = bm.GetPixel(x, y);
                            for (int i = 1; i < 16 && index == 0; i++)
                            {
                                if (Colors[i].Color == getColor)
                                {
                                    index = (byte)i;
                                }
                            }

                            SetByte(x, y, index);
                        }
                    }

                    Bitmap bs = new Bitmap(NewSprite.Width * 8, NewSprite.Height * 8);
                    NSE_Framework.Draw.uDrawSprite(ref bs, NewSprite);

                    PictureBoxSprite.Size = new System.Drawing.Size(NewSprite.Width * 8 + 2, NewSprite.Height * 8 + 2);
                    PictureBoxSprite.Image = bs;
                    PictureBoxSprite.Refresh();

                    ButtonSave.Enabled = true;

                }

            }
        }
        void SetByte(int x, int y, byte val)
        {
            int pos = Position2Index(new Size(this.NewSprite.Width * 8, this.NewSprite.Height * 8), new Point(x, y), NewSprite.Type);

            if (NewSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
            {
                if (x % 2 == 0)
                {
                    //CurrentSprite.ImageData[pos] = BitConverter.GetBytes(((CurrentSprite.ImageData[pos] >> 4) << 4) + val)[0];
                    NewSprite.ImageData[pos] = (byte)((NewSprite.ImageData[pos] & 0xf0) + val);
                }
                else
                {
                    //CurrentSprite.ImageData[pos] = BitConverter.GetBytes(
                    //    CurrentSprite.ImageData[pos] -
                    //    BitConverter.GetBytes(((CurrentSprite.ImageData[pos] >> 4) << 4))[0]
                    //    + (val * 0x10))[0];
                    NewSprite.ImageData[pos] = (byte)((NewSprite.ImageData[pos] & 0x0f) + (val << 4));
                }
            }
            else if (NewSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
            {
                NewSprite.ImageData[pos] = val;
            }
            else
            {
                throw new Exception("Non-supported sprite type.");
            }
        }
        int Position2Index(Size Size, Point Position, NSE_Framework.Data.Sprite.SpriteType Type)
        {

            Position.X = Clamp(Position.X, 0, NewSprite.Width * 8 - 1);
            Position.Y = Clamp(Position.Y, 0, NewSprite.Height * 8 - 1);

            if (Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
            {

                int r = (Position.Y - (Position.Y % 8)) / 2 * Size.Width;
                r += (Position.X - (Position.X % 8)) * 4;
                r += (Position.Y % 8) * 4;
                r += (Position.X % 8) / 2;

                return Clamp(r, 0, Size.Width * Size.Height / 2 - 1); ;
            }
            else if (Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
            {
                int r = (Position.Y - (Position.Y % 8)) * Size.Width;
                r += (Position.X - (Position.X % 8)) * 8;
                r += (Position.Y % 8) * 8;
                r += (Position.X % 8);

                return Clamp(r, 0, Size.Width * Size.Height - 1);
            }
            else
            {
                return -1;
            }

        }
        int Clamp(int val, int min, int max)
        {
            if (val.CompareTo(min) < 0) return min;
            else if (val.CompareTo(max) > 0) return max;
            else return val;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Bitmap|*.bmp;";
            saveDialog.FileName = "m_" + oldName;

            if (saveDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                NSE_Framework.IO.Export.ExportBitmap(saveDialog.FileName, NewSprite);
            }  
        }
    }
}
