using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using NetworkLib.Logger;

namespace NetworkLib.Extensions
{
    /// <summary>
    ///     Represents the Extension class.
    /// </summary>
    public static class Extension
    {
        /// <summary>
        ///     Receives data from a network stream.
        /// </summary>
        /// <exception cref="InvalidOperationException">Is thrown if the header information is unequal the reserved length.</exception>
        /// <param name="stream">Contains the given stream.</param>
        /// <returns>
        ///     Returns the received packet data.
        /// </returns>
        public static byte[] ReceivePacketDataFrom(this NetworkStream stream)
        {
            var tmpData = new List<byte>();

            // Todo: replace header length.
            var data = new byte[4];

            if (stream.Read(data, 0, data.Length) != 4)
                throw new InvalidOperationException("Header length is incorrect!");

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
        ///     Gets the string from the received bytes.
        ///     Uses ASCII for encoding.
        /// </summary>
        /// <param name="data">The bytes.</param>
        /// <returns>
        ///     Returns the encoded string.
        /// </returns>
        public static string EncodeReceivedBytesAsString(this byte[] data)
        {
            var encodedData = Encoding.ASCII.GetString(data);

            Log.Start(
                $"Method: {nameof(EncodeReceivedBytesAsString)} called. Encoded {data.Length} using {nameof(Encoding.ASCII)}.");

            return encodedData;
        }

        /// <summary>
        ///     Serializes an object into a byte array.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///     Returns the serializes byte array.
        /// </returns>
        public static IEnumerable<byte> ConvertToByte(this object obj)
        {
            if (obj == null) return null;

            using (var ms = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(ms, obj);

                return ms.ToArray();
            }
        }

        /// <summary>
        ///     Converts a byte array into an object.
        /// </summary>
        /// <param name="bytes">The byte array.</param>
        /// <returns>
        ///     Returns the deserializes object.
        /// </returns>
        public static object ConvertToObject(this IEnumerable<byte> bytes)
        {
            using (var ms = new MemoryStream())
            {
                var tmp = bytes.ToArray();
                var binaryFormatter = new BinaryFormatter();
                ms.Write(tmp, 0, tmp.Length);
                ms.Seek(0, SeekOrigin.Begin);
                return binaryFormatter.Deserialize(ms);
            }
        }
    }
}