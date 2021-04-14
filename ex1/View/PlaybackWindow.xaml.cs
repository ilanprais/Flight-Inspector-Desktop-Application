using System.Windows;
using System.Windows.Controls;
using ex1.ViewModel;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.ComponentModel;
using System.Collections.Generic;

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

            _playBackVM.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "AnomalyDetails")
                {
                    var remove = new List<UIElement>();
                    foreach (var element in sliderGrid.Children)
                    {
                        if (element is Rectangle || element is Button)
                        {
                            remove.Add(element as UIElement);
                        }
                    }
                    foreach (var element in remove)
                    {
                        sliderGrid.Children.Remove(element);
                    }
                    

                    foreach (var frame in _playBackVM.AnomalyDetails.Keys)
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
                        btn.MouseEnter += redBtn_Hover;
                        btn.MouseLeave += redBtn_Leave;

                        btn.Margin = new Thickness(-450 + ((double)(frame)) / _playBackVM.FramesNumber * 900, 5, 0, 0);

                        sliderGrid.Children.Add(rect);
                        sliderGrid.Children.Add(btn);
                    }
                }
            };
        }

        private void redBtn_Hover(object sender, RoutedEventArgs e)
        {
            Popup info = new Popup();
            int frameNumber = (int)((((sender as Button).Margin.Left + 450) / 900) * _playBackVM.FramesNumber);
            string txt = "";
            foreach (string property in _playBackVM.AnomalyDetails[frameNumber])
            {
                txt += property + "\n";
            }
            TextBlock tb = new TextBlock();
            tb.Background = Brushes.Black;
            tb.Foreground = Brushes.White;
            tb.Text = txt;
            info.Child = tb;
            info.HorizontalAlignment = HorizontalAlignment.Center;

            StackPanel sp = new StackPanel();
            sp.Margin = new Thickness((450 + (sender as Button).Margin.Left)/2, 10, 0, 0);
            sp.Children.Add(info);

            sliderGrid.Children.Add(sp);
            info.IsOpen = true;
            
        }

        private void redBtn_Leave(object sender, RoutedEventArgs e)
        {
            foreach(var elm in sliderGrid.Children)
            {
                if(elm is StackPanel)
                {
                    ((elm as StackPanel).Children[0] as Popup).IsOpen = false;
                }
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
