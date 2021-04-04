using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Controls;

namespace ex1.View
{
    class PlayButtonTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (System.Convert.ToInt32(value) == PlaybackWindow.Current.slider.Maximum - 1)
            {
                PlaybackWindow.Current.IsPlaying = false;

                var content = new ContentControl();
                content.Content = "Play";
                return content;
            }

            return PlaybackWindow.Current.playBtn.Content;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
