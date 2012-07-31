namespace NSE_Framework.Controls
{
    partial class SelectColor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Colors = new System.Windows.Forms.PictureBox();
            this.paletteMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.swapColors = new System.Windows.Forms.ToolStripMenuItem();
            this.switchColors = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.Colors)).BeginInit();
            this.paletteMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // Colors
            // 
            this.Colors.ContextMenuStrip = this.paletteMenuStrip;
            this.Colors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Colors.Location = new System.Drawing.Point(0, 0);
            this.Colors.Name = "Colors";
            this.Colors.Size = new System.Drawing.Size(256, 256);
            this.Colors.TabIndex = 1;
            this.Colors.TabStop = false;
            this.Colors.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Colors_MouseDown);
            // 
            // paletteMenuStrip
            // 
            this.paletteMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.swapColors,
            this.switchColors});
            this.paletteMenuStrip.Name = "paletteMenuStrip";
            this.paletteMenuStrip.Size = new System.Drawing.Size(147, 48);
            // 
            // swapColors
            // 
            this.swapColors.Name = "swapColors";
            this.swapColors.Size = new System.Drawing.Size(146, 22);
            this.swapColors.Text = "Swap Colors";
            this.swapColors.Click += new System.EventHandler(this.swapColors_Click);
            // 
            // switchColors
            // 
            this.switchColors.Name = "switchColors";
            this.switchColors.Size = new System.Drawing.Size(146, 22);
            this.switchColors.Text = "Switch Colors";
            this.switchColors.Click += new System.EventHandler(this.switchColors_Click);
            // 
            // SelectColor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImage = global::NSE_Framework.Properties.Resources.transparent;
            this.Controls.Add(this.Colors);
            this.Name = "SelectColor";
            this.Size = new System.Drawing.Size(256, 256);
            ((System.ComponentModel.ISupportInitialize)(this.Colors)).EndInit();
            this.paletteMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Colors;
        private System.Windows.Forms.ContextMenuStrip paletteMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem switchColors;
        private System.Windows.Forms.ToolStripMenuItem swapColors;
    }
}
