using System.Linq;
using System.Reflection;

namespace Calculator {

    enum Cals {
        Probability,
        Survivor,
        FailureRate
    }

    public static class Program {
        public static int Main(string[] args) {
            double function = 4.28 * Math.Pow(10, -4);
            double time = 720;
            double[] values = { function, time };
            Exponential.print(Cals.Survivor, values);
            return 0;
        }
    }

    static class Exponential {
        const double e = Math.E;

        public static double Probability(double lambda, double time) {
            return lambda * Math.Pow(e, -1 * lambda * time);
        }

        public static double Survivor(double lambda, double time) {
            return Math.Pow(e, -lambda * time);
        }

        public static double FailureRate(double lambda) {
            return lambda;
        }

        public static void print(Cals cal, double[] args) {
            double value = -1;
            string calString = "";
            switch(cal) {
                case Cals.Probability:
                value = Probability(args[0], args[1]);
                calString = "Probability";
                break;
                case Cals.Survivor:
                value = Survivor(args[0], args[1]);
                calString = "Survivor";
                break;
                case Cals.FailureRate:
                value = FailureRate(args[0]);
                calString = "FailureRate";
                break;
                default:
                    throw new Exception();
            }
            string returnString = $"Function: {calString} returns {value} with the given values: (Lambda, Time) ";
            string stringValue = String.Join(", ", args.Select(x => x.ToString()));
            returnString += stringValue;
            Console.WriteLine(returnString);
        }
    }

    public static class Gamma {
        public static double Probability() {
            throw new NotImplementedException();
        }

        public static double Survivor() {
            throw new NotImplementedException();
        }

        public static double FailureRate() {
            throw new NotImplementedException();
        }
    }

    public static class Weibull {
        public static double Probability(double alpha, double lambda, double time) {
            throw new NotImplementedException();
        }

        public static double Survivor() {
            throw new NotImplementedException();
        }

        public static double FailureRate() {
            throw new NotImplementedException();
        }
    }

    public static class LogNormal {
        public static double Probability() {
            throw new NotImplementedException();
        }

        public static double Survivor() {
            throw new NotImplementedException();
        }

        public static double FailureRate() {
            throw new NotImplementedException();
        }
    }

    public static class BirnbaumSaunders {
        public static double Probability() {
            throw new NotImplementedException();
        }

        public static double Survivor() {
            throw new NotImplementedException();
        }

        public static double FailureRate() {
            throw new NotImplementedException();
        }
    }

    public static class GumbellSmallestExtreme {
        public static double Probability() {
            throw new NotImplementedException();
        }

        public static double Survivor() {
            throw new NotImplementedException();
        }

        public static double FailureRate() {
            throw new NotImplementedException();
        }
    }

    public static class InverseGaussian {
        public static double Probability() {
            throw new NotImplementedException();
        }

        public static double Survivor() {
            throw new NotImplementedException();
        }

        public static double FailureRate() {
            throw new NotImplementedException();
        }
    }
}

