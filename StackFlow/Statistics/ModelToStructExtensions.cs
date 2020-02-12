using StackFlow.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using StackFlow.Statistics.Structs;

namespace StackFlow.Statistics
{
    /// <summary>
    /// holds some extension methods for stackflow.models namespace which converts them to structs with an ID
    /// </summary>
    public static class ModelToStructExtensions
    {

        #region Public Methods
        public static Session TransformToSessionStruct(this StackFlowSession Source, int SessionId)
        {
            Session s = new Session();
            s.Name = Source.Name != null ? Source.Name.ToCharArray() : null;
            s.Id = SessionId;
            return s;
        }

        public static Stack TransformToStackStruct(this WorkStack Source, int SessionId, int StackId)
        {
            Stack s = new Stack();
            s.Name = Source.Name != null ? Source.Name.ToCharArray() : null;
            s.Description = Source.Description != null ? Source.Description.ToCharArray() : null;
            s.Priority = Source.Priority;
            s.Opened = Source.Opened;
            s.Closed = Source.Closed;
            s.Id = StackId;
            s.SessionId = SessionId;
            return s;
        }
        public static Item TransformToItemStruct(this WorkStackItem Source, int SessionId, int StackId, int ItemId)
        {
            Item s = new Item();
            s.Name = Source.Name != null ? Source.Name.ToCharArray() : null;
            s.Description = Source.Description != null ? Source.Description.ToCharArray() : null;
            s.Priority = Source.Priority;
            s.Opened = Source.CreatedDate;
            s.Closed = Source.ClosedDate;
            int countNotes = Source.Notes.Count;
            s.Notes = new char[countNotes][];
            for (int i = 0; i < countNotes; i++)
            {
                s.Notes[i] = Source.Notes[i].ToCharArray();
            }
            s.Id = ItemId;
            s.SessionId = SessionId;
            s.StackId = StackId;
            return s;
        }
        public static ActiveTime TransformToActiveTime(this ActiveTimeSpan Source, int SessionId, int StackId, int ItemId, int TimeId)
        {
            ActiveTime s = new ActiveTime();
            s.ActiveTimeSpan = Source;
            s.SessionId = SessionId;
            s.StackId = StackId;
            s.ItemId = ItemId;
            s.Id = TimeId;
            return s;
        }

        #endregion


    }
}
