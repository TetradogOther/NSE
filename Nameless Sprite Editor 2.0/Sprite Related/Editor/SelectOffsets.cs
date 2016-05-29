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
    public partial class SelectOffsets : Form
    {
        List<int> offsets;

        public SelectOffsets(List<int> Offsets)
        {
            InitializeComponent();
            this.offsets = Offsets;

            CheckedList.Items.Clear();

            foreach (int i in Offsets)
            {
                CheckedList.Items.Add("0x" + i.ToString("X"),true);
            }
        }

        public List<int> SelectedOffsets
        {
            get
            {
                List<int> i = new List<int>();

                for (int x = 0; x < offsets.Count; x++)
                {
                    if (CheckedList.GetItemChecked(x) )
                    {
                        i.Add(offsets[x]);
                    }

                    
                }
                return i;
            }
        }

        private void ButtonRepoint_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure you want to cancel?\n\nNo pointers will be changed to reflect this change?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
        }

    }
}
