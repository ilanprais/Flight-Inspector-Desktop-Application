using System;
using System.ComponentModel;
using ex1.Model;

namespace ex1.ViewModel
{
    public class RudderViewModel : AbstractNotifier
    {
        private readonly IFlightGearModel _model;

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

        public double Aileron
        {
            get
            {
                try
                {
                    return _model.CurrentFrame.ValuesMap["aileron"];
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public double Elevator
        {
            get
            {
                try
                {
                    return _model.CurrentFrame.ValuesMap["elevator"];
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public double Rudder
        {
            get
            {
                try
                {
                    return _model.CurrentFrame.ValuesMap["rudder"];
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public double Throttle
        {
            get
            {
                try
                {
                    return _model.CurrentFrame.ValuesMap["throttle"];
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}
