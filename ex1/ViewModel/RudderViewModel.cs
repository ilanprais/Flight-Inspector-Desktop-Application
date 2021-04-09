﻿using System.ComponentModel;
using ex1.Model;

namespace ex1.ViewModel
{
    public class RudderViewModel : AbstractNotifier
    {
        private IFlightGearModel _model;

        public RudderViewModel(IFlightGearModel model)
        {
            _model = model;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "CurrentFramePosition")
                {
                    NotifyPropertyChanged(nameof(Aileron));
                    NotifyPropertyChanged(nameof(Elevator));
                    NotifyPropertyChanged(nameof(Rudder));
                    NotifyPropertyChanged(nameof(Throttle));
                }
                else
                {
                    NotifyPropertyChanged(e.PropertyName);
                }
            };
        }

        public double Aileron { get => _model.CurrentFrame.ValuesMap["aileron"]; }
        public double Elevator { get => _model.CurrentFrame.ValuesMap["elevator"]; }
        public double Rudder { get => _model.CurrentFrame.ValuesMap["rudder"]; }
        public double Throttle { get => _model.CurrentFrame.ValuesMap["throttle"]; }
    }
}
