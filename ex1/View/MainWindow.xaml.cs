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
            //Member Field
        private GeneralViewModel _generalVM = (Application.Current as App).GeneralVM;

        //Window Initializer
        public MainWindow()
        {
            InitializeComponent();
            if (!Directory.Exists(@"..\..\..\Resources\tmp"))
            {
                Directory.CreateDirectory(@"..\..\..\Resources\tmp");
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            _generalVM.DisconnectFromFG();
            Directory.Delete(@"..\..\..\Resources\tmp", true);

        }
        //On Click method for the file button
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
        
        //On Click method for the dll button
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
