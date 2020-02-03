using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Plotting
{
    public class PriorityDistribution
    {
        public static PlotModel GetTestPlotModel()
        {
            PlotModel model = new PlotModel()
            {
                Title = "Test Plot "
            };
            var axisx = new CategoryAxis() { Title = "Categories" };
            axisx.Labels.AddRange(new string[] { "One", "Two", "Three", "Four" });
            model.Axes.Add(axisx);
            //model.Axes.Add(new LinearAxis());
            model.Axes.Add(new LinearAxis() { AbsoluteMaximum = 50, AbsoluteMinimum = 0 });
            for (int i = 0; i < 4; i++)
            {
                LineSeries series = new LineSeries();
                List<DataPoint> items = new List<DataPoint>();
                for (int k = 0; k < 10; k++)
                {
                    items.Add(new DataPoint(k, i * k));
                }
                series.ItemsSource = items;
                series.Title = $"Series {i}";
                model.Series.Add(series);
            }
            return model;
        }
        
    }
}
