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
            plot = new PlotView();
            plot.Model = model;
            plot.Text = "Priority over time";
            plot.Show();
        }
    }
}
