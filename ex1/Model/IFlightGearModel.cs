using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ex1.Model
{
    public interface IFlightGearModel : INotifyPropertyChanged
    {
        public string FlightDataFilePath { get; set; }

        public List<Frame> Frames { get; }
        public Dictionary<int, List<string>> AnomalyDetails { get; }

        public int CurrentFramePosition { get; set; }
        public Frame CurrentFrame { get; }

        public double Velocity { get; set; }
        public bool RenderingStopped { get; set; }
        public int FrameRate { get; }

        public Task ConnectToFG(string ip, int port);
        public Task DisconnectFromFG();
        public Task Render();

        public void DetectAnomaly();

        public RandomVariable FindMostCorelative(RandomVariable variable, List<RandomVariable> variables);
        public (double, double) LinearRegression(RandomVariable first, RandomVariable second);
    }
}
