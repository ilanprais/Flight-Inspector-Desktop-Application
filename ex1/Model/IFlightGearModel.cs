using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ex1.Model
{
    interface IFlightGearModel : INotifyPropertyChanged
    {
        public List<Frame> Frames { get; set; }
        public Frame CurrentFrame { get; set; }

        public bool RenderingStopped { get; set; }
        public int FrameRate { get; set; }

        public void ConnectToFG(string ip, int port);
        public void DisconnectFromFG();
        public void Render();
    }
}
