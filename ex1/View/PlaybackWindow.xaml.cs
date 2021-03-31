﻿using System;
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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.start(sender, e);
            startBtn.IsEnabled = false;
        }
    }
}
