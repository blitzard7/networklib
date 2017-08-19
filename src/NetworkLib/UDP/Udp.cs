using System;
using System.Net;
using System.Net.Sockets;

namespace NetworkLib
{
    /// <summary>
    ///     Represents the <see cref="Udp"/> class.
    ///     Provides user datagram protocol network services.
    /// </summary>
    public class Udp
    {
        /// <summary>
        ///     Represents the <see cref="UdpClient"/> client.
        /// </summary>
        private UdpClient _udpClient;

        /// <summary>
        ///     Represents the <see cref="IPEndPoint"/> udp's IP end point.
        /// </summary>
        private IPEndPoint _ep;

        /// <summary>
        /// Initializes a new instance of the <see cref="Udp"/> class.
        /// </summary>
        /// <param name="ep">Contains the given <see cref="IPEndPoint"/>.</param>
        public Udp(IPEndPoint ep)
        {
            _ep = ep;
            _udpClient = new UdpClient(ep);
        }

        /// <summary>
        ///     Represents the <see cref="EndPointReceivedEventArgs"/> event.
        /// </summary>
        public event EventHandler<EndPointReceivedEventArgs> OnEndPointReceived;

        /// <summary>
        ///     Connects the specified end point.
        /// </summary>
        /// <param name="endPoint">Contains the given end point to connect to.</param>
        public void Connect(IPEndPoint endPoint)
        {
            _udpClient.Connect(endPoint);
        }

        /// <summary>
        ///     Sends the specified data to a remote host.
        /// </summary>
        /// <param name="data">Contains the to send data.</param>
        public void Send(byte[] data)
        {
            _udpClient.Send(data, data.Length);
        }

        /// <summary>
        ///     Begins receiving a datagram from a remote host asynchronously.
        /// </summary>
        /// <param name="endPoint">Contains the given Ip end point.</param>
        public void Receive(IPEndPoint endPoint)
        {
            _udpClient.BeginReceive(new AsyncCallback(ReceiveData), null);
        }

        /// <summary>
        ///     Fires the <see cref="OnEndPointReceived"/> event, if a new IP end point has been received.
        /// </summary>
        /// <param name="ep">Contains the received IP end point.</param>
        protected void FireOnEndPointReceived(IPEndPoint ep)
        {
            OnEndPointReceived?.Invoke(this, new EndPointReceivedEventArgs(ep));
        }

        /// <summary>
        ///     Receives data asynchronously from a remote host.
        /// </summary>
        /// <param name="ar">Contains the given <see cref="IAsyncResult"/>.</param>
        private void ReceiveData(IAsyncResult ar)
        {
            var endPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = _udpClient.EndReceive(ar, ref endPoint);
            FireOnEndPointReceived(endPoint);
        }
    }
}