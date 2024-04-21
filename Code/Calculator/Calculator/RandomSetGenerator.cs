﻿using MathNet.Numerics.LinearAlgebra.Factorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {

    public interface IRandomSetGenerator {
        IRandomSetGenerator SetMean(double mean);
        IRandomSetGenerator SetVariance(double variance);
        IRandomSetGenerator SetStandardDeviation(double std);
        IRandomSetGenerator SetCount(int count);
        IRandomSetGenerator SetLowerQuartile(double lowerQuartile);
        IRandomSetGenerator SetMediumQuartile(double mediumQuartile);
        IRandomSetGenerator SetHighQuartile(double highQuartile);
        IRandomSetGenerator SetMax(double max);
        IRandomSetGenerator SetMin(double min);
        int GetCount();
        double GetMean();
        double GetVariance();
        double GetStandardDeviation();
        double GetLowerQuartile();
        double GetMediumQuartile();
        double GetHigherQuartile();
        double GetMax();
        double GetMin();
        IRandomSetGenerator SetSeed(int seed);
        int? GetSeed();
    }

    abstract class Distrubtion : IRandomSetGenerator {
        double? mean;
        double? variance;
        double? standardDeviation;
        int? count;
        double? lowerQuartile;
        double? mediumQuartile;
        double? highQuartile;
        double? max;
        double? min;
        int? seed;

        public IRandomSetGenerator SetCount(int count) {
            this.count = count; return this;
        }

        public IRandomSetGenerator SetHighQuartile(double highQuartile) {
            this.highQuartile = highQuartile; return this;
        }

        public IRandomSetGenerator SetLowerQuartile(double lowerQuartile) {
            this.lowerQuartile = lowerQuartile; return this;
        }

        public IRandomSetGenerator SetMax(double max) {
            this.max = max; return this;
        }

        public IRandomSetGenerator SetMean(double mean) {
            this.mean = mean; return this;
        }

        public IRandomSetGenerator SetMediumQuartile(double mediumQuartile) {
            this.mediumQuartile = mediumQuartile; return this;
        }

        public IRandomSetGenerator SetMin(double min) {
            this.min = min; return this;
        }

        public IRandomSetGenerator SetStandardDeviation(double std) {
            this.standardDeviation = std; return this;
        }

        public IRandomSetGenerator SetVariance(double variance) {
            this.variance = variance; return this;
        }

        public bool VerifyAttributes() {
            bool nonNull = true;
            if(mean is null) {
                Console.WriteLine("mean is null");
                nonNull = false;
            }
            if(variance is null) {
                Console.WriteLine("variance is null");
                nonNull = false;
            }
            if(standardDeviation is null) {
                Console.WriteLine("StandardDeviation is null");
                nonNull = false;
            }
            if(count is null) {
                Console.WriteLine("Count is null");
                nonNull = false;
            }
            if(lowerQuartile is null) {
                Console.WriteLine("Lower Quartile is null");
                nonNull = false;
            }
            if(mediumQuartile is null) {
                Console.WriteLine("Medium Quartile is null");
                nonNull = false;
            }
            if(highQuartile is null) {
                Console.WriteLine("High Quartile is null");
                nonNull = false;
            }
            if(max is null) {
                Console.WriteLine("Max is null");
                nonNull = false;
            }
            if(nonNull == false) Console.WriteLine("Depending on the distrobution type, it may no function properly due to some attributes missing assignment");
            return nonNull;
        }
        public static void PrintLowerQuartile(double[] dataset) {
            var lowerQuartile = CalculatePercentile(dataset, 0.25);
            Console.WriteLine($"Lower Quartile: {lowerQuartile}");
        }

        public static void PrintHigherQuartile(double[] dataset) {
            var higherQuartile = CalculatePercentile(dataset, 0.75);
            Console.WriteLine($"Higher Quartile: {higherQuartile}");
        }

        public static void PrintStandardDeviation(double[] dataset) {
            var stdDeviation = CalculateStandardDeviation(dataset);
            Console.WriteLine($"Standard Deviation: {stdDeviation}");
        }

        public static void PrintMean(double[] dataset) {
            double mean = dataset.Average();
            Console.WriteLine($"Mean: {mean}");
        }

        private static double CalculatePercentile(double[] dataset, double percentile) {
            var sortedData = dataset.OrderBy(x => x).ToArray();
            double index = (dataset.Length - 1) * percentile;
            if(index % 1 == 0)
                return sortedData[(int)index];

            int lowerIndex = (int)Math.Floor(index);
            int upperIndex = (int)Math.Ceiling(index);

            double lowerValue = sortedData[lowerIndex];
            double upperValue = sortedData[upperIndex];

            return lowerValue + (upperValue - lowerValue) * (index - lowerIndex);
        }

        private static double CalculateStandardDeviation(double[] dataset) {
            double mean = dataset.Average();
            double sumOfSquares = dataset.Sum(value => Math.Pow(value - mean, 2));
            double variance = sumOfSquares / (dataset.Length - 1);
            return Math.Sqrt(variance);
        }

        public int GetCount() {
            return (int)count;
        }

        public double GetMean() {
            return (double)mean;
        }

        public double GetVariance() {
            return (double)variance;
        }

        public double GetStandardDeviation() {
            return (double)standardDeviation;
        }

        public double GetLowerQuartile() {
            return (double)lowerQuartile;
        }

        public double GetMediumQuartile() {
            return (double)mediumQuartile;
        }

        public double GetHigherQuartile() {
            return (double)highQuartile;
        }

        public double GetMax() {
            return (double)max;
        }

        public double GetMin() {
            return (double)min;
        }

        public IRandomSetGenerator SetSeed(int seed) {
            this.seed = seed;
            return this;
        }

        public int? GetSeed() {
            return seed;
        }
    }

    class NormalDistrubtion : Distrubtion, IRandomSetGenerator {
        Random random;
        public NormalDistrubtion() {
            this.random = new Random();
        }

        public NormalDistrubtion(double mean, double std, int count, double min, double max) {
            SetMean(mean)
                .SetStandardDeviation(std)
                .SetCount(count)
                .SetMin(min)
                .SetMax(max);
            this.random = new Random();
        }
        public List<double> GenerateNormalDataset() {
            if(GetSeed() is not null) random = new Random((int)GetSeed());
            VerifyAttributes();
            var normal = new MathNet.Numerics.Distributions.Normal(GetMean(), GetStandardDeviation());
            List<double> dataset = new List<double>();

            double percentageLower = 0.25; // 25%
            double percentageMedium = 0.5; // 50%
            double percentageHigh = 0.25; // 25%

            int lowerCount = (int)(GetCount() * percentageLower);
            int mediumCount = (int)(GetCount() * percentageMedium);
            int highCount = (int)(GetCount() * percentageHigh);

            // Generate values for each quartile
            for(int i = 0; i < lowerCount; i++) {
                double sample;
                int j = 0;
                do {
                    sample = normal.Sample();
                    j++;
                    if(j == 10) break;
                } while(sample < GetMin() || sample > GetLowerQuartile());

                dataset.Add(sample);
            }

            for(int i = 0; i < mediumCount; i++) {
                double sample;
                int j = 0;
                do {
                    sample = normal.Sample();
                    j++;
                    if(j == 10) break;
                } while(sample < GetLowerQuartile() || sample > GetHigherQuartile());

                dataset.Add(sample);
            }

            for(int i = 0; i < highCount; i++) {
                double sample;
                int j = 0;
                do {
                    sample = normal.Sample();
                    j++;
                    if(j == 10) break;//If the loops gets stuck when generating a random value between the quatile and max
                } while(sample < GetHigherQuartile() || sample > GetMax());

                dataset.Add(sample);
            }
            PrintLowerQuartile(dataset.ToArray());
            PrintHigherQuartile(dataset.ToArray());
            PrintMean(dataset.ToArray());
            PrintStandardDeviation(dataset.ToArray());
            //dataset.Sort();
            return dataset;
        }

        public List<double> GenerateNormalDataset(int count, double mean, double std, double lowerQuartile, double mediumQuartile, double highQuartile, double min, double max) {
            var normal = new MathNet.Numerics.Distributions.Normal(mean, std);
            List<double> dataset = new List<double>();

            double percentageLower = 0.25;
            double percentageMedium = 0.5;
            double percentageHigh = 0.25;

            int lowerCount = (int)(count * percentageLower);
            int mediumCount = (int)(count * percentageMedium);
            int highCount = (int)(count * percentageHigh);

            // Generate values for each quartile
            for(int i = 0; i < lowerCount; i++) {
                double sample;
                do {
                    sample = normal.Sample();
                } while(sample < min || sample > lowerQuartile);

                dataset.Add(sample);
            }

            for(int i = 0; i < mediumCount; i++) {
                double sample;
                do {
                    sample = normal.Sample();
                } while(sample < lowerQuartile || sample > highQuartile);

                dataset.Add(sample);
            }

            for(int i = 0; i < highCount; i++) {
                double sample;
                do {
                    sample = normal.Sample();
                } while(sample < highQuartile || sample > max);

                dataset.Add(sample);
            }
            PrintLowerQuartile(dataset.ToArray());
            PrintHigherQuartile(dataset.ToArray());
            PrintMean(dataset.ToArray());
            PrintStandardDeviation(dataset.ToArray());
            return dataset;
        }
    }
}
