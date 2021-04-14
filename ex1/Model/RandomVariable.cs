using System.Collections.Generic;
using System;

namespace ex1.Model
{
    public class RandomVariable
    {
                //Constructor
        public RandomVariable(List<double> values)
        {
            Values = values;

            double sum = 0, squaresSum = 0;
            for (var i = 0; i < Values.Count; ++i)
            {
                var current = Values[i];

                sum += current;
                squaresSum += current * current;
            }

            Average = sum / Values.Count;
            Varience = squaresSum / Values.Count - Average * Average;
        }

                //Properties
        public List<double> Values { get; private set; }
        public double Average { get; private set; }
        public double Varience { get; private set; }

                //Calculates the Covariance
        public double Covarience(RandomVariable other)
        {
            double sum = 0;

            for (var i = 0; i < Values.Count; i++)
            {
                sum += Values[i] * other.Values[i];
            }

            return sum / Values.Count - Average * other.Average;
        }
        
                //Calculates the PCC
        public double PCC(RandomVariable other)
        {
            if (Varience == 0 || other.Varience == 0)
            {
                return 0;
            }

            return Covarience(other) / (Math.Sqrt(Varience) * Math.Sqrt(other.Varience));
        }

                //Finds the most corelative variable
        public RandomVariable FindMostCorelative(List<RandomVariable> variables)
        {
            RandomVariable mostCorelative = null;

            var biggestPCC = -1.0;
            foreach (var variable in variables)
            {
                if (variable == this)
                {
                    continue;
                }

                var pcc = PCC(variable);
                if (pcc > biggestPCC)
                {
                    mostCorelative = variable;
                    biggestPCC = pcc;
                }
            }

            return mostCorelative;
        }

                //Calculates the linear regression
        public (double, double) LinearRegression(RandomVariable other)
        {
            return (Covarience(other) / Varience, other.Average - (Covarience(other) / Varience) * Average);
        }
    }
}
