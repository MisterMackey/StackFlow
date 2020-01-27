using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.EventArgClasses
{
    public class WorkInterruptionEventArgs : EventArgs
    {
        public string NameOfNewStack { get; set; }
        public string DescriptionOfNewStack { get; set; }
    }
}
