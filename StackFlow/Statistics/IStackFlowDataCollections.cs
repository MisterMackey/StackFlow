using StackFlow.Statistics.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Statistics
{
    public interface IStackFlowDataCollections
    {
        public void Initialize();
        public IEnumerable<Session> Sessions { get; }
        public IEnumerable<Stack> Stacks { get; }
        public IEnumerable<Item> Items { get; }
        public IEnumerable<ActiveTime> Times { get; }
    }
}
