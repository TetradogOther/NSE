using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSE2
{
    public partial class NewSprite : Form
    {
        bool rs = false;

        public int sWidth;
        public int sHeight;
        public string sName;
        public NSE_Framework.Data.SpritePalette sPalette;

        public NewSprite()
        {
            InitializeComponent();
        }

        void RefreshSize()
        {
            if (rs )
            {
                 uint size;

                
                if (ComboBoxWidth.SelectedIndex == 0)
                {
                    size = uint.Parse(TextBoxWidth.Text) * uint.Parse(TextBoxHeight.Text) * 32;
                }
                else
                {
                    size = uint.Parse(TextBoxWidth.Text) * uint.Parse(TextBoxHeight.Text) /2;
                }

                if (ComboBoxBits.SelectedIndex == 1)
                {
                    size *= 2;
                }

                if (size > 0x100000)
                {
                    LabelSize.Text = (size / 1048576.0f).ToString() + " MB";
                }
                else if (size > 1024)
                {
                    LabelSize.Text = (size / 1024.0f).ToString() + " KB";
                }
                else
                {
                    LabelSize.Text = size.ToString() + " B";
                }
                

            }
        }

        private void DigitKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar)))             
            {
                e.Handled = true;
            }
        }

        private void NewSprite_Load(object sender, EventArgs e)
        {
            rs = false;
            ComboBoxBits.SelectedIndex = 0;
            ComboBoxColors.SelectedIndex = 0;
            ComboBoxWidth.SelectedIndex = 0;

            TextBoxWidth.Text = "2";
            TextBoxHeight.Text = "4";

            rs = true;

            RefreshSize();
        }

        private void ComboBoxWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxHeight.SelectedIndex != ComboBoxWidth.SelectedIndex)
            {
                ComboBoxHeight.SelectedIndex = ComboBoxWidth.SelectedIndex;

                if (rs )
                {
                    rs = false;
                    if (ComboBoxWidth.SelectedIndex == 0)
                    {
                        TextBoxWidth.MaxLength = 3;
                        TextBoxHeight.MaxLength = 3;
                        TextBoxHeight.Text = (uint.Parse(TextBoxHeight.Text) / 8).ToString();
                        TextBoxWidth.Text = (uint.Parse(TextBoxWidth.Text) / 8).ToString();
                    }
                    else if (ComboBoxWidth.SelectedIndex == 1)
                    {
                        TextBoxWidth.MaxLength = 4;
                        TextBoxHeight.MaxLength = 4;
                        TextBoxHeight.Text = (uint.Parse(TextBoxHeight.Text) * 8).ToString();
                        TextBoxWidth.Text = (uint.Parse(TextBoxWidth.Text) * 8).ToString();
                    }
                    rs = true;
                    RefreshSize();
                }
            }


        }

        private void ComboBoxHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxHeight.SelectedIndex != ComboBoxWidth.SelectedIndex)
            {
                ComboBoxWidth.SelectedIndex = ComboBoxHeight.SelectedIndex;

                if (rs )
                {
                    rs = false;
                    if (ComboBoxWidth.SelectedIndex == 0)
                    {
                        TextBoxWidth.MaxLength = 3;
                        TextBoxHeight.MaxLength = 3;
                        TextBoxHeight.Text = (uint.Parse(TextBoxHeight.Text) / 8).ToString();
                        TextBoxWidth.Text = (uint.Parse(TextBoxWidth.Text) / 8).ToString();
                    }
                    else if (ComboBoxWidth.SelectedIndex == 1)
                    {
                        TextBoxWidth.MaxLength = 4;
                        TextBoxHeight.MaxLength = 4;
                        TextBoxHeight.Text = (uint.Parse(TextBoxHeight.Text) * 8).ToString();
                        TextBoxWidth.Text = (uint.Parse(TextBoxWidth.Text) * 8).ToString();
                    }
                    rs = true;
                    RefreshSize();
                }
            }
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (rs )
            {
                rs = false;

                TextBox t = (TextBox)sender;

                if (t.Text.Length == 0)
                {
                    if (ComboBoxWidth.SelectedIndex == 0)
                    {
                        t.Text = "1";
                    }
                    else
                    {
                        t.Text = "8";
                    }
                }

                if (uint.Parse(t.Text) <= 0)
                {
                    if (ComboBoxWidth.SelectedIndex == 0)
                    {
                        t.Text = "1";
                    }
                    else
                    {
                        t.Text = "8";
                    }
                }


                rs = true;
                RefreshSize();
            }


        }

        private void TextBoxWidth_Leave(object sender, EventArgs e)
        {
            if (rs )
            {
                rs = false;

                TextBox t = (TextBox)sender;

                if (ComboBoxWidth.SelectedIndex == 1)
                {
                    uint val = uint.Parse(t.Text);
                    if (uint.Parse(t.Text) > 0xFE0)
                    {
                        val = 0xFE0;
                    }
                    t.Text = (val - (val % 8)).ToString();
                }
                else
                {
                    uint val = uint.Parse(t.Text) * 8;
                    if (val > 0xFE0)
                    {
                        val = 0xFE0;
                    }
                    t.Text = ((val - (val % 8)) / 8).ToString();
                }
                rs = true;
                RefreshSize();
            }
        }

        private void TextBoxHeight_Leave(object sender, EventArgs e)
        {
            if (rs )
            {
                rs = false;

                TextBox t = (TextBox)sender;
                if (ComboBoxWidth.SelectedIndex == 1)
                {
                    uint val = uint.Parse(t.Text);
                    if (uint.Parse(t.Text) > 0xFE0)
                    {
                        val = 0xFE0;
                    }
                    t.Text = (val - (val % 8)).ToString();
                }
                else
                {
                    uint val = uint.Parse(t.Text) * 8;
                    if (val > 0xFE0)
                    {
                        val = 0xFE0;
                    }
                    t.Text = ((val - (val % 8)) / 8).ToString();
                }
                rs = true;
                RefreshSize();
            }
        }

        private void ButtonOK_Click(object sender, EventArgs e)
        {


            if (ComboBoxWidth.SelectedIndex == 0)
            {
                sWidth = Math.Abs(int.Parse(TextBoxWidth.Text));
                sHeight = Math.Abs(int.Parse(TextBoxHeight.Text));
            }

            else
            {
                sWidth = Math.Abs(int.Parse(TextBoxWidth.Text) / 8);
                sHeight = Math.Abs(int.Parse(TextBoxHeight.Text) / 8);
            }

            if (sWidth * sHeight > 0x1000)
            {
                if (MessageBox.Show(this, "Um... This is a \"large\" sprite, and,\nNSE doesn't always play nice with \"large\" pictures.\nCreate the sprite anyways?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != System.Windows.Forms.DialogResult.Yes)
                {
                    return;
                }
            }

            sName = TextBoxName.Text;
            if (sName == "")
            {
                sName = "Untitled";
            }

            #region Palette
            if (ComboBoxBits.SelectedIndex == 0)
            {                
                if (ComboBoxColors.SelectedIndex == 1)
                {
                    byte[] paletteData = new byte[32];
                    for (int i = 0; i <= 15; i++)
                    {
                        byte[] col = NSE_Framework.Data.Translator.PaletteToByte(new NSE_Framework.Data.GBAcolor((byte)(i * 16), (byte)(i * 16), (byte)(i * 16)));
                        paletteData[i * 2] = col[0];
                        paletteData[i * 2 + 1] = col[1];
                    }
                    sPalette = new NSE_Framework.Data.SpritePalette(NSE_Framework.Data.SpritePalette.PaletteType.Color16, paletteData);
                }
                else
                {
                    byte[] paletteData = new byte[32] { 0xff, 0x7f, 0x00, 0x00, 0xef, 0x3d, 0x18, 0x63, 0x11, 0x08, 0x7d, 0x10, 0xff, 0x11, 0xdf, 0x03, 0xc4, 0x26, 0x80, 0x76, 0x27, 0x65, 0x34, 0x51, 0xf7, 0x29, 0x3f, 0x07, 0x96, 0x0f, 0x73, 0x77 };
                    sPalette = new NSE_Framework.Data.SpritePalette(NSE_Framework.Data.SpritePalette.PaletteType.Color16, paletteData);
                }
            }
            else if (ComboBoxBits.SelectedIndex == 1)
            {
                if (ComboBoxColors.SelectedIndex == 1)
                {
                    byte[] paletteData = new byte[512];

                    for (int i = 0; i <= 15; i++)
                    {
                        byte[] col = NSE_Framework.Data.Translator.PaletteToByte(new NSE_Framework.Data.GBAcolor((byte)(i * 16), (byte)(i * 16), (byte)(i * 16)));

                        for (int n = 0; n <= 15; n++)
                        {

                            paletteData[(n * 32) + (i * 2)] = col[0];
                            paletteData[(n * 32) + (i * 2) + 1] = col[1];
                        }
                    }

                    sPalette = new NSE_Framework.Data.SpritePalette(NSE_Framework.Data.SpritePalette.PaletteType.Color256, paletteData);
                }
                else
                {
                    #region 256Color
                    byte[] paletteData = new byte[512] { 0xBF, 0x00, 0x3F, 0x02, 0x9F, 0x03, 0xF6, 0x03, 0xEB, 0x03, 0xE1, 0x0B, 0xE0, 0x37, 0xE0, 0x67, 0x40, 0x7F, 0xE0, 0x7D, 0x60, 0x7C, 0x09, 0x7C, 0x14, 0x7C, 0x1E, 0x74, 0x1F, 0x48, 0x1F, 0x1C, 0xDE, 0x04, 0x3E, 0x06, 0x7E, 0x07, 0xD6, 0x07, 0xCB, 0x07, 0xC2, 0x0F, 0xC1, 0x37, 0xC1, 0x63, 0x41, 0x7B, 0xE1, 0x79, 0x81, 0x78, 0x29, 0x78, 0x34, 0x78, 0x3D, 0x70, 0x3E, 0x48, 0x3E, 0x20, 0xDD, 0x08, 0x1D, 0x0A, 0x5D, 0x0B, 0xB5, 0x0B, 0xAB, 0x0B, 0xA3, 0x0F, 0xA2, 0x37, 0xA2, 0x5F, 0x22, 0x77, 0xE2, 0x75, 0x82, 0x74, 0x49, 0x74, 0x54, 0x74, 0x5D, 0x6C, 0x5D, 0x48, 0x5D, 0x20, 0xFC, 0x0C, 0x1C, 0x0E, 0x3C, 0x0F, 0x95, 0x0F, 0x8C, 0x0F, 0x84, 0x13, 0x83, 0x37, 0x83, 0x5F, 0x03, 0x73, 0xE3, 0x71, 0xA3, 0x70, 0x6A, 0x70, 0x73, 0x70, 0x7C, 0x68, 0x7C, 0x48, 0x7C, 0x24, 0x1B, 0x11, 0x1B, 0x12, 0x3B, 0x13, 0x75, 0x13, 0x6C, 0x13, 0x65, 0x17, 0x64, 0x37, 0x64, 0x5B, 0x04, 0x6F, 0xE4, 0x6D, 0xC4, 0x6C, 0x8A, 0x6C, 0x93, 0x6C, 0x9B, 0x68, 0x9B, 0x48, 0x9B, 0x24, 0x1A, 0x15, 0x1A, 0x16, 0x1A, 0x17, 0x54, 0x17, 0x4C, 0x17, 0x45, 0x1B, 0x45, 0x3B, 0x45, 0x5B, 0xE5, 0x6A, 0xE5, 0x69, 0xE5, 0x68, 0xAB, 0x68, 0xB3, 0x68, 0xBA, 0x64, 0xBA, 0x44, 0xBA, 0x28, 0x39, 0x19, 0x19, 0x1A, 0xF9, 0x1A, 0x34, 0x1B, 0x2D, 0x1B, 0x26, 0x1F, 0x26, 0x3B, 0x26, 0x57, 0xC6, 0x66, 0xE6, 0x65, 0x06, 0x65, 0xCB, 0x64, 0xD2, 0x64, 0xD9, 0x60, 0xD9, 0x44, 0xD9, 0x28, 0x58, 0x1D, 0x18, 0x1E, 0xD8, 0x1E, 0x13, 0x1F, 0x0D, 0x1F, 0x07, 0x23, 0x07, 0x3B, 0x07, 0x53, 0xC7, 0x62, 0xE7, 0x61, 0x07, 0x61, 0xEC, 0x60, 0xF2, 0x60, 0xF8, 0x5C, 0xF8, 0x44, 0xF8, 0x2C, 0x57, 0x21, 0x17, 0x22, 0xB7, 0x22, 0xF3, 0x22, 0xED, 0x22, 0xE8, 0x26, 0xE8, 0x3A, 0xE8, 0x52, 0xA8, 0x5E, 0xE8, 0x5D, 0x28, 0x5D, 0x0C, 0x5D, 0x12, 0x5D, 0x17, 0x59, 0x17, 0x45, 0x17, 0x2D, 0x76, 0x25, 0x16, 0x26, 0xB6, 0x26, 0xD2, 0x26, 0xCD, 0x26, 0xC9, 0x2A, 0xC9, 0x3A, 0xC9, 0x4E, 0x89, 0x5A, 0xE9, 0x59, 0x49, 0x59, 0x2D, 0x59, 0x32, 0x59, 0x36, 0x55, 0x36, 0x45, 0x36, 0x31, 0x95, 0x29, 0x15, 0x2A, 0x95, 0x2A, 0xB2, 0x2A, 0xAE, 0x2A, 0xAA, 0x2A, 0xAA, 0x3A, 0xAA, 0x4E, 0x6A, 0x56, 0xEA, 0x55, 0x6A, 0x55, 0x4D, 0x55, 0x51, 0x55, 0x55, 0x51, 0x55, 0x45, 0x55, 0x31, 0x94, 0x2D, 0x14, 0x2E, 0x74, 0x2E, 0x92, 0x2E, 0x8E, 0x2E, 0x8B, 0x2E, 0x8B, 0x3E, 0x8B, 0x4A, 0x6B, 0x52, 0xEB, 0x51, 0x8B, 0x51, 0x6D, 0x51, 0x71, 0x51, 0x74, 0x51, 0x74, 0x41, 0x74, 0x35, 0xB3, 0x31, 0x13, 0x32, 0x53, 0x32, 0x71, 0x32, 0x6E, 0x32, 0x6C, 0x32, 0x6C, 0x3E, 0x6C, 0x4A, 0x4C, 0x4E, 0xEC, 0x4D, 0xAC, 0x4D, 0x8E, 0x4D, 0x91, 0x4D, 0x93, 0x4D, 0x93, 0x41, 0x93, 0x39, 0xD2, 0x35, 0x12, 0x36, 0x52, 0x36, 0x51, 0x36, 0x4F, 0x36, 0x4D, 0x36, 0x4D, 0x3E, 0x4D, 0x46, 0x2D, 0x4A, 0xED, 0x49, 0xAD, 0x49, 0xAE, 0x49, 0xB0, 0x49, 0xB2, 0x49, 0xB2, 0x41, 0xB2, 0x39, 0xD1, 0x39, 0x11, 0x3A, 0x31, 0x3A, 0x30, 0x3A, 0x2F, 0x3A, 0x2E, 0x3A, 0x2E, 0x3E, 0x2E, 0x46, 0x2E, 0x46, 0xEE, 0x45, 0xCE, 0x45, 0xCF, 0x45, 0xD0, 0x45, 0xD1, 0x45, 0xD1, 0x41, 0xD1, 0x3D, 0xF0, 0x3D, 0x10, 0x3E, 0x10, 0x3E, 0x10, 0x3E, 0x0F, 0x3E, 0x0F, 0x3E, 0x0F, 0x3E, 0x0F, 0x42, 0x0F, 0x42, 0xEF, 0x41, 0xEF, 0x41, 0xEF, 0x41, 0xF0, 0x41, 0xF0, 0x41, 0xFF, 0x7F, 0x00, 0x00 };
                    #endregion
                    sPalette = new NSE_Framework.Data.SpritePalette(NSE_Framework.Data.SpritePalette.PaletteType.Color256, paletteData);
                }

            }
            #endregion

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }

        private void ComboBoxBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshSize();
        }

        private void ComboBoxPreset_SelectedIndexChanged(object sender, EventArgs e)
        {
            rs = false;
            ComboBoxWidth.SelectedIndex = 0;

            if (ComboBoxPreset.SelectedIndex == 0)
            {
                TextBoxWidth.Text = "2";
                TextBoxHeight.Text = "4";
                ComboBoxBits.SelectedIndex = 0;
                ComboBoxColors.SelectedIndex = 0;
            }
            else if (ComboBoxPreset.SelectedIndex == 1)
            {
                TextBoxWidth.Text = "4";
                TextBoxHeight.Text = "4";
                ComboBoxBits.SelectedIndex = 0;
                ComboBoxColors.SelectedIndex = 0;
            }
            else if (ComboBoxPreset.SelectedIndex == 2)
            {
                TextBoxWidth.Text = "8";
                TextBoxHeight.Text = "8";
                ComboBoxBits.SelectedIndex = 0;
                ComboBoxColors.SelectedIndex = 0;
            }

            rs = true;
            RefreshSize();
        }

        private void TextBoxName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBoxName.Text))
            {
                TextBoxName.Text = "Untitled";
            }
        }


    }
}
