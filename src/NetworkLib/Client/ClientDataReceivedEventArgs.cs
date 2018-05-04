using System;
using System.Collections.Generic;

namespace NetworkLib.Client
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents the <see cref="T:NetworkLib.Client.ClientDataReceivedEventArgs" /> class.
    /// </summary>
    /// <seealso cref="T:System.EventArgs" />
    public class ClientDataReceivedEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NetworkLib.Client.ClientDataReceivedEventArgs" /> class.
        /// </summary>
        /// <param name="data">Contains the received data.</param>
        public ClientDataReceivedEventArgs(IEnumerable<byte> data)
        {
            Data = data;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        /// <value>
        ///     Contains the received data.
        /// </value>
        public IEnumerable<byte> Data { get; }
    }
}