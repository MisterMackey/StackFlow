using StackFlow.EventArgClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow
{
    public interface IStackFlowForm
    {
        public event EventHandler UserClicksInterrupt;
        public event EventHandler UserModifiesActiveStack;
        public event EventHandler UserModifiesFloatingStack;
        public event EventHandler UserSavesSession;
        public event EventHandler UserLoadsSession;
        //window handle, key, keyId, modifier
        public event EventHandler<HotKeyRegisterEventArgs> UserRequestsNewHotkey;
        public event EventHandler<HotKeyPressEventArgs> UserPressedHotkey;
    }
}
