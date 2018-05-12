# networklib 
## Features
* Server

```cs
var server = new Server(ip, port); 
// ip: represents an IPAddress.
// port: is optional, default is set to 5000.
```

* Client
```cs
var client = new Client(ip, port);
// ip: represents an IPAddress.
// port: is optional, default is set to 5000.
```

* UDP
```cs
 var udp = new UDP(ep);
// ep: represents an IPEndPoint.
```

## Example
### Server

- Initialization
```cs
var server = new Server(IPAddress.Loopback, 7337); 
server.OnClientConnected += ServerOnClientConnected; // callback for connected client.
server.OnClientDisconnected += ServerOnClientDisconnected; // callback for disconnected client.
server.OnClientRequestReceived += ServerOnClientRequestReceived; // callback for clients request received.
server.Start(); // starting server.

```

### Client

- Initialization 

```cs
 var client = new Client(IPAddress.Parse("127.0.0.1"), 7337); // using server's IPAddress and running on port 7337.
client.OnDataReceived += ClientOnDataReceived; // callback for received data.
client.Start(); // starting client.
```

- Sending data to server

```cs
byte[] packet = Packet.GeneratePacket(Encoding.ASCII.GetBytes("Hello World")); // generating packet.
client.SendToServer(packet); // sending packet to server.
```

### UDP
- Initialization
```cs
var udp = new Udp(new IPEndPoint(IPAddress.Loopback, 7337));
udp.OnEndPointReceived += UdpOnEndPointReceived; // callback for received end-point.
```
