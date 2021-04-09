using ex1.Model;
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

        public double Altimeter { get => _model.CurrentFrame.ValuesMap["altimeter"]; }
        public double AirSpeed { get => _model.CurrentFrame.ValuesMap["airspeed"]; }
        public double Direction { get => 0; }
        public double Pitch { get => _model.CurrentFrame.ValuesMap["pitch"]; }
        public double Roll { get => _model.CurrentFrame.ValuesMap["roll"]; }
        public double Yaw { get => _model.CurrentFrame.ValuesMap["yaw"]; }
    }
}
