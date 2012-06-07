namespace OWEditor
{
    partial class SpritePropertiesForm
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
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.TabPagePreferences = new System.Windows.Forms.TabPage();
            this.TabPageHex = new System.Windows.Forms.TabPage();
            this.MainTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.TabPagePreferences);
            this.MainTabControl.Controls.Add(this.TabPageHex);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(391, 232);
            this.MainTabControl.TabIndex = 0;
            // 
            // TabPagePreferences
            // 
            this.TabPagePreferences.Location = new System.Drawing.Point(4, 22);
            this.TabPagePreferences.Name = "TabPagePreferences";
            this.TabPagePreferences.Padding = new System.Windows.Forms.Padding(3);
            this.TabPagePreferences.Size = new System.Drawing.Size(383, 206);
            this.TabPagePreferences.TabIndex = 0;
            this.TabPagePreferences.Text = "Preferences";
            this.TabPagePreferences.UseVisualStyleBackColor = true;
            // 
            // TabPageHex
            // 
            this.TabPageHex.Location = new System.Drawing.Point(4, 22);
            this.TabPageHex.Name = "TabPageHex";
            this.TabPageHex.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageHex.Size = new System.Drawing.Size(383, 206);
            this.TabPageHex.TabIndex = 1;
            this.TabPageHex.Text = "Hex View";
            this.TabPageHex.UseVisualStyleBackColor = true;
            // 
            // SpritePropertiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(391, 232);
            this.Controls.Add(this.MainTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SpritePropertiesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Sprite Properties";
            this.MainTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage TabPagePreferences;
        private System.Windows.Forms.TabPage TabPageHex;
    }
}