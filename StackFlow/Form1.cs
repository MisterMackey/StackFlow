
using StackFlow.Models;
using System;
using System.Windows.Forms;

namespace StackFlow
{
    public partial class Form1 : Form
    {
        private readonly int PutThisFormToForeGround;
        internal StackFlowSession ActiveSession { get; set; }
        #region Events

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
        public Form1()
        {
            InitializeComponent();
        }

        private void BindEventHandlers()
        {

        }

    }
}
