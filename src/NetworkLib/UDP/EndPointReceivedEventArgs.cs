using System.Net;

namespace NetworkLib.UDP
{
    /// <summary>
    ///     Represents the <see cref="EndPointReceivedEventArgs"/> class.
    /// </summary>
    public class EndPointReceivedEventArgs
    {
        private readonly IPEndPoint _ep;

        /// <summary>
        /// Initializes a new instance of the <see cref="EndPointReceivedEventArgs"/> class.
        /// </summary>
        /// <param name="ep">Contains the received <see cref="IPEndPoint"/>.</param>
        public EndPointReceivedEventArgs(IPEndPoint ep)
        {
            _ep = ep;
        }

        /// <summary>
        /// Gets the <see cref="IPEndPoint"/>.
        /// </summary>
        /// <value>
        ///     Contains the received IP end point.
        /// </value>
        public IPEndPoint Ep => _ep;
    }
}