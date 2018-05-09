using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NetworkLib.Client;
using NetworkLib.Contracts;
using NetworkLib.Events;
using NetworkLib.Logger;

namespace NetworkLib.Server
{
    /// <summary>
    ///     Represents the Server class.
    /// </summary>
    public class Server : IServer
    {
        /// <summary>
        ///     Represents the server's IP end point.
        /// </summary>
        private readonly IPEndPoint _ep;

        /// <summary>
        ///     Represents the connection port.
        /// </summary>
        private readonly int _port;

        /// <summary>
        ///     Represents the server listener.
        /// </summary>
        private TcpListener _listener;

        /// <summary>
        ///     Initializes a new instance of the Server class.
        /// </summary>
        /// <param name="ip">Contains the given IP address.</param>
        /// <param name="port">Possible given port. Default is set to 5000.</param>
        public Server(IPAddress ip, int port = 5000)
        {
            _ep = new IPEndPoint(ip, port);
            _port = port;
            ConnectedClients = new List<Client.Client>();
        }

        /// <summary>
        ///     Gets a value indicating whether the server is active or not.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        ///     Gets the connected clients.
        /// </summary>
        public List<Client.Client> ConnectedClients { get; }

        /// <summary>
        ///     Gets the IP address.
        /// </summary>
        public IPAddress Ip => ((IPEndPoint) _listener.Server.RemoteEndPoint).Address;

        /// <summary>
        ///     Gets the port.
        /// </summary>
        public int Port => _port;

        /// <summary>
        ///     Starts the server.
        /// </summary>
        /// <exception cref="InvalidOperationException">Is thrown if the server has already been instantiated and started.</exception>
        public void Start()
        {
            if (IsActive) throw new InvalidOperationException("Server is already running!");

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
            if (!IsActive) throw new InvalidOperationException("Server has already been stopped!");

            _listener.Stop();
            IsActive = false;
        }

        /// <summary>
        ///     Sends the proceeded data back to all connected clients.
        /// </summary>
        /// <param name="data">The data.</param>
        public void SendToClient(IEnumerable<byte> data)
        {
            try
            {
                var tmpData = data.ToArray();
                foreach (var cc in ConnectedClients)
                    if (cc.IsActive)
                        cc.Stream.Write(tmpData, 0, tmpData.Length);
            }
            catch (ObjectDisposedException)
            {
                Log.Start(
                    $"Exception {nameof(ObjectDisposedException)}: has been thrown in {nameof(SendToClient)} method");
            }
            catch (IOException)
            {
                Log.Start($"Exception {nameof(IOException)}: has been thrown in {nameof(SendToClient)} method");
            }
        }

        /// <summary>
        ///     Represents the NewClientConnectedEventArgs.
        /// </summary>
        public event EventHandler<NewClientConnectedEventArgs> OnClientConnected;

        /// <summary>
        ///     Represents the ClientDisconnectedEventArgs.
        /// </summary>
        public event EventHandler<ClientDisconnectedEventArgs> OnClientDisconnected;

        /// <summary>
        ///     Represents the ClientRequestReceivedEventArgs.
        /// </summary>
        public event EventHandler<ClientRequestReceivedEventArgs> OnClientRequestReceived;

        /// <summary>
        ///     Represents the ConnectionLostEventArgs.
        /// </summary>
        public event EventHandler<ConnectionLostEventArgs> OnConnectionLost;

        /// <summary>
        ///     Fires the <see cref="OnClientConnected" /> event if a new client has been connected.
        /// </summary>
        /// <param name="c">Contains the new connected client.</param>
        protected void FireOnClientConnected(Client.Client c)
        {
            OnClientConnected?.Invoke(this, new NewClientConnectedEventArgs(c));
        }

        /// <summary>
        ///     Fires the <see cref="OnClientDisconnected" /> event if a client has been disconnected.
        /// </summary>
        /// <param name="c">Contains the disconnected client.</param>
        protected void FireOnClientDisconnected(Client.Client c)
        {
            OnClientDisconnected?.Invoke(this, new ClientDisconnectedEventArgs(c));
        }

        /// <summary>
        ///     Fires the <see cref="OnClientRequestReceived" /> event if the server has received the client's request.
        /// </summary>
        /// <param name="cr">Contains the client's request.</param>
        /// <param name="sender">Contains the client who has sent the request.</param>
        protected void FireOnClientRequestReceived(IEnumerable<byte> cr, Client.Client sender)
        {
            OnClientRequestReceived?.Invoke(this, new ClientRequestReceivedEventArgs(cr, sender));
        }

        /// <summary>
        ///     Fires the <see cref="OnConnectionLost" /> event if the connection has been lost.
        /// </summary>
        /// <param name="message">[Optional] The message info.</param>
        protected void FireOnConnectionLost(string message = "")
        {
            OnConnectionLost?.Invoke(this, new ConnectionLostEventArgs(message));
        }

        /// <summary>
        ///     sListens over the network for incoming data.
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
                        var cc = new Client.Client(client);
                        cc.OnDataReceived += ClientOnDataReceived;
                        ConnectedClients.Add(cc);
                        FireOnClientConnected(cc);
                    }
                }
                catch (SocketException e)
                {
                    IsActive = false;
                    FireOnConnectionLost("Socket exception encountered");
                    Log.Start($"Exception {nameof(e)}:\n" +
                              $"{e.StackTrace}");
                }
            }).Start();
        }

        /// <summary>
        ///     Is called if the server receives data from the client.
        /// </summary>
        /// <param name="sender">Contains the given sender.</param>
        /// <param name="e">The <see cref="ClientDataReceivedEventArgs" /> instance containing the event data.</param>
        private void ClientOnDataReceived(object sender, ClientDataReceivedEventArgs e)
        {
            try
            {
                var cc = (Client.Client) sender;
                FireOnClientRequestReceived(e.Data, cc);
            }
            catch (InvalidCastException ex)
            {
                Log.Start($"Exception {nameof(ex)}:\n" +
                          $"{ex.StackTrace}");
            }
        }
    }
}