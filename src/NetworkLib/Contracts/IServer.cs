using System.Collections.Generic;

namespace NetworkLib.Contracts
{
    public interface IServer
    {
        void Start();
        void Stop();
        void SendToClient(IEnumerable<byte> data);
    }
}