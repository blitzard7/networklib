using System;
using System.Collections.Generic;
using NetworkLib.Enums;

namespace NetworkLib.Server
{
    /// <summary>
    ///     Represents the <see cref="ServerResponse"/> class.
    /// </summary>
    [Serializable]
    public class ServerResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerResponse"/> class.
        /// </summary>
        public ServerResponse()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerResponse"/> class.
        /// </summary>
        /// <param name="data">Contains the given data to be send to the clients.</param>
        /// <param name="type">Contains the <see cref="ResponseType"/>.</param>
        public ServerResponse(IEnumerable<byte> data, ResponseType type)
        {
            Data = data;
            Type = type;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        ///     Contains information which is going to be sent to the clients.
        /// </value>
        public IEnumerable<byte> Data { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        ///     Contains the <see cref="ResponseType"/>.
        /// </value>
        public ResponseType Type { get; set; }
    }
}