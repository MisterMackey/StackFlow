using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackFlow.EventArgClasses;
using StackFlow.Models;
using StackFlowTests;

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
                Assert.IsTrue(sesh.CompletedItems.Count == i + 1);
            }
            Assert.IsTrue(sesh.CompletedItems.TrueForAll(x => x.ClosedDate != null));
            //TODO : tests for floating stack once i figure out how i want that thing to behave in the first place
        }
    }
}