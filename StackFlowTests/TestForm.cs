using StackFlow;
using StackFlow.EventArgClasses;
using StackFlow.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace StackFlowTests
{
    /// <summary>
    /// this subclass is used to test the controller, as it needs an IStackFlowForm to attach to
    /// </summary>
    partial class TestForm : Form, IStackFlowForm
    {
        internal StackFlowSession ActiveSession { get; set; }
        public event EventHandler UserClicksInterrupt;
        public event EventHandler<ActiveStackModificationEventArgs> UserModifiesActiveStack;
        public event EventHandler<FloatingStackModificationEventArgs> UserModifiesFloatingStack;
        public event EventHandler<SessionSaveOrLoadEventArgs> UserSavesSession;
        public event EventHandler<SessionSaveOrLoadEventArgs> UserLoadsSession;
        public event EventHandler<HotKeyRegisterEventArgs> UserRequestsNewHotkey;
        public event EventHandler<HotKeyPressEventArgs> UserPressedHotkey;
        public StackFlowSession GetActiveSession()
        {
            return ActiveSession;
        }

        public void SetActiveSession(StackFlowSession Session)
        {
            //probably trigger save event here?
            ActiveSession = Session;
        }
        public void InvokeHotkeyRegister(HotKeyRegisterEventArgs args)
        {
            UserRequestsNewHotkey?.Invoke(this, args);
        }
        public void InvokeHotkeyPress(HotKeyPressEventArgs args)
        {
            UserPressedHotkey?.Invoke(this, args);
        }
        public void InvokeSaveSession(SessionSaveOrLoadEventArgs args)
        {
            UserSavesSession?.Invoke(this, args);
        }
        public void InvokeLoadSession(SessionSaveOrLoadEventArgs args)
        {
            UserLoadsSession?.Invoke(this, args);
        }
        public void InvokeModifyActiveStack(ActiveStackModificationEventArgs args)
        {
            UserModifiesActiveStack?.Invoke(this, args);
        }
        public void InvokeModifyFloatingStack(FloatingStackModificationEventArgs args)
        {
            UserModifiesFloatingStack?.Invoke(this, args);
        }
    }

}
