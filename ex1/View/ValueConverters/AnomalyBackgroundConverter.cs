using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;

namespace ex1.View.ValueConverters
{
    class AnomalyBackgroundConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach(int frame in (values[1] as Dictionary<int, List<string>>).Keys)
            {
                if(frame == (int)values[0])
                {
                    return Brushes.Red;
                }
            }
            return Brushes.Green;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
