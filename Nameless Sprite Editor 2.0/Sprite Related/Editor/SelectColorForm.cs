using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NSE_Framework;
using System.Runtime.InteropServices;



namespace NSE2
{

    public partial class SelectColorForm : Form
    {
        [DllImport("gdi32.dll")]
        static extern int GetPixel(IntPtr hdc, int x, int y);

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetDesktopWindow();

        ColorDialog cd = new ColorDialog();

        public SelectColorForm()
        {
            InitializeComponent();           
        }

        public void LoadEditor(ref NSE_Framework.Controls.Editor Editor)
        {
            this.MainSelectColor.LoadEditor(ref Editor);
            this.MainSelectColor.Index = Editor.SelectedColorIndex;

            if (MainSelectColor.Editor.CurrentSprite.CompressedPalette != -1)
            {
                this.Text = "Select Color - Compressed";
            }
            else
            {
                this.Text = "Select Color";
            }
                        
        }

        private void SelectColorForm_Load(object sender, EventArgs e)
        {
            if (Program.MainForm.currentEditor != null)
            {
                this.LoadEditor(ref Program.MainForm.cEditor);
            }
        }

        private void MainSelectColor_SelectedColorChanged(object sender, EventArgs e)
        {
            PictureCurrent.BackColor = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Color;

            this.LabelR.Text = "R: " + MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Red.ToString();
            this.LabelG.Text = "G: " + MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Green.ToString();
            this.LabelB.Text = "B: " + MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Blue.ToString();

            this.BarR.Value = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Red / 8;
            this.BarG.Value = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Green / 8;
            this.BarB.Value = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Blue / 8;

            byte[] output = NSE_Framework.Data.Translator.PaletteToByte(MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index]);

            this.TextBoxGBA.Text = output[0].ToString("X2") + output[1].ToString("X2");

            if (MainSelectColor.Editor.CurrentSprite.CompressedPalette != -1)
            {
                this.Text = "Select Color - Compressed";
            }
            else
            {
                this.Text = "Select Color";
            }
        }

        private void ButtonCollapse_Click(object sender, EventArgs e)
        {
            if (this.Size == new Size(289, 467))
            {
                this.Size = new Size(289, 315);
                ButtonCollapse.Text = "+";
            }
            else
            {
                this.Size = new Size(289, 467);
                ButtonCollapse.Text = "-";
            }

        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            
            cd.Color = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Color;
            if (cd.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index] = new NSE_Framework.Data.GBAcolor(cd.Color);
                this.MainSelectColor.Refresh();

                PictureCurrent.BackColor = cd.Color;               
                this.LabelR.Text = "R: " + MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Red.ToString();
                this.LabelG.Text = "G: " + MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Green.ToString();
                this.LabelB.Text = "B: " + MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Blue.ToString();

                this.BarR.Value = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Red / 8;
                this.BarG.Value = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Green / 8;
                this.BarB.Value = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Blue / 8;

                byte[] output = NSE_Framework.Data.Translator.PaletteToByte(MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index]);

                this.TextBoxGBA.Text = output[0].ToString("X2") + output[1].ToString("X2");

