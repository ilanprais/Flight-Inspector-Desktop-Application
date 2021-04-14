using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ex1.Model;
using System.Threading.Tasks;
using System.Xml;

namespace ex1.ViewModel
{
    public class GeneralViewModel : AbstractNotifier
    {
            //Member Field
        private readonly IFlightGearModel _model;

        //Constructor
        public GeneralViewModel(IFlightGearModel model)
        {
            _model = model;
            _model.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged(e.PropertyName);
            };
        }

        //Method to load the XML file in the provided filepath
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

        //Method to load the CSV file in the provided filepath
        public void LoadCSVFile(string filePath)
        {
            _model.FlightDataFilePath = filePath;

            _model.CurrentFramePosition = 0;
            _model.RenderingStopped = true;
        }

        //Method to load the DLL file in the provided filepath
        public void LoadDLLFile(string filePath)
        {
            if (File.Exists(@"..\..\..\Resources\anomalyDetector.dll"))
            {
                System.IO.File.Move(@"..\..\..\Resources\anomalyDetector.dll", @"..\..\..\Resources\tmp\"+System.Guid.NewGuid().ToString());
            }
            File.Copy(filePath, @"..\..\..\Resources\anomalyDetector.dll", true);
            _model.DetectAnomaly();
        }

        //Method to connect to the flightgear
        public Task ConnectToFG(string ip, int port)
        {
            return _model.ConnectToFG(ip, port);
        }

        //Method to disconnect from the flightgear
        public Task DisconnectFromFG()
        {
            return _model.DisconnectFromFG();
        }
    }
}
