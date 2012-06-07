namespace NSE2
{
    partial class Navigate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Navigate));
            this.BookMarkTreeView = new System.Windows.Forms.TreeView();
            this.TreeImageList = new System.Windows.Forms.ImageList(this.components);
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ButtonOpen = new System.Windows.Forms.Button();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.TabNavigate = new System.Windows.Forms.TabPage();
            this.GroupDrawMode = new System.Windows.Forms.GroupBox();
            this.NavigateLabelInfo = new System.Windows.Forms.Label();
            this.NavigateLz77 = new System.Windows.Forms.CheckBox();
            this.ComboBoxNavigateColors = new System.Windows.Forms.ComboBox();
            this.NavigateGray = new System.Windows.Forms.CheckBox();
            this.GroupPreview = new System.Windows.Forms.GroupBox();
            this.NavigatePreview = new System.Windows.Forms.PictureBox();
            this.GroupOffsets = new System.Windows.Forms.GroupBox();
            this.TextboxNavigatePalette = new System.Windows.Forms.TextBox();
            this.LabelNavigateOffsetPalette = new System.Windows.Forms.Label();
            this.TextboxNavigateImage = new System.Windows.Forms.TextBox();
            this.LabelNavigateOffsetImage = new System.Windows.Forms.Label();
            this.GroupSize = new System.Windows.Forms.GroupBox();
            this.LabelNavigateHeight = new System.Windows.Forms.Label();
            this.LabelNavigateWidth = new System.Windows.Forms.Label();
            this.ComboBoxNavigateHeight = new System.Windows.Forms.ComboBox();
            this.ComboBoxNavigateWidth = new System.Windows.Forms.ComboBox();
            this.TabEdit = new System.Windows.Forms.TabPage();
            this.GroupEditPreview = new System.Windows.Forms.GroupBox();
            this.EditPreview = new System.Windows.Forms.PictureBox();
            this.GroupEditMode = new System.Windows.Forms.GroupBox();
            this.EditLabelInfo = new System.Windows.Forms.Label();
            this.EditLz77 = new System.Windows.Forms.CheckBox();
            this.ComboBoxEditColors = new System.Windows.Forms.ComboBox();
            this.EditGray = new System.Windows.Forms.CheckBox();
            this.GroupEditOffsets = new System.Windows.Forms.GroupBox();
            this.TextBoxEditPalette = new System.Windows.Forms.TextBox();
            this.EditLabelPalette = new System.Windows.Forms.Label();
            this.TextBoxEditImage = new System.Windows.Forms.TextBox();
            this.EditLabelImage = new System.Windows.Forms.Label();
            this.GroupEditSize = new System.Windows.Forms.GroupBox();
            this.EditLabelHeight = new System.Windows.Forms.Label();
            this.EditLabelWidth = new System.Windows.Forms.Label();
            this.ComboBoxEditHeight = new System.Windows.Forms.ComboBox();
            this.ComboBoxEditWidth = new System.Windows.Forms.ComboBox();
            this.PanelBookMark = new System.Windows.Forms.Panel();
            this.ToolStripBookMark = new System.Windows.Forms.ToolStrip();
            this.ToolStripAddBookMark = new System.Windows.Forms.ToolStripButton();
            this.ToolStripAddFolder = new System.Windows.Forms.ToolStripButton();
            this.ToolStripDelete = new System.Windows.Forms.ToolStripButton();
            this.ToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripAddCurrent = new System.Windows.Forms.ToolStripButton();
            this.Tabs.SuspendLayout();
            this.TabNavigate.SuspendLayout();
            this.GroupDrawMode.SuspendLayout();
            this.GroupPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NavigatePreview)).BeginInit();
            this.GroupOffsets.SuspendLayout();
            this.GroupSize.SuspendLayout();
            this.TabEdit.SuspendLayout();
            this.GroupEditPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EditPreview)).BeginInit();
            this.GroupEditMode.SuspendLayout();
            this.GroupEditOffsets.SuspendLayout();
            this.GroupEditSize.SuspendLayout();
            this.PanelBookMark.SuspendLayout();
            this.ToolStripBookMark.SuspendLayout();
            this.SuspendLayout();
            // 
            // BookMarkTreeView
            // 
            this.BookMarkTreeView.AllowDrop = true;
            this.BookMarkTreeView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BookMarkTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BookMarkTreeView.HideSelection = false;
            this.BookMarkTreeView.ImageIndex = 0;
            this.BookMarkTreeView.ImageList = this.TreeImageList;
            this.BookMarkTreeView.Location = new System.Drawing.Point(0, 25);
            this.BookMarkTreeView.Name = "BookMarkTreeView";
            this.BookMarkTreeView.SelectedImageIndex = 0;
            this.BookMarkTreeView.Size = new System.Drawing.Size(250, 225);
            this.BookMarkTreeView.TabIndex = 41;
            this.BookMarkTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.BookMarkTreeView_AfterLabelEdit);
            this.BookMarkTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.BookMarkTreeView_ItemDrag);
            this.BookMarkTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.BookMarkTreeView_AfterSelect);
            this.BookMarkTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.BookMarkTreeView_DragDrop);
            this.BookMarkTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.BookMarkTreeView_DragEnter);
            // 
            // TreeImageList
            // 
            this.TreeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeImageList.ImageStream")));
            this.TreeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.TreeImageList.Images.SetKeyName(0, "Star.png");
            this.TreeImageList.Images.SetKeyName(1, "Folder.png");
            // 
            // ButtonClose
            // 
            this.ButtonClose.Location = new System.Drawing.Point(490, 280);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(85, 23);
            this.ButtonClose.TabIndex = 43;
            this.ButtonClose.Text = "Close";
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // ButtonOpen
            // 
            this.ButtonOpen.Location = new System.Drawing.Point(375, 280);
            this.ButtonOpen.Name = "ButtonOpen";
            this.ButtonOpen.Size = new System.Drawing.Size(109, 23);
            this.ButtonOpen.TabIndex = 42;
            this.ButtonOpen.Text = "Open";
            this.ButtonOpen.UseVisualStyleBackColor = true;
            this.ButtonOpen.Click += new System.EventHandler(this.Open_Click);
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.TabNavigate);
            this.Tabs.Controls.Add(this.TabEdit);
            this.Tabs.Location = new System.Drawing.Point(7, 7);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(312, 296);
            this.Tabs.TabIndex = 44;
            this.Tabs.SelectedIndexChanged += new System.EventHandler(this.Tabs_SelectedIndexChanged);
            // 
            // TabNavigate
            // 
            this.TabNavigate.BackColor = System.Drawing.Color.White;
            this.TabNavigate.Controls.Add(this.GroupDrawMode);
            this.TabNavigate.Controls.Add(this.GroupPreview);
            this.TabNavigate.Controls.Add(this.GroupOffsets);
            this.TabNavigate.Controls.Add(this.GroupSize);
            this.TabNavigate.Location = new System.Drawing.Point(4, 22);
            this.TabNavigate.Name = "TabNavigate";
            this.TabNavigate.Padding = new System.Windows.Forms.Padding(3);
            this.TabNavigate.Size = new System.Drawing.Size(304, 270);
            this.TabNavigate.TabIndex = 0;
            this.TabNavigate.Text = "Navigate";
            // 
            // GroupDrawMode
            // 
            this.GroupDrawMode.Controls.Add(this.NavigateLabelInfo);
            this.GroupDrawMode.Controls.Add(this.NavigateLz77);
            this.GroupDrawMode.Controls.Add(this.ComboBoxNavigateColors);
            this.GroupDrawMode.Controls.Add(this.NavigateGray);
            this.GroupDrawMode.Location = new System.Drawing.Point(177, 79);
            this.GroupDrawMode.Name = "GroupDrawMode";
            this.GroupDrawMode.Size = new System.Drawing.Size(120, 129);
            this.GroupDrawMode.TabIndex = 9;
            this.GroupDrawMode.TabStop = false;
            this.GroupDrawMode.Text = "Draw Mode";
            // 
            // NavigateLabelInfo
            // 
            this.NavigateLabelInfo.AutoSize = true;
            this.NavigateLabelInfo.Location = new System.Drawing.Point(7, 94);
            this.NavigateLabelInfo.Name = "NavigateLabelInfo";
            this.NavigateLabelInfo.Size = new System.Drawing.Size(106, 26);
            this.NavigateLabelInfo.TabIndex = 10;
            this.NavigateLabelInfo.Text = "* Enabled if available\r\nfor this sprite.";
            // 
            // NavigateLz77
            // 
            this.NavigateLz77.AutoSize = true;
            this.NavigateLz77.Checked = true;
            this.NavigateLz77.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NavigateLz77.Enabled = false;
            this.NavigateLz77.Location = new System.Drawing.Point(18, 42);
            this.NavigateLz77.Name = "NavigateLz77";
            this.NavigateLz77.Size = new System.Drawing.Size(56, 17);
            this.NavigateLz77.TabIndex = 9;
            this.NavigateLz77.Text = "Lz77 *";
            this.NavigateLz77.UseVisualStyleBackColor = true;
            this.NavigateLz77.CheckedChanged += new System.EventHandler(this.RefreshPreview);
            // 
            // ComboBoxNavigateColors
            // 
            this.ComboBoxNavigateColors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxNavigateColors.FormattingEnabled = true;
            this.ComboBoxNavigateColors.Items.AddRange(new object[] {
            "16 Color",
            "256 Color"});
            this.ComboBoxNavigateColors.Location = new System.Drawing.Point(18, 65);
            this.ComboBoxNavigateColors.Name = "ComboBoxNavigateColors";
            this.ComboBoxNavigateColors.Size = new System.Drawing.Size(91, 21);
            this.ComboBoxNavigateColors.TabIndex = 8;
            this.ComboBoxNavigateColors.SelectedIndexChanged += new System.EventHandler(this.RefreshPreview);
            // 
            // NavigateGray
            // 
            this.NavigateGray.AutoSize = true;
            this.NavigateGray.Location = new System.Drawing.Point(18, 19);
            this.NavigateGray.Name = "NavigateGray";
            this.NavigateGray.Size = new System.Drawing.Size(73, 17);
            this.NavigateGray.TabIndex = 7;
            this.NavigateGray.Text = "Grayscale";
            this.NavigateGray.UseVisualStyleBackColor = true;
            this.NavigateGray.CheckedChanged += new System.EventHandler(this.RefreshPreview);
            // 
            // GroupPreview
            // 
            this.GroupPreview.Controls.Add(this.NavigatePreview);
            this.GroupPreview.Location = new System.Drawing.Point(3, 79);
            this.GroupPreview.Name = "GroupPreview";
            this.GroupPreview.Size = new System.Drawing.Size(167, 185);
            this.GroupPreview.TabIndex = 6;
            this.GroupPreview.TabStop = false;
            this.GroupPreview.Text = "Preview";
            // 
            // NavigatePreview
            // 
            this.NavigatePreview.BackgroundImage = global::NSE2.Properties.Resources.transparent;
            this.NavigatePreview.Location = new System.Drawing.Point(7, 19);
            this.NavigatePreview.Name = "NavigatePreview";
            this.NavigatePreview.Size = new System.Drawing.Size(154, 160);
            this.NavigatePreview.TabIndex = 0;
            this.NavigatePreview.TabStop = false;
            // 
            // GroupOffsets
            // 
            this.GroupOffsets.BackColor = System.Drawing.Color.White;
            this.GroupOffsets.Controls.Add(this.TextboxNavigatePalette);
            this.GroupOffsets.Controls.Add(this.LabelNavigateOffsetPalette);
            this.GroupOffsets.Controls.Add(this.TextboxNavigateImage);
            this.GroupOffsets.Controls.Add(this.LabelNavigateOffsetImage);
            this.GroupOffsets.Location = new System.Drawing.Point(3, 8);
            this.GroupOffsets.Name = "GroupOffsets";
            this.GroupOffsets.Size = new System.Drawing.Size(167, 65);
            this.GroupOffsets.TabIndex = 5;
            this.GroupOffsets.TabStop = false;
            this.GroupOffsets.Text = "Offsets";
            // 
            // TextboxNavigatePalette
            // 
            this.TextboxNavigatePalette.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextboxNavigatePalette.Location = new System.Drawing.Point(75, 39);
            this.TextboxNavigatePalette.MaxLength = 6;
            this.TextboxNavigatePalette.Name = "TextboxNavigatePalette";
            this.TextboxNavigatePalette.ShortcutsEnabled = false;
            this.TextboxNavigatePalette.Size = new System.Drawing.Size(68, 20);
            this.TextboxNavigatePalette.TabIndex = 3;
            this.TextboxNavigatePalette.Text = "0";
            this.TextboxNavigatePalette.TextChanged += new System.EventHandler(this.RefreshPreview);
            this.TextboxNavigatePalette.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HexKeyPress);
            this.TextboxNavigatePalette.Leave += new System.EventHandler(this.LeaveFeild);
            // 
            // LabelNavigateOffsetPalette
            // 
            this.LabelNavigateOffsetPalette.AutoSize = true;
            this.LabelNavigateOffsetPalette.Location = new System.Drawing.Point(26, 41);
            this.LabelNavigateOffsetPalette.Name = "LabelNavigateOffsetPalette";
            this.LabelNavigateOffsetPalette.Size = new System.Drawing.Size(43, 13);
            this.LabelNavigateOffsetPalette.TabIndex = 2;
            this.LabelNavigateOffsetPalette.Text = "Palette:";
            // 
            // TextboxNavigateImage
            // 
            this.TextboxNavigateImage.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextboxNavigateImage.Location = new System.Drawing.Point(75, 13);
            this.TextboxNavigateImage.MaxLength = 6;
            this.TextboxNavigateImage.Name = "TextboxNavigateImage";
            this.TextboxNavigateImage.ShortcutsEnabled = false;
            this.TextboxNavigateImage.Size = new System.Drawing.Size(68, 20);
            this.TextboxNavigateImage.TabIndex = 1;
            this.TextboxNavigateImage.Text = "0";
            this.TextboxNavigateImage.TextChanged += new System.EventHandler(this.RefreshPreview);
            this.TextboxNavigateImage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HexKeyPress);
            this.TextboxNavigateImage.Leave += new System.EventHandler(this.LeaveFeild);
            // 
            // LabelNavigateOffsetImage
            // 
            this.LabelNavigateOffsetImage.AutoSize = true;
            this.LabelNavigateOffsetImage.Location = new System.Drawing.Point(30, 16);
            this.LabelNavigateOffsetImage.Name = "LabelNavigateOffsetImage";
            this.LabelNavigateOffsetImage.Size = new System.Drawing.Size(39, 13);
            this.LabelNavigateOffsetImage.TabIndex = 0;
            this.LabelNavigateOffsetImage.Text = "Image:";
            // 
            // GroupSize
            // 
            this.GroupSize.Controls.Add(this.LabelNavigateHeight);
            this.GroupSize.Controls.Add(this.LabelNavigateWidth);
            this.GroupSize.Controls.Add(this.ComboBoxNavigateHeight);
            this.GroupSize.Controls.Add(this.ComboBoxNavigateWidth);
            this.GroupSize.Location = new System.Drawing.Point(176, 8);
            this.GroupSize.Name = "GroupSize";
            this.GroupSize.Size = new System.Drawing.Size(122, 65);
            this.GroupSize.TabIndex = 4;
            this.GroupSize.TabStop = false;
            this.GroupSize.Text = "Size";
            // 
            // LabelNavigateHeight
            // 
            this.LabelNavigateHeight.AutoSize = true;
            this.LabelNavigateHeight.Location = new System.Drawing.Point(13, 40);
            this.LabelNavigateHeight.Name = "LabelNavigateHeight";
            this.LabelNavigateHeight.Size = new System.Drawing.Size(41, 13);
            this.LabelNavigateHeight.TabIndex = 3;
            this.LabelNavigateHeight.Text = "Height:";
            // 
            // LabelNavigateWidth
            // 
            this.LabelNavigateWidth.AutoSize = true;
            this.LabelNavigateWidth.Location = new System.Drawing.Point(16, 16);
            this.LabelNavigateWidth.Name = "LabelNavigateWidth";
            this.LabelNavigateWidth.Size = new System.Drawing.Size(38, 13);
            this.LabelNavigateWidth.TabIndex = 2;
            this.LabelNavigateWidth.Text = "Width:";
            // 
            // ComboBoxNavigateHeight
            // 
            this.ComboBoxNavigateHeight.FormattingEnabled = true;
            this.ComboBoxNavigateHeight.Items.AddRange(new object[] {
            "2",
            "4",
            "8",
            "16"});
            this.ComboBoxNavigateHeight.Location = new System.Drawing.Point(60, 37);
            this.ComboBoxNavigateHeight.MaxLength = 3;
            this.ComboBoxNavigateHeight.Name = "ComboBoxNavigateHeight";
            this.ComboBoxNavigateHeight.Size = new System.Drawing.Size(50, 21);
            this.ComboBoxNavigateHeight.TabIndex = 1;
            this.ComboBoxNavigateHeight.Text = "1";
            this.ComboBoxNavigateHeight.TextChanged += new System.EventHandler(this.RefreshPreview);
            this.ComboBoxNavigateHeight.Leave += new System.EventHandler(this.LeaveFeild);
            // 
            // ComboBoxNavigateWidth
            // 
            this.ComboBoxNavigateWidth.FormattingEnabled = true;
            this.ComboBoxNavigateWidth.Items.AddRange(new object[] {
            "2",
            "4",
            "8",
            "16"});
            this.ComboBoxNavigateWidth.Location = new System.Drawing.Point(60, 13);
            this.ComboBoxNavigateWidth.MaxLength = 3;
            this.ComboBoxNavigateWidth.Name = "ComboBoxNavigateWidth";
            this.ComboBoxNavigateWidth.Size = new System.Drawing.Size(50, 21);
            this.ComboBoxNavigateWidth.TabIndex = 0;
            this.ComboBoxNavigateWidth.Text = "1";
            this.ComboBoxNavigateWidth.SelectedIndexChanged += new System.EventHandler(this.RefreshPreview);
            this.ComboBoxNavigateWidth.TextChanged += new System.EventHandler(this.RefreshPreview);
            this.ComboBoxNavigateWidth.Leave += new System.EventHandler(this.LeaveFeild);
            // 
            // TabEdit
            // 
            this.TabEdit.Controls.Add(this.GroupEditPreview);
            this.TabEdit.Controls.Add(this.GroupEditMode);
            this.TabEdit.Controls.Add(this.GroupEditOffsets);
            this.TabEdit.Controls.Add(this.GroupEditSize);
            this.TabEdit.Location = new System.Drawing.Point(4, 22);
            this.TabEdit.Name = "TabEdit";
            this.TabEdit.Padding = new System.Windows.Forms.Padding(3);
            this.TabEdit.Size = new System.Drawing.Size(304, 270);
            this.TabEdit.TabIndex = 1;
            this.TabEdit.Text = "Edit BookMarks";
            this.TabEdit.UseVisualStyleBackColor = true;
            // 
            // GroupEditPreview
            // 
            this.GroupEditPreview.Controls.Add(this.EditPreview);
            this.GroupEditPreview.Enabled = false;
            this.GroupEditPreview.Location = new System.Drawing.Point(3, 79);
            this.GroupEditPreview.Name = "GroupEditPreview";
            this.GroupEditPreview.Size = new System.Drawing.Size(167, 185);
            this.GroupEditPreview.TabIndex = 13;
            this.GroupEditPreview.TabStop = false;
            this.GroupEditPreview.Text = "BookMark Preview";
            // 
            // EditPreview
            // 
            this.EditPreview.BackgroundImage = global::NSE2.Properties.Resources.transparent;
            this.EditPreview.Location = new System.Drawing.Point(7, 19);
            this.EditPreview.Name = "EditPreview";
            this.EditPreview.Size = new System.Drawing.Size(154, 160);
            this.EditPreview.TabIndex = 0;
            this.EditPreview.TabStop = false;
            // 
            // GroupEditMode
            // 
            this.GroupEditMode.Controls.Add(this.EditLabelInfo);
            this.GroupEditMode.Controls.Add(this.EditLz77);
            this.GroupEditMode.Controls.Add(this.ComboBoxEditColors);
            this.GroupEditMode.Controls.Add(this.EditGray);
            this.GroupEditMode.Enabled = false;
            this.GroupEditMode.Location = new System.Drawing.Point(177, 79);
            this.GroupEditMode.Name = "GroupEditMode";
            this.GroupEditMode.Size = new System.Drawing.Size(120, 129);
            this.GroupEditMode.TabIndex = 12;
            this.GroupEditMode.TabStop = false;
            this.GroupEditMode.Text = "Draw Mode";
            // 
            // EditLabelInfo
            // 
            this.EditLabelInfo.AutoSize = true;
            this.EditLabelInfo.Location = new System.Drawing.Point(7, 94);
            this.EditLabelInfo.Name = "EditLabelInfo";
            this.EditLabelInfo.Size = new System.Drawing.Size(106, 26);
            this.EditLabelInfo.TabIndex = 10;
            this.EditLabelInfo.Text = "* Enabled if available\r\nfor this sprite.";
            // 
            // EditLz77
            // 
            this.EditLz77.AutoSize = true;
            this.EditLz77.Checked = true;
            this.EditLz77.CheckState = System.Windows.Forms.CheckState.Checked;
            this.EditLz77.Enabled = false;
            this.EditLz77.Location = new System.Drawing.Point(18, 42);
            this.EditLz77.Name = "EditLz77";
            this.EditLz77.Size = new System.Drawing.Size(56, 17);
            this.EditLz77.TabIndex = 9;
            this.EditLz77.Text = "Lz77 *";
            this.EditLz77.UseVisualStyleBackColor = true;
            this.EditLz77.CheckedChanged += new System.EventHandler(this.RefreshBookMarkPreview);
            // 
            // ComboBoxEditColors
            // 
            this.ComboBoxEditColors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxEditColors.FormattingEnabled = true;
            this.ComboBoxEditColors.Items.AddRange(new object[] {
            "16 Color",
            "256 Color"});
            this.ComboBoxEditColors.Location = new System.Drawing.Point(18, 65);
            this.ComboBoxEditColors.Name = "ComboBoxEditColors";
            this.ComboBoxEditColors.Size = new System.Drawing.Size(91, 21);
            this.ComboBoxEditColors.TabIndex = 8;
            this.ComboBoxEditColors.SelectedIndexChanged += new System.EventHandler(this.RefreshBookMarkPreview);
            // 
            // EditGray
            // 
            this.EditGray.AutoSize = true;
            this.EditGray.Location = new System.Drawing.Point(18, 19);
            this.EditGray.Name = "EditGray";
            this.EditGray.Size = new System.Drawing.Size(73, 17);
            this.EditGray.TabIndex = 7;
            this.EditGray.Text = "Grayscale";
            this.EditGray.UseVisualStyleBackColor = true;
            this.EditGray.CheckedChanged += new System.EventHandler(this.RefreshBookMarkPreview);
            // 
            // GroupEditOffsets
            // 
            this.GroupEditOffsets.BackColor = System.Drawing.Color.White;
            this.GroupEditOffsets.Controls.Add(this.TextBoxEditPalette);
            this.GroupEditOffsets.Controls.Add(this.EditLabelPalette);
            this.GroupEditOffsets.Controls.Add(this.TextBoxEditImage);
            this.GroupEditOffsets.Controls.Add(this.EditLabelImage);
            this.GroupEditOffsets.Enabled = false;
            this.GroupEditOffsets.Location = new System.Drawing.Point(3, 8);
            this.GroupEditOffsets.Name = "GroupEditOffsets";
            this.GroupEditOffsets.Size = new System.Drawing.Size(167, 65);
            this.GroupEditOffsets.TabIndex = 11;
            this.GroupEditOffsets.TabStop = false;
            this.GroupEditOffsets.Text = "Offsets";
            // 
            // TextBoxEditPalette
            // 
            this.TextBoxEditPalette.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextBoxEditPalette.Location = new System.Drawing.Point(75, 39);
            this.TextBoxEditPalette.MaxLength = 6;
            this.TextBoxEditPalette.Name = "TextBoxEditPalette";
            this.TextBoxEditPalette.ShortcutsEnabled = false;
            this.TextBoxEditPalette.Size = new System.Drawing.Size(68, 20);
            this.TextBoxEditPalette.TabIndex = 3;
            this.TextBoxEditPalette.Text = "0";
            this.TextBoxEditPalette.TextChanged += new System.EventHandler(this.RefreshBookMarkPreview);
            this.TextBoxEditPalette.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HexKeyPress);
            this.TextBoxEditPalette.Leave += new System.EventHandler(this.LeaveFeild);
            // 
            // EditLabelPalette
            // 
            this.EditLabelPalette.AutoSize = true;
            this.EditLabelPalette.Location = new System.Drawing.Point(26, 41);
            this.EditLabelPalette.Name = "EditLabelPalette";
            this.EditLabelPalette.Size = new System.Drawing.Size(43, 13);
            this.EditLabelPalette.TabIndex = 2;
            this.EditLabelPalette.Text = "Palette:";
            // 
            // TextBoxEditImage
            // 
            this.TextBoxEditImage.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextBoxEditImage.Location = new System.Drawing.Point(75, 13);
            this.TextBoxEditImage.MaxLength = 6;
            this.TextBoxEditImage.Name = "TextBoxEditImage";
            this.TextBoxEditImage.ShortcutsEnabled = false;
            this.TextBoxEditImage.Size = new System.Drawing.Size(68, 20);
            this.TextBoxEditImage.TabIndex = 1;
            this.TextBoxEditImage.Text = "0";
            this.TextBoxEditImage.TextChanged += new System.EventHandler(this.RefreshBookMarkPreview);
            this.TextBoxEditImage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HexKeyPress);
            this.TextBoxEditImage.Leave += new System.EventHandler(this.LeaveFeild);
            // 
            // EditLabelImage
            // 
            this.EditLabelImage.AutoSize = true;
            this.EditLabelImage.Location = new System.Drawing.Point(30, 16);
            this.EditLabelImage.Name = "EditLabelImage";
            this.EditLabelImage.Size = new System.Drawing.Size(39, 13);
            this.EditLabelImage.TabIndex = 0;
            this.EditLabelImage.Text = "Image:";
            // 
            // GroupEditSize
            // 
            this.GroupEditSize.Controls.Add(this.EditLabelHeight);
            this.GroupEditSize.Controls.Add(this.EditLabelWidth);
            this.GroupEditSize.Controls.Add(this.ComboBoxEditHeight);
            this.GroupEditSize.Controls.Add(this.ComboBoxEditWidth);
            this.GroupEditSize.Enabled = false;
            this.GroupEditSize.Location = new System.Drawing.Point(176, 8);
            this.GroupEditSize.Name = "GroupEditSize";
            this.GroupEditSize.Size = new System.Drawing.Size(122, 65);
            this.GroupEditSize.TabIndex = 10;
            this.GroupEditSize.TabStop = false;
            this.GroupEditSize.Text = "Size";
            // 
            // EditLabelHeight
            // 
            this.EditLabelHeight.AutoSize = true;
            this.EditLabelHeight.Location = new System.Drawing.Point(13, 40);
            this.EditLabelHeight.Name = "EditLabelHeight";
            this.EditLabelHeight.Size = new System.Drawing.Size(41, 13);
            this.EditLabelHeight.TabIndex = 3;
            this.EditLabelHeight.Text = "Height:";
            // 
            // EditLabelWidth
            // 
            this.EditLabelWidth.AutoSize = true;
            this.EditLabelWidth.Location = new System.Drawing.Point(16, 16);
            this.EditLabelWidth.Name = "EditLabelWidth";
            this.EditLabelWidth.Size = new System.Drawing.Size(38, 13);
            this.EditLabelWidth.TabIndex = 2;
            this.EditLabelWidth.Text = "Width:";
            // 
            // ComboBoxEditHeight
            // 
            this.ComboBoxEditHeight.FormattingEnabled = true;
            this.ComboBoxEditHeight.Items.AddRange(new object[] {
            "2",
            "4",
            "8",
            "16"});
            this.ComboBoxEditHeight.Location = new System.Drawing.Point(60, 37);
            this.ComboBoxEditHeight.MaxLength = 3;
            this.ComboBoxEditHeight.Name = "ComboBoxEditHeight";
            this.ComboBoxEditHeight.Size = new System.Drawing.Size(50, 21);
            this.ComboBoxEditHeight.TabIndex = 1;
            this.ComboBoxEditHeight.Text = "1";
            this.ComboBoxEditHeight.SelectedIndexChanged += new System.EventHandler(this.RefreshBookMarkPreview);
            this.ComboBoxEditHeight.TextChanged += new System.EventHandler(this.RefreshBookMarkPreview);
            this.ComboBoxEditHeight.Leave += new System.EventHandler(this.LeaveFeild);
            // 
            // ComboBoxEditWidth
            // 
            this.ComboBoxEditWidth.FormattingEnabled = true;
            this.ComboBoxEditWidth.Items.AddRange(new object[] {
            "2",
            "4",
            "8",
            "16"});
            this.ComboBoxEditWidth.Location = new System.Drawing.Point(60, 13);
            this.ComboBoxEditWidth.MaxLength = 3;
            this.ComboBoxEditWidth.Name = "ComboBoxEditWidth";
            this.ComboBoxEditWidth.Size = new System.Drawing.Size(50, 21);
            this.ComboBoxEditWidth.TabIndex = 0;
            this.ComboBoxEditWidth.Text = "1";
            this.ComboBoxEditWidth.SelectedIndexChanged += new System.EventHandler(this.RefreshBookMarkPreview);
            this.ComboBoxEditWidth.TextChanged += new System.EventHandler(this.RefreshBookMarkPreview);
            this.ComboBoxEditWidth.Leave += new System.EventHandler(this.LeaveFeild);
            // 
            // PanelBookMark
            // 
            this.PanelBookMark.Controls.Add(this.BookMarkTreeView);
            this.PanelBookMark.Controls.Add(this.ToolStripBookMark);
            this.PanelBookMark.Location = new System.Drawing.Point(325, 24);
            this.PanelBookMark.Name = "PanelBookMark";
            this.PanelBookMark.Size = new System.Drawing.Size(250, 250);
            this.PanelBookMark.TabIndex = 47;
            // 
            // ToolStripBookMark
            // 
            this.ToolStripBookMark.Enabled = false;
            this.ToolStripBookMark.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStripBookMark.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripAddBookMark,
            this.ToolStripAddFolder,
            this.ToolStripDelete,
            this.ToolStripSeparator,
            this.ToolStripAddCurrent});
            this.ToolStripBookMark.Location = new System.Drawing.Point(0, 0);
            this.ToolStripBookMark.Name = "ToolStripBookMark";
            this.ToolStripBookMark.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ToolStripBookMark.Size = new System.Drawing.Size(250, 25);
            this.ToolStripBookMark.TabIndex = 42;
            this.ToolStripBookMark.Text = "toolStrip1";
            // 
            // ToolStripAddBookMark
            // 
            this.ToolStripAddBookMark.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripAddBookMark.Image = global::NSE2.Properties.Resources.NewStar;
            this.ToolStripAddBookMark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripAddBookMark.Name = "ToolStripAddBookMark";
            this.ToolStripAddBookMark.Size = new System.Drawing.Size(23, 22);
            this.ToolStripAddBookMark.Text = "Add BookMark";
            this.ToolStripAddBookMark.Click += new System.EventHandler(this.ToolStripAddBookMark_Click);
            // 
            // ToolStripAddFolder
            // 
            this.ToolStripAddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripAddFolder.Image = global::NSE2.Properties.Resources.NewFolder;
            this.ToolStripAddFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripAddFolder.Name = "ToolStripAddFolder";
            this.ToolStripAddFolder.Size = new System.Drawing.Size(23, 22);
            this.ToolStripAddFolder.Text = "Add Folder";
            this.ToolStripAddFolder.Click += new System.EventHandler(this.ToolStripAddFolder_Click);
            // 
            // ToolStripDelete
            // 
            this.ToolStripDelete.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ToolStripDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripDelete.Image = global::NSE2.Properties.Resources.Delete;
            this.ToolStripDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripDelete.Name = "ToolStripDelete";
            this.ToolStripDelete.Size = new System.Drawing.Size(23, 22);
            this.ToolStripDelete.Text = "Delete";
            this.ToolStripDelete.Click += new System.EventHandler(this.ToolStripDelete_Click);
            // 
            // ToolStripSeparator
            // 
            this.ToolStripSeparator.Name = "ToolStripSeparator";
            this.ToolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripAddCurrent
            // 
            this.ToolStripAddCurrent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripAddCurrent.Image = global::NSE2.Properties.Resources.AddStar;
            this.ToolStripAddCurrent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripAddCurrent.Name = "ToolStripAddCurrent";
            this.ToolStripAddCurrent.Size = new System.Drawing.Size(23, 22);
            this.ToolStripAddCurrent.Text = "BookMark Current Sprite";
            this.ToolStripAddCurrent.Visible = false;
            this.ToolStripAddCurrent.Click += new System.EventHandler(this.ToolStripAddCurrent_Click);
            // 
            // Navigate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 306);
            this.Controls.Add(this.PanelBookMark);
            this.Controls.Add(this.Tabs);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.ButtonOpen);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Navigate";
            this.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Navigate";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Navigate_FormClosed);
            this.Load += new System.EventHandler(this.Navigate_Load);
            this.Tabs.ResumeLayout(false);
            this.TabNavigate.ResumeLayout(false);
            this.GroupDrawMode.ResumeLayout(false);
            this.GroupDrawMode.PerformLayout();
            this.GroupPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NavigatePreview)).EndInit();
            this.GroupOffsets.ResumeLayout(false);
            this.GroupOffsets.PerformLayout();
            this.GroupSize.ResumeLayout(false);
            this.GroupSize.PerformLayout();
            this.TabEdit.ResumeLayout(false);
            this.GroupEditPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.EditPreview)).EndInit();
            this.GroupEditMode.ResumeLayout(false);
            this.GroupEditMode.PerformLayout();
            this.GroupEditOffsets.ResumeLayout(false);
            this.GroupEditOffsets.PerformLayout();
            this.GroupEditSize.ResumeLayout(false);
            this.GroupEditSize.PerformLayout();
            this.PanelBookMark.ResumeLayout(false);
            this.PanelBookMark.PerformLayout();
            this.ToolStripBookMark.ResumeLayout(false);
            this.ToolStripBookMark.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TreeView BookMarkTreeView;
        internal System.Windows.Forms.Button ButtonClose;
        internal System.Windows.Forms.Button ButtonOpen;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage TabNavigate;
        private System.Windows.Forms.GroupBox GroupPreview;
        private System.Windows.Forms.PictureBox NavigatePreview;
        private System.Windows.Forms.GroupBox GroupOffsets;
        private System.Windows.Forms.TextBox TextboxNavigatePalette;
        private System.Windows.Forms.Label LabelNavigateOffsetPalette;
        private System.Windows.Forms.TextBox TextboxNavigateImage;
        private System.Windows.Forms.Label LabelNavigateOffsetImage;
        private System.Windows.Forms.GroupBox GroupSize;
        private System.Windows.Forms.Label LabelNavigateHeight;
        private System.Windows.Forms.Label LabelNavigateWidth;
        private System.Windows.Forms.ComboBox ComboBoxNavigateHeight;
        private System.Windows.Forms.ComboBox ComboBoxNavigateWidth;
        private System.Windows.Forms.TabPage TabEdit;
        private System.Windows.Forms.CheckBox NavigateGray;
        private System.Windows.Forms.ComboBox ComboBoxNavigateColors;
        private System.Windows.Forms.GroupBox GroupDrawMode;
        private System.Windows.Forms.Label NavigateLabelInfo;
        private System.Windows.Forms.CheckBox NavigateLz77;
        private System.Windows.Forms.GroupBox GroupEditPreview;
        private System.Windows.Forms.PictureBox EditPreview;
        private System.Windows.Forms.GroupBox GroupEditMode;
        private System.Windows.Forms.Label EditLabelInfo;
        private System.Windows.Forms.CheckBox EditLz77;
        private System.Windows.Forms.ComboBox ComboBoxEditColors;
        private System.Windows.Forms.CheckBox EditGray;
        private System.Windows.Forms.GroupBox GroupEditOffsets;
        private System.Windows.Forms.TextBox TextBoxEditPalette;
        private System.Windows.Forms.Label EditLabelPalette;
        private System.Windows.Forms.TextBox TextBoxEditImage;
        private System.Windows.Forms.Label EditLabelImage;
        private System.Windows.Forms.GroupBox GroupEditSize;
        private System.Windows.Forms.Label EditLabelHeight;
        private System.Windows.Forms.Label EditLabelWidth;
        private System.Windows.Forms.ComboBox ComboBoxEditHeight;
        private System.Windows.Forms.ComboBox ComboBoxEditWidth;
        private System.Windows.Forms.Panel PanelBookMark;
        private System.Windows.Forms.ToolStrip ToolStripBookMark;
        private System.Windows.Forms.ToolStripButton ToolStripAddBookMark;
        private System.Windows.Forms.ToolStripButton ToolStripAddFolder;
        private System.Windows.Forms.ImageList TreeImageList;
        private System.Windows.Forms.ToolStripButton ToolStripDelete;
        private System.Windows.Forms.ToolStripSeparator ToolStripSeparator;
        public System.Windows.Forms.ToolStripButton ToolStripAddCurrent;
    }
}