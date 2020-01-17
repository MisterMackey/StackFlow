using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Controllers
{
    public interface IController
    {
        /// <summary>
        /// Instructs the controller to perform steps necessary prior to functioning such as registering to event handlers or loading config or whatever
        /// </summary>
        void Initialize(Form1 form);
    }
}
