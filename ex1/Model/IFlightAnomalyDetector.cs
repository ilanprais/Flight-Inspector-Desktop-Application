using System.Collections.Generic;

namespace ex1.Model
{
    public interface IFlightAnomalyDetector
    {
        Dictionary<int, List<string>> DetectAnomaly(string flightDataFilePath);
    }
}
