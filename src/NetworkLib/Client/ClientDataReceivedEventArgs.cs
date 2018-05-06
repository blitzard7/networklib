using System;
using System.Collections.Generic;

namespace NetworkLib.Client
{
    /// <inheritdoc />
    /// <summary>
    /// Represents the ClientDataReceivedEventArgs class.
    /// </summary>
    public class ClientDataReceivedEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the ClientDataReceivedEventArgs class.
        /// </summary>
        /// <param name="data">Contains the received data.</param>
        public ClientDataReceivedEventArgs(IEnumerable<byte> data)
        {
            Data = data;
        }

        /// <summary>
        /// Gets the data.
        /// </summary>
        public IEnumerable<byte> Data { get; }
    }
}