using System;
using System.Windows;
using System.Threading.Tasks;
using Microsoft.Win32;
using ex1.ViewModel;
using System.Windows.Media;


namespace ex1.View
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).GeneralVM;
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            Content = new MainWindow().Content;
        }


        private void DllButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (openFileDialog.ShowDialog() == true)
            {
                (DataContext as GeneralViewModel).LoadCSVFile(openFileDialog.FileName);
                DllPathBox.Text = openFileDialog.FileName;
            }

            step4box.Background = Brushes.Green;
        }

        private void XmlButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (openFileDialog.ShowDialog() == true)
            {
                (DataContext as GeneralViewModel).LoadXMLFile(openFileDialog.FileName);
                XmlPathBox.Text = openFileDialog.FileName;
            }

            FileButton.IsEnabled = true;
            step3box.Background = Brushes.Green;
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (openFileDialog.ShowDialog() == true)
            {
                (DataContext as GeneralViewModel).LoadCSVFile(openFileDialog.FileName);
                PathBox.Text = openFileDialog.FileName;
            }

            step2box.Background = Brushes.Green;
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            StatusBox.Text = "Connecting";
            ConnectButton.IsEnabled = false;

            try
            {
                await (Application.Current as App).GeneralVM.ConnectToFG("127.0.0.1", 8081);
            }
            catch (Exception)
            {
                ConnectButton.IsEnabled = true;
                StatusBox.Text = "Couldn't connect to FlightGear";
                return;
            }

            StatusBox.Text = "Connected! Moving to the application";
            step1box.Background = Brushes.Green;
        }
    }
}
