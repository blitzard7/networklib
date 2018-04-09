using System;
using System.IO;

namespace NetworkLib.Logger
{
    /// <summary>
    ///     Represents the <see cref="Log" /> class.
    ///     Logs each event that happens into a .txt file for further information.
    /// </summary>
    public static class Log
    {
        private static readonly string _loggingPath = Environment.ExpandEnvironmentVariables(@"%AppData\");
        private static int _counter = 0;

        /// <summary>
        ///     Starts logging.
        /// </summary>
        public static void Start(string message)
        {
            var fs = new FileStream(_loggingPath + _counter, FileMode.Append, FileAccess.Write);
            try
            {
                using (var writer = new StreamWriter(fs))
                {
                    writer.WriteLine($"[{DateTime.Now}]: {message}");
                }
            }
            finally
            {
                fs?.Dispose();
            }

            _counter++;
        }
    }
}