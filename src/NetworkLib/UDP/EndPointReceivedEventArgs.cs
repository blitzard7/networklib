using System.Net;

namespace NetworkLib.UDP
{
    /// <summary>
    ///     Represents the EndPointReceivedEventArgs class.
    /// </summary>
    public class EndPointReceivedEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the EndPointReceivedEventArgs class.
        /// </summary>
        /// <param name="ep">The IP end point.</param>
        public EndPointReceivedEventArgs(IPEndPoint ep)
        {
            Ep = ep;
        }

        /// <summary>
        ///     Gets the IP end point.
        /// </summary>
        public IPEndPoint Ep { get; }
    }
}