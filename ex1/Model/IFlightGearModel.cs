using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Net.Sockets;

namespace ex1.Model
{
    interface IFlightGearModel : INotifyPropertyChanged
    {
        public TcpClient TCPClient { get; set; }

        // need to add properties for the details of the flight, such as height, speed, etc.

        public void RenderDetails(string details);
    }
}
