using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;

namespace Calculator {

    enum Cals {
        Probability,
        Survivor,
        FailureRate,
        MTTF
    }

    public static class Program {
        public static int Main(string[] args) {
            double function = 4.28 * Math.Pow(10, -4);
            double time = 720;
            double[] values = { function, time };
            Exponential exponential = new Exponential();
            exponential.print(Cals.Survivor, values);
            return 0;
        }
    }

    abstract class AbstractCalculator {
        public const double e = Math.E;
        public int factorial(int x) {
            int value = 0;
            for(int i = x; i > 0; i--) {
                value *= x;
                x--;
            }
            return value;
        }

        public double Probability(double lambda, double time) {
            throw new NotImplementedException();
        }

        public double Survivor(double lambda, double time) {
            throw new NotImplementedException();
        }

        public double FailureRate(double lambda) {
            throw new NotImplementedException();
        }

        public double MTTF(double lambda) {
            throw new NotImplementedException();
        }

        public void print(Cals cal, double[] args) {
            throw new NotImplementedException();
        }

    }

    class Exponential : AbstractCalculator {
        public new double Probability(double lambda, double time) {
            return lambda * Math.Pow(e, -1 * lambda * time);
        }

        public new double Survivor(double lambda, double time) {
            return Math.Pow(e, -lambda * time);
        }

        public new double FailureRate(double lambda) {
            return lambda;
        }

        public new double MTTF(double lambda) {
            return 1 / lambda;
        }

        public new void print(Cals cal, double[] args) {
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
                case Cals.MTTF:
                value = MTTF(args[0]);
                break;
                default:
                    throw new Exception();
            }
            string returnString = $"Function: {calString} returns {value} with the given values: (Lambda, Time) { String.Join(", ", args.Select(x => x.ToString())) }";
            Console.WriteLine(returnString);
        }
    }

    class Gamma : AbstractCalculator {

        public double Probability(double lambda, double gamma, double kelvin, double time) {
            return (lambda) / (gamma * (kelvin)) * Math.Pow(lambda * time, (kelvin - 1)) * Math.Pow(e, -lambda * time);
        }

        public double Survivor(double lambda, int kelvin, double time) {
            double result = 0;
            for (int x = 0; x < kelvin - 1; x++) {
                result += (Math.Pow(lambda * time, x) / factorial(x))*Math.Pow(e, -lambda * time);
            }
            return result;
        }

        public double FailureRate() {
            throw new NotImplementedException("Not possible using a function, do this manual");
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

