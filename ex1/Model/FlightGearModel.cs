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
        private IAsyncFGClient _fgClient;

        private Frame _currentFrame = new Frame();
        private int _currentFramePosition = 0;

        private volatile bool _renderingStopped = false;
        private volatile int _frameRate = 10;

        public FlightGearModel(IAsyncFGClient fgClient)
        {
            _fgClient = fgClient;
        }

        public List<Frame> Frames { get; set; }
        public Frame CurrentFrame
        {
            get => _currentFrame;
            set
            {
                _currentFrame = value;
                NotifyPropertyChanged(nameof(CurrentFrame));
            }
        }

        public bool RenderingStopped { get => _renderingStopped; set => _renderingStopped = value; }
        public int FrameRate { get => _frameRate; set => _frameRate = value; }

        public void Render()
        {
            Task.Run(() =>
            {
                for (; !RenderingStopped && _currentFramePosition < Frames.Count - 1; ++_currentFramePosition)
                {
                    var frame = Frames[_currentFramePosition];
                    _fgClient.Send(_currentFrame.ToString());

                    CurrentFrame = frame;

                    Thread.Sleep(1000 / FrameRate);
                }
            });
        }

        public Task ConnectToFG(string ip, int port)
        {
            return _fgClient.Connect(ip, port);
        }

        public Task DisconnectFromFG()
        {
            return _fgClient.Disconnect();
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
