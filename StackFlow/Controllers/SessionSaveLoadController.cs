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

        private void OnSessionLoadFromDisk(object sender, SessionSaveOrLoadArgs e)
        {
            string fullPath = $"{e.Folder}\\{e.SessionName}";
            var sesh = Procedures.SessionProcedures.LoadSession(fullPath);
            FormReference.SetActiveSession(sesh);
        }

        private void OnSessionSaveToDisk(object sender, SessionSaveOrLoadArgs e)
        {
            string fullPath = $"{e.Folder}\\{e.SessionName}";
            var sesh = FormReference.GetActiveSession();
            Procedures.SessionProcedures.SaveSession(fullPath, sesh);
        }
    }
}
