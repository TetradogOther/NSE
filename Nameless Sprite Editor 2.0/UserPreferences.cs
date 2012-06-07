using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NSE2
{
    public partial class UserPreferences : Form
    {
        public UserPreferences()
        {
            InitializeComponent();
        }

        string[] files;

        private void UserPreferences_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "\\Core\\BookMarks\\") == false)
            {
                Directory.CreateDirectory(Application.StartupPath + "\\Core\\BookMarks\\");
            }

            files = Directory.GetFiles(Application.StartupPath + "\\Core\\BookMarks\\");

            if (files.Length != 0)
            {
                foreach (string file in files)
                {
                    ListBoxBookMarks.Items.Add(Path.GetFileNameWithoutExtension(file));
                }
                if (ListBoxBookMarks.Items.Contains(Program.MainForm.BookMarkFile) == true)
                {
                    bool found = false;
                    int i = 0;
                    while (found == false && i < ListBoxBookMarks.Items.Count)
                    {
                        if (files[i].Contains(Program.MainForm.BookMarkFile + ".nbmx"))
                        {
                            found = true;
                            ListBoxBookMarks.SelectedIndex = i;
                        }
                        i++;
                    }
                    if (found == false)
                    {
                        ListBoxBookMarks.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                ListBoxBookMarks.Items.Add("(none)");
                ListBoxBookMarks.SelectedIndex = 0;
                ButtonChoose.Enabled = false;
            }
        }

        private void ButtonChoose_Click(object sender, EventArgs e)
        {
            Program.MainForm.BookMarkFile = ListBoxBookMarks.SelectedItem.ToString();

            if (File.Exists(Application.StartupPath + "\\Core\\BookMarks\\" + Program.MainForm.BookMarkFile + ".nbmx") == true)
            {
                Program.BookMarkTree = NSE_Framework.IO.Import.ImportBookMarkTree(Application.StartupPath + "\\Core\\BookMarks\\" + Program.MainForm.BookMarkFile + ".nbmx");
                Program.BookMarkTree.Name = Program.MainForm.BookMarkFile;
            }

            if (Program.Navigate.Visible == true)
            {
                Program.Navigate.Close();
            }

            this.Close();
        }
    }
}
