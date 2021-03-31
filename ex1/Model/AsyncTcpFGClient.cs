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

        public async Task Connect(string ip, int port)
        {
            await _tcpClient.ConnectAsync(ip, port);
        }
        public async Task Disconnect()
        {
            await _tcpClient.GetStream().DisposeAsync();
        }

        public async Task Send(string message)
        {
            await _tcpClient.GetStream().WriteAsync(Encoding.ASCII.GetBytes(message));
        }
    }
}
