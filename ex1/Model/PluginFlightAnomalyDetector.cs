using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ex1.Model
{
    public class PluginFlightAnomalyDetector : IFlightAnomalyDetector
    {
        private const string AnomalyDetectionPluginPath = @"..\..\..\View\Resources\anomalyDetector.dll";
        private const string NormalFlightDataFilePath = @"..\..\..\View\Resources\reg_flight.csv";

        [DllImport(AnomalyDetectionPluginPath)]
        public static extern void g(StringBuilder str, int len, string normal, string anomaly);

        public Dictionary<int, List<string>> DetectAnomaly(string flightDataFilePath)
        {
            StringBuilder sb = new StringBuilder(10000000);
            g(sb, sb.Capacity, NormalFlightDataFilePath, flightDataFilePath);
            string anomalyString = sb.ToString();

            return new Dictionary<int, List<string>>();
        }
    }
}
