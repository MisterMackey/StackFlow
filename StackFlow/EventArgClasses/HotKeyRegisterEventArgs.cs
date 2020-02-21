using System;
using System.Windows.Forms;

namespace StackFlow.EventArgClasses
{
    public enum HotKeyableActions
    {
        BringToForeground
            ,Save
            ,Load
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
