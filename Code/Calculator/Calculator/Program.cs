using MathNet.Numerics.LinearAlgebra.Factorization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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

    // The class that handles the creation of the application windows
    //I stole this class
    public class MyApplicationContext : ApplicationContext {

        private int _formCount;
        private MainForm _form1;

        private Rectangle _form1Position;
        private Rectangle _form2Position;

        private FileStream _userData;

        public MyApplicationContext() {

            _formCount = 0;

            // Handle the ApplicationExit event to know when the application is exiting.
            Application.ApplicationExit += new EventHandler(this.OnApplicationExit);

            try {
                // Create a file that the application will store user specific data in.
                _userData = new FileStream(Application.UserAppDataPath + "\\appdata.txt", FileMode.OpenOrCreate);
            }
            catch(IOException e) {
                // Inform the user that an error occurred.
                MessageBox.Show("An error occurred while attempting to show the application." +
                                "The error is:" + e.ToString());

                // Exit the current thread instead of showing the windows.
                ExitThread();
            }

            // Create both application forms and handle the Closed event
            // to know when both forms are closed.
            _form1 = new Calculator.MainForm();
            _form1.Closed += new EventHandler(OnFormClosed);
            _form1.Closing += new CancelEventHandler(OnFormClosing);
            _formCount++;


            // Get the form positions based upon the user specific data.
            if(ReadFormDataFromFile()) {
                // If the data was read from the file, set the form
                // positions manually.
                _form1.StartPosition = FormStartPosition.Manual;

                _form1.Bounds = _form1Position;
            }

            // Show both forms.
            _form1.Show();
        }

        private void OnApplicationExit(object sender, EventArgs e) {
            // When the application is exiting, write the application data to the
            // user file and close it.
            WriteFormDataToFile();

            try {
                // Ignore any errors that might occur while closing the file handle.
                _userData.Close();
            }
            catch { }
        }

        private void OnFormClosing(object sender, CancelEventArgs e) {
            // When a form is closing, remember the form position so it
            // can be saved in the user data file.
            if(sender is MainForm)
                _form1Position = ((Form)sender).Bounds;
        }

        private void OnFormClosed(object sender, EventArgs e) {
            // When a form is closed, decrement the count of open forms.

            // When the count gets to 0, exit the app by calling
            // ExitThread().
            _formCount--;
            if(_formCount == 0) {
                ExitThread();
            }
        }

        private bool WriteFormDataToFile() {
            // Write the form positions to the file.
            UTF8Encoding encoding = new UTF8Encoding();

            RectangleConverter rectConv = new RectangleConverter();
            string form1pos = rectConv.ConvertToString(_form1Position);
            string form2pos = rectConv.ConvertToString(_form2Position);

            byte[] dataToWrite = encoding.GetBytes("~" + form1pos + "~" + form2pos);

            try {
                // Set the write position to the start of the file and write
                _userData.Seek(0, SeekOrigin.Begin);
                _userData.Write(dataToWrite, 0, dataToWrite.Length);
                _userData.Flush();

                _userData.SetLength(dataToWrite.Length);
                return true;
            }
            catch {
                // An error occurred while attempting to write, return false.
                return false;
            }
        }

        private bool ReadFormDataFromFile() {
            // Read the form positions from the file.
            UTF8Encoding encoding = new UTF8Encoding();
            string data;

            if(_userData.Length != 0) {
                byte[] dataToRead = new byte[_userData.Length];

                try {
                    // Set the read position to the start of the file and read.
                    _userData.Seek(0, SeekOrigin.Begin);
                    _userData.Read(dataToRead, 0, dataToRead.Length);
                }
                catch(IOException e) {
                    string errorInfo = e.ToString();
                    // An error occurred while attempt to read, return false.
                    return false;
                }

                // Parse out the data to get the window rectangles
                data = encoding.GetString(dataToRead);

                try {
                    // Convert the string data to rectangles
                    RectangleConverter rectConv = new RectangleConverter();
                    string form1pos = data.Substring(1, data.IndexOf("~", 1) - 1);

                    _form1Position = (Rectangle)rectConv.ConvertFromString(form1pos);

                    string form2pos = data.Substring(data.IndexOf("~", 1) + 1);
                    _form2Position = (Rectangle)rectConv.ConvertFromString(form2pos);

                    return true;
                }
                catch {
                    // Error occurred while attempting to convert the rectangle data.
                    // Return false to use default values.
                    return false;
                }
            }
            else {
                // No data in the file, return false to use default values.
                return false;
            }
        }
    }

        public class Program {
        [STAThread]
        public static void Main(string[] args) {
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
            //Application.EnableVisualStyles();

            NormalDistrubtion normalDis = new NormalDistrubtion();
            ContinousData conDis = new ContinousData();

            //List<double> values = normalDis.GenerateNormalDataset(110, 5086.53, 1000, 2000, 6700, -406.26, 8548.20);
            for(int i = 0; i < 5; i++) {
                conDis.SetLowerQuartile(200).SetHighQuartile(6700).SetMin(-406.26).SetMax(8548.2).SetMean(5086.53).SetCount(110);
                conDis.setRequiredDataPercentage(75);
                conDis.SetTargetRange(6100, 6700);
                List<double> values = conDis.GenerateData();
                Console.WriteLine($"Total Count: {values.Count}");
                _ = new CSVFileManager($"Feed_Flow_{i}", values);
            }

            /*
            values = normalDis.GenerateNormalDataset(100, 93.33, 20, 20, 100, 6.66, 123.59);
            Console.WriteLine($"Total Count: {values.Count}");

            _ = new CSVFileManager("Feed_Temp", values);

            values = normalDis.GenerateNormalDataset(107, 116.07, 20, 40, 130, 5.4, 174.69);
            Console.WriteLine($"Total Count: {values.Count}");

            _ = new CSVFileManager("Bottom_temp", values);

            values = normalDis.GenerateNormalDataset(103, 71.69, 20, 24, 82, 5.83, 104.96);
            Console.WriteLine($"Total Count: {values.Count}");

            _ = new CSVFileManager("Top_Temp", values);

            values = normalDis.GenerateNormalDataset(108, 70.54, 20, 2000, 85, -0.02, 90.98);
            Console.WriteLine($"Total Count: {values.Count}");

            _ = new CSVFileManager("Reboiler_flow", values);*/
            /*Visualizer visualizer = new();
            visualizer.AddSet("1", normalValues.ToArray());
            visualizer.SetPlotName("First Attempt");
            */
            MyApplicationContext context = new MyApplicationContext();

            Application.Run(context);
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

