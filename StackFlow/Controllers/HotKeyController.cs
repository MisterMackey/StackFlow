using StackFlow.EventArgClasses;
using StackFlow.Procedures;
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
            HotkeyIdToHotKeyableActionMapping.Add(TransformToKey(e.HotKeyId,e.Modifier), e.Action);
        }

        private void RegisterKey(IntPtr wHandle, int key, int hotkeyId, int modifier, HotKeyableActions action)
        {
            if (action == HotKeyableActions.BringToForeground)// requires global register
            {
                RegisterHotKey(wHandle, hotkeyId, modifier, key); 
            }
            //no need to global register local keys
        }


        private int TransformToKey(int Key, int Modifier)
        {
            return ((Key + Modifier) * Key + (Key - Modifier) * Modifier); //or whatever
        }
        private void OnHotkeyPress(object sender, HotKeyPressEventArgs e)
        {
            var hotkey = HotkeyIdToHotKeyableActionMapping[TransformToKey(e.KeyId, e.Modifier)];
            //havent quite got this to work yet cuz i need methods that belong to the form itself...
            //var action = HotkeyableActionToActionMapping[hotkey];            
            //((Form)sender).Invoke(action);
            switch (hotkey)
            {
                case HotKeyableActions.BringToForeground:
                    ((Form)sender).Activate();
                    break;
                case HotKeyableActions.Save:
                    SessionProcedures.SaveSession(e.Message, ((IStackFlowForm)sender).GetActiveSession());
                    break;
            }
        }
        private void MapActions()
        {

        }
    }
}
