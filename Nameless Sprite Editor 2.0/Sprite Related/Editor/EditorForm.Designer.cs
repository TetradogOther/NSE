namespace NSE2
{
    partial class EditorForm
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("No Sprite Library loaded");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditorForm));
            this.LibraryTree = new System.Windows.Forms.TreeView();
            this.SidePanel = new System.Windows.Forms.Panel();
            this.ToolStripBookMark = new System.Windows.Forms.ToolStrip();
            this.ToolStripAddBookMark = new System.Windows.Forms.ToolStripButton();
            this.ToolStripAddFolder = new System.Windows.Forms.ToolStripButton();
            this.Seperator = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripDelete = new System.Windows.Forms.ToolStripButton();
            this.Editor = new NSE_Framework.Controls.Editor();
            this.SidePanel.SuspendLayout();
            this.ToolStripBookMark.SuspendLayout();
            this.SuspendLayout();
            // 
            // LibraryTree
            // 
            this.LibraryTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.LibraryTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LibraryTree.HideSelection = false;
            this.LibraryTree.LabelEdit = true;
            this.LibraryTree.Location = new System.Drawing.Point(0, 25);
            this.LibraryTree.Name = "LibraryTree";
            treeNode1.Name = "Node0";
            treeNode1.Text = "No Sprite Library loaded";
            this.LibraryTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.LibraryTree.Size = new System.Drawing.Size(187, 239);
            this.LibraryTree.TabIndex = 5;
            this.LibraryTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.LibraryTree_AfterLabelEdit);
            this.LibraryTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LibraryTree_AfterSelect);
            // 
            // SidePanel
            // 
            this.SidePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SidePanel.Controls.Add(this.LibraryTree);
            this.SidePanel.Controls.Add(this.ToolStripBookMark);
            this.SidePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.SidePanel.Location = new System.Drawing.Point(283, 0);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(189, 266);
            this.SidePanel.TabIndex = 44;
            this.SidePanel.Visible = false;
            // 
            // ToolStripBookMark
            // 
            this.ToolStripBookMark.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolStripBookMark.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripAddBookMark,
            this.ToolStripAddFolder,
            this.Seperator,
            this.ToolStripDelete});
            this.ToolStripBookMark.Location = new System.Drawing.Point(0, 0);
            this.ToolStripBookMark.Name = "ToolStripBookMark";
            this.ToolStripBookMark.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ToolStripBookMark.Size = new System.Drawing.Size(187, 25);
            this.ToolStripBookMark.TabIndex = 43;
            this.ToolStripBookMark.Text = "toolStrip1";
            // 
            // ToolStripAddBookMark
            // 
            this.ToolStripAddBookMark.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripAddBookMark.Image = global::NSE2.Properties.Resources.NewStar;
            this.ToolStripAddBookMark.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripAddBookMark.Name = "ToolStripAddBookMark";
            this.ToolStripAddBookMark.Size = new System.Drawing.Size(23, 22);
            this.ToolStripAddBookMark.Text = "Add Frame";
            this.ToolStripAddBookMark.Click += new System.EventHandler(this.ToolStripAddBookMark_Click);
            // 
            // ToolStripAddFolder
            // 
            this.ToolStripAddFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripAddFolder.Image = global::NSE2.Properties.Resources.NewFolder;
            this.ToolStripAddFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripAddFolder.Name = "ToolStripAddFolder";
            this.ToolStripAddFolder.Size = new System.Drawing.Size(23, 22);
            this.ToolStripAddFolder.Text = "Add SpriteSet";
            this.ToolStripAddFolder.Click += new System.EventHandler(this.ToolStripAddFolder_Click);
            // 
            // Seperator
            // 
            this.Seperator.Name = "Seperator";
            this.Seperator.Size = new System.Drawing.Size(6, 25);
            // 
            // ToolStripDelete
            // 
            this.ToolStripDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripDelete.Image = global::NSE2.Properties.Resources.Delete;
            this.ToolStripDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripDelete.Name = "ToolStripDelete";
            this.ToolStripDelete.Size = new System.Drawing.Size(23, 22);
            this.ToolStripDelete.Text = "Delete";
            this.ToolStripDelete.Click += new System.EventHandler(this.ToolStripDelete_Click);
            // 
            // Editor
            // 
            this.Editor.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.Editor.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.Editor.BrushStroke = 0;
            this.Editor.CompressExportedSprites = true;
            this.Editor.CurrentIndex = -1;
            this.Editor.CurrentSprite = null;
            this.Editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Editor.Location = new System.Drawing.Point(0, 0);
            this.Editor.Mode = NSE_Framework.Controls.Editor.EditMode.Pencil;
            this.Editor.Name = "Editor";
            this.Editor.RulersVisible = true;
            this.Editor.SelectedColorIndex = ((byte)(0));
            this.Editor.Size = new System.Drawing.Size(283, 266);
            this.Editor.SpriteBackColor = true;
            this.Editor.TabIndex = 0;
            this.Editor.Zoom = 1;
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(472, 266);
            this.Controls.Add(this.Editor);
            this.Controls.Add(this.SidePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditorForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.SidePanel.ResumeLayout(false);
            this.SidePanel.PerformLayout();
            this.ToolStripBookMark.ResumeLayout(false);
            this.ToolStripBookMark.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public NSE_Framework.Controls.Editor Editor;
        private System.Windows.Forms.TreeView LibraryTree;
        private System.Windows.Forms.Panel SidePanel;
        private System.Windows.Forms.ToolStrip ToolStripBookMark;
        private System.Windows.Forms.ToolStripButton ToolStripAddBookMark;
        private System.Windows.Forms.ToolStripButton ToolStripAddFolder;
        private System.Windows.Forms.ToolStripButton ToolStripDelete;
        private System.Windows.Forms.ToolStripSeparator Seperator;


    }
}