using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NSE2
{
    public static class Program
    {
        public static NSE_Framework.IO.IniFile INI; 
        public static string Version = "v2.0 Beta 2.1";        
        public static MainForm MainForm;
        public static Navigate Navigate;
        public static NSE_Framework.IO.BookMarkTree BookMarkTree;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            INI = new NSE_Framework.IO.IniFile(Application.StartupPath + "\\Core\\Settings.ini");
            MainForm = new MainForm();
            
            if (args.Length == 1)
            {
                string exstension = System.IO.Path.GetExtension(args[0]).ToLower();
                if (exstension == ".gba" || exstension == ".agb" || exstension == ".bin")
                {
                    MainForm.LoadRom(args[0]);
                }
                else if (exstension == ".png" || exstension == ".bmp")
                {
                    MainForm.LoadImage(args[0]);
                }
                else if (exstension == ".nslx")
                {
                    MainForm.LoadSprite(args[0]);
                }
            }
            else if (args.Length > 1)
            {
                string exstension = System.IO.Path.GetExtension(args[0]).ToLower();
                if (exstension == ".gba" || exstension == ".agb" || exstension == ".bin")
                {
                    MainForm.LoadRom(args[0]);
                }
            }

            string lastUpdate = INI.IniReadValue("NSE", "LastUpdate");
            if (lastUpdate != DateTime.Today.Day.ToString() && lastUpdate.ToLower() != "disable")
            {

                try
                {
                    System.Net.WebClient client = new System.Net.WebClient();
                    String updateCode = client.DownloadString("http://www.lastlink.tk/Projects/Nameless Sprite Editor 2.X/version.html");
                    if (updateCode[0] == '#')
                    {
                        updateCode = updateCode.Substring(1, updateCode.IndexOf('!') - 1);

                        if (Program.Version != updateCode)
                        {
                            if (MessageBox.Show("An update is available for Nameless Sprite Editor 2.X.\nVersion name: " + updateCode + "\nGet the new version?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start("http://www.lastlink.tk/Projects/?p=Nameless Sprite Editor 2.X");
                            }
                        }

                        Program.INI.IniWriteValue("NSE", "LastUpdate", DateTime.Today.Day.ToString());
                    }
                }
                catch
                {
                    Program.INI.IniWriteValue("NSE", "LastUpdate", DateTime.Today.Day.ToString());
                }
            }

            Application.Run(Program.MainForm);
            
        }
    }
}
