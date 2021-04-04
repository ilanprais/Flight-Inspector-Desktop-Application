using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using ex1.Model;

namespace ex1.ViewModel
{
    public class FlightGearViewModel : INotifyPropertyChanged
    {
        private IFlightGearModel _model;

        private bool _isConnected = false;

        public FlightGearViewModel(IFlightGearModel model)
        {
            _model = model;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == nameof(CurrentFramePosition))
                {
                    NotifyPropertyChanged(nameof(Aileron));
                    NotifyPropertyChanged(nameof(Elevator));
                    NotifyPropertyChanged(nameof(Rudder));
                    NotifyPropertyChanged(nameof(Throttle));

                    NotifyPropertyChanged(nameof(Altimeter));
                    NotifyPropertyChanged(nameof(AirSpeed));
                    NotifyPropertyChanged(nameof(Direction));
                    NotifyPropertyChanged(nameof(Pitch));
                    NotifyPropertyChanged(nameof(Roll));
                    NotifyPropertyChanged(nameof(Yaw));

                    NotifyPropertyChanged(nameof(CurrentFramePosition));
                }
                else
                {
                    NotifyPropertyChanged(e.PropertyName);
                }
            };
        }

        public int FramesNumber
        {
            get
            {
                if (_model.Frames == null)
                {
                    return 0;
                }
                else
                {
                    return _model.Frames.Count - 1;
                }
            }
        }
        public int CurrentFramePosition { get => _model.CurrentFramePosition; set => _model.CurrentFramePosition = value; }
        public double Velocity { get => _model.Velocity; set => _model.Velocity = value; }
        public int FrameRate { get => _model.FrameRate; }

        public double Aileron { get => Math.Round(_model.CurrentFrame.Aileron, 1); }
        public double Elevator { get => Math.Round(_model.CurrentFrame.Elevator, 1); }
        public double Rudder { get => Math.Round(_model.CurrentFrame.Rudder, 1); }
        public double Throttle { get => Math.Round(_model.CurrentFrame.Throttle, 1); }
        public double Altimeter { get => Math.Round(_model.CurrentFrame.Altimeter, 1); }
        public double AirSpeed { get => Math.Round(_model.CurrentFrame.AirSpeed, 1); }
        public double Direction { get => Math.Round(_model.CurrentFrame.Direction, 1); }
        public double Pitch { get => Math.Round(_model.CurrentFrame.Pitch, 1); }
        public double Roll { get => Math.Round(_model.CurrentFrame.Roll, 1); }
        public double Yaw { get => Math.Round(_model.CurrentFrame.Yaw, 1); }

        public void LoadFile(string filePath)
        {
            _model.Frames = new List<Frame>();

            try
            {
                using (StreamReader file = new StreamReader(filePath))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        _model.Frames.Add(new Frame(line));
                    }
                }
            }
            catch (Exception e)
            {
                // need to handle exception
            }

            NotifyPropertyChanged(nameof(FramesNumber));
        }

        public async void Render()
        {
            if (!_isConnected) {
                //await _model.ConnectToFG("127.0.0.1", 8081);
                _isConnected = true;
            }

            _model.RenderingStopped = false;
            _model.Render();
        }

        public void PauseRendering()
        {
            _model.RenderingStopped = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
