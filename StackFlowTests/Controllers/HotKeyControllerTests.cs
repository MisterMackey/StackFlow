using Microsoft.VisualStudio.TestTools.UnitTesting;
using StackFlow.EventArgClasses;
using StackFlowTests;
using System.Windows.Forms;

namespace StackFlow.Controllers.Tests
{
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
            var controller = new HotKeyController();
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