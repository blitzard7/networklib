using System;

namespace NetworkLib.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents the PacketLengthAttribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PacketLengthAttribute : Attribute
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the PacketLengthAttribute class.
        /// </summary>
        /// <param name="length">The length of the packet.</param>
        public PacketLengthAttribute(int length)
        {
            Length = length;
        }

        /// <summary>
        ///     Gets the packet length.
        /// </summary>
        public int Length { get; }
    }
}