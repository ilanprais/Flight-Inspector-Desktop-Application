using System.Collections.Generic;

namespace ex1.Model
{
    public interface IFlightAnomalyDetector
    {
                //Detects if there was an anomaly
        Dictionary<int, List<string>> DetectAnomaly(string flightDataFilePath);
    }
}
