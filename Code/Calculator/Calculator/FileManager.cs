using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;

namespace Calculator {
    class FileManager {
        string line = "";
        public string file = "";
        public FileManager(string file) {
            this.file = file;
        }


    }

    class FaultTreeManager : FileManager {
        public FaultTreeManager(string file) : base(file) {
        }

        private Node GetNodes(XElement element) {
            List<Node> nodes = new List<Node>();
            Node node = new Node();
            double value = 0.0;
            if(element.HasElements) {
                IEnumerable<XElement> els = element.Elements();
                foreach(XElement el in els) {
                    ;
                    node.AddChildNode(GetNodes(el).SetGateRelation(element.FirstAttribute.Value));
                }
            }
            else {
                try {
                    value = double.Parse(element.Value.Replace("\n", "").Replace("\t", ""), CultureInfo.InvariantCulture);
                }
                catch(Exception e) {
                    Console.WriteLine(e.ToString());
                }
                node.SetValue(value);
            }
            node.SetName(element.Name.LocalName);
            Console.WriteLine(element.Name.LocalName);
            return node;
        }

        public FaultTree GetFaultTreeFromConfig() {
            Node node = new Node(); //TOPNODE
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            XmlReader streamReader = XmlReader.Create(file, settings);
            streamReader.Read();
            switch(streamReader.NodeType) {
                case XmlNodeType.Element:
                node.Name = streamReader.Name;
                if(streamReader.HasAttributes == false) {
                    throw new Exception("MISSING DECLARTION OF GATE IN TOPNODE!");
                }
                else {
                    if(streamReader.GetAttribute(0) == "OR")
                        node.SetGateRelation(GateType.OR);
                    else if(streamReader.GetAttribute(0) == "AND")
                        node.SetGateRelation(GateType.AND);
                    else
                        throw new Exception("MISSING DECLARTION OF GATE AFTER TOPNODE!");
                }
                XElement Xml = XElement.Load(streamReader);
                IEnumerable<XElement> elements = Xml.Elements();
                foreach(XElement element in elements) {
                    node.AddChildNode(GetNodes(element));
                }
                return new FaultTree(node);
                default:
                throw new Exception("INVALID XML FILE, REQUIRE FIRST ELEMENT TO BE A NODE!");
            }
        }
    }

    class CSVFileManager : FileManager {
        public CSVFileManager(string file, List<double> values) : base(file) {
            using (StreamWriter sw = new StreamWriter($"{file}.csv")) {
            foreach(double value in values) {
                sw.WriteLine(value);
                }
            }
        }
    }
}
