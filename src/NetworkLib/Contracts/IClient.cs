using System.Collections.Generic;

namespace NetworkLib.Contracts
{
    public interface IClient
    {
        void Start();
        void Stop();
        void SendToServer(IEnumerable<byte> data);
    }
}