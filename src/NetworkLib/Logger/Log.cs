using System;
using System.IO;

namespace NetworkLib.Logger
{
    /// <summary>
    /// Represents the <see cref="Log" /> class.
    /// Logs each event that happens into a .txt file for further information.
    /// </summary>
    public static class Log
    {
        private static readonly string LoggingPath = Environment.ExpandEnvironmentVariables(@"%AppData%\");
        private const string FileName = "Network_Log";

        /// <summary>
        /// Starts logging.
        /// </summary>
        public static void Start(string message)
        {
            var fs = new FileStream(LoggingPath + FileName, FileMode.Append, FileAccess.Write);
            try
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.WriteLine($"[{DateTime.Now}]: {message}");
                }
            }
            finally
            {
                fs.Dispose();
            }
        }
    }
}