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
        //Member Field
        public GraphsViewModel _graphsVM = (Application.Current as App).GraphsVM;

        //Window Initializer
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
        //Method for when the selection was changed in the cmbProperties drop down list (Combobox control)
        private void cmbProperties_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _graphsVM.ChangeField(cmbProperties.SelectedItem.ToString());
        }
    }
}
