using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NetworkLib.Events;
using NetworkLib.Extensions;
using NetworkLib.Logger;

namespace NetworkLib.Client
{
    /// <summary>
    ///     Represents the <see cref="Client"/> class.
    /// </summary>
    public class Client
    {
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
        private readonly IPEndPoint _ep;

        /// <summary>
        ///     Represents the port.
        /// </summary>
        private int _port;

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

        /// <summary>
        ///     Represents the <see cref="ClientDataReceivedEventArgs"/>.
        /// </summary>
        public event EventHandler<ClientDataReceivedEventArgs> OnDataReceived;

        /// <summary>
        ///     Represents the <see cref="ConnectionLostEventArgs"/>.
        /// </summary>
        public event EventHandler<ConnectionLostEventArgs> OnConnectionLost;

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
        public int Port => _port;

        /// <summary>
        /// Gets the ip.
        /// </summary>
        /// <value>
        ///     Contains the IP address of the client.
        /// </value>
        public IPAddress Ip => ((IPEndPoint)_client.Client.RemoteEndPoint).Address;

        /// <summary>
        /// Gets the stream.
        /// </summary>
        /// <value>
        ///     Contains the client's stream.
        /// </value>
        public NetworkStream Stream
        {
            get => _stream;
            private set => _stream = value;
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

            try
            {
                _client = new TcpClient();
                _client.Connect(_ep);
                _stream = _client.GetStream();
                IsActive = true;

                ReceiveData();
            }
            catch (SocketException)
            {
                IsActive = false;
                FireOnConnectionLost("[Can't connect to server]");
                Log.Start("Stream has been disposed.");
            }
            catch (ObjectDisposedException)
            {
                IsActive = false;
                FireOnConnectionLost("[Stream disposed]");
                Log.Start("Stream has been disposed.");
            }
        }

        /// <summary>
        ///     Stops the client.
        /// </summary>
        /// <exception cref="InvalidOperationException">Is thrown if the client has already been stopped.</exception>
        public void Stop()
        {
            if (!IsActive)
            {
                Log.Start($"Throwing exception: {nameof(InvalidOperationException)}. Client has already been stopped!");
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
                var packet = Packet.Packet.GeneratePacket(data);

                Log.Start($"Packet successfully generated for {data.Length} amount on bytes." +
                          $"Writing packet into stream.");
                _stream.Write(packet, 0, packet.Length);
            }
            catch (ObjectDisposedException)
            {
                IsActive = false;
                FireOnConnectionLost("[Stream disposed]");
                Log.Start("Stream has been disposed.");
            }
            catch (IOException)
            {
                IsActive = false;
                FireOnConnectionLost("[Connection to remote endpoint lost]");
                Log.Start("Stream has been disposed.");
            }
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
        ///     Fires the <see cref="OnConnectionLost"/> event if the connection has been lost.
        /// </summary>
        /// <param name="message">[Optional] The message info.</param>
        protected void FireOnConnectionLost(string message = "")
        {
            OnConnectionLost?.Invoke(this, new ConnectionLostEventArgs(message));
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
                        var data = _stream.ReceivePacketDataFrom();
                        FireOnDataReceived(data);
                        Log.Start($"Received {data.Length} amount on data.");
                    }
                }
                catch (ObjectDisposedException)
                {
                    IsActive = false;
                    FireOnConnectionLost("[Stream disposed]");
                    Log.Start("Stream has been disposed.");
                }
                catch (IOException)
                {
                    IsActive = false;
                    FireOnConnectionLost("[Connection to remote endpoint lost]");
                    Log.Start("Connection to remote endpoint lost.");
                }
            }).Start();
        }
    }
}