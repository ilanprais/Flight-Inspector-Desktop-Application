using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
using ex1.Model;
using System.Net.Sockets;

namespace ex1.ViewModel
{
    public class FlightGearViewModel : INotifyPropertyChanged
    {
        private IFlightGearModel _model;

        private string _filePathName;
        private bool _isConnected = false;

        public FlightGearViewModel(IFlightGearModel model)
        {
            _model = model;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == nameof(CurrentFramePosition))
                {
                    NotifyPropertyChanged(nameof(Altimeter));
                    NotifyPropertyChanged(nameof(AirSpeed));
                    NotifyPropertyChanged(nameof(Direction));
                    NotifyPropertyChanged(nameof(Pitch));
                    NotifyPropertyChanged(nameof(Row));
                    NotifyPropertyChanged(nameof(Yaw));

                    NotifyPropertyChanged(nameof(CurrentFramePosition));
                    NotifyPropertyChanged(nameof(CurrentTime));
                }
                else
                {
                    NotifyPropertyChanged(e.PropertyName);
                }
            };
        }

        public string FilePath
        {
            get => _filePathName;
            set
            {
                _model.Frames = new List<Frame>();

                using (StreamReader file = new StreamReader(value))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        _model.Frames.Add(new Frame(line));
                    }
                }

                NotifyPropertyChanged(nameof(FramesNumber));

                _filePathName = value;
                NotifyPropertyChanged(nameof(FilePath));
            }
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
        public int Velocity
        {
            get => _model.FrameRate;
            set
            {
                if (value > 3)
                {
                    _model.Velocity = 3;
                }
                else if (value < 0)
                {
                    _model.Velocity = 0;
                }
                else
                {
                    _model.Velocity = value;
                }
            }
        }
        public int CurrentFramePosition { get => _model.CurrentFramePosition; set => _model.CurrentFramePosition = value; }
        public string CurrentTime
        {
            get
            {
                TimeSpan t = TimeSpan.FromSeconds((double) CurrentFramePosition / _model.FrameRate);
                return string.Format("{0:D2}:{1:D2}:{2:D1}",
                    t.Minutes,
                    t.Seconds,
                    t.Milliseconds / 10);
            }
        }

        public double Altimeter { get => Math.Round(_model.CurrentFrame.Altimeter, 1); }
        public double AirSpeed { get => Math.Round(_model.CurrentFrame.AirSpeed, 1); }
        public double Direction { get => Math.Round(_model.CurrentFrame.Direction, 1); }
        public double Pitch { get => Math.Round(_model.CurrentFrame.Pitch, 1); }
        public double Row { get => Math.Round(_model.CurrentFrame.Row, 1); }
        public double Yaw { get => Math.Round(_model.CurrentFrame.Yaw, 1); }

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
