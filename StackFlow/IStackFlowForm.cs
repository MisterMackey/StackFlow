using StackFlow.EventArgClasses;
using StackFlow.Models;
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
        public event EventHandler<SessionSaveOrLoadArgs> UserSavesSession;
        public event EventHandler<SessionSaveOrLoadArgs> UserLoadsSession;
        //window handle, key, keyId, modifier
        public event EventHandler<HotKeyRegisterEventArgs> UserRequestsNewHotkey;
        public event EventHandler<HotKeyPressEventArgs> UserPressedHotkey;
        /// <summary>
        /// Gets the active session attached to the IStackFlowForm
        /// </summary>
        /// <returns></returns>
        StackFlowSession GetActiveSession();
        /// <summary>
        /// sets the Active session attached to the IStackFlowForm
        /// </summary>
        /// <param name=""></param>
        void SetActiveSession(StackFlowSession Session);
    }
}
