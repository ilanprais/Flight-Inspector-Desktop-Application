using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private bool _isPlaying = false;

        public event PropertyChangedEventHandler PropertyChanged;

        public PlaybackWindow()
        {
            InitializeComponent();
            slider.Value = 0;
            DataContext = MainWindow.vm;
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (!_isPlaying)
            {
                MainWindow.vm.Render();
                playBtn.Content = "Pause";
                _isPlaying = true;
            }
            else
            {
                MainWindow.vm.PauseRendering();
                playBtn.Content = "Play";
                _isPlaying = false;
            }
        }

        private void fast_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.vm.Velocity += 5;
        }

        private void slow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.vm.Velocity -= 5;
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.vm.CurrentFramePosition = 0;
        }
    }
}
