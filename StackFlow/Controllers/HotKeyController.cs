using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace StackFlow.Controllers
{
    public class HotKeyController : IController
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private const int StackFlowHotkeyId = 4826;
        //sadly i have not found how to do this outside of the form itself
        public void Initialize(Form1 form)
        {
            form.UserRequestsNewHotkey += OnKeyRegister;
        }

        private void OnKeyRegister(object sender, Tuple<IntPtr, int, int, int> e)
        {
            RegisterKey(e.Item1, e.Item2, e.Item3, e.Item4);
        }

        private void RegisterKey(IntPtr wHandle, int key, int hotkeyId, int modifier)
        {
            RegisterHotKey(wHandle, hotkeyId, modifier, key);
        }
    }
}
