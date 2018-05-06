using System.Net;

namespace NetworkLib.UDP
{
    /// <summary>
    /// Represents the EndPointReceivedEventArgs class.
    /// </summary>
    public class EndPointReceivedEventArgs
    {
        private readonly IPEndPoint _ep;

        /// <summary>
        /// Initializes a new instance of the EndPointReceivedEventArgs class.
        /// </summary>
        /// <param name="ep">The IP end point.</param>
        public EndPointReceivedEventArgs(IPEndPoint ep)
        {
            _ep = ep;
        }

        /// <summary>
        /// Gets the IP end point.
        /// </summary>
        public IPEndPoint Ep => _ep;
    }
}