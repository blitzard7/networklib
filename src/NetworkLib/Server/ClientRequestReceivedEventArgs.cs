using System;
using System.Collections;
using System.Collections.Generic;

namespace NetworkLib.Server
{
    /// <summary>
    ///     Represents the <see cref="ClientRequestReceivedEventArgs"/> class.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ClientRequestReceivedEventArgs : EventArgs
    {
        private readonly byte[] _clientRequestData;
        private readonly Client.Client _sender;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRequestReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="cr">Contains the client's request.</param>
        /// <param name="sender">Contains the client who has sent the request.</param>
        public ClientRequestReceivedEventArgs(byte[] cr, Client.Client sender)
        {
            _clientRequestData = cr;
            _sender = sender;
        }

        /// <summary>
        /// Gets the client request data.
        /// </summary>
        /// <value>
        ///     Contains the client's request as bytes.
        /// </value>
        public byte[] ClientRequestData => _clientRequestData;

        /// <summary>
        /// Gets the sender.
        /// </summary>
        /// <value>
        ///     Contains information about the client who has sent the request.
        /// </value>
        public Client.Client Sender => _sender;
    }
}