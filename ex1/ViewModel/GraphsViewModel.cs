using System.ComponentModel;
using System.Collections.Generic;
using OxyPlot;
using System.Linq;
using System;
using ex1.Model;

namespace ex1.ViewModel
{
    public class GraphsViewModel : AbstractNotifier
    {
        private readonly IFlightGearModel _model;

        private Dictionary<string, RandomVariable> _properties;

        public static readonly List<string> Properties = Frame.Properties;

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

                    CurrentProperty = null;
                    CurrentCorelativeProperty = null;
                }
                else if (e.PropertyName == "CurrentFramePosition")
                {
                    NotifyPropertyChanged(nameof(CurrentPropertyValues));
                    NotifyPropertyChanged(nameof(CurrentCorelativePropertyValues));
                    NotifyPropertyChanged(nameof(LinearRegressionLine));
                    NotifyPropertyChanged(nameof(LinearRegressionPoints));
                }
                else
                {
                    NotifyPropertyChanged(e.PropertyName);
                }
            };
        }

        public string CurrentProperty { get; private set; }
        public string CurrentCorelativeProperty { get; private set; }

        public List<DataPoint> CurrentPropertyValues
        {
            get
            {
                var currentPropertyValues = new List<DataPoint>();

                if (CurrentProperty != null && _properties.ContainsKey(CurrentProperty))
                {
                    var propertyValues = _properties[CurrentProperty].Values;

                    for (var i = 0; i < _model.CurrentFramePosition; ++i)
                    {
                        currentPropertyValues.Add(new DataPoint(i, propertyValues[i]));
                    }
                }

                return currentPropertyValues;
            }
        }
        public List<DataPoint> CurrentCorelativePropertyValues
        {
            get
            {
                var currentCorelativePropertyValues = new List<DataPoint>();

                if (CurrentCorelativeProperty != null && _properties.ContainsKey(CurrentCorelativeProperty))
                {
                    var corelativePropertyValues = _properties[CurrentCorelativeProperty].Values;

                    for (var i = 0; i < _model.CurrentFramePosition; ++i)
                    {
                        currentCorelativePropertyValues.Add(new DataPoint(i, corelativePropertyValues[i]));
                    }
                }

                return currentCorelativePropertyValues;
            }
        }

        public List<DataPoint> LinearRegressionLine
        {
            get
            {
                if (_model.CurrentFramePosition == 0)
                {
                    return new List<DataPoint>();
                }

                var ab = _model.LinearRegression(_properties[CurrentProperty], _properties[CurrentCorelativeProperty]);
                return new List<DataPoint>
                {
                    new DataPoint(MinimumCurrentPropertyValue, ab.Item1 * MinimumCurrentPropertyValue + ab.Item2),
                    new DataPoint(MaximumCurrentPropertyValue, ab.Item1 * MaximumCurrentPropertyValue + ab.Item2)
                };
            }
        }
        public List<DataPoint> LinearRegressionPoints
        {
            get
            {
                var points = new List<DataPoint>();

                if (_model.CurrentFramePosition != 0)
                {
                    var maxFrameIndex = Math.Min(_model.Frames.Count, _model.CurrentFramePosition + 30 * _model.FrameRate);
                    for (var i = _model.CurrentFramePosition; i < maxFrameIndex; i += 15)
                    {
                        points.Add(new DataPoint(_properties[CurrentProperty].Values[i], _properties[CurrentCorelativeProperty].Values[i]));
                    }
                }

                return points;
            }
        }

        public double MaximumCurrentPropertyValue { get => _properties[CurrentProperty].Values.Max(); }
        public double MinimumCurrentPropertyValue { get => _properties[CurrentProperty].Values.Min(); }
        public double MaximumCurrentCorelativePropertyValue { get => _properties[CurrentCorelativeProperty].Values.Max(); }
        public double MinimumCurrentCorelativePropertyValue { get => _properties[CurrentCorelativeProperty].Values.Min(); }

        public void ChangeField(string field)
        {
            CurrentProperty = field;

            var mostCorelative = _model.FindMostCorelative(_properties[CurrentProperty], _properties.Values.ToList());
            CurrentCorelativeProperty = _properties.FirstOrDefault(x => x.Value == mostCorelative).Key;
            
            NotifyPropertyChanged(nameof(CurrentPropertyValues));
            NotifyPropertyChanged(nameof(CurrentCorelativePropertyValues));
            NotifyPropertyChanged(nameof(CurrentProperty));
            NotifyPropertyChanged(nameof(CurrentCorelativeProperty));

            NotifyPropertyChanged(nameof(LinearRegressionLine));
            NotifyPropertyChanged(nameof(LinearRegressionPoints));
            NotifyPropertyChanged(nameof(MaximumCurrentPropertyValue));
            NotifyPropertyChanged(nameof(MinimumCurrentPropertyValue));
            NotifyPropertyChanged(nameof(MaximumCurrentCorelativePropertyValue));
            NotifyPropertyChanged(nameof(MinimumCurrentCorelativePropertyValue));
        }
    }
}
