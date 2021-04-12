using System.ComponentModel;
using System.Collections.Generic;
using OxyPlot;
using ex1.Model;
using System;

namespace ex1.ViewModel
{
    public class GraphsViewModel : AbstractNotifier
    {
        private readonly IFlightGearModel _model;

        private Dictionary<string, RandomVariable> _properties;

        private string _currentProperty;
        private string _currentCorelativeProperty;

        public GraphsViewModel(IFlightGearModel model)
        {
            _model = model;
            _model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Frames")
                {
                    _properties = new Dictionary<string, RandomVariable>();

                    foreach (var property in Frame.Properties)
                    {
                        var values = new List<double>();
                        foreach (var frame in _model.Frames)
                        {
                            values.Add(frame.ValuesMap[property]);
                        }

                        _properties[property] = new RandomVariable(values); 
                    }

                    NotifyPropertyChanged(nameof(CurrentPropertyValues));
                    NotifyPropertyChanged(nameof(CurrentCorelativePropertyValues));
                }
                else if (e.PropertyName == "CurrentFramePosition")
                {
                    NotifyPropertyChanged(nameof(CurrentPropertyValues));
                    NotifyPropertyChanged(nameof(CurrentCorelativePropertyValues));
                }
                else
                {
                    NotifyPropertyChanged(e.PropertyName);
                }
            };
        }

        public List<DataPoint> CurrentPropertyValues
        {
            get
            {
                if (_currentProperty == null || !_properties.ContainsKey(_currentProperty))
                {
                    return new List<DataPoint>();
                }
                var propertyValues = _properties[_currentProperty].Values;

                var currentPropertyValues = new List<DataPoint>();
                for (var i = 0; i < _model.CurrentFramePosition; ++i)
                {
                    currentPropertyValues.Add(new DataPoint(i, propertyValues[i]));
                }

                return currentPropertyValues;
            }
        }

        public List<DataPoint> CurrentCorelativePropertyValues
        {
            get
            {
                if (_currentCorelativeProperty == null || !_properties.ContainsKey(_currentCorelativeProperty))
                {
                    return new List<DataPoint>();
                }
                var corelativePropertyValues = _properties[_currentCorelativeProperty].Values;

                var currentCorelativePropertyValues = new List<DataPoint>();
                for (var i = 0; i < _model.CurrentFramePosition; ++i)
                {
                    currentCorelativePropertyValues.Add(new DataPoint(i, corelativePropertyValues[i]));
                }

                return currentCorelativePropertyValues;
            }
        }

        public void ChangeField(string field)
        {
            _currentProperty = field;
            
            var biggestPCC = -1;
            foreach (var item in _properties)
            {
                if (item.Key == _currentProperty)
                {
                    continue;
                }

                var pcc = RandomVariable.PCC(_properties[_currentProperty], item.Value);
                if (pcc > biggestPCC)
                {
                    _currentCorelativeProperty = item.Key;
                }
            }
            

            NotifyPropertyChanged(nameof(CurrentPropertyValues));
            NotifyPropertyChanged(nameof(CurrentCorelativePropertyValues));
        }
    }
}
