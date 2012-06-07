namespace NSE2
{
    partial class SelectColorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectColorForm));
            this.PictureCurrent = new System.Windows.Forms.PictureBox();
            this.LabelR = new System.Windows.Forms.Label();
            this.LabelG = new System.Windows.Forms.Label();
            this.LabelB = new System.Windows.Forms.Label();
            this.BarR = new System.Windows.Forms.ProgressBar();
            this.BarG = new System.Windows.Forms.ProgressBar();
            this.BarB = new System.Windows.Forms.ProgressBar();
            this.TextBoxGBA = new System.Windows.Forms.TextBox();
            this.LabelGBA = new System.Windows.Forms.Label();
            this.ButtonEdit = new System.Windows.Forms.Button();
            this.PickedColor = new System.Windows.Forms.PictureBox();
            this.ButtonEyedropper = new System.Windows.Forms.Button();
            this.GroupEdit = new System.Windows.Forms.GroupBox();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ButtonCollapse = new System.Windows.Forms.Button();
            this.EyedropTimer = new System.Windows.Forms.Timer(this.components);
            this.MainSelectColor = new NSE_Framework.Controls.SelectColor();
            ((System.ComponentModel.ISupportInitialize)(this.PictureCurrent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PickedColor)).BeginInit();
            this.GroupEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureCurrent
            // 
            this.PictureCurrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureCurrent.Location = new System.Drawing.Point(7, 19);
            this.PictureCurrent.Name = "PictureCurrent";
            this.PictureCurrent.Size = new System.Drawing.Size(75, 50);
            this.PictureCurrent.TabIndex = 1;
            this.PictureCurrent.TabStop = false;
            // 
            // LabelR
            // 
            this.LabelR.AutoSize = true;
            this.LabelR.Location = new System.Drawing.Point(88, 19);
            this.LabelR.Name = "LabelR";
            this.LabelR.Size = new System.Drawing.Size(39, 13);
            this.LabelR.TabIndex = 2;
            this.LabelR.Text = "R: 255";
            // 
            // LabelG
            // 
            this.LabelG.AutoSize = true;
            this.LabelG.Location = new System.Drawing.Point(88, 38);
            this.LabelG.Name = "LabelG";
            this.LabelG.Size = new System.Drawing.Size(39, 13);
            this.LabelG.TabIndex = 3;
            this.LabelG.Text = "G: 255";
            // 
            // LabelB
            // 
            this.LabelB.AutoSize = true;
            this.LabelB.Location = new System.Drawing.Point(88, 60);
            this.LabelB.Name = "LabelB";
            this.LabelB.Size = new System.Drawing.Size(38, 13);
            this.LabelB.TabIndex = 4;
            this.LabelB.Text = "B: 255";
            // 
            // BarR
            // 
            this.BarR.Cursor = System.Windows.Forms.Cursors.Default;
            this.BarR.Location = new System.Drawing.Point(129, 19);
            this.BarR.Maximum = 31;
            this.BarR.Name = "BarR";
            this.BarR.Size = new System.Drawing.Size(128, 13);
            this.BarR.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BarR.TabIndex = 6;
            this.BarR.Value = 15;
            // 
            // BarG
            // 
            this.BarG.Cursor = System.Windows.Forms.Cursors.Default;
            this.BarG.Location = new System.Drawing.Point(129, 38);
            this.BarG.Maximum = 31;
            this.BarG.Name = "BarG";
            this.BarG.Size = new System.Drawing.Size(128, 13);
            this.BarG.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BarG.TabIndex = 7;
            this.BarG.Value = 15;
            // 
            // BarB
            // 
            this.BarB.Cursor = System.Windows.Forms.Cursors.Default;
            this.BarB.Location = new System.Drawing.Point(129, 60);
            this.BarB.Maximum = 31;
            this.BarB.Name = "BarB";
            this.BarB.Size = new System.Drawing.Size(128, 13);
            this.BarB.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.BarB.TabIndex = 8;
            this.BarB.Value = 15;
            // 
            // TextBoxGBA
            // 
            this.TextBoxGBA.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.TextBoxGBA.Location = new System.Drawing.Point(129, 79);
            this.TextBoxGBA.MaxLength = 4;
            this.TextBoxGBA.Name = "TextBoxGBA";
            this.TextBoxGBA.Size = new System.Drawing.Size(65, 20);
            this.TextBoxGBA.TabIndex = 9;
            this.TextBoxGBA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TextBoxGBA.TextChanged += new System.EventHandler(this.TextBoxGBA_TextChanged);
            this.TextBoxGBA.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HexKeyPress);
            this.TextBoxGBA.Leave += new System.EventHandler(this.TextBoxGBA_Leave);
            // 
            // LabelGBA
            // 
            this.LabelGBA.AutoSize = true;
            this.LabelGBA.Location = new System.Drawing.Point(88, 82);
            this.LabelGBA.Name = "LabelGBA";
            this.LabelGBA.Size = new System.Drawing.Size(32, 13);
            this.LabelGBA.TabIndex = 10;
            this.LabelGBA.Text = "GBA:";
            // 
            // ButtonEdit
            // 
            this.ButtonEdit.Location = new System.Drawing.Point(7, 77);
            this.ButtonEdit.Name = "ButtonEdit";
            this.ButtonEdit.Size = new System.Drawing.Size(75, 23);
            this.ButtonEdit.TabIndex = 11;
            this.ButtonEdit.Text = "Edit";
            this.ButtonEdit.UseVisualStyleBackColor = true;
            this.ButtonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // PickedColor
            // 
            this.PickedColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PickedColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PickedColor.Location = new System.Drawing.Point(91, 105);
            this.PickedColor.Name = "PickedColor";
            this.PickedColor.Size = new System.Drawing.Size(75, 23);
            this.PickedColor.TabIndex = 12;
            this.PickedColor.TabStop = false;
            this.PickedColor.Click += new System.EventHandler(this.PickedColor_Click);
            // 
            // ButtonEyedropper
            // 
            this.ButtonEyedropper.Image = global::NSE2.Properties.Resources.eyedropper;
            this.ButtonEyedropper.Location = new System.Drawing.Point(7, 105);
            this.ButtonEyedropper.Name = "ButtonEyedropper";
            this.ButtonEyedropper.Size = new System.Drawing.Size(75, 23);
            this.ButtonEyedropper.TabIndex = 13;
            this.ButtonEyedropper.UseVisualStyleBackColor = true;
            this.ButtonEyedropper.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ButtonEyedropper_MouseDown);
            this.ButtonEyedropper.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ButtonEyedropper_MouseUp);
            // 
            // GroupEdit
            // 
            this.GroupEdit.Controls.Add(this.ButtonSave);
            this.GroupEdit.Controls.Add(this.ButtonEyedropper);
            this.GroupEdit.Controls.Add(this.PickedColor);
            this.GroupEdit.Controls.Add(this.ButtonEdit);
            this.GroupEdit.Controls.Add(this.LabelGBA);
            this.GroupEdit.Controls.Add(this.TextBoxGBA);
            this.GroupEdit.Controls.Add(this.BarB);
            this.GroupEdit.Controls.Add(this.BarG);
            this.GroupEdit.Controls.Add(this.BarR);
            this.GroupEdit.Controls.Add(this.LabelB);
            this.GroupEdit.Controls.Add(this.LabelG);
            this.GroupEdit.Controls.Add(this.LabelR);
            this.GroupEdit.Controls.Add(this.PictureCurrent);
            this.GroupEdit.Location = new System.Drawing.Point(12, 275);
            this.GroupEdit.Name = "GroupEdit";
            this.GroupEdit.Size = new System.Drawing.Size(261, 163);
            this.GroupEdit.TabIndex = 14;
            this.GroupEdit.TabStop = false;
            this.GroupEdit.Text = "Edit Current";
            // 
            // ButtonSave
            // 
            this.ButtonSave.Location = new System.Drawing.Point(6, 134);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(250, 23);
            this.ButtonSave.TabIndex = 14;
            this.ButtonSave.Text = "Save Palette to ROM";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ButtonCollapse
            // 
            this.ButtonCollapse.Location = new System.Drawing.Point(250, 270);
            this.ButtonCollapse.Name = "ButtonCollapse";
            this.ButtonCollapse.Size = new System.Drawing.Size(20, 20);
            this.ButtonCollapse.TabIndex = 14;
            this.ButtonCollapse.Text = "+";
            this.ButtonCollapse.UseVisualStyleBackColor = true;
            this.ButtonCollapse.Click += new System.EventHandler(this.ButtonCollapse_Click);
            // 
            // EyedropTimer
            // 
            this.EyedropTimer.Tick += new System.EventHandler(this.EyedropTimer_Tick);
            // 
            // MainSelectColor
            // 
            this.MainSelectColor.BackColor = System.Drawing.Color.Transparent;
            this.MainSelectColor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MainSelectColor.BackgroundImage")));
            this.MainSelectColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MainSelectColor.Index = ((byte)(0));
            this.MainSelectColor.Location = new System.Drawing.Point(12, 11);
            this.MainSelectColor.Name = "MainSelectColor";
            this.MainSelectColor.ShowBackground = true;
            this.MainSelectColor.Size = new System.Drawing.Size(258, 258);
            this.MainSelectColor.TabIndex = 0;
            this.MainSelectColor.SelectedColorChanged += new System.EventHandler(this.MainSelectColor_SelectedColorChanged);
            this.MainSelectColor.SelectedEditorChanged += new System.EventHandler(this.MainSelectColor_SelectedColorChanged);
            this.MainSelectColor.Redrawn += new System.EventHandler(this.MainSelectColor_SelectedColorChanged);
            // 
            // SelectColorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 291);
            this.Controls.Add(this.ButtonCollapse);
            this.Controls.Add(this.GroupEdit);
            this.Controls.Add(this.MainSelectColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(289, 315);
            this.Name = "SelectColorForm";
            this.Padding = new System.Windows.Forms.Padding(30);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Select Color";
            this.Activated += new System.EventHandler(this.SelectColorForm_Activated);
            this.Load += new System.EventHandler(this.SelectColorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PictureCurrent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PickedColor)).EndInit();
            this.GroupEdit.ResumeLayout(false);
            this.GroupEdit.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private NSE_Framework.Controls.SelectColor MainSelectColor;
        private System.Windows.Forms.PictureBox PictureCurrent;
        private System.Windows.Forms.Label LabelR;
        private System.Windows.Forms.Label LabelG;
        private System.Windows.Forms.Label LabelB;
        private System.Windows.Forms.ProgressBar BarR;
        private System.Windows.Forms.ProgressBar BarG;
        private System.Windows.Forms.ProgressBar BarB;
        private System.Windows.Forms.TextBox TextBoxGBA;
        private System.Windows.Forms.Label LabelGBA;
        private System.Windows.Forms.Button ButtonEdit;
        private System.Windows.Forms.PictureBox PickedColor;
        private System.Windows.Forms.Button ButtonEyedropper;
        private System.Windows.Forms.GroupBox GroupEdit;
        private System.Windows.Forms.Timer EyedropTimer;
        private System.Windows.Forms.Button ButtonSave;
        public System.Windows.Forms.Button ButtonCollapse;

    }
}