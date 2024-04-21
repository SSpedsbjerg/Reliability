using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator {
    class CountDistrobution {
        double[] values;
        double margin = 1;
        double min = 0;
        double max = 100;
        private int partitions = 0;

        public void SetData(double[] values) {
            this.values = values;
        }

        public void SetMargin(double margin) {
            this.margin = margin;
        }

        public void SetMargin(int number) {
            double difference = max - min;
            this.margin = difference / number;
        }

        public void SetMin(double min) {
            this.min = min;
        }

        public void SetMax(double max) {
            this.max = max;
        }

        private void CalculatePartitions() {
            const double roundingErrorFix = 0.99;
            double difference = max - min;
            this.partitions = (int)((difference / margin) + roundingErrorFix);
        }

        public Tuple<double, int>[] Calculate() {
            CalculatePartitions();
            List<double> values = this.values.ToList();
            values.Sort();
            List<Tuple<double, int>> tupValues = new List<Tuple<double, int>>();
            for (int i = 0; i < partitions; i++) {
                double locationValue = min + (margin * i);
                int instances = 0;
                double listLocation = 0;
                while (locationValue >= listLocation) {
                    if (values.ElementAt(i) <= locationValue) {
                        instances++;
                    }
                    listLocation = values.ElementAt(i);
                }
                tupValues.Append(new Tuple<double, int>(locationValue, instances));
            }
            return tupValues.ToArray();
        }
   }
}
