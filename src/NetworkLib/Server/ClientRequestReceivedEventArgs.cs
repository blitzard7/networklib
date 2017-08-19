using System;

namespace NetworkLib
{
    /// <summary>
    ///     Represents the <see cref="ClientRequestReceivedEventArgs"/> class.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ClientRequestReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRequestReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="cr">Contains the client's request.</param>
        /// <param name="sender">Contains the client who has sent the request.</param>
        public ClientRequestReceivedEventArgs(byte[] cr, Client sender)
        {
            ClientRequestData = cr;
            Sender = sender;
        }

        /// <summary>
        /// Gets the client request data.
        /// </summary>
        /// <value>
        ///     Contains the client's request as bytes.
        /// </value>
        public byte[] ClientRequestData { get; private set; }

        /// <summary>
        /// Gets the sender.
        /// </summary>
        /// <value>
        ///     Contains information about the client who has sent the request.
        /// </value>
        public Client Sender { get; private set; }
    }
}