using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ex1.Model;
using System.Threading.Tasks;
using System.Xml;
using System.Runtime.InteropServices;
using System.Text;

namespace ex1.ViewModel
{
    public class GeneralViewModel : AbstractNotifier
    {
        private readonly IFlightGearModel _model;

        public GeneralViewModel(IFlightGearModel model)
        {
            _model = model;
            _model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        public void LoadXMLFile(string filePath)
        {
            Frame.Properties = new List<string>();

            var xml = new XmlDocument();
            xml.Load(filePath);

            foreach (XmlNode elem in xml.SelectNodes("//input/chunk"))
            {
                Frame.Properties.Add(elem["name"].InnerText.Replace('_', '-'));
            }
        }

        public void LoadCSVFile(string filePath)
        {
            var frames = new List<Frame>();

            using (StreamReader file = new StreamReader(filePath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    frames.Add(new Frame(line));
                }
            }

            _model.Frames = frames;

            _model.CurrentFramePosition = 0;
            _model.RenderingStopped = true;
        }
        public static void Copy(string inputFilePath, string outputFilePath)
        {
            int bufferSize = 1024 * 1024;

            using (FileStream fileStream = new FileStream(outputFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            //using (FileStream fs = File.Open(<file-path>, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                FileStream fs = new FileStream(inputFilePath, FileMode.Open, FileAccess.ReadWrite);
                fileStream.SetLength(fs.Length);
                int bytesRead = -1;
                byte[] bytes = new byte[bufferSize];

                while ((bytesRead = fs.Read(bytes, 0, bufferSize)) > 0)
                {
                    fileStream.Write(bytes, 0, bytesRead);
                }
            }
        }
        [DllImport(@"..\..\..\View\Resources\anomalyDetector.dll")]
        public static extern void g(StringBuilder str, int len, string normal, string anomaly);
        public void LoadDLLFile(string filePath)
        {
            //File.Copy(Path.Combine(@"C: \Users\danbi\Downloads","meshek_hamaim_israek.pptx"), Path.Combine(@"C: \Users\danbi\Downloads","185383.pptx"));
            //if (File.Exists(@"..\..\..\View\Resources\anomalyDetector.dll"))
            //{
            //    File.Delete(@"..\..\..\View\Resources\anomalyDetector.dll");
            //}
            //var directory = new DirectoryInfo(@"..\..\..\View\Resources\anomalyDetector.dll");
            //if (directory.Exists)
            //{
            //    Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(@"..\..\..\View\Resources\anomalyDetector.dll", Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents);
            //}
            string path = filePath;
            
            string path2 = @"..\..\..\View\Resources\anomalyDetector.dll";
            
            //Copy(path, path2);
            File.Copy(path, path2, true);
            StringBuilder sb = new StringBuilder(10000000);
            //Console.WriteLine(AttemptAdd(a, 10));
            g(sb, sb.Capacity, "C:\\Users\\danbi\\Downloads\\reg_flight.csv", "C:\\Users\\danbi\\Downloads\\anomaly_flight.csv");
            string str = sb.ToString();
            //using (StreamReader file = new StreamReader(filePath))
            //{
            //    string line;
            //    while ((line = file.ReadLine()) != null)
            //    {
            //        frames.Add(new Frame(line));
            //    }
            //}

            //_model.Frames = frames;

            //_model.CurrentFramePosition = 0;
            //_model.RenderingStopped = true;
        }

        public Task ConnectToFG(string ip, int port)
        {
            return _model.ConnectToFG(ip, port);
        }

        public Task DisconnectFromFG()
        {
            return _model.DisconnectFromFG();
        }
    }
}
