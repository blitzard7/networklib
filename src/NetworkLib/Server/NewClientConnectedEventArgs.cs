using System;

namespace NetworkLib.Server
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents the <see cref="T:NetworkLib.Server.NewClientConnectedEventArgs" /> class.
    /// </summary>
    /// <seealso cref="T:System.EventArgs" />
    public class NewClientConnectedEventArgs : EventArgs
    {
        private readonly Client.Client _newClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewClientConnectedEventArgs"/> class.
        /// </summary>
        /// <param name="c">Contains the new connected client.</param>
        public NewClientConnectedEventArgs(Client.Client c)
        {
            _newClient = c;
        }

        /// <summary>
        /// Gets the new connected client.
        /// </summary>
        /// <value>
        ///     Contains information about the new connected client.
        /// </value>
        public Client.Client NewClient => _newClient;
    }
}