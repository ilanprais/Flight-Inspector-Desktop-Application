using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ex1.Model
{
    public interface IAsyncFGClient
    {
                //Connects to the flightgear
        public Task Connect(string ip, int port);
        
                //Disconnects from the flightgear
        public Task Disconnect();

                //Sends a message to the flightgear
        public Task Send(string message);
    }
}
