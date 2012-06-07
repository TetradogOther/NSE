using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace NSE2
{
    public class LogWriter
    {
        string LogPath;
        StreamWriter logWriter;
        public LogWriter(string fileName)
        {
            if (fileName != null)
            {
                if (!Directory.Exists(Application.StartupPath + "\\Core\\Logs"))
                {
                    Directory.CreateDirectory(Application.StartupPath + "\\Core\\Logs");
                }
                if (logWriter != null)
                    logWriter.Close();

                LogPath = Application.StartupPath + "\\Core\\Logs\\" + fileName + ".txt";
            }
            else
            {
                LogPath = null;
            }
        }

        public void LogMessage(string Message, bool Date = true)
        {
            if ( LogPath != null)
            {
                logWriter = new StreamWriter(LogPath, true);

                if (Date)
                {
                    logWriter.WriteLine(DateTime.Now.ToString() + " : " + Message);
                }
                else
                {
                    logWriter.WriteLine(Message);
                }

                logWriter.Close();
            }
        }
    }
}
