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
    public partial class Import : Form
    {        

        Bitmap previewBitmap;
        Bitmap paletteBitmap;

        NSE_Framework.Data.Sprite Sprite;
        NSE_Framework.IO.SpriteLibrary Library;

        public Import()
        {
            InitializeComponent();
        }

        private void DrawPalette()
        {
            paletteBitmap = new Bitmap(128, 128, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(paletteBitmap);
            short x = 0;
            short y = 0;

            NSE_Framework.Data.SpritePalette palette;

            if (Sprite != null)
            {
                palette = Sprite.Palette;
            }
            else if (Library != null)
            {
                palette = Library.Sprites[LibraryTree.SelectedNode.Parent.Index].Palette;
            }
            else
            {
                throw new Exception("No palette to draw!");
            }

            foreach (NSE_Framework.Data.GBAcolor p in palette.Colors)
            {
                g.FillRectangle(new SolidBrush(p.Color), x, y, 8, 8);
                if (x < 120)
                {
                    x += 8;
                }
                else
                {
                    x = 0;
                    y += 8;
                }
            }

            PaletteRep.Image = paletteBitmap;
            PaletteRep.Refresh();
        }

        private void ButtonImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog iDialog = new OpenFileDialog();
            iDialog.Title = "Select a file to import:";
            iDialog.CheckFileExists = true;
            iDialog.Filter = "Importable Sprite|*.nslx;*.bmp;*.png*";

            if (iDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK & iDialog.FileName != "")
            {
                string extension = Path.GetExtension(iDialog.FileName).Substring(1).ToLower();
                LibraryTree.Nodes.Clear();
                LibraryTree.Nodes.Add("No Sprite Library loaded");
                Library = null;
                Sprite = null;

                if (extension == "png" || extension == "bmp")
                {
                    try
                    {
                        Sprite = NSE_Framework.IO.Import.ImportImage(iDialog.FileName);
                        this.Text = "Import - " + Path.GetFileName(iDialog.FileName);
                        previewBitmap = new Bitmap(Sprite.Width * 8, Sprite.Height * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                        NSE_Framework.Draw.uDrawSprite(ref previewBitmap, Sprite);
                        Preview.Image = previewBitmap;

                        DrawPalette();
                        LabelSize.Text = "Size: ( " + Sprite.Width.ToString() + ", " + Sprite.Height.ToString() + ")";

                        if (Sprite.Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color16)
                        {
                            LabelColors.Text = "Colors: 16";
                        }
                        else if (Sprite.Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color256)
                        {
                            LabelColors.Text = "Colors: 256";
                        }
                        ButtonSave.Enabled = true;
                        ComboBoxMode.Enabled = true;
                        ComboBoxMode.SelectedIndex = 0;
                        ButtonSave.Focus();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (extension == "nslx")
                {
                    LibraryTree.Nodes.Clear();

                    Library = NSE_Framework.IO.Import.ImportSpriteLibrary(iDialog.FileName);
                    this.Text = "Import - " + this.Library.Origin;
                    
                    foreach (NSE_Framework.IO.SpriteSet sSet in Library.Sprites)
                    {
                        TreeNode node = new TreeNode(sSet.Name);
                        foreach (NSE_Framework.IO.SpriteData sDat in sSet.SpriteData)
                        {
                            node.Nodes.Add(new TreeNode(sDat.Name));
                        }
                        LibraryTree.Nodes.Add(node);
                    }

                    LibraryTree.SelectedNode = LibraryTree.Nodes[0].Nodes[0];
                    ButtonSave.Enabled = true;
                    ComboBoxMode.Enabled = true;
                    ComboBoxMode.SelectedIndex = 0;
                    ButtonSave.Focus();
                }

                


            }

        }

        private void LibraryTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (LibraryTree.SelectedNode.Level == 1)
            {
                int spriteIndex = LibraryTree.SelectedNode.Parent.Index;
                int dataIndex = LibraryTree.SelectedNode.Index;

                previewBitmap = new Bitmap(this.Library.Sprites[spriteIndex].Width * 8, this.Library.Sprites[spriteIndex].Height * 8, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                byte[] data = this.Library.Sprites[spriteIndex].SpriteData[dataIndex].Data;

                if (this.Library.Sprites[spriteIndex].SpriteData[dataIndex].Compressed == true)
                {
                    data = NSE_Framework.Data.Lz77.DecompressBytes(data);
                }


                NSE_Framework.Draw.uDrawData(ref previewBitmap,
                    data,
                    this.Library.Sprites[spriteIndex].Palette,
                    new Size(this.Library.Sprites[spriteIndex].Width, this.Library.Sprites[spriteIndex].Height));
                Preview.Image = previewBitmap;

                LabelSize.Text = "Size: ( " + this.Library.Sprites[0].Width.ToString() + ", " + this.Library.Sprites[0].Height.ToString() + ")";

                if (this.Library.Sprites[spriteIndex].Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color16)
                {
                    LabelColors.Text = "Colors: 16";
                }
                else if (this.Library.Sprites[spriteIndex].Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color256)
                {
                    LabelColors.Text = "Colors: 256";
                }
                DrawPalette();

            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (Program.MainForm.currentEditor != null)
            {

                if (Sprite != null)
                {
                    if (ComboBoxMode.SelectedIndex == 0 || ComboBoxMode.SelectedIndex == 2)
                    {
                        if (Program.MainForm.currentEditor.CurrentSprite.Width == Sprite.Width && Program.MainForm.currentEditor.CurrentSprite.Height == Sprite.Height && Program.MainForm.currentEditor.CurrentSprite.Type == Sprite.Type)
                        {
                            Sprite.ImageData.CopyTo(Program.MainForm.currentEditor.CurrentSprite.ImageData, 0);
                            Program.MainForm.currentEditor.Redraw();
                        }
                    }

                    if (ComboBoxMode.SelectedIndex == 1 || ComboBoxMode.SelectedIndex == 2)
                    {
                        if (Program.MainForm.currentEditor.CurrentSprite.Type == Sprite.Type)
                        {
                            Program.MainForm.currentEditor.CurrentSprite.Palette = new NSE_Framework.Data.SpritePalette(Sprite.Palette.Type, Sprite.Palette.GetGBABytes);
                            if (Program.MainForm.currentEditor.ParentSelectColor != null)
                            {
                                Program.MainForm.currentEditor.ParentSelectColor.Redraw();
                            }
                            Program.MainForm.currentEditor.Redraw();
                        }
                    }

                }
                else if(this.Library != null)
                {
                    if (LibraryTree.SelectedNode.Level == 1)
                    {
                        int spriteIndex = LibraryTree.SelectedNode.Parent.Index;
                        int dataIndex = LibraryTree.SelectedNode.Index;

                        if (ComboBoxMode.SelectedIndex == 0 || ComboBoxMode.SelectedIndex == 2)
                        {
                            if (Program.MainForm.currentEditor.CurrentSprite.Width == this.Library.Sprites[spriteIndex].Width
                                && Program.MainForm.currentEditor.CurrentSprite.Height == this.Library.Sprites[spriteIndex].Height
                                && (int)Program.MainForm.currentEditor.CurrentSprite.Type == (int)this.Library.Sprites[spriteIndex].Palette.Type)
                            {
                                byte[] data = this.Library.Sprites[spriteIndex].SpriteData[dataIndex].Data;

                                if (this.Library.Sprites[spriteIndex].SpriteData[dataIndex].Compressed == true)
                                {
                                    data = NSE_Framework.Data.Lz77.DecompressBytes(data);
                                }

                                data.CopyTo(Program.MainForm.currentEditor.CurrentSprite.ImageData, 0);
                                //Program.MainForm.currentEditor.CurrentSprite.ImageData = data;
                                Program.MainForm.currentEditor.Redraw();
                            }
                        }

                        if (ComboBoxMode.SelectedIndex == 1 || ComboBoxMode.SelectedIndex == 2)
                        {

                            if ((int)Program.MainForm.currentEditor.CurrentSprite.Type == (int)this.Library.Sprites[spriteIndex].Palette.Type)
                            {
                                Program.MainForm.currentEditor.CurrentSprite.Palette = new NSE_Framework.Data.SpritePalette(this.Library.Sprites[spriteIndex].Palette.Type,this.Library.Sprites[spriteIndex].Palette.GetGBABytes) ;
                                if (Program.MainForm.currentEditor.ParentSelectColor != null)
                                {
                                    Program.MainForm.currentEditor.ParentSelectColor.Redraw();
                                }
                                Program.MainForm.currentEditor.Redraw();
                            }
                        }
                    }
                }
            }
        }

    }
}
