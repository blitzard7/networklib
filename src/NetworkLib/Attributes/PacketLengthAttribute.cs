using System;

namespace NetworkLib.Attributes
{
    /// <inheritdoc />
    /// <summary>
    /// Represents the PacketLengthAttribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PacketLengthAttribute : Attribute
    {
        /// <summary>
        /// The defined length of the packet.
        /// </summary>
        private readonly int _length; 

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the PacketLengthAttribute class.
        /// </summary>
        /// <param name="length">The length of the packet.</param>
        public PacketLengthAttribute(int length)
        {
            _length = length;
        }

        /// <summary>
        /// Gets the packet length.
        /// </summary>
        public int Length => _length;
    }
}