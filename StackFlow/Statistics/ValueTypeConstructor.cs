using StackFlow.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Statistics
{
    /// <summary>
    /// holds definitions for structs used by <see cref="StackFlowDataCollections"/> to achieve data locality
    /// essentially they are valuetype versions of the classes under models with some small adjustments 
    /// to make it easier and more efficient to query certain things (at the cost of some memory).
    /// </summary>
    #region Types
        //todo: seperate the struct defeiniteion from the methods creating them.
    public struct Session
    {
        public char[] Name;
        public int Id;
    }
    public struct Stack
    {
        public char[] Name;
        public char[] Description;
        public WorkStackItemPriority Priority;
        public DateTimeOffset Opened;
        public DateTimeOffset? Closed;
        public int Id;
        public int SessionId;
    }
    public struct Item
    {
        public char[] Name;
        public char[] Description;
        public WorkStackItemPriority Priority;
        public DateTimeOffset Opened;
        public DateTimeOffset? Closed;
        public char[][] Notes;
        public int Id;
        public int StackId;
        public int SessionId;
    }
    public struct ActiveTime
    {
        public ActiveTimeSpan ActiveTimeSpan;
        public int Id;
        public int ItemId;
        public int StackId;
        public int SessionId;
    }
#endregion
    /// <summary>
    /// Holds methods to construct the valuetypes whiel also dealing with ID assignment
    /// </summary>
    public class ValueTypeConstructor
    {
        private IdTracker idTracker = new IdTracker();

        #region Public Methods
        public Session TransformToSessionStruct(StackFlowSession Source)
        {
            Session s = new Session();
            s.Name = Source.Name.ToCharArray();
            s.Id = idTracker.getNextSessionId();
            return s;
        }

        public Stack TransformToStackStruct(WorkStack Source, int SessionId)
        {
            Stack s = new Stack();
            s.Name = Source.Name.ToCharArray();
            s.Description = Source.Description.ToCharArray();
            s.Priority = Source.Priority;
            s.Opened = Source.Opened;
            s.Closed = Source.Closed;
            s.Id = idTracker.getNextStackId();
            s.SessionId = SessionId;
            return s;
        }
        public Item TransformToItemStruct(WorkStackItem Source, int SessionId, int StackId)
        {
            Item s = new Item();
            s.Name = Source.Name.ToCharArray();
            s.Description = Source.Description.ToCharArray();
            s.Priority = Source.Priority;
            s.Opened = Source.CreatedDate;
            s.Closed = Source.ClosedDate;
            int countNotes = Source.Notes.Count;
            s.Notes = new char[countNotes][];
            for (int i = 0; i < countNotes; i++)
            {
                s.Notes[i] = Source.Notes[i].ToCharArray();
            }
            s.Id = idTracker.getNextItemId();
            s.SessionId = SessionId;
            s.StackId = StackId;
            return s;
        }
        public ActiveTime TransformToActiveTime(ActiveTimeSpan Source, int SessionId, int StackId, int ItemId)
        {
            ActiveTime s = new ActiveTime();
            s.ActiveTimeSpan = Source;
            s.SessionId = SessionId;
            s.StackId = StackId;
            s.ItemId = ItemId;
            return s;
        }

        #endregion

        #region private Stuff
        private class IdTracker
        {
            private int currentHighestSessionId =0;
            private int currentHighestStackId =0;
            private int currentHighestItemId=0;
            private int currentHighestTimespanIdId=0;
            public int getNextSessionId()
            {
                return currentHighestSessionId++;
            }
            public int getNextStackId()
            {
                return currentHighestStackId++;
            }
            public int getNextItemId()
            {
                return currentHighestItemId++;
            }
            public int getNextTimeSpanId()
            {
                return currentHighestTimespanIdId++;
            }
        }
        #endregion
    }
}
