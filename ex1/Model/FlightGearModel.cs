using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.IO;

namespace ex1.Model
{
    public class FlightGearModel : IFlightGearModel
    {
        //Member Fields
        private readonly IAsyncFGClient _fgClient;
        private readonly IFlightAnomalyDetector _anomalyDetector;

        private List<Frame> _frames = null;
        private Dictionary<int, List<string>> _anomalyDetails = null;

        private volatile int _currentFramePosition = 0;

        private volatile bool _renderingStopped = false;
        private double _velocity = 1;
        private const int _frameRate = 20;

        //Constructor
        public FlightGearModel(IAsyncFGClient fgClient, IFlightAnomalyDetector anomalyDetector)
        {
            _fgClient = fgClient;
            _anomalyDetector = anomalyDetector;
        }

        //Properties

        public List<Frame> Frames
        {
            get => _frames;
            set
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

        //Renders the model
        public Task Render()
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    var task = _fgClient.Send(CurrentFrame.ToString());

                    if (!RenderingStopped && CurrentFramePosition < Frames.Count - 1)
                    {
                        ++CurrentFramePosition;
                    }

                    await Task.Delay((int)(1000 / ((double)FrameRate * Velocity)));
                    await task;
                }
            });
        }
                //Connects to the flightgear
        public Task ConnectToFG(string ip, int port)
        {
            return _fgClient.Connect(ip, port);
        }
        
                //Disconnects from the flightgear
        public Task DisconnectFromFG()
        {
            return _fgClient.Disconnect();
        }

        //Checks if there is an anomaly
        public void DetectAnomaly(string flightDataFilePath)
        {
            AnomalyDetails = _anomalyDetector.DetectAnomaly(flightDataFilePath);
        }

        //Finds the most corelative variable
        public RandomVariable FindMostCorelative(RandomVariable variable, List<RandomVariable> variables)
        {
            return variable.FindMostCorelative(variables);
        }
        
                //Calculates the linear regression
        public (double, double) LinearRegression(RandomVariable first, RandomVariable second)
        {
            return first.LinearRegression(second);
        }

        //Event handler for when a property is changes
        public event PropertyChangedEventHandler PropertyChanged;

        //Notifies the given property that it was changed an updates it
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
