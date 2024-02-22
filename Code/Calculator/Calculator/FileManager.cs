using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace Calculator {
    class FileManager {
        string line = "";
        string file = "";
        public FileManager(string file) {
            this.file = file;
        }

        private Node GetNodes(XElement element) {
            if(element.HasElements) {

            }
            IEnumerable<XElement> els = element.Elements();
            Node node = new Node();
            /*
            XmlReader StreamReader = null;
            node.Name = streamReader.Name;
            if(streamReader.HasAttributes == false) {
                throw new Exception($"MISSING DECLARTION OF GATE IN {streamReader.Name}!");
            }
            else {
                if(streamReader.GetAttribute(0) == "OR") node.SetGateRelation(GateType.OR);
                else if(streamReader.GetAttribute(0) == "AND") node.SetGateRelation(GateType.AND);
                else throw new Exception($"MISSING DECLARTION OF GATE AFTER {streamReader.Name}!");
            }
            while(streamReader.Read()) {
                if (streamReader.NodeType == XmlNodeType.Element) {
                    node.AddChildNode(GetNodes(streamReader.ReadSubtree()));
                }
                else if( streamReader.NodeType == XmlNodeType.Text) {
                    node.SetValue(double.Parse(streamReader.Value));
                }
            }*/
            return node;
        }

        public FaultTree GetFaultTreeFromConfig() {
            Node node = new Node();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            XmlReader streamReader = XmlReader.Create(file, settings);
            streamReader.Read();
            switch(streamReader.NodeType) {
                case XmlNodeType.Element:
                Console.WriteLine("<" + streamReader.Name + ">");
                node.Name = streamReader.Name;
                if(streamReader.HasAttributes == false) {
                    throw new Exception("MISSING DECLARTION OF GATE IN TOPNODE!");
                }
                else {
                    if(streamReader.GetAttribute(0) == "OR") node.SetGateRelation(GateType.OR);
                    else if(streamReader.GetAttribute(0) == "AND") node.SetGateRelation(GateType.AND);
                    else throw new Exception("MISSING DECLARTION OF GATE AFTER TOPNODE!");
                }
                XElement el = XElement.Load(streamReader);
                IEnumerable<XElement> els = el.Elements();
                //XmlReader innerReader = streamReader.ReadSubtree();
                foreach (XElement element in els) {
                    node.AddChildNode(GetNodes(element));
                }
                //node.AddChildNode(GetNodes(innerReader));
                //return new FaultTree(node);
                break;
                default:
                throw new Exception("INVALID XML FILE, REQUIRE FIRST ELEMENT TO BE A NODE!");
            }
            return null;
        }
    }

}
