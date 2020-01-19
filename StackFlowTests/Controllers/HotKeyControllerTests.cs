using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackFlow.Controllers;
using StackFlow.EventArgClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace StackFlow.Controllers.Tests
{
    /// <summary>
    /// this subclass is used to test the controller, as it needs an IStackFlowForm to attach to
    /// </summary>
    partial class TestForm : Form, IStackFlowForm
    {
        public event EventHandler UserClicksInterrupt;
        public event EventHandler UserModifiesActiveStack;
        public event EventHandler UserModifiesFloatingStack;
        public event EventHandler UserSavesSession;
        public event EventHandler UserLoadsSession;
        public event EventHandler<HotKeyRegisterEventArgs> UserRequestsNewHotkey;
        public event EventHandler<HotKeyPressEventArgs> UserPressedHotkey;
        public void InvokeHotkeyRegister(HotKeyRegisterEventArgs args)
        {
            UserRequestsNewHotkey?.Invoke(this, args);
        }
        public void InvokeHotkeyPress(HotKeyPressEventArgs args)
        {
            UserPressedHotkey?.Invoke(this, args);
        }
    }
    [TestClass()]
    public class HotKeyControllerTests
    {
        

        [TestMethod()]
        public void InitializeTest()
        {
            var form = new TestForm();
            var controller = new HotKeyController();
            controller.Initialize(form);
        }
        [TestMethod()]
        public void RegisterAndInvokeForeground()
        {
            var form = new TestForm();
            var controller =  new HotKeyController();
            controller.Initialize(form);
            form.InvokeHotkeyRegister(new HotKeyRegisterEventArgs()
            {
                Action = HotKeyableActions.BringToForeground
                ,
                HotKeyId = 678
                ,
                WindowHandleToRegisterHotKeyTo = form.Handle
                ,
                KeyToRegister = Keys.F
                ,
                Modifier = 6
            });
            form.InvokeHotkeyPress(new HotKeyPressEventArgs()
            {
                HandleHotKeyIsRegisteredTo = form.Handle
                ,
                KeyId = 678
            });
            //did we crash yet? no? good than its probly working :D
        }
    }
}