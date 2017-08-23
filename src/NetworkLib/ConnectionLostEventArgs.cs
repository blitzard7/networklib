using System;

namespace NetworkLib
{
    public class ConnectionLostEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectionLostEventArgs"/> class.
        /// </summary>
        /// <param name="message">Contains the lost connection info message.</param>
        public ConnectionLostEventArgs(string message = "")
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the info message.
        /// </summary>
        /// <value>
        ///     Contains the reason for the lost connection.
        /// </value>
        public string Message { get; private set; }
    }
}