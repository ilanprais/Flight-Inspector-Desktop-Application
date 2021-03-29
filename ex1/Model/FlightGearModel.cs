using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Net.Sockets;

namespace ex1.Model
{
    class FlightGearModel : IFlightGearModel
    {
        private TcpClient _fgClient;

        private double _altimeter;
        private double _airSpeed;
        private double _direction;
        private double _pitch;
        private double _row;
        private double _yaw;

        public StreamReader DetailsCSV { get; set; }
        public bool RenderingStopped { get; set; }
        public int Rate { get; set; }

        public double Altimeter
        {
            get => _altimeter;
            set
            {
                _altimeter = value;
                NotifyPropertyChanged(nameof(Altimeter));
            }
        }
        public double AirSpeed
        {
            get => _airSpeed;
            set
            {
                _altimeter = value;
                NotifyPropertyChanged(nameof(AirSpeed));
            }
        }
        public double Direction
        {
            get => _direction;
            set
            {
                _altimeter = value;
                NotifyPropertyChanged(nameof(Direction));
            }
        }
        public double Pitch
        {
            get => _pitch;
            set
            {
                _altimeter = value;
                NotifyPropertyChanged(nameof(Pitch));
            }
        }
        public double Row
        {
            get => _row;
            set
            {
                _altimeter = value;
                NotifyPropertyChanged(nameof(Row));
            }
        }
        public double Yaw
        {
            get => _yaw;
            set
            {
                _altimeter = value;
                NotifyPropertyChanged(nameof(Yaw));
            }
        }

        public void ConnectToFG(string ip, int port)
        {
            throw new NotImplementedException();
        }

        public void Render()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
