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
        private PlayBackViewModel _playBackVM = (Application.Current as App).PlayBackVM;

        private bool _renderStarted = false;

        public static PlaybackWindow Current;

        public bool IsPlaying { get; set; }

        public PlaybackWindow()
        {
            InitializeComponent();
            DataContext = _playBackVM;

            Current = this;
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (!IsPlaying)
            {
                if (!_renderStarted)
                {
                    _playBackVM.Render();
                    _renderStarted = true;
                }
                else
                {
                    if (slider.Value == slider.Maximum)
                    {
                        slider.Value = 0;
                    }

                    _playBackVM.ResumeRendering();
                }

                playBtn.Content = "Pause";
                IsPlaying = true;
            }
            else
            {
                _playBackVM.PauseRendering();
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
                _playBackVM.CurrentFramePosition = 0;
            }
        }
    }
}
