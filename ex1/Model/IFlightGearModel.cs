using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ex1.Model
{
    public interface IFlightGearModel : INotifyPropertyChanged
    {
            //Properties
        public string FlightDataFilePath { get; set; }

        public List<Frame> Frames { get; }
        public Dictionary<int, List<string>> AnomalyDetails { get; }

        public int CurrentFramePosition { get; set; }
        public Frame CurrentFrame { get; }

        public double Velocity { get; set; }
        public bool RenderingStopped { get; set; }
        public int FrameRate { get; }

        //Connects to the flightgear
        public Task ConnectToFG(string ip, int port);
        
                //Disconnects from the flightgear
        public Task DisconnectFromFG();
                //Renders the model
        public Task Render();

        //Detects if there was an anomaly
        public void DetectAnomaly();

        //Finds the most corelative variable
        public RandomVariable FindMostCorelative(RandomVariable variable, List<RandomVariable> variables);
        
                //Calculates the linear regression
        public (double, double) LinearRegression(RandomVariable first, RandomVariable second);
    }
}
