using System;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using ex1.ViewModel;


namespace ex1.View
{
    /// <summary>
    /// Interaction logic for WelcomeWindow.xaml
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        private GeneralViewModel _generalVM = (Application.Current as App).GeneralVM;

        private bool _fgConnected = false;
        private bool _xmlUploded = false;
        private bool _csvUploaded = false;

        public WelcomeWindow()
        {
            InitializeComponent();
        }

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            Content = new MainWindow().Content;
        }

        private void DllButton_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog
            //{
            //    Multiselect = false,
            //    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            //};
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    (DataContext as GeneralViewModel).LoadCSVFile(openFileDialog.FileName);
            //    DllPathBox.Text = openFileDialog.FileName;
            //}

            step4box.Background = Brushes.Green;
        }

        private void XmlButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _generalVM.LoadXMLFile(openFileDialog.FileName);
                }
                catch (Exception)
                {
                    XmlPathBox.Text = "Invalid XML file";
                    return;
                }

                XmlPathBox.Text = openFileDialog.FileName;

                _xmlUploded = true;
                step2box.Background = Brushes.Green;
                enableContinueButton();
            }
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_xmlUploded)
            {
                PathBox.Text = "Please uplode the XML file first and then upload the CSV file";
                return;
            }

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _generalVM.LoadCSVFile(openFileDialog.FileName);
                }
                catch (Exception)
                {
                    PathBox.Text = "Invalid CSV file";
                    return;
                }

                PathBox.Text = openFileDialog.FileName;

                _csvUploaded = true;
                step3box.Background = Brushes.Green;
                enableContinueButton();
            }
        }

        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            StatusBox.Text = "Connecting";
            ConnectButton.IsEnabled = false;

            try
            {
                //await _generalVM.ConnectToFG("127.0.0.1", 8081);
            }
            catch
            {
                ConnectButton.IsEnabled = true;
                StatusBox.Text = "Couldn't connect to FlightGear";
                return;
            }

            StatusBox.Text = "Connected to FlightGear";

            _fgConnected = true;
            step1box.Background = Brushes.Green;
            enableContinueButton();
        }

        private void enableContinueButton()
        {
            if (_fgConnected && _xmlUploded && _csvUploaded)
            {
                ContinueButton.IsEnabled = true;
            }
        }
    }
}
