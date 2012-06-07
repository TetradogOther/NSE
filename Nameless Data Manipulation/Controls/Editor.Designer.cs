namespace NSE_Framework.Controls
{
    partial class Editor
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
            this.EditorPanel = new System.Windows.Forms.Panel();
            this.RulerY = new System.Windows.Forms.PictureBox();
            this.RulerX = new System.Windows.Forms.PictureBox();
            this.EditBox = new NSE_Framework.Controls.ExtendedPictureBox();
            this.EditorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RulerY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RulerX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EditBox)).BeginInit();
            this.SuspendLayout();
            // 
            // EditorPanel
            // 
            this.EditorPanel.AllowDrop = true;
            this.EditorPanel.AutoScroll = true;
            this.EditorPanel.BackColor = System.Drawing.Color.Transparent;
            this.EditorPanel.Controls.Add(this.EditBox);
            this.EditorPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EditorPanel.Location = new System.Drawing.Point(15, 15);
            this.EditorPanel.Margin = new System.Windows.Forms.Padding(0);
            this.EditorPanel.Name = "EditorPanel";
            this.EditorPanel.Size = new System.Drawing.Size(445, 246);
            this.EditorPanel.TabIndex = 6;
            this.EditorPanel.MouseHover += new System.EventHandler(this.Editor_MouseHover);
            this.EditorPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Editor_MouseMove);
            // 
            // RulerY
            // 
            this.RulerY.BackColor = System.Drawing.Color.White;
            this.RulerY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.RulerY.Dock = System.Windows.Forms.DockStyle.Left;
            this.RulerY.Location = new System.Drawing.Point(0, 15);
            this.RulerY.Name = "RulerY";
            this.RulerY.Size = new System.Drawing.Size(15, 246);
            this.RulerY.TabIndex = 5;
            this.RulerY.TabStop = false;
            // 
            // RulerX
            // 
            this.RulerX.BackColor = System.Drawing.Color.White;
            this.RulerX.Dock = System.Windows.Forms.DockStyle.Top;
            this.RulerX.Location = new System.Drawing.Point(0, 0);
            this.RulerX.Name = "RulerX";
            this.RulerX.Size = new System.Drawing.Size(460, 15);
            this.RulerX.TabIndex = 4;
            this.RulerX.TabStop = false;
            // 
            // EditBox
            // 
            this.EditBox.BackColor = System.Drawing.Color.White;
            this.EditBox.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            this.EditBox.Location = new System.Drawing.Point(198, 99);
            this.EditBox.Margin = new System.Windows.Forms.Padding(5);
            this.EditBox.Name = "EditBox";
            this.EditBox.Size = new System.Drawing.Size(48, 48);
            this.EditBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.EditBox.TabIndex = 0;
            this.EditBox.TabStop = false;
            this.EditBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainMouseDown);
            this.EditBox.MouseHover += new System.EventHandler(this.Editor_MouseHover);
            this.EditBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainFunction);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Controls.Add(this.EditorPanel);
            this.Controls.Add(this.RulerY);
            this.Controls.Add(this.RulerX);
            this.Name = "Editor";
            this.Size = new System.Drawing.Size(460, 261);
            this.Load += new System.EventHandler(this.Editor_Load);
            this.LocationChanged += new System.EventHandler(this.Editor_LocationChanged);
            this.MouseHover += new System.EventHandler(this.Editor_MouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Editor_MouseMove);
            this.Resize += new System.EventHandler(this.Editor_Resize);
            this.EditorPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RulerY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RulerX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EditBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel EditorPanel;
        private System.Windows.Forms.PictureBox RulerY;
        private System.Windows.Forms.PictureBox RulerX;
        private ExtendedPictureBox EditBox;

    }
}
