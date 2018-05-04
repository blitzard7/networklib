using System;
using System.Collections.Generic;
using NetworkLib.Enums;

namespace NetworkLib.Client
{
    /// <summary>
    ///     Represents the <see cref="ClientRequest"/> class.
    /// </summary>
    [Serializable]
    public class ClientRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRequest"/> class.
        /// </summary>
        public ClientRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRequest"/> class.
        /// </summary>
        /// <param name="rawData">Contains the data which is sent to the server.</param>
        /// <param name="type">Contains the <see cref="RequestType"/>.</param>
        public ClientRequest(IEnumerable<byte> rawData, RequestType type)
        {
            Data = rawData;
            Type = type;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        ///     Contains the data.
        /// </value>
        public IEnumerable<byte> Data { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        ///     Contains the <see cref="RequestType"/>.
        /// </value>
        public RequestType Type { get; set; }
    }
}