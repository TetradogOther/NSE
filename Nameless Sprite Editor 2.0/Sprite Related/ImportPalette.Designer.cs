namespace NSE2
{
    partial class ImportPalette
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
            this.ColorsBox = new System.Windows.Forms.PictureBox();
            this.ButtonLoad = new System.Windows.Forms.Button();
            this.ButtonImport = new System.Windows.Forms.Button();
            this.ComboBoxMode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.ColorsBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ColorsBox
            // 
            this.ColorsBox.BackgroundImage = global::NSE2.Properties.Resources.transparent;
            this.ColorsBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ColorsBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ColorsBox.Location = new System.Drawing.Point(10, 12);
            this.ColorsBox.Name = "ColorsBox";
            this.ColorsBox.Size = new System.Drawing.Size(258, 258);
            this.ColorsBox.TabIndex = 0;
            this.ColorsBox.TabStop = false;
            this.ColorsBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorsBox_MouseDown);
            // 
            // ButtonLoad
            // 
            this.ButtonLoad.Location = new System.Drawing.Point(10, 276);
            this.ButtonLoad.Name = "ButtonLoad";
            this.ButtonLoad.Size = new System.Drawing.Size(65, 23);
            this.ButtonLoad.TabIndex = 1;
            this.ButtonLoad.Text = "Load";
            this.ButtonLoad.UseVisualStyleBackColor = true;
            this.ButtonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // ButtonImport
            // 
            this.ButtonImport.Enabled = false;
            this.ButtonImport.Location = new System.Drawing.Point(172, 276);
            this.ButtonImport.Name = "ButtonImport";
            this.ButtonImport.Size = new System.Drawing.Size(96, 23);
            this.ButtonImport.TabIndex = 2;
            this.ButtonImport.Text = "Import";
            this.ButtonImport.UseVisualStyleBackColor = true;
            this.ButtonImport.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // ComboBoxMode
            // 
            this.ComboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxMode.FormattingEnabled = true;
            this.ComboBoxMode.Items.AddRange(new object[] {
            "16 Color",
            "256 Color"});
            this.ComboBoxMode.Location = new System.Drawing.Point(81, 276);
            this.ComboBoxMode.Name = "ComboBoxMode";
            this.ComboBoxMode.Size = new System.Drawing.Size(85, 21);
            this.ComboBoxMode.TabIndex = 3;
            this.ComboBoxMode.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMode_SelectedIndexChanged);
            // 
            // ImportPalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 307);
            this.Controls.Add(this.ComboBoxMode);
            this.Controls.Add(this.ButtonImport);
            this.Controls.Add(this.ButtonLoad);
            this.Controls.Add(this.ColorsBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportPalette";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Import Color Table";
            ((System.ComponentModel.ISupportInitialize)(this.ColorsBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ColorsBox;
        private System.Windows.Forms.Button ButtonLoad;
        private System.Windows.Forms.Button ButtonImport;
        private System.Windows.Forms.ComboBox ComboBoxMode;
    }
}