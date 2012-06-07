namespace TestPlugin
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.selectColor1 = new NSE_Framework.Controls.SelectColor();
            this.editor1 = new NSE_Framework.Controls.Editor();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 142);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(350, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Color Calculator";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(347, 130);
            this.label1.TabIndex = 1;
            this.label1.Text = resources.GetString("label1.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TestPlugin.Properties.Resources.Ribbon;
            this.pictureBox1.Location = new System.Drawing.Point(12, 171);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(350, 19);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // selectColor1
            // 
            this.selectColor1.BackColor = System.Drawing.Color.Transparent;
            this.selectColor1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("selectColor1.BackgroundImage")));
            this.selectColor1.Index = ((byte)(0));
            this.selectColor1.Location = new System.Drawing.Point(61, 195);
            this.selectColor1.Name = "selectColor1";
            this.selectColor1.ShowBackground = true;
            this.selectColor1.Size = new System.Drawing.Size(256, 128);
            this.selectColor1.TabIndex = 4;
            // 
            // editor1
            // 
            this.editor1.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.editor1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.editor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.editor1.BrushStroke = 0;
            this.editor1.CompressExportedSprites = true;
            this.editor1.CurrentIndex = -1;
            this.editor1.CurrentSprite = null;
            this.editor1.Dock = System.Windows.Forms.DockStyle.Right;
            this.editor1.Location = new System.Drawing.Point(378, 0);
            this.editor1.Name = "editor1";
            this.editor1.RulersVisible = true;
            this.editor1.SelectedColorIndex = ((byte)(0));
            this.editor1.Size = new System.Drawing.Size(345, 326);
            this.editor1.SpriteBackColor = false;
            this.editor1.TabIndex = 3;
            this.editor1.Zoom = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::TestPlugin.Properties.Resources.BG;
            this.ClientSize = new System.Drawing.Size(723, 326);
            this.Controls.Add(this.selectColor1);
            this.Controls.Add(this.editor1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NSE Test Plugin";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Enter += new System.EventHandler(this.Form1_Enter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private NSE_Framework.Controls.Editor editor1;
        private NSE_Framework.Controls.SelectColor selectColor1;
    }
}

