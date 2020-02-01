using StackFlow.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace StackFlow.Procedures
{
    public static class SessionProcedures
    {
        #region Public

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
            AddNewWorkStack(parentSession, newStack, setActive);
        }
        public static void AddNewWorkStack(StackFlowSession parentSession, WorkStack newStack, bool setActive)
        {
            parentSession.Session.Add(newStack);
            if (setActive)
            {
                SetActiveStack(parentSession, newStack);
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
            parentSession.Session.Remove(stack);
            stack.SetInActive();
            //set the new active session as the first stack in the remaining list that has the max prio
            parentSession.ActiveStack = null; // in case its the last stack
            if (parentSession.Session.Any())
            {
                //first session that satisfies condition: prio equals max prio found in list
                WorkStack newActiveStack = parentSession.Session.First(stk => stk.Priority ==
                parentSession.Session.Max(x => x.Priority));
                parentSession.ActiveStack = newActiveStack;
                newActiveStack.SetActive();
            }
            return stack;
        }
        public static void SwitchActiveStack(StackFlowSession parentSession, WorkStack newActiveStack)
        {
            //alrdy active?
            if (parentSession.ActiveStack == newActiveStack) { return; }
            //is this a newly created stack or is it in session already?
            if (parentSession.Session.Contains(newActiveStack))
            {
                SetActiveStack(parentSession, newActiveStack);
            }
            else
            {
                AddNewWorkStack(parentSession, newActiveStack, true);
            }
        }

        public static void SetActive(this StackFlowSession Session)
        {
            if (Session.IsActive) { return; }
            Session.IsActive = true;
            ActiveTimeSpan span = DateTimeOffset.Now;
            Session.PeriodsWhenActivated.Add(span);
        }
        public static void SetInActive(this StackFlowSession Session)
        {
            if (!Session.IsActive) { return; }
            Session.IsActive = false;
            var span = Session.PeriodsWhenActivated.Last();
            DateTimeOffset n = DateTimeOffset.Now;
            span.ClosedAbsoluteTime = n;
            span.ActiveTime = TimeSpan.FromTicks(span.ActivatedAbsoluteTime.Ticks - n.Ticks);
        }
        #endregion

        #region Private

        /// <summary>
        /// used internally to set an active stack while also handling the assignment of active timespans
        /// </summary>
        /// <param name="sesh"></param>
        /// <param name="stack"></param>
        private static void SetActiveStack(StackFlowSession sesh, WorkStack stack)
        {
            sesh.ActiveStack.SetInActive();
            sesh.ActiveStack = stack;
            stack.SetActive();
        }
        #endregion
    }
}
