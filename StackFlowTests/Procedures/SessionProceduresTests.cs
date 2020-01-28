using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackFlow.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace StackFlow.Procedures.Tests
{
    [TestClass()]
    public class SessionProceduresTests
    {
        [TestMethod()]
        public void SerializationAndDeserializationTest()
        {
            StackFlowSession s = new StackFlowSession();
            s.Session = new List<WorkStack>();
            for (int i = 0; i < 3; i++)
            {
                s.Session.Add(new WorkStack($"stack {i}", ""));
                for (int k = 0; k < 10; k++)
                {
                    s.Session[i].Push(new WorkStackItem($"item {k}"));
                }
            }
            //k so now we have a session with 3 stacks and 10 items in each stack

            DirectoryInfo dir = new DirectoryInfo(Environment.CurrentDirectory);

            SessionProcedures.SaveSession(dir.FullName + @"\testfile.dat", s);

            Assert.IsTrue(File.Exists(dir.FullName + @"\testfile.dat"));

            //k lets try loading it back into memory.
            StackFlowSession d = SessionProcedures.LoadSession(dir.FullName + @"\testfile.dat");

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(expected: s.Session[i].Name, actual: d.Session[i].Name);
                for (int k = 0; k < 10; k++)
                {
                    Assert.AreEqual(expected: s.Session[i].Pop().Name, actual: d.Session[i].Pop().Name);
                }
            }
        }

        [TestMethod()]
        public void AddNewWorkStackTest()
        {
            StackFlowSession sesh = new StackFlowSession();
            SessionProcedures.AddNewWorkStack(sesh, "new", "lololol", true);
            Assert.IsTrue(sesh.Session.Count == 1);
            Assert.IsTrue(sesh.ActiveStack.Name == "new");
            SessionProcedures.AddNewWorkStack(sesh, "blab", "Lo", false);
            Assert.IsTrue(sesh.Session.Count == 2);
            Assert.IsTrue(sesh.ActiveStack.Name == "new");
        }

    }
}
