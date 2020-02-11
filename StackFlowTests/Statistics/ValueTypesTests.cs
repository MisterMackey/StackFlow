using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackFlow.Models;
using StackFlow.Statistics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Statistics.Tests
{
    [TestClass()]
    public class ValueTypesTests
    {
        [TestMethod()]
        public void TransformToSessionStructTest()
        {
            StackFlowSession sesh = new StackFlowSession() { Name = "test", };
            var res = sesh.TransformToSessionStruct(0);
            Assert.IsTrue(res.Id == 0);
            Assert.AreEqual(new string(res.Name), "test");
            sesh = new StackFlowSession() { Name = "derp", };
            res = sesh.TransformToSessionStruct(1);
            Assert.IsTrue(res.Id == 1);
            Assert.AreEqual(new string(res.Name), "derp");
        }

        [TestMethod()]
        public void TransformToStackStructTest()
        {
            WorkStack stack = new WorkStack("test", "");
            var res = stack.TransformToStackStruct(0,0);
            Assert.IsTrue(res.Id == 0);
            Assert.AreEqual(new string(res.Name), stack.Name);
            Assert.AreEqual(res.Priority, stack.Priority);
            Assert.AreEqual(res.SessionId, 0);
        }

        [TestMethod()]
        public void TransformToItemStructTest()
        {
            WorkStackItem it = new WorkStackItem("test", "pfff", WorkStackItemPriority.Today);
            var res = it.TransformToItemStruct(0,0,0);
            Assert.AreEqual(new string(res.Name), it.Name);
            Assert.AreEqual(new string(res.Description), it.Description);
            Assert.AreEqual(res.Priority, it.Priority);
            Assert.AreEqual(res.Opened, it.CreatedDate);
            Assert.IsTrue(res.Id == 0 && res.SessionId == 0 && res.StackId == 0);
        }

        [TestMethod()]
        public void TransformToActiveTimeTest()
        {
            ActiveTimeSpan a = new ActiveTimeSpan();
            var res = a.TransformToActiveTime(0,0, 0, 0);
            Assert.AreEqual(res.ActiveTimeSpan, a);
            Assert.IsTrue(res.Id == 0 && res.SessionId == 0 && res.StackId == 0 && res.ItemId == 0);
        }
    }
}