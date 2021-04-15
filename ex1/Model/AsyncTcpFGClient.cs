using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace ex1.Model
{
    public class AsyncTcpFGClient : IAsyncFGClient
    {
            //Member Field
        readonly TcpClient _tcpClient = new TcpClient();

        //Connect method
        public Task Connect(string ip, int port)
        {
            return _tcpClient.ConnectAsync(ip, port);
        }
        
                //Disconnect method
        public Task Disconnect()
        {
            return _tcpClient.GetStream().DisposeAsync().AsTask();
        }

        //Method to send a string to the Flightgear
        public Task Send(string message)
        {
            return _tcpClient.GetStream().WriteAsync(Encoding.ASCII.GetBytes(message + "\n")).AsTask();
        }
    }
}
