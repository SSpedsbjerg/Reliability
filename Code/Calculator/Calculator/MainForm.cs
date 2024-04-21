using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator {
    public partial class MainForm : Form {
        public MainForm() {
            InitializeComponent();
        }


        //Calculate Button
        private void button1_Click(object sender, EventArgs e) {
            double lowQ = double.Parse(LowQuatileTextBox.Text);
            double midQ = double.Parse(MidQuatileTextBox.Text);
            double highQ = double.Parse(HighQuatileTextBox.Text);
            int count = int.Parse(CountTextBox.Text);
            double mean = double.Parse(MeanTextBox.Text);
            double std = double.Parse(STDTextBox.Text);
            double min = double.Parse(MinTextBox.Text);
            double max = double.Parse(MaxTextBox.Text);
            double var = double.Parse(Variance.Text);
            Console.WriteLine("Parsed in values");
            //normals
            if(DisDropDown.SelectedIndex == 0) {
                NormalDistrubtion ns = new NormalDistrubtion();
                ns.SetMean(mean)
                    .SetMin(min)
                    .SetMax(max)
                    .SetCount(count)
                    .SetHighQuartile(highQ)
                    .SetLowerQuartile(lowQ)
                    .SetStandardDeviation(std)
                    .SetVariance(var);
                Console.WriteLine("Parsed in values into the normal Distrubtion");
                List<double> data = ns.GenerateNormalDataset();
                Console.WriteLine("Generated the Data");
                Visualizer vis = new Visualizer();
                vis.AddSet("set 1", data.ToArray());
                Console.WriteLine("Added in the data");
                vis.Display(mainPlotView);
                Console.WriteLine("Displaying the data");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
        }
    }
}
