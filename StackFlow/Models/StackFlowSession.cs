using System;
using System.Collections.Generic;

namespace StackFlow.Models
{
    [Serializable]
    public class StackFlowSession
    {
        public string Name { get; set; }
        public StackFlowSession()
        {
            Session = new List<WorkStack>();
            CompletedStacks = new List<WorkStack>();
            FloatingStack = new WorkStack("Default Floating Stack", "");
            ActiveStack = new WorkStack("Default Active Stack", "");
            PeriodsWhenActivated = new List<ActiveTimeSpan>();
        }
        /// <summary>
        /// Holds all the workstacks except the floating stack
        /// </summary>
        public List<WorkStack> Session { get; set; }
        /// <summary>
        /// holds the floating stack, which is kindof a global list of low prio items (items in this stack should never have high prio)
        /// Im not rly done refining what this is supposed to be and do yet
        /// </summary>
        public WorkStack FloatingStack { get; set; }
        /// <summary>
        /// Holds the currently active stack, i.e. the stack the user is currently working on
        /// </summary>
        public WorkStack ActiveStack { get; set; }
        /// <summary>
        /// holds items that were completed during the session
        /// </summary>
        public List<WorkStack> CompletedStacks { get; set; }
        /// <summary>
        /// Holds a bunch of structs which track 2 timestamps and a timespan. the timestamps should mark times at which this object was
        /// activated and de-activated. The timespan should hold the diff and is mainly there to prevent calculating that stuff a million times.
        /// </summary>
        public List<ActiveTimeSpan> PeriodsWhenActivated { get; set; }
    }
}
