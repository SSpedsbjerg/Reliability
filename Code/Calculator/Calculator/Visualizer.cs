using MathNet.Numerics;
using OxyPlot;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Calculator {

    interface IVisualizer {
        bool AddSet(string name, double[] values);
        bool RemoveSet(string name);
        void Display(PlotView plotView);
        void SetPlotName(string name);
    }

    //Visualize 1d datasets
    class Visualizer : IVisualizer {
        List<OxyPlot.Series.LineSeries> lines = new List<OxyPlot.Series.LineSeries>();
        string plotName = "INSERT PLOTNAME";
        List<Tuple<string, double[]>> valuesSets = new List<Tuple<string, double[]>>();
        public bool AddSet(string name, double[] values) {
            valuesSets.Add(Tuple.Create(name, values));
            return true;
        }

        public void Display(PlotView plotView) {
            foreach(Tuple<string, double[]> sets in valuesSets) {
                var line = new OxyPlot.Series.LineSeries() {
                    Title = sets.Item1,
                    Color = OxyPlot.OxyColors.Blue,
                    StrokeThickness = 1,
                    MarkerSize = 2,
                    MarkerType = OxyPlot.MarkerType.Circle
                };
                for(int i = 0; i < sets.Item2.Length; i++) {
                    line.Points.Add(new OxyPlot.DataPoint(i, sets.Item2[i]));
                }
                lines.Add(line);
            }
            var model = new OxyPlot.PlotModel { Title = plotName };
            foreach (var line in lines) {
                model.Series.Add(line);
            }
            plotView.Model = model;
            plotView.Show();
        }

        public bool RemoveSet(string name) {
            foreach(Tuple<string, double[]> sets in valuesSets) {
                if(name == sets.Item1) {
                    valuesSets.Remove(sets);
                    return true;
                }
            }
            Console.WriteLine($"Couldn't find a set with the name of {name}");
            return false;
        }

        public void SetPlotName(string name) {
            this.plotName = name;
        }
    }

    //TODO: Implement 2d and 3d visualizer
}
