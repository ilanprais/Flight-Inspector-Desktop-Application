using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ex1.ViewModel
{
    public class AbstractNotifier : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
