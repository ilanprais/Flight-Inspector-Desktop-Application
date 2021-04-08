using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ex1.Model;

namespace ex1.ViewModel
{
    public class GeneralViewModel : AbstractNotifier
    {
        private IFlightGearModel _model;

        public GeneralViewModel(IFlightGearModel model)
        {
            _model = model;
            _model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        public void LoadFile(string filePath)
        {
            var frames = new List<Frame>();

            try
            {
                using (StreamReader file = new StreamReader(filePath))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        frames.Add(new Frame(line));
                    }
                }

                _model.Frames = frames;
            }
            catch (Exception e)
            {
                // need to handle exception
            }

            _model.CurrentFramePosition = 0;
        }
    }
}
