using StackFlow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Statistics.Structs
{
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

}
