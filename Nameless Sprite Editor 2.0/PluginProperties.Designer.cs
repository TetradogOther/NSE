namespace NSE2
{
    partial class PluginProperties
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
            this.components = new System.ComponentModel.Container();
            this.PluginList = new System.Windows.Forms.ListView();
            this.PluginName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Version = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Author = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Classification = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ListMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.launchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PanelBottom = new System.Windows.Forms.Panel();
            this.LabelNote = new System.Windows.Forms.Label();
            this.ListMenuStrip.SuspendLayout();
            this.PanelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // PluginList
            // 
            this.PluginList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.PluginName,
            this.Version,
            this.Author,
            this.Classification,
            this.Type});
            this.PluginList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginList.Location = new System.Drawing.Point(10, 10);
            this.PluginList.Name = "PluginList";
            this.PluginList.Size = new System.Drawing.Size(438, 238);
            this.PluginList.TabIndex = 3;
            this.PluginList.UseCompatibleStateImageBehavior = false;
            this.PluginList.View = System.Windows.Forms.View.Details;
            // 
            // PluginName
            // 
            this.PluginName.Text = "Plugin";
            this.PluginName.Width = 108;
            // 
            // Version
            // 
            this.Version.Text = "Version";
            this.Version.Width = 57;
            // 
            // Author
            // 
            this.Author.Text = "Author";
            this.Author.Width = 67;
            // 
            // Classification
            // 
            this.Classification.Text = "Classification";
            this.Classification.Width = 103;
            // 
            // Type
            // 
            this.Type.Text = "Type";
            this.Type.Width = 95;
            // 
            // ListMenuStrip
            // 
            this.ListMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.launchToolStripMenuItem});
            this.ListMenuStrip.Name = "ListMenuStrip";
            this.ListMenuStrip.Size = new System.Drawing.Size(114, 26);
            // 
            // launchToolStripMenuItem
            // 
            this.launchToolStripMenuItem.Name = "launchToolStripMenuItem";
            this.launchToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.launchToolStripMenuItem.Text = "Launch";
            // 
            // PanelBottom
            // 
            this.PanelBottom.Controls.Add(this.LabelNote);
            this.PanelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PanelBottom.Location = new System.Drawing.Point(10, 248);
            this.PanelBottom.Name = "PanelBottom";
            this.PanelBottom.Size = new System.Drawing.Size(438, 41);
            this.PanelBottom.TabIndex = 4;
            // 
            // LabelNote
            // 
            this.LabelNote.AutoSize = true;
            this.LabelNote.Location = new System.Drawing.Point(59, 7);
            this.LabelNote.Name = "LabelNote";
            this.LabelNote.Size = new System.Drawing.Size(321, 26);
            this.LabelNote.TabIndex = 0;
            this.LabelNote.Text = "Note: This page displays all loaded plugins,\r\nbut only shows the information that" +
    " each individual plugin provides.";
            this.LabelNote.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // PluginProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 289);
            this.Controls.Add(this.PluginList);
            this.Controls.Add(this.PanelBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PluginProperties";
            this.Padding = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Plugin Properties";
            this.Load += new System.EventHandler(this.PluginManager_Load);
            this.ListMenuStrip.ResumeLayout(false);
            this.PanelBottom.ResumeLayout(false);
            this.PanelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView PluginList;
        private System.Windows.Forms.ColumnHeader PluginName;
        private System.Windows.Forms.ColumnHeader Version;
        private System.Windows.Forms.ColumnHeader Author;
        private System.Windows.Forms.ColumnHeader Classification;
        private System.Windows.Forms.ColumnHeader Type;
        private System.Windows.Forms.ContextMenuStrip ListMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem launchToolStripMenuItem;
        private System.Windows.Forms.Panel PanelBottom;
        private System.Windows.Forms.Label LabelNote;
    }
}