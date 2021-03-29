using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ComponentModel;
using ex1.Model;

namespace ex1.ViewModel
{
    class FlightGearViewModel : INotifyPropertyChanged
    {
        private IFlightGearModel _model;

        FlightGearViewModel(IFlightGearModel model)
        {
            _model = model;
            model.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e) {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        public double Altimeter
        {
            get => _model.Altimeter;
        }
        public double AirSpeed
        {
            get => _model.AirSpeed;
        }
        public double Direction
        {
            get => _model.Direction;
        }
        public double Pitch
        {
            get => _model.Pitch;
        }
        public double Row
        {
            get => _model.Row;
        }
        public double Yaw
        {
            get => _model.Yaw;
        }

        public void Render()
        {
            _model.Render();
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
