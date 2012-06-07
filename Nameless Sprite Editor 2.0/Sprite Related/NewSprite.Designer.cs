namespace NSE2
{
    partial class NewSprite
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
            this.LabelName = new System.Windows.Forms.Label();
            this.TextBoxName = new System.Windows.Forms.TextBox();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.PanelMain = new System.Windows.Forms.Panel();
            this.ComboBoxBits = new System.Windows.Forms.ComboBox();
            this.ComboBoxColors = new System.Windows.Forms.ComboBox();
            this.LabelColorMode = new System.Windows.Forms.Label();
            this.ComboBoxHeight = new System.Windows.Forms.ComboBox();
            this.ComboBoxWidth = new System.Windows.Forms.ComboBox();
            this.TextBoxHeight = new System.Windows.Forms.TextBox();
            this.TextBoxWidth = new System.Windows.Forms.TextBox();
            this.LabelHeight = new System.Windows.Forms.Label();
            this.LabelWidth = new System.Windows.Forms.Label();
            this.PanelPreset = new System.Windows.Forms.Panel();
            this.ComboBoxPreset = new System.Windows.Forms.ComboBox();
            this.LabelPreset = new System.Windows.Forms.Label();
            this.LabelIamgeSizeTitle = new System.Windows.Forms.Label();
            this.LabelSize = new System.Windows.Forms.Label();
            this.PanelMain.SuspendLayout();
            this.PanelPreset.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Location = new System.Drawing.Point(102, 15);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(38, 13);
            this.LabelName.TabIndex = 0;
            this.LabelName.Text = "Name:";
            // 
            // TextBoxName
            // 
            this.TextBoxName.Location = new System.Drawing.Point(141, 12);
            this.TextBoxName.Name = "TextBoxName";
            this.TextBoxName.Size = new System.Drawing.Size(208, 20);
            this.TextBoxName.TabIndex = 0;
            this.TextBoxName.Text = "Untitled";
            this.TextBoxName.Leave += new System.EventHandler(this.TextBoxName_Leave);
            // 
            // ButtonOK
            // 
            this.ButtonOK.Location = new System.Drawing.Point(367, 10);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(128, 23);
            this.ButtonOK.TabIndex = 0;
            this.ButtonOK.Text = "OK";
            this.ButtonOK.UseVisualStyleBackColor = true;
            this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(367, 39);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(128, 23);
            this.ButtonCancel.TabIndex = 1;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            // 
            // PanelMain
            // 
            this.PanelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelMain.Controls.Add(this.ComboBoxBits);
            this.PanelMain.Controls.Add(this.ComboBoxColors);
            this.PanelMain.Controls.Add(this.LabelColorMode);
            this.PanelMain.Controls.Add(this.ComboBoxHeight);
            this.PanelMain.Controls.Add(this.ComboBoxWidth);
            this.PanelMain.Controls.Add(this.TextBoxHeight);
            this.PanelMain.Controls.Add(this.TextBoxWidth);
            this.PanelMain.Controls.Add(this.LabelHeight);
            this.PanelMain.Controls.Add(this.LabelWidth);
            this.PanelMain.Location = new System.Drawing.Point(18, 50);
            this.PanelMain.Name = "PanelMain";
            this.PanelMain.Size = new System.Drawing.Size(330, 144);
            this.PanelMain.TabIndex = 4;
            // 
            // ComboBoxBits
            // 
            this.ComboBoxBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxBits.FormattingEnabled = true;
            this.ComboBoxBits.Items.AddRange(new object[] {
            "4 bit : 16 color",
            "8 bit : 256 color"});
            this.ComboBoxBits.Location = new System.Drawing.Point(201, 83);
            this.ComboBoxBits.Name = "ComboBoxBits";
            this.ComboBoxBits.Size = new System.Drawing.Size(121, 21);
            this.ComboBoxBits.TabIndex = 10;
            this.ComboBoxBits.SelectedIndexChanged += new System.EventHandler(this.ComboBoxBits_SelectedIndexChanged);
            // 
            // ComboBoxColors
            // 
            this.ComboBoxColors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxColors.FormattingEnabled = true;
            this.ComboBoxColors.Items.AddRange(new object[] {
            "GBA Color",
            "Grayscale"});
            this.ComboBoxColors.Location = new System.Drawing.Point(95, 83);
            this.ComboBoxColors.Name = "ComboBoxColors";
            this.ComboBoxColors.Size = new System.Drawing.Size(100, 21);
            this.ComboBoxColors.TabIndex = 9;
            // 
            // LabelColorMode
            // 
            this.LabelColorMode.AutoSize = true;
            this.LabelColorMode.Location = new System.Drawing.Point(28, 86);
            this.LabelColorMode.Name = "LabelColorMode";
            this.LabelColorMode.Size = new System.Drawing.Size(64, 13);
            this.LabelColorMode.TabIndex = 8;
            this.LabelColorMode.Text = "Color Mode:";
            // 
            // ComboBoxHeight
            // 
            this.ComboBoxHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxHeight.FormattingEnabled = true;
            this.ComboBoxHeight.Items.AddRange(new object[] {
            "blocks",
            "pixels"});
            this.ComboBoxHeight.Location = new System.Drawing.Point(201, 53);
            this.ComboBoxHeight.Name = "ComboBoxHeight";
            this.ComboBoxHeight.Size = new System.Drawing.Size(121, 21);
            this.ComboBoxHeight.TabIndex = 7;
            this.ComboBoxHeight.SelectedIndexChanged += new System.EventHandler(this.ComboBoxHeight_SelectedIndexChanged);
            // 
            // ComboBoxWidth
            // 
            this.ComboBoxWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxWidth.FormattingEnabled = true;
            this.ComboBoxWidth.Items.AddRange(new object[] {
            "blocks",
            "pixels"});
            this.ComboBoxWidth.Location = new System.Drawing.Point(201, 27);
            this.ComboBoxWidth.Name = "ComboBoxWidth";
            this.ComboBoxWidth.Size = new System.Drawing.Size(121, 21);
            this.ComboBoxWidth.TabIndex = 5;
            this.ComboBoxWidth.SelectedIndexChanged += new System.EventHandler(this.ComboBoxWidth_SelectedIndexChanged);
            // 
            // TextBoxHeight
            // 
            this.TextBoxHeight.Location = new System.Drawing.Point(95, 54);
            this.TextBoxHeight.MaxLength = 3;
            this.TextBoxHeight.Name = "TextBoxHeight";
            this.TextBoxHeight.ShortcutsEnabled = false;
            this.TextBoxHeight.Size = new System.Drawing.Size(100, 20);
            this.TextBoxHeight.TabIndex = 6;
            this.TextBoxHeight.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitKeyPress);
            this.TextBoxHeight.Leave += new System.EventHandler(this.TextBoxHeight_Leave);
            // 
            // TextBoxWidth
            // 
            this.TextBoxWidth.Location = new System.Drawing.Point(95, 28);
            this.TextBoxWidth.MaxLength = 3;
            this.TextBoxWidth.Name = "TextBoxWidth";
            this.TextBoxWidth.ShortcutsEnabled = false;
            this.TextBoxWidth.Size = new System.Drawing.Size(100, 20);
            this.TextBoxWidth.TabIndex = 4;
            this.TextBoxWidth.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            this.TextBoxWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitKeyPress);
            this.TextBoxWidth.Leave += new System.EventHandler(this.TextBoxWidth_Leave);
            // 
            // LabelHeight
            // 
            this.LabelHeight.AutoSize = true;
            this.LabelHeight.Location = new System.Drawing.Point(51, 57);
            this.LabelHeight.Name = "LabelHeight";
            this.LabelHeight.Size = new System.Drawing.Size(41, 13);
            this.LabelHeight.TabIndex = 1;
            this.LabelHeight.Text = "Height:";
            // 
            // LabelWidth
            // 
            this.LabelWidth.AutoSize = true;
            this.LabelWidth.Location = new System.Drawing.Point(54, 31);
            this.LabelWidth.Name = "LabelWidth";
            this.LabelWidth.Size = new System.Drawing.Size(38, 13);
            this.LabelWidth.TabIndex = 0;
            this.LabelWidth.Text = "Width:";
            // 
            // PanelPreset
            // 
            this.PanelPreset.Controls.Add(this.ComboBoxPreset);
            this.PanelPreset.Controls.Add(this.LabelPreset);
            this.PanelPreset.Location = new System.Drawing.Point(35, 38);
            this.PanelPreset.Name = "PanelPreset";
            this.PanelPreset.Padding = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.PanelPreset.Size = new System.Drawing.Size(248, 30);
            this.PanelPreset.TabIndex = 5;
            this.PanelPreset.TabStop = true;
            // 
            // ComboBoxPreset
            // 
            this.ComboBoxPreset.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComboBoxPreset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxPreset.FormattingEnabled = true;
            this.ComboBoxPreset.Items.AddRange(new object[] {
            "2x4 OW 4bpp",
            "4x4 OW 4bpp",
            "8x8 Sprite 4bpp"});
            this.ComboBoxPreset.Location = new System.Drawing.Point(45, 3);
            this.ComboBoxPreset.Name = "ComboBoxPreset";
            this.ComboBoxPreset.Size = new System.Drawing.Size(198, 21);
            this.ComboBoxPreset.TabIndex = 3;
            this.ComboBoxPreset.SelectedIndexChanged += new System.EventHandler(this.ComboBoxPreset_SelectedIndexChanged);
            // 
            // LabelPreset
            // 
            this.LabelPreset.AutoSize = true;
            this.LabelPreset.Dock = System.Windows.Forms.DockStyle.Left;
            this.LabelPreset.Location = new System.Drawing.Point(5, 3);
            this.LabelPreset.MinimumSize = new System.Drawing.Size(0, 20);
            this.LabelPreset.Name = "LabelPreset";
            this.LabelPreset.Size = new System.Drawing.Size(40, 20);
            this.LabelPreset.TabIndex = 0;
            this.LabelPreset.Text = "Preset:";
            this.LabelPreset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LabelIamgeSizeTitle
            // 
            this.LabelIamgeSizeTitle.AutoSize = true;
            this.LabelIamgeSizeTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelIamgeSizeTitle.Location = new System.Drawing.Point(402, 89);
            this.LabelIamgeSizeTitle.Name = "LabelIamgeSizeTitle";
            this.LabelIamgeSizeTitle.Size = new System.Drawing.Size(60, 13);
            this.LabelIamgeSizeTitle.TabIndex = 6;
            this.LabelIamgeSizeTitle.Text = "Sprite Size:";
            // 
            // LabelSize
            // 
            this.LabelSize.AutoSize = true;
            this.LabelSize.Location = new System.Drawing.Point(385, 108);
            this.LabelSize.MinimumSize = new System.Drawing.Size(100, 0);
            this.LabelSize.Name = "LabelSize";
            this.LabelSize.Size = new System.Drawing.Size(100, 13);
            this.LabelSize.TabIndex = 7;
            this.LabelSize.Text = "0B";
            this.LabelSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NewSprite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(507, 211);
            this.Controls.Add(this.LabelSize);
            this.Controls.Add(this.LabelIamgeSizeTitle);
            this.Controls.Add(this.PanelPreset);
            this.Controls.Add(this.PanelMain);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOK);
            this.Controls.Add(this.TextBoxName);
            this.Controls.Add(this.LabelName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewSprite";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Sprite";
            this.Load += new System.EventHandler(this.NewSprite_Load);
            this.PanelMain.ResumeLayout(false);
            this.PanelMain.PerformLayout();
            this.PanelPreset.ResumeLayout(false);
            this.PanelPreset.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelName;
        private System.Windows.Forms.TextBox TextBoxName;
        private System.Windows.Forms.Button ButtonOK;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.Panel PanelMain;
        private System.Windows.Forms.Panel PanelPreset;
        private System.Windows.Forms.ComboBox ComboBoxPreset;
        private System.Windows.Forms.Label LabelPreset;
        private System.Windows.Forms.ComboBox ComboBoxHeight;
        private System.Windows.Forms.ComboBox ComboBoxWidth;
        private System.Windows.Forms.TextBox TextBoxHeight;
        private System.Windows.Forms.TextBox TextBoxWidth;
        private System.Windows.Forms.Label LabelHeight;
        private System.Windows.Forms.Label LabelWidth;
        private System.Windows.Forms.Label LabelIamgeSizeTitle;
        private System.Windows.Forms.Label LabelSize;
        private System.Windows.Forms.ComboBox ComboBoxBits;
        private System.Windows.Forms.ComboBox ComboBoxColors;
        private System.Windows.Forms.Label LabelColorMode;
    }
}