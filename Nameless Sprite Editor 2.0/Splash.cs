using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSE2
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
            Version.Text = "Version: " + Program.Version;
        }

        private void Close_CheckedChanged(object sender, EventArgs e)
        {
            if (CloseCheck.Checked == true)
                this.Visible = false;

        }

        private void Navigate_Click(object sender, EventArgs e)
        {
            if (Program.Navigate.IsDisposed == true)
            {
                Program.Navigate = new Navigate();
            }

            if (Program.Navigate.Visible == false)
            {
                Program.Navigate.Show(Program.MainForm);
            }
        }

        private void LoadRom_Click(object sender, EventArgs e)
        {
            Program.MainForm.LoadRomClick(sender, e);
        }

        private void NewSprite_Click(object sender, EventArgs e)
        {
            Program.MainForm.newSprite();
        }

        private void OpenSrite_Click(object sender, EventArgs e)
        {
            Program.MainForm.OpenSpriteClick(sender, e);
        }







    }
}
