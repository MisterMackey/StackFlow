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
                int i = 0;
                categorySource.Add(prio);
                var time = data.GetTimeWorkedOnPriority(p, From, Until);
                double value = time.TotalMinutes;
                seriesSource.Add(new BarItem(value, i++));
            }
            series.ItemsSource = seriesSource;
            categoryAxis.ItemsSource = categorySource;
            categoryAxis.Title = "Priority";
            series.Title = "Time spent in minutes";

            model.Series.Add(series);
            model.Axes.Add(categoryAxis);

            return model;
        }
    }
}

/*
 *             PlotModel model = new PlotModel()
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
        */
