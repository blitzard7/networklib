using System.Net;

namespace NetworkLib
{
    /// <summary>
    ///     Represents the <see cref="EndPointReceivedEventArgs"/> class.
    /// </summary>
    public class EndPointReceivedEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndPointReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="ep">Contains the received <see cref="IPEndPoint"/>.</param>
        public EndPointReceivedEventArgs(IPEndPoint ep)
        {
            EP = ep;
        }

        /// <summary>
        /// Gets the <see cref="IPEndPoint"/>.
        /// </summary>
        /// <value>
        ///     Contains the received IP end point.
        /// </value>
        public IPEndPoint EP { get; private set; }
    }
}