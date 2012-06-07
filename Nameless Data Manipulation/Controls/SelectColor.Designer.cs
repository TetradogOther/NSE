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
            this.Colors = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Colors)).BeginInit();
            this.SuspendLayout();
            // 
            // Colors
            // 
            this.Colors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Colors.Location = new System.Drawing.Point(0, 0);
            this.Colors.Name = "Colors";
            this.Colors.Size = new System.Drawing.Size(256, 256);
            this.Colors.TabIndex = 1;
            this.Colors.TabStop = false;
            this.Colors.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Colors_MouseDown);
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
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SelectColor_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SelectColor_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.Colors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Colors;
    }
}
