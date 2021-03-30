using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace ex1.Model
{
    interface IFlightGearModel : INotifyPropertyChanged
    {
        // need to add properties for the details of the flight, such as height, speed, etc.

        public StreamReader DetailsCSV { get; set; }
        public bool RenderingStopped { get; set; }
        public int Rate { get; set; }

        public double Altimeter { get; set; }
        public double AirSpeed { get; set; }
        public double Direction { get; set; }
        public double Pitch { get; set; }
        public double Row { get; set; }
        public double Yaw { get; set; }

        public void ConnectToFG(string ip, int port);
        public void DisconnectFromFG();
        public void Render();
    }
}
