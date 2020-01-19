using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.EventArgClasses
{
    public class SessionSaveOrLoadArgs : EventArgs
    {
        public string SessionName { get; set; }
        public string Folder { get; set; }
    }
}
