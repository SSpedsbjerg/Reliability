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
    }




    class NormalDistrubtion : IRandomSetGenerator {
        private double mean;
        private double std;
        private int count;
        private double min;
        private double max;
        Random random = new Random();
        public NormalDistrubtion() {
            this.random = new Random();
        }

        public NormalDistrubtion(double mean, double std, int count, double min, double max) {
            this.mean = mean;
            this.std = std;
            this.count = count;
            this.min = min;
            this.max = max;
            this.random = new Random();
        }


        public List<double> GenerateNormalDataset(int count, double mean, double std, double lowerQuartile, double mediumQuartile, double highQuartile, double min, double max) {
            var normal = new MathNet.Numerics.Distributions.Normal(mean, std);
            var dataset = new List<double>();

            // Define the percentage of data within each quartile
            double percentageLower = 0.25; // 25%
            double percentageMedium = 0.5; // 50%
            double percentageHigh = 0.25; // 25%

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

        public IRandomSetGenerator SetCount(int count) {
            this.count = count; 
            return this;
        }

        public IRandomSetGenerator SetHighQuartile(double highQuartile) {
            throw new NotImplementedException();
        }

        public IRandomSetGenerator SetLowerQuartile(double lowerQuartile) {
            throw new NotImplementedException();
        }

        public IRandomSetGenerator SetMax(double max) {
            throw new NotImplementedException();
        }

        public IRandomSetGenerator SetMean(double mean) {
            throw new NotImplementedException();
        }

        public IRandomSetGenerator SetMediumQuartile(double mediumQuartile) {
            throw new NotImplementedException();
        }

        public IRandomSetGenerator SetStandardDeviation(double std) {
            throw new NotImplementedException();
        }

        public IRandomSetGenerator SetVariance(double variance) {
            throw new NotImplementedException();
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
            var mean = dataset.Average();
            Console.WriteLine($"Mean: {mean}");
        }

        private static double CalculatePercentile(double[] dataset, double percentile) {
            // Sort the dataset
            var sortedData = dataset.OrderBy(x => x).ToArray();

            // Calculate the index for the specified percentile
            double index = (dataset.Length - 1) * percentile;

            // If the index is an integer, return the corresponding value
            if(index % 1 == 0)
                return sortedData[(int)index];

            // If the index is not an integer, interpolate between the two surrounding values
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

    }
}
