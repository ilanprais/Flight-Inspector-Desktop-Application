using System.Collections.Generic;
using System;

namespace ex1.ViewModel
{
    public class RandomVariable
    {
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

        public List<double> Values { get; private set; }
        public double Average { get; private set; }
        public double Varience { get; private set; }

        public static double Covarience(RandomVariable first, RandomVariable second)
        {
            double sum = 0;

            for (var i = 0; i < first.Values.Count; i++)
            {
                sum += first.Values[i] * second.Values[i];
            }

            return sum / first.Values.Count - first.Average * second.Average;
        }
        public static double PCC(RandomVariable first, RandomVariable second)
        {
            if (first.Varience == 0 || second.Varience == 0)
            {
                return 0;
            }

            return Covarience(first, second) / (Math.Sqrt(first.Varience) * Math.Sqrt(second.Varience));
        }
    }
}