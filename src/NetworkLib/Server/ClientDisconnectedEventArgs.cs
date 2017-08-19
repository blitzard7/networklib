﻿using System;

namespace NetworkLib
{
    /// <summary>
    ///     Represents the <see cref="ClientDisconnectedEventArgs"/> class.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ClientDisconnectedEventArgs : EventArgs
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDisconnectedEventArgs"/> class.
        /// </summary>
        /// <param name="c">Contains the disconnected client.</param>
        public ClientDisconnectedEventArgs(Client c)
        {
            DisconnectedClient = c;
        }

        /// <summary>
        /// Gets the disconnected client.
        /// </summary>
        /// <value>
        ///     Contains information about the disconnected client.
        /// </value>
        public Client DisconnectedClient { get; private set; }
    }
}