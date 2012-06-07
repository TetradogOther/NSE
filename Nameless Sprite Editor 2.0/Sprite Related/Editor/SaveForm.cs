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

    public partial class SaveForm : Form
    {
        int OrigonalOffset;
        byte[] Data;
        int OrigonalLength;
        NSE_Framework.Write write;

        public int SaveOffset = -1;

        public SaveForm(NSE_Framework.Write write, byte[] Data, int Offset)
        {

            InitializeComponent();

            if (Program.MainForm.Read.FileLength > 0x1000000)
            {
                TextBox1.MaxLength = 7;
            }

            CheckBoxAbort.Enabled = false;

            TextBox1.Text = Offset.ToString("X2");
            Label3.Text = Data.Length.ToString();

            this.OrigonalOffset = Offset;
            this.Data = Data;
            this.OrigonalLength = Data.Length;
            this.write = write;
        }

        public SaveForm(NSE_Framework.Write write, byte[] Data, int Offset, int OrigonalLength)
        {
            InitializeComponent();

            if (Program.MainForm.Read.FileLength > 0x1000000)
            {
                TextBox1.MaxLength = 7;
            }


            if (Offset != -1)
            {
                TextBox1.Text = Offset.ToString("X2");
            }
            else
            {
                NSE_Framework.Find find = new NSE_Framework.Find(Program.MainForm.Filename);
                int f = find.FindFreeSpace(0, Data.Length);
                if (f != -1)
                {
                    TextBox1.Text = f.ToString("X2");
                }
            }
            Label3.Text = OrigonalLength.ToString();

            this.OrigonalOffset = Offset;
            this.Data = Data;
            this.OrigonalLength = OrigonalLength;
            this.write = write;
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            if (int.Parse(TextBox1.Text, System.Globalization.NumberStyles.HexNumber) < Program.MainForm.Read.FileLength - 512)
            {

                this.SaveOffset = int.Parse(TextBox1.Text, System.Globalization.NumberStyles.HexNumber);



                if (Program.MainForm.SafetyRepointing == true && this.SaveOffset % 4 == 0 || Program.MainForm.SafetyRepointing == false)
                {

                    byte[] ExistingData = Program.MainForm.Read.ReadBytes(SaveOffset, Data.Length);

                    if (IsFreeSpace(ExistingData, SaveOffset, OrigonalOffset, OrigonalLength) == true || CheckBoxAbort.Checked == false)
                    {
                        write.WriteBytes(Data, this.SaveOffset);


                        Program.MainForm.LogWriter.LogMessage("Saved Data 0x" + OrigonalOffset.ToString("X") + " to 0x" + SaveOffset.ToString("X"));
                        

                        if (CheckBoxPointers.Checked == true && SaveOffset != OrigonalOffset && OrigonalOffset != -1)
                        {
                            byte[] _old = BitConverter.GetBytes(OrigonalOffset);
                            _old = new byte[] { _old[0], _old[1], _old[2], (byte)(_old[3] + 0x8) };

                            byte[] _new = BitConverter.GetBytes(this.SaveOffset);
                            _new = new byte[] { _new[0], _new[1], _new[2], (byte)(_new[3] + 0x8) };

                            NSE_Framework.Find find = new NSE_Framework.Find(Program.MainForm.Filename);
                            List<int> ReplacedOffsets = new List<int>();

                            if (Program.MainForm.AdvancedRepointing == false)
                            {
                                find.SearchAndReplace(_old, _new, out ReplacedOffsets);

                                if (ReplacedOffsets.Count != 0)
                                {

                                    string mes = "";
                                    foreach (int i in ReplacedOffsets)
                                    {
                                        mes = mes + " 0x" + i.ToString("X2");
                                    }

                                    MessageBox.Show(this, "Replaced the pointers at offsets:\n\n" + mes, "Notice.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                                    Program.MainForm.LogWriter.LogMessage("Replaced pointers at offsets: " + mes);
                                    this.Close();
                                }
                                else
                                {
                                    this.Close();
                                }
                            }
                            else
                            {
                                ReplacedOffsets = find.Search(_old);
                                if (ReplacedOffsets.Count != 0)
                                {
                                    NSE2.SelectOffsets so = new SelectOffsets(ReplacedOffsets);
                                    so.ShowDialog(this);
                                    if (so.DialogResult == System.Windows.Forms.DialogResult.OK)
                                    {
                                        ReplacedOffsets = so.SelectedOffsets;

                                        if (ReplacedOffsets.Count != 0)
                                        {
                                            foreach (int o in ReplacedOffsets)
                                            {
                                                write.WriteBytes(_new, o);
                                            }

                                            string mes = "";
                                            foreach (int i in ReplacedOffsets)
                                            {
                                                mes = mes + " 0x" + i.ToString("X2");
                                            }

                                            MessageBox.Show(this, "Replaced the pointers at offsets:\n\n" + mes, "Notice.", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                                            Program.MainForm.LogWriter.LogMessage("Replaced pointers at offsets: " + mes);

                                            this.Close();
                                        }
                                        else
                                        {
                                            this.Close();
                                        }
                                    }
                                    else if (so.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                                    {
                                        this.Close();
                                    }

                                }
                                else
                                {
                                    this.Close();
                                }

                            }
                        }
                        else
                        {
                            this.Close();
                        }

                    }
                    else
                    {
                        MessageBox.Show(this, Data.Length.ToString() + " bytes is too long for this offset, Aborting", "Abort", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        SaveOffset = -1;
                    }

                }
                else
                {
                    MessageBox.Show(this, "Aborted:\n\t0x" + this.SaveOffset.ToString("X") + " is not a PointerSafe offset.\nPointerSafe offsets end in 0, 4, 8, or C.\n\nTo disable SafetyRepointing, un-check Options>Safety Repointing.", "Safety Repointing", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
