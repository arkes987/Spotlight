using System;
using System.Diagnostics;
using System.IO;

namespace Spotlight.Models
{
    public static class Logger
    {
        public static void WriteLog(string message)
        {
            Debug.WriteLine(message);
            Console.WriteLine(message);
            LogToFile(message);
        }

        public static void WriteLog(Exception ex)
        {
            Debug.WriteLine(ex.Message);
            Console.WriteLine(ex.Message);
            LogToFile(ex.Message);
        }

        private static void LogToFile(string message)
        {
            var baseLocation = AppDomain.CurrentDomain.BaseDirectory;
            var dayLocation = baseLocation + "\\" + DateTime.Now.ToString("yyyy-MM-dd");

            if (!System.IO.Directory.Exists(dayLocation))
                System.IO.Directory.CreateDirectory(dayLocation);

            var fileLocation = dayLocation + "\\log.txt";

            using (StreamWriter sw = File.AppendText(fileLocation))
            {
                sw.WriteLine(DateTime.Now);
                sw.WriteLine(message);
            }
        }
    }
}
