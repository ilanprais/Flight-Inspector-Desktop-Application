﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;
using System.Collections.Generic;

namespace ex1.View.ValueConverters
{
    class AnomalyTextConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (int frame in (values[1] as List<int>))
            {
                if (frame == (int)values[0])
                {
                    return "Anomaly Detected";
                }
            }
            return "No Anomaly Detected";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
