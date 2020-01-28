using StackFlow.EventArgClasses;

namespace StackFlow.Controllers
{
    public class SessionSaveLoadController : IController
    {
        private IStackFlowForm FormReference;
        public void Initialize(IStackFlowForm form)
        {
            FormReference = form;
            form.UserLoadsSession += OnSessionLoadFromDisk;
            form.UserSavesSession += OnSessionSaveToDisk;
        }

        private void OnSessionLoadFromDisk(object sender, SessionSaveOrLoadEventArgs e)
        {
            string fullPath = $"{e.Folder}\\{e.SessionName}";
            var sesh = Procedures.SessionProcedures.LoadSession(fullPath);
            FormReference.SetActiveSession(sesh);
            FormReference.UpdateSessionFull();
        }

        private void OnSessionSaveToDisk(object sender, SessionSaveOrLoadEventArgs e)
        {
            string fullPath = $"{e.Folder}\\{e.SessionName}";
            var sesh = FormReference.GetActiveSession();
            sesh.Name = e.SessionName;
            Procedures.SessionProcedures.SaveSession(fullPath, sesh);
        }
    }
}
