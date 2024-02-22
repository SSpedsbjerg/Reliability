﻿using System.Runtime.CompilerServices;

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

            FileManager manager = new FileManager("C:/Users/simon/Documents/GitHub/Reliability/Code/ConfigFiles/Lecture2.xml");
            manager.GetFaultTreeFromConfig();
            FaultTree tree = new FaultTree();

            /*
            double function = 4.28 * Math.Pow(10, -4);
            double time = 720;
            double[] values = { function, time };
            Exponential exponential = new Exponential();
            exponential.print(Cals.Survivor, values);*/
            return 0;
        }
    }

    enum GateType {
        AND,
        OR
    }

    class Node {
        double value;
        public List<Node> ChildNodes = null;
        public List<Node> ParentNodes = null;
        public GateRelation GateRelation = null;
        public string Name = "";
        public Node() {
            ChildNodes = new List<Node>();
            ParentNodes = new List<Node>();
        }
        
        public Node AddChildNode(Node node) {
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

        public void SetValue(double value) {
            this.value = value;
        }

        public void addGateRelation(GateRelation gateRelation) {
            GateRelation = gateRelation;
        }

        public void SetGateRelation(GateType type) {
            GateRelation = new GateRelation(this, type);
        }

        public bool Update() {
            GateRelation.AddChildNotes(ChildNodes);
            Console.WriteLine(value);
            return false;
        }
    }

    class GateRelation {
        private GateType GateType;
        double value = -1;
        Node parrentNode;

        public GateRelation(Node parrentNode, GateType gate) {
            this.parrentNode = parrentNode;
            this.GateType = gate;
        }

        public void AddChildNotes(List<Node> nodes) {
            double value = 0;
            if(GateType == GateType.OR) {
                value = 1;

                foreach (Node node in nodes) {
                    if (node is ValueNode) {
                        ValueNode v_node = (ValueNode)node;
                        value *= (1 - v_node.GetValue());
                    }
                }
                this.value = 1 - value;
                Console.WriteLine(value);
                parrentNode.SetValue(value);
                return;
            }

            else if (GateType == GateType.AND) {

            }
        }

        public void SetGateType(GateType gateType) {
            this.GateType = gateType;
        }

        public GateType GetGateType() {
            return this.GateType;
        }

        public double GetValue() {
            return value;
        }

        public bool Update() {
            return false;
        }

    }

    class ValueNode : Node {
        double value;
        public ValueNode SetValue(double value) {
            this.value = value;
            foreach (Node parent in ParentNodes) {
                parent.Update();
            }
            return this;
        }

        public double GetValue() {
            return value;
        }
    }

    class FaultTree {
        List<Node> nodes = null;
        Node TopNode = null;

        public FaultTree(Node node) {
            TopNode = node;
        }
        public FaultTree() {
            nodes = new List<Node>();
            Node TopNode = new Node();
                ValueNode Employee_Resigns = new ValueNode();
                    ValueNode Retirement = new ValueNode();
                        ValueNode Opportunity = new ValueNode();
                            ValueNode Recruitment = new ValueNode();
                            ValueNode Applied = new ValueNode();
                        ValueNode Personal_reasons = new ValueNode();
                            ValueNode Unsatisfied = new ValueNode();
                            ValueNode Bad_Work_Environment = new ValueNode();
                            ValueNode Health_Reasons = new ValueNode();
                ValueNode Employee_Laid_Off = new ValueNode();
                    ValueNode Unsaisfactory_Employee = new ValueNode();
                        ValueNode Bad_Work_Ethic = new ValueNode();
                            ValueNode Social = new ValueNode();
                            ValueNode Lazy = new ValueNode();
                        ValueNode Lack_Of_Ability = new ValueNode();
                    ValueNode Cuts = new ValueNode();
                        ValueNode Department_Not_Needed = new ValueNode();
                        ValueNode Budget_Cuts = new ValueNode();

            Cuts.AddChildNode(Department_Not_Needed);
            Cuts.AddChildNode(Budget_Cuts);

            Bad_Work_Ethic.AddChildNode(Social);
            Bad_Work_Ethic.AddChildNode(Lazy);

            Unsaisfactory_Employee.AddChildNode(Bad_Work_Ethic);
            Unsaisfactory_Employee.AddChildNode(Lack_Of_Ability);

            Employee_Laid_Off.AddChildNode(Cuts);
            Employee_Laid_Off.AddChildNode(Unsaisfactory_Employee);

            Personal_reasons.AddChildNode(Unsatisfied);
            Personal_reasons.AddChildNode(Bad_Work_Environment);
            Personal_reasons.AddChildNode(Health_Reasons);

            Opportunity.AddChildNode(Recruitment);
            Opportunity.AddChildNode(Applied);

            Retirement.AddChildNode(Opportunity);
            Retirement.AddChildNode(Personal_reasons);

            Employee_Resigns.AddChildNode(Retirement);

            TopNode.AddChildNode(Employee_Resigns);
            TopNode.AddChildNode(Employee_Laid_Off);


            nodes.AddRange(new List<Node> {
                TopNode,
                Employee_Resigns,
                Retirement,
                Opportunity,
                Personal_reasons,
                Employee_Laid_Off,
                Unsaisfactory_Employee,
                Bad_Work_Ethic,
                Cuts,
                Budget_Cuts,
                Department_Not_Needed,
                Lack_Of_Ability,
                Lazy,
                Social,
                Health_Reasons,
                Bad_Work_Environment,
                Unsatisfied,
                Applied,
                Recruitment
            });

            Recruitment.SetValue(0.2);
            Applied.SetValue(0.5);
            Unsatisfied.SetValue(0.1);
            Bad_Work_Environment.SetValue(0.2);
            Health_Reasons.SetValue(0.3);
            Social.SetValue(0.4);
            Lazy.SetValue(0.7);
            Lack_Of_Ability.SetValue(0.1);
            Department_Not_Needed.SetValue(0.2);
            Budget_Cuts.SetValue(0.3);

            foreach (Node node in nodes) {
                if(node.hasChild() && node.GateRelation is null) {
                    node.addGateRelation(new GateRelation(node, GateType.OR));
                }
            }

            TopNode.Update();


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

