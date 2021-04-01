using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace ex1.View
{
    class TimeValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan t = TimeSpan.FromSeconds(System.Convert.ToDouble(values[0]) / System.Convert.ToInt32(values[1]));
            return string.Format("{0:D2}:{1:D2}:{2:D2}",
                t.Minutes,
                t.Seconds,
                t.Milliseconds / 10);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
