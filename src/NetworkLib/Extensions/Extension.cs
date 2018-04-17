using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using NetworkLib.Logger;

namespace NetworkLib.Extensions
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

            var totalLength = BitConverter.ToInt32(data, 0);
            var readLength = totalLength;

            while (tmpData.Count < totalLength)
            {
                var buffer = new byte[readLength];
                var bufferSize = stream.Read(buffer, 0, buffer.Length);
                tmpData.AddRange(buffer);

                readLength -= bufferSize;
            }

            return tmpData.ToArray();
        }

        /// <summary>
        /// Gets the string from the received bytes.
        /// Uses ASCII for encoding.
        /// </summary>
        /// <exception cref="ArgumentException">Is thrown when the argument passed to this method is not valid.</exception>
        /// <exception cref="ArgumentNullException">Is thrown when a null reference is passed.</exception>
        /// <exception cref="DecoderFallbackException"></exception>
        /// <param name="data">The bytes.</param>
        /// <returns>
        /// Returns the encoded string.
        /// </returns>
        public static string EncodeReceivedBytesAsString(this byte[] data)
        {
            var encodedData = Encoding.ASCII.GetString(data);

            Log.Start(
                $"Method: {nameof(EncodeReceivedBytesAsString)} called. Encoded {data.Length} using {nameof(Encoding.ASCII)}.");

            return encodedData;
        }
    }
}