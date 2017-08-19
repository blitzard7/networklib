using System;

namespace NetworkLib
{
    /// <summary>
    ///     Represents the <see cref="ClientDataReceivedEventArgs"/> class.
    /// </summary>
    /// <seealso cref="EventArgs" />
    public class ClientDataReceivedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDataReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="data">Contains the received data.</param>
        public ClientDataReceivedEventArgs(byte[] data)
        {
            Data = data;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        ///     Contains the received data.
        /// </value>
        public byte[] Data { get; private set; }
    }
}