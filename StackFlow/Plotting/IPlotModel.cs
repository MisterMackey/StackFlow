using OxyPlot;
using StackFlow.Statistics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Plotting
{
    public interface IPlotModel
    {  
        public PlotModel GetModel(PriorityDistributionOverTime data, DateTimeOffset From, DateTimeOffset Until);    
    }
}
