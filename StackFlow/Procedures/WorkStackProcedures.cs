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
            var item = Stack.Peek();
            item.Description = newItem.Description;
            item.Name = newItem.Name;
            item.Notes.Clear();
            foreach (string note in newItem.Notes)
            {
                item.Notes.Add(note);
            }
            item.Priority = newItem.Priority;
        }
        public static void SetItemComplete(WorkStack Stack, WorkStackItem Item)
        {
            //unroll all items on top of it
            Stack<WorkStackItem> tempStack = UnRollStackUntilItemEncountered(Stack, Item);
            Stack.Push(Item);//add it back
            CompleteTopItem(Stack); //complete it properly
             //and add the other stuff back
            PutItemsBack(Stack, tempStack);
        }
        public static void InsertItemIntoStack(WorkStack Stack, WorkStackItem NewItemDesiredParent, WorkStackItem NewItem)
        {
            //unroll all items on top of it
            Stack<WorkStackItem> tempStack = UnRollStackUntilItemEncountered(Stack, NewItemDesiredParent);
            Stack.Push(NewItemDesiredParent);//add parent back
            Stack.Push(NewItem);//add new item
            //and add the other stuff back
            PutItemsBack(Stack, tempStack);
        }
        public static void SetActive(this WorkStack Stack)
        {
            if (Stack.IsActive) { return; }
            Stack.IsActive = true;
            ActiveTimeSpan span = DateTimeOffset.Now;
            Stack.PeriodsWhenActivated.Add(span);
        }
        public static void SetInActive(this WorkStack Stack)
        {
            if (!Stack.IsActive) { return; }
            Stack.IsActive = false;
            var span = Stack.PeriodsWhenActivated.Last(); //valuetype! creates a copy
            DateTimeOffset n = DateTimeOffset.Now;
            span.ClosedAbsoluteTime = n;
            span.ActiveTime = TimeSpan.FromTicks(span.ActivatedAbsoluteTime.Ticks - n.Ticks);
            //replace the last value
            Stack.PeriodsWhenActivated.RemoveAt(Stack.PeriodsWhenActivated.Count - 1);
            Stack.PeriodsWhenActivated.Add(span);
        }

        private static void PutItemsBack(WorkStack Stack, Stack<WorkStackItem> tempStack)
        {
            while (tempStack.Any())
            {
                Stack.Push(tempStack.Pop());
            }
        }

        private static Stack<WorkStackItem> UnRollStackUntilItemEncountered(WorkStack Stack, WorkStackItem Item)
        {
            if (!Stack.Contains(Item)) { throw new ArgumentException("Item not present in Stack"); }
            Stack<WorkStackItem> tempStack = new Stack<WorkStackItem>();
            WorkStackItem currItem = Stack.Pop();
            while (currItem != Item)
            {
                tempStack.Push(currItem);
                currItem = Stack.Pop();
            }

            return tempStack;
        }
    }
}
