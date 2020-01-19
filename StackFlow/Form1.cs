using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using StackFlow.Controllers;
using StackFlow.EventArgClasses;
using StackFlow.Models;

namespace StackFlow
{
    public partial class Form1 : Form, IStackFlowForm
    {
        private readonly IController _controller;
        private int PutThisFormToForeGround;
        internal StackFlowSession ActiveSession { get; set; }
        #region Events
        public event EventHandler UserClicksInterrupt;
        public event EventHandler UserModifiesActiveStack;
        public event EventHandler UserModifiesFloatingStack;
        public event EventHandler<SessionSaveOrLoadArgs> UserSavesSession;
        public event EventHandler<SessionSaveOrLoadArgs> UserLoadsSession;
        //window handle, key, keyId, modifier
        public event EventHandler<HotKeyRegisterEventArgs> UserRequestsNewHotkey;
        public event EventHandler<HotKeyPressEventArgs> UserPressedHotkey;
        #endregion

        #region public
        public StackFlowSession GetActiveSession()
        {
            return ActiveSession;
        }

        public void SetActiveSession(StackFlowSession Session)
        {
            //probably trigger save event here?
            ActiveSession = Session;
        }
        #endregion
        public Form1(IController controller)
        {
            InitializeComponent();
            _controller = controller;
            _controller.Initialize(this);
            BindEventHandlers();
            // Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8
            // Compute the addition of each combination of the keys you want to be pressed
            // ALT+CTRL = 1 + 2 = 3 , CTRL+SHIFT = 2 + 4 = 6...
            PutThisFormToForeGround = 453132;
            //following register ctrl shift f as hotkey to maximize window
            UserRequestsNewHotkey?.Invoke(this, new HotKeyRegisterEventArgs() { 
            HotKeyId = PutThisFormToForeGround
            ,KeyToRegister = Keys.F
            ,WindowHandleToRegisterHotKeyTo = this.Handle
            ,Modifier = 6
            ,Action = HotKeyableActions.BringToForeground});
        }

        private void BindEventHandlers()
        {
            
        }

        protected override void WndProc(ref Message m)
        {
            //invoke event and let the controller deal with it
            if (m.Msg == 0x0312)
            {
                UserPressedHotkey?.Invoke(this, new HotKeyPressEventArgs()
                {
                    HandleHotKeyIsRegisteredTo = this.Handle
                    ,
                    KeyId = m.WParam.ToInt32()
                });
            }
            base.WndProc(ref m);
        }


    }
}
