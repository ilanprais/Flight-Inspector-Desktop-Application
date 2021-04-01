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
                if (e.PropertyName == "CurrentFramePosition")
                {
                    NotifyPropertyChanged(nameof(Altimeter));
                    NotifyPropertyChanged(nameof(AirSpeed));
                    NotifyPropertyChanged(nameof(Direction));
                    NotifyPropertyChanged(nameof(Pitch));
                    NotifyPropertyChanged(nameof(Row));
                    NotifyPropertyChanged(nameof(Yaw));
                }
                else if (e.PropertyName == "Frames")
                {
                    NotifyPropertyChanged(nameof(FramesNumber));
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
                    return _model.Frames.Count;
                }
            }
        }
        public int FrameRate
        {
            get => _model.FrameRate;
            set
            {
                if (value > 40)
                {
                    _model.FrameRate = 40;
                }
                else if (value < 10)
                {
                    _model.FrameRate = 10;
                }
                else
                {
                    _model.FrameRate = value;
                }
            }
        }
        public int CurrentFramePosition { get => _model.CurrentFramePosition; set => _model.CurrentFramePosition = value; }

        public double Altimeter { get => Math.Round(_model.CurrentFrame.Altimeter, 1); }
        public double AirSpeed { get => Math.Round(_model.CurrentFrame.AirSpeed, 1); }
        public double Direction { get => Math.Round(_model.CurrentFrame.Direction, 1); }
        public double Pitch { get => Math.Round(_model.CurrentFrame.Pitch, 1); }
        public double Row { get => Math.Round(_model.CurrentFrame.Row, 1); }
        public double Yaw { get => Math.Round(_model.CurrentFrame.Yaw, 1); }

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
