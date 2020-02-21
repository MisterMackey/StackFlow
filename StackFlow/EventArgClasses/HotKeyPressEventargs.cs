using System;

namespace StackFlow.EventArgClasses
{
    public class HotKeyPressEventArgs : EventArgs
    {
        public int KeyId { get; set; }
        public int Modifier { get; set; }
        public IntPtr HandleHotKeyIsRegisteredTo { get; set; }
        public string Message { get; set; }
    }
}
