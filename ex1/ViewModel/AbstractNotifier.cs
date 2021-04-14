using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ex1.ViewModel
{
    public class AbstractNotifier : INotifyPropertyChanged
    {
            //Member Field
        public event PropertyChangedEventHandler PropertyChanged;

        //Notifies if a property is changed
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
