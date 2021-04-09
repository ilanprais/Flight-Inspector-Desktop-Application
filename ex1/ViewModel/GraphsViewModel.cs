using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using OxyPlot;
using ex1.Model;

namespace ex1.ViewModel
{
    public class GraphsViewModel : AbstractNotifier
    {
        private IFlightGearModel _model;

        private Dictionary<string, ObservableCollection<DataPoint>> _fieldValues = new Dictionary<string, ObservableCollection<DataPoint>>();
        private string _currentField = "altimeter";

        public GraphsViewModel(IFlightGearModel model)
        {
            _model = model;
            _model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Frames")
                {
                    var frame = new Frame();
                    foreach (var item in frame.ValuesMap)
                    {
                        _fieldValues[item.Key] = new ObservableCollection<DataPoint>();
                    }

                    for (var i = 0; i < _model.Frames.Count; ++i)
                    {
                        foreach (var item in _model.Frames[i].ValuesMap)
                        {
                            _fieldValues[item.Key].Add(new DataPoint(i, item.Value));
                        }
                    }

                    NotifyPropertyChanged(nameof(CurrentFieldValues));
                }
                else if (e.PropertyName == "CurrentFramePosition")
                {
                    NotifyPropertyChanged(nameof(CurrentFieldValues));
                }
                else
                {
                    NotifyPropertyChanged(e.PropertyName);
                }
            };
        }

        public ObservableCollection<DataPoint> CurrentFieldValues
        {
            get
            {
                var currentFieldsList = new ObservableCollection<DataPoint>();
                for (var i = 0; i < _model.CurrentFramePosition; ++i)
                {
                    currentFieldsList.Add(_fieldValues[_currentField][i]);
                }

                return currentFieldsList;
            }
        }

        public void ChangeField(string field)
        {
            _currentField = field;
            NotifyPropertyChanged(nameof(CurrentFieldValues));
        }
    }
}
