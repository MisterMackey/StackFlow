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
        #region private fields
        private IEnumerable<StackFlowSession> SourceData;
        private bool IsInitialized;
        private bool IsInitializing;
        private Session[] _Sessions;
        private Stack[] _Stacks;
        private Item[] _Items;
        private ActiveTime[] _Times;
        #endregion

        #region Public
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

            //start initializing
            //this function calls other functions that will init the rest of the collections
            CreateSessions(tempSeshs, tempStacks, tempItems, tempTiems);
            //resize and commit to collections
            _Sessions = tempSeshs.ToArray();
            _Stacks = tempStacks.ToArray();
            _Items = tempItems.ToArray();
            _Times = tempTiems.ToArray();
            IsInitialized = true;
            IsInitializing = false;
        }
        #endregion

        #region Initializing methods
        private void CreateSessions(List<Session> tempSeshs, List<Stack> tempStacks, List<Item> tempItems, List<ActiveTime> tempTiems)
        {
            int i = 0, j = 0, k = 0, l = 0; //some iterators ima need
            foreach (var source in SourceData)
            {
                tempSeshs.Add(source.TransformToSessionStruct(i));
                //handle stacks in session
                CreateStacks(tempStacks, tempItems, tempTiems, i, ref j, ref k, ref l, source.Session);
                //handle completed stacks
                CreateStacks(tempStacks, tempItems, tempTiems, i, ref j, ref k, ref l, source.CompletedStacks);
                i++;
            }
        }

        private static void CreateStacks(List<Stack> tempStacks, List<Item> tempItems, List<ActiveTime> tempTiems, int i, ref int j, ref int k, ref int l, IEnumerable<WorkStack> source)
        {
            foreach (WorkStack innerSource in source)
            {
                tempStacks.Add(innerSource.TransformToStackStruct(i, j));
                //handle items in stack
                CreateItems(tempItems, tempTiems, i, j, ref k, ref l, innerSource);
                //handle completed items in hte stack
                CreateItems(tempItems, tempTiems, i, j, ref k, ref l, innerSource.CompletedItems);
                //handle activetimespans in stack, these will get -1 as their itemId
                CreateActiveTimes(tempTiems, i, j, -1, ref l, innerSource.PeriodsWhenActivated);
                j++;
            }
        }



        private static void CreateItems(List<Item> tempItems, List<ActiveTime> tempTiems, int i, int j, ref int k, ref int l, IEnumerable<WorkStackItem> source)
        {
            foreach (WorkStackItem innerSource in source)
            {
                tempItems.Add(innerSource.TransformToItemStruct(i, j, k));
                //handle activetimes in item
                CreateActiveTimes(tempTiems, i, j, k, ref l, innerSource.PeriodsWhenActivated);
                k++;
            }
        }
        private static void CreateActiveTimes(List<ActiveTime> tempTiems, int i, int j, int k, ref int l, IEnumerable<ActiveTimeSpan> source)
        {
            foreach (ActiveTimeSpan innerInnerSource in source)
            {
                tempTiems.Add(innerInnerSource.TransformToActiveTime(i, j, -1, l));
                l++;
            }
        }

        #endregion

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
