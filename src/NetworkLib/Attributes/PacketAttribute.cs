using System;

namespace NetworkLib.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class PacketAttribute : Attribute
    {
        public PacketAttribute() { }


    }
}