using System;
using System.Windows;
using System.Threading.Tasks;

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
            await Task.Delay(2000);

            new MainWindow().Show();
            Close();
        }
    }
}
