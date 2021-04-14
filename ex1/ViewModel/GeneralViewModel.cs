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

            _model.CurrentFramePosition = 0;
            _model.RenderingStopped = true;
        }

        public void LoadDLLFile(string filePath)
        {
            File.Copy(filePath, @"..\..\..\View\Resources\anomalyDetector.dll", true);
            _model.DetectAnomaly();
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
