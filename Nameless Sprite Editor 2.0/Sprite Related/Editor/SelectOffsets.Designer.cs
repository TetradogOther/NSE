namespace NSE2
{
    partial class SelectOffsets
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
            this.CheckedList = new System.Windows.Forms.CheckedListBox();
            this.ButtonRepoint = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CheckedList
            // 
            this.CheckedList.FormattingEnabled = true;
            this.CheckedList.Location = new System.Drawing.Point(12, 12);
            this.CheckedList.Name = "CheckedList";
            this.CheckedList.Size = new System.Drawing.Size(259, 259);
            this.CheckedList.TabIndex = 0;
            // 
            // ButtonRepoint
            // 
            this.ButtonRepoint.Location = new System.Drawing.Point(12, 277);
            this.ButtonRepoint.Name = "ButtonRepoint";
            this.ButtonRepoint.Size = new System.Drawing.Size(128, 23);
            this.ButtonRepoint.TabIndex = 1;
            this.ButtonRepoint.Text = "Repoint";
            this.ButtonRepoint.UseVisualStyleBackColor = true;
            this.ButtonRepoint.Click += new System.EventHandler(this.ButtonRepoint_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Location = new System.Drawing.Point(143, 277);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(128, 23);
            this.ButtonCancel.TabIndex = 2;
            this.ButtonCancel.Text = "Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // SelectOffsets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 311);
            this.ControlBox = false;
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonRepoint);
            this.Controls.Add(this.CheckedList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectOffsets";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Select Offsets to Repoint";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox CheckedList;
        private System.Windows.Forms.Button ButtonRepoint;
        private System.Windows.Forms.Button ButtonCancel;
    }
}