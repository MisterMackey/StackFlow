using StackFlow.Models;
using System;

namespace StackFlow.EventArgClasses
{
    public enum ActiveStackModificationTypes
    {
        //these types should operate on the head of the stack
        ItemCompleted,
        ItemAdded,
        ItemModified,
        //these types may operate on any item in the stack
        ItemRemoved,
        ItemInserted,
        ItemChanged
    }
    public class ActiveStackModificationEventArgs : EventArgs
    {
        public ActiveStackModificationTypes TypeOfChange { get; set; }
        public WorkStackItem NewItem { get; set; }


    }
}
