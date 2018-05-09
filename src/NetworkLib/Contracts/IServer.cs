using System;
using System.Collections.Generic;
using System.Net;
using NetworkLib.Events;
using NetworkLib.Server;

namespace NetworkLib.Contracts
{
    public interface IServer
    {
        void Start();
        void Stop();
        void SendToClient(IEnumerable<byte> data);
    }
}