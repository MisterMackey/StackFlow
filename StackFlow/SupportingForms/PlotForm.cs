using OxyPlot;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace StackFlow.SupportingForms
{
    public partial class PlotForm : Form
    {
        private PlotView plot;
        public PlotForm(PlotModel model)
        {

            this.Size = new Size(1000, 1000);
            InitializeComponent();
            plot = new PlotView();
            Controls.Add(plot);
            SuspendLayout();
            plot.Model = model;
            plot.Text = "Priority over time";
            plot.Visible = true;
            plot.Size = new Size(900,900);
            plot.Location = new Point(50, 50);
            plot.Show();
            PerformLayout();
        }
    }
}
/*
 *             InitializeComponent();
            PlotView = new PlotView();
            this.Controls.Add(PlotView);
            SuspendLayout();
            PlotView.Model = PriorityDistribution.GetTestPlotModel();
            PlotView.Visible = true;
            PlotView.Location = new Point(0,0);
            PlotView.Size = new Size(500, 500);
            PlotView.Show();
            PerformLayout();
*/