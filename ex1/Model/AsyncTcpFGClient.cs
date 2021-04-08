using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ex1.Model
{
    public class AsyncTcpFGClient : IAsyncFGClient
    {
        readonly TcpClient _tcpClient = new TcpClient();

        public Task Connect(string ip, int port)
        {
            return _tcpClient.ConnectAsync(ip, port);
        }
        public Task Disconnect()
        {
            return _tcpClient.GetStream().DisposeAsync().AsTask();
        }

        public Task Send(string message)
        {
            return _tcpClient.GetStream().WriteAsync(Encoding.ASCII.GetBytes(message + "\n")).AsTask();
        }
    }
}
