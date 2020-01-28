﻿using StackFlow.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace StackFlow.Procedures
{
    public static class SessionProcedures
    {
        public static void SaveSession(string filepath, StackFlowSession session)
        {
            Stream stream;
            if (File.Exists(filepath))
            {
                stream = File.OpenWrite(filepath);
            }
            else
            {
                stream = File.Create(filepath);
            }
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, session);
            stream.Close();
        }

        public static void AddNewWorkStack(StackFlowSession parentSession, string nameOfNewStack, string descriptionOfNewStack, bool setActive)
        {
            WorkStack newStack = new WorkStack(nameOfNewStack, descriptionOfNewStack);
            parentSession.Session.Add(newStack);
            if (setActive)
            {
                parentSession.ActiveStack = newStack; 
            }
            newStack.Push(new WorkStackItem("RootItem", "Default Item", WorkStackItemPriority.Whenever));
        }

        public static StackFlowSession LoadSession(string filepath)
        {
            Stream stream;
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException("File not found");
            }
            stream = File.OpenRead(filepath);
            BinaryFormatter formatter = new BinaryFormatter();
            StackFlowSession session = (StackFlowSession)formatter.Deserialize(stream);
            stream.Close();
            return session;
        }
        public static WorkStack CompleteStack(StackFlowSession parentSession, bool setIncompleteItemsToComplete)
        {
            WorkStack stack = parentSession.ActiveStack;
            if (stack.Any() && setIncompleteItemsToComplete) //any items left in the stack requires override
            {
                while (stack.Any())
                {
                    var retItem = WorkStackProcedures.CompleteTopItem(stack);
                    retItem.Description = retItem.Description + "\n\n======\n\nSet to complete automatically due to completion of parent workstack";
                }
            }
            else if (stack.Any() && !setIncompleteItemsToComplete)
            {
                throw new InvalidOperationException("Attempted to complete workstack containing incomplete childitems. Close childitems or set the bool in the method call to true");
            }
            parentSession.CompletedStacks.Add(stack);
            //set the new active session as the first stack in the remaining list that has the max prio
            parentSession.ActiveStack = null; // in case its the last stack
            if (parentSession.Session.Any())
            {
                //first session that satisfies condition: prio equals max prio found in list
                WorkStack newActiveStack = parentSession.Session.First(stk => stk.Priority ==
                parentSession.Session.Max(x => x.Priority));
                parentSession.ActiveStack = newActiveStack;
            }
            return stack;
        }
    }
}
