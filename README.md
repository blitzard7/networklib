# networklib 
## Build Status
[![Build Status](https://samanta-mika.visualstudio.com/_apis/public/build/definitions/60e9c34c-6d2b-4310-a617-5b28af2c0fcf/5/badge)](https://samanta-mika.visualstudio.com/Networklibrary/_build/index?definitionId=5)
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
#
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


#
## Contribute
If you are interested on contributing please read [CONTRIBUTING.md](https://github.com/samantaSophia/networklib/blob/master/CONTRIBUTING.md)

#
## How to use it in your solution
1. Download this repository and build it
2. Add a reference to the built NetworkLib.dll in your solution
3. Add the following using-statement to your project files:
```cs
using NetworkLib;
```
