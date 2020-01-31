using StackFlow.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackFlow.Procedures
{
    public static class WorkStackProcedures
    {
        public static WorkStackItem CompleteTopItem(WorkStack Stack)
        {
            var CompletedItem = Stack.Pop();
            CompletedItem.ClosedDate = DateTime.Now;
            Stack.CompletedItems.Add(CompletedItem);
            return CompletedItem;
        }

        public static void AddNewTask(WorkStackItem newItem, WorkStack Stack)
        {
            Stack.Push(newItem);
        }

        public static void ModifyTop(WorkStackItem newItem, WorkStack Stack)
        {
            var item = Stack.Pop();
            item.Description = newItem.Description;
            item.Name = newItem.Name;
            item.Notes.Clear();
            foreach (string note in newItem.Notes)
            {
                item.Notes.Add(note);
            }
            item.Priority = newItem.Priority;
            Stack.Push(item);
        }
        public static void SetItemComplete(WorkStack Stack, WorkStackItem Item)
        {
            if (!Stack.Contains(Item)) { throw new ArgumentException("Item not present in Stack"); }
            Stack<WorkStackItem> tempStack = new Stack<WorkStackItem>();
            WorkStackItem currItem = Stack.Pop();
            while (currItem != Item)
            {
                tempStack.Push(currItem);
                currItem = Stack.Pop();
            }
            Stack.Push(Item);//add it back
            CompleteTopItem(Stack); //complete it properly
            //and add the other stuff back
            while (tempStack.Any())
            {
                Stack.Push(tempStack.Pop());
            }
        }
    }
}
