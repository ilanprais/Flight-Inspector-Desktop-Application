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
            _model.FlightDataFilePath = filePath;

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

        [DllImport(@"..\..\..\View\Resources\anomalyDetector.dll")]
        public static extern void g(StringBuilder str, int len, string normal, string anomaly);
        public void LoadDLLFile(string filePath)
        {
            string path = filePath;
            
            string path2 = @"..\..\..\View\Resources\anomalyDetector.dll";
            
            File.Copy(path, path2, true);
            StringBuilder sb = new StringBuilder(10000000);
            g(sb, sb.Capacity, @"..\..\..\View\Resources\reg_flight.csv", _model.FlightDataFilePath);
            string str = sb.ToString();
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
