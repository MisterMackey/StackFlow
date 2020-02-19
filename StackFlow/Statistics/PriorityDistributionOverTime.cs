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
            int[] StackIds = Collection.Stacks.
                Where(stack => stack.Priority == Priority).
                Select(s => s.Id).
                GroupBy(id => id).
                Select(grouping => grouping.Key).
                ToArray();

            var sumActiveTicks = Collection.Times.
                Where(time => time.ActiveTimeSpan.ActivatedAbsoluteTime >= From && time.ActiveTimeSpan.ClosedAbsoluteTime <= Until).
                Where(time => StackIds.Contains(time.StackId)).
                Where(time => time.ActiveTimeSpan.ActivatedAbsoluteTime != null).
                Select(time => time.ActiveTimeSpan).
                Sum(span => span.ActiveTime.Value.Ticks);
            TimeSpan timeSpan = new TimeSpan(ticks: sumActiveTicks);
            return timeSpan;
        }
    }
}
