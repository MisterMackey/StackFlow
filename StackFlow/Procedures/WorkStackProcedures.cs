﻿using StackFlow.EventArgClasses;
using StackFlow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Procedures
{
    public static class WorkStackProcedures
    {
        public static WorkStackItem CompleteTopItem(WorkStack Stack)
        {
            var CompletedItem = Stack.Pop();
            CompletedItem.ClosedDate = DateTime.Now;
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
    }
}