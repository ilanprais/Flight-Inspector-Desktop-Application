using Microsoft.Win32;
using System;
using System.Windows;
using ex1.ViewModel;
using System.ComponentModel;

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

        public void loadFile_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog
            //{
            //    Multiselect = false,
            //    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            //};
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    (DataContext as GeneralViewModel).LoadFile(openFileDialog.FileName);
            //    filePathTextBox.Text = openFileDialog.FileName;
            //}
            string path = "C:\\Users\\ilandovprais\\Documents\\reg_flight.csv";
            (DataContext as GeneralViewModel).LoadFile(path);
            filePathTextBox.Text = path;
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            (DataContext as GeneralViewModel).DisconnectFromFG();
        }
    }
}
