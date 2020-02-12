using StackFlow.Models;
using StackFlow.Statistics.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StackFlow.Statistics
{
    internal class SingleThreadInitializeStackFlowDataCollection : IStackFlowDataCollections
    {
        private IEnumerable<StackFlowSession> SourceData;
        private bool IsInitialized;
        private bool IsInitializing;
        private Session[] _Sessions;
        private Stack[] _Stacks;
        private Item[] _Items;
        private ActiveTime[] _Times;

        public IEnumerable<Session> Sessions => TryGetSessions();

        public IEnumerable<Stack> Stacks => TryGetStacks();

        public IEnumerable<Item> Items => TryGetItems();

        public IEnumerable<ActiveTime> Times => TryGetActiveTimes();

        public SingleThreadInitializeStackFlowDataCollection(IEnumerable<StackFlowSession> Source)
        {
            SourceData = Source;
            IsInitialized = false;
            IsInitializing = false;
        }


        public void Initialize()
        {
            //meh, should be pretty safe to race conds unless i try to deliberately f it up.
            if (IsInitializing) { throw new InvalidOperationException("Collection are already initializing, please wait for operation to finish"); }
            IsInitializing = true;
            IsInitialized = false;
            int sescnt = SourceData.Count();
            //numbers are approximate guesstimates, can be finetuned later maybe
            List<Session> tempSeshs = new List<Session>(sescnt);
            List<Stack> tempStacks = new List<Stack>(sescnt * 100);
            List<Item> tempItems = new List<Item>(sescnt * 1000);
            List<ActiveTime> tempTiems = new List<ActiveTime>(sescnt * 4000);
            int i=0 , j =0 , k =0, l = 0; //some iterators ima need
            foreach (var source in SourceData)
            {
                tempSeshs.Add(source.TransformToSessionStruct(i));
                //handle stacks
                foreach (var innerSource in source.Session)
                {
                    tempStacks.Add(innerSource.TransformToStackStruct(i, j));
                    //handle items in stack
                    foreach (var innerInnerSource in )
                    j++;
                }

                i++;
            }
        }

        #region Getters
        private IEnumerable<Session> TryGetSessions()
        {
            if (!IsInitialized) { throw new InvalidOperationException("DataCollection is not yet initialized, please wait until initialization is complete"); }
            return _Sessions;
        }
        private IEnumerable<Stack> TryGetStacks()
        {
            if (!IsInitialized) { throw new InvalidOperationException("DataCollection is not yet initialized, please wait until initialization is complete"); }
            return _Stacks;
        }
        private IEnumerable<Item> TryGetItems()
        {
            if (!IsInitialized) { throw new InvalidOperationException("DataCollection is not yet initialized, please wait until initialization is complete"); }
            return _Items;
        }
        private IEnumerable<ActiveTime> TryGetActiveTimes()
        {
            if (!IsInitialized) { throw new InvalidOperationException("DataCollection is not yet initialized, please wait until initialization is complete"); }
            return _Times;
        }
        #endregion
    }
}
