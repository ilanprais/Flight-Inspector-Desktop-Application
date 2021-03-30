using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;

namespace ex1.Model
{
    class FlightGearModel : IFlightGearModel
    {
        private IFGClient _fgClient;

        private int _currentFramePosition;

        private volatile bool _renderingStopped = false;
        private volatile int _frameRate;

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
            for (var i = _currentFramePosition; !_renderingStopped && _currentFramePosition < Frames.Count; ++i)
            {
                var frame = Frames[_currentFramePosition];
                Task.Run(() => _fgClient.Send(frame.ToString()));
                CurrentFrame = frame;

                Thread.Sleep(1000 / FrameRate);
            }
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
