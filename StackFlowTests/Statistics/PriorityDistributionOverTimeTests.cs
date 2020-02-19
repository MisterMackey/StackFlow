using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackFlow.Models;
using StackFlow.Statistics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Statistics.Tests
{
    [TestClass()]
    public class PriorityDistributionOverTimeTests
    {
        [TestMethod()]
        public void PriorityDistributionOverTimeTest()
        {
            StackFlowSession[] sauce = new StackFlowSession[1];
            sauce[0] = new StackFlowSession();
            PriorityDistributionOverTime test = new PriorityDistributionOverTime(sauce);
        }

        [TestMethod()]
        public void GetTimeWorkedOnPriorityTest()
        {
            StackFlowSession sesh = new StackFlowSession();
            //take all possible values of the enum and loop over them to protect against future modifications
            var valuesOfPriority = Enum.GetNames(typeof(WorkStackItemPriority));
            for (int i = 0; i < valuesOfPriority.Length; i++)
            {
                ActiveTimeSpan span = new ActiveTimeSpan();
                span.ActiveTime = new TimeSpan(hours: i + 1, minutes: 0, seconds: 0);
                span.ActivatedAbsoluteTime = DateTimeOffset.MinValue;
                span.ClosedAbsoluteTime = DateTimeOffset.MaxValue;
                WorkStack stack = new WorkStack("test", "will work 1h on prio 1, 2h on prio 2 and so on");
                stack.PeriodsWhenActivated.Add(span);
                stack.Push(new WorkStackItem("test", "stack takes prio from its items", Priority: (WorkStackItemPriority)Enum.Parse(typeof(WorkStackItemPriority), valuesOfPriority[i])));
                sesh.Session.Add(stack);
            }

            //get the prioritydistribution
            PriorityDistributionOverTime test = new PriorityDistributionOverTime(new StackFlowSession[] { sesh });
            for (int i = 0; i < valuesOfPriority.Length; i++)
            {
                var time = test.GetTimeWorkedOnPriority(
                    (WorkStackItemPriority)Enum.Parse(typeof(WorkStackItemPriority), valuesOfPriority[i]), DateTimeOffset.MinValue, DateTimeOffset.MaxValue);
                Assert.IsTrue(time.TotalHours == i + 1);
            }
        }
    }
}