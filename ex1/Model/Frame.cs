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
            ValuesMap = new Dictionary<string, double>();

            _stringRepresentation = frameString;

            string[] words = frameString.Split(',');

            ValuesMap["aileron"] = Double.Parse(words[0]);
            ValuesMap["elevator"] = Double.Parse(words[1]);
            ValuesMap["rudder"] = Double.Parse(words[3]);
            ValuesMap["throttle"] = Double.Parse(words[6]) + Double.Parse(words[7]);

            ValuesMap["altimeter"] = Double.Parse(words[25]);
            ValuesMap["airspeed"] = Double.Parse(words[21]);
            ValuesMap["direction"] = Double.Parse(words[19]);
            ValuesMap["pitch"] = Double.Parse(words[18]);
            ValuesMap["roll"] = Double.Parse(words[17]);
            ValuesMap["yaw"] = Double.Parse(words[19]);
        }
        public Frame()
        {
            ValuesMap = new Dictionary<string, double>();

            _stringRepresentation = "0";
            for (var i = 0; i < 40; ++i)
            {
                _stringRepresentation += ",0";
            }

            ValuesMap["aileron"] = 0;
            ValuesMap["elevator"] = 0;
            ValuesMap["rudder"] = 0;
            ValuesMap["throttle"] = 0;

            ValuesMap["altimeter"] = 0;
            ValuesMap["airspeed"] = 0;
            ValuesMap["direction"] = 0;
            ValuesMap["pitch"] = 0;
            ValuesMap["roll"] = 0;
            ValuesMap["yaw"] = 0;
        }

        public Dictionary<string, double> ValuesMap { get; set; }

        public override string ToString()
        {
            return _stringRepresentation;
        }
    }
}
