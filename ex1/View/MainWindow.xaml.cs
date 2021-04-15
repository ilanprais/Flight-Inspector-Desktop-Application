using Microsoft.Win32;
using System;
using System.Windows;
using ex1.ViewModel;
using System.ComponentModel;
using System.IO;

namespace ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private GeneralViewModel _generalVM = (Application.Current as App).GeneralVM;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _generalVM.LoadCSVFile(openFileDialog.FileName);
                    graphs._graphsVM.ChangeField(GraphsViewModel.Properties[0]);
                }
                catch (Exception)
                {
                    filePathTextBox.Text = "Invalid CSV file";
                    return;
                }

                filePathTextBox.Text = openFileDialog.FileName;

                graphs.cmbProperties.SelectedIndex = 0;
                playback.playBtn.Content = "Start";
                playback.IsPlaying = false;
            }
        }

        private void DllButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    _generalVM.LoadDLLFile(openFileDialog.FileName);
                }
                catch (Exception)
                {
                    filePathTextBox.Text = "Invalid dll file";
                    return;
                }

                filePathTextBox.Text = openFileDialog.FileName;

                graphs.cmbProperties.SelectedIndex = 0;
                playback.playBtn.Content = "Start";
                playback.IsPlaying = false;
            }
        }
    }
}
