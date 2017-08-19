using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace NetworkLib
{
    /// <summary>
    ///     Represents the <see cref="Extension"/> class.
    ///     Contains extension for various types.
    /// </summary>
    public static class Extension
    {
        /// <summary>
        ///     Receives data from a network stream.
        ///     Receives the packet information without the header.
        /// </summary>
        /// <param name="stream">Contains the given stream.</param>
        /// <returns>
        ///     Returns the received packet data.
        /// </returns>
        /// <exception cref="InvalidOperationException">Is thrown if the header information is unequal the reserved length.</exception>
        public static byte[] ReceivePacketDataFrom(this NetworkStream stream)
        {
            var tmpData = new List<byte>();
            var data = new byte[4];

            if (stream.Read(data, 0, data.Length) != 4)
            {
                throw new InvalidOperationException("Header length is incorrect!");
            }

            int totalLength = BitConverter.ToInt32(data, 0);
            int readLength = totalLength;

            while (tmpData.Count < totalLength)
            {
                var buffer = new byte[readLength];
                int bufferSize = stream.Read(buffer, 0, buffer.Length);
                tmpData.AddRange(buffer);

                readLength -= bufferSize;
            }

            return tmpData.ToArray();
        }
    }
}