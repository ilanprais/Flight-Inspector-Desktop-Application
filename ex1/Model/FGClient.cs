using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace ex1.Model
{
    class FGClient : IFGClient
    {
        TcpClient _tcpClient;

        public async void Connect(string ip, int port)
        {
            await _tcpClient.ConnectAsync(ip, port);
        }
        public void Disconnect()
        {
            _tcpClient.Dispose();
        }

        public async void Send(string message)
        {
            await _tcpClient.GetStream().WriteAsync(Encoding.ASCII.GetBytes(message));
        }
    }
}
