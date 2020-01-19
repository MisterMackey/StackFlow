using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace StackFlow.EventArgClasses
{
    public enum HotKeyableActions
    {
        BringToForeground
    }
    public class HotKeyRegisterEventArgs : EventArgs
    {
        public Keys KeyToRegister { get; set; }
        public int HotKeyId { get; set; }
        public int Modifier { get; set; }
        public IntPtr WindowHandleToRegisterHotKeyTo { get; set; }
        public HotKeyableActions Action { get; set; }
    }
}
