using System;

namespace NetworkLib.Server
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents the ClientDisconnectedEventArgs class.
    /// </summary>
    /// <seealso cref="T:System.EventArgs" />
    public class ClientDisconnectedEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:NetworkLib.Server.ClientDisconnectedEventArgs" /> class.
        /// </summary>
        /// <param name="c">Contains the disconnected client.</param>
        public ClientDisconnectedEventArgs(Client.Client c)
        {
            DisconnectedClient = c;
        }

        /// <summary>
        ///     Gets the disconnected client.
        /// </summary>
        public Client.Client DisconnectedClient { get; }
    }
}