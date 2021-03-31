using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ex1.Model
{
    public interface IAsyncFGClient
    {
        public Task Connect(string ip, int port);
        public Task Disconnect();

        public Task Send(string message);
    }
}
