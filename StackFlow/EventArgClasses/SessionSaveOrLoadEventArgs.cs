using System;

namespace StackFlow.EventArgClasses
{
    public class SessionSaveOrLoadEventArgs : EventArgs
    {
        public string SessionName { get; set; }
        public string Folder { get; set; }
    }
}
