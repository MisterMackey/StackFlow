using StackFlow.Models;
using System;

namespace StackFlow.EventArgClasses
{
    public class FloatingStackModificationEventArgs : EventArgs
    {
        public ActiveStackModificationTypes TypeOfChange { get; set; }
        public WorkStackItem NewItem { get; set; }
    }
}
