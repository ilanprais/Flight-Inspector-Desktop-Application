using System.Windows;
using System.Windows.Controls;

namespace ex1.View
{
    /// <summary>
    /// Interaction logic for PlaybackWindow.xaml
    /// </summary>
    public partial class PlaybackWindow : UserControl
    {
        private bool _renderStarted = false;

        public static PlaybackWindow Current;

        public bool IsPlaying { get; set; }

        public PlaybackWindow()
        {
            InitializeComponent();
            DataContext = MainWindow.vm;

            Current = this;
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (!IsPlaying)
            {
                if (!_renderStarted)
                {
                    MainWindow.vm.Render();
                    _renderStarted = true;
                }
                else
                {
                    if (slider.Value == slider.Maximum)
                    {
                        slider.Value = 0;
                    }

                    MainWindow.vm.ResumeRendering();
                }

                playBtn.Content = "Pause";
                IsPlaying = true;
            }
            else
            {
                MainWindow.vm.PauseRendering();
                playBtn.Content = "Play";
                IsPlaying = false;
            }
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.vm.CurrentFramePosition = 0;
        }
    }
}
