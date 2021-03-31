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
            Altimeter = Double.Parse(words[16]);
            AirSpeed = Double.Parse(words[20]);

        }
        public Frame()
        {
            _stringRepresentation = "0";
            for (var i = 0; i < 40; ++i)
            {
                _stringRepresentation += ",0";
            }

            Altimeter = 0;
            AirSpeed = 0;
            Direction = 0;
            Pitch = 0;
            Row = 0;
            Yaw = 0;
        }

        public double Altimeter { get; set; }
        public double AirSpeed { get; set; }
        public double Direction { get; set; }
        public double Pitch { get; set; }
        public double Row { get; set; }
        public double Yaw { get; set; }

        public override string ToString()
        {
            return _stringRepresentation;
        }
    }
}
