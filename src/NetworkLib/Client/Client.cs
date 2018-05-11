using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using NetworkLib.Contracts;
using NetworkLib.Events;
using NetworkLib.Extensions;
using NetworkLib.Logger;

namespace NetworkLib.Client
{
    /// <summary>
    ///     Represents the Client class.
    /// </summary>
    public class Client : IClient
    {
        /// <summary>
        ///     Represents the client's IP end point.
        /// </summary>
        private readonly IPEndPoint _ep;

        /// <summary>
        ///     Represents the tcp client.
        /// </summary>
        private TcpClient _client;

        /// <summary>
        ///     Represents the client's network stream.
        /// </summary>
        private NetworkStream _stream;

        /// <summary>
        ///     Initializes a new instance of the Client class.
        /// </summary>
        /// <param name="ip">Contains the given IP address.</param>
        /// <param name="port">Possible given port. Default is set to 5000.</param>
        public Client(IPAddress ip, int port = 5000)
        {
            _ep = new IPEndPoint(ip, port);
            Port = port;
        }

        /// <summary>
        ///     Initializes a new instance of the Client class.
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
        ///     Gets or sets a value indicating whether the client is active or not.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        ///     Gets the port.
        /// </summary>
        public int Port { get; }

        /// <summary>
        ///     Gets the client's IP address.
        /// </summary>
        public IPAddress Ip => ((IPEndPoint) _client.Client.RemoteEndPoint).Address;

        internal NetworkStream Stream => _stream;

        /// <summary>
        ///     Starts the client.
        /// </summary>
        /// <exception cref="InvalidOperationException">Is thrown if the client has already been instantiated and started.</exception>
        public void Start()
        {
            if (IsActive) throw new InvalidOperationException("Client has been already started.");

            try
            {
                _client = new TcpClient();
                _client.Connect(_ep);
                _stream = _client.GetStream();
                IsActive = true;

                ReceiveData();
            }
            catch (SocketException e)
            {
                IsActive = false;
                FireOnConnectionLost($"Exception {nameof(e)} has been encountered while connecting to server.");
                Log.Start($"Exception {nameof(e)}:\n" +
                          $"{e.StackTrace}");
            }
            catch (ObjectDisposedException e)
            {
                IsActive = false;
                FireOnConnectionLost($"Exception {nameof(e)} has been encountered while connecting to server.");
                Log.Start($"Exception {nameof(e)}:\n" +
                          $"{e.StackTrace}");
            }
        }

        /// <summary>
        ///     Stops the client.
        /// </summary>
        /// <exception cref="InvalidOperationException">Is thrown if the client has already been stopped.</exception>
        public void Stop()
        {
            if (!IsActive) throw new InvalidOperationException("Client has already been stopped!");

            _client.Close();
            IsActive = false;
        }

        /// <summary>
        ///     Sends given data to the server.
        /// </summary>
        /// <param name="data">Contains the data as a byte array.</param>
        public void SendToServer(IEnumerable<byte> data)
        {
            try
            {
                var tmpData = data.ToArray();
                var packet = Packet.Packet.GeneratePacket(tmpData);

                Log.Start($"Packet successfully generated for {tmpData.Length} amount on bytes." +
                          "Writing packet into stream.");
                _stream.Write(packet, 0, packet.Length);
            }
            catch (ObjectDisposedException e)
            {
                IsActive = false;
                FireOnConnectionLost($"Exception {nameof(e)}, object has been disposed.");
                Log.Start($"Exception {nameof(e)}:\n" +
                          $"{e.StackTrace}");
            }
            catch (IOException e)
            {
                IsActive = false;
                FireOnConnectionLost($"Exception {nameof(e)} encountered.");
                Log.Start($"Exception {nameof(e)}:\n" +
                          $"{e.StackTrace}");
            }
        }

        /// <summary>
        ///     Represents the ClientDataReceivedEventArgs.
        /// </summary>
        public event EventHandler<ClientDataReceivedEventArgs> OnDataReceived;

        /// <summary>
        ///     Represents the ConnectionLostEventArgs.
        /// </summary>
        public event EventHandler<ConnectionLostEventArgs> OnConnectionLost;

        /// <summary>
        ///     Fires the <see cref="OnDataReceived" /> event if the client receives new data.
        /// </summary>
        /// <param name="data">Contains the received data.</param>
        private void FireOnDataReceived(IEnumerable<byte> data)
        {
            OnDataReceived?.Invoke(this, new ClientDataReceivedEventArgs(data));
        }

        /// <summary>
        ///     Fires the <see cref="OnConnectionLost" /> event if the connection has been lost.
        /// </summary>
        /// <param name="message">[Optional] The message info.</param>
        private void FireOnConnectionLost(string message = "")
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
                catch (ObjectDisposedException e)
                {
                    IsActive = false;
                    FireOnConnectionLost($"Exception {nameof(e)}, object has been disposed.");
                    Log.Start($"Exception {nameof(e)}:\n" +
                              $"{e.StackTrace}");
                }
                catch (IOException e)
                {
                    IsActive = false;
                    FireOnConnectionLost($"Exception {nameof(e)} encountered. Connection to remote endpoint lost.");
                    Log.Start($"Exception {nameof(e)}:\n" +
                              $"{e.StackTrace}");
                }
            }).Start();
        }
    }
}