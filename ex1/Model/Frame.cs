using System;
using System.Collections.Generic;
using System.Text;

namespace ex1.Model
{
    public class Frame
    {
        private string _stringRepresentation;

        Frame(string frameString)
        {
            _stringRepresentation = frameString;
        }

        public override string ToString()
        {
            return _stringRepresentation;
        }

        public double Altimeter { get; set; }
        public double AirSpeed { get; set; }
        public double Direction { get; set; }
        public double Pitch { get; set; }
        public double Row { get; set; }
        public double Yaw { get; set; }
    }
}
