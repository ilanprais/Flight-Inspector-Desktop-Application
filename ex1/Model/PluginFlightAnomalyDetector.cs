using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ex1.Model
{
    public class PluginFlightAnomalyDetector : IFlightAnomalyDetector
    {
            //Member Fields
        private const string AnomalyDetectionPluginPath = @"..\..\..\Resources\anomalyDetector.dll";
        private const string NormalFlightDataFilePath = @"..\..\..\Resources\reg_flight.csv";

        //Plugin
        [DllImport(AnomalyDetectionPluginPath)]
        public static extern void g(StringBuilder str, int len, string normal, string anomaly);

        //Returns a dictionary with the anomaly frame number and the properties in which the anomaly occured in
        public Dictionary<int, List<string>> DetectAnomaly(string flightDataFilePath)
        {
            StringBuilder sb = new StringBuilder(10000000);
            g(sb, sb.Capacity, NormalFlightDataFilePath, flightDataFilePath);
            string anomalyString = sb.ToString();
            
            List<string> rows = new List<string>(anomalyString.Split("\n"));
            rows.Remove("");
            
            Dictionary<int, List<string>> result = new Dictionary<int, List<string>>();
            foreach (string row in rows)
            {
                int frameNum = int.Parse(row.Split(":")[0]);
                string reportsStr = row.Split(":")[1];
                List<string> reportsIndexes = new List<string>(reportsStr.Split(","));
                reportsIndexes.Remove("");
                List<string> reports = new List<string>();
                foreach(string report in reportsIndexes)
                {
                    reports.Add(Frame.Properties[int.Parse(report.Split("-")[0])]+"-"+ Frame.Properties[int.Parse(report.Split("-")[1])]+"\n");
                }
                
                result.Add(frameNum, reports);
            }
            return result;
        }
    }
}
