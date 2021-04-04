using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace ex1.View
{
    class ControllerValuesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {/*
            var aileron = System.Convert.ToDouble(values[0]);
            var elevator = System.Convert.ToDouble(values[1]);

            return (30 + Sigmoid(aileron)) + "," + (30 + Sigmoid(elevator)) + ","
                + (30 - Sigmoid(aileron)) + "," + (30 - Sigmoid(elevator));*/
            return "30,30,30,30";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }

        private float Sigmoid(double value)
        {
            return (float) (1.0 / (1.0 + Math.Pow(Math.E, -value))) * 50 - 25;
        }
    }
}
