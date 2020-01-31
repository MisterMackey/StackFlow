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

        [TestMethod()]
        public void InsertItemIntoStackTest()
        {
            WorkStack test = new WorkStack("test", "");
            //add some items
            for (int i = 0; i < 3; i++)
            {
                test.Push(new WorkStackItem($"{i}"));
            }
            //add an item we gonna use as parent
            WorkStackItem t = new WorkStackItem("hoi");
            test.Push(t);
            //moar items
            for (int i = 3; i < 6; i++)
            {
                test.Push(new WorkStackItem($"{i}"));
            }
            //add item on top of parent
            WorkStackItem a = new WorkStackItem("lemme step right in");
            WorkStackProcedures.InsertItemIntoStack(test, t, a);
            Assert.IsTrue(test.Contains(a));
            //unroll the whole thing and check that the order is correct
            WorkStackItem[] testarr = new WorkStackItem[8];
            int cnt = test.Count;
            for (int i = 0; i < cnt; i++)
            {
                testarr[i] = test.Pop();
            }
            //should be 3 items (6,5,4), then a, then t.
            Assert.AreSame(testarr[3], a);
            Assert.AreSame(testarr[4], t);
        }
    }
}