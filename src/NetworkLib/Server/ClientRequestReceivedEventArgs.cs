using System;
using System.Collections;
using System.Collections.Generic;

namespace NetworkLib.Server
{
    /// <inheritdoc />
    /// <summary>
    /// Represents the ClientRequestReceivedEventArgs class.
    /// </summary>
    /// <seealso cref="T:System.EventArgs" />
    public class ClientRequestReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ClientRequestReceivedEventArgs class.
        /// </summary>
        /// <param name="cr">Contains the client's request.</param>
        /// <param name="sender">Contains the client who has sent the request.</param>
        public ClientRequestReceivedEventArgs(IEnumerable<byte> cr, Client.Client sender)
        {
            ClientRequestData = cr;
            Sender = sender;
        }

        /// <summary>
        /// Gets the client request data.
        /// </summary>
        public IEnumerable<byte> ClientRequestData { get; }

        /// <summary>
        /// Gets the sender.
        /// </summary>
        public Client.Client Sender { get; }
    }
}