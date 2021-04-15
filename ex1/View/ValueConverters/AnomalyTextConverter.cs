using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;

namespace ex1.View.ValueConverters
{
    class AnomalyTextConverter : IMultiValueConverter
    {
            //Returns the fitting text for the anomaly status window
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (int frame in (values[1] as Dictionary<int, List<string>>).Keys)
            {
                if (frame == (int)values[0])
                {
                    return "Anomaly Detected";
                }
            }
            return "No Anomaly Detected";
        }

        //Converts back
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