                this.MainSelectColor.Redraw();
                this.MainSelectColor.Editor.Redraw();
            }
        }



        private void ButtonEyedropper_MouseDown(object sender, MouseEventArgs e)
        {
            EyedropTimer.Enabled = true;
            this.Cursor = Cursors.Cross;
        }

        private void EyedropTimer_Tick(object sender, EventArgs e)
        {
            IntPtr winDc = GetWindowDC(GetDesktopWindow());
            byte[] b = BitConverter.GetBytes(GetPixel(winDc, Cursor.Position.X, Cursor.Position.Y));

            NSE_Framework.Data.GBAcolor c = new NSE_Framework.Data.GBAcolor(b[0],b[1],b[2]);
            PickedColor.BackColor = c.Color;

        }

        private void ButtonEyedropper_MouseUp(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Default;
            EyedropTimer.Enabled = false;
        }

        private void PickedColor_Click(object sender, EventArgs e)
        {
            MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index] = new NSE_Framework.Data.GBAcolor(PickedColor.BackColor);
            this.MainSelectColor.Refresh();

            PictureCurrent.BackColor = PickedColor.BackColor;
            this.LabelR.Text = "R: " + MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Red.ToString();
            this.LabelG.Text = "G: " + MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Green.ToString();
            this.LabelB.Text = "B: " + MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Blue.ToString();

            this.BarR.Value = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Red / 8;
            this.BarG.Value = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Green / 8;
            this.BarB.Value = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Blue / 8;

            byte[] output = NSE_Framework.Data.Translator.PaletteToByte(MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index]);

            this.TextBoxGBA.Text = output[0].ToString("X2") + output[1].ToString("X2");

            this.MainSelectColor.Editor.Redraw();
            this.MainSelectColor.Redraw();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (this.MainSelectColor.Editor != null && Program.MainForm.Filename != null)
            {
                if (this.MainSelectColor.Editor.CurrentSprite.CompressedPalette == -1)
                {
                    //Regular Sprite
                    byte[] pb = this.MainSelectColor.Editor.CurrentSprite.Palette.GetGBABytes;
                    if (this.MainSelectColor.Editor.CurrentSprite.UniquePalette == true)
                    {
                        Program.MainForm.Write.WriteBytes(
                            pb,
                            this.MainSelectColor.Editor.CurrentSprite.PaletteOffset);
                    }
                    else
                    {
                        SaveForm saveForm = new SaveForm(Program.MainForm.Write,
                            pb, this.MainSelectColor.Editor.CurrentSprite.PaletteOffset, pb.Length);
                        saveForm.CheckBoxAbort.Enabled = false;
                        saveForm.ShowDialog(this);


                        if (saveForm.SaveOffset != -1)
                        {
                            int oo = MainSelectColor.Editor.CurrentSprite.PaletteOffset;
                            if (oo != saveForm.SaveOffset)
                            {
                                if (Program.BookMarkTree != null)
                                {
                                    List<string> Names = new List<string>();
                                    ScanTree(ref Program.BookMarkTree, ref Names, oo, saveForm.SaveOffset);

                                    if (Names.Count > 0)
                                    {
                                        string sNames = "";
                                        foreach (string s in Names)
                                        {
                                            sNames = sNames + s + ", ";
                                        }

                                        MessageBox.Show(this, "The BookMark(s): " + sNames + "have been automagically adjusted :)\nTo see changes re-open Navigate.", "Notice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }

                            this.MainSelectColor.Editor.CurrentSprite.PaletteOffset = saveForm.SaveOffset;
                            this.MainSelectColor.Editor.CurrentSprite.CompressedPalette = -1;

                        }
                    }
                }
                else
                {
                    //Compressed Sprite
                    byte[] data = NSE_Framework.Data.Lz77.CompressBytes(this.MainSelectColor.Editor.CurrentSprite.Palette.GetGBABytes);
                    SaveForm saveForm = new SaveForm(Program.MainForm.Write, data,
                        this.MainSelectColor.Editor.CurrentSprite.PaletteOffset,
                        this.MainSelectColor.Editor.CurrentSprite.CompressedPalette);

                    saveForm.ShowDialog(this);

                    if (saveForm.SaveOffset != -1)
                    {


                        this.MainSelectColor.Editor.CurrentSprite.PaletteOffset = saveForm.SaveOffset;
                        this.MainSelectColor.Editor.CurrentSprite.CompressedPalette = data.Length;
                    }

                }


            }
        }

        private void ScanTree(ref NSE_Framework.IO.BookMarkTree Tree, ref List<string> Names, int Old, int New)
        {

            for (int t = 0; t < Tree.ChildTrees.Count; t++)
            {
                NSE_Framework.IO.BookMarkTree tree = Tree.ChildTrees[t];
                ScanTree(ref tree, ref Names, Old, New);

            }

            int l = Tree.BookMarks.Count;
            for (int b = 0; b < l; b++)
            {
                if (Tree.BookMarks[b].ImageOffset == Old)
                {
                    Tree.BookMarks.Add(new NSE_Framework.IO.BookMark(Tree.BookMarks[b].Name + " Backup", Tree.BookMarks[b].ImageOffset, Tree.BookMarks[b].PaletteOffset, Tree.BookMarks[b].Width, Tree.BookMarks[b].Height, Tree.BookMarks[b].SpriteType, Tree.BookMarks[b].Lz77));
                    Tree.BookMarks[b].ImageOffset = New;
                    Names.Add(Tree.BookMarks[b].Name);

                    if (Tree.BookMarks[b].PaletteOffset == Old)
                    {
                        Tree.BookMarks[b].PaletteOffset = New;
                    }
                }


                if (Tree.BookMarks[b].PaletteOffset == Old)
                {
                    Tree.BookMarks.Add(new NSE_Framework.IO.BookMark(Tree.BookMarks[b].Name + " Backup", Tree.BookMarks[b].ImageOffset, Tree.BookMarks[b].PaletteOffset, Tree.BookMarks[b].Width, Tree.BookMarks[b].Height, Tree.BookMarks[b].SpriteType, Tree.BookMarks[b].Lz77));
                    Tree.BookMarks[b].PaletteOffset = New;
                    Names.Add(Tree.BookMarks[b].Name);
                }
            }

        }

        private void TextBoxGBA_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Focused == true)
            {
                if (((TextBox)sender).TextLength == 4)
                {
                    int v = int.Parse(TextBoxGBA.Text, System.Globalization.NumberStyles.HexNumber);
                    byte[] b = BitConverter.GetBytes(v);
                    MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index] = NSE_Framework.Data.Translator.ByteToPalette(b[1], b[0]);

                    this.MainSelectColor.Refresh();

                    PictureCurrent.BackColor = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Color;

                    this.LabelR.Text = "R: " + MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Red.ToString();
                    this.LabelG.Text = "G: " + MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Green.ToString();
                    this.LabelB.Text = "B: " + MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Blue.ToString();

                    this.BarR.Value = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Red / 8;
                    this.BarG.Value = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Green / 8;
                    this.BarB.Value = MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index].Blue / 8;

                    this.MainSelectColor.Editor.Redraw();
                    this.MainSelectColor.Redraw();

                }
            }
        }

        private void TextBoxGBA_Leave(object sender, EventArgs e)
        {
            if (TextBoxGBA.TextLength != 4)
            {
                byte[] output = NSE_Framework.Data.Translator.PaletteToByte(MainSelectColor.Editor.CurrentSprite.Palette.Colors[MainSelectColor.Index]);

                this.TextBoxGBA.Text = output[0].ToString("X2") + output[1].ToString("X2");
            }
        }


        private void SelectColorForm_Activated(object sender, EventArgs e)
        {
            MainSelectColor.Refresh();
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


    }
}
