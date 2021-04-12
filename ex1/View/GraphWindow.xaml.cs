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
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            _graphsVM.ChangeField((sender as MenuItem).Header.ToString());
        }
    }
}
