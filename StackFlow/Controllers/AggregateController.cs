using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Controllers
{
    /// <summary>
    /// AggregateController has a bunch of controllers
    /// </summary>
    public class AggregateController : IController
    {
        private readonly IController[] _BabyControllers;
        public void Initialize(IStackFlowForm form)
        {
            for (int i = 0; i < _BabyControllers.Length; i++)
            {
                _BabyControllers[i].Initialize(form);
            }
        }
        public AggregateController() : this(new IController[]
        {
            //add new controllers here for default set
            new HotKeyController()
        })
        {

        }
        public AggregateController(IController[] children)
        {
            _BabyControllers = children;
        }
    }
}
