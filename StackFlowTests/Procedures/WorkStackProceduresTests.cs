using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackFlow.Models;

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
    }
}