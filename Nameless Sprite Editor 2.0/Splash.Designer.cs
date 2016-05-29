using System;
namespace NSE2
{
    partial class Splash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash));
            this.BorderPanel = new System.Windows.Forms.Panel();
            this.OpenSrite = new System.Windows.Forms.Button();
            this.EditGroup = new System.Windows.Forms.GroupBox();
            this.Scroll_Edit = new System.Windows.Forms.Panel();
            this.Navigate = new System.Windows.Forms.Button();
            this.ModifyGroup = new System.Windows.Forms.GroupBox();
            this.Scroll_Modify = new System.Windows.Forms.Panel();
            this.HexEditor = new System.Windows.Forms.Button();
            this.LoadRom = new System.Windows.Forms.Button();
            this.CreateGroup = new System.Windows.Forms.GroupBox();
            this.Scroll_Create = new System.Windows.Forms.Panel();
            this.NewSprite = new System.Windows.Forms.Button();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.CloseCheck = new System.Windows.Forms.CheckBox();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.Version = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.NSE_Icon = new System.Windows.Forms.PictureBox();
            this.BorderPanel.SuspendLayout();
            this.EditGroup.SuspendLayout();
            this.Scroll_Edit.SuspendLayout();
            this.ModifyGroup.SuspendLayout();
            this.Scroll_Modify.SuspendLayout();
            this.CreateGroup.SuspendLayout();
            this.Scroll_Create.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            this.TopPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NSE_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // BorderPanel
            // 
            this.BorderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BorderPanel.Controls.Add(this.OpenSrite);
            this.BorderPanel.Controls.Add(this.EditGroup);
            this.BorderPanel.Controls.Add(this.ModifyGroup);
            this.BorderPanel.Controls.Add(this.LoadRom);
            this.BorderPanel.Controls.Add(this.CreateGroup);
            this.BorderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BorderPanel.Location = new System.Drawing.Point(0, 46);
            this.BorderPanel.Name = "BorderPanel";
            this.BorderPanel.Size = new System.Drawing.Size(484, 176);
            this.BorderPanel.TabIndex = 2;
            // 
            // OpenSrite
            // 
            this.OpenSrite.Location = new System.Drawing.Point(339, 6);
            this.OpenSrite.Name = "OpenSrite";
            this.OpenSrite.Size = new System.Drawing.Size(145, 23);
            this.OpenSrite.TabIndex = 1;
            this.OpenSrite.Text = "Open Sprite";
            this.OpenSrite.UseVisualStyleBackColor = true;
            this.OpenSrite.Click += new System.EventHandler(this.OpenSrite_Click);
            // 
            // EditGroup
            // 
            this.EditGroup.Controls.Add(this.Scroll_Edit);
            this.EditGroup.Enabled = false;
            this.EditGroup.Location = new System.Drawing.Point(170, 39);
            this.EditGroup.Name = "EditGroup";
            this.EditGroup.Size = new System.Drawing.Size(157, 137);
            this.EditGroup.TabIndex = 9;
            this.EditGroup.TabStop = false;
            this.EditGroup.Text = "Edit Sprites";
            // 
            // Scroll_Edit
            // 
            this.Scroll_Edit.AutoScroll = true;
            this.Scroll_Edit.Controls.Add(this.Navigate);
            this.Scroll_Edit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Scroll_Edit.Location = new System.Drawing.Point(3, 16);
            this.Scroll_Edit.Name = "Scroll_Edit";
            this.Scroll_Edit.Size = new System.Drawing.Size(151, 118);
            this.Scroll_Edit.TabIndex = 8;
            // 
            // Navigate
            // 
            this.Navigate.Location = new System.Drawing.Point(3, 3);
            this.Navigate.Name = "Navigate";
            this.Navigate.Size = new System.Drawing.Size(125, 23);
            this.Navigate.TabIndex = 7;
            this.Navigate.TabStop = false;
            this.Navigate.Text = "Navigate";
            this.Navigate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Navigate.UseVisualStyleBackColor = true;
            this.Navigate.Click += new System.EventHandler(this.Navigate_Click);
            // 
            // ModifyGroup
            // 
            this.ModifyGroup.Controls.Add(this.Scroll_Modify);
            this.ModifyGroup.Enabled = false;
            this.ModifyGroup.Location = new System.Drawing.Point(333, 39);
            this.ModifyGroup.Name = "ModifyGroup";
            this.ModifyGroup.Size = new System.Drawing.Size(157, 137);
            this.ModifyGroup.TabIndex = 10;
            this.ModifyGroup.TabStop = false;
            this.ModifyGroup.Text = "Modify (Data)";
            // 
            // Scroll_Modify
            // 
            this.Scroll_Modify.AutoScroll = true;
            this.Scroll_Modify.Controls.Add(this.HexEditor);
            this.Scroll_Modify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Scroll_Modify.Location = new System.Drawing.Point(3, 16);
            this.Scroll_Modify.Name = "Scroll_Modify";
            this.Scroll_Modify.Size = new System.Drawing.Size(151, 118);
            this.Scroll_Modify.TabIndex = 1;
            // 
            // HexEditor
            // 
            this.HexEditor.Enabled = false;
            this.HexEditor.Location = new System.Drawing.Point(3, 3);
            this.HexEditor.Name = "HexEditor";
            this.HexEditor.Size = new System.Drawing.Size(125, 23);
            this.HexEditor.TabIndex = 0;
            this.HexEditor.TabStop = false;
            this.HexEditor.Text = "Hex Editor";
            this.HexEditor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.HexEditor.UseVisualStyleBackColor = true;
            // 
            // LoadRom
            // 
            this.LoadRom.Location = new System.Drawing.Point(13, 6);
            this.LoadRom.Name = "LoadRom";
            this.LoadRom.Size = new System.Drawing.Size(311, 23);
            this.LoadRom.TabIndex = 0;
            this.LoadRom.Text = "Load Rom";
            this.LoadRom.UseVisualStyleBackColor = true;
            this.LoadRom.Click += new System.EventHandler(this.LoadRom_Click);
            // 
            // CreateGroup
            // 
            this.CreateGroup.Controls.Add(this.Scroll_Create);
            this.CreateGroup.Location = new System.Drawing.Point(7, 39);
            this.CreateGroup.Name = "CreateGroup";
            this.CreateGroup.Size = new System.Drawing.Size(157, 137);
            this.CreateGroup.TabIndex = 8;
            this.CreateGroup.TabStop = false;
            this.CreateGroup.Text = "Create";
            // 
            // Scroll_Create
            // 
            this.Scroll_Create.AutoScroll = true;
            this.Scroll_Create.Controls.Add(this.NewSprite);
            this.Scroll_Create.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Scroll_Create.Location = new System.Drawing.Point(3, 16);
            this.Scroll_Create.Name = "Scroll_Create";
            this.Scroll_Create.Size = new System.Drawing.Size(151, 118);
            this.Scroll_Create.TabIndex = 6;
            // 
            // NewSprite
            // 
            this.NewSprite.Location = new System.Drawing.Point(3, 3);
            this.NewSprite.Name = "NewSprite";
            this.NewSprite.Size = new System.Drawing.Size(125, 23);
            this.NewSprite.TabIndex = 5;
            this.NewSprite.TabStop = false;
            this.NewSprite.Text = "New Sprite";
            this.NewSprite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NewSprite.UseVisualStyleBackColor = true;
            this.NewSprite.Click += new System.EventHandler(this.NewSprite_Click);
            // 
            // BottomPanel
            // 
            this.BottomPanel.BackgroundImage = global::NSE2.Properties.Resources.BackGround;
            this.BottomPanel.Controls.Add(this.CloseCheck);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 222);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(484, 20);
            this.BottomPanel.TabIndex = 12;
            // 
            // CloseCheck
            // 
            this.CloseCheck.AutoSize = true;
            this.CloseCheck.BackColor = System.Drawing.Color.Transparent;
            this.CloseCheck.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CloseCheck.ForeColor = System.Drawing.Color.White;
            this.CloseCheck.Location = new System.Drawing.Point(363, 2);
            this.CloseCheck.Name = "CloseCheck";
            this.CloseCheck.Size = new System.Drawing.Size(113, 17);
            this.CloseCheck.TabIndex = 4;
            this.CloseCheck.Text = "Hide This Window";
            this.CloseCheck.UseVisualStyleBackColor = false;
            this.CloseCheck.CheckedChanged += new System.EventHandler(this.Close_CheckedChanged);
            // 
            // TopPanel
            // 
            this.TopPanel.BackColor = System.Drawing.Color.Transparent;
            this.TopPanel.BackgroundImage = global::NSE2.Properties.Resources.BackGround;
            this.TopPanel.Controls.Add(this.Version);
            this.TopPanel.Controls.Add(this.Title);
            this.TopPanel.Controls.Add(this.NSE_Icon);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(484, 46);
            this.TopPanel.TabIndex = 11;
            // 
            // Version
            // 
            this.Version.BackColor = System.Drawing.Color.Transparent;
            this.Version.ForeColor = System.Drawing.Color.White;
            this.Version.Location = new System.Drawing.Point(340, 23);
            this.Version.Name = "Version";
            this.Version.Size = new System.Drawing.Size(145, 13);
            this.Version.TabIndex = 3;
            this.Version.Text = "Version";
            this.Version.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.ForeColor = System.Drawing.Color.White;
            this.Title.Location = new System.Drawing.Point(49, 15);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(201, 24);
            this.Title.TabIndex = 2;
            this.Title.Text = "Nameless Sprite Editor";
            // 
            // NSE_Icon
            // 
            this.NSE_Icon.BackColor = System.Drawing.Color.Transparent;
            this.NSE_Icon.Image = ((System.Drawing.Image)(resources.GetObject("NSE_Icon.Image")));
            this.NSE_Icon.Location = new System.Drawing.Point(11, 10);
            this.NSE_Icon.Name = "NSE_Icon";
            this.NSE_Icon.Size = new System.Drawing.Size(32, 31);
            this.NSE_Icon.TabIndex = 0;
            this.NSE_Icon.TabStop = false;
            // 
            // Splash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(484, 242);
            this.ControlBox = false;
            this.Controls.Add(this.BorderPanel);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.BottomPanel);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 280);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 280);
            this.Name = "Splash";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Start Page - Welcome to NSE";
            this.BorderPanel.ResumeLayout(false);
            this.EditGroup.ResumeLayout(false);
            this.Scroll_Edit.ResumeLayout(false);
            this.ModifyGroup.ResumeLayout(false);
            this.Scroll_Modify.ResumeLayout(false);
            this.CreateGroup.ResumeLayout(false);
            this.Scroll_Create.ResumeLayout(false);
            this.BottomPanel.ResumeLayout(false);
            this.BottomPanel.PerformLayout();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NSE_Icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox NSE_Icon;
        private System.Windows.Forms.Panel BorderPanel;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button NewSprite;
        private System.Windows.Forms.Button Navigate;
        private System.Windows.Forms.Button HexEditor;
        public System.Windows.Forms.Panel Scroll_Create;
        public System.Windows.Forms.Panel Scroll_Edit;
        public System.Windows.Forms.Panel Scroll_Modify;
        public System.Windows.Forms.Label Version;
        public System.Windows.Forms.CheckBox CloseCheck;
        public System.Windows.Forms.Button LoadRom;
        public System.Windows.Forms.Button OpenSrite;
        public System.Windows.Forms.GroupBox CreateGroup;
        public System.Windows.Forms.GroupBox EditGroup;
        public System.Windows.Forms.GroupBox ModifyGroup;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel BottomPanel;
    }
}