using StackFlow.Procedures;
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

        [TestMethod()]
        public void CompleteStackTest()
        {
            StackFlowSession sesh = new StackFlowSession();
            WorkStack stack = new WorkStack("fdsa", "fds");
            sesh.ActiveStack = stack;
            sesh.Session.Add(stack);
            //the following should simple complete the stack, add it to completed stacks and there should be no new active stack
            SessionProcedures.CompleteStack(sesh, false);
            Assert.IsNull(sesh.ActiveStack);
            Assert.IsTrue(sesh.Session.Count == 0);
            Assert.IsTrue(sesh.CompletedStacks.Contains(stack));
            //we will add 3 new stacks, both with some items. completing without setting the override ought the throw an exception.
            //completing with setting the override ought to complete all the tasks within, complete the stack, and set the stack with highest prio as the new active stack
            WorkStack[] stacks = new WorkStack[] {
            new WorkStack("original", ""),
            new WorkStack("low prio", ""),
            new WorkStack("high prio", "")};
            sesh.ActiveStack = stacks[0];
            sesh.Session.AddRange(stacks);
            sesh.ActiveStack.Push(new WorkStackItem("blocking"));
            stacks[1].Push(new WorkStackItem("low prio item", "", WorkStackItemPriority.Whenever));
            stacks[2].Push(new WorkStackItem("high prio item", WorkStackItemPriority.PantsOnFire));

            Assert.ThrowsException<InvalidOperationException>(() => SessionProcedures.CompleteStack(sesh, false));
            SessionProcedures.CompleteStack(sesh, true);
            Assert.IsTrue(stacks[0].CompletedItems.Count == 1);
            Assert.AreSame(expected: stacks[2], actual: sesh.ActiveStack);
        }

        [TestMethod()]
        public void SwitchActiveStackTest()
        {
            WorkStack[] stacks = new WorkStack[] {
            new WorkStack("original", ""),
            new WorkStack("low prio", ""),
            new WorkStack("high prio", "")};
            StackFlowSession sesh = new StackFlowSession();

            sesh.ActiveStack = stacks[0];
            sesh.Session.AddRange(stacks);
            SessionProcedures.SwitchActiveStack(sesh, stacks[1]);
            Assert.AreSame(expected: stacks[1], actual: sesh.ActiveStack);
            var hurp = new WorkStack("new", "");
            SessionProcedures.SwitchActiveStack(sesh, hurp);
            Assert.AreSame(expected: hurp, actual: sesh.ActiveStack);
            Assert.IsTrue(sesh.Session.Contains(hurp));
            SessionProcedures.SwitchActiveStack(sesh, hurp); //should not error out
        }
    }
}
