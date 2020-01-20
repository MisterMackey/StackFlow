using StackFlow.EventArgClasses;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace StackFlow.Controllers
{
    public class HotKeyController : IController
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        private Dictionary<int, HotKeyableActions> HotkeyIdToHotKeyableActionMapping;
        private Dictionary<HotKeyableActions, Action> HotkeyableActionToActionMapping;
        //sadly i have not found how to do this outside of the form itself
        public void Initialize(IStackFlowForm form)
        {
            form.UserRequestsNewHotkey += OnKeyRegister;
            form.UserPressedHotkey += OnHotkeyPress;
            HotkeyIdToHotKeyableActionMapping = new Dictionary<int, HotKeyableActions>();
            HotkeyableActionToActionMapping = new Dictionary<HotKeyableActions, Action>();
            MapActions();
        }

        private void OnKeyRegister(object sender, HotKeyRegisterEventArgs e)
        {
            RegisterKey(e.WindowHandleToRegisterHotKeyTo, (int)e.KeyToRegister, e.HotKeyId, e.Modifier, e.Action);
            HotkeyIdToHotKeyableActionMapping.Add(e.HotKeyId, e.Action);
        }

        private void RegisterKey(IntPtr wHandle, int key, int hotkeyId, int modifier, HotKeyableActions action)
        {
            RegisterHotKey(wHandle, hotkeyId, modifier, key);
        }
        private void OnHotkeyPress(object sender, HotKeyPressEventArgs e)
        {
            var hotkey = HotkeyIdToHotKeyableActionMapping[e.KeyId];
            //havent quite got this to work yet cuz i need methods that belong to the form itself...
            //var action = HotkeyableActionToActionMapping[hotkey];            
            //((Form)sender).Invoke(action);
            switch (hotkey)
            {
                case HotKeyableActions.BringToForeground:
                    ((Form)sender).Activate();
                    break;
            }
        }
        private void MapActions()
        {

        }
    }
}
