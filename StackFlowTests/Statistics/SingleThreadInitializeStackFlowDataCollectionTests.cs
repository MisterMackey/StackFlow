using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackFlow.Models;
using StackFlow.Statistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace StackFlow.Statistics.Tests
{
    [TestClass()]
    public class SingleThreadInitializeStackFlowDataCollectionTests
    {
        [TestMethod()]
        public void InitializeTest()
        {
            //todo: put this in a helper class
            //create some stuff to initialize
            //for dem performance tests, right now its 13.3s for multiplier of 10 on a 4.2Ghz ryzen 3600x. a.k.a. atrocious
            //multiplier of 1 is 14ms tho so for small sessions the singlethreaded approach will work for now.
            int multiplier = 1; 
            int numSessions = 1 * multiplier;
            int stacksPerSession = 50 * multiplier;
            int itemsPerStack = 10 * multiplier;
            int activeTimesPerStackOrItem = 10 * multiplier;

            List<StackFlowSession> sourceList = new List<StackFlowSession>();
            for (int i = 0; i < numSessions; i++)
            {
                sourceList.Add(new StackFlowSession());
                for (int j = 0; j < stacksPerSession; j++)
                {
                    sourceList[i].Session.Add(new WorkStack("test", "test"));
                    for (int k = 0; k < itemsPerStack; k++)
                    {
                        sourceList[i].Session[j].Push(new WorkStackItem("test"));
                        for (int l = 0; l < activeTimesPerStackOrItem; l++)
                        {
                            sourceList[i].Session[j].Peek().PeriodsWhenActivated.Add(new ActiveTimeSpan());
                        }
                        for (int l = 0; l < activeTimesPerStackOrItem; l++)
                        {
                            sourceList[i].Session[j].PeriodsWhenActivated.Add(new ActiveTimeSpan());
                        }
                    }
                }
            }

            var coll = StackFlowDataCollections.CreateStackFlowDataCollection(sourceList);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            coll.Initialize();
            stopwatch.Stop();
            Assert.IsTrue(coll.Sessions.Count() == numSessions);
            Assert.IsTrue(coll.Stacks.Count() == numSessions * stacksPerSession);
            Console.WriteLine($"{stopwatch.ElapsedMilliseconds} milliseconds elapsed");
        }

        [TestMethod()]
        public void SingleThreadInitializeStackFlowDataCollectionTest()
        {
            var obj = StackFlowDataCollections.CreateStackFlowDataCollection(new List<StackFlowSession>());
        }
    }
}