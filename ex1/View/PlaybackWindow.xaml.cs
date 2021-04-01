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
    /// Interaction logic for PlaybackWindow.xaml
    /// </summary>
    public partial class PlaybackWindow : UserControl
    {
        public PlaybackWindow()
        {
            InitializeComponent();
            slider.Value = 0;
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.vm.Render();
            startBtn.IsEnabled = false;
            pauseBtn.IsEnabled = true;
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainWindow.vm.CurrentFramePosition = (int) e.NewValue;
        }

        private void pause_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.vm.PauseRendering();
            pauseBtn.IsEnabled = false;
            startBtn.IsEnabled = true;
        }

        private void fast_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.vm.FrameRate += 5;
        }

        private void slow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.vm.FrameRate -= 5;
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.vm.CurrentFramePosition = 0;
        }
    }
}
