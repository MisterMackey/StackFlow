using StackFlow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.EventArgClasses
{
    public class FloatingStackModificationEventArgs : EventArgs
    {
        public ActiveStackModificationTypes TypeOfChange { get; set; }
        public WorkStackItem NewItem { get; set; }
    }
}
