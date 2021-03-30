using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;

namespace ex1.Model
{
    public class FlightGearModel : IFlightGearModel
    {
        private IFGClient _fgClient;

        private int _currentFramePosition = 0;

        private volatile bool _renderingStopped = false;
        private volatile int _frameRate;
        public string FilePath { get; set; }

        private List<Frame> _frames;
        public Frame CurrentFrame
        {
            get
            {
                if (_frames != null)
                {
                    return _frames[_currentFramePosition];
                }
                return new Frame();
            }
            set
            {
                _frames[_currentFramePosition] = value;
                NotifyPropertyChanged(nameof(CurrentFrame));
            }
        }

        public bool RenderingStopped { get => _renderingStopped; set => _renderingStopped = value; }
        public int FrameRate { get => _frameRate; set => _frameRate = value; }

        public void ConnectToFG(string ip, int port)
        {
            _fgClient.Connect(ip, port);
        }

        public void DisconnectFromFG()
        {
            _fgClient.Disconnect();
        }
        public double Altitude
        {
            get
            {
                return CurrentFrame.Altimeter;
            }
            set
            {
                CurrentFrame.Altimeter = value;
                NotifyPropertyChanged(nameof(Altitude));

            }
        }

        public void Render()
        {
            new Thread(delegate ()
            {
                //IEnumerable<Frame> frames = new List<Frame>();
                _currentFramePosition = 0;
                List<string> lines = new List<string>();
                _frames = new List<Frame>();
                System.IO.StreamReader file = new System.IO.StreamReader("C:\\Users\\danbi\\IdeaProjects\\ctf\\src\\reg_flight.csv");
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    lines.Add(line);
                    _frames.Add(new Frame(line));
                }
                file.Close();
                TcpClient tcpClient = new TcpClient();

                //tcpClient.Connect("localhost", 5400);

                for (; !_renderingStopped && _currentFramePosition < _frames.Count-1; )
                {
                    Altitude= _frames[_currentFramePosition].Altimeter;
                    //var frame = Frames[_currentFramePosition];
                    //tcpClient.GetStream().Write(System.Text.Encoding.ASCII.GetBytes(lines[_currentFramePosition]),0, lines[_currentFramePosition].Length);
                    //Task.Run(() => _fgClient.Send(frame.ToString()));
                    Thread.Sleep(1);
                    ++_currentFramePosition;


                }
            }).Start();
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
