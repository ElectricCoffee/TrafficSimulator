using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficSim.Util
{
    public static class Error
    {
        public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\error-log.txt";

        /// <summary>
        /// Writes an exception's message to a specified file
        /// </summary>
        /// <param name="ex">An Exception</param>
        /// <param name="path">The path to the file, must include file name</param>
        public static void Log(Exception ex, string path)
        {
            string
                innerMessage = ex.InnerException != null ? ex.InnerException.Message : "N/A",
                finalMessage = String.Format("{0}\n  Inner exception: {1}", ex.Message, innerMessage);

            Log(finalMessage, path);
        }

        /// <summary>
        /// Writes an exception's message to a file named "error-log.txt" on the desktop
        /// </summary>
        /// <param name="ex">An Exception</param>
        public static void Log(Exception ex)
        {
            Log(ex, DesktopPath);
        }

        /// <summary>
        /// Writes a string to a file
        /// </summary>
        /// <param name="str">The string containing the message</param>
        /// <param name="path">The path to the file, must include file name</param>
        public static void Log(String str, string path)
        {
            using (var file = System.IO.File.AppendText(path)) 
            {
                file.WriteLine(str);
            }
        }

        /// <summary>
        /// Writes a string to a file named "error-log.txt" on the desktop
        /// </summary>
        /// <param name="str">The string containing the message</param>
        public static void Log(String str)
        {
            Log(str, DesktopPath);
        }

        /// <summary>
        /// Reads the text in the log file and returns it as a string
        /// </summary>
        /// <param name="path">the path to the log file, this must include the file name</param>
        /// <returns></returns>
        public static string ReadLog(string path)
        {
            return System.IO.File.ReadAllText(path);
        }

        /// <summary>
        /// Reads the text in the "error-log.txt" file on the desktop and returns it as a string
        /// </summary>
        /// <returns></returns>
        public static string ReadLog()
        {
            return System.IO.File.ReadAllText(DesktopPath);
        }
    }
}
