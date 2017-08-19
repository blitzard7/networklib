using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkLib
{
    /// <summary>
    ///     Represents the <see cref="Log" /> class.
    ///     Logs each event that happens into a .txt file for further information.
    /// </summary>
    public class Log
    {
        /// <summary>
        ///     Represents the path where the log file is stored.
        /// </summary>
        private string _path;

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="path">Contains the given path.</param>
        public Log(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Gets the path.
        /// </summary>
        /// <value>
        ///     Contains the path of the log file.
        /// </value>
        public string Path
        {
            get { return _path; }
            private set
            {
                _path = value;
            }
        }

        /// <summary>
        ///     Changes the path into another one.
        /// </summary>
        /// <param name="newPath">Contains the new path.</param>
        public void ChangePath(string newPath)
        {
            _path = newPath;
        }

        /// <summary>
        ///     Starts logging.
        /// </summary>
        public void Start()
        {
           throw new NotImplementedException();
        }

        /// <summary>
        ///     Stops logging.
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}