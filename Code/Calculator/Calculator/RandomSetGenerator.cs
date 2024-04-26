using System.Collections.Generic;

namespace Calculator {

    public interface IRandomSetGenerator {
        IRandomSetGenerator SetMean(double mean);
        IRandomSetGenerator SetVariance(double variance);
        IRandomSetGenerator SetStandardDeviation(double std);
        IRandomSetGenerator SetCount(int count);
        IRandomSetGenerator SetLowerQuartile(double lowerQuartile);
        IRandomSetGenerator SetHighQuartile(double highQuartile);
        IRandomSetGenerator SetMax(double max);
        IRandomSetGenerator SetMin(double min);
        int GetCount();
        double GetMean();
        double GetVariance();
        double GetStandardDeviation();
        double GetLowerQuartile();
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

    class ContinousData : Distrubtion {

        double? range0 = null;
        double? range1 = null;
        double requiredDataPercentage = 0;

        public ContinousData() {

        }

        public void SetTargetRange(double value1, double value2) {
            range0 = value1; range1 = value2;
        }

        public void voidTargetRange() {
            range0 = null; range1 = null;
        }

        public void setRequiredDataPercentage(double value) {
            requiredDataPercentage = value;
        }

        private List<double> GenerateTargetPoints() {
            if(range0 == null || range1 == null) { return null; }
            List<double> points = new List<double>();
            Random random = new Random();
            int totalPoints = (int)((this.GetCount() / 10) + 1);
            for (int i = 0; i < ((requiredDataPercentage / 100) * totalPoints) + 1; i++) {
                points.Add((double)(range0 + (random.NextDouble() * (range1 - range0))));
            }
            for (int i = 0; i < totalPoints - ((requiredDataPercentage / 100) * totalPoints) + 1; i++) {
                points.Add((double)(this.GetMin() + (random.NextDouble() * (this.GetMax() - this.GetMin()))));
            }
            Shuffel(points);
            return points;
        }

        private void Shuffel(List<double> values) {
            Random random = new Random();
            int n = values.Count;
            while(n > 1) {
                n--;
                int k = random.Next(n + 1);
                double value = values[k];
                values[k] = values[n];
                values[n] = value;
            }
        }

        public List<double> GenerateData() {
            List<double> points = GenerateTargetPoints();
            List<double> values = new List<double>();
            int i = 0;
            Random random = new Random();
            values.Add(points[i]);
            int j = 0;
            while (this.GetCount() > values.Count()) {
                double maxAddition = ((double)range1 / ((double)range0 + (double)range1));
                try {
                    values.Add(values.Last() + (random.NextDouble() * ((points[i + 1] - values.Last()))));
                    j++;
                    if (j >= this.GetCount() / (int)((this.GetCount() / 10) + 1)) {
                        i++;
                        j = 0;
                        values.Add(points[i]);
                    }
                }
                catch(IndexOutOfRangeException) {
                    return values;
                }
            }
            return values;
        }

    }

    class RandomData : Distrubtion {
        public RandomData() {
            
        }

        public List<double> GenerateData() {
            Random random = new Random();
            List<double> dataset = new List<double>();

            // Calculate standard deviation from quartiles
            double stdDev = (GetHigherQuartile() - GetLowerQuartile()) / 1.349; // 1.349 is the approximate value of the interquartile range for a normal distribution

            // Generate dataset by sampling from a normal distribution
            for(int i = 0; i < GetCount(); i++) {
                double value;
                do {
                    // Sample from a normal distribution within the given range
                    value = random.NextDouble() * stdDev + GetMean() - (stdDev / 2.0);
                } while(value < GetMin() || value > GetMax()); // Ensure value is within min and max
                dataset.Add(value);
            }

            return dataset;
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
                    if(j == 100) break;
                } while(sample < GetMin() || sample > GetLowerQuartile());

                dataset.Add(sample);
            }

            for(int i = 0; i < mediumCount; i++) {
                double sample;
                int j = 0;
                do {
                    sample = normal.Sample();
                    j++;
                    if(j == 100) break;
                } while(sample < GetLowerQuartile() || sample > GetHigherQuartile());

                dataset.Add(sample);
            }

            for(int i = 0; i < highCount; i++) {
                double sample;
                int j = 0;
                do {
                    sample = normal.Sample();
                    j++;
                    if(j == 100) break;//If the loops gets stuck when generating a random value between the quatile and max
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

        public List<double> GenerateNormalDataset(int count, double mean, double std, double lowerQuartile, double highQuartile, double min, double max) {
            var normal = new MathNet.Numerics.Distributions.Normal(mean, std);
            List<double> dataset = new List<double>();
            int limit = 999999;
            double percentageLower = 0.25;
            double percentageMedium = 0.5;
            double percentageHigh = 0.25;

            int lowerCount = (int)(count * percentageLower);
            int mediumCount = (int)(count * percentageMedium);
            int highCount = (int)(count * percentageHigh);
            // Generate values for each quartile
            for(int i = 0; i < lowerCount; i++) {
                double sample;
                int j = 0;

                do {
                    sample = normal.Sample();
                    if(dataset.Count == 0)
                        dataset.Add(sample);
                    j++;
                    if(j == limit)
                        break;//If the loops gets stuck when generating a random value between the quatile and max
                } while(sample < min || sample > lowerQuartile);

                dataset.Add(sample);
            }

            for(int i = 0; i < mediumCount; i++) {
                double sample;
                int j = 0;
                do {
                    sample = normal.Sample();
                    j++;
                    if(j == limit)
                        break;//If the loops gets stuck when generating a random value between the quatile and max
                } while(sample < lowerQuartile || sample > highQuartile);

                dataset.Add(sample);
            }

            for(int i = 0; i < highCount; i++) {
                double sample;
                int j = 0;
                do {
                    sample = normal.Sample();
                    j++;
                    if(j == limit)
                        break;//If the loops gets stuck when generating a random value between the quatile and max
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
