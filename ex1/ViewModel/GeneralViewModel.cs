﻿using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using ex1.Model;
using System.Threading.Tasks;
using System.Xml;

namespace ex1.ViewModel
{
    public class GeneralViewModel : AbstractNotifier
    {
        private readonly IFlightGearModel _model;

        private const string AnomalyDetectionPluginPath = @"..\..\..\Resources\anomalyDetector.dll";

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
            if (File.Exists(AnomalyDetectionPluginPath))
            {
                File.Move(AnomalyDetectionPluginPath, @"..\..\..\Resources\tmp\" + System.Guid.NewGuid().ToString());
            }

            File.Copy(filePath, AnomalyDetectionPluginPath, true);
            _model.DetectAnomaly();

            _model.CurrentFramePosition = 0;
            _model.RenderingStopped = true;
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
