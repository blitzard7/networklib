using System;
using System.Collections.Generic;
using System.Reflection;
using NetworkLib.Attributes;

namespace NetworkLib.Helper
{
    /// <summary>
    /// Represents the AttributesReader class.
    /// </summary>
    public static class AttributesReader
    {
        // TODO: getting value of the properties which applied the attribute.

        /// <summary>
        /// Gets all the PropertyInfo with the PacketLengthAttribute from the given Type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// Returns a list containing the PropertyInfo.
        /// </returns>
        internal static List<PropertyInfo> GetPropertiesWithPacketLengthAttribute(Type type)
        {
            var propInfo = type.GetProperties();
            var propertiesContainingAttributes = new List<PropertyInfo>();

            foreach (var prop in propInfo)
            {
                if (!(Attribute.GetCustomAttribute(prop, typeof(PacketLengthAttribute)) is PacketLengthAttribute attribute)) continue;
                propertiesContainingAttributes.Add(prop);
            }

            return propertiesContainingAttributes;
       }

        /// <summary>
        /// Gets the properties containing the PacketLengthAttribute.
        /// </summary>
        /// <returns>
        /// Returns a list of Property containing the property info and the class where the property is located.
        /// </returns>
        public static IEnumerable<Property> GetProperties()
        {
            var types = AssemblyReader.GetAssemblyClasses();
            var props = new List<Property>();

            foreach (var type in types)
            {
                var properties = GetPropertiesWithPacketLengthAttribute(type);
                properties.ForEach(x =>
                {
                    props.Add(new Property { ProptertyInfo = x, ClassContainingProp = type });
                });
            }

            return props;
        }

        /// <summary>
        /// Get all classes which contains the PacketAttribute and is serializable.
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
                var hasPacketAndSerializbaleAttribute =  packetAtt != null && serializableAtt != null;

                if (!hasPacketAndSerializbaleAttribute) return null;
                classes.Add(member);
            }
           

            return classes;
        }
    }
}