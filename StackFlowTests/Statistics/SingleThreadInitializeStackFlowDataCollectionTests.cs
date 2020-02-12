using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackFlow.Models;
using StackFlow.Statistics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Statistics.Tests
{
    [TestClass()]
    public class SingleThreadInitializeStackFlowDataCollectionTests
    {
        [TestMethod()]
        public void InitializeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SingleThreadInitializeStackFlowDataCollectionTest()
        {
            var obj = StackFlowDataCollections.CreateStackFlowDataCollection(new List<StackFlowSession>());
        }
    }
}