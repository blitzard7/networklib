using System;
using System.Reflection;

namespace NetworkLib.Helper
{
    /// <summary>
    /// Represents the Property class.
    /// </summary>
    public class Property
    {
        /// <summary>
        /// Gets or sets the PropertyInfo.
        /// </summary>
        public PropertyInfo ProptertyInfo { get; set; }

        /// <summary>
        /// Gets or sets the Class.
        /// </summary>
        public Type ClassContainingProp { get; set; }
    }
}