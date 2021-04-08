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

        public double Aileron { get => _model.CurrentFrame.Aileron; }
        public double Elevator { get => _model.CurrentFrame.Elevator; }
        public double Rudder { get => _model.CurrentFrame.Rudder; }
        public double Throttle { get => _model.CurrentFrame.Throttle; }
        public double Altimeter { get => _model.CurrentFrame.Altimeter; }
        public double AirSpeed { get => _model.CurrentFrame.AirSpeed; }
        public double Direction { get => _model.CurrentFrame.Direction; }
        public double Pitch { get => _model.CurrentFrame.Pitch; }
        public double Roll { get => _model.CurrentFrame.Roll; }
        public double Yaw { get => _model.CurrentFrame.Yaw; }

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
                await _model.ConnectToFG("127.0.0.1", 8081);
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
