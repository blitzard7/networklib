﻿using System;

namespace NetworkLib.Events
{
    /// <inheritdoc />
    /// <summary>
    /// Represents the ConnectionLostEventArgs class.
    /// </summary>
    public class ConnectionLostEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ConnectionLostEventArgs class.
        /// </summary>
        /// <param name="message">Contains the lost connection info message.</param>
        public ConnectionLostEventArgs(string message = "")
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the info message.
        /// </summary>
        public string Message { get; }
    }
}