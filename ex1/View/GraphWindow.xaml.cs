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
        private GraphsViewModel _graphsVM = (Application.Current as App).GraphsVM;

        public GraphWindow()
        {
            InitializeComponent();
            DataContext = _graphsVM;

            foreach (var property in GraphsViewModel.Properties)
            {
                var item = new MenuItem();
                item.Header = property;
                item.FontSize = 8;
                item.HorizontalAlignment = HorizontalAlignment.Center;
                item.Width = 100;
                item.Click += MenuItem_Click;
                propertyMenu.Items.Add(item);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            _graphsVM.ChangeField((sender as MenuItem).Header.ToString());

            foreach (MenuItem item in propertyMenu.Items)
            {
                item.Background = Brushes.SlateGray;
            }
            (sender as MenuItem).Background = Brushes.Black;

            graphName.Text = (sender as MenuItem).Header.ToString();
            //corName.Text = currentCoralitiveProperty;
        }
    }
}
