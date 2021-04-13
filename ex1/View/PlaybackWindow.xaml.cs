using System.Windows;
using System.Windows.Controls;
using ex1.ViewModel;
using System.Windows.Shapes;
using System.Windows.Media;

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

            foreach(var frame in _playBackVM.AnomalyList)
            {
                Rectangle rect = new Rectangle();
                rect.Width = 7;
                rect.Height = 7;
                rect.Fill = Brushes.Red;

                rect.Margin = new Thickness(-450 + ((double)(frame)) / _playBackVM.FramesNumber * 900, 5, 0, 0);

                Button btn = new Button();
                btn.Width = 10;
                btn.Height = 10;
                btn.Background = Brushes.Transparent;
                btn.BorderThickness = new Thickness(0);
                btn.Click += redBtn_Click;

                btn.Margin = new Thickness(-450 + ((double)(frame)) / _playBackVM.FramesNumber * 900, 5, 0, 0);

                sliderGrid.Children.Add(rect);
                sliderGrid.Children.Add(btn);
            }

        }

        private void redBtn_Click(object sender, RoutedEventArgs e)
        {
            _playBackVM.CurrentFramePosition =(int)((((sender as Button).Margin.Left + 450) / 900)*_playBackVM.FramesNumber);
            _playBackVM.PauseRendering();
            playBtn.Content = "Play";
            IsPlaying = false;
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
