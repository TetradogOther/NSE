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
    public partial class Navigate : Form
    {
        bool _Refresh = true;
        bool b_Refresh = true;
        NSE_Framework.Data.Sprite PreviewSprite;

        NSE_Framework.IO.NBM_Decoder Decoder;

        NSE_Framework.IO.BookMark selectedBookMark = null;
        NSE_Framework.IO.BookMark SelectedBookMark
        {
            get { return selectedBookMark; }

            set 
            {
                selectedBookMark = value; 
                if (value == null)
                {
                    GroupEditMode.Enabled = false;
                    GroupEditOffsets.Enabled = false;
                    GroupEditPreview.Enabled = false;
                    GroupEditSize.Enabled = false;
                }
                else
                {
                    GroupEditMode.Enabled = true;
                    GroupEditOffsets.Enabled = true;
                    GroupEditPreview.Enabled = true;
                    GroupEditSize.Enabled = true;

                    if (value.Commands.Count != 0 && Program.MainForm.BookMarkScripting == true)
                    {
                        Decoder.Decode(ref selectedBookMark);
                    }

                    b_Refresh = false;
                    _Refresh = false;


                    if (value.SpriteType == NSE_Framework.Data.Sprite.SpriteType.Color16)
                    {
                        ComboBoxEditColors.SelectedIndex = 0;
                        ComboBoxNavigateColors.SelectedIndex = 0;
                    }
                    else
                    {
                        ComboBoxEditColors.SelectedIndex = 1;
                        ComboBoxNavigateColors.SelectedIndex = 1;
                    }

                    TextBoxEditImage.Text = value.ImageOffset.ToString("X");
                    TextboxNavigateImage.Text = value.ImageOffset.ToString("X");

                    if (value.PaletteOffset != -1)
                    {
                        TextBoxEditPalette.Text = value.PaletteOffset.ToString("X");
                        EditGray.Checked = false;

                        TextboxNavigatePalette.Text = value.PaletteOffset.ToString("X");
                        NavigateGray.Checked = false;
                    }
                    else
                    {
                        TextBoxEditPalette.Text = "0";
                        EditGray.Checked = true;

                        TextboxNavigatePalette.Text = "0";
                        NavigateGray.Checked = true;
                    }

                    ComboBoxEditWidth.DropDownStyle = ComboBoxStyle.Simple;
                    ComboBoxEditHeight.DropDownStyle = ComboBoxStyle.Simple;
                    ComboBoxEditHeight.Enabled = true;

                    ComboBoxNavigateWidth.DropDownStyle = ComboBoxStyle.Simple;
                    ComboBoxNavigateHeight.DropDownStyle = ComboBoxStyle.Simple;
                    ComboBoxNavigateHeight.Enabled = true;

                    //ComboBoxEditHeight.Enabled = true;
                    //ComboBoxEditWidth.Enabled = true;

                    ComboBoxEditWidth.Text = value.Width.ToString();
                    ComboBoxNavigateWidth.Text = value.Width.ToString();

                    ComboBoxEditHeight.Text = value.Height.ToString();
                    ComboBoxNavigateHeight.Text = value.Height.ToString();

                    

                    EditLz77.Checked = value.Lz77;
                    NavigateLz77.Checked = value.Lz77;

                    b_Refresh = true;
                    _Refresh = true;



                    if (Tabs.SelectedIndex == 0)
                    {
                        RefreshPreview(this, null);
                    }
                    else
                    {
                        RefreshBookMarkPreview(this, null);
                    } 
                }

                
            }
        }

        public Navigate()
        {
            InitializeComponent();
            _Refresh = true;
            PreviewSprite = new NSE_Framework.Data.Sprite(1, 1, NSE_Framework.Data.Sprite.SpriteType.Color16);
            
        }

        private void Open_Click(object sender, EventArgs e)
        {
            //Program.MainForm.ActiveMdiChild.WindowState = FormWindowState.Normal;
            
            int spriteOffset = int.Parse(TextboxNavigateImage.Text, System.Globalization.NumberStyles.HexNumber);
            int paletteOffset = int.Parse(TextboxNavigatePalette.Text, System.Globalization.NumberStyles.HexNumber);

            int spriteWidth = int.Parse(ComboBoxNavigateWidth.Text);
            int spriteHeight = int.Parse(ComboBoxNavigateHeight.Text);

            int spriteLz = CheckLz77(spriteOffset, CheckLz77Type.Sprite);;
            int paletteLz;
            if (NavigateGray.Checked == true)
            {
                paletteLz =-1;
                paletteOffset = -1;
            }
            else
            {
                paletteLz = CheckLz77(paletteOffset, CheckLz77Type.Palette);
                
            }

            
             



            byte[] spriteData;
            if (spriteLz != -1 && NavigateLz77.Checked == true)
            {
                NavigateLz77.Enabled = true;
                spriteData = Program.MainForm.Read.ReadLz77Bytes(spriteOffset, out spriteLz);
            }
            else
            {
                if (spriteLz == -1)
                {
                    NavigateLz77.Checked = true;
                    NavigateLz77.Enabled = false;
                }
                else
                {
                    spriteLz = -1;
                }
                # region CheckTypeSpriteData
                if (ComboBoxNavigateColors.SelectedIndex == 0)
                {
                    spriteData = Program.MainForm.Read.ReadBytes(spriteOffset, spriteWidth * spriteHeight * 32);
                }
                else if (ComboBoxNavigateColors.SelectedIndex == 1)
                {
                    spriteData = Program.MainForm.Read.ReadBytes(spriteOffset, spriteWidth * spriteHeight * 64);
                }
                else
                {
                    throw new Exception("Non-supported sprite type.");
                }
                #endregion
            }

            byte[] paletteData;
            if (paletteLz != -1)
            {
                paletteData = Program.MainForm.Read.ReadLz77Bytes(paletteOffset, out paletteLz);
            }
            else
            {
                #region CheckTypePalette
                if (ComboBoxNavigateColors.SelectedIndex == 0)
                {
                    if (NavigateGray.Checked == false)
                    {
                        paletteData = Program.MainForm.Read.ReadBytes(paletteOffset, 32);
                    }
                    else
                    {
                        paletteData = new byte[32];

                        for (int i = 0; i <= 15; i++)
                        {
                            byte[] col = NSE_Framework.Data.Translator.PaletteToByte(new NSE_Framework.Data.GBAcolor((byte)(i * 16), (byte)(i * 16), (byte)(i * 16)));
                            paletteData[i * 2] = col[0];
                            paletteData[i * 2 + 1] = col[1];
                        }

                        paletteOffset = -1;

                    }
                }
                else if (ComboBoxNavigateColors.SelectedIndex == 1)
                {
                    if (NavigateGray.Checked == false)
                    {
                        paletteData = Program.MainForm.Read.ReadBytes(paletteOffset, 512);
                    }
                    else
                    {
                        paletteData = new byte[512];

                        for (int i = 0; i <= 15; i++)
                        {
                            byte[] col = NSE_Framework.Data.Translator.PaletteToByte(new NSE_Framework.Data.GBAcolor((byte)(i * 16), (byte)(i * 16), (byte)(i * 16)));

                            for (int n = 0; n <= 15; n++)
                            {

                                paletteData[(n * 32) + (i * 2)] = col[0];
                                paletteData[(n * 32) + (i * 2) + 1] = col[1];
                            } 
                        }
                        paletteOffset = -1;
                    }
                }
                else
                {
                    throw new Exception("Non-supported sprite type.");
                }
                #endregion

            }

            NSE_Framework.Data.Sprite s;
            #region CheckTypeSprite
            if (ComboBoxNavigateColors.SelectedIndex == 0)
            {


                 s = new NSE_Framework.Data.Sprite(spriteOffset, paletteOffset, spriteWidth, spriteHeight,
    NSE_Framework.Data.Sprite.SpriteType.Color16,
     spriteData,
         new NSE_Framework.Data.SpritePalette(
            NSE_Framework.Data.SpritePalette.PaletteType.Color16,
             paletteData)
     );
            }

            else if (ComboBoxNavigateColors.SelectedIndex == 1)
            {

                s = new NSE_Framework.Data.Sprite(spriteOffset, paletteOffset, spriteWidth, spriteHeight,
    NSE_Framework.Data.Sprite.SpriteType.Color256,
     spriteData,
         new NSE_Framework.Data.SpritePalette(
            NSE_Framework.Data.SpritePalette.PaletteType.Color256,
             paletteData)
     );
            }

            else
            {
                throw new Exception("Non-supported sprite type.");
            }

            #endregion
            s.CompressedSprite = spriteLz;
            s.CompressedPalette = paletteLz;
            
            EditorForm edit = new EditorForm(s);
            
            Program.MainForm.Focus();

            edit.MdiParent = Program.MainForm;
            edit.Show();

            if (!Directory.Exists(Application.StartupPath + "\\Core\\Logs"))
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Core\\Logs");
            }

            string pal = "GrayScale";
            if (s.UniquePalette == true)
            {
                pal = "0x" + s.PaletteOffset.ToString("X");
            }

            Program.MainForm.LogWriter.LogMessage("Navigated 0x" +
                s.ImageOffset.ToString("X") + " " + pal + " : " + s.Width.ToString() +", " + s.Height.ToString());

            if (Program.MainForm.Splash.Visible == true)
            {
                Program.MainForm.Splash.Visible = false;
            }
        }
        private void RefreshPreview(object sender, EventArgs e)
        {
            bool show = false;
            if (sender.GetType() == typeof(TextBox))
            {
                TextBox t = (TextBox)sender;
                if (t.TextLength > 0)
                {
                    if (int.Parse(t.Text, System.Globalization.NumberStyles.HexNumber) > Program.MainForm.Read.FileLength)
                    {
                        t.Text = Program.MainForm.Read.FileLength.ToString("X");
                    }
                    show = true;
                }
            }
            else if (sender.GetType() == typeof(ComboBox))
            {
                ComboBox c = (ComboBox)sender;
                if (c.Text.Length > 0 && c.Text != "0" && c.Enabled == true)
                {
                    show = true;
                    
                }
            }
            else if (sender.GetType() == typeof(CheckBox))
            {
                    show = true;
            }
            else if (sender == this)
            {
                show = true;
            }

            if (show == true)
            {
                int spriteOffset = int.Parse(TextboxNavigateImage.Text, System.Globalization.NumberStyles.HexNumber);
                int paletteOffset = int.Parse(TextboxNavigatePalette.Text, System.Globalization.NumberStyles.HexNumber);


                if (paletteOffset > Program.MainForm.Read.FileLength - 512)
                {
                    TextboxNavigatePalette.Text = (Program.MainForm.Read.FileLength - 512).ToString("X");
                }

                if (spriteOffset > Program.MainForm.Read.FileLength - 1)
                {
                    TextboxNavigateImage.Text = (Program.MainForm.Read.FileLength - 1).ToString("X");
                }

            }

            if (show == true && _Refresh == true)
            {
                _Refresh = false;
                int spriteOffset = int.Parse(TextboxNavigateImage.Text, System.Globalization.NumberStyles.HexNumber);
                int paletteOffset = int.Parse(TextboxNavigatePalette.Text, System.Globalization.NumberStyles.HexNumber);

                int spriteLz = CheckLz77(spriteOffset, CheckLz77Type.Sprite);
                int paletteLz = CheckLz77(paletteOffset, CheckLz77Type.Palette);

                int spriteWidth = 1;
                int spriteHeight = 1;

                if (ComboBoxNavigateWidth.Text == "")
                    ComboBoxNavigateWidth.Text = "1";

                if (ComboBoxNavigateHeight.Text == "")
                    ComboBoxNavigateHeight.Text = "1";

                if (ComboBoxNavigateWidth.DropDownStyle == ComboBoxStyle.DropDownList)
                {
                    if (ComboBoxNavigateWidth.SelectedItem == null)
                    {
                        ComboBoxNavigateWidth.SelectedIndex = 0;

                    }
                    spriteWidth = int.Parse(ComboBoxNavigateWidth.SelectedItem.ToString());
                    ComboBoxNavigateWidth.Text = ComboBoxNavigateWidth.SelectedItem.ToString();
                }
                else
                {
                    spriteWidth = int.Parse(ComboBoxNavigateWidth.Text);
                    spriteHeight = int.Parse(ComboBoxNavigateHeight.Text);
                }
                
                
                 

                if (NavigateGray.Checked == true)
                {
                    paletteLz = -1;
                }

                if (NavigateGray.Checked == true)
                {
                    paletteOffset = -1;
                }

                NSE_Framework.Data.Sprite.SpriteType checkType;

                if (ComboBoxNavigateColors.SelectedIndex == 1)
                {
                    checkType = NSE_Framework.Data.Sprite.SpriteType.Color256;
                }
                else
                {
                    checkType = NSE_Framework.Data.Sprite.SpriteType.Color16;
                }

                byte[] spriteData;
                if (spriteLz != -1 && NavigateLz77.Checked == true)
                {
                    NavigateLz77.Enabled = true;
                    spriteData = Program.MainForm.Read.ReadLz77Bytes(spriteOffset, out spriteLz);

                    if (PreviewSprite.ImageOffset != spriteOffset || checkType != PreviewSprite.Type || ComboBoxNavigateWidth.DropDownStyle != ComboBoxStyle.DropDownList)
                    {                     
                        # region GenerateSizes
                        
                        ComboBoxNavigateWidth.Items.Clear();
                        ComboBoxNavigateHeight.Enabled = false;
                        ComboBoxNavigateHeight.Text = "0";

                        ComboBoxNavigateWidth.DropDownStyle = ComboBoxStyle.DropDownList;

                        int l;
                        if (checkType == NSE_Framework.Data.Sprite.SpriteType.Color16)
                        {
                            l = spriteData.Length / 32;
                        }
                        else
                        {
                            l = spriteData.Length / 64;
                        }

                        int s = 1;

                        while (s <= l)
                        {
                            if (l % s == 0)
                            {
                                ComboBoxNavigateWidth.Items.Add(s.ToString());
                            }

                            s++;
                        }

                        if (ComboBoxNavigateWidth.SelectedItem == null)
                        {
                            if (SelectedBookMark != null)
                            {
                                if (ComboBoxNavigateWidth.Items.Contains(SelectedBookMark.Width.ToString()) == true)
                                {
                                    ComboBoxNavigateWidth.SelectedIndex = ComboBoxNavigateWidth.Items.IndexOf(SelectedBookMark.Width.ToString());
                                }
                                else if (ComboBoxNavigateWidth.Items.Count > 1)
                                {
                                    ComboBoxNavigateWidth.SelectedIndex = ComboBoxNavigateWidth.Items.Count / 2;
                                }
                                else
                                {
                                    ComboBoxNavigateWidth.SelectedIndex = 0;
                                }
                            }
                            else if (ComboBoxNavigateWidth.Items.Count > 1)
                            {
                                ComboBoxNavigateWidth.SelectedIndex = ComboBoxNavigateWidth.Items.Count / 2;
                            }
                            else
                            {
                                ComboBoxNavigateWidth.SelectedIndex = 0;
                            }

                        }
                        spriteWidth = int.Parse(ComboBoxNavigateWidth.SelectedItem.ToString());
                        ComboBoxNavigateWidth.Text = ComboBoxNavigateWidth.SelectedItem.ToString();


                        #endregion
                    }

                    if (checkType == NSE_Framework.Data.Sprite.SpriteType.Color16)
                    {
                        spriteHeight = spriteData.Length / 32 / spriteWidth;
                    }
                    else
                    {
                        spriteHeight = spriteData.Length / 64 / spriteWidth;
                    }
                    
                    ComboBoxNavigateHeight.Text = spriteHeight.ToString();

                }
                else
                {
                    if (spriteLz == -1)
                    {

                        NavigateLz77.Checked = true;
                        NavigateLz77.Enabled = false;

                    }
                    else
                    {
                        NavigateLz77.Enabled = true;
                    }

                    if (ComboBoxNavigateWidth.DropDownStyle == ComboBoxStyle.DropDownList)
                    {
                        #region UnGenerateSizes
                        
                        ComboBoxNavigateWidth.DropDownStyle = ComboBoxStyle.DropDown;
                        ComboBoxNavigateWidth.Items.Clear();
                        ComboBoxNavigateWidth.Items.Add(2);
                        ComboBoxNavigateWidth.Items.Add(4);
                        ComboBoxNavigateWidth.Items.Add(8);
                        ComboBoxNavigateWidth.Items.Add(16);

                        ComboBoxNavigateHeight.Enabled = true;
                        ComboBoxNavigateHeight.Items.Clear();
                        ComboBoxNavigateHeight.Items.Add(2);
                        ComboBoxNavigateHeight.Items.Add(4);
                        ComboBoxNavigateHeight.Items.Add(8);
                        ComboBoxNavigateHeight.Items.Add(16);
                        

                        spriteHeight = int.Parse(ComboBoxNavigateHeight.Text);
                        #endregion
                    }

                    # region CheckTypeSpriteData
                    if (ComboBoxNavigateColors.SelectedIndex == 0)
                    {
                        spriteData = Program.MainForm.Read.ReadBytes(spriteOffset, spriteWidth * spriteHeight * 32);
                    }
                    else if (ComboBoxNavigateColors.SelectedIndex == 1)
                    {
                        spriteData = Program.MainForm.Read.ReadBytes(spriteOffset, spriteWidth * spriteHeight * 64);
                    }
                    else
                    {
                        _Refresh = true;
                        throw new Exception("Non-supported sprite type.");
                    }
                    #endregion
                }

                byte[] paletteData;
                if (paletteLz != -1)
                {
                    paletteData = Program.MainForm.Read.ReadLz77Bytes(paletteOffset, out paletteLz);
                }
                else
                {
                    #region CheckTypePalette
                    if (ComboBoxNavigateColors.SelectedIndex == 0)
                    {
                        if (NavigateGray.Checked == false)
                        {
                            paletteData = Program.MainForm.Read.ReadBytes(paletteOffset, 32);
                        }
                        else
                        {
                            paletteData = new byte[32];

                            for (int i = 0; i <= 15; i++)
                            {
                                byte[] col = NSE_Framework.Data.Translator.PaletteToByte(new NSE_Framework.Data.GBAcolor((byte)(i * 16), (byte)(i * 16), (byte)(i * 16)));
                                paletteData[i * 2] = col[0];
                                paletteData[i * 2 + 1] = col[1];
                            }

                        }
                    }
                    else if (ComboBoxNavigateColors.SelectedIndex == 1)
                    {
                        if (NavigateGray.Checked == false)
                        {
                            paletteData = Program.MainForm.Read.ReadBytes(paletteOffset, 512);
                        }
                        else
                        {
                            paletteData = new byte[512];

                            for (int i = 0; i < 16; i++)
                            {
                                byte[] col = NSE_Framework.Data.Translator.PaletteToByte(new NSE_Framework.Data.GBAcolor((byte)(i * 16), (byte)(i * 16), (byte)(i * 16)));

                                for (int n = 0; n < 16; n++)
                                {

                                    paletteData[(n * 32) + (i * 2)] = col[0];
                                    paletteData[(n * 32) + (i * 2) + 1] = col[1];
                                }
                            }

                        }
                    }
                    else
                    {
                        _Refresh = true;
                        throw new Exception("Non-supported sprite type.");
                    }
                    #endregion
                }

                #region CheckTypeSprite
                if (ComboBoxNavigateColors.SelectedIndex == 0)
                {


                    PreviewSprite = new NSE_Framework.Data.Sprite(spriteOffset, paletteOffset, spriteWidth, spriteHeight,
       NSE_Framework.Data.Sprite.SpriteType.Color16,
        spriteData,
            new NSE_Framework.Data.SpritePalette(
               NSE_Framework.Data.SpritePalette.PaletteType.Color16,
                paletteData)
        );
                }

                else if (ComboBoxNavigateColors.SelectedIndex == 1)
                {

                    PreviewSprite = new NSE_Framework.Data.Sprite(spriteOffset, paletteOffset, spriteWidth, spriteHeight,
        NSE_Framework.Data.Sprite.SpriteType.Color256,
         spriteData,
             new NSE_Framework.Data.SpritePalette(
                NSE_Framework.Data.SpritePalette.PaletteType.Color256,
                 paletteData)
         );
                }

                else
                {
                    _Refresh = true;
                    throw new Exception("Non-supported sprite type.");
                }

                #endregion


                Bitmap bm = new Bitmap(spriteWidth * 8, spriteHeight * 8);

                NSE_Framework.Draw.uDrawData(
                    ref bm,
                    PreviewSprite.ImageData,
                    PreviewSprite.Palette,
                    new Size(PreviewSprite.Width, PreviewSprite.Height));
                NavigatePreview.Image = bm;

                _Refresh = true;
                
            }

 
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

        private void LeaveFeild(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(TextBox))
            {
                TextBox t = (TextBox)sender;
                if (t.TextLength == 0)
                {
                    t.Text = "0";
                }
            }
            else if(sender.GetType() == typeof(ComboBox))
            {
                ComboBox c = (ComboBox)sender;
                if (c.Text.Length == 0 || c.Text == "0")
                {
                    c.Text = "1";
                }
            }
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Navigate_Load(object sender, EventArgs e)
        {
            this.ComboBoxNavigateColors.SelectedIndex = 0;

            if (Program.BookMarkTree == null)
            {
                Program.BookMarkTree = new NSE_Framework.IO.BookMarkTree("BookMarks");               
            }

            //NSE_Framework.IO.BookMark testScript = new NSE_Framework.IO.BookMark("TestScript", 0, -1, 1, 1, NSE_Framework.Data.Sprite.SpriteType.Color16);
            
            //testScript.Commands.Add(new byte[]{0x09,0x01, 0x00,0x00, 0xAC,0x00,0x00,0x00, 0x04,0x00,0x00,0x00});
            //testScript.Commands.Add(new byte[]{0x03,0x01, 0x00,0x00, 0x00,0x00,0x00,0x00, 0x42,0x50,0x52,0x45});
            //    testScript.Commands.Add(new byte[]{0x0A,0x01, 0x00, 0x01,0x00,0x00,0x00});
            //testScript.Commands.Add(new byte[]{0x02});

            //Program.BookMarkTree.AddBookMark(testScript);

            this.BookMarkTreeView.Nodes.Clear();
            TreeNode node = new TreeNode(Program.BookMarkTree.Name,1,1);
            node.Tag = -1;
           
            CreateTree(ref Program.BookMarkTree, ref node);

            BookMarkTreeView.Nodes.Add(node);

            Decoder = new NSE_Framework.IO.NBM_Decoder(ref Program.MainForm.Read);

            if(Program.MainForm.Read.FileLength > 0x1000000)
            {
                TextboxNavigateImage.MaxLength = 7;
                TextboxNavigatePalette.MaxLength = 7;
                TextBoxEditImage.MaxLength = 7;
                TextBoxEditPalette.MaxLength = 7;
            }
        }

        private void CreateTree(ref NSE_Framework.IO.BookMarkTree Tree,ref TreeNode Parent)
        {
            
            for (int t = 0; t < Tree.ChildTrees.Count; t++)
            {
                TreeNode node = new TreeNode(Tree.ChildTrees[t].Name,1,1);
                node.Tag = -1;
                NSE_Framework.IO.BookMarkTree tree = Tree.ChildTrees[t];
                //if (tree.Parent == null)
                //{
                    tree.Parent = Tree;
                //}
                CreateTree(ref tree, ref node);

                Parent.Nodes.Add(node);

            }

            //int i = 0;
            foreach (NSE_Framework.IO.BookMark b in Tree.BookMarks)
            {
                TreeNode node = new TreeNode(b.Name);
                node.Tag = 0;
                Parent.Nodes.Add(node);

                //if (b.Parent == null)
                //{
                    b.Parent = Tree;
                //}
                //i++;
            }

        }

        enum CheckLz77Type
        {
            Sprite,
            Palette
        }

        int CheckLz77(int Offset, CheckLz77Type Type)
        {
            byte[] test = Program.MainForm.Read.ReadBytes(Offset, 5);
            if (test[0] == 0x10)
            {
                int length = BitConverter.ToInt32(new Byte[] { test[1], test[2], test[3] ,0x0}, 0);

                if (Type == CheckLz77Type.Sprite && test[4] <= 63 && length >= 64 && length % 8 == 0)
                {
                    return length;
                }
                else if (Type == CheckLz77Type.Palette && length == 0x20 && test[4] <= 63 || Type == CheckLz77Type.Palette && length == 0x200)
                {
                    return length;
                }
                else
                {
                    return -1;
                }


            }
            else
            {
                return -1;
            }
        }

        //////////////////////////////////

        private void button1_Click(object sender, EventArgs e)
        {
            NSE_Framework.IO.BookMark b = new NSE_Framework.IO.BookMark("Test", 0, 1, 1, 1, NSE_Framework.Data.Sprite.SpriteType.Color16);
            //b.Commands.Add(new NSE_Framework.IO.BookMarkCommands.Append(5));
            //b.Commands.Add(new NSE_Framework.IO.BookMarkCommands.Append(1));
            //b.Commands.Add(new NSE_Framework.IO.BookMarkCommands.Add(0, 1, NSE_Framework.IO.BookMarkCommands.MathEnum.Mem_Val));
            //b.Commands.Add(new NSE_Framework.IO.BookMarkCommands.Return(0, NSE_Framework.IO.BookMarkCommands.ReturnType.ImageOffset));
            
            b.Commands.Add(new byte[] { 0x01, 0x01, 0x00, 0x00, 0x00, 0x00, 0x00 });
            b.Commands.Add(new byte[] { 0x08, 0x01, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,
                0x05, 0x00, 0x00, 0x00 });
            b.Commands.Add(new byte[] { 0x09, 0x01, 0x00, 0x00,
                0xF8, 0x3B, 0x3A, 0x00,
                0x04, 0x00, 0x00, 0x00});

            NSE_Framework.IO.NBM_Decoder d = new NSE_Framework.IO.NBM_Decoder(ref Program.MainForm.Read);
            d.Decode(ref b);
            b.Commands.Clear();
        }

        private void RefreshBookMarkPreview(object sender, EventArgs e)
        {
            bool show = false;
            if (sender.GetType() == typeof(TextBox))
            {
                TextBox t = (TextBox)sender;
                if (t.TextLength > 0)
                {
                    if (int.Parse(t.Text, System.Globalization.NumberStyles.HexNumber) > Program.MainForm.Read.FileLength)
                    {
                        t.Text = Program.MainForm.Read.FileLength.ToString("X");
                    }
                    show = true;
                }
            }
            else if (sender.GetType() == typeof(ComboBox))
            {
                ComboBox c = (ComboBox)sender;
                if (c.Text.Length > 0 && c.Text != "0" && c.Enabled == true)
                {
                    show = true;
                }
            }
            else if (sender.GetType() == typeof(CheckBox))
            {
                show = true;
            }
            else if (sender == this)
            {
                show = true;
            }

            if (show == true)
            {
                int spriteOffset = int.Parse(TextBoxEditImage.Text, System.Globalization.NumberStyles.HexNumber);
                int paletteOffset = int.Parse(TextBoxEditPalette.Text, System.Globalization.NumberStyles.HexNumber);


                if (paletteOffset > Program.MainForm.Read.FileLength - 512)
                {
                    TextBoxEditPalette.Text = (Program.MainForm.Read.FileLength - 512).ToString("X");
                }
                if (spriteOffset > Program.MainForm.Read.FileLength - 1)
                {
                    TextBoxEditImage.Text = (Program.MainForm.Read.FileLength - 1).ToString("X");
                }

            }
            if (show == true && b_Refresh == true)
            {
                b_Refresh = false;

                int spriteOffset = int.Parse(TextBoxEditImage.Text, System.Globalization.NumberStyles.HexNumber);
                int paletteOffset = int.Parse(TextBoxEditPalette.Text, System.Globalization.NumberStyles.HexNumber);

                int spriteLz = CheckLz77(spriteOffset, CheckLz77Type.Sprite);
                int paletteLz = CheckLz77(paletteOffset, CheckLz77Type.Palette);

                int spriteWidth = 1;
                int spriteHeight = 1;

                if (ComboBoxEditWidth.Text == "")
                    ComboBoxEditWidth.Text = "1";

                if (ComboBoxEditHeight.Text == "")
                    ComboBoxEditHeight.Text = "1";

                if (ComboBoxEditWidth.DropDownStyle == ComboBoxStyle.DropDownList  && spriteLz != -1)
                {
                    if (ComboBoxEditWidth.SelectedItem == null)
                    {
                        ComboBoxEditWidth.SelectedIndex = 0;

                    }
                    spriteWidth = int.Parse(ComboBoxEditWidth.SelectedItem.ToString());
                    ComboBoxEditWidth.Text = ComboBoxEditWidth.SelectedItem.ToString();
                }
                else
                {
                    spriteWidth = int.Parse(ComboBoxEditWidth.Text);
                    spriteHeight = int.Parse(ComboBoxEditHeight.Text);
                }

                    selectedBookMark.Width = spriteWidth;
                    selectedBookMark.Height = spriteHeight;
                    selectedBookMark.ImageOffset = spriteOffset;
                    


                if (EditGray.Checked == true)
                {
                    paletteLz = -1;
                }

                if (EditGray.Checked == true)
                {
                    paletteOffset = -1;
                }

                selectedBookMark.PaletteOffset = paletteOffset;

                NSE_Framework.Data.Sprite.SpriteType checkType;

                if (ComboBoxEditColors.SelectedIndex == 1)
                {
                    checkType = NSE_Framework.Data.Sprite.SpriteType.Color256;
                }
                else
                {
                    checkType = NSE_Framework.Data.Sprite.SpriteType.Color16;
                }

                selectedBookMark.SpriteType = checkType;

                byte[] spriteData;
                if (spriteLz != -1 && EditLz77.Checked == true)
                {
                    EditLz77.Enabled = true;
                    spriteData = Program.MainForm.Read.ReadLz77Bytes(spriteOffset, out spriteLz);
                    selectedBookMark.Lz77 = true;

                    if (PreviewSprite.ImageOffset != spriteOffset || checkType != PreviewSprite.Type || ComboBoxEditWidth.DropDownStyle != ComboBoxStyle.DropDownList)
                    {
                        # region GenerateSizes

                        ComboBoxEditWidth.Items.Clear();
                        ComboBoxEditHeight.Enabled = false;
                        ComboBoxEditHeight.Text = "0";

                        ComboBoxEditWidth.DropDownStyle = ComboBoxStyle.DropDownList;

                        int l;
                        if (checkType == NSE_Framework.Data.Sprite.SpriteType.Color16)
                        {
                            l = spriteData.Length / 32;
                        }
                        else
                        {
                            l = spriteData.Length / 64;
                        }

                        int s = 1;

                        while (s <= l)
                        {
                            if (l % s == 0)
                            {
                                ComboBoxEditWidth.Items.Add(s.ToString());
                            }

                            s++;
                        }

                        if (ComboBoxEditWidth.SelectedItem == null)
                        {
                            if (SelectedBookMark != null)
                            {
                                if (ComboBoxEditWidth.Items.Contains(SelectedBookMark.Width.ToString()) == true)
                                {
                                    ComboBoxEditWidth.SelectedIndex = ComboBoxEditWidth.Items.IndexOf(SelectedBookMark.Width.ToString());
                                }
                                else if (ComboBoxEditWidth.Items.Count > 1)
                                {
                                    ComboBoxEditWidth.SelectedIndex = ComboBoxEditWidth.Items.Count / 2;
                                }
                                else
                                {
                                    ComboBoxEditWidth.SelectedIndex = 0;
                                }
                            }
                            else if (ComboBoxEditWidth.Items.Count > 1)
                            {
                                ComboBoxEditWidth.SelectedIndex = ComboBoxEditWidth.Items.Count / 2;
                            }
                            else
                            {
                                ComboBoxEditWidth.SelectedIndex = 0;
                            }

                        }
                        spriteWidth = int.Parse(ComboBoxEditWidth.SelectedItem.ToString());
                        ComboBoxEditWidth.Text = ComboBoxEditWidth.SelectedItem.ToString();


                        #endregion
                    }

                    if (checkType == NSE_Framework.Data.Sprite.SpriteType.Color16)
                    {
                        spriteHeight = spriteData.Length / 32 / spriteWidth;
                    }
                    else
                    {
                        spriteHeight = spriteData.Length / 64 / spriteWidth;
                    }

                    ComboBoxEditHeight.Text = spriteHeight.ToString();

                }
                else
                {
                    if (spriteLz == -1)
                    {

                        EditLz77.Checked = true;
                        EditLz77.Enabled = false;

                    }
                    else
                    {
                        EditLz77.Enabled = true;
                    }

                    if (ComboBoxEditWidth.DropDownStyle == ComboBoxStyle.DropDownList)
                    {
                        #region UnGenerateSizes

                        ComboBoxEditWidth.DropDownStyle = ComboBoxStyle.DropDown;
                        ComboBoxEditWidth.Items.Clear();
                        ComboBoxEditWidth.Items.Add(2);
                        ComboBoxEditWidth.Items.Add(4);
                        ComboBoxEditWidth.Items.Add(8);
                        ComboBoxEditWidth.Items.Add(16);

                        ComboBoxEditHeight.Enabled = true;
                        ComboBoxEditHeight.Items.Clear();
                        ComboBoxEditHeight.Items.Add(2);
                        ComboBoxEditHeight.Items.Add(4);
                        ComboBoxEditHeight.Items.Add(8);
                        ComboBoxEditHeight.Items.Add(16);


                        spriteHeight = int.Parse(ComboBoxEditHeight.Text);
                        #endregion
                    }

                    # region CheckTypeSpriteData
                    if (ComboBoxEditColors.SelectedIndex == 0)
                    {
                        spriteData = Program.MainForm.Read.ReadBytes(spriteOffset, spriteWidth * spriteHeight * 32);
                    }
                    else if (ComboBoxEditColors.SelectedIndex == 1)
                    {
                        spriteData = Program.MainForm.Read.ReadBytes(spriteOffset, spriteWidth * spriteHeight * 64);
                    }
                    else
                    {
                        b_Refresh = true;
                        throw new Exception("Non-supported sprite type.");
                    }
                    #endregion
                    selectedBookMark.Lz77 = false;
                }

                byte[] paletteData;
                if (paletteLz != -1)
                {
                    paletteData = Program.MainForm.Read.ReadLz77Bytes(paletteOffset, out paletteLz);
                }
                else
                {
                    #region CheckTypePalette
                    if (ComboBoxEditColors.SelectedIndex == 0)
                    {
                        if (EditGray.Checked == false)
                        {
                            paletteData = Program.MainForm.Read.ReadBytes(paletteOffset, 32);
                        }
                        else
                        {
                            paletteData = new byte[32];

                            for (int i = 0; i <= 15; i++)
                            {
                                byte[] col = NSE_Framework.Data.Translator.PaletteToByte(new NSE_Framework.Data.GBAcolor((byte)(i * 16), (byte)(i * 16), (byte)(i * 16)));
                                paletteData[i * 2] = col[0];
                                paletteData[i * 2 + 1] = col[1];
                            }

                        }
                    }
                    else if (ComboBoxEditColors.SelectedIndex == 1)
                    {
                        if (EditGray.Checked == false)
                        {
                            paletteData = Program.MainForm.Read.ReadBytes(paletteOffset, 512);
                        }
                        else
                        {
                            paletteData = new byte[512];

                            for (int i = 0; i < 16; i++)
                            {
                                byte[] col = NSE_Framework.Data.Translator.PaletteToByte(new NSE_Framework.Data.GBAcolor((byte)(i * 16), (byte)(i * 16), (byte)(i * 16)));

                                for (int n = 0; n < 16; n++)
                                {

                                    paletteData[(n * 32) + (i * 2)] = col[0];
                                    paletteData[(n * 32) + (i * 2) + 1] = col[1];
                                }
                            }

                        }
                    }
                    else
                    {
                        b_Refresh = true;
                        throw new Exception("Non-supported sprite type.");
                    }
                    #endregion
                }

                #region CheckTypeSprite
                if (ComboBoxEditColors.SelectedIndex == 0)
                {


                    PreviewSprite = new NSE_Framework.Data.Sprite(spriteOffset, paletteOffset, spriteWidth, spriteHeight,
       NSE_Framework.Data.Sprite.SpriteType.Color16,
        spriteData,
            new NSE_Framework.Data.SpritePalette(
               NSE_Framework.Data.SpritePalette.PaletteType.Color16,
                paletteData)
        );
                }

                else if (ComboBoxEditColors.SelectedIndex == 1)
                {

                    PreviewSprite = new NSE_Framework.Data.Sprite(spriteOffset, paletteOffset, spriteWidth, spriteHeight,
        NSE_Framework.Data.Sprite.SpriteType.Color256,
         spriteData,
             new NSE_Framework.Data.SpritePalette(
                NSE_Framework.Data.SpritePalette.PaletteType.Color256,
                 paletteData)
         );
                }

                else
                {
                    b_Refresh = true;
                    throw new Exception("Non-supported sprite type.");
                }

                #endregion


                Bitmap bm = new Bitmap(spriteWidth * 8, spriteHeight * 8);

                NSE_Framework.Draw.uDrawData(
                    ref bm,
                    PreviewSprite.ImageData,
                    PreviewSprite.Palette,
                    new Size(PreviewSprite.Width, PreviewSprite.Height));
                EditPreview.Image = bm;

                b_Refresh = true;

            }


        }

        private void BookMarkTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if ((int)BookMarkTreeView.SelectedNode.Tag != -1)
            {
                if (BookMarkTreeView.SelectedNode.Level > 1)
                {

                    TreeNode n = BookMarkTreeView.SelectedNode.Parent;
                    int l = n.Level;
                    if (l != 0)
                    {
                        int[] indexes = new int[l];
                        indexes[indexes.Length - 1] = n.Index;
                        for (int i = 1; i < l; i++)
                        {
                            indexes[indexes.Length - 1 - i] = n.Parent.Index;
                            n = n.Parent;
                        }
                        NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;
                        foreach (int i in indexes)
                        {
                            Tree = Tree.ChildTrees[i];
                        }
                        SelectedBookMark = Tree.BookMarks[BookMarkTreeView.SelectedNode.Index - Tree.ChildTrees.Count];
                        if (selectedBookMark.Parent == null)
                        {
                            selectedBookMark.Parent = Tree;
                        }

                    }
                }
                else
                {
                    SelectedBookMark = Program.BookMarkTree.BookMarks[BookMarkTreeView.SelectedNode.Index - Program.BookMarkTree.ChildTrees.Count];
                    if (selectedBookMark.Parent == null)
                    {
                        selectedBookMark.Parent = Program.BookMarkTree;
                    }
                }

            }
            else
            {
                SelectedBookMark = null;
            }
        }

        private void ToolStripAddBookMark_Click(object sender, EventArgs e)
        {
            if (BookMarkTreeView.SelectedNode != null)
            {
                    TreeNode n;
                    if ((int)BookMarkTreeView.SelectedNode.Tag == -1)
                    {
                        n = BookMarkTreeView.SelectedNode;
                    }
                    else
                    {
                        n = BookMarkTreeView.SelectedNode.Parent;
                    }

                    int l = n.Level;
                        
                 if(l!= 0)
                 {
                    int[] indexes = new int[l];

                    indexes[indexes.Length - 1] = n.Index;
                   
                    for (int i = 1; i < l; i++)
                    {
                        indexes[indexes.Length - 1 - i] = n.Parent.Index;
                        n = n.Parent;
                    }


                    NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;

                    foreach (int i in indexes)
                    {
                        Tree = Tree.ChildTrees[i];
                    }

                    Tree.AddBookMark(new NSE_Framework.IO.BookMark("New BookMark", 0, 0, 1, 1, NSE_Framework.Data.Sprite.SpriteType.Color16));

                    TreeNode newnode = new TreeNode("New BookMark");
                    newnode.Tag = 0;
                    
                    if ((int)BookMarkTreeView.SelectedNode.Tag == -1)
                    {
                         BookMarkTreeView.SelectedNode.Nodes.Add(newnode);
                    }
                    else
                    {
                        BookMarkTreeView.SelectedNode.Parent.Nodes.Add(newnode);
                    }
                    BookMarkTreeView.SelectedNode = newnode;
                    
                    
                }
                else
                {

                Program.BookMarkTree.AddBookMark(new NSE_Framework.IO.BookMark("New BookMark", 0, 0, 1, 1, NSE_Framework.Data.Sprite.SpriteType.Color16));
                TreeNode newnode = new TreeNode("New BookMark");
                newnode.Tag = 0;
                BookMarkTreeView.Nodes[0].Nodes.Add(newnode);
                BookMarkTreeView.SelectedNode = newnode;

                }
            }
        }

        private void ToolStripAddFolder_Click(object sender, EventArgs e)
        {
            if (BookMarkTreeView.SelectedNode != null)
            {
                TreeNode n;
                if ((int)BookMarkTreeView.SelectedNode.Tag == -1)
                {
                    n = BookMarkTreeView.SelectedNode;
                }
                else
                {
                    n = BookMarkTreeView.SelectedNode.Parent;
                }

                int l = n.Level;
                if (l != 0)
                {
                    int[] indexes = new int[l];
                    indexes[indexes.Length - 1] = n.Index;
                    for (int i = 1; i < l; i++)
                    {
                        indexes[indexes.Length - 1 - i] = n.Parent.Index;
                        n = n.Parent;
                    }
                    NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;
                    foreach (int i in indexes)
                    {
                        Tree = Tree.ChildTrees[i];
                    }
                    Tree.AddTree(new NSE_Framework.IO.BookMarkTree("New Folder"));

                    TreeNode newnode = new TreeNode("New Folder",1,1);
                    newnode.Tag = - 1;

                    if ((int)BookMarkTreeView.SelectedNode.Tag == -1)
                    {
                        BookMarkTreeView.SelectedNode.Nodes.Insert(Tree.ChildTrees.Count - 1, newnode);
                    }
                    else
                    {
                        BookMarkTreeView.SelectedNode.Parent.Nodes.Insert(Tree.ChildTrees.Count - 1, newnode);
                    }
                    BookMarkTreeView.SelectedNode = newnode;
                }
                else
                {
                    Program.BookMarkTree.AddTree(new NSE_Framework.IO.BookMarkTree("New Folder"));
                    TreeNode newnode = new TreeNode("New Folder", 1, 1);
                    newnode.Tag = -1;

                    BookMarkTreeView.Nodes[0].Nodes.Insert(Program.BookMarkTree.ChildTrees.Count - 1, newnode);
                    BookMarkTreeView.SelectedNode = newnode;
                }
            }
        }

        private void BookMarkTreeView_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null && e.Label != "")
            {
                if ((int)BookMarkTreeView.SelectedNode.Tag != -1)
                {
                    if (BookMarkTreeView.SelectedNode.Level > 1)
                    {

                        TreeNode n = BookMarkTreeView.SelectedNode.Parent;
                        int l = n.Level;
                        if (l != 0)
                        {
                            int[] indexes = new int[l];
                            indexes[indexes.Length - 1] = n.Index;
                            for (int i = 1; i < l; i++)
                            {
                                indexes[indexes.Length - 1 - i] = n.Parent.Index;
                                n = n.Parent;
                            }
                            NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;
                            foreach (int i in indexes)
                            {
                                Tree = Tree.ChildTrees[i];
                            }
                            Tree.BookMarks[BookMarkTreeView.SelectedNode.Index - Tree.ChildTrees.Count].Name = e.Label;

                        }
                    }
                    else
                    {
                        Program.BookMarkTree.BookMarks[BookMarkTreeView.SelectedNode.Index - Program.BookMarkTree.ChildTrees.Count].Name = e.Label;
                    }

                }
                else
                {
                    if (BookMarkTreeView.SelectedNode.Level >= 1)
                    {
                        TreeNode n = BookMarkTreeView.SelectedNode;
                        int l = n.Level;
                        if (l != 0)
                        {
                            int[] indexes = new int[l];
                            indexes[indexes.Length - 1] = n.Index;
                            for (int i = 1; i < l; i++)
                            {
                                indexes[indexes.Length - 1 - i] = n.Parent.Index;
                                n = n.Parent;
                            }
                            NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;
                            foreach (int i in indexes)
                            {
                                Tree = Tree.ChildTrees[i];
                            }
                            Tree.Name = e.Label;
                        }
                    }
                    else
                    {
                        Program.BookMarkTree.Name = e.Label;
                    }
                }
            }
            else
            {
                e.CancelEdit = true;
            }
        }

        private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Tabs.SelectedIndex == 0)
            {
                ToolStripBookMark.Enabled = false;

                ButtonOpen.Enabled = true;

                BookMarkTreeView.LabelEdit = false;
            }
            else
            {
                if (BookMarkTreeView.SelectedNode != null)
                {
                    BookMarkTreeView_AfterSelect(null, null);
                }

                ToolStripBookMark.Enabled = true;

                ButtonOpen.Enabled = false;

                BookMarkTreeView.LabelEdit = true;
            }
        }

        private void ToolStripDelete_Click(object sender, EventArgs e)
        {
            if (BookMarkTreeView.SelectedNode != null)
            {
                if (BookMarkTreeView.SelectedNode.Level != 0)
                {
                    if ((int)BookMarkTreeView.SelectedNode.Tag != -1)
                    {
                        if (MessageBox.Show(this, "Are you sure you want to delete the bookmark '" + BookMarkTreeView.SelectedNode.Text + "' ?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (BookMarkTreeView.SelectedNode.Level > 1)
                            {

                                TreeNode n = BookMarkTreeView.SelectedNode.Parent;
                                int l = n.Level;
                                if (l != 0)
                                {
                                    int[] indexes = new int[l];
                                    indexes[indexes.Length - 1] = n.Index;
                                    for (int i = 1; i < l; i++)
                                    {
                                        indexes[indexes.Length - 1 - i] = n.Parent.Index;
                                        n = n.Parent;
                                    }
                                    NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;
                                    foreach (int i in indexes)
                                    {
                                        Tree = Tree.ChildTrees[i];
                                    }
                                    Tree.BookMarks.RemoveAt(BookMarkTreeView.SelectedNode.Index - Tree.ChildTrees.Count);
                                    BookMarkTreeView.SelectedNode.Remove();

                                }
                            }
                            else
                            {
                                Program.BookMarkTree.BookMarks.RemoveAt(BookMarkTreeView.SelectedNode.Index - Program.BookMarkTree.ChildTrees.Count);
                                BookMarkTreeView.SelectedNode.Remove();
                            }
                        }

                    }
                    else
                    {
                        if (MessageBox.Show(this, "Are you sure you want to delete the folder '" + BookMarkTreeView.SelectedNode.Text + "' ?\n\n\tDeleting this folder will erase all of its child content.", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
                        {
                            if (BookMarkTreeView.SelectedNode.Level > 1)
                            {
                                TreeNode n = BookMarkTreeView.SelectedNode;
                                int l = n.Level;
                                if (l != 0)
                                {
                                    int[] indexes = new int[l];
                                    indexes[indexes.Length - 1] = n.Index;
                                    for (int i = 1; i < l; i++)
                                    {
                                        indexes[indexes.Length - 1 - i] = n.Parent.Index;
                                        n = n.Parent;
                                    }
                                    NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;
                                    for (int i = 0; i < indexes.Length - 1; i++)
                                    {
                                        Tree = Tree.ChildTrees[indexes[i]];
                                    }
                                    Tree.ChildTrees.RemoveAt(BookMarkTreeView.SelectedNode.Index);
                                    BookMarkTreeView.SelectedNode.Remove();
                                }
                            }
                            else
                            {
                                Program.BookMarkTree.ChildTrees.RemoveAt(BookMarkTreeView.SelectedNode.Index);
                                BookMarkTreeView.SelectedNode.Remove();
                            }
                        }
                    }
                }
            }
        }

        private void Navigate_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(Program.MainForm.Filename != null)
            {
            NSE_Framework.IO.Export.ExportBookMarkTree(Application.StartupPath + "\\Core\\BookMarks\\" + BookMarkTreeView.Nodes[0].Text + ".nbmx", Program.BookMarkTree);
            Program.MainForm.BookMarkFile = BookMarkTreeView.Nodes[0].Text;
            }
        }

        private void ToolStripAddCurrent_Click(object sender, EventArgs e)
        {
            if (Program.MainForm.currentEditor != null)
            {
                if (Program.MainForm.currentEditor.CurrentSprite != null)
                {                  
                    if (BookMarkTreeView.SelectedNode != null)
                    {
                        TreeNode n;
                        if ((int)BookMarkTreeView.SelectedNode.Tag == -1)
                        {
                            n = BookMarkTreeView.SelectedNode;
                        }
                        else
                        {
                            n = BookMarkTreeView.SelectedNode.Parent;
                        }

                        int l = n.Level;

                        if (l != 0)
                        {
                            int[] indexes = new int[l];

                            indexes[indexes.Length - 1] = n.Index;

                            for (int i = 1; i < l; i++)
                            {
                                indexes[indexes.Length - 1 - i] = n.Parent.Index;
                                n = n.Parent;
                            }


                            NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;

                            foreach (int i in indexes)
                            {
                                Tree = Tree.ChildTrees[i];
                            }

                            int ci = Program.MainForm.currentEditor.CurrentIndex;

                            NSE_Framework.IO.BookMark bm = new NSE_Framework.IO.BookMark("New BookMark",
                                Program.MainForm.currentEditor.Sprites[ci].ImageOffset,
                                Program.MainForm.currentEditor.Sprites[ci].PaletteOffset,
                                Program.MainForm.currentEditor.Sprites[ci].Width,
                                Program.MainForm.currentEditor.Sprites[ci].Height,
                                Program.MainForm.currentEditor.Sprites[ci].Type);

                            if (Program.MainForm.currentEditor.Sprites[ci].CompressedSprite > 0)
                            {
                                bm.Lz77 = true;
                            }

                            Tree.AddBookMark(bm);

                            TreeNode newnode = new TreeNode("New BookMark");
                            newnode.Tag = 0;

                            if ((int)BookMarkTreeView.SelectedNode.Tag == -1)
                            {
                                BookMarkTreeView.SelectedNode.Nodes.Add(newnode);
                            }
                            else
                            {
                                BookMarkTreeView.SelectedNode.Parent.Nodes.Add(newnode);
                            }
                            BookMarkTreeView.SelectedNode = newnode;


                        }
                        else
                        {
                            int ci = Program.MainForm.currentEditor.CurrentIndex;

                            NSE_Framework.IO.BookMark bm = new NSE_Framework.IO.BookMark("New BookMark",
                                Program.MainForm.currentEditor.Sprites[ci].ImageOffset,
                                Program.MainForm.currentEditor.Sprites[ci].PaletteOffset,
                                Program.MainForm.currentEditor.Sprites[ci].Width,
                                Program.MainForm.currentEditor.Sprites[ci].Height,
                                Program.MainForm.currentEditor.Sprites[ci].Type);

                            if (Program.MainForm.currentEditor.Sprites[ci].CompressedSprite > 0)
                            {
                                bm.Lz77 = true;
                            }


                            Program.BookMarkTree.AddBookMark(bm);
                            TreeNode newnode = new TreeNode("New BookMark");
                            newnode.Tag = 0;
                            BookMarkTreeView.Nodes[0].Nodes.Add(newnode);
                            BookMarkTreeView.SelectedNode = newnode;

                        }
                    }
                }
            }
        }

        private void BookMarkTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (Tabs.SelectedIndex != 0)
            {

                TreeNode node = e.Item as TreeNode;

                if ((int)node.Tag != -1)
                {
                    if (node.Level > 1)
                    {

                        TreeNode n = node.Parent;
                        int l = n.Level;
                        if (l != 0)
                        {
                            int[] indexes = new int[l];
                            indexes[indexes.Length - 1] = n.Index;
                            for (int i = 1; i < l; i++)
                            {
                                indexes[indexes.Length - 1 - i] = n.Parent.Index;
                                n = n.Parent;
                            }
                            NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;
                            foreach (int i in indexes)
                            {
                                Tree = Tree.ChildTrees[i];
                            }
                            DoDragDrop(Tree.BookMarks[node.Index - Tree.ChildTrees.Count], DragDropEffects.Move);

                        }
                    }
                    else
                    {
                        DoDragDrop(Program.BookMarkTree.BookMarks[node.Index - Program.BookMarkTree.ChildTrees.Count], DragDropEffects.Move);
                    }
                }
                else
                {
                    if (node.Level != 0)
                    {
                        TreeNode n = node;
                        int l = n.Level;
                        if (l != 0)
                        {
                            int[] indexes = new int[l];
                            indexes[indexes.Length - 1] = n.Index;
                            for (int i = 1; i < l; i++)
                            {
                                indexes[indexes.Length - 1 - i] = n.Parent.Index;
                                n = n.Parent;
                            }
                            NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;
                            foreach (int i in indexes)
                            {
                                Tree = Tree.ChildTrees[i];
                            }
                            DoDragDrop(Tree, DragDropEffects.Move);

                        }
                    }
                }

            }
            //DoDragDrop(e.Item, DragDropEffects.Move);
        }
        
        private void BookMarkTreeView_DragEnter(object sender, DragEventArgs e)
        {
            if (Tabs.SelectedIndex != 0)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void BookMarkTreeView_DragDrop(object sender, DragEventArgs e)
        {
            
            
            if (e.Data.GetDataPresent(typeof(NSE_Framework.IO.BookMark)))
            {
                #region BookMark
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode DestinationNode = ((TreeView)sender).GetNodeAt(pt);
                if (DestinationNode.Text != BookMarkTreeView.SelectedNode.Text)
                {



                    NSE_Framework.IO.BookMark obookMark = (NSE_Framework.IO.BookMark)e.Data.GetData(typeof(NSE_Framework.IO.BookMark));// (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");


                    if ((int)DestinationNode.Tag == -1)
                    {
                        #region b2f


                        NSE_Framework.IO.BookMarkTree vTree = null;

                        if (DestinationNode.Level > 1)
                        {

                            TreeNode n = DestinationNode;
                            int l = n.Level;
                            if (l != 0)
                            {
                                int[] indexes = new int[l];
                                indexes[indexes.Length - 1] = n.Index;
                                for (int i = 1; i < l; i++)
                                {
                                    indexes[indexes.Length - 1 - i] = n.Parent.Index;
                                    n = n.Parent;
                                }
                                NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;
                                foreach (int i in indexes)
                                {
                                    Tree = Tree.ChildTrees[i];
                                }
                                vTree = Tree;

                            }
                        }
                        else
                        {
                            vTree = Program.BookMarkTree;
                        }

                        vTree.AddBookMark(new NSE_Framework.IO.BookMark(obookMark.Name,
                                obookMark.ImageOffset,
                                obookMark.PaletteOffset,
                                obookMark.Width,
                                obookMark.Height,
                                obookMark.SpriteType,
                                obookMark.Lz77));


                        TreeNode newnode = new TreeNode(obookMark.Name);
                        newnode.Tag = 0;

                        DestinationNode.Nodes.Add(newnode);


                        TreeNode oldNode = BookMarkTreeView.SelectedNode;
                        BookMarkTreeView.SelectedNode = newnode;

                        if (obookMark.Parent != null)
                        {
                            obookMark.Parent.BookMarks.Remove(obookMark);
                            oldNode.Remove();
                        }

                        #endregion
                    }
                    else
                    {
                        #region b2b

                        NSE_Framework.IO.BookMark vbookMark = null;

                        if (DestinationNode.Level > 1)
                        {

                            TreeNode n = DestinationNode.Parent;
                            int l = n.Level;
                            if (l != 0)
                            {
                                int[] indexes = new int[l];
                                indexes[indexes.Length - 1] = n.Index;
                                for (int i = 1; i < l; i++)
                                {
                                    indexes[indexes.Length - 1 - i] = n.Parent.Index;
                                    n = n.Parent;
                                }
                                NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;
                                foreach (int i in indexes)
                                {
                                    Tree = Tree.ChildTrees[i];
                                }
                                vbookMark = Tree.BookMarks[DestinationNode.Index - Tree.ChildTrees.Count];

                            }
                        }
                        else
                        {
                            vbookMark = Program.BookMarkTree.BookMarks[DestinationNode.Index - Program.BookMarkTree.ChildTrees.Count];
                        }

                        int vindex = DestinationNode.Index - vbookMark.Parent.ChildTrees.Count;

                        vbookMark.Parent.BookMarks.Insert(vindex, new NSE_Framework.IO.BookMark(obookMark.Name,
                                obookMark.ImageOffset,
                                obookMark.PaletteOffset,
                                obookMark.Width,
                                obookMark.Height,
                                obookMark.SpriteType,
                                obookMark.Lz77));

                        TreeNode newnode = new TreeNode(obookMark.Name);
                        newnode.Tag = 0;

                        DestinationNode.Parent.Nodes.Insert(DestinationNode.Index, newnode);


                        TreeNode oldNode = BookMarkTreeView.SelectedNode;
                        BookMarkTreeView.SelectedNode = newnode;

                        if (obookMark.Parent != null)
                        {
                            obookMark.Parent.BookMarks.Remove(obookMark);
                            oldNode.Remove();
                        }






                        #endregion
                    }
                }
                #endregion
            }
            else if (e.Data.GetDataPresent(typeof(NSE_Framework.IO.BookMarkTree)))
            {
                #region Folder
                Point pt = ((TreeView)sender).PointToClient(new Point(e.X, e.Y));
                TreeNode DestinationNode = ((TreeView)sender).GetNodeAt(pt);

                if (DestinationNode.Text != BookMarkTreeView.SelectedNode.Text &
                    !DestinationNode.FullPath.Contains(BookMarkTreeView.SelectedNode.FullPath))
                {

                    NSE_Framework.IO.BookMarkTree oTree = (NSE_Framework.IO.BookMarkTree)e.Data.GetData(typeof(NSE_Framework.IO.BookMarkTree));// (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");


                    if ((int)DestinationNode.Tag == -1)
                    {
                        #region f2f

                        NSE_Framework.IO.BookMarkTree vTree = null;

                        if (DestinationNode.Level != 0)
                        {

                            TreeNode n = DestinationNode;
                            int l = n.Level;
                            if (l != 0)
                            {
                                int[] indexes = new int[l];
                                indexes[indexes.Length - 1] = n.Index;
                                for (int i = 1; i < l; i++)
                                {
                                    indexes[indexes.Length - 1 - i] = n.Parent.Index;
                                    n = n.Parent;
                                }
                                NSE_Framework.IO.BookMarkTree Tree = Program.BookMarkTree;
                                foreach (int i in indexes)
                                {
                                    Tree = Tree.ChildTrees[i];
                                }
                                vTree = Tree;

                            }
                        }
                        else
                        {
                            vTree = Program.BookMarkTree;
                        }

                        int vindex = vTree.Parent.ChildTrees.Count;


                        TreeNode oldNode = BookMarkTreeView.SelectedNode;
                        TreeNode newnode = new TreeNode(oTree.Name, 1, 1);
                        newnode.Tag = -1;
                        CreateTree(ref oTree, ref newnode);

                        //newnode.Tag = - 1;

                        DestinationNode.Nodes.Insert(vTree.ChildTrees.Count, newnode);




                        bool d = false;
                        if (oTree.Parent != null)
                        {
                            oTree.Parent.ChildTrees.Remove(oTree);
                            d = true;

                        }

                        vTree.ChildTrees.Insert(vTree.ChildTrees.Count, oTree);

                        if (d)
                        {
                            BookMarkTreeView.SelectedNode = oldNode;
                            oldNode.Remove();
                        }


                        #endregion
                    }
                }
                #endregion
            }
        }



    }
}
