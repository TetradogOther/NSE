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

    public partial class InsertForm : Form
    {
        byte[] Data;
        NSE_Framework.Write write;

        public int SaveOffset = -1;

        public InsertForm(NSE_Framework.Write write, byte[] Data)
        {

            InitializeComponent();

            if (Program.MainForm.Read.FileLength > 0x1000000)
            {
                TextBox1.MaxLength = 7;
            }

            NSE_Framework.Find find = new NSE_Framework.Find(Program.MainForm.Filename);
            TextBox1.Text = find.FindFreeSpace(0X800000, Data.Length, true).ToString("X2");
            Label3.Text = Data.Length.ToString();

            this.Data = Data;
            this.write = write;
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            if (int.Parse(TextBox1.Text, System.Globalization.NumberStyles.HexNumber) + Data.Length < Program.MainForm.Read.FileLength - 512)
            {

                this.SaveOffset = int.Parse(TextBox1.Text, System.Globalization.NumberStyles.HexNumber);



                if (Program.MainForm.SafetyRepointing == true && this.SaveOffset % 4 == 0 || Program.MainForm.SafetyRepointing == false)
                {            
                    byte[] ExistingData = Program.MainForm.Read.ReadBytes(SaveOffset, Data.Length);

                    if (IsFreeSpace(ExistingData, SaveOffset) == true || CheckBoxAbort.Checked == false)
                    {
                        write.WriteBytes(Data, this.SaveOffset);
                        MessageBox.Show(this, "Inserted data at offset 0x" + this.SaveOffset.ToString("X"), "Success:", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Program.MainForm.LogWriter.LogMessage("Inserted Data at 0x" + SaveOffset.ToString("X"));
                        this.Close();
                        

                    }
                    else
                    {
                        MessageBox.Show(this, Data.Length.ToString() + " bytes is too long for this offset, Aborting", "Abort", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SaveOffset = -1;
                    }
                }
                else
                {
                    MessageBox.Show(this, "Aborted:\n\t0x" + this.SaveOffset.ToString("X") + " is not a PointerSafe offset.\nPointerSafe offsets are multiples of 4 (ex. ending in 0, 4, 8, or C).\n\nTo disable SafetyRepointing, un-check Options>Safety Repointing.", "Abort:", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                TextBox1.Text = (Program.MainForm.Read.FileLength - 513).ToString("X");
            }
           
        }

        bool IsFreeSpace(byte[] Data,int Offset, int OldOffset, int OldLength)
        {
            if(Offset + Data.Length > Program.MainForm.Read.FileLength - 513)
            {
                return false;
            }

            for(int i = 0; i < Data.Length; i++)
            {
                if (Data[i] != 0xff)
                {
                    if(Offset + i < OldOffset || Offset +i > OldOffset + OldLength || OldOffset == -1)
                        return false;
                }
            }
            return true;
        }

        bool IsFreeSpace(byte[] Data, int Offset)
        {
            if (Offset + Data.Length > Program.MainForm.Read.FileLength - 513)
            {
                return false;
            }

            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] != 0xff)
                {
                    return false;
                }
            }
            return true;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           NSE_Framework.Find find = new NSE_Framework.Find(Program.MainForm.Filename);
           int f = find.FindFreeSpace(int.Parse(TextBox1.Text, System.Globalization.NumberStyles.HexNumber), Data.Length, Program.MainForm.SafetyRepointing);
           if (f != -1)
           {
               TextBox1.Text = f.ToString("X2");
           }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (TextBox1.Text.Length > 0)
            {
                if (int.Parse(TextBox1.Text, System.Globalization.NumberStyles.HexNumber) > Program.MainForm.Read.FileLength - 513)
                {
                    TextBox1.Text = (Program.MainForm.Read.FileLength - 513).ToString("X");
                }
            }
        }
    }
}
