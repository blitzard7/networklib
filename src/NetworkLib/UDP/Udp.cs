using System;
using System.Net;
using System.Net.Sockets;

namespace NetworkLib.UDP
{
    /// <summary>
    /// Represents theUd class.
    /// Provides user datagram protocol network services.
    /// </summary>
    public class Udp
    {
        /// <summary>
        /// Represents the UdpClient client.
        /// </summary>
        private readonly UdpClient _udpClient;

        /// <summary>
        /// Initializes a new instance of the Udp class.
        /// </summary>
        /// <param name="ep">The IP end point.</param>
        public Udp(IPEndPoint ep)
        {
            _udpClient = new UdpClient(ep);
        }

        /// <summary>
        /// Represents the EndPointReceivedEventArgs event.
        /// </summary>
        public event EventHandler<EndPointReceivedEventArgs> OnEndPointReceived;

        /// <summary>
        /// Connects the specified end point.
        /// </summary>
        /// <param name="endPoint">Contains the given end point to connect to.</param>
        public void Connect(IPEndPoint endPoint)
        {
            _udpClient.Connect(endPoint);
        }

        /// <summary>
        /// Sends the specified data to a remote host.
        /// </summary>
        /// <param name="data">Contains the to send data.</param>
        public void Send(byte[] data)
        {
            _udpClient.Send(data, data.Length);
        }

        /// <summary>
        /// Begins receiving a datagram from a remote host asynchronously.
        /// </summary>
        /// <param name="endPoint">Contains the given Ip end point.</param>
        public void Receive(IPEndPoint endPoint)
        {
            _udpClient.BeginReceive(ReceiveData, null);
        }

        /// <summary>
        /// OnEndPointReceived event is fired, if a new IP end point has been received.
        /// </summary>
        /// <param name="ep">Contains the received IP end point.</param>
        protected void FireOnEndPointReceived(IPEndPoint ep)
        {
            OnEndPointReceived?.Invoke(this, new EndPointReceivedEventArgs(ep));
        }

        /// <summary>
        /// Receives data asynchronously from a remote host.
        /// </summary>
        /// <param name="ar">The IAsyncResult.</param>
        private void ReceiveData(IAsyncResult ar)
        {
            var endPoint = new IPEndPoint(IPAddress.Any, 0);
            _udpClient.EndReceive(ar, ref endPoint);
            FireOnEndPointReceived(endPoint);
        }
    }
}