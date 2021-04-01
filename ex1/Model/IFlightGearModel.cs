using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;

namespace ex1.Model
{
    public interface IFlightGearModel : INotifyPropertyChanged
    {
        public List<Frame> Frames { get; set; }
        public int CurrentFramePosition { get; set; }
        public Frame CurrentFrame { get; }

        public double Velocity { get; set; }
        public bool RenderingStopped { get; set; }
        public int FrameRate { get; }

        public Task ConnectToFG(string ip, int port);
        public Task DisconnectFromFG();
        public Task Render();
    }
}
