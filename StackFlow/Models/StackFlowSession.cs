﻿using System;
using System.Collections.Generic;

namespace StackFlow.Models
{
    [Serializable]
    public class StackFlowSession
    {
        public StackFlowSession()
        {
            Session = new List<WorkStack>();
            CompletedItems = new List<WorkStackItem>();

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
        public List<WorkStackItem> CompletedItems { get; set; }
    }
}
