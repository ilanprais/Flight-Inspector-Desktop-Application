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
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as GraphsViewModel).ChangeField((sender as MenuItem).Name);
        }
    }
}
