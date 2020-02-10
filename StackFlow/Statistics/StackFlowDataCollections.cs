using StackFlow.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StackFlow.Statistics
{
    /// <summary>
    /// Contains collections of the various object models used within stackflow. Unlike a typical session, which just has pointers everywhere, this class
    /// will first transform all objects into suitable <see cref="ValueTypeConstructor"/>and load them into Lists, thus achieving data locality and speeding up subsequent 
    /// requests to the data. Reporting features ustilizing loops over these collections should enjoy a significant speedup after initialization.
    /// </summary>
    public class StackFlowDataCollections
    {
        public IEnumerable<Session> Sessions { get; private set; }
        public IEnumerable<Stack> Stacks { get; private set; }
        public IEnumerable<Item> Items { get; private set; }
        public IEnumerable<ActiveTime> TimePeriods { get; private set; }
        private ValueTypeConstructor VConstructor = new ValueTypeConstructor();
        private List<Task> StackTasks = new List<Task>();
        private List<Task> ItemTasks = new List<Task>();
        private List<Task> ActiveTimeTasks = new List<Task>();
        private object StackLock = new object();
        private object ItemLock = new object();
        private object ActiveTimeLock = new object();
        //TODO: decide if i rly want to create that many task objects
        //heck lets try
        public void InitializeCollections(IEnumerable<StackFlowSession> SessionList)
        {
            
        }

        #region Initializer Methods
        private IEnumerable<Session> InitSessionsFromList(IEnumerable<StackFlowSession> source)
        {
            List<Session> sessions = new List<Session>(100);
            foreach (var s in source)
            {
                var str = VConstructor.TransformToSessionStruct(s);
                sessions.Add(str);
                List<WorkStack> ernie = new List<WorkStack>(s.CompletedStacks.Count + s.Session.Count);
                ernie.AddRange(s.CompletedStacks);
                ernie.AddRange(s.Session);
                StackTasks.Add(new Task(
                    () => ThreadSafeAddStacksToColl(ernie, str.Id)));
            }
            int size = sessions.Count;
            Session[] ret = new Session[size];
            sessions.CopyTo(ret);
            return ret;
        }
        
        private IEnumerable<Stack> InitStacksFromList(IEnumerable<WorkStack> source, int sessionId)
        {
            List<Stack> stacks = new List<Stack>(1000);
            foreach (var s in source)
            {
                var str = VConstructor.TransformToStackStruct(s, sessionId);
                stacks.Add(str);
                List<WorkStackItem> ernie = new List<WorkStackItem>(s.CompletedItems.Count + s.Count);
                ernie.AddRange(s.CompletedItems);
                ernie.AddRange(s);
                ItemTasks.Add(new Task(
                    () => ThreadSafeAddItemsToColl(ernie, sessionId, str.Id)));
                ActiveTimeTasks.Add(new Task(
                    () => ThreadSafeAddActiveTimesToColl(s.PeriodsWhenActivated, sessionId, str.Id)));
            }
            int size = stacks.Count;
            Stack[] ret = new Stack[size];
            stacks.CopyTo(ret);
            return ret;
        }
        private IEnumerable<Item>

        private void ThreadSafeAddStacksToColl(IEnumerable<WorkStack> source, int sessionId)
        {

        }

        private void ThreadSafeAddItemsToColl(IEnumerable<WorkStackItem> source, int sessionId, int stackId)
        {

        }

        private void ThreadSafeAddActiveTimesToColl(IEnumerable<ActiveTimeSpan> source, int sessionId, int stackId, int itemId = -1)
        {

        }
        #endregion


    }
}
