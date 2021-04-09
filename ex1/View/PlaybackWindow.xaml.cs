using System.Windows;
using System.Windows.Controls;
using ex1.ViewModel;

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
            DataContext = (Application.Current as App).PlayBackVM;

            Current = this;
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (!IsPlaying)
            {
                if (!_renderStarted)
                {
                    (DataContext as PlayBackViewModel).Render();
                    _renderStarted = true;
                }
                else
                {
                    if (slider.Value == slider.Maximum)
                    {
                        slider.Value = 0;
                    }

                    (DataContext as PlayBackViewModel).ResumeRendering();
                }

                playBtn.Content = "Pause";
                IsPlaying = true;
            }
            else
            {
                (DataContext as PlayBackViewModel).PauseRendering();
                playBtn.Content = "Play";
                IsPlaying = false;
            }
        }

        private void restart_Click(object sender, RoutedEventArgs e)
        {
            if (slider.Value == slider.Maximum)
            {
                playBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
            else
            {
                (DataContext as PlayBackViewModel).CurrentFramePosition = 0;
            }
        }
    }
}
