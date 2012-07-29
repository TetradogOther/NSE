using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Plugins;

namespace NSE2
{
    public partial class MainForm : Form, IPluginHost
    {

        public string BookMarkFile;

        private bool BS;
        public bool BookMarkScripting
        {
            get { return BS; }
            set { BS = value; MenuItemEnableBS.Checked = value; }
        }

        private bool AR;
        public bool AdvancedRepointing
        {
            get { return AR; }
            set { AR = value; MenuItemAdvancedRePt.Checked = value; }
        }

        private bool SR;
        public bool SafetyRepointing
        {
            get { return SR; }
            set 
            {
                if (SR != value)
                {
                    if (value == false)
                    {

                        if (MessageBox.Show(this, "WARNING:\n\nDisabling SafetyRepointing may cause freezing in game, while attempting to load stored Lz compressed data.", "Notice:", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                        {
                            SR = value;
                            MenuItemSafetyRepointing.Checked = value;
                        }
                    }
                    else
                    {                        
                        SR = value;
                        MenuItemSafetyRepointing.Checked = true;
                        MessageBox.Show(this, "SafetyRepointing has been enabled.", "Notice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }



        public NSE_Framework.Read Read;
        public NSE_Framework.Write Write;
        public NSE_Framework.Find Find;
        public Plugins.IPlugin[] ipi1_0;

        public LogWriter LogWriter;

        public SelectColorForm SelectColor;

        bool CtrlDown = false;

        public NSE_Framework.Controls.Editor cEditor;

        public NSE_Framework.Controls.Editor currentEditor
        {
            get { return cEditor; }

            set 
            { 
                cEditor = value;

                if (value != null)
                {
                    if (value.CurrentIndex != -1)
                    {
                        Program.Navigate.ToolStripAddCurrent.Visible = true;
                    }
                }
                else
                {
                    Program.Navigate.ToolStripAddCurrent.Visible = false;
                }
            }
        }

        public Splash Splash;
        
        public MainForm()
        {            
            InitializeComponent();            
            Splash = new Splash();
            Program.Navigate = new Navigate();
            this.MouseWheel += new MouseEventHandler(Editor_MouseWheel);
            LogWriter = new LogWriter(null);
            
        }

        public void LoadRomClick(object sender, EventArgs e)
        {

            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open ROM file...";
            open.Filter = "GBA Games|*.gba;*.agb;*.bin*";
            if (open.ShowDialog(this) == DialogResult.OK)
            {
                LoadRom(open.FileName);
            }

            


        }

        public void LoadRom(string openFileName)
        {
            if (System.IO.File.Exists(openFileName) == true && !string.IsNullOrEmpty(openFileName))
            {
                this.filename = openFileName;
                this.Read = new NSE_Framework.Read(openFileName);
                this.Write = new NSE_Framework.Write(openFileName);
                this.Find = new NSE_Framework.Find(openFileName);

                if (this.SelectColor != null)
                {
                    if (SelectColor.Visible == true)
                    {
                        this.SelectColor.Close();
                    }
                }

                Splash.CreateGroup.Enabled = true;
                Splash.EditGroup.Enabled = true;
                Splash.ModifyGroup.Enabled = true;
                MenuItemPEdit.Enabled = true;
                MenuItemPModify.Enabled = true;

                if (Program.INI.IniReadValue("NSE", "LogFiles").Trim().ToLower() != "false")
                {
                    LogWriter = new LogWriter(Path.GetFileNameWithoutExtension(Program.MainForm.Filename));
                    LogWriter.LogMessage("Opened with NSE 2.X.");
                }
                else
                {
                    LogWriter = new LogWriter(null);
                }
            }

        }

        public void LoadImage(string ImageFileName)
        {
            if (System.IO.File.Exists(ImageFileName) == true && !string.IsNullOrEmpty(ImageFileName))
            {
                NSE_Framework.Data.Sprite imageSprite = NSE_Framework.IO.Import.ImportImage(ImageFileName);

                EditorForm edit = new EditorForm(imageSprite, ImageFileName);

                Program.MainForm.Focus();

                edit.MdiParent = Program.MainForm;
                edit.Show();

                LogWriter.LogMessage("Opened Image " + ImageFileName);
                
            }
        }

        public void LoadSprite(string SpriteFileName)
        {
            if (System.IO.File.Exists(SpriteFileName) == true && !string.IsNullOrEmpty(SpriteFileName))
            {
                NSE_Framework.IO.SpriteLibrary spriteLibrary = NSE_Framework.IO.Import.ImportSpriteLibrary(SpriteFileName);

                EditorForm edit = new EditorForm(spriteLibrary, SpriteFileName);

                Program.MainForm.Focus();

                edit.MdiParent = Program.MainForm;
                edit.Show();

                LogWriter.LogMessage("Opened Sprite " + SpriteFileName);

                if (Program.MainForm.Splash.Visible == true)
                {
                    Program.MainForm.Splash.Visible = false;
                }

            }
        }

        public void newSprite()
        {
            NewSprite nsForm = new NewSprite();
            if (nsForm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                NSE_Framework.IO.SpriteLibrary sLibrary = new NSE_Framework.IO.SpriteLibrary(nsForm.sName);
                sLibrary.Sprites.Add(new NSE_Framework.IO.SpriteSet(nsForm.sName, nsForm.sPalette, nsForm.sWidth, nsForm.sHeight));
                if(nsForm.sPalette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color16)
                {
                sLibrary.Sprites[0].SpriteData.Add(new NSE_Framework.IO.SpriteData(nsForm.sName, new byte[nsForm.sWidth * nsForm.sHeight * 32]));
                }
                else if(nsForm.sPalette.Type == NSE_Framework.Data.SpritePalette.PaletteType.Color256)
                {
                    sLibrary.Sprites[0].SpriteData.Add(new NSE_Framework.IO.SpriteData(nsForm.sName, new byte[nsForm.sWidth * nsForm.sHeight * 64]));         
                }
                EditorForm edit = new EditorForm(sLibrary);

                edit.MdiParent = this;
                edit.Show();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //StrokePanel not visible
            StrokePanel.Location = new Point(-150, 0);
            // Set Zoom In/Out shortcut text:
            MenuItemZoomIn.Text = "Zoom In\tCtrl++";
            MenuItemZoomOut.Text = "Zoom Out\tCtrl+-";
            
            //Show the splash screen
            Splash.MdiParent = this;
            if(this.MdiChildren.Length == 1)
            {
            Splash.Show();
            CenterSplash();
            }
            //Plugins
            SearchForPlugins();
            //BrushStroke Preview
            StrokePreview.Image = new Bitmap(32, 32);
            Graphics p = Graphics.FromImage(StrokePreview.Image);
            p.FillRectangle(Brushes.Black, 15, 15, 3, 3);

            

            this.BookMarkFile = Program.INI.IniReadValue("NSE", "BookMarkFile");
            if (Program.INI.IniReadValue("NSE", "BookMarkScripting").ToLower() == "false")
            {
                this.BookMarkScripting = false;
            }
            else
            {
                this.BookMarkScripting = true;
            }

            if (Program.INI.IniReadValue("NSE", "AdvancedRepointing").ToLower() == "false")
            {
                this.AdvancedRepointing = false;
            }
            else
            {
                this.AdvancedRepointing = true;
            }

            if (Program.INI.IniReadValue("NSE", "SafetyRepointing").ToLower() == "false")
            {
                this.SR = false;
            }
            else
            {
                this.SR = true;
            }

            MenuItemEnableBS.Checked = this.BookMarkScripting;
            MenuItemAdvancedRePt.Checked = this.AdvancedRepointing;
            MenuItemSafetyRepointing.Checked = this.SafetyRepointing;
            
            if (this.BookMarkFile == "")
            {
                this.BookMarkFile = "BookMarks";
            }

            if(File.Exists(Application.StartupPath + "\\Core\\BookMarks\\" + this.BookMarkFile + ".nbmx") == true)
            {
                Program.BookMarkTree = NSE_Framework.IO.Import.ImportBookMarkTree(Application.StartupPath + "\\Core\\BookMarks\\" + this.BookMarkFile + ".nbmx");
            }

            

        }
 
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.INI.IniWriteValue("NSE", "BookMarkFile", this.BookMarkFile);
            Program.INI.IniWriteValue("NSE", "BookMarkScripting", this.BookMarkScripting.ToString());
            Program.INI.IniWriteValue("NSE", "AdvancedRepointing", this.AdvancedRepointing.ToString());
            Program.INI.IniWriteValue("NSE", "SafetyRepointing", this.SafetyRepointing.ToString());
        }

        void CenterSplash()
        {
            if (Splash != null)
            {
                Splash.Location = new Point(this.Width / 2 - Splash.Width / 2 - 10, this.Height / 2 - Splash.Height / 2 - 50);
            }
        }

        void RefreshTools()
        {
            if (currentEditor != null)
            {
                #region Enable
                MenuItemBackcolor.Enabled = true;
                MenuItemBackcolor.Checked = currentEditor.SpriteBackColor;


                StrokePreview.Image = new Bitmap(32, 32);
                Graphics p = Graphics.FromImage(StrokePreview.Image);
                int d = (currentEditor.BrushStroke + 1) * 2 + 1;
                p.FillRectangle(Brushes.Black, 16 - d / 2, 16 - d / 2, d, d);

                if (currentEditor.Mode == NSE_Framework.Controls.Editor.EditMode.Pencil)
                {
                    ToolStripPencil.Checked = true;
                    ToolStripBrush.Checked = false;
                    ToolStripFill.Checked = false;
                    ToolStripEyedropper.Checked = false;
                    ToolStripSelection.Checked = false;
                }
                else if (currentEditor.Mode == NSE_Framework.Controls.Editor.EditMode.Brush)
                {
                    ToolStripPencil.Checked = false;
                    ToolStripBrush.Checked = true;
                    ToolStripFill.Checked = false;
                    ToolStripEyedropper.Checked = false;
                    ToolStripSelection.Checked = false;
                }
                else if (currentEditor.Mode == NSE_Framework.Controls.Editor.EditMode.Bucket)
                {
                    ToolStripPencil.Checked = false;
                    ToolStripBrush.Checked = false;
                    ToolStripFill.Checked = true;
                    ToolStripEyedropper.Checked = false;
                    ToolStripSelection.Checked = false;
                }
                else if (currentEditor.Mode == NSE_Framework.Controls.Editor.EditMode.Eyedropper)
                {
                    ToolStripPencil.Checked = false;
                    ToolStripBrush.Checked = false;
                    ToolStripFill.Checked = false;
                    ToolStripEyedropper.Checked = true;
                    ToolStripSelection.Checked = false;
                }
                else if (currentEditor.Mode == NSE_Framework.Controls.Editor.EditMode.Selection)
                {
                    ToolStripPencil.Checked = false;
                    ToolStripBrush.Checked = false;
                    ToolStripFill.Checked = false;
                    ToolStripEyedropper.Checked = false;
                    ToolStripSelection.Checked = true;
                }

                //StrokePanel.Visible = false;
                StrokePanel.Location = new Point(-150, 0);
                


                if (SelectColor == null)
                {
                    SelectColor = new SelectColorForm();
                    SelectColor.Dock = DockStyle.Right;
                }
                if (SelectColor.Visible == false)
                {
                    if (this.SelectColor.IsDisposed == true)
                    {
                        this.SelectColor = new SelectColorForm();
                    }
                }
                else
                {

                }
                SelectColor.LoadEditor(ref cEditor);

                #endregion
            }
            else
            {
                ToolStripPencil.Checked = false;
                ToolStripBrush.Checked = false;
                ToolStripFill.Checked = false;
                ToolStripEyedropper.Checked = false;
                ToolStripSelection.Checked = false;

                StrokePanel.Location = new Point(-150, 0);
                this.ToolStrip.Enabled = false;
                if (SelectColor != null)
                {
                    if (SelectColor.Visible == true)
                    {
                        this.SelectColor.Close();
                    }
                }
            }

        }

        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ToolStrip.Enabled = true;
                
                if (this.ActiveMdiChild.GetType() == typeof(EditorForm))
                {
                    currentEditor = ((EditorForm)this.ActiveMdiChild).Editor;
                    RefreshTools();
                }
                else if (this.currentEditor != null)
                {
                    if (this.currentEditor.ParentForm != this.ActiveMdiChild)
                    {
                        currentEditor = null;
                        ToolStripPencil.Checked = false;
                        ToolStripBrush.Checked = false;
                        ToolStripFill.Checked = false;
                        ToolStripEyedropper.Checked = false;
                        ToolStripSelection.Checked = false;
                        //StrokePanel.Visible = false;
                        StrokePanel.Location = new Point(-150, 0);

                        if (this.ActiveMdiChild.GetType() == typeof(Splash))
                        {
                            ((NSE2.Splash)ActiveMdiChild).WindowState = FormWindowState.Normal;
                            CenterSplash();
                        }
                        MenuItemBackcolor.Enabled = false;
                        this.ToolStrip.Enabled = false;
                        if (SelectColor != null)
                        {
                            if (SelectColor.Visible == true)
                            {
                                this.SelectColor.Close();
                            }
                        }
                    }
                }
                else
                {
                    currentEditor = null;
                    ToolStripPencil.Checked = false;
                    ToolStripBrush.Checked = false;
                    ToolStripFill.Checked = false;
                    ToolStripEyedropper.Checked = false;
                    ToolStripSelection.Checked = false;
                    //StrokePanel.Visible = false;
                    StrokePanel.Location = new Point(-150, 0);

                    if (this.ActiveMdiChild.GetType() == typeof(Splash))
                    {
                        ((NSE2.Splash)ActiveMdiChild).WindowState = FormWindowState.Normal;
                        CenterSplash();
                    }
                    MenuItemBackcolor.Enabled = false;
                    this.ToolStrip.Enabled = false;
                    if (SelectColor != null)
                    {
                        if (SelectColor.Visible == true)
                        {
                            this.SelectColor.Close();
                        }
                    }
                }
            }
            else
            {
                currentEditor = null;
                ToolStripPencil.Checked = false;
                ToolStripBrush.Checked = false;
                ToolStripFill.Checked = false;
                ToolStripEyedropper.Checked = false;
                ToolStripSelection.Checked = false;
                //StrokePanel.Visible = false;
                StrokePanel.Location = new Point(-150, 0);
                this.ToolStrip.Enabled = false;
                if (SelectColor != null)
                {
                    if (SelectColor.Visible == true)
                    {
                        this.SelectColor.Close();
                    }
                }
            }
            
        }

        private void MenuItemBackcolor_Click(object sender, EventArgs e)
        {
            if (currentEditor != null)
            {
                if (MenuItemBackcolor.Checked == true)
                {
                    MenuItemBackcolor.Checked = false;
                    currentEditor.SpriteBackColor = false;
                }
                else if (MenuItemBackcolor.Checked == false)
                {
                    MenuItemBackcolor.Checked = true;
                    currentEditor.SpriteBackColor = true;
                }
            }
        }

        private void MenuItemZoomIn_Click(object sender, EventArgs e)
        {
            if(currentEditor != null)
            {
                currentEditor.Zoom++;
            }
        }
        private void MenuItemZoomOut_Click(object sender, EventArgs e)
        {
            if (currentEditor != null)
            {
                currentEditor.Zoom--;
            }
        }
        //Overide for short cuts
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) 
        { 

            if (keyData == (Keys.Oemplus | Keys.Control))
            {
                if (currentEditor != null)
                {
                    currentEditor.Zoom++;
                }
                return true; 
            }
            else if (keyData == (Keys.OemMinus | Keys.Control))
            {
                if (currentEditor != null)
                {
                    currentEditor.Zoom--;
                }
                return true; 
            }
            else if (keyData == (Keys.OemOpenBrackets))
            {
                if (currentEditor != null)
                {
                    StrokeButtonMinus_Click(null, null);
                }
                return true;
            }
            else if (keyData == (Keys.OemCloseBrackets))
            {
                if (currentEditor != null)
                {
                    StrokeButtonPlus_Click(null, null);
                }
                return true;
            }
            //else if(keyData == (Keys.Tab))
            //{
            //    ToolStripPalette_Click(null, null);
            //    return true;
            //}
            //else if (keyData == (Keys.P))
            //{
            //    ToolStripPencil_Click(null, null);
            //    return true;
            //}
            //else if (keyData == (Keys.B ))
            //{
            //    ToolStripBrush_Click(null, null);
            //    return true;
            //}
            //else if (keyData == (Keys.F ))
            //{
            //    ToolStripFill_Click(null, null);
            //    return true;
            //}
            //else if (keyData == (Keys.E ))
            //{
            //    ToolStripEyedropper_Click(null, null);
            //    return true;
            //}

            return base.ProcessCmdKey(ref msg, keyData); 
        }

        private void Editor_MouseWheel(object sender, MouseEventArgs e)
        {

            if (this.currentEditor != null && CtrlDown==true)
            {
                int d = Math.Sign(e.Delta);
                if (d > 0)
                {
                    this.currentEditor.Zoom++;
                }
                else if (d < 0)
                {
                    this.currentEditor.Zoom--;
                }
            }


        }

#region Plugin

        void SearchForPlugins()
        {
            string path = Application.StartupPath;
            string[] pluginFiles = Directory.GetFiles(path, "*.dll");
            ipi1_0 = new IPlugin[pluginFiles.Length];

            for (int i = 0; i < pluginFiles.Length; i++)
            {
                string args = pluginFiles[i].Substring(
                    pluginFiles[i].LastIndexOf("\\") + 1,
                    pluginFiles[i].IndexOf(".dll") -
                    pluginFiles[i].LastIndexOf("\\") - 1);

                Type ObjType = null;
                try
                {
                    // load it
                    Assembly ass = null;
                    ass = Assembly.Load(args);
                    if (ass != null)
                    {
                        ObjType = ass.GetType(args + ".Plugin");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                try
                {
                    // OK Lets create the object as we have the Report Type
                    if (ObjType != null)
                    {
                        
                        ipi1_0[i] = (IPlugin)Activator.CreateInstance(ObjType);
                        ipi1_0[i].Host = this;

                        ipi1_0[i].Initialize();
                        
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        // Plugin Accessible

        string filename;

        public string Filename
        {
            get { return filename; }
        }

        int y_create = 29;
        int y_edit = 29;
        int y_modify = 29;

        public bool Register(IPlugin ipi)
        {
            MenuItem mn = new MenuItem(ipi.Name, new EventHandler(NewLoad));

            Console.WriteLine("Registered: " + ipi.Name);
            //MainMenu.MenuItems.Add(mn);

            if (ipi.Classification == PluginClassification.Create
                || ipi.Classification == PluginClassification.EditSprite
                || ipi.Classification == PluginClassification.ModifyData)
            {
                Button b = new Button();
                b.Text = ipi.Name;
                b.TextAlign = ContentAlignment.MiddleLeft;
                b.TabStop = false;
                b.Click += new EventHandler(NewLoad);
                b.Size = new Size(125, 23);

                if (ipi.Classification == PluginClassification.Create)
                {
                    MenuItemPCreate.MenuItems.Add(mn);

                    b.Location = new Point(2, y_create);
                    y_create += 29;
                    Splash.Scroll_Create.Controls.Add(b);
                }
                else if (ipi.Classification == PluginClassification.EditSprite)
                {
                    MenuItemPEdit.MenuItems.Add(mn);
                    b.Location = new Point(2, y_edit);
                    y_edit += 29;
                    Splash.Scroll_Edit.Controls.Add(b);
                }
                else if (ipi.Classification == PluginClassification.ModifyData)
                {
                    MenuItemPModify.MenuItems.Add(mn);
                    b.Location = new Point(2, y_modify);
                    y_modify += 29;
                    Splash.Scroll_Modify.Controls.Add(b);
                }
            }
            else 
            {
                if (ipi.Classification == PluginClassification.GhostCreate)
                {
                    MenuItemPCreate.MenuItems.Add(mn);
                }
                else if (ipi.Classification == PluginClassification.GhostEditSprite)
                {
                    MenuItemPEdit.MenuItems.Add(mn);
                }
                else if (ipi.Classification == PluginClassification.GhostModifyData)
                {
                    MenuItemPModify.MenuItems.Add(mn);
                }
            }
            
           
            return true;
        }

        public void SetEditor(ref NSE_Framework.Controls.Editor Editor)
        {
            if (Editor != null)
            {
                if (Editor.CurrentSprite != null)
                {
                    currentEditor = Editor;
                    this.ToolStrip.Enabled = true;
                    MenuItemBackcolor.Enabled = true;
                    MenuItemBackcolor.Checked = currentEditor.SpriteBackColor;


                    StrokePreview.Image = new Bitmap(32, 32);
                    Graphics p = Graphics.FromImage(StrokePreview.Image);
                    int d = (currentEditor.BrushStroke + 1) * 2 + 1;
                    p.FillRectangle(Brushes.Black, 16 - d / 2, 16 - d / 2, d, d);

                    RefreshTools();
                }
            }
        }

        private void NewLoad(object sender, System.EventArgs e)
        {
            string strType = "null";
            if (sender.GetType() == typeof (MenuItem))
            {
                strType = ((MenuItem)sender).Text;
            }
            else if (sender.GetType() == typeof(Button))
            {
                strType = ((Button)sender).Text;
            }



            for (int i = 0; i < ipi1_0.Length; i++)
            {
                if (ipi1_0[i] != null)
                {
                    if (ipi1_0[i].Name == strType)
                    {
                        
                        ipi1_0[i].Load();
                        if (ipi1_0[i].Type == PluginType.MdiChildForm)
                        {
                            if (Splash.Visible)
                            {
                                Splash.Hide();
                            }
                            ipi1_0[i].Mainform.MdiParent = this;
                            ipi1_0[i].Mainform.Show();
                        }
                        else if(ipi1_0[i].Type == PluginType.Form)
                        {
                            ipi1_0[i].Mainform.Show();
                        }
                        else if (ipi1_0[i].Type == PluginType.DialogForm)
                        {
                            ipi1_0[i].Mainform.ShowDialog(this);
                        }

                        
                        break;
                    }
                }
            }
        }

        public void NewImport(string Name, EventHandler importEvent)
        {
            MenuItem mn = new MenuItem(Name, importEvent);
            MenuItemImport.MenuItems.Add(mn);

            if (MenuItemImportBar.Visible == false)
            {
                MenuItemImportBar.Visible = true;
            }
        }
        public void NewExport(string Name, EventHandler exportEvent)
        {
            MenuItem mn = new MenuItem(Name, exportEvent);
            MenuItemExport.MenuItems.Add(mn);

            if (MenuItemExportBar.Visible == false)
            {
                MenuItemExportBar.Visible = true;
            }
        }

        public void NewEditorForm(NSE_Framework.IO.SpriteLibrary SpriteLibrary, string FileName)
        {
            EditorForm e = new EditorForm(SpriteLibrary, FileName);
            e.MdiParent = this;
            e.Show();
        }
        public void NewEditorForm(NSE_Framework.IO.SpriteLibrary SpriteLibrary)
        {
            EditorForm e = new EditorForm(SpriteLibrary);
            e.MdiParent = this;
            e.Show();
        }
        public void NewEditorForm(NSE_Framework.Data.Sprite Sprite)
        {
            EditorForm e = new EditorForm(Sprite);
            e.MdiParent = this;
            e.Show();
        }
        public void NewEditorForm(NSE_Framework.Data.Sprite Sprite, string FileName)
        {
            EditorForm e = new EditorForm(Sprite, FileName);
            e.MdiParent = this;
            e.Show();
        }


#endregion

        private void ToolStripPalette_Click(object sender, EventArgs e)
        {
            if (SelectColor == null)
            {
                SelectColor = new SelectColorForm();
            }
            if (SelectColor.Visible == false)
            {
                if (this.SelectColor.IsDisposed == true)
                {
                    this.SelectColor = new SelectColorForm();
                }

                this.SelectColor.LoadEditor(ref cEditor);
               this.SelectColor.Show(this);
            }
            else
            {

            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {

                CenterSplash();

        }

        private void MenuItemStartPage_Click(object sender, EventArgs e)
        {
            //CenterSplash();
            Splash.CloseCheck.Checked = false;
            Splash.Visible = true;
            
        }

        private void MenuItemNavigate_Click(object sender, EventArgs e)
        {
            if (Filename != null)
            {
                if (Program.Navigate.IsDisposed == true)
                {
                    Program.Navigate = new Navigate();
                }

                if (Program.Navigate.Visible == false)
                {
                    Program.Navigate.Show(Program.MainForm);
                }
            }
            
        }

        private void ToolStripSelection_Click(object sender, EventArgs e)
        {
            if (currentEditor != null)
            {


                currentEditor.Mode = NSE_Framework.Controls.Editor.EditMode.Selection;
                ToolStripSelection.Checked = true;
                ToolStripPencil.Checked = false;
                ToolStripBrush.Checked = false;
                ToolStripFill.Checked = false;
                ToolStripEyedropper.Checked = false;

            }
        }   

        private void ToolStripPencil_Click(object sender, EventArgs e)
        {
            if (currentEditor != null)
            {


                currentEditor.Mode = NSE_Framework.Controls.Editor.EditMode.Pencil;
                ToolStripPencil.Checked = true;
                ToolStripSelection.Checked = false;
                ToolStripBrush.Checked = false;
                ToolStripFill.Checked = false;
                ToolStripEyedropper.Checked = false;

            }

        }
        
        private void ToolStripBrush_Click(object sender, EventArgs e)
        {
            if (currentEditor != null)
            {
                //if (this.ActiveMdiChild != null)
                //{
                //    this.ActiveMdiChild.Focus();
                //}
                

                if (ToolStripBrush.Checked == false)
                {
                    currentEditor.Mode = NSE_Framework.Controls.Editor.EditMode.Brush;
                    ToolStripBrush.Checked = true;
                    ToolStripSelection.Checked = false;
                    ToolStripPencil.Checked = false;
                    ToolStripFill.Checked = false;
                    ToolStripEyedropper.Checked = false;
                }
                else
                {

                    if (StrokePanel.Location.X == -150)
                    {
                        StrokePanel.Location = new Point(100, 26);
                    }
                    else if (StrokePanel.Location.X == 100)
                    {
                        StrokePanel.Location = new Point(-150, 0);
                    }
                }
            }
        }

        private void ToolStripFill_Click(object sender, EventArgs e)
        {
            //StrokePanel.Visible = false;
            StrokePanel.Location = new Point(-150, 0);
            if (currentEditor != null)
            {
                if (this.ActiveMdiChild != null)
                {
                    this.ActiveMdiChild.Focus();
                }

                currentEditor.Mode = NSE_Framework.Controls.Editor.EditMode.Bucket;
                ToolStripFill.Checked = true;
                ToolStripSelection.Checked = false;
                ToolStripBrush.Checked = false;
                ToolStripPencil.Checked = false;
                ToolStripEyedropper.Checked = false;

            }




        }

        private void ToolStripEyedropper_Click(object sender, EventArgs e)
        {
            if (currentEditor != null)
            {
                if (this.ActiveMdiChild != null)
                {
                    this.ActiveMdiChild.Focus();
                }

                ToolStripSelection.Checked = false;
                ToolStripFill.Checked = false;
                ToolStripBrush.Checked = false;
                ToolStripPencil.Checked = false;
                ToolStripEyedropper.Checked = true;

                currentEditor.PreviousMode = currentEditor.Mode;
                currentEditor.Mode = NSE_Framework.Controls.Editor.EditMode.Eyedropper;
            }

            
        }

        private void Save(object sender, EventArgs e)
        {

            if (currentEditor != null)
            {

                if (currentEditor.CurrentSprite.CompressedSprite == -1)
                {
                    //Regular Sprite
                    #region RegularSprite
                    if (currentEditor.CurrentSprite.UniqueImage == true && !string.IsNullOrEmpty(filename))
                    {
                        //Check to make sure somebody didn't mess up while making a plug-in
                        if (NSE_Framework.Data.Lz77.CheckLz77(this.Read, currentEditor.CurrentSprite.ImageOffset, NSE_Framework.Data.CheckLz77Type.Sprite) == -1)
                        {
                            #region InternalSprite
                            Write.WriteBytes(
                                currentEditor.CurrentSprite.ImageData,
                                currentEditor.CurrentSprite.ImageOffset);

                            Program.MainForm.LogWriter.LogMessage("Saved Data 0x" + currentEditor.CurrentSprite.ImageOffset.ToString("X"));
                            #endregion
                        }
                        else
                        {
                            //Somebody has failed pretty badly, time to tell the user
                            MessageBox.Show(this, "So... I have good news and bad news: \n\n-The BAD news is that you were about to screw-up your game pretty badly (Puking crap data all-over your shiny ROM).\n-The GOOD news is that I just saved you from hair-pulling agony.\n\nI recommend you EXPORT any unsaved data to a sprite library (including this sprite), and REPORT the problem.\nIf a plugin caused this, now would be a good time to close it (after you export any unsaved data) and stop using it  until a fix is found.\n\nYou're welcome :)", "Trust-Fall", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else if (currentEditor.CurrentSprite.UniqueImage == false || string.IsNullOrEmpty(filename))
                    {
                        // note: These are ONLY sprites opened through an "EDITOR FORM"
                        // editor forms are opened through "OPEN SPRITE" or through a plugin using "NewEditorForm"
                        #region ExternalSprite
                        if (this.ActiveMdiChild.GetType() == typeof(EditorForm))
                        {
                            string sfileName = (this.ActiveMdiChild as EditorForm).FileName;

                            if (sfileName == null)
                            {
                                #region newSprite
                                SaveFileDialog saveDialog = new SaveFileDialog();
                                saveDialog.Filter = "Sprite Library 2.X|*.nslx;";
                                saveDialog.FileName = (this.ActiveMdiChild as EditorForm).SpriteLibrary.Origin;

                                if (saveDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                {
                                    sfileName = saveDialog.FileName;
                                    (this.ActiveMdiChild as EditorForm).FileName = saveDialog.FileName;
                                    (this.ActiveMdiChild as EditorForm).Text = (this.ActiveMdiChild as EditorForm).SpriteLibrary.Origin + " - " + saveDialog.FileName;
                                }
                                #endregion
                            }
                            else if (sfileName != null)
                            {
                                #region saveSprite
                                if ((this.ActiveMdiChild as EditorForm).SpriteLibrary == null)
                                {
                                    #region ImageFile
                                    NSE_Framework.IO.Export.ExportBitmap(Path.ChangeExtension(sfileName, ".bmp"), currentEditor.CurrentSprite);
                                    if (Path.GetExtension(sfileName).ToLower() == ".png")
                                    {

                                        (this.ActiveMdiChild as EditorForm).FileName = Path.ChangeExtension(sfileName, ".bmp");
                                        (this.ActiveMdiChild as EditorForm).RefreshTitle();
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region NSL
                                    (this.ActiveMdiChild as EditorForm).SpriteLibrary.Sprites[(this.ActiveMdiChild as EditorForm).index].SpriteData[(this.ActiveMdiChild as EditorForm).frame].Data = (this.ActiveMdiChild as EditorForm).Editor.CurrentSprite.ImageData;
                                    (this.ActiveMdiChild as EditorForm).SpriteLibrary.Sprites[(this.ActiveMdiChild as EditorForm).index].SpriteData[(this.ActiveMdiChild as EditorForm).frame].Compressed = false;
                                    NSE_Framework.IO.Export.ExportSpriteLibrary((this.ActiveMdiChild as EditorForm).FileName, (this.ActiveMdiChild as EditorForm).SpriteLibrary);
                                    #endregion
                                }

                                #endregion
                            }

                            return;

                        }
                        #endregion

                        // if it's a new sprite, that's been created by a plugin or aliens
                        // if we end up here, it's time to be worried, something fishy is probably going on.
                        // That's why, here, we TRY to CATCH any rouge fish that may pass through here.
                        #region Assume
                        try
                        {
                            if (!string.IsNullOrEmpty(filename))
                            {
                                
                                // We are left to assume that the sprite is to be inserted into the ROM
                                InsertForm iForm = new InsertForm(this.Write, currentEditor.CurrentSprite.ImageData);
                                iForm.ShowDialog(this);

                                if (iForm.SaveOffset != -1)
                                {
                                    currentEditor.CurrentSprite.ImageOffset = iForm.SaveOffset;
                                    currentEditor.CurrentSprite.CompressedSprite = -1;
                                    if (this.ActiveMdiChild.GetType() == typeof(EditorForm))
                                    {
                                        ((EditorForm)this.ActiveMdiChild).RefreshTitle();
                                    }
                                }
                                
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(this, "Hmmm... It appears that a problem has occured;\n It's probably those stupid aliens again. You've got to watch out nowadays. You never know when an alien is just going to fly out of nowhere, and then BANG, you're getting a clever error message like this, when all you want to do is get some work done.\n\nAnyways here's the problem: " + ex.InnerException, "Sarcasm Alert!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        #endregion
                    }
                    #endregion
                }
                else if (currentEditor.CurrentSprite.CompressedSprite != -1 && !string.IsNullOrEmpty(filename))
                {
                    //Compressed Sprite
                    //These will only be saved to the ROM, no external saves
                    #region CompressedSprite

                    // First we compress the sprite data
                    byte[] data = NSE_Framework.Data.Lz77.CompressBytes(currentEditor.CurrentSprite.ImageData);

                    //Next we check if this image was opened from the ROM
                    if (currentEditor.CurrentSprite.UniqueImage == true)
                    {
                        #region InternalImage
                        //Build the saveform
                        SaveForm saveForm = new SaveForm(this.Write, data,
                        currentEditor.CurrentSprite.ImageOffset,
                        currentEditor.CurrentSprite.CompressedSprite);

                        saveForm.ShowDialog(this);

                        if (saveForm.SaveOffset != -1)
                        {
                            int oo = currentEditor.CurrentSprite.ImageOffset;
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

                                        MessageBox.Show(this, "The BookMark(s): " + sNames + "have been automagically adjusted :)\nTo the see changes, re-open the Navigate window.", "Notice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }


                            currentEditor.CurrentSprite.ImageOffset = saveForm.SaveOffset;
                            currentEditor.CurrentSprite.CompressedSprite = data.Length;
                            if (this.ActiveMdiChild.GetType() == typeof(EditorForm))
                            {
                                ((EditorForm)this.ActiveMdiChild).RefreshTitle();
                            }

                        }
                        #endregion
                    }
                    else if (currentEditor.CurrentSprite.UniqueImage == false)
                    {
                        #region ExternalImage
                        InsertForm iForm = new InsertForm(this.Write, data);
                        iForm.ShowDialog(this);

                        if (iForm.SaveOffset != -1)
                        {
                            currentEditor.CurrentSprite.ImageOffset = iForm.SaveOffset;

                        }

                        #endregion
                    }
                    

                    #endregion
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
            for (int b = 0; b < l; b++ )
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

        private void MenuItemAbout_Click(object sender, EventArgs e)
        {            
            MessageBox.Show(this, "Nameless Sprite Editor: Created by Link12552, 2010 - 2012\n\nA tool used to edit sprites and other data in Gameboy Advance games.\n\nEditor Version: 2.0.0.0\nNSE Framework Version: 1.0.0.6\nPlugins Framework Version: 1.0.0.0\nLz77 Compression Version 2.0.0.0\nBookMarks Version 1.0.0.0\n*.NSLX Version 1.0.0.0", "About NSE - " + Program.Version, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void MenuItemImport_Click(object sender, EventArgs e)
        {
            Import import = new Import();
            import.Show(this);
        }

        private void MainForm_Key(object sender, KeyEventArgs e)
        {
            if (e.Control == true)
            {
                CtrlDown = true;
            }
            else
            {
                CtrlDown = false;
            }
        }

        private void MenuItemExportSprite_Click(object sender, EventArgs e)
        {
            if (currentEditor != null)
            {
                if (this.ActiveMdiChild.GetType() == typeof(EditorForm))
                {
                    if ((this.ActiveMdiChild as EditorForm).SpriteLibrary != null)
                    {
                        SaveFileDialog saveDialog = new SaveFileDialog();
                        saveDialog.Filter = "Sprite Library 2.X|*.nslx;";

                        if (saveDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                        {
                            (this.ActiveMdiChild as EditorForm).SpriteLibrary.Sprites[(this.ActiveMdiChild as EditorForm).index].SpriteData[(this.ActiveMdiChild as EditorForm).frame].Data = (this.ActiveMdiChild as EditorForm).Editor.CurrentSprite.ImageData;
                            (this.ActiveMdiChild as EditorForm).SpriteLibrary.Sprites[(this.ActiveMdiChild as EditorForm).index].SpriteData[(this.ActiveMdiChild as EditorForm).frame].Compressed = false;
                            NSE_Framework.IO.Export.ExportSpriteLibrary(saveDialog.FileName, (this.ActiveMdiChild as EditorForm).SpriteLibrary);

                            (this.ActiveMdiChild as EditorForm).FileName = saveDialog.FileName;
                            (this.ActiveMdiChild as EditorForm).Text = (this.ActiveMdiChild as EditorForm).SpriteLibrary.Origin + " - " + saveDialog.FileName;
                        }

                        return;
                    }
                }

                if (currentEditor.Sprites.Count > 0)
                {
                    NSE_Framework.IO.SpriteLibrary Library;
                    if (!string.IsNullOrEmpty(filename))
                    {
                        Library = new NSE_Framework.IO.SpriteLibrary(Read.ReadString(0xA0, 18));
                    }
                    else
                    {
                        Library = new NSE_Framework.IO.SpriteLibrary("NSE 2.X");
                    }
                    

                    Library.Sprites.Add(new NSE_Framework.IO.SpriteSet("Sprite 0", currentEditor.Sprites[0].Palette, currentEditor.Sprites[0].Width, currentEditor.Sprites[0].Height));

                    string frameName = "Frame 0";

                    if (currentEditor.Sprites[0].UniqueImage == true)
                    {
                        frameName += " - " + currentEditor.CurrentSprite.ImageOffset.ToString("X2");
                    }

                    if (currentEditor.CurrentSprite.ImageData.Length <= 0x40000)
                    {
                        Library.Sprites[0].SpriteData.Add(new NSE_Framework.IO.SpriteData(frameName, NSE_Framework.Data.Lz77.CompressBytes(currentEditor.Sprites[0].ImageData), true));
                    }
                    else
                    {
                        Library.Sprites[0].SpriteData.Add(new NSE_Framework.IO.SpriteData(frameName, currentEditor.Sprites[0].ImageData, false));
                    }
                    
                    int ss = 0;
                    int fn = 1;

                    for (int i = 1; i < currentEditor.Sprites.Count; i++ )
                    {
                        if (currentEditor.Sprites[i].Height != Library.Sprites[ss].Height ||
                            currentEditor.Sprites[i].Width != Library.Sprites[ss].Width ||
                            ComparePalettes(Library.Sprites[ss].Palette, currentEditor.Sprites[i].Palette) == false)
                        {
                            fn = 0;
                            ss++;

                            Library.Sprites.Add(new NSE_Framework.IO.SpriteSet("Sprite " + ss.ToString(), currentEditor.Sprites[i].Palette, currentEditor.Sprites[i].Width, currentEditor.Sprites[i].Height));
                        }

                        frameName = "Frame " + fn.ToString();

                        if (currentEditor.Sprites[i].UniqueImage == true)
                        {
                            frameName += " - " + currentEditor.Sprites[i].ImageOffset.ToString("X2");
                        }

                            
                            if (currentEditor.Sprites[i].ImageData.Length <= 0x40000)
                            {
                                Library.Sprites[ss].SpriteData.Add(new NSE_Framework.IO.SpriteData(frameName, NSE_Framework.Data.Lz77.CompressBytes(currentEditor.Sprites[i].ImageData), true));
                            }
                            else
                            {
                                Library.Sprites[ss].SpriteData.Add(new NSE_Framework.IO.SpriteData(frameName, currentEditor.Sprites[i].ImageData, false));
                            }
                            fn++;
                       
                    }


                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Sprite Library 2.X|*.nslx;";

                    if (saveDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        NSE_Framework.IO.Export.ExportSpriteLibrary(saveDialog.FileName, Library);
                    }
                }
            }
        }

        private bool ComparePalettes(NSE_Framework.Data.SpritePalette pal1, NSE_Framework.Data.SpritePalette pal2)
        {
            if (pal1.Type == pal2.Type && pal1.Colors.Length == pal2.Colors.Length)
            {
                for (int i = 0; i < pal1.Colors.Length; i ++ )
                {
                    if (pal1.Colors[i].Color != pal2.Colors[i].Color)
                    {
                        return false;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void MenuItemExportBitmap_Click(object sender, EventArgs e)
        {
            if (currentEditor != null)
            {
                if (currentEditor.CurrentSprite != null)
                {
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Bitmap|*.bmp;";

                    if (saveDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        NSE_Framework.IO.Export.ExportBitmap(saveDialog.FileName, currentEditor.CurrentSprite);
                    }
                }
            }
            
        }

        private void MenuItemClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MenuItemChooseBM_Click(object sender, EventArgs e)
        {
            UserPreferences up = new UserPreferences();
            up.ShowDialog(this);
        }

        private void MenuItemEnableBS_Click(object sender, EventArgs e)
        {
            if (MenuItemEnableBS.Checked == true)
            {
                MenuItemEnableBS.Checked = false;
                BookMarkScripting = false;
            }
            else
            {
                MenuItemEnableBS.Checked = true;
                BookMarkScripting = true;
            }
        }

        private void MenuItemAdvancedRePt_Click(object sender, EventArgs e)
        {
            if (MenuItemAdvancedRePt.Checked == true)
            {
                MenuItemAdvancedRePt.Checked = false;
                AdvancedRepointing = false;
            }
            else
            {
                MenuItemAdvancedRePt.Checked = true;
                AdvancedRepointing = true;
            }
        }

        private void MenuItemSafetyRepointing_Click(object sender, EventArgs e)
        {
            if (MenuItemSafetyRepointing.Checked == true)
            {
                if(MessageBox.Show(this, "WARNING:\n\nDisabling SafetyRepointing may cause freezing in game, while attempting to load stored Lz compressed data.","Notice:",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.OK)
                {
                MenuItemSafetyRepointing.Checked = false;
                SR = false;
                }
            }
            else
            {
                MenuItemSafetyRepointing.Checked = true;
                SR = true;
                MessageBox.Show(this, "SafetyRepointing has been enabled.", "Notice:", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MenuItemImportP_Click(object sender, EventArgs e)
        {
            ImportPalette ip = new ImportPalette();
            ip.Show(this);
        }

        private void MenuItemUndo_Click(object sender, EventArgs e)
        {
            if (currentEditor != null)
            {
                currentEditor.Undo();
            }
        }

        private void MenuItemRedo_Click(object sender, EventArgs e)
        {
            if (currentEditor != null)
            {
                currentEditor.Redo();
            }
        }

        private void MenuItemEditPalette_Click(object sender, EventArgs e)
        {
            if (currentEditor != null)
            {

                if (SelectColor == null)
                {
                    SelectColor = new SelectColorForm();
                }
                if (SelectColor.Visible == false)
                {
                    if (this.SelectColor.IsDisposed == true)
                    {
                        this.SelectColor = new SelectColorForm();
                    }

                    this.SelectColor.LoadEditor(ref cEditor);
                    this.SelectColor.Size = new Size(289, 467);
                    this.SelectColor.ButtonCollapse.Text = "-";
                    this.SelectColor.Show(this);
                }
                else
                {
                    this.SelectColor.Size = new Size(289, 467);
                    this.SelectColor.ButtonCollapse.Text = "-";
                }
                
            }
        }

        private void MenuItemInsertImage_Click(object sender, EventArgs e)
        {
            if (currentEditor != null && !string.IsNullOrEmpty(filename))
            {
                if (currentEditor.CurrentIndex != -1)
                {
                    InsertForm insert = new InsertForm(this.Write, currentEditor.CurrentSprite.ImageData);
                    insert.ShowDialog(this);
                }
            }
        }

        private void MenuItemInsertPalette_Click(object sender, EventArgs e)
        {
            if (currentEditor != null && !string.IsNullOrEmpty(filename))
            {
                if (currentEditor.CurrentIndex != -1)
                {
                    InsertForm insert = new InsertForm(this.Write, currentEditor.CurrentSprite.Palette.GetGBABytes);
                    insert.ShowDialog(this);
                }
            }
        }

        private void MenuItemInsertCImage_Click(object sender, EventArgs e)
        {
            if (currentEditor != null && !string.IsNullOrEmpty(filename))
            {
                if (currentEditor.CurrentIndex != -1)
                {
                    InsertForm insert = new InsertForm(this.Write, NSE_Framework.Data.Lz77.CompressBytes(currentEditor.CurrentSprite.ImageData));
                    insert.ShowDialog(this);
                }
            }
        }

        private void MenuItemInsertCPalette_Click(object sender, EventArgs e)
        {
            if (currentEditor != null && !string.IsNullOrEmpty(filename))
            {
                if (currentEditor.CurrentIndex != -1)
                {
                    InsertForm insert = new InsertForm(this.Write, NSE_Framework.Data.Lz77.CompressBytes(currentEditor.CurrentSprite.Palette.GetGBABytes));
                    insert.ShowDialog(this);
                }
            }
        }

        public void OpenSpriteClick(object sender, EventArgs e)
        {
            OpenFileDialog iDialog = new OpenFileDialog();
            iDialog.Title = "Select a file to open:";
            iDialog.CheckFileExists = true;
            iDialog.Filter = "Openable Sprite|*.nslx;*.bmp;*.png*";

            if (iDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK & iDialog.FileName != "")
            {
                string exstension = System.IO.Path.GetExtension(iDialog.FileName).ToLower();

                try
                {
                    if (exstension == ".png" || exstension == ".bmp")
                    {
                        Program.MainForm.LoadImage(iDialog.FileName);
                    }
                    else if (exstension == ".nslx")
                    {
                        Program.MainForm.LoadSprite(iDialog.FileName);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            bool lr = false;

            foreach (string file in files)
            {
                string exstension = System.IO.Path.GetExtension(file).ToLower();
                if (exstension == ".gba" || exstension == ".agb" || exstension == ".bin")
                {
                    if (lr == false)
                    {
                        if (MessageBox.Show(this, "Load the ROM: " + file, "Notice:", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            LoadRom(file);
                            lr = true;
                        }
                    }
                }
                else if (exstension == ".png" || exstension == ".bmp")
                {
                    LoadImage(file);
                }
                else if (exstension == ".nslx")
                {
                    LoadSprite(file);
                }
            }  
        }

        private void MenuItemNewSprite_Click(object sender, EventArgs e)
        {
            newSprite();
        }

        private void MenuItemOnline_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://nse.codeplex.com/");
        }

        private void StrokeButtonPlus_Click(object sender, EventArgs e)
        {
            if (currentEditor.BrushStroke < 7)
            {
                currentEditor.BrushStroke++;
                StrokePreview.Image = new Bitmap(32, 32);
                Graphics p = Graphics.FromImage(StrokePreview.Image);
                int d = (currentEditor.BrushStroke + 1) * 2 + 1;
                p.FillRectangle(Brushes.Black, 16 - d / 2, 16 - d / 2, d, d);                
            }
        }

        private void StrokeButtonMinus_Click(object sender, EventArgs e)
        {
            if (currentEditor.BrushStroke > 0)
            {
                currentEditor.BrushStroke--;
                StrokePreview.Image = new Bitmap(32, 32);
                Graphics p = Graphics.FromImage(StrokePreview.Image);
                int d = (currentEditor.BrushStroke + 1) * 2 + 1;
                p.FillRectangle(Brushes.Black, 16 - d / 2, 16 - d / 2, d, d);                
            }
        }

        private void MenuItemPManager_Click(object sender, EventArgs e)
        {
            PluginProperties man = new PluginProperties();
            man.ShowDialog(this);
        }

 
    }
}
