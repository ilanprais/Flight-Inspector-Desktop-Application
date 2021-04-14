using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.IO;

namespace ex1.Model
{
    public class FlightGearModel : IFlightGearModel
    {
        private readonly IAsyncFGClient _fgClient;
        private readonly IFlightAnomalyDetector _anomalyDetector;

        private string _flightDataFilePath;

        private List<Frame> _frames = null;
        private Dictionary<int, List<string>> _anomalyDetails = null;

        private volatile int _currentFramePosition = 0;

        private volatile bool _renderingStopped = false;
        private double _velocity = 1;
        private const int _frameRate = 20;

        public FlightGearModel(IAsyncFGClient fgClient, IFlightAnomalyDetector anomalyDetector)
        {
            _fgClient = fgClient;
            _anomalyDetector = anomalyDetector;
        }

        public string FlightDataFilePath
        {
            get => _flightDataFilePath;
            set
            {
                _flightDataFilePath = value;

                var frames = new List<Frame>();

                using (StreamReader file = new StreamReader(value))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        frames.Add(new Frame(line));
                    }
                }

                Frames = frames;
                AnomalyDetails = null;
            }
        }

        public List<Frame> Frames
        {
            get => _frames;
            private set
            {
                _frames = value;
                NotifyPropertyChanged(nameof(Frames));
            }
        }
        public Dictionary<int, List<string>> AnomalyDetails
        {
            get => _anomalyDetails;
            set
            {
                _anomalyDetails = value;
                NotifyPropertyChanged(nameof(AnomalyDetails));
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

        public void DetectAnomaly()
        {
            AnomalyDetails = _anomalyDetector.DetectAnomaly(FlightDataFilePath);
        }

        public RandomVariable FindMostCorelative(RandomVariable variable, List<RandomVariable> variables)
        {
            return variable.FindMostCorelative(variables);
        }
        public (double, double) LinearRegression(RandomVariable first, RandomVariable second)
        {
            return first.LinearRegression(second);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
