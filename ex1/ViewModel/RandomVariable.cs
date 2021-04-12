﻿using System.Collections.Generic;
using System;

namespace ex1.ViewModel
{
    public class RandomVariable
    {
        public RandomVariable(List<double> values)
        {
            double sum = 0, squaresSum = 0;

            for (var i = 0; i < values.Count; ++i)
            {
                var current = values[i];

                sum += current;
                squaresSum += current * current;
            }

            Average = sum / values.Count;
            Varience = squaresSum / values.Count - Average * Average;
        }

        public List<double> Values { get; }
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
            return Covarience(first, second) / (Math.Sqrt(first.Varience) * Math.Sqrt(second.Varience));
        }
    }
}