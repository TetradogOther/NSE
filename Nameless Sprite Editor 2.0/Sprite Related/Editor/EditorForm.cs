using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NSE_Framework;

namespace NSE2
{
    public partial class EditorForm : Form
    {
        public string FileName = null;
        public NSE_Framework.IO.SpriteLibrary SpriteLibrary = null;
        public int index;
        public int frame;

        public void RefreshTitle()
        {
            if (FileName == null)
            {
                if (this.Editor.CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
                {
                    if (this.Editor.CurrentSprite.CompressedSprite != -1)
                    {
                        this.Text = "Sprite: 0x" + this.Editor.CurrentSprite.ImageOffset.ToString("X") + " Compressed @ " + this.Editor.CurrentSprite.CompressedSprite.ToString() + " bytes (4bpp)";
                    }
                    else
                    {
                        this.Text = "Sprite: 0x" + this.Editor.CurrentSprite.ImageOffset.ToString("X") + " @ " + (this.Editor.CurrentSprite.Width * this.Editor.CurrentSprite.Height * 32) + " bytes (4bpp)";
                    }

                }
                else if (this.Editor.CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
                {

                    if (this.Editor.CurrentSprite.CompressedSprite != -1)
                    {
                        this.Text = "Sprite: 0x" + this.Editor.CurrentSprite.ImageOffset.ToString("X") + " Compressed @ " + this.Editor.CurrentSprite.CompressedSprite.ToString() + " bytes (8bpp)";
                    }
                    else
                    {
                        this.Text = "Sprite: 0x" + this.Editor.CurrentSprite.ImageOffset.ToString("X") + " @ " + (this.Editor.CurrentSprite.Width * this.Editor.CurrentSprite.Height * 64) + " bytes (8bpp)";
                    }
                }
            }
            else
            {
                if (this.Editor.CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
                {
                    this.Text = this.Text = FileName + " : @ " + (this.Editor.CurrentSprite.Width * this.Editor.CurrentSprite.Height * 32) + " bytes (4bpp)";

                }
                else if (this.Editor.CurrentSprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
                {
                    this.Text = this.Text = FileName + " : @ " + (this.Editor.CurrentSprite.Width * this.Editor.CurrentSprite.Height * 64) + " bytes (8bpp)";
                }
                
            }
        }

        public EditorForm(NSE_Framework.IO.SpriteLibrary SpriteLibrary, string FileName)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            NSE_Framework.Data.Sprite sprite = new NSE_Framework.Data.Sprite(SpriteLibrary.Sprites[0].Width, SpriteLibrary.Sprites[0].Height, NSE_Framework.Data.Translator.PaletteToSpriteType(SpriteLibrary.Sprites[0].Palette.Type));
            sprite.Palette = SpriteLibrary.Sprites[0].Palette;
            if (SpriteLibrary.Sprites[0].SpriteData[0].Compressed)
            {
                sprite.ImageData = NSE_Framework.Data.Lz77.DecompressBytes(SpriteLibrary.Sprites[0].SpriteData[0].Data);
            }
            else 
            {
                sprite.ImageData = SpriteLibrary.Sprites[0].SpriteData[0].Data;
            }

            LibraryTree.Nodes.Clear();


            foreach (NSE_Framework.IO.SpriteSet sSet in SpriteLibrary.Sprites)
            {
                TreeNode node = new TreeNode(sSet.Name);
                foreach (NSE_Framework.IO.SpriteData sDat in sSet.SpriteData)
                {
                    node.Nodes.Add(new TreeNode(sDat.Name));
                }
                LibraryTree.Nodes.Add(node);
            }

            LibraryTree.SelectedNode = LibraryTree.Nodes[0].Nodes[0];

            
            this.Editor.CurrentSprite = sprite;
            this.SpriteLibrary = SpriteLibrary;
            this.index = 0;
            this.frame = 0;
            this.Text = SpriteLibrary.Origin + " - " + FileName;
            this.FileName = FileName;
            this.SidePanel.Visible = true;
        }

        public EditorForm(NSE_Framework.IO.SpriteLibrary SpriteLibrary)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            NSE_Framework.Data.Sprite sprite = new NSE_Framework.Data.Sprite(SpriteLibrary.Sprites[0].Width, SpriteLibrary.Sprites[0].Height, NSE_Framework.Data.Translator.PaletteToSpriteType(SpriteLibrary.Sprites[0].Palette.Type));
            sprite.Palette = SpriteLibrary.Sprites[0].Palette;
            if (SpriteLibrary.Sprites[0].SpriteData[0].Compressed)
            {
                sprite.ImageData = NSE_Framework.Data.Lz77.DecompressBytes(SpriteLibrary.Sprites[0].SpriteData[0].Data);
            }
            else
            {
                sprite.ImageData = SpriteLibrary.Sprites[0].SpriteData[0].Data;
            }

            LibraryTree.Nodes.Clear();


            foreach (NSE_Framework.IO.SpriteSet sSet in SpriteLibrary.Sprites)
            {
                TreeNode node = new TreeNode(sSet.Name);
                foreach (NSE_Framework.IO.SpriteData sDat in sSet.SpriteData)
                {
                    node.Nodes.Add(new TreeNode(sDat.Name));
                }
                LibraryTree.Nodes.Add(node);
            }

            LibraryTree.SelectedNode = LibraryTree.Nodes[0].Nodes[0];


            this.Editor.CurrentSprite = sprite;
            this.SpriteLibrary = SpriteLibrary;
            this.index = 0;
            this.frame = 0;
            this.Text = SpriteLibrary.Origin;
            this.SidePanel.Visible = true;
        }

        public EditorForm(NSE_Framework.Data.Sprite Sprite)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Editor.CurrentSprite = Sprite;
            if (Sprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
            {
                if (Sprite.UniqueImage == true)
                {
                    if (Sprite.CompressedSprite != -1)
                    {
                        this.Text = "Sprite: 0x" + Sprite.ImageOffset.ToString("X") + " Compressed @ " + Sprite.CompressedSprite.ToString() + " bytes (4bpp)";
                    }
                    else
                    {
                        this.Text = "Sprite: 0x" + Sprite.ImageOffset.ToString("X") + " @ " + (Sprite.Width * Sprite.Height * 32) + " bytes (4bpp)";
                    }
                }
                else
                {
                    this.Text = "Sprite: @ " + (Sprite.Width * Sprite.Height * 32) + " bytes (4bpp)";
                }
                
            }
            else if (Sprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
            {
                if (Sprite.UniqueImage == true)
                {
                    if (Sprite.CompressedSprite != -1)
                    {
                        this.Text = "Sprite: 0x" + Sprite.ImageOffset.ToString("X") + " Compressed @ " + Sprite.CompressedSprite.ToString() + " bytes (8bpp)";
                    }
                    else
                    {
                        this.Text = "Sprite: 0x" + Sprite.ImageOffset.ToString("X") + " @ " + (Sprite.Width * Sprite.Height * 64) + " bytes (8bpp)";
                    }
                }
                else
                {
                    this.Text = "Sprite: @ " + (Sprite.Width * Sprite.Height * 64) + " bytes (8bpp)";
                }

            }
            this.Editor.CurrentSprite = Sprite;

        }

        public EditorForm(NSE_Framework.Data.Sprite Sprite, string FileName)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Editor.CurrentSprite = Sprite;
            if (Sprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color16)
            {                        
                this.Text = this.Text = FileName + " : @ "+ (Sprite.Width * Sprite.Height * 32) + " bytes (4bpp)";
                    
            }
            else if (Sprite.Type == NSE_Framework.Data.Sprite.SpriteType.Color256)
            {
                this.Text = this.Text = FileName + " : @ " + (Sprite.Width * Sprite.Height * 64) + " bytes (8bpp)";
            }
            this.Editor.CurrentSprite = Sprite;

            this.FileName = FileName;
        }

        private void LibraryTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (LibraryTree.SelectedNode.Level == 1)
            {
                SpriteLibrary.Sprites[index].SpriteData[frame].Data = Editor.CurrentSprite.ImageData;
                SpriteLibrary.Sprites[index].SpriteData[frame].Compressed = false;

                index = LibraryTree.SelectedNode.Parent.Index;
                frame = LibraryTree.SelectedNode.Index;


                NSE_Framework.Data.Sprite sprite = new NSE_Framework.Data.Sprite(SpriteLibrary.Sprites[index].Width, SpriteLibrary.Sprites[index].Height, NSE_Framework.Data.Translator.PaletteToSpriteType(SpriteLibrary.Sprites[index].Palette.Type));
                sprite.Palette = SpriteLibrary.Sprites[index].Palette;

                sprite.ImageData = this.SpriteLibrary.Sprites[index].SpriteData[frame].Data;

                if (this.SpriteLibrary.Sprites[index].SpriteData[frame].Compressed == true)
                {
                    sprite.ImageData = NSE_Framework.Data.Lz77.DecompressBytes(sprite.ImageData);
                }

                Editor.Sprites[0] = sprite;
                Editor.CurrentIndex = 0;


            }
        }

        private void LibraryTree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null && e.Label != "")
            {
                if (!string.IsNullOrWhiteSpace(e.Label) && e.Label.Length > 0)
                {
                    if (e.Node.Level == 0)
                    {
                        SpriteLibrary.Sprites[e.Node.Index].Name = e.Label;
                    }
                    else if (e.Node.Level == 1)
                    {
                        SpriteLibrary.Sprites[e.Node.Parent.Index].SpriteData[e.Node.Index].Name = e.Label;
                    }
                }
                else
                {
                    e.CancelEdit = true;
                }
            }
            else
            {
                e.CancelEdit = true;
            }
        }

        private void ToolStripAddBookMark_Click(object sender, EventArgs e)
        {
            if (LibraryTree.SelectedNode != null)
            {
                if (LibraryTree.SelectedNode.Level == 0)
                {
                    byte[] Data;

                    if(SpriteLibrary.Sprites[LibraryTree.SelectedNode.Index].Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color16)
                    {
                        Data = new byte[SpriteLibrary.Sprites[LibraryTree.SelectedNode.Index].Width * SpriteLibrary.Sprites[LibraryTree.SelectedNode.Index].Height * 32];
                    }
                    else
                    {
                        Data = new byte[SpriteLibrary.Sprites[LibraryTree.SelectedNode.Index].Width * SpriteLibrary.Sprites[LibraryTree.SelectedNode.Index].Height * 64];
                    }

                    SpriteLibrary.Sprites[LibraryTree.SelectedNode.Index].SpriteData.Add(new NSE_Framework.IO.SpriteData("Untitled",Data));
                    TreeNode newNode = new TreeNode("Untitled");
                    LibraryTree.SelectedNode.Nodes.Add(newNode);
                    LibraryTree.SelectedNode = newNode;

                }
                else if (LibraryTree.SelectedNode.Level == 1)
                {
                    byte[] Data;
                    int index = LibraryTree.SelectedNode.Parent.Index;

                    if (SpriteLibrary.Sprites[index].Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color16)
                    {
                        Data = new byte[SpriteLibrary.Sprites[index].Width * SpriteLibrary.Sprites[index].Height * 32];
                    }
                    else
                    {
                        Data = new byte[SpriteLibrary.Sprites[index].Width * SpriteLibrary.Sprites[index].Height * 64];
                    }

                    SpriteLibrary.Sprites[index].SpriteData.Add(new NSE_Framework.IO.SpriteData("Untitled", Data));
                    TreeNode newNode = new TreeNode("Untitled");
                    LibraryTree.SelectedNode.Parent.Nodes.Add(newNode);
                    LibraryTree.SelectedNode = newNode;
                }
            }
        }

