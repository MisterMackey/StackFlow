using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackFlow.EventArgClasses;
using StackFlow.Models;
using StackFlowTests;
using System.Collections.Generic;

namespace StackFlow.Controllers.Tests
{
    [TestClass()]
    public class SessionControllerTests
    {
        [TestMethod()]
        public void InitializeTest()
        {
            var test = new TestForm();
            var controller = new SessionController();
            controller.Initialize(test);
        }
        [TestMethod()]
        public void SessionControllerTest()
        {
            var test = new TestForm();
            var controller = new SessionController();
            controller.Initialize(test);
            // we'll set the activestack explicitly
            StackFlowSession sesh = new StackFlowSession();
            WorkStack stackz = new WorkStack("teststack", "");
            sesh.ActiveStack = stackz;
            test.SetActiveSession(sesh);
            //put some new tasks and check that items are added

            for (int i = 0; i < 10; i++)
            {
                test.InvokeModifyActiveStack(
                    new ActiveStackModificationEventArgs()
                    {
                        TypeOfChange = ActiveStackModificationTypes.ItemAdded
                        ,
                        NewItem = new WorkStackItem($"{i}")
                    });
                Assert.IsTrue(stackz.Count == i + 1);
            }

            //nice, lets modify the top and check that the modification went through
            test.InvokeModifyActiveStack(
                new ActiveStackModificationEventArgs()
                {
                    TypeOfChange = ActiveStackModificationTypes.ItemModified,
                    NewItem = new WorkStackItem("MODDED SON")
                });
            Assert.AreEqual("MODDED SON", stackz.Peek().Name);

            //finally, lets pop a few items and check that they get a closeddate and are put into the completed list on the session
            for (int i = 0; i < 5; i++)
            {
                test.InvokeModifyActiveStack(
                        new ActiveStackModificationEventArgs()
                        {
                            TypeOfChange = ActiveStackModificationTypes.ItemCompleted,
                        });
                Assert.IsTrue(stackz.CompletedItems.Count == i + 1);
            }
            Assert.IsTrue(stackz.CompletedItems.TrueForAll(x => x.ClosedDate != null));
            //TODO : tests for floating stack once i figure out how i want that thing to behave in the first place
        }
        [TestMethod()]
        public void SessionInterruptTest()
        {
            var test = new TestForm();
            var controller = new SessionController();
            controller.Initialize(test);
            test.SetActiveSession(new StackFlowSession());
            //session should be emtpy
            Assert.IsTrue(test.GetActiveSession().Session.Count == 0);
            //invoke the interrupt, which should trigger the creation of a new workstack
            test.InvokeUserInterrupt(new WorkInterruptionEventArgs() { NameOfNewStack = "test", DescriptionOfNewStack ="teststtes"});
            //verify that one stack is created and its the active stack.
            Assert.IsTrue(test.GetActiveSession().ActiveStack.Name == "test");
            Assert.IsTrue(test.GetActiveSession().Session.Count == 1);
        }
        [TestMethod()]
        public void SessionModifyMidOfStack()
        {
            var test = new TestForm();
            var controller = new SessionController();
            controller.Initialize(test);
            test.SetActiveSession(new StackFlowSession());
            //add some stuff
            var sesh = test.GetActiveSession();
            WorkStackItem bottom = new WorkStackItem("bottom");
            WorkStackItem middle = new WorkStackItem("middle");
            WorkStackItem top = new WorkStackItem("top");
            sesh.ActiveStack.Push(bottom);
            sesh.ActiveStack.Push(top);
            test.InvokeModifyActiveStack(
                new ActiveStackModificationEventArgs()
                {
                    NewItem = middle,
                    DesiredParentIfInserting = bottom,
                    TypeOfChange = ActiveStackModificationTypes.ItemInserted
                }) ;
            List<WorkStackItem> testresults = new List<WorkStackItem>();
            foreach (var it in sesh.ActiveStack)
            {
                testresults.Add(it);
            }
            Assert.AreSame(testresults[1], middle);
            testresults.Clear();
            test.InvokeModifyActiveStack(
                new ActiveStackModificationEventArgs()
                {
                    NewItem = middle,
                    TypeOfChange = ActiveStackModificationTypes.ItemRemoved
                });
            Assert.IsFalse(sesh.ActiveStack.Contains(middle));
            Assert.IsTrue(sesh.ActiveStack.CompletedItems.Contains(middle));
        }
    }
}