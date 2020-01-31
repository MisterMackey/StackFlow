using StackFlow.Procedures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackFlow.Models;
using System;

namespace StackFlow.Procedures.Tests
{
    [TestClass()]
    public class WorkStackProceduresTests
    {
        [TestMethod()]
        public void CompleteTopItemTest()
        {
            WorkStack test = new WorkStack("test", "");
            var testitem = new WorkStackItem("hai");
            test.Push(testitem);
            var retrieved = WorkStackProcedures.CompleteTopItem(test);
            Assert.IsTrue(test.Count == 0);
            Assert.AreSame(retrieved, testitem);
            Assert.IsNotNull(retrieved.ClosedDate); //should be filled since we 'completed' it via the procedure
            Assert.IsTrue(test.CompletedItems.Contains(testitem));
        }

        [TestMethod()]
        public void AddNewTaskTest()
        {
            WorkStack test = new WorkStack("test", "");
            var testitem = new WorkStackItem("hai");
            WorkStackProcedures.AddNewTask(testitem, test);
            var retrieved = test.Pop();
            Assert.IsTrue(test.Count == 0);
            Assert.AreSame(retrieved, testitem);
            Assert.IsNull(retrieved.ClosedDate);//shouldnt be filled since we popped it directly and didn't use the procedure
        }

        [TestMethod()]
        public void ModifyTopTest()
        {
            WorkStack test = new WorkStack("test", "");
            var testitem = new WorkStackItem("hai");
            var slightlydifferentItem = new WorkStackItem("hello");
            test.Push(testitem);
            WorkStackProcedures.ModifyTop(slightlydifferentItem, test);
            var retrieved = test.Pop();
            //we did not remove the item and put a new one, we only updated the top item. this means the reference points to the same object still!
            Assert.AreSame(retrieved, testitem);
            Assert.AreNotSame(retrieved, slightlydifferentItem);
            //the name should now reflect the name of the slightly different item tho
            Assert.AreEqual(slightlydifferentItem.Name, retrieved.Name);
        }

        [TestMethod()]
        public void SetItemCompleteTest()
        {
            WorkStack test = new WorkStack("test", "");
            //add some items
            for (int i = 0; i < 3; i++)
            {
                test.Push(new WorkStackItem($"{i}"));
            }
            //add an item we gonna remove later
            WorkStackItem t = new WorkStackItem("hoi");
            test.Push(t);
            //moar items
            for (int i = 3; i < 6; i++)
            {
                test.Push(new WorkStackItem($"{i}"));
            }
            //remove the middle item
            WorkStackProcedures.SetItemComplete(test, t);
            Assert.IsFalse(test.Contains(t));
            Assert.IsTrue(test.CompletedItems.Contains(t));
            Assert.ThrowsException<ArgumentException>(() => WorkStackProcedures.SetItemComplete(test, t));
        }
    }
}