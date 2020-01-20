namespace StackFlow.Controllers
{
    public interface IController
    {
        /// <summary>
        /// Instructs the controller to perform steps necessary prior to functioning such as registering to event handlers or loading config or whatever
        /// </summary>
        void Initialize(IStackFlowForm form);
    }
}
