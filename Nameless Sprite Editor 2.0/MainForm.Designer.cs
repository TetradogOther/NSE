namespace NSE2
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Main_Menu = new System.Windows.Forms.MainMenu(this.components);
            this.MenuItemFile = new System.Windows.Forms.MenuItem();
            this.MenuItemLoad = new System.Windows.Forms.MenuItem();
            this.MenuItemFileLine1 = new System.Windows.Forms.MenuItem();
            this.MenuItemOpenSprite = new System.Windows.Forms.MenuItem();
            this.MenuItemNewSprite = new System.Windows.Forms.MenuItem();
            this.MenuItemFileLine2 = new System.Windows.Forms.MenuItem();
            this.MenuItemNavigate = new System.Windows.Forms.MenuItem();
            this.MenuItemSave = new System.Windows.Forms.MenuItem();
            this.MenuItemInsert = new System.Windows.Forms.MenuItem();
            this.MenuItemInsertImage = new System.Windows.Forms.MenuItem();
            this.MenuItemInsertPalette = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.MenuItemInsertCImage = new System.Windows.Forms.MenuItem();
            this.MenuItemInsertCPalette = new System.Windows.Forms.MenuItem();
            this.MenuItemFileLine3 = new System.Windows.Forms.MenuItem();
            this.MenuItemImport = new System.Windows.Forms.MenuItem();
            this.MenuItemImportS = new System.Windows.Forms.MenuItem();
            this.MenuItemImportCT = new System.Windows.Forms.MenuItem();
            this.MenuItemImportBar = new System.Windows.Forms.MenuItem();
            this.MenuItemExport = new System.Windows.Forms.MenuItem();
            this.MenuItemExportSprite = new System.Windows.Forms.MenuItem();
            this.MenuItemExportBitmap = new System.Windows.Forms.MenuItem();
            this.MenuItemExportBar = new System.Windows.Forms.MenuItem();
            this.MenuItemFileLine4 = new System.Windows.Forms.MenuItem();
            this.MenuItemClose = new System.Windows.Forms.MenuItem();
            this.MenuItemEdit = new System.Windows.Forms.MenuItem();
            this.MenuItemUndo = new System.Windows.Forms.MenuItem();
            this.MenuItemRedo = new System.Windows.Forms.MenuItem();
            this.MenuItemEditBar = new System.Windows.Forms.MenuItem();
            this.MenuItemEditPalette = new System.Windows.Forms.MenuItem();
            this.MenuItemPlugins = new System.Windows.Forms.MenuItem();
            this.MenuItemPCreate = new System.Windows.Forms.MenuItem();
            this.MenuItemPEdit = new System.Windows.Forms.MenuItem();
            this.MenuItemPModify = new System.Windows.Forms.MenuItem();
            this.MenuItemView = new System.Windows.Forms.MenuItem();
            this.MenuItemStartPage = new System.Windows.Forms.MenuItem();
            this.MenuItemViewLine1 = new System.Windows.Forms.MenuItem();
            this.MenuItemBackcolor = new System.Windows.Forms.MenuItem();
            this.MenuItemZoom = new System.Windows.Forms.MenuItem();
            this.MenuItemZoomIn = new System.Windows.Forms.MenuItem();
            this.MenuItemZoomOut = new System.Windows.Forms.MenuItem();
            this.MenuItemOptions = new System.Windows.Forms.MenuItem();
            this.MenuItemChooseBM = new System.Windows.Forms.MenuItem();
            this.MenuItemEnableBS = new System.Windows.Forms.MenuItem();
            this.OptionsBar = new System.Windows.Forms.MenuItem();
            this.MenuItemAdvancedRePt = new System.Windows.Forms.MenuItem();
            this.MenuItemSafetyRepointing = new System.Windows.Forms.MenuItem();
            this.MenuItemOther = new System.Windows.Forms.MenuItem();
            this.MenuItemAbout = new System.Windows.Forms.MenuItem();
            this.MenuItemOnline = new System.Windows.Forms.MenuItem();
            this.ToolStrip = new System.Windows.Forms.ToolStrip();
            this.ToolStripPalette = new System.Windows.Forms.ToolStripButton();
            this.ToolStripPencil = new System.Windows.Forms.ToolStripButton();
            this.ToolStripBrush = new System.Windows.Forms.ToolStripButton();
            this.ToolStripFill = new System.Windows.Forms.ToolStripButton();
            this.ToolStripEyedropper = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSelection = new System.Windows.Forms.ToolStripButton();
            this.Tips = new System.Windows.Forms.ToolTip(this.components);
            this.StrokePanel = new System.Windows.Forms.Panel();
            this.StrokeButtonMinus = new System.Windows.Forms.Button();
            this.StrokeButtonPlus = new System.Windows.Forms.Button();
            this.StrokePreview = new System.Windows.Forms.PictureBox();
            this.StrokeLabel = new System.Windows.Forms.Label();
            this.MenuItemPProperties = new System.Windows.Forms.MenuItem();
            this.MenuItemPBar = new System.Windows.Forms.MenuItem();
            this.ToolStrip.SuspendLayout();
            this.StrokePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StrokePreview)).BeginInit();
            this.SuspendLayout();
            // 
            // Main_Menu
            // 
            this.Main_Menu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemFile,
            this.MenuItemEdit,
            this.MenuItemPlugins,
            this.MenuItemView,
            this.MenuItemOptions,
            this.MenuItemOther});
            // 
            // MenuItemFile
            // 
            this.MenuItemFile.Index = 0;
            this.MenuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemLoad,
            this.MenuItemFileLine1,
            this.MenuItemOpenSprite,
            this.MenuItemNewSprite,
            this.MenuItemFileLine2,
            this.MenuItemNavigate,
            this.MenuItemSave,
            this.MenuItemInsert,
            this.MenuItemFileLine3,
            this.MenuItemImport,
            this.MenuItemExport,
            this.MenuItemFileLine4,
            this.MenuItemClose});
            this.MenuItemFile.Text = "File";
            // 
            // MenuItemLoad
            // 
            this.MenuItemLoad.Index = 0;
            this.MenuItemLoad.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
            this.MenuItemLoad.Text = "Load Rom";
            this.MenuItemLoad.Click += new System.EventHandler(this.LoadRomClick);
            // 
            // MenuItemFileLine1
            // 
            this.MenuItemFileLine1.Index = 1;
            this.MenuItemFileLine1.Text = "-";
            // 
            // MenuItemOpenSprite
            // 
            this.MenuItemOpenSprite.Index = 2;
            this.MenuItemOpenSprite.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftO;
            this.MenuItemOpenSprite.Text = "Open Sprite";
            this.MenuItemOpenSprite.Click += new System.EventHandler(this.OpenSpriteClick);
            // 
            // MenuItemNewSprite
            // 
            this.MenuItemNewSprite.Index = 3;
            this.MenuItemNewSprite.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftN;
            this.MenuItemNewSprite.Text = "New Sprite";
            this.MenuItemNewSprite.Click += new System.EventHandler(this.MenuItemNewSprite_Click);
            // 
            // MenuItemFileLine2
            // 
            this.MenuItemFileLine2.Index = 4;
            this.MenuItemFileLine2.Text = "-";
            // 
            // MenuItemNavigate
            // 
            this.MenuItemNavigate.Index = 5;
            this.MenuItemNavigate.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
            this.MenuItemNavigate.Text = "Navigate";
            this.MenuItemNavigate.Click += new System.EventHandler(this.MenuItemNavigate_Click);
            // 
            // MenuItemSave
            // 
            this.MenuItemSave.Index = 6;
            this.MenuItemSave.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
            this.MenuItemSave.Text = "Save";
            this.MenuItemSave.Click += new System.EventHandler(this.Save);
            // 
            // MenuItemInsert
            // 
            this.MenuItemInsert.Index = 7;
            this.MenuItemInsert.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemInsertImage,
            this.MenuItemInsertPalette,
            this.menuItem1});
            this.MenuItemInsert.Text = "Insert";
            // 
            // MenuItemInsertImage
            // 
            this.MenuItemInsertImage.Index = 0;
            this.MenuItemInsertImage.Text = "Image Data";
            this.MenuItemInsertImage.Click += new System.EventHandler(this.MenuItemInsertImage_Click);
            // 
            // MenuItemInsertPalette
            // 
            this.MenuItemInsertPalette.Index = 1;
            this.MenuItemInsertPalette.Text = "Palette Data";
            this.MenuItemInsertPalette.Click += new System.EventHandler(this.MenuItemInsertPalette_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemInsertCImage,
            this.MenuItemInsertCPalette});
            this.menuItem1.Text = "Compressed";
            // 
            // MenuItemInsertCImage
            // 
            this.MenuItemInsertCImage.Index = 0;
            this.MenuItemInsertCImage.Text = "Image Data";
            this.MenuItemInsertCImage.Click += new System.EventHandler(this.MenuItemInsertCImage_Click);
            // 
            // MenuItemInsertCPalette
            // 
            this.MenuItemInsertCPalette.Index = 1;
            this.MenuItemInsertCPalette.Text = "Palette Data";
            this.MenuItemInsertCPalette.Click += new System.EventHandler(this.MenuItemInsertCPalette_Click);
            // 
            // MenuItemFileLine3
            // 
            this.MenuItemFileLine3.Index = 8;
            this.MenuItemFileLine3.Text = "-";
            // 
            // MenuItemImport
            // 
            this.MenuItemImport.Index = 9;
            this.MenuItemImport.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemImportS,
            this.MenuItemImportCT,
            this.MenuItemImportBar});
            this.MenuItemImport.Text = "Import";
            // 
            // MenuItemImportS
            // 
            this.MenuItemImportS.Index = 0;
            this.MenuItemImportS.Shortcut = System.Windows.Forms.Shortcut.CtrlI;
            this.MenuItemImportS.Text = "Import Sprite Data";
            this.MenuItemImportS.Click += new System.EventHandler(this.MenuItemImport_Click);
            // 
            // MenuItemImportCT
            // 
            this.MenuItemImportCT.Index = 1;
            this.MenuItemImportCT.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftI;
            this.MenuItemImportCT.Text = "Import Color Table";
            this.MenuItemImportCT.Click += new System.EventHandler(this.MenuItemImportP_Click);
            // 
            // MenuItemImportBar
            // 
            this.MenuItemImportBar.Index = 2;
            this.MenuItemImportBar.Text = "-";
            this.MenuItemImportBar.Visible = false;
            // 
            // MenuItemExport
            // 
            this.MenuItemExport.Index = 10;
            this.MenuItemExport.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemExportSprite,
            this.MenuItemExportBitmap,
            this.MenuItemExportBar});
            this.MenuItemExport.Text = "Export";
            // 
            // MenuItemExportSprite
            // 
            this.MenuItemExportSprite.Index = 0;
            this.MenuItemExportSprite.Shortcut = System.Windows.Forms.Shortcut.CtrlE;
            this.MenuItemExportSprite.Text = "Export Sprite Library";
            this.MenuItemExportSprite.Click += new System.EventHandler(this.MenuItemExportSprite_Click);
            // 
            // MenuItemExportBitmap
            // 
            this.MenuItemExportBitmap.Index = 1;
            this.MenuItemExportBitmap.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftE;
            this.MenuItemExportBitmap.Text = "Export Bitmap";
            this.MenuItemExportBitmap.Click += new System.EventHandler(this.MenuItemExportBitmap_Click);
            // 
            // MenuItemExportBar
            // 
            this.MenuItemExportBar.Index = 2;
            this.MenuItemExportBar.Text = "-";
            this.MenuItemExportBar.Visible = false;
            // 
            // MenuItemFileLine4
            // 
            this.MenuItemFileLine4.Index = 11;
            this.MenuItemFileLine4.Text = "-";
            // 
            // MenuItemClose
            // 
            this.MenuItemClose.Index = 12;
            this.MenuItemClose.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.MenuItemClose.Text = "Close";
            this.MenuItemClose.Click += new System.EventHandler(this.MenuItemClose_Click);
            // 
            // MenuItemEdit
            // 
            this.MenuItemEdit.Index = 1;
            this.MenuItemEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemUndo,
            this.MenuItemRedo,
            this.MenuItemEditBar,
            this.MenuItemEditPalette});
            this.MenuItemEdit.Text = "Edit";
            // 
            // MenuItemUndo
            // 
            this.MenuItemUndo.Index = 0;
            this.MenuItemUndo.Shortcut = System.Windows.Forms.Shortcut.CtrlZ;
            this.MenuItemUndo.Text = "Undo";
            this.MenuItemUndo.Click += new System.EventHandler(this.MenuItemUndo_Click);
            // 
            // MenuItemRedo
            // 
            this.MenuItemRedo.Index = 1;
            this.MenuItemRedo.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftZ;
            this.MenuItemRedo.Text = "Redo";
            this.MenuItemRedo.Click += new System.EventHandler(this.MenuItemRedo_Click);
            // 
            // MenuItemEditBar
            // 
            this.MenuItemEditBar.Index = 2;
            this.MenuItemEditBar.Text = "-";
            // 
            // MenuItemEditPalette
            // 
            this.MenuItemEditPalette.Index = 3;
            this.MenuItemEditPalette.Shortcut = System.Windows.Forms.Shortcut.CtrlP;
            this.MenuItemEditPalette.Text = "Palette";
            this.MenuItemEditPalette.Click += new System.EventHandler(this.MenuItemEditPalette_Click);
            // 
            // MenuItemPlugins
            // 
            this.MenuItemPlugins.Index = 2;
            this.MenuItemPlugins.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemPProperties,
            this.MenuItemPBar,
            this.MenuItemPCreate,
            this.MenuItemPEdit,
            this.MenuItemPModify});
            this.MenuItemPlugins.Text = "Plugins";
            // 
            // MenuItemPCreate
            // 
            this.MenuItemPCreate.Index = 2;
            this.MenuItemPCreate.Text = "Create";
            // 
            // MenuItemPEdit
            // 
            this.MenuItemPEdit.Enabled = false;
            this.MenuItemPEdit.Index = 3;
            this.MenuItemPEdit.Text = "Edit";
            // 
            // MenuItemPModify
            // 
            this.MenuItemPModify.Enabled = false;
            this.MenuItemPModify.Index = 4;
            this.MenuItemPModify.Text = "Modify";
            // 
            // MenuItemView
            // 
            this.MenuItemView.Index = 3;
            this.MenuItemView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemStartPage,
            this.MenuItemViewLine1,
            this.MenuItemBackcolor,
            this.MenuItemZoom});
            this.MenuItemView.Text = "View";
            // 
            // MenuItemStartPage
            // 
            this.MenuItemStartPage.Index = 0;
            this.MenuItemStartPage.Text = "Start Page";
            this.MenuItemStartPage.Click += new System.EventHandler(this.MenuItemStartPage_Click);
            // 
            // MenuItemViewLine1
            // 
            this.MenuItemViewLine1.Index = 1;
            this.MenuItemViewLine1.Text = "-";
            // 
            // MenuItemBackcolor
            // 
            this.MenuItemBackcolor.Checked = true;
            this.MenuItemBackcolor.Index = 2;
            this.MenuItemBackcolor.Shortcut = System.Windows.Forms.Shortcut.CtrlT;
            this.MenuItemBackcolor.Text = "Backcolor";
            this.MenuItemBackcolor.Click += new System.EventHandler(this.MenuItemBackcolor_Click);
            // 
            // MenuItemZoom
            // 
            this.MenuItemZoom.Index = 3;
            this.MenuItemZoom.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemZoomIn,
            this.MenuItemZoomOut});
            this.MenuItemZoom.Text = "Zoom";
            // 
            // MenuItemZoomIn
            // 
            this.MenuItemZoomIn.Index = 0;
            this.MenuItemZoomIn.Text = "Zoom In";
            this.MenuItemZoomIn.Click += new System.EventHandler(this.MenuItemZoomIn_Click);
            // 
            // MenuItemZoomOut
            // 
            this.MenuItemZoomOut.Index = 1;
            this.MenuItemZoomOut.Text = "Zoom Out";
            this.MenuItemZoomOut.Click += new System.EventHandler(this.MenuItemZoomOut_Click);
            // 
            // MenuItemOptions
            // 
            this.MenuItemOptions.Index = 4;
            this.MenuItemOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemChooseBM,
            this.MenuItemEnableBS,
            this.OptionsBar,
            this.MenuItemAdvancedRePt,
            this.MenuItemSafetyRepointing});
            this.MenuItemOptions.Text = "Options";
            // 
            // MenuItemChooseBM
            // 
            this.MenuItemChooseBM.Index = 0;
            this.MenuItemChooseBM.Text = "Choose BookMarks";
            this.MenuItemChooseBM.Click += new System.EventHandler(this.MenuItemChooseBM_Click);
            // 
            // MenuItemEnableBS
            // 
            this.MenuItemEnableBS.Checked = true;
            this.MenuItemEnableBS.Index = 1;
            this.MenuItemEnableBS.Text = "Enable BookMark Scripting";
            this.MenuItemEnableBS.Click += new System.EventHandler(this.MenuItemEnableBS_Click);
            // 
            // OptionsBar
            // 
            this.OptionsBar.Index = 2;
            this.OptionsBar.Text = "-";
            // 
            // MenuItemAdvancedRePt
            // 
            this.MenuItemAdvancedRePt.Checked = true;
            this.MenuItemAdvancedRePt.Index = 3;
            this.MenuItemAdvancedRePt.Text = "Advanced Repointing";
            this.MenuItemAdvancedRePt.Click += new System.EventHandler(this.MenuItemAdvancedRePt_Click);
            // 
            // MenuItemSafetyRepointing
            // 
            this.MenuItemSafetyRepointing.Checked = true;
            this.MenuItemSafetyRepointing.Index = 4;
            this.MenuItemSafetyRepointing.Text = "Safety Repointing";
            this.MenuItemSafetyRepointing.Click += new System.EventHandler(this.MenuItemSafetyRepointing_Click);
            // 
            // MenuItemOther
            // 
            this.MenuItemOther.Index = 5;
            this.MenuItemOther.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.MenuItemAbout,
            this.MenuItemOnline});
            this.MenuItemOther.Text = "Other";
            // 
            // MenuItemAbout
            // 
            this.MenuItemAbout.Index = 0;
            this.MenuItemAbout.Text = "About";
            this.MenuItemAbout.Click += new System.EventHandler(this.MenuItemAbout_Click);
            // 
            // MenuItemOnline
            // 
            this.MenuItemOnline.Index = 1;
            this.MenuItemOnline.Text = "Online";
            this.MenuItemOnline.Click += new System.EventHandler(this.MenuItemOnline_Click);
            // 
            // ToolStrip
            // 
            this.ToolStrip.Enabled = false;
            this.ToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripPalette,
            this.ToolStripPencil,
            this.ToolStripBrush,
            this.ToolStripFill,
            this.ToolStripEyedropper,
            this.ToolStripSelection});
            this.ToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.ToolStrip.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip.Name = "ToolStrip";
            this.ToolStrip.Padding = new System.Windows.Forms.Padding(10, 0, 1, 0);
            this.ToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ToolStrip.Size = new System.Drawing.Size(878, 25);
            this.ToolStrip.TabIndex = 1;
            this.ToolStrip.Text = "ToolStrip";
            this.ToolStrip.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // ToolStripPalette
            // 
            this.ToolStripPalette.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripPalette.Image = global::NSE2.Properties.Resources.palette;
            this.ToolStripPalette.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripPalette.Name = "ToolStripPalette";
            this.ToolStripPalette.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.ToolStripPalette.Size = new System.Drawing.Size(50, 22);
            this.ToolStripPalette.Text = "Palette";
            this.ToolStripPalette.ToolTipText = "Palette";
            this.ToolStripPalette.Click += new System.EventHandler(this.ToolStripPalette_Click);
            // 
            // ToolStripPencil
            // 
            this.ToolStripPencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripPencil.Image = global::NSE2.Properties.Resources.pencil;
            this.ToolStripPencil.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripPencil.Name = "ToolStripPencil";
            this.ToolStripPencil.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.ToolStripPencil.Size = new System.Drawing.Size(40, 22);
            this.ToolStripPencil.Text = "Pencil";
            this.ToolStripPencil.ToolTipText = "Pencil";
            this.ToolStripPencil.Click += new System.EventHandler(this.ToolStripPencil_Click);
            // 
            // ToolStripBrush
            // 
            this.ToolStripBrush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripBrush.Image = global::NSE2.Properties.Resources.brush;
            this.ToolStripBrush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripBrush.Name = "ToolStripBrush";
            this.ToolStripBrush.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.ToolStripBrush.Size = new System.Drawing.Size(40, 22);
            this.ToolStripBrush.Text = "Brush";
            this.ToolStripBrush.ToolTipText = "Brush";
            this.ToolStripBrush.Click += new System.EventHandler(this.ToolStripBrush_Click);
            // 
            // ToolStripFill
            // 
            this.ToolStripFill.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripFill.Image = global::NSE2.Properties.Resources.bucket;
            this.ToolStripFill.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripFill.Name = "ToolStripFill";
            this.ToolStripFill.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.ToolStripFill.Size = new System.Drawing.Size(40, 22);
            this.ToolStripFill.Text = "Fill";
            this.ToolStripFill.ToolTipText = "Fill";
            this.ToolStripFill.Click += new System.EventHandler(this.ToolStripFill_Click);
            // 
            // ToolStripEyedropper
            // 
            this.ToolStripEyedropper.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripEyedropper.Image = global::NSE2.Properties.Resources.eyedropper;
            this.ToolStripEyedropper.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripEyedropper.Name = "ToolStripEyedropper";
            this.ToolStripEyedropper.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.ToolStripEyedropper.Size = new System.Drawing.Size(40, 22);
            this.ToolStripEyedropper.Text = "Eye Dropper";
            this.ToolStripEyedropper.ToolTipText = "Eye Dropper";
            this.ToolStripEyedropper.Click += new System.EventHandler(this.ToolStripEyedropper_Click);
            // 
            // ToolStripSelection
            // 
            this.ToolStripSelection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripSelection.Image = global::NSE2.Properties.Resources.Selection;
            this.ToolStripSelection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripSelection.Name = "ToolStripSelection";
            this.ToolStripSelection.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.ToolStripSelection.Size = new System.Drawing.Size(40, 22);
            this.ToolStripSelection.Text = "Selection Tool";
            this.ToolStripSelection.ToolTipText = "Selection Tool";
            this.ToolStripSelection.Visible = false;
            this.ToolStripSelection.Click += new System.EventHandler(this.ToolStripSelection_Click);
            // 
            // StrokePanel
            // 
            this.StrokePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StrokePanel.Controls.Add(this.StrokeButtonMinus);
            this.StrokePanel.Controls.Add(this.StrokeButtonPlus);
            this.StrokePanel.Controls.Add(this.StrokePreview);
            this.StrokePanel.Controls.Add(this.StrokeLabel);
            this.StrokePanel.Location = new System.Drawing.Point(100, 26);
            this.StrokePanel.Name = "StrokePanel";
            this.StrokePanel.Size = new System.Drawing.Size(92, 70);
            this.StrokePanel.TabIndex = 4;
            // 
            // StrokeButtonMinus
            // 
            this.StrokeButtonMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StrokeButtonMinus.Location = new System.Drawing.Point(50, 36);
            this.StrokeButtonMinus.Name = "StrokeButtonMinus";
            this.StrokeButtonMinus.Size = new System.Drawing.Size(30, 22);
            this.StrokeButtonMinus.TabIndex = 4;
            this.StrokeButtonMinus.Text = "-";
            this.StrokeButtonMinus.UseVisualStyleBackColor = true;
            this.StrokeButtonMinus.Click += new System.EventHandler(this.StrokeButtonMinus_Click);
            // 
            // StrokeButtonPlus
            // 
            this.StrokeButtonPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StrokeButtonPlus.Location = new System.Drawing.Point(50, 11);
            this.StrokeButtonPlus.Name = "StrokeButtonPlus";
            this.StrokeButtonPlus.Size = new System.Drawing.Size(30, 22);
            this.StrokeButtonPlus.TabIndex = 3;
            this.StrokeButtonPlus.Text = "+";
            this.StrokeButtonPlus.UseVisualStyleBackColor = true;
            this.StrokeButtonPlus.Click += new System.EventHandler(this.StrokeButtonPlus_Click);
            // 
            // StrokePreview
            // 
            this.StrokePreview.BackgroundImage = global::NSE2.Properties.Resources.transparent;
            this.StrokePreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StrokePreview.Location = new System.Drawing.Point(6, 19);
            this.StrokePreview.Name = "StrokePreview";
            this.StrokePreview.Size = new System.Drawing.Size(34, 34);
            this.StrokePreview.TabIndex = 2;
            this.StrokePreview.TabStop = false;
            // 
            // StrokeLabel
            // 
            this.StrokeLabel.AutoSize = true;
            this.StrokeLabel.Location = new System.Drawing.Point(3, 3);
            this.StrokeLabel.Name = "StrokeLabel";
            this.StrokeLabel.Size = new System.Drawing.Size(41, 13);
            this.StrokeLabel.TabIndex = 1;
            this.StrokeLabel.Text = "Stroke-";
            // 
            // MenuItemPProperties
            // 
            this.MenuItemPProperties.Index = 0;
            this.MenuItemPProperties.Text = "Properties";
            this.MenuItemPProperties.Click += new System.EventHandler(this.MenuItemPManager_Click);
            // 
            // MenuItemPBar
            // 
            this.MenuItemPBar.Index = 1;
            this.MenuItemPBar.Text = "-";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(878, 418);
            this.Controls.Add(this.StrokePanel);
            this.Controls.Add(this.ToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Menu = this.Main_Menu;
            this.Name = "MainForm";
            this.Text = "Nameless Sprite Editor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MdiChildActivate += new System.EventHandler(this.MainForm_MdiChildActivate);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_Key);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_Key);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ToolStrip.ResumeLayout(false);
            this.ToolStrip.PerformLayout();
            this.StrokePanel.ResumeLayout(false);
            this.StrokePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StrokePreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MainMenu Main_Menu;
        private System.Windows.Forms.MenuItem MenuItemFile;
        private System.Windows.Forms.MenuItem MenuItemLoad;
        private System.Windows.Forms.MenuItem MenuItemSave;
        private System.Windows.Forms.MenuItem MenuItemInsert;
        private System.Windows.Forms.MenuItem MenuItemFileLine2;
        private System.Windows.Forms.MenuItem MenuItemImport;
        private System.Windows.Forms.MenuItem MenuItemExport;
        private System.Windows.Forms.MenuItem MenuItemFileLine3;
        private System.Windows.Forms.MenuItem MenuItemClose;
        private System.Windows.Forms.MenuItem MenuItemEdit;
        private System.Windows.Forms.MenuItem MenuItemView;
        private System.Windows.Forms.MenuItem MenuItemBackcolor;
        private System.Windows.Forms.MenuItem MenuItemZoom;
        private System.Windows.Forms.MenuItem MenuItemZoomIn;
        private System.Windows.Forms.MenuItem MenuItemZoomOut;
        private System.Windows.Forms.ToolStrip ToolStrip;
        private System.Windows.Forms.ToolStripButton ToolStripPencil;
        private System.Windows.Forms.ToolStripButton ToolStripFill;
        private System.Windows.Forms.ToolStripButton ToolStripEyedropper;
        private System.Windows.Forms.ToolStripButton ToolStripPalette;
        private System.Windows.Forms.MenuItem MenuItemStartPage;
        private System.Windows.Forms.MenuItem MenuItemViewLine1;
        private System.Windows.Forms.MenuItem MenuItemNavigate;
        private System.Windows.Forms.MenuItem MenuItemFileLine4;
        private System.Windows.Forms.MenuItem MenuItemOther;
        private System.Windows.Forms.MenuItem MenuItemAbout;
        private System.Windows.Forms.MenuItem MenuItemExportSprite;
        private System.Windows.Forms.MenuItem MenuItemExportBitmap;
        private System.Windows.Forms.ToolTip Tips;
        private System.Windows.Forms.MenuItem MenuItemOptions;
        private System.Windows.Forms.MenuItem MenuItemChooseBM;
        private System.Windows.Forms.MenuItem MenuItemEnableBS;
        private System.Windows.Forms.MenuItem OptionsBar;
        private System.Windows.Forms.MenuItem MenuItemAdvancedRePt;
        private System.Windows.Forms.MenuItem MenuItemSafetyRepointing;
        private System.Windows.Forms.MenuItem MenuItemInsertCImage;
        private System.Windows.Forms.MenuItem MenuItemInsertCPalette;
        private System.Windows.Forms.MenuItem MenuItemImportS;
        private System.Windows.Forms.MenuItem MenuItemImportCT;
        private System.Windows.Forms.MenuItem MenuItemUndo;
        private System.Windows.Forms.MenuItem MenuItemRedo;
        private System.Windows.Forms.MenuItem MenuItemEditBar;
        private System.Windows.Forms.MenuItem MenuItemEditPalette;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem MenuItemInsertImage;
        private System.Windows.Forms.MenuItem MenuItemInsertPalette;
        private System.Windows.Forms.Panel StrokePanel;
        private System.Windows.Forms.PictureBox StrokePreview;
        private System.Windows.Forms.Label StrokeLabel;
        private System.Windows.Forms.MenuItem MenuItemOpenSprite;
        private System.Windows.Forms.MenuItem MenuItemFileLine1;
        private System.Windows.Forms.MenuItem MenuItemNewSprite;
        private System.Windows.Forms.MenuItem MenuItemOnline;
        private System.Windows.Forms.MenuItem MenuItemPlugins;
        private System.Windows.Forms.MenuItem MenuItemPCreate;
        private System.Windows.Forms.MenuItem MenuItemPEdit;
        private System.Windows.Forms.MenuItem MenuItemPModify;
        private System.Windows.Forms.MenuItem MenuItemImportBar;
        private System.Windows.Forms.MenuItem MenuItemExportBar;
        private System.Windows.Forms.ToolStripButton ToolStripBrush;
        private System.Windows.Forms.Button StrokeButtonPlus;
        private System.Windows.Forms.Button StrokeButtonMinus;
        private System.Windows.Forms.ToolStripButton ToolStripSelection;
        private System.Windows.Forms.MenuItem MenuItemPProperties;
        private System.Windows.Forms.MenuItem MenuItemPBar;


    }
}

