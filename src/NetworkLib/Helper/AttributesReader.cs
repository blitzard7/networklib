using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using NetworkLib.Attributes;

namespace NetworkLib.Helper
{
    /// <summary>
    ///     Represents the AttributesReader class.
    /// </summary>
    public static class AttributesReader
    {
        // TODO: getting value of the properties which applied the attribute.

        /// <summary>
        ///     Gets all the PropertyInfo with the PacketLengthAttribute from the given Type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///     Returns a list containing the PropertyInfo.
        /// </returns>
        internal static IEnumerable<PropertyInfo> GetPropertiesWithPacketLengthAttribute(Type type)
        {
            var propInfo = type.GetProperties();
            var propertiesContainingAttributes = new List<PropertyInfo>();

            foreach (var prop in propInfo)
            {
                if (!(Attribute.GetCustomAttribute(prop, typeof(PacketLengthAttribute)) is PacketLengthAttribute
                    attribute)) continue;
                propertiesContainingAttributes.Add(prop);
            }

            return propertiesContainingAttributes;
        }

        /// <summary>
        ///     Get all classes which contains the PacketAttribute and is serializable.
        /// </summary>
        /// <returns></returns>
        internal static IEnumerable<MemberInfo> GetMemberInfoWithPacketAttribute()
        {
            var members = AssemblyReader.GetAssemblyClasses();
            var classes = new List<MemberInfo>();

            foreach (var member in members)
            {
                var packetAtt = Attribute.GetCustomAttribute(member, typeof(PacketAttribute));
                var serializableAtt = Attribute.GetCustomAttribute(member, typeof(SerializableAttribute));
                var hasPacketAndSerializbaleAttribute = packetAtt != null && serializableAtt != null;

                if (!hasPacketAndSerializbaleAttribute) return new List<MemberInfo>();
                classes.Add(member);
            }

            return classes;
        }
    }
}