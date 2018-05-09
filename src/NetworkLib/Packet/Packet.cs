using System;
using System.Collections.Generic;
using System.Linq;
using NetworkLib.Extensions;
using NetworkLib.Helper;

namespace NetworkLib.Packet
{
    /// <summary>
    /// Represents the <see cref="Packet"/> class.
    /// </summary>
    public static class Packet
    {
        /*
         * Todo: Packet refactoring
         * - Create customizable packet.
         * - Change GeneratePacket method signature.
         */

        /// <summary>
        /// Generates the packet data.
        /// Contains the header information and the packet itself.
        /// </summary>
        /// <example>
        /// 
        /// The packet is built like this:   
        /// -------------------------------------------
        /// |4 Bytes Packet Length as Header | Packet |
        /// -------------------------------------------
        /// 
        /// For example the packet contains "Hello World" as bytes.
        /// The header would contain the length of "Hello World" as bytes.
        /// </example>
        /// <param name="data">Contains the given data.</param>
        /// <returns>
        /// Returns the packet with the header information and the packet itself.
        /// </returns>
        [Obsolete("Use GeneratePacket(object type) instead.")]
        public static byte[] GeneratePacket(byte[] data)
        {
            var tmpData = new List<byte>();
            tmpData.AddRange(BitConverter.GetBytes(data.Length));
            tmpData.AddRange(data);

            return tmpData.ToArray();
        }

        /// <summary>
        /// Generates the packet for a specified object.
        /// </summary>
        /// <param name="type">The object.</param>
        /// <returns>
        /// Returns the generated packet as bytes.
        /// </returns>
        public static IEnumerable<IEnumerable<byte>> GeneratePacket(object type)
        {
            var data = new List<List<byte>>();
            var propData = AssemblyReader.GetPropertyValues(type).ToList();

            foreach (var prop in propData)
            {
                data.Add(prop.ConvertToByte().ToList());
            }         
            
            return data.ToArray();
        }

        /// <summary>
        /// Collects the header information from the packet.
        /// The header information should contain the length of the packet.
        /// </summary>
        /// <param name="data">Contains the data.</param>
        /// <returns>
        /// Returns the header information.
        /// </returns>
        public static byte[] CollectHeaderInformation(IEnumerable<byte> data)
        {
            return data.Take(4).ToArray();
        }

        /// <summary>
        /// Collects the packet information.
        /// </summary>
        /// <param name="data">Contains the packet.</param>
        /// <returns>
        /// Returns only the packet information, without the header data.
        /// </returns>
        public static byte[] CollectPacketInformation(IEnumerable<byte> data)
        {
            return data.Skip(4).ToArray();
        }
    }
}