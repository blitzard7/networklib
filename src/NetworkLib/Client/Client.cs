using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetworkLib
{
    /// <summary>
    ///     Represents the <see cref="Client"/> class.
    /// </summary>
    public class Client
    {
        #region Fields
        /// <summary>
        ///     Represents the <see cref="TcpClient"/> client.
        /// </summary>
        private TcpClient _client;

        /// <summary>
        ///     Represents the client's network stream.
        /// </summary>
        private NetworkStream _stream;

        /// <summary>
        ///     Represents the client's IP end point.
        /// </summary>
        private IPEndPoint _ep;

        /// <summary>
        ///     Represents the port.
        /// </summary>
        private int _port;
        #endregion Fields

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="ip">Contains the given IP address.</param>
        /// <param name="port">Possible given port. Default is set to 5000.</param>
        public Client(IPAddress ip, int port = 5000)
        {
            _ep = new IPEndPoint(ip, port);
            _port = port;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="client">Contains the given client.</param>
        public Client(TcpClient client)
        {
            _client = client;
            _stream = client.GetStream();
            IsActive = true;
            ReceiveData();
        }
        #endregion Ctor

        /// <summary>
        ///     Represents the <see cref="ClientDataReceivedEventArgs"/> event.
        ///     Is raised if new data has been received.
        /// </summary>
        public event EventHandler<ClientDataReceivedEventArgs> OnDataReceived;

        /// <summary>
        /// Gets or sets a value indicating whether the client is active or not.
        /// </summary>
        /// <value>
        ///   Is true if the client is active.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>
        ///     Contains the port value.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown if the value of the port was set to 0.</exception>
        public int Port
        {
            get { return _port; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Port can't be set less than 0!");
                }

                _port = value;
            }
        }

        /// <summary>
        /// Gets the ip.
        /// </summary>
        /// <value>
        ///     Contains the IP address of the client.
        /// </value>
        public IPAddress IP
        {
            get { return ((IPEndPoint)_client.Client.RemoteEndPoint).Address; }
        }

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <value>
        ///     Contains the client's stream.
        /// </value>
        public NetworkStream Stream
        {
            get { return _stream; }
            private set
            {
                _stream = value;
            }
        }

        /// <summary>
        ///     Starts the client.
        /// </summary>
        /// <exception cref="InvalidOperationException">Is thrown if the client has already been instantiated and started.</exception>
        public void Start()
        {
            if (IsActive)
            {
                throw new InvalidOperationException("Client is already running.");
            }

            _client = new TcpClient();
            _client.Connect(_ep);
            _stream = _client.GetStream();
            IsActive = true;

            ReceiveData();
        }

        /// <summary>
        ///     Stops the client.
        /// </summary>
        /// <exception cref="InvalidOperationException">Is thrown if the client has already been stopped.</exception>
        public void Stop()
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("Client has already been stopped!");
            }

            _client.Close();
            IsActive = false;
        }

        /// <summary>
        ///     Sends given data to the server.
        /// </summary>
        /// <param name="data">Contains the data as a byte array.</param>
        public void SendToServer(byte[] data)
        {
            try
            {
                _stream.Write(data, 0, data.Length);
            }
            catch (SocketException) { IsActive = false; }
            catch (ObjectDisposedException) { IsActive = false; }
            catch (IOException) { IsActive = false; }
        }

        /// <summary>
        ///     Fires the <see cref="OnDataReceived"/> event if the client receives new data.
        /// </summary>
        /// <param name="data">Contains the received data.</param>
        protected void FireOnDataReceived(byte[] data)
        {
            OnDataReceived?.Invoke(this, new ClientDataReceivedEventArgs(data));
        }

        /// <summary>
        ///     Receives data from the connection between server and client.
        /// </summary>
        private void ReceiveData()
        {
            new Thread(() =>
            {
                try
                {
                    while (IsActive)
                    {
                        byte[] data = _stream.ReceivePacketDataFrom();
                        FireOnDataReceived(data);
                    }
                }
                catch (SocketException) { IsActive = false; }
                catch (ObjectDisposedException) { IsActive = false; }
                catch (IOException) { IsActive = false; }
            }).Start();
        }
    }
}