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

        public Frame CurrentFrame { get => _model.CurrentFrame; }

        public int FrameRate { set => _model.FrameRate = value; }

        public void Render()
        {
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
