using System;
using System.Collections.Generic;
using System.Reflection;
using NetworkLib.Attributes;

namespace NetworkLib.Helper
{
    public static class AttributesReader
    {
        // TODO: getting value of the properties which applied the attribute.

        public static List<PacketLengthAttribute> GetPacketLengthAttributes(Type type)
        {
            var propInfo = type.GetProperties();
            var packetLengthAttributes = new List<PacketLengthAttribute>();

            foreach (var prop in propInfo)
            {
                if (!(Attribute.GetCustomAttribute(prop, typeof(PacketLengthAttribute)) is PacketLengthAttribute attribute)) continue;
                packetLengthAttributes.Add(attribute);
            }

            return packetLengthAttributes;
       }

        public static List<PacketAttribute> GetPacketAttributes(Type type)
        {
            var packetAtt = Attribute.GetCustomAttribute(type, typeof(PacketAttribute));
            var serializableAtt = Attribute.GetCustomAttribute(type, typeof(SerializableAttribute));
            var hasPacketAndSerializbaleAttribute =  packetAtt != null && serializableAtt != null;

            if (!hasPacketAndSerializbaleAttribute) return null;
            
            var packetAttributes = new List<PacketAttribute>();

            return packetAttributes;
        }

        private static void InstantiateAssembly()
        {

        }
    }
}