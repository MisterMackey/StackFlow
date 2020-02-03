using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.WindowsForms;
using StackFlow.Plotting;

namespace StackFlow.SupportingForms
{
    public partial class TestForm : Form
    {
        public OxyPlot.WindowsForms.PlotView PlotView;
        public TestForm()
        {
            InitializeComponent();
            PlotView = new PlotView();
            this.Controls.Add(PlotView);
            SuspendLayout();
            PlotView.Model = PriorityDistribution.GetTestPlotModel();
            PlotView.Visible = true;
            PlotView.Location = new Point(0,0);
            PlotView.Size = new Size(500, 500);
            PlotView.Show();
            PerformLayout();
        }
    }
}
