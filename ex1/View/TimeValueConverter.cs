using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace ex1.View
{
    public class TimeValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan t = TimeSpan.FromSeconds((double) value / (int) parameter);
            return string.Format("{0:D2}:{1:D2}:{2:D1}",
                t.Minutes,
                t.Seconds,
                t.Milliseconds / 10);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
