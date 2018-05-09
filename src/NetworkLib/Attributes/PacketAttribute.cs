using System;

namespace NetworkLib.Attributes
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents the PacketAttribute class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class PacketAttribute : Attribute
    {
    }
}