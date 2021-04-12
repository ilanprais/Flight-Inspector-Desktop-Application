using Microsoft.Win32;
using System;
using System.Windows;
using ex1.ViewModel;
using System.ComponentModel;

namespace ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).GeneralVM;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            (DataContext as GeneralViewModel).DisconnectFromFG();
        }
    }
}
