using System;
using NetworkLib.Attributes;

namespace NetworkLibDemoProject
{
    /// <summary>
    /// Represents the DemoClass class.
    /// </summary>
    [Serializable]
    [Packet]
    public class DemoClass
    {
        /// <summary>
        /// Initializes a new instance of the DemoClass class.
        /// </summary>
        public DemoClass()
        {
        }
        
        /// <summary>
        /// Gets or sets the Amount of the demo property.
        /// Packet length is set to value 2.
        /// </summary>
        [PacketLength(2)]
        public int Amount { get; set; }
        
        /// <summary>
        /// Gets or sets the Content of the demo property.
        /// Packet length is set to value 10.
        /// </summary>
        [PacketLength(10)]
        public string Content { get; set; }
        
        /// <summary>
        /// Gets or sets the Name of the demo property.
        /// Packet length is set to value 5.
        /// </summary>
        [PacketLength(5)]
        public string Name { get; set; }
    }
}