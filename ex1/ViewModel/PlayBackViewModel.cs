using System.Collections.Generic;
using System.ComponentModel;
using ex1.Model;

namespace ex1.ViewModel
{
    public class PlayBackViewModel : AbstractNotifier
    {
        private readonly IFlightGearModel _model;

        public PlayBackViewModel(IFlightGearModel model)
        {
            _model = model;
            _model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Frames")
                {
                    NotifyPropertyChanged(nameof(FramesNumber));
                }
                else
                {
                    NotifyPropertyChanged(e.PropertyName);
                }
            };
        }

        public Dictionary<int, List<string>> AnomalyDict
        {
            get
            {
                Dictionary<int, List<string>> dict = new Dictionary<int, List<string>>();
                dict[FramesNumber / 2] = new List<string>();
                dict[FramesNumber / 2].Add("aileron");
                dict[FramesNumber / 2].Add("altitude");

                dict[FramesNumber / 3] = new List<string>();
                dict[FramesNumber / 3].Add("speed");
                dict[FramesNumber / 3].Add("bob");

                dict[FramesNumber / 4] = new List<string>();
                dict[FramesNumber / 4].Add("speed");
                dict[FramesNumber / 4].Add("bob");

                dict[FramesNumber] = new List<string>();
                dict[FramesNumber].Add("yaw");
                dict[FramesNumber].Add("pitch");
                dict[FramesNumber].Add("roll");

                return dict;

            }
        }
        public int CurrentFramePosition { get => _model.CurrentFramePosition; set => _model.CurrentFramePosition = value; }
        public double Velocity { get => _model.Velocity; set => _model.Velocity = value; }
        public int FrameRate { get => _model.FrameRate; }
        public int FramesNumber
        {
            get
            {
                if (_model.Frames == null)
                {
                    return 2000;
                }
                else
                {
                    return _model.Frames.Count - 1;
                }
            }
        }

        public void Render()
        {
            _model.RenderingStopped = false;
            _model.Render();
        }

        public void PauseRendering()
        {
            _model.RenderingStopped = true;
        }

        public void ResumeRendering()
        {
            _model.RenderingStopped = false;
        }
    }
}
