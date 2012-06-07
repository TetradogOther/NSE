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
    public partial class PluginProperties : Form
    {
        public PluginProperties()
        {
            InitializeComponent();
        }

        private void PluginManager_Load(object sender, EventArgs e)
        {
            foreach (Plugins.IPlugin p in Program.MainForm.ipi1_0)
            {
                if (p != null)
                {
                    ListViewItem i = new ListViewItem(p.Name);
                    i.SubItems.Add(p.Version);
                    i.SubItems.Add(p.Author);
                    i.SubItems.Add(p.Classification.ToString());
                    i.SubItems.Add(p.Type.ToString());
                    PluginList.Items.Add(i);
                }
            }
        }
    }
}
