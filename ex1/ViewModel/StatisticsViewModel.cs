using ex1.Model;
using System;
using System.ComponentModel;

namespace ex1.ViewModel
{
    public class StatisticsViewModel : AbstractNotifier
    {
        private readonly IFlightGearModel _model;

        public StatisticsViewModel(IFlightGearModel model)
        {
            _model = model;
            model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e) {
                if (e.PropertyName == "CurrentFramePosition")
                {
                    NotifyPropertyChanged(nameof(Altimeter));
                    NotifyPropertyChanged(nameof(AirSpeed));
                    NotifyPropertyChanged(nameof(Direction));
                    NotifyPropertyChanged(nameof(Pitch));
                    NotifyPropertyChanged(nameof(Roll));
                    NotifyPropertyChanged(nameof(Yaw));
                }
                else
                {
                    NotifyPropertyChanged(e.PropertyName);
                }
            };
        }

        public double Altimeter 
        {
            get
            {
                try
                {
                    return _model.CurrentFrame.ValuesMap["altimeter-indicated-altitude-ft"];
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public double AirSpeed
        {
            get
            {
                try
                {
                    return _model.CurrentFrame.ValuesMap["airspeed-indicator-indicated-speed-kt"];
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public double Direction
        {
            get
            {
                try
                {
                    return _model.CurrentFrame.ValuesMap["side-slip-deg"];
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public double Pitch
        {
            get
            {
                try
                {
                    return _model.CurrentFrame.ValuesMap["pitch-deg"];
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public double Roll
        {
            get
            {
                try
                {
                    return _model.CurrentFrame.ValuesMap["roll-deg"];
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        public double Yaw
        {
            get
            {
                try
                {
                    return _model.CurrentFrame.ValuesMap["heading-deg"];
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}
