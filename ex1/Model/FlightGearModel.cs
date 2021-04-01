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

        private volatile int _currentFramePosition = 0;
        private volatile bool _renderingStopped = false;
        private volatile int _frameRate = 20;

        public FlightGearModel(IAsyncFGClient fgClient)
        {
            _fgClient = fgClient;
        }

        public List<Frame> Frames { get; set; }
        public int CurrentFramePosition
        {
            get => _currentFramePosition;
            set
            {
                _currentFramePosition = value;
                NotifyPropertyChanged(nameof(CurrentFramePosition));
            }
        }
        public Frame CurrentFrame
        {
            get
            {
                try
                {
                    return Frames[CurrentFramePosition];
                }
                catch (Exception e)
                {
                    return new Frame();
                }
            }
        }

        public int FrameRate { get => _frameRate; set => _frameRate = value; }
        public bool RenderingStopped { get => _renderingStopped; set => _renderingStopped = value; }

        public Task Render()
        {
            return Task.Run(async () =>
            {
                while (!RenderingStopped && CurrentFramePosition < Frames.Count - 1)
                {
                    //_fgClient.Send(CurrentFrame.ToString());
                    ++CurrentFramePosition;

                    await Task.Delay(1000 / FrameRate);
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
