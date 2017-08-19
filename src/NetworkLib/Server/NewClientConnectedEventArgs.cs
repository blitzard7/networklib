using System;

namespace NetworkLib
{
    /// <summary>
    ///     Represents the <see cref="NewClientConnectedEventArgs"/> class.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class NewClientConnectedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NewClientConnectedEventArgs"/> class.
        /// </summary>
        /// <param name="c">Contains the new connected client.</param>
        public NewClientConnectedEventArgs(Client c)
        {
            NewClient = c;
        }

        /// <summary>
        /// Gets the new connected client.
        /// </summary>
        /// <value>
        ///     Contains information about the new connected client.
        /// </value>
        public Client NewClient { get; private set; }
    }
}