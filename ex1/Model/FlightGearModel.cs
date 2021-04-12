using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ex1.Model
{
    public class FlightGearModel : IFlightGearModel
    {
        private readonly IAsyncFGClient _fgClient;

        private List<Frame> _frames = null;
        private volatile int _currentFramePosition = 0;

        private volatile bool _renderingStopped = false;
        private double _velocity = 1;
        private const int _frameRate = 20;

        public FlightGearModel(IAsyncFGClient fgClient)
        {
            _fgClient = fgClient;
        }

        public List<Frame> Frames
        {
            get => _frames;
            set
            {
                _frames = value;
                NotifyPropertyChanged(nameof(Frames));
            }
        }
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
                catch (Exception)
                {
                    return null;
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
                    //var task = _fgClient.Send(CurrentFrame.ToString());

                    if (!RenderingStopped && CurrentFramePosition < Frames.Count - 1)
                    {
                        ++CurrentFramePosition;
                    }

                    await Task.Delay((int)(1000 / ((double)FrameRate * Velocity)));
                    //await task;
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
