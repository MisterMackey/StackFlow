using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Models.Tests
{
    [TestClass()]
    public class ActiveTimeSpanOverloadedOperatorTests
    {
        [TestMethod()]
        public void ActiveTimeSpanOperatorTests()
        {
            //implicit operator datetimeoffset to activetimespan
            DateTimeOffset dto = DateTimeOffset.Now;
            ActiveTimeSpan obj = dto; //conversion happens here
            Assert.AreEqual(obj.ActivatedAbsoluteTime, dto);

            //implicit operater activetimespan to timespan?
            TimeSpan ts = new TimeSpan(1000);
            obj.ActiveTime = ts;
            TimeSpan? newtS = obj; //conversion happens here
            Assert.AreEqual(newtS, ts);

            //equals operator, it compares timespans
            ActiveTimeSpan obj2 = new ActiveTimeSpan() { ActiveTime = new TimeSpan(2000) };
            Assert.AreNotEqual(obj, obj2);
            obj2.ActiveTime = new TimeSpan(1000);
            Assert.AreEqual(obj, obj2);
        }
    }
}
