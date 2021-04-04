using System;
using System.Collections.Generic;
using System.Text;

namespace ex1.Model
{
    public class Frame
    {
        private string _stringRepresentation;

        public Frame(string frameString)
        {
            _stringRepresentation = frameString;

            string[] words = frameString.Split(',');

            Aileron = Double.Parse(words[0]);
            Elevator = Double.Parse(words[1]);
            Rudder = Double.Parse(words[3]);
            Throttle = Double.Parse(words[6]) + Double.Parse(words[7]);

            Altimeter = Double.Parse(words[25]);
            AirSpeed = Double.Parse(words[21]);
            //Direction = Double.Parse(words[19]);
            Pitch = Double.Parse(words[18]);
            Roll = Double.Parse(words[17]);
            Yaw = Double.Parse(words[19]);
        }
        public Frame()
        {
            _stringRepresentation = "0";
            for (var i = 0; i < 40; ++i)
            {
                _stringRepresentation += ",0";
            }

            Aileron = 0;
            Elevator = 0;
            Rudder = 0;
            Throttle = 0;
            Altimeter = 0;
            AirSpeed = 0;
            Direction = 0;
            Pitch = 0;
            Roll = 0;
            Yaw = 0;
        }

        public double Aileron { get; set; }
        public double Elevator { get; set; }
        public double Rudder { get; set; }
        public double Throttle { get; set; }
        public double Altimeter { get; set; }
        public double AirSpeed { get; set; }
        public double Direction { get; set; }
        public double Pitch { get; set; }
        public double Roll { get; set; }
        public double Yaw { get; set; }

        public override string ToString()
        {
            return _stringRepresentation;
        }
    }
}
