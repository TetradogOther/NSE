namespace NSE2
{
    partial class Import
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
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("No Sprite Library loaded");
            this.ButtonLoad = new System.Windows.Forms.Button();
            this.ComboBoxMode = new System.Windows.Forms.ComboBox();
            this.Preview = new System.Windows.Forms.PictureBox();
            this.PaletteRep = new System.Windows.Forms.PictureBox();
            this.LibraryTree = new System.Windows.Forms.TreeView();
            this.LabelSize = new System.Windows.Forms.Label();
            this.ButtonSave = new System.Windows.Forms.Button();
            this.LabelColors = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaletteRep)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonLoad
            // 
            this.ButtonLoad.Location = new System.Drawing.Point(175, 169);
            this.ButtonLoad.Name = "ButtonLoad";
            this.ButtonLoad.Size = new System.Drawing.Size(105, 23);
            this.ButtonLoad.TabIndex = 2;
            this.ButtonLoad.Text = "Load Sprite";
            this.ButtonLoad.UseCompatibleTextRendering = true;
            this.ButtonLoad.UseVisualStyleBackColor = true;
            this.ButtonLoad.Click += new System.EventHandler(this.ButtonImport_Click);
            // 
            // ComboBoxMode
            // 
            this.ComboBoxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxMode.Enabled = false;
            this.ComboBoxMode.FormattingEnabled = true;
            this.ComboBoxMode.Items.AddRange(new object[] {
            "Image",
            "Palette",
            "Image + Palette"});
            this.ComboBoxMode.Location = new System.Drawing.Point(286, 170);
            this.ComboBoxMode.Name = "ComboBoxMode";
            this.ComboBoxMode.Size = new System.Drawing.Size(147, 21);
            this.ComboBoxMode.TabIndex = 3;
            // 
            // Preview
            // 
            this.Preview.BackgroundImage = global::NSE2.Properties.Resources.transparent;
            this.Preview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Preview.Location = new System.Drawing.Point(175, 3);
            this.Preview.MinimumSize = new System.Drawing.Size(256, 128);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(258, 162);
            this.Preview.TabIndex = 0;
            this.Preview.TabStop = false;
            // 
            // PaletteRep
            // 
            this.PaletteRep.BackgroundImage = global::NSE2.Properties.Resources.transparent;
            this.PaletteRep.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PaletteRep.Location = new System.Drawing.Point(437, 3);
            this.PaletteRep.Name = "PaletteRep";
            this.PaletteRep.Size = new System.Drawing.Size(130, 130);
            this.PaletteRep.TabIndex = 1;
            this.PaletteRep.TabStop = false;
            // 
            // LibraryTree
            // 
            this.LibraryTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LibraryTree.HideSelection = false;
            this.LibraryTree.Location = new System.Drawing.Point(8, 3);
            this.LibraryTree.Name = "LibraryTree";
            treeNode2.Name = "Node0";
            treeNode2.Text = "No Sprite Library loaded";
            this.LibraryTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.LibraryTree.Size = new System.Drawing.Size(161, 188);
            this.LibraryTree.TabIndex = 4;
            this.LibraryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LibraryTree_AfterSelect);
            // 
            // LabelSize
            // 
            this.LabelSize.AutoSize = true;
            this.LabelSize.Location = new System.Drawing.Point(440, 136);
            this.LabelSize.Name = "LabelSize";
            this.LabelSize.Size = new System.Drawing.Size(30, 13);
            this.LabelSize.TabIndex = 5;
            this.LabelSize.Text = "Size:";
            // 
            // ButtonSave
            // 
            this.ButtonSave.Enabled = false;
            this.ButtonSave.Location = new System.Drawing.Point(439, 169);
            this.ButtonSave.Name = "ButtonSave";
            this.ButtonSave.Size = new System.Drawing.Size(128, 23);
            this.ButtonSave.TabIndex = 6;
            this.ButtonSave.Text = "Save";
            this.ButtonSave.UseVisualStyleBackColor = true;
            this.ButtonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // LabelColors
            // 
            this.LabelColors.AutoSize = true;
            this.LabelColors.Location = new System.Drawing.Point(440, 152);
            this.LabelColors.Name = "LabelColors";
            this.LabelColors.Size = new System.Drawing.Size(39, 13);
            this.LabelColors.TabIndex = 7;
            this.LabelColors.Text = "Colors:";
            // 
            // Import
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 197);
            this.Controls.Add(this.LabelColors);
            this.Controls.Add(this.ButtonSave);
            this.Controls.Add(this.LabelSize);
            this.Controls.Add(this.LibraryTree);
            this.Controls.Add(this.Preview);
            this.Controls.Add(this.ComboBoxMode);
            this.Controls.Add(this.ButtonLoad);
            this.Controls.Add(this.PaletteRep);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Import";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 40);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Import";
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PaletteRep)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Preview;
        private System.Windows.Forms.PictureBox PaletteRep;
        private System.Windows.Forms.Button ButtonLoad;
        private System.Windows.Forms.ComboBox ComboBoxMode;
        private System.Windows.Forms.TreeView LibraryTree;
        private System.Windows.Forms.Label LabelSize;
        private System.Windows.Forms.Button ButtonSave;
        private System.Windows.Forms.Label LabelColors;
    }
}