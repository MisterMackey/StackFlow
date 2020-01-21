using StackFlow;
using StackFlow.Models;
using System;
using System.Windows.Forms;

namespace StackFlowTests
{
    /// <summary>
    /// this subclass is used to test the controller, as it needs an IStackFlowForm to attach to
    /// </summary>
    partial class TestForm : Form
    {
        internal StackFlowSession ActiveSession { get; set; }

        public StackFlowSession GetActiveSession()
        {
            return ActiveSession;
        }

        public void SetActiveSession(StackFlowSession Session)
        {
            //probably trigger save event here?
            ActiveSession = Session;
        }

    }

}
