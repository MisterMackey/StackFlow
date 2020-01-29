using StackFlow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.EventArgClasses
{
    public class StackSwitchEventArgs : EventArgs
    {
        public WorkStack Stack { get; set; }
    }
}
