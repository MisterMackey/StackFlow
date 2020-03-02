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
            InitializeComponent();
            DrawPlotView(model);
            DateTimePickerFrom.Value = DateTime.Now.AddDays(-14); //init the from picker to 2 weeks ago
        }

        private void DrawPlotView(PlotModel model)
        {
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
