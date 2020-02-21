using System;
using System.Collections.Generic;
using System.Text;
using OxyPlot;
using OxyPlot.WindowsForms;
using OxyPlot.Series;
using StackFlow.Statistics;
using StackFlow.Models;
using OxyPlot.Axes;

namespace StackFlow.Plotting
{
    public class PriorityDistributionPlotModel : IPlotModel
    {
        public PlotModel GetModel(PriorityDistributionOverTime data, DateTimeOffset From, DateTimeOffset Until)
        {
            PlotModel model = new PlotModel();
            BarSeries series = new BarSeries();
            List<BarItem> seriesSource = new List<BarItem>();
            CategoryAxis categoryAxis = new CategoryAxis();
            List<string> categorySource = new List<string>();
            var priorityList = Enum.GetNames(typeof(WorkStackItemPriority));

            foreach (var prio in priorityList)
            {
                WorkStackItemPriority p = (WorkStackItemPriority)Enum.Parse(typeof(WorkStackItemPriority), prio);

                categorySource.Add(prio);
                var time = data.GetTimeWorkedOnPriority(p, From, Until);
                double value = time.TotalMinutes;
                seriesSource.Add(new BarItem(value));
            }
            series.ItemsSource = seriesSource;
            categoryAxis.ItemsSource = categorySource;
            categoryAxis.Title = "Priority";
            series.Title = "Time spent in minutes";

            return model;
        }
    }
}
