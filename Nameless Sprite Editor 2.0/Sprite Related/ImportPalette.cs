using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NSE2
{
    public partial class ImportPalette : Form
    {
        Bitmap paletteBitmap;

        int index;

        public ImportPalette()
        {
            InitializeComponent();
        }

        NSE_Framework.Data.GBAcolor[] Colors;

        private void ButtonLoad_Click(object sender, EventArgs e)
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
                                Colors = new NSE_Framework.Data.GBAcolor[Math.Min(palette.Entries.Length, 256)];
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

                            if (Colors.Length == 256)
                            {
                                ComboBoxMode.Items.Clear();
                                ComboBoxMode.Items.Add("16 Color");
                                ComboBoxMode.Items.Add("256 Color");
                                ComboBoxMode.SelectedIndex = 0;
                            }
                            else
                            {
                                ComboBoxMode.Items.Clear();
                                ComboBoxMode.Items.Add("16 Color");
                                ComboBoxMode.SelectedIndex = 0;
                            }

                            index = 0;
                            ButtonImport.Enabled = true;
                            ComboBoxMode.Enabled = true;
                        }
                        else if (extension == "act")
                        {
                            FileStream stream = new FileStream(iDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            BinaryReader br = new BinaryReader(stream);
                            byte[] b = br.ReadBytes((int)stream.Length);

                            Colors = new NSE_Framework.Data.GBAcolor[Math.Min((b.Length - b.Length % 3) / 3, 256)];

                            for (int x = 0; x < Colors.Length; x++)
                            {
                                Colors[x] = new NSE_Framework.Data.GBAcolor(b[x * 3], b[x * 3 + 1], b[x * 3 + 2]);
                            }

                            DrawPalette();

                            index = 0;
                            if (Colors.Length >= 256)
                            {
                                ComboBoxMode.Items.Clear();
                                ComboBoxMode.Items.Add("16 Color");
                                ComboBoxMode.Items.Add("256 Color");
                                ComboBoxMode.SelectedIndex = 0;
                            }
                            else
                            {
                                ComboBoxMode.Items.Clear();
                                ComboBoxMode.Items.Add("16 Color");
                                ComboBoxMode.SelectedIndex = 0;
                            }

                            ButtonImport.Enabled = true;
                            ComboBoxMode.Enabled = true;

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
            paletteBitmap = new Bitmap(256, 256, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(paletteBitmap);
            short x = 0;
            short y = 0;


            if (Colors == null)
            {
                throw new Exception("No palette to draw!");
            }

            foreach (NSE_Framework.Data.GBAcolor p in Colors)
            {
                g.FillRectangle(new SolidBrush(p.Color), x, y, 16, 16);
                if (x < 240)
                {
                    x += 16;
                }
                else
                {
                    x = 0;
                    y += 16;
                }
            }

            ColorsBox.Image = paletteBitmap;
            ColorsBox.Refresh();
        }

        private void ColorsBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (ComboBoxMode.SelectedIndex == 0)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    int y = ColorsBox.PointToClient(Cursor.Position).Y;

                    if (y > 0 && y < 256)
                    {
                        int i = (y - y % 16) / 16;
                        if (i * 16 < Colors.Length)
                        {
                            index = i;

                            ColorsBox.Refresh();
                            Graphics gx = this.ColorsBox.CreateGraphics();
                            gx.DrawRectangle(new Pen(Color.Red, 1), 0, index * 16, 256, 15);
                            gx.DrawRectangle(new Pen(Color.Black, 1), 1, index * 16 + 1, 254, 13);
                        }


                    }
                }
            }
        }

        private void ComboBoxMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorsBox.Refresh();
            if (ComboBoxMode.SelectedIndex == 0)
            {
                index = 0;
                Graphics gx = this.ColorsBox.CreateGraphics();
                gx.DrawRectangle(new Pen(Color.Black, 1), 0, index * 16, 256, 15);
                gx.DrawRectangle(new Pen(Color.Red, 1), 1, index * 16 + 1, 254, 13);
            }
        }

        private void ButtonImport_Click(object sender, EventArgs e)
        {
            if(Program.MainForm.currentEditor != null)
            {
            if (ComboBoxMode.SelectedIndex == 0)
            {
                if (Program.MainForm.currentEditor.CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        Program.MainForm.currentEditor.CurrentSprite.Palette.Colors[i] = Colors[index * 16 + i];
                    }
                    Program.MainForm.currentEditor.Redraw();
                    if(Program.MainForm.currentEditor.ParentSelectColor != null && Program.MainForm.currentEditor.ParentSelectColor.IsDisposed == false)
                    {
                        Program.MainForm.currentEditor.ParentSelectColor.Redraw();
                    }
                }
                else
                {
                    MessageBox.Show(this, "The current sprite does not have a 16 color palette.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if(ComboBoxMode.SelectedIndex == 1)
            {
                if (Program.MainForm.currentEditor.CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
                {
                    Program.MainForm.currentEditor.CurrentSprite.Palette.Colors = Colors;
                    Program.MainForm.currentEditor.Redraw();
                    if (Program.MainForm.currentEditor.ParentSelectColor != null && Program.MainForm.currentEditor.ParentSelectColor.IsDisposed == false)
                    {
                        Program.MainForm.currentEditor.ParentSelectColor.Redraw();
                    }


                }
                else
                {
                    MessageBox.Show(this, "The current sprite does not have a 256 color palette.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            }
        }
    }
}
