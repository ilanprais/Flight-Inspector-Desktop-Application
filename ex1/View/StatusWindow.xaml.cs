using ex1.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ex1.View
{
    /// <summary>
    /// Interaction logic for StatusWindow.xaml
    /// </summary>
    public partial class StatusWindow : UserControl
    {
        private PlayBackViewModel _playBackVM = (Application.Current as App).PlayBackVM;
        public StatusWindow()
        {
            InitializeComponent();

            DataContext = _playBackVM;
        }
    }
}
