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
            ValueTypes vt = new ValueTypes();
            StackFlowSession sesh = new StackFlowSession() { Name = "test", };
            var res = vt.TransformToSessionStruct(sesh);
            Assert.IsTrue(res.Id == 0);
            Assert.AreEqual(new string(res.Name), "test");
            sesh = new StackFlowSession() { Name = "derp", };
            res = vt.TransformToSessionStruct(sesh);
            Assert.IsTrue(res.Id == 1);
            Assert.AreEqual(new string(res.Name), "derp");
        }

        [TestMethod()]
        public void TransformToStackStructTest()
        {
            ValueTypes vt = new ValueTypes();
            WorkStack stack = new WorkStack("test", "");
            var res = vt.TransformToStackStruct(stack, 0);
            Assert.IsTrue(res.Id == 0);
            Assert.AreEqual(new string(res.Name), "test");
        }

        [TestMethod()]
        public void TransformToItemStructTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void TransformToActiveTimeTest()
        {
            Assert.Fail();
        }
    }
}