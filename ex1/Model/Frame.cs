using System;
using System.Collections.Generic;

namespace ex1.Model
{
    public class Frame
    {
                //Member Fields
        private readonly string _stringRepresentation;

        public static List<string> Properties;

                //Constructor
        public Frame(string frameString)
        {
            ValuesMap = new Dictionary<string, double>();

            _stringRepresentation = frameString;

            string[] words = frameString.Split(',');
            for (var i = 0; i < Properties.Count; ++i)
            {
                ValuesMap[Properties[i]] = Double.Parse(words[i]);
            }
        }
        //Property
        public Dictionary<string, double> ValuesMap { get; set; }

                //ToString method
        public override string ToString()
        {
            return _stringRepresentation;
        }
    }
}
