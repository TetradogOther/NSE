namespace ColorMatch
{
    partial class mainForm
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
            this.PictureBoxPalette = new System.Windows.Forms.PictureBox();
            this.ButtonImportPalette = new System.Windows.Forms.Button();
            this.ButtonColorMatch = new System.Windows.Forms.Button();
            this.PictureBoxSprite = new System.Windows.Forms.PictureBox();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.ScrollPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPalette)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxSprite)).BeginInit();
            this.ScrollPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // PictureBoxPalette
            // 
            this.PictureBoxPalette.BackgroundImage = global::ColorMatch.Properties.Resources.transparent;
            this.PictureBoxPalette.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBoxPalette.Location = new System.Drawing.Point(10, 9);
            this.PictureBoxPalette.Name = "PictureBoxPalette";
            this.PictureBoxPalette.Size = new System.Drawing.Size(34, 130);
            this.PictureBoxPalette.TabIndex = 0;
            this.PictureBoxPalette.TabStop = false;
            // 
            // ButtonImportPalette
            // 
            this.ButtonImportPalette.Location = new System.Drawing.Point(10, 224);
            this.ButtonImportPalette.Name = "ButtonImportPalette";
            this.ButtonImportPalette.Size = new System.Drawing.Size(75, 23);
            this.ButtonImportPalette.TabIndex = 1;
            this.ButtonImportPalette.Text = "Get Palette";
            this.ButtonImportPalette.UseVisualStyleBackColor = true;
            this.ButtonImportPalette.Click += new System.EventHandler(this.ButtonImportPalette_Click);
            // 
            // ButtonColorMatch
            // 
            this.ButtonColorMatch.Enabled = false;
            this.ButtonColorMatch.Location = new System.Drawing.Point(91, 224);
            this.ButtonColorMatch.Name = "ButtonColorMatch";
            this.ButtonColorMatch.Size = new System.Drawing.Size(82, 23);
            this.ButtonColorMatch.TabIndex = 2;
            this.ButtonColorMatch.Text = "Color Match";
            this.ButtonColorMatch.UseVisualStyleBackColor = true;
            this.ButtonColorMatch.Click += new System.EventHandler(this.ButtonColorMatch_Click);
            // 
            // PictureBoxSprite
            // 
            this.PictureBoxSprite.BackgroundImage = global::ColorMatch.Properties.Resources.transparent;
            this.PictureBoxSprite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBoxSprite.Location = new System.Drawing.Point(4, 3);
            this.PictureBoxSprite.Name = "PictureBoxSprite";
            this.PictureBoxSprite.Size = new System.Drawing.Size(66, 66);
            this.PictureBoxSprite.TabIndex = 3;
            this.PictureBoxSprite.TabStop = false;
            // 
            // ButtonSave
            // 
            this.ButtonSave.Enabled = false;
            this.ButtonSave.Location = new System.Drawing.Point(193, 224);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(75, 23);
            this.ButtonSave.TabIndex = 4;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // ScrollPanel
            // 
            this.ScrollPanel.AutoScroll = true;
            this.ScrollPanel.Controls.Add(this.PictureBoxSprite);
            this.ScrollPanel.Location = new System.Drawing.Point(50, 9);
            this.ScrollPanel.Name = "ScrollPanel";
            this.ScrollPanel.Size = new System.Drawing.Size(218, 203);
            this.ScrollPanel.TabIndex = 5;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 256);
            this.Controls.Add(this.ScrollPanel);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.ButtonColorMatch);
            this.Controls.Add(this.ButtonImportPalette);
            this.Controls.Add(this.PictureBoxPalette);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Color Match ALPHA : 4BPP ONLY";
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPalette)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxSprite)).EndInit();
            this.ScrollPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox PictureBoxPalette;
        private System.Windows.Forms.Button ButtonImportPalette;
        private System.Windows.Forms.Button ButtonColorMatch;
        private System.Windows.Forms.PictureBox PictureBoxSprite;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Panel ScrollPanel;
    }
}