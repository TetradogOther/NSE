namespace NSE2
{
    partial class UserPreferences
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
            this.ButtonChoose = new System.Windows.Forms.Button();
            this.ListBoxBookMarks = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // ButtonChoose
            // 
            this.ButtonChoose.Location = new System.Drawing.Point(12, 230);
            this.ButtonChoose.Name = "ButtonChoose";
            this.ButtonChoose.Size = new System.Drawing.Size(235, 23);
            this.ButtonChoose.TabIndex = 0;
            this.ButtonChoose.Text = "Choose";
            this.ButtonChoose.UseVisualStyleBackColor = true;
            this.ButtonChoose.Click += new System.EventHandler(this.ButtonChoose_Click);
            // 
            // ListBoxBookMarks
            // 
            this.ListBoxBookMarks.FormattingEnabled = true;
            this.ListBoxBookMarks.Location = new System.Drawing.Point(12, 12);
            this.ListBoxBookMarks.Name = "ListBoxBookMarks";
            this.ListBoxBookMarks.Size = new System.Drawing.Size(235, 212);
            this.ListBoxBookMarks.TabIndex = 1;
            // 
            // UserPreferences
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 262);
            this.Controls.Add(this.ListBoxBookMarks);
            this.Controls.Add(this.ButtonChoose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserPreferences";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose a BookMark file :";
            this.Load += new System.EventHandler(this.UserPreferences_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonChoose;
        private System.Windows.Forms.ListBox ListBoxBookMarks;

    }
}