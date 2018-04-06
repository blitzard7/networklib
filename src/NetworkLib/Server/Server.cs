using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NetworkLib
{
    /// <summary>
    ///     Represents the <see cref="Server"/> class.
    /// </summary>
    public class Server
    {
        #region Fields
        /// <summary>
        ///     Represents the <see cref="TcpListener"/> server listener.
        /// </summary>
        private TcpListener _listener;

        /// <summary>
        ///     Represents the <see cref="IPEndPoint"/> server's IP end point.
        /// </summary>
        private IPEndPoint _ep;

        /// <summary>
        ///     Represents the connection port.
        /// </summary>
        private int _port;
        #endregion Fields

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="ip">Contains the given IP address.</param>
        /// <param name="port">Possible given port. Default is set to 5000.</param>
        public Server(IPAddress ip, int port = 5000)
        {
            _ep = new IPEndPoint(ip, port);
            _port = port;
            ConnectedClients = new List<Client>();
        }

        /// <summary>
        ///     Represents the <see cref="NewClientConnectedEventArgs"/>.
        /// </summary>
        public event EventHandler<NewClientConnectedEventArgs> OnClientConnected;

        /// <summary>
        ///     Represents the <see cref="ClientDisconnectedEventArgs"/>.
        /// </summary>
        public event EventHandler<ClientDisconnectedEventArgs> OnClientDisconnected;

        /// <summary>
        ///     Represents the <see cref="ClientRequestReceivedEventArgs"/>.
        /// </summary>
        public event EventHandler<ClientRequestReceivedEventArgs> OnClientRequestReceived;

        /// <summary>
        ///     Represents the <see cref="ConnectionLostEventArgs"/>.
        /// </summary>
        public event EventHandler<ConnectionLostEventArgs> OnConnectionLost;

        /// <summary>
        /// Gets a value indicating whether the server is active or not.
        /// </summary>
        /// <value>
        ///   Is true if the server is active.
        /// </value>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Gets the connected clients.
        /// </summary>
        /// <value>
        ///     A list represented by <see cref="Client"/>.
        /// </value>
        public List<Client> ConnectedClients { get; private set; }

        /// <summary>
        /// Gets the IP address.
        /// </summary>
        /// <value>
        ///     Contains the server's IP address.
        /// </value>
        public IPAddress IP => ((IPEndPoint)_listener.Server.RemoteEndPoint).Address;

        /// <summary>
        /// Gets the port.
        /// </summary>
        /// <value>
        ///     Contains the value of the port.
        /// </value>
        /// <exception cref="ArgumentOutOfRangeException">Is thrown if the port has been set less than 0.</exception>
        public int Port
        {
            get => _port;
            private set
            {
                if (_port < 0)
                {
                    throw new ArgumentOutOfRangeException("Port can't be less than 0!");
                }

                _port = value;
            }
        }

        /// <summary>
        ///     Starts the server.
        /// </summary>
        /// <exception cref="InvalidOperationException">Is thrown if the server has already been instantiated and started.</exception>
        public void Start()
        {
            if (IsActive)
            {
                throw new InvalidOperationException("Server is already running!");
            }

            _listener = new TcpListener(_ep);
            _listener.Start();
            IsActive = true;

            Listen();
        }

        /// <summary>
        ///     Stops the server.
        /// </summary>
        /// <exception cref="InvalidOperationException">Is thrown if the server has already been stopped.</exception>
        public void Stop()
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("Server has already been stopped!");
            }

            _listener.Stop();
            IsActive = false;
        }

        /// <summary>
        ///     Sends the proceeded data back to all connected clients.
        /// </summary>
        /// <param name="data">Contains the to send data.</param>
        public void SendBackToClient(byte[] data)
        {
            try
            {
                foreach (var cc in ConnectedClients)
                {
                    if (cc.IsActive)
                    {
                        cc.Stream.Write(data, 0, data.Length);
                    }
                }
            }
            catch (ObjectDisposedException) { }
            catch (IOException) { }
        }

        /// <summary>
        ///     Fires the <see cref="OnClientConnected"/> event if a new client has been connected.
        /// </summary>
        /// <param name="c">Contains the new connected client.</param>
        protected void FireOnClientConnected(Client c)
        {
            OnClientConnected?.Invoke(this, new NewClientConnectedEventArgs(c));
        }

        /// <summary>
        ///     Fires the <see cref="OnClientDisconnected"/> event if a client has been disconnected.
        /// </summary>
        /// <param name="c">Contains the disconnected client.</param>
        protected void FireOnClientDisconnected(Client c)
        {
            OnClientDisconnected?.Invoke(this, new ClientDisconnectedEventArgs(c));
        }

        /// <summary>
        ///     Fires the <see cref="OnClientRequestReceived"/> event if the server has received the client's request.
        /// </summary>
        /// <param name="cr">Contains the client's request.</param>
        /// <param name="sender">Contains the client who has sent the request.</param>
        protected void FireOnClientRequestReceived(byte[] cr, Client sender)
        {
            OnClientRequestReceived?.Invoke(this, new ClientRequestReceivedEventArgs(cr, sender));
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
        ///     Listens over the network for incoming data.
        /// </summary>
        private void Listen()
        {
            new Thread(() =>
            {
                try
                {
                    while (IsActive)
                    {
                        var client = _listener.AcceptTcpClient();
                        var cc = new Client(client);
                        cc.OnDataReceived += ClientOnDataReceived;
                        ConnectedClients.Add(cc);
                        FireOnClientConnected(cc);
                    }
                }
                catch (SocketException)
                {
                    IsActive = false;
                    FireOnConnectionLost("[Socket exception encountered]");
                }
            }).Start();
        }

        /// <summary>
        ///     Callback method for <see cref="Client.OnDataReceived"/> event: is called if the server receives data from the client.
        /// </summary>
        /// <param name="sender">Contains the given sender.</param>
        /// <param name="e">The <see cref="ClientDataReceivedEventArgs"/> instance containing the event data.</param>
        private void ClientOnDataReceived(object sender, ClientDataReceivedEventArgs e)
        {
            try
            {
                var cc = (Client)sender;

                FireOnClientRequestReceived(e.Data, cc);
            }
            catch (InvalidCastException) { }
        }
    }
}