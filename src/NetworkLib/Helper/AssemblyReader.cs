using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NetworkLib.Helper
{
    /// <summary>
    /// Represents the AssemblyReader class.
    /// </summary>
    public static class AssemblyReader
    {
        /// <summary>
        /// Gets the entry assembly.
        /// </summary>
        /// <returns>
        /// Returns the entry assembly.
        /// </returns>
        private static Assembly GetEntryAssembly() => Assembly.GetEntryAssembly();
        
        /// <summary>
        /// Gets or sets the current assembly.
        /// </summary>
        public static Assembly CurrentAssembly { get; set; }

        /// <summary>
        /// Gets all the classes from the given assembly.
        /// Is no assembly specified, the current entry assembly will be taken.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>
        /// Returns an IEnumerable of Type containing all class information.
        /// </returns>
        public static IEnumerable<Type> GetAssemblyClasses(Assembly assembly = null)
        {
               
            assembly = CurrentAssembly ?? assembly ?? GetEntryAssembly();

            return assembly.GetTypes().ToList();
        }

        /// <summary>
        /// Gets all properties which has an attribute of type PacketLength from the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// Returns an IEnumerable of PropertyInfo.
        /// </returns>
        private static IEnumerable<PropertyInfo> GetPropertiesFromClasses(Type type) =>
            AttributesReader.GetPropertiesWithPacketLengthAttribute(type);

        /// <summary>
        /// Gets all values from the properties of a specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        /// Returns an IEnumrable of object containing the values of the properties.
        /// </returns>
        public static IEnumerable<object> GetPropertyValues(object type)
        {
            var properties = GetPropertiesFromClasses(type.GetType());

            return properties.Select(property => property.GetValue(type)).ToList();
        }
    }
}