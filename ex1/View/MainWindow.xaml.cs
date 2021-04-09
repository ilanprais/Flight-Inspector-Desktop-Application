using Microsoft.Win32;
using System;
using System.Windows;
using ex1.ViewModel;

namespace ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = (Application.Current as App).GeneralVM;
        }

        private void loadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (openFileDialog.ShowDialog() == true)
            {
                (DataContext as GeneralViewModel).LoadFile(openFileDialog.FileName);
                filePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void MainWindow_Closing(object sender, RoutedEventArgs e)
        {
            (DataContext as GeneralViewModel).DisconnectFromFG();
        }
    }
}
