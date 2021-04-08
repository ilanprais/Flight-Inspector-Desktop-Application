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

        private void loadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                vm.LoadFile(openFileDialog.FileName);
                filePathTextBox.Text = openFileDialog.FileName;
            }
            //String path = "C:\\Users\\ilandovprais\\Documents\\reg_flight.csv";
            //vm.LoadFile(path);
            //filePathTextBox.Text = path;

        }
    }
}
