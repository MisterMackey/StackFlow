using OxyPlot;
using OxyPlot.WindowsForms;
using StackFlow.Plotting;
using StackFlow.Statistics;
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
        private PriorityDistributionOverTime data;
        public PlotForm(PlotModel model, PriorityDistributionOverTime data)
        {
            this.data = data;
            InitializeComponent();
            DrawPlotView(model);
            DateTimePickerFrom.Value = DateTime.Now.AddDays(-14); //init the from picker to 2 weeks ago
            BindEventHandlers();
        }

        private void BindEventHandlers()
        {
            ButtonRegenerate.Click += RegeneratePlotView;
        }

        private void RegeneratePlotView(object sender, EventArgs e)
        {
            var from = DateTimePickerFrom.Value;
            var to = DateTimePickerTo.Value;
            PriorityDistributionPlotModel newModel = new PriorityDistributionPlotModel();
            var plotmodel = newModel.GetModel(this.data, from, to);
            DrawPlotView(plotmodel);
        }

        private void DrawPlotView(PlotModel model)
        {
            TabMainPage.TabPages["TimeStats"].Controls.Remove(plot);
            plot = new PlotView();            
            TabMainPage.TabPages["TimeStats"].Controls.Add(plot);
            SuspendLayout();
            plot.Model = model;
            plot.Text = "Priority over time";
            plot.Visible = true;
            plot.Size = new Size(900, 900);
            plot.Location = new Point(50, 50);
            plot.Show();
            PerformLayout();
        }
    }
}
