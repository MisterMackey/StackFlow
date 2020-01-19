using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace StackFlow.EventArgClasses
{
    public class HotKeyPressEventArgs : EventArgs
    {
        public int KeyId { get; set; }
        public IntPtr HandleHotKeyIsRegisteredTo { get; set; }
    }
}
