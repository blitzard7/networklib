using System;
using System.Collections.Generic;
using System.Net;
using NetworkLib.Client;
using NetworkLib.Events;

namespace NetworkLib.Contracts
{
    public interface IClient
    {
        void Start();
        void Stop();
        void SendToServer(IEnumerable<byte> data);
    }
}