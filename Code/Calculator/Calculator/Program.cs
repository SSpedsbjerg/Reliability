using MathNet.Numerics.LinearAlgebra.Factorization;
using System.Runtime.CompilerServices;

namespace Calculator {

    enum Cals {
        Probability,
        Survivor,
        FailureRate,
        MTTF
    }

    /**
     * 
     * Created by: Simon dos Reis Spedsbjerg
     * 
     */

    /**
     * 
     *  How to
     *  
     *  Simply create your variables, and insert them into the function, some are not able to be calculated by using this code which is due to the nature of calculation and are sumply easier and quicker to do in hand.
     *  Create an instance of the type you would want to use and then choose the function. The enum Cals defines the function you would want to perform.
     *  The print method is created to give data into the console in somewhat usefull format.
     * 
     */

    public static class Program {
        public static int Main(string[] args) {
            /*
            FileManager manager = new FileManager("C:/Users/simon/Documents/GitHub/Reliability/Code/ConfigFiles/Lecture2.xml");
            FaultTree tree = manager.GetFaultTreeFromConfig();
            Console.WriteLine($"Success: {tree.Update(tree.TopNode)}");
            Console.WriteLine(tree.ToString());
            */
            /*
            double function = 4.28 * Math.Pow(10, -4);
            double time = 720;
            double[] values = { function, time };
            Exponential exponential = new Exponential();
            exponential.print(Cals.Survivor, values);*/

            NormalDistrubtion normalDis = new NormalDistrubtion();
            List<double> normalValues = normalDis.GenerateNormalDataset(691, 116.071744, 29.964951, 110.599998, 128.10, 132.89999, 5.4, 174.69);
            return 0;
        }
    }

    enum GateType {
        AND,
        OR
    }

    class Node {
        public double value = -1;
        public List<Node> ChildNodes = null;
        public List<Node> ParentNodes = null;
        public GateRelation GateRelation = null;
        public string Name = "";
        public Node() {
            ChildNodes = new List<Node>();
            ParentNodes = new List<Node>();
        }
        public Node(double value) {
            ChildNodes = new List<Node>();
            ParentNodes = new List<Node>();
            this.value = value;
        }
        
        public Node AddChildNode(Node node) {
            node.ParentNodes.Add(this);
            ChildNodes.Add(node);
            return this;
        }

        public void AddParentNode(Node node) {
            node.AddChildNode(this);
        }

        public bool hasChild() {
            if (ChildNodes is not null) {
                if (ChildNodes.Count != 0) {
                    return true;
                }
            }
            return false;
        }

        public Node SetName(string Name) {
            this.Name = Name;
            return this;
        }

        public Node SetValue(double value) {
            this.value = value;
            return this;
        }

        public Node SetGateRelation(GateRelation gateRelation) {
            GateRelation = gateRelation;
            return this;
        }

        public Node SetGateRelation(GateType type) {
            GateRelation = new GateRelation(this, type);
            return this;
        }

        public Node SetGateRelation(string type) {
            if(type.Equals("OR")) {
                GateRelation = new GateRelation(this, GateType.OR);
                return this;
            }
            else if(type.Equals("AND")) {
                GateRelation = new GateRelation(this, GateType.AND);
                return this;
            }
            else
                return this;
            
        }

        public new string ToString() {
            return $"{this.Name} : {this.value} | Parents : {this.ParentNodes.ToArray().ToString()}, Children : {this.ChildNodes.ToArray().ToString()}";
        }
    }

    class GateRelation {
        private GateType GateType;
        Node parrentNode;

        public GateRelation(Node parrentNode, GateType gate) {
            this.parrentNode = parrentNode;
            this.GateType = gate;
        }

        public void SetGateType(GateType gateType) {
            this.GateType = gateType;
        }

        public GateType GetGateType() {
            return this.GateType;
        }
    }

    class ValueNode : Node {
        public new ValueNode SetValue(double value) {
            this.value = value;
            return this;
        }

        public double GetValue() {
            return value;
        }
    }

    class FaultTree {
        List<Node> nodes = null;
        public Node TopNode = null;

        private List<Node> GetAllChildNodes(Node parentNode) {
            List<Node> result = new List<Node>();
            if(parentNode.ChildNodes.Count != 0) {
                foreach(Node childNode in parentNode.ChildNodes) {
                    result.AddRange(GetAllChildNodes(childNode));
                    result.Add(parentNode);
                }
            }
            else if(parentNode.ChildNodes.Count == 0) {
                result.Add(parentNode);
            }
            result = result.Distinct().ToList();
            return result;
        }

        public FaultTree(Node node) {
            TopNode = node;
            nodes = GetAllChildNodes(TopNode);
        }

        public bool Update(Node topNode) {
            foreach(Node node in topNode.ChildNodes) {
                if(node.value == -1) {
                    Update(node);
                }
            }
            topNode.value = 1;
            double value = 1;
            foreach(Node node in topNode.ChildNodes) {
                value *= (1 - node.value);
            }
            topNode.value = 1 - value;
            return true;
            /*
            try {
                if(topNode.GateRelation.GetGateType() == GateType.OR) {
                    topNode.value = 1;
                    foreach(Node node in topNode.ChildNodes) {
                        topNode.value *= (1 - node.value);
                    }
                    return true;
                }
                else if(topNode.GateRelation.GetGateType() == GateType.AND) {
                    topNode.value = 0;
                    Console.WriteLine("NOT IMPLEMENTED UPDATE AND GATE");
                }
            }
            catch(Exception e) { //An error with some of the values which causes their gates to be null from the XML parser
                topNode.value = 1;
                foreach(Node node in topNode.ChildNodes) {
                    topNode.value *= (1 - node.value);
                }
                return true;
            }*/
            return false;
        }

        public override string ToString() {
            this.Update(TopNode);
            string returnString = "";
            foreach (Node node in nodes) {
                returnString += node.ToString() + "\n";
            }
            return returnString;
        }
    }



    abstract class AbstractCalculator {
        public const double e = Math.E;
        public int Factorial(int x) {
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
                result += (Math.Pow(lambda * time, x) / Factorial(x))*Math.Pow(e, -lambda * time);
            }
            return result;
        }

        public double FailureRate() {
            throw new NotImplementedException("Not possible using a function, do this manual");
        }
    }

    class Weibull : AbstractCalculator {
        public double Probability(double alpha, double lambda, double time) {
            throw new NotImplementedException();
        }

        public double Survivor() {
            throw new NotImplementedException();
        }

        public double FailureRate() {
            throw new NotImplementedException();
        }
    }

    class LogNormal : AbstractCalculator {
        public double Probability() {
            throw new NotImplementedException();
        }

        public double Survivor() {
            throw new NotImplementedException();
        }

        public double FailureRate() {
            throw new NotImplementedException();
        }
    }

    class BirnbaumSaunders : AbstractCalculator {
        public double Probability() {
            throw new NotImplementedException();
        }

        public double Survivor() {
            throw new NotImplementedException();
        }

        public double FailureRate() {
            throw new NotImplementedException();
        }
    }

    class GumbellSmallestExtreme : AbstractCalculator {
        public double Probability() {
            throw new NotImplementedException();
        }

        public double Survivor() {
            throw new NotImplementedException();
        }

        public double FailureRate() {
            throw new NotImplementedException();
        }
    }

    class InverseGaussian : AbstractCalculator {
        public double Probability() {
            throw new NotImplementedException();
        }

        public double Survivor() {
            throw new NotImplementedException();
        }

        public double FailureRate() {
            throw new NotImplementedException();
        }
    }
}