        private void ToolStripAddFolder_Click(object sender, EventArgs e)
        {

            NewSprite nsForm = new NewSprite();
            if (nsForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                NSE_Framework.IO.SpriteSet nSet = new NSE_Framework.IO.SpriteSet(nsForm.sName, nsForm.sPalette, nsForm.sWidth, nsForm.sHeight);
                if(nSet.Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color16)
                {
                nSet.SpriteData.Add(new NSE_Framework.IO.SpriteData("Untitled", new Byte[nSet.Width * nSet.Height * 32]));
                }
                else if(nSet.Palette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color256)
                {
                nSet.SpriteData.Add(new NSE_Framework.IO.SpriteData("Untitled", new Byte[nSet.Width * nSet.Height * 64]));
                }

                SpriteLibrary.Sprites.Add(nSet);

                TreeNode newNode = new TreeNode(nsForm.sName);
                newNode.Nodes.Add("Untitled");

                LibraryTree.Nodes.Add(newNode);
                LibraryTree.SelectedNode = newNode.Nodes[0];
            }
        }

        private void ToolStripDelete_Click(object sender, EventArgs e)
        {
            if (LibraryTree.SelectedNode != null)
            {
                if (LibraryTree.SelectedNode.Level == 0 && LibraryTree.Nodes.Count > 1)
                {
                    if (MessageBox.Show(this, "Are you sure you want to delete " + LibraryTree.SelectedNode.Text + "?\nAll child data will be deleted.", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        int i = LibraryTree.SelectedNode.Index;

                        if (i != 0)
                        {
                            LibraryTree.SelectedNode = LibraryTree.Nodes[i - 1].Nodes[0];
                            index = i - 1;
                        }
                        else if (i == 0)
                        {
                            LibraryTree.SelectedNode = LibraryTree.Nodes[1].Nodes[0];
                            index = 0;
                        }
                        SpriteLibrary.Sprites.RemoveAt(i);
                        LibraryTree.Nodes.RemoveAt(i);


                    }
                }
                else if (LibraryTree.SelectedNode.Level == 1 && LibraryTree.SelectedNode.Parent.Nodes.Count > 1)
                {
                    if (MessageBox.Show(this, "Are you sure you want to delete " + LibraryTree.SelectedNode.Text + "?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                    {
                        int i = LibraryTree.SelectedNode.Index;

                        if (i != 0)
                        {
                            LibraryTree.SelectedNode = LibraryTree.Nodes[index].Nodes[i - 1];
                            frame = i - 1;
                        }
                        else if (i == 0)
                        {
                            LibraryTree.SelectedNode = LibraryTree.Nodes[index].Nodes[1];
                            frame = 0;
                        }
                        SpriteLibrary.Sprites[index].SpriteData.RemoveAt(i);
                        LibraryTree.Nodes[index].Nodes.RemoveAt(i);
                    }
                }
                else
                {
                    MessageBox.Show(this, "There must be at least one sprite in the library and,\nat least one frame for each sprite.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
            }
        }

        private void LibraryTree_ItemDrag(object sender, ItemDragEventArgs e)
        {

        }

        private void LibraryTree_DragDrop(object sender, DragEventArgs e)
        {

        }





    }

}
