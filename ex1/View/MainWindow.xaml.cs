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

namespace ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ViewModel.FlightGearViewModel VM= new ViewModel.FlightGearViewModel(new Model.FlightGearModel());
        public MainWindow()
        {
            InitializeComponent();
            DataContext = VM;
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
            //openFileDialog.Filter = "*.csv";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                VM.FilePath = openFileDialog.FileName;
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            VM.Render();
            start.Visibility = Visibility.Hidden;
        }
    }
}
