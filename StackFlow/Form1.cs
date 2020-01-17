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

namespace StackFlow
{
    public partial class Form1 : Form
    {
        private readonly IController _controller;
        private int PutThisFormToForeGround;
        #region Events
        public event EventHandler UserClicksInterrupt;
        public event EventHandler UserModifiesActiveStack;
        public event EventHandler UserModifiesFloatingStack;
        public event EventHandler UserSavesSession;
        public event EventHandler UserLoadsSession;
        //window handle, key, keyId, modifier
        public event EventHandler<Tuple<IntPtr,int,int,int>> UserRequestsNewHotkey;
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
            UserRequestsNewHotkey?.Invoke(this, new Tuple<IntPtr, int, int, int>(
                this.Handle, (int)Keys.F, PutThisFormToForeGround, 6));
        }

        private void BindEventHandlers()
        {
            throw new NotImplementedException();
        }

        protected override void WndProc(ref Message m)
        {
            //additional blocks checking for other WParams associated to other hotkeys can handle that
            if (m.Msg == 0x0312 && m.WParam.ToInt32() == PutThisFormToForeGround)
            {
                // My hotkey has been typed

                // Do what you want here
                // ...
                Activate();
            }
            base.WndProc(ref m);
        }

        

    }
}
