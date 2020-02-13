using StackFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackFlow.Statistics
{
    public class PriorityDistributionOverTime
    {
        private IStackFlowDataCollections Collection;
        public PriorityDistributionOverTime(IEnumerable<StackFlowSession> source)
        {
            Collection = StackFlowDataCollections.CreateStackFlowDataCollection(source);
            Collection.Initialize();
        }

        public TimeSpan GetTimeWorkedOnPriority(WorkStackItemPriority Priority, DateTimeOffset From, DateTimeOffset Until)
        {
            TimeSpan timeSpan = new TimeSpan(ticks:0);
            int[] StackIds = Collection.Stacks.
                Where(stack => stack.Priority == Priority).
                Select(s => s.Id).
                GroupBy(id => id).
                Select(grouping => grouping.Key).
                ToArray();

            ActiveTimeSpan sumSpan = Collection.Times.
                Where(time => StackIds.Contains(time.StackId)).
                Select(time => time.ActiveTimeSpan).
                Sum();
            return timeSpan;
        }
    }
}
