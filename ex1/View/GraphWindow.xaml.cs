using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using ex1.ViewModel;
using System.Collections.Generic;

namespace ex1.View
{
    /// <summary>
    /// Interaction logic for GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : UserControl
    {
        public GraphsViewModel _graphsVM = (Application.Current as App).GraphsVM;

        public GraphWindow()
        {
            InitializeComponent();
            DataContext = _graphsVM;

            List<string> ls = new List<string>();
            foreach (var property in GraphsViewModel.Properties)
            {
                ls.Add(property);
            }
            cmbProperties.ItemsSource = ls;
            cmbProperties.SelectedIndex = 0;
        }

        private void cmbProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _graphsVM.ChangeField(cmbProperties.SelectedItem.ToString());
        }
    }
}
