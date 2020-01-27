using StackFlow.EventArgClasses;
using StackFlow.Models;
using System;
using System.Windows.Forms;

namespace StackFlow
{
    public interface IStackFlowForm
    {
        public event EventHandler<WorkInterruptionEventArgs> UserClicksInterrupt;
        public event EventHandler<ActiveStackModificationEventArgs> UserModifiesActiveStack;
        public event EventHandler<FloatingStackModificationEventArgs> UserModifiesFloatingStack;
        public event EventHandler<SessionSaveOrLoadEventArgs> UserSavesSession;
        public event EventHandler<SessionSaveOrLoadEventArgs> UserLoadsSession;
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
        public GroupBox ActiveStack { get; }
    }
}
