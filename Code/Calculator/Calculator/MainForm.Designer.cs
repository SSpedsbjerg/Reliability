namespace Calculator {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            mainPlotView = new OxyPlot.WindowsForms.PlotView();
            LabelCount = new Label();
            Calculate = new Button();
            CountTextBox = new TextBox();
            MeanTextBox = new TextBox();
            LabelMean = new Label();
            STDTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            Quatiles = new Label();
            LowQuatileTextBox = new TextBox();
            MidQuatileTextBox = new TextBox();
            HighQuatileTextBox = new TextBox();
            Lower = new Label();
            label3 = new Label();
            label4 = new Label();
            DisDropDown = new ComboBox();
            label5 = new Label();
            MaxTextBox = new TextBox();
            label6 = new Label();
            MinTextBox = new TextBox();
            label7 = new Label();
            label8 = new Label();
            Variance = new TextBox();
            SuspendLayout();
            // 
            // mainPlotView
            // 
            mainPlotView.Location = new Point(318, 12);
            mainPlotView.Name = "mainPlotView";
            mainPlotView.PanCursor = Cursors.Hand;
            mainPlotView.Size = new Size(470, 500);
            mainPlotView.TabIndex = 0;
            mainPlotView.Text = "plotView1";
            mainPlotView.ZoomHorizontalCursor = Cursors.SizeWE;
            mainPlotView.ZoomRectangleCursor = Cursors.SizeNWSE;
            mainPlotView.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // LabelCount
            // 
            LabelCount.AutoSize = true;
            LabelCount.Location = new Point(24, 42);
            LabelCount.Name = "LabelCount";
            LabelCount.Size = new Size(60, 25);
            LabelCount.TabIndex = 1;
            LabelCount.Text = "Count";
            // 
            // Calculate
            // 
            Calculate.Location = new Point(21, 488);
            Calculate.Name = "Calculate";
            Calculate.Size = new Size(112, 34);
            Calculate.TabIndex = 2;
            Calculate.Text = "Calculate";
            Calculate.UseVisualStyleBackColor = true;
            Calculate.Click += button1_Click;
            // 
            // CountTextBox
            // 
            CountTextBox.Location = new Point(193, 39);
            CountTextBox.Name = "CountTextBox";
            CountTextBox.Size = new Size(92, 31);
            CountTextBox.TabIndex = 3;
            // 
            // MeanTextBox
            // 
            MeanTextBox.Location = new Point(193, 76);
            MeanTextBox.Name = "MeanTextBox";
            MeanTextBox.Size = new Size(92, 31);
            MeanTextBox.TabIndex = 5;
            // 
            // LabelMean
            // 
            LabelMean.AutoSize = true;
            LabelMean.Location = new Point(24, 79);
            LabelMean.Name = "LabelMean";
            LabelMean.Size = new Size(56, 25);
            LabelMean.TabIndex = 4;
            LabelMean.Text = "Mean";
            // 
            // STDTextBox
            // 
            STDTextBox.Location = new Point(193, 113);
            STDTextBox.Name = "STDTextBox";
            STDTextBox.Size = new Size(92, 31);
            STDTextBox.TabIndex = 7;
            STDTextBox.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 116);
            label1.Name = "label1";
            label1.Size = new Size(56, 25);
            label1.TabIndex = 6;
            label1.Text = "Mean";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 113);
            label2.Name = "label2";
            label2.Size = new Size(163, 25);
            label2.TabIndex = 8;
            label2.Text = "Standard Deviation";
            // 
            // Quatiles
            // 
            Quatiles.AutoSize = true;
            Quatiles.Location = new Point(20, 199);
            Quatiles.Name = "Quatiles";
            Quatiles.Size = new Size(76, 25);
            Quatiles.TabIndex = 9;
            Quatiles.Text = "Quatiles";
            // 
            // LowQuatileTextBox
            // 
            LowQuatileTextBox.Location = new Point(24, 256);
            LowQuatileTextBox.Name = "LowQuatileTextBox";
            LowQuatileTextBox.Size = new Size(92, 31);
            LowQuatileTextBox.TabIndex = 10;
            // 
            // MidQuatileTextBox
            // 
            MidQuatileTextBox.Location = new Point(122, 256);
            MidQuatileTextBox.Name = "MidQuatileTextBox";
            MidQuatileTextBox.Size = new Size(92, 31);
            MidQuatileTextBox.TabIndex = 11;
            // 
            // HighQuatileTextBox
            // 
            HighQuatileTextBox.Location = new Point(220, 256);
            HighQuatileTextBox.Name = "HighQuatileTextBox";
            HighQuatileTextBox.Size = new Size(92, 31);
            HighQuatileTextBox.TabIndex = 12;
            // 
            // Lower
            // 
            Lower.AutoSize = true;
            Lower.Location = new Point(24, 228);
            Lower.Name = "Lower";
            Lower.Size = new Size(59, 25);
            Lower.TabIndex = 13;
            Lower.Text = "Lower";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(122, 228);
            label3.Name = "label3";
            label3.Size = new Size(43, 25);
            label3.TabIndex = 14;
            label3.Text = "Mid";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(220, 228);
            label4.Name = "label4";
            label4.Size = new Size(50, 25);
            label4.TabIndex = 15;
            label4.Text = "High";
            // 
            // DisDropDown
            // 
            DisDropDown.FormattingEnabled = true;
            DisDropDown.Items.AddRange(new object[] { "Normal" });
            DisDropDown.Location = new Point(24, 335);
            DisDropDown.Name = "DisDropDown";
            DisDropDown.Size = new Size(182, 33);
            DisDropDown.TabIndex = 16;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(21, 307);
            label5.Name = "label5";
            label5.Size = new Size(153, 25);
            label5.TabIndex = 17;
            label5.Text = "Distrubution Type";
            // 
            // MaxTextBox
            // 
            MaxTextBox.Location = new Point(193, 434);
            MaxTextBox.Name = "MaxTextBox";
            MaxTextBox.Size = new Size(92, 31);
            MaxTextBox.TabIndex = 21;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(24, 437);
            label6.Name = "label6";
            label6.Size = new Size(138, 25);
            label6.TabIndex = 20;
            label6.Text = "Maximum Value";
            // 
            // MinTextBox
            // 
            MinTextBox.Location = new Point(193, 397);
            MinTextBox.Name = "MinTextBox";
            MinTextBox.Size = new Size(92, 31);
            MinTextBox.TabIndex = 19;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(24, 400);
            label7.Name = "label7";
            label7.Size = new Size(135, 25);
            label7.TabIndex = 18;
            label7.Text = "Minimum Value";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(24, 153);
            label8.Name = "label8";
            label8.Size = new Size(77, 25);
            label8.TabIndex = 23;
            label8.Text = "Variance";
            // 
            // Variance
            // 
            Variance.Location = new Point(193, 153);
            Variance.Name = "Variance";
            Variance.Size = new Size(92, 31);
            Variance.TabIndex = 22;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 534);
            Controls.Add(label8);
            Controls.Add(Variance);
            Controls.Add(MaxTextBox);
            Controls.Add(label6);
            Controls.Add(MinTextBox);
            Controls.Add(label7);
            Controls.Add(label5);
            Controls.Add(DisDropDown);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(Lower);
            Controls.Add(HighQuatileTextBox);
            Controls.Add(MidQuatileTextBox);
            Controls.Add(LowQuatileTextBox);
            Controls.Add(Quatiles);
            Controls.Add(label2);
            Controls.Add(STDTextBox);
            Controls.Add(MeanTextBox);
            Controls.Add(LabelMean);
            Controls.Add(CountTextBox);
            Controls.Add(Calculate);
            Controls.Add(LabelCount);
            Controls.Add(mainPlotView);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OxyPlot.WindowsForms.PlotView mainPlotView;
        private Label LabelCount;
        private Button Calculate;
        private TextBox CountTextBox;
        private TextBox MeanTextBox;
        private Label LabelMean;
        private TextBox STDTextBox;
        private Label label1;
        private Label label2;
        private Label Quatiles;
        private TextBox LowQuatileTextBox;
        private TextBox MidQuatileTextBox;
        private TextBox HighQuatileTextBox;
        private Label Lower;
        private Label label3;
        private Label label4;
        private ComboBox DisDropDown;
        private Label label5;
        private TextBox MaxTextBox;
        private Label label6;
        private TextBox MinTextBox;
        private Label label7;
        private Label label8;
        private TextBox Variance;
    }
}