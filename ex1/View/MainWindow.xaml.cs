using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ex1.ViewModel;
using ex1.Model;

namespace ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static FlightGearViewModel vm = new FlightGearViewModel(new FlightGearModel(new AsyncTcpFGClient()));
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void GraphWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void loadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                vm.FilePath = openFileDialog.FileName;
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            vm.Render();
            start.Visibility = Visibility.Hidden;
        }

        private void Statistics_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void GraphWindow_Loaded_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
