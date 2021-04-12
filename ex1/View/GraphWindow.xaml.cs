using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using ex1.ViewModel;

namespace ex1.View
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : UserControl
    {
        public GraphWindow()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).GraphsVM;

            foreach(string p in GeneralViewModel.getPropertyNames())
            {
                MenuItem item = new MenuItem();
                item.Header = p;
                item.FontSize = 8;
                item.HorizontalAlignment = HorizontalAlignment.Center;
                item.Width = 100;
                item.Click += MenuItem_Click;
                propertyMenu.Items.Add(item);
            }
            

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //(DataContext as GraphsViewModel).ChangeField((sender as MenuItem).Name);
            foreach(MenuItem item in propertyMenu.Items)
            {
                item.Background = Brushes.SlateGray;
            }
            (sender as MenuItem).Background = Brushes.Black;
        }


    }
}
