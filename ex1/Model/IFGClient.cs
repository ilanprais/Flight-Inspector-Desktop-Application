using System;
using System.Collections.Generic;
using System.Text;

namespace ex1.Model
{
    interface IFGClient
    {
        public void Connect(string ip, int port);
        public void Disconnect();

        public void Send(string message);
    }
}
