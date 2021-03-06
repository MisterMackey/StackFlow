﻿using StackFlow.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StackFlow.Statistics
{
    /// <summary>
    /// Contains collections of the various object models used within stackflow. Unlike a typical session, which just has pointers everywhere, this class
    /// will first transform all objects into suitable <see cref="ModelToStructExtensions"/>and load them into Lists, thus achieving data locality and speeding up subsequent 
    /// requests to the data. Reporting features ustilizing loops over these collections should enjoy a significant speedup after initialization.
    /// </summary>
    public abstract class StackFlowDataCollections
    {
       public static IStackFlowDataCollections CreateStackFlowDataCollection(IEnumerable<StackFlowSession> Source)
        {
            return new SingleThreadInitializeStackFlowDataCollection(Source);
        }
    }
}
