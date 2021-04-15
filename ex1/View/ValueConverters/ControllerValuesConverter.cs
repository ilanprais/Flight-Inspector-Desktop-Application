using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace ex1.View.ValueConverters
{
    class ControllerValuesConverter : IMultiValueConverter
    {       
        //Returns thr fitting thickness
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var aileron = System.Convert.ToDouble(values[0]);
            var elevator = System.Convert.ToDouble(values[1]);

            return new Thickness(30 + Sigmoid(aileron), 30 + Sigmoid(elevator), 30 - Sigmoid(aileron), 30 - Sigmoid(elevator));
        }

        //Converts back
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        //Calculates the sigmoid of a value
        private float Sigmoid(double value)
        {
            return (float) (1.0 / (1.0 + Math.Pow(Math.E, -value))) * 150 - 75;
        }
    }
}
