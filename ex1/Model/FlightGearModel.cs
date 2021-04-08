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
        private double _velocity = 1;

        private readonly int _frameRate = 20;

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
                if (Frames == null)
                {
                    return new Frame();
                }
                else
                {
                    return Frames[CurrentFramePosition];
                }
            }
        }

        public double Velocity { get => _velocity; set => _velocity = value; }
        public bool RenderingStopped { get => _renderingStopped; set => _renderingStopped = value; }
        public int FrameRate { get => _frameRate; }

        public Task Render()
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    var task = _fgClient.Send(CurrentFrame.ToString());

                    if (!RenderingStopped)
                    {
                        ++CurrentFramePosition;
                    }

                    await Task.Delay((int)(1000 / ((double)FrameRate * Velocity)));
                    await task;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
