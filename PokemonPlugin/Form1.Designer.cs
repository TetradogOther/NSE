namespace PokemonPlugin
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.TopPanel = new System.Windows.Forms.Panel();
            this.NumericUpDownPalette = new System.Windows.Forms.NumericUpDown();
            this.PictureboxA2 = new System.Windows.Forms.PictureBox();
            this.PictureboxA1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.IndexText = new System.Windows.Forms.TextBox();
            this.ButtonNext = new System.Windows.Forms.Button();
            this.ButtonPrev = new System.Windows.Forms.Button();
            this.PictureboxBS = new System.Windows.Forms.PictureBox();
            this.PictureboxBN = new System.Windows.Forms.PictureBox();
            this.PictureboxFS = new System.Windows.Forms.PictureBox();
            this.PictureboxFN = new System.Windows.Forms.PictureBox();
            this.EditBox = new NSE_Framework.Controls.Editor();
            this.LabelPal = new System.Windows.Forms.Label();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownPalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxA2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxA1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxBN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxFS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxFN)).BeginInit();
            this.SuspendLayout();
            // 
            // TopPanel
            // 
            this.TopPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TopPanel.Controls.Add(this.LabelPal);
            this.TopPanel.Controls.Add(this.NumericUpDownPalette);
            this.TopPanel.Controls.Add(this.PictureboxA2);
            this.TopPanel.Controls.Add(this.PictureboxA1);
            this.TopPanel.Controls.Add(this.groupBox1);
            this.TopPanel.Controls.Add(this.PictureboxBS);
            this.TopPanel.Controls.Add(this.PictureboxBN);
            this.TopPanel.Controls.Add(this.PictureboxFS);
            this.TopPanel.Controls.Add(this.PictureboxFN);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(535, 93);
            this.TopPanel.TabIndex = 1;
            // 
            // NumericUpDownPalette
            // 
            this.NumericUpDownPalette.Enabled = false;
            this.NumericUpDownPalette.Location = new System.Drawing.Point(336, 54);
            this.NumericUpDownPalette.Maximum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.NumericUpDownPalette.Name = "NumericUpDownPalette";
            this.NumericUpDownPalette.Size = new System.Drawing.Size(36, 20);
            this.NumericUpDownPalette.TabIndex = 8;
            this.NumericUpDownPalette.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumericUpDownPalette.ValueChanged += new System.EventHandler(this.NumericUpDownPalette_ValueChanged);
            // 
            // PictureboxA2
            // 
            this.PictureboxA2.BackgroundImage = global::PokemonPlugin.Properties.Resources.transparent;
            this.PictureboxA2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureboxA2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureboxA2.Location = new System.Drawing.Point(341, 11);
            this.PictureboxA2.Name = "PictureboxA2";
            this.PictureboxA2.Size = new System.Drawing.Size(34, 34);
            this.PictureboxA2.TabIndex = 6;
            this.PictureboxA2.TabStop = false;
            this.PictureboxA2.Click += new System.EventHandler(this.PictureboxA2_Click);
            // 
            // PictureboxA1
            // 
            this.PictureboxA1.BackgroundImage = global::PokemonPlugin.Properties.Resources.transparent;
            this.PictureboxA1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureboxA1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureboxA1.Location = new System.Drawing.Point(301, 11);
            this.PictureboxA1.Name = "PictureboxA1";
            this.PictureboxA1.Size = new System.Drawing.Size(34, 34);
            this.PictureboxA1.TabIndex = 5;
            this.PictureboxA1.TabStop = false;
            this.PictureboxA1.Click += new System.EventHandler(this.PictureboxA1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Label4);
            this.groupBox1.Controls.Add(this.IndexText);
            this.groupBox1.Controls.Add(this.ButtonNext);
            this.groupBox1.Controls.Add(this.ButtonPrev);
            this.groupBox1.Location = new System.Drawing.Point(383, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(139, 57);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Navigator";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label4.Location = new System.Drawing.Point(14, 26);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(33, 13);
            this.Label4.TabIndex = 16;
            this.Label4.Text = "Index";
            // 
            // IndexText
            // 
            this.IndexText.BackColor = System.Drawing.SystemColors.Window;
            this.IndexText.Location = new System.Drawing.Point(70, 22);
            this.IndexText.MaxLength = 4;
            this.IndexText.Name = "IndexText";
            this.IndexText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.IndexText.Size = new System.Drawing.Size(35, 20);
            this.IndexText.TabIndex = 13;
            this.IndexText.Text = "0";
            this.IndexText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.IndexText.TextChanged += new System.EventHandler(this.IndexText_TextChanged);
            this.IndexText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DigitKeyPress);
            // 
            // ButtonNext
            // 
            this.ButtonNext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonNext.Location = new System.Drawing.Point(108, 24);
            this.ButtonNext.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ButtonNext.Size = new System.Drawing.Size(17, 17);
            this.ButtonNext.TabIndex = 15;
            this.ButtonNext.Text = ">";
            this.ButtonNext.UseVisualStyleBackColor = true;
            this.ButtonNext.Click += new System.EventHandler(this.ButtonNext_Click);
            // 
            // ButtonPrev
            // 
            this.ButtonPrev.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonPrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPrev.Location = new System.Drawing.Point(50, 24);
            this.ButtonPrev.Margin = new System.Windows.Forms.Padding(0);
            this.ButtonPrev.Name = "ButtonPrev";
            this.ButtonPrev.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ButtonPrev.Size = new System.Drawing.Size(17, 17);
            this.ButtonPrev.TabIndex = 14;
            this.ButtonPrev.Text = "<";
            this.ButtonPrev.UseVisualStyleBackColor = true;
            this.ButtonPrev.Click += new System.EventHandler(this.ButtonPrev_Click);
            // 
            // PictureboxBS
            // 
            this.PictureboxBS.BackgroundImage = global::PokemonPlugin.Properties.Resources.transparent;
            this.PictureboxBS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureboxBS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureboxBS.Location = new System.Drawing.Point(227, 11);
            this.PictureboxBS.Name = "PictureboxBS";
            this.PictureboxBS.Size = new System.Drawing.Size(66, 66);
            this.PictureboxBS.TabIndex = 3;
            this.PictureboxBS.TabStop = false;
            this.PictureboxBS.Click += new System.EventHandler(this.PictureboxBS_Click);
            // 
            // PictureboxBN
            // 
            this.PictureboxBN.BackgroundImage = global::PokemonPlugin.Properties.Resources.transparent;
            this.PictureboxBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureboxBN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureboxBN.Location = new System.Drawing.Point(155, 11);
            this.PictureboxBN.Name = "PictureboxBN";
            this.PictureboxBN.Size = new System.Drawing.Size(66, 66);
            this.PictureboxBN.TabIndex = 2;
            this.PictureboxBN.TabStop = false;
            this.PictureboxBN.Click += new System.EventHandler(this.PictureboxBN_Click);
            // 
            // PictureboxFS
            // 
            this.PictureboxFS.BackgroundImage = global::PokemonPlugin.Properties.Resources.transparent;
            this.PictureboxFS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureboxFS.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureboxFS.Location = new System.Drawing.Point(83, 11);
            this.PictureboxFS.Name = "PictureboxFS";
            this.PictureboxFS.Size = new System.Drawing.Size(66, 66);
            this.PictureboxFS.TabIndex = 1;
            this.PictureboxFS.TabStop = false;
            this.PictureboxFS.Click += new System.EventHandler(this.PictureboxFS_Click);
            // 
            // PictureboxFN
            // 
            this.PictureboxFN.BackgroundImage = global::PokemonPlugin.Properties.Resources.transparent;
            this.PictureboxFN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureboxFN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PictureboxFN.Location = new System.Drawing.Point(11, 11);
            this.PictureboxFN.Name = "PictureboxFN";
            this.PictureboxFN.Size = new System.Drawing.Size(66, 66);
            this.PictureboxFN.TabIndex = 0;
            this.PictureboxFN.TabStop = false;
            this.PictureboxFN.Click += new System.EventHandler(this.PictureboxFN_Click);
            // 
            // EditBox
            // 
            this.EditBox.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.EditBox.BrushStroke = 0;
            this.EditBox.CompressExportedSprites = true;
            this.EditBox.CurrentIndex = -1;
            this.EditBox.CurrentSprite = null;
            this.EditBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditBox.Location = new System.Drawing.Point(0, 93);
            this.EditBox.Mode = NSE_Framework.Controls.Editor.EditMode.Pencil;
            this.EditBox.Name = "EditBox";
            this.EditBox.RulersVisible = true;
            this.EditBox.SelectedColorIndex = ((byte)(0));
            this.EditBox.Size = new System.Drawing.Size(535, 252);
            this.EditBox.SpriteBackColor = true;
            this.EditBox.TabIndex = 0;
            this.EditBox.Zoom = 1;
            // 
            // LabelPal
            // 
            this.LabelPal.AutoSize = true;
            this.LabelPal.Location = new System.Drawing.Point(306, 56);
            this.LabelPal.Name = "LabelPal";
            this.LabelPal.Size = new System.Drawing.Size(28, 13);
            this.LabelPal.TabIndex = 9;
            this.LabelPal.Text = "Pal :";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 345);
            this.Controls.Add(this.EditBox);
            this.Controls.Add(this.TopPanel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pokemon Sprites Plugin";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.LoadForm);
            this.Enter += new System.EventHandler(this.Form1_Enter);
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownPalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxA2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxA1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxBN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxFS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureboxFN)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private NSE_Framework.Controls.Editor EditBox;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.PictureBox PictureboxBS;
        private System.Windows.Forms.PictureBox PictureboxBN;
        private System.Windows.Forms.PictureBox PictureboxFS;
        private System.Windows.Forms.PictureBox PictureboxFN;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox IndexText;
        internal System.Windows.Forms.Button ButtonNext;
        internal System.Windows.Forms.Button ButtonPrev;
        private System.Windows.Forms.PictureBox PictureboxA2;
        private System.Windows.Forms.PictureBox PictureboxA1;
        private System.Windows.Forms.NumericUpDown NumericUpDownPalette;
        private System.Windows.Forms.Label LabelPal;
    }
}

