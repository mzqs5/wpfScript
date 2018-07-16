using System;
using System.IO;
using System.Text;

namespace wpfclx
{
    public class Log
    {
        public static void log(string action, string errorMsg)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + @"Log\" + action + @"\";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var time = DateTime.Now;
            string fileFullPath = path + time.ToString("yyyy-MM-dd") + ".txt";
            StringBuilder str = new StringBuilder();
            str.Append("Time:    " + time.ToString() + "\r\n");
            str.Append("Action:  " + action + "\r\n");
            str.Append("Message: " + errorMsg + "\r\n");
            str.Append("-----------------------------------------------------------\r\n\r\n");
            StreamWriter sw;
            if (!File.Exists(fileFullPath))
            {
                sw = File.CreateText(fileFullPath);
            }
            else
            {
                sw = File.AppendText(fileFullPath);
            }
            sw.WriteLine(str.ToString());
            sw.Close();
        }
    }
}
