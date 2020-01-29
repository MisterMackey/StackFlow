using StackFlow.EventArgClasses;
using StackFlow.Procedures;
using System;

namespace StackFlow.Controllers
{
    public class SessionController : IController
    {
        private IStackFlowForm FormReference;
        public void Initialize(IStackFlowForm form)
        {
            FormReference = form;
            form.UserModifiesActiveStack += OnActiveStackModification;
            form.UserModifiesFloatingStack += OnFloatingStackModification;
            form.UserClicksInterrupt += OnUserInterrupt;
            form.UserRequestsStackSwitch += OnUserStackSwitch;
        }

        private void OnUserStackSwitch(object sender, StackSwitchEventArgs e)
        {
            SessionProcedures.SwitchActiveStack(FormReference.GetActiveSession(), e.Stack);
            FormReference.UpdateSessionFull();
        }

        private void OnUserInterrupt(object sender, WorkInterruptionEventArgs e)
        {
            var sesh = FormReference.GetActiveSession();
            SessionProcedures.AddNewWorkStack(sesh, e.NameOfNewStack, e.DescriptionOfNewStack, true);
            FormReference.UpdateSessionFull();
        }

        private void OnFloatingStackModification(object sender, FloatingStackModificationEventArgs e)
        {
            //to prevent duplicate code
            ActiveStackModificationEventArgs args = new ActiveStackModificationEventArgs() { NewItem = e.NewItem, TypeOfChange = e.TypeOfChange };
            OnStackModification(args, true);
        }

        private void OnActiveStackModification(object sender, ActiveStackModificationEventArgs e)
        {
            OnStackModification(e, false);
        }

        private void OnStackModification(ActiveStackModificationEventArgs e, bool IsModifyingFloatingStack)
        {
            var stackToModify = IsModifyingFloatingStack ? //modding floatingstack?
                FormReference.GetActiveSession().FloatingStack //yes
                : FormReference.GetActiveSession().ActiveStack; //no (= modding active stack)
            switch (e.TypeOfChange)
            {
                case ActiveStackModificationTypes.ItemAdded:
                    WorkStackProcedures.AddNewTask(e.NewItem, FormReference.GetActiveSession().ActiveStack);
                    break;
                case ActiveStackModificationTypes.ItemCompleted:
                    try
                    {
                        WorkStackProcedures.CompleteTopItem(stackToModify);
                    }
                    catch (InvalidOperationException)
                    {
                        //stack is empty
                        SessionProcedures.CompleteStack(FormReference.GetActiveSession(), true);
                    }
                    break;
                case ActiveStackModificationTypes.ItemModified:
                    WorkStackProcedures.ModifyTop(e.NewItem, FormReference.GetActiveSession().ActiveStack);
                    break;
                case ActiveStackModificationTypes.ItemInserted:
                    throw new NotImplementedException("changed items not on top of stcak not supported");
                    break;
                case ActiveStackModificationTypes.ItemRemoved:
                    throw new NotImplementedException("changed items not on top of stcak not supported");
                    break;
                case ActiveStackModificationTypes.ItemChanged:
                    throw new NotImplementedException("changed items not on top of stcak not supported");
                    break;
                case ActiveStackModificationTypes.StackCompleted:
                    SessionProcedures.CompleteStack(FormReference.GetActiveSession(), true);
                    break;
            }
            FormReference.UpdateSessionFull();
        }
    }
}
