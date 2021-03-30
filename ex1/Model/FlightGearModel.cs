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

        public List<Frame> Frames { get; set; }
        public Frame CurrentFrame
        {
            get => Frames[_currentFramePosition];
            set
            {
                Frames[_currentFramePosition] = value;
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

        public void Render()
        {
            new Thread(delegate ()
            {
                //IEnumerable<Frame> frames = new List<Frame>();
                List<string> lines = new List<string>();
                System.IO.StreamReader file = new System.IO.StreamReader(FilePath);
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    lines.Add(line);
                }
                file.Close();
                TcpClient tcpClient = new TcpClient();

                tcpClient.Connect("localhost", 5400);

                for (; !_renderingStopped && _currentFramePosition < lines.Count; ++_currentFramePosition)
                {
                    //var frame = Frames[_currentFramePosition];
                    tcpClient.GetStream().Write(System.Text.Encoding.ASCII.GetBytes(lines[_currentFramePosition]));
                    //Task.Run(() => _fgClient.Send(frame.ToString()));
                    Thread.Sleep(100);
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
