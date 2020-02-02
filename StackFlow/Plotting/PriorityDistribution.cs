using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Plotting
{
    public class PriorityDistribution
    {
        public void init()
        {
            var testmodel = new PlotModel();
            var series = new BarSeries();
            var iti = new List<BarItem>();
            BarItem itum = new BarItem(value: 2);
            iti.Add(itum);
            series.ItemsSource = iti;
            testmodel.Series.Add(series);

            PlotView view = new PlotView();
            view.Model = testmodel;
            //todo: draw a form with this and play around
            
        }
        
    }
}
