using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace StackFlow.Models
{
    [Serializable]
    public class WorkStack : IEnumerable<WorkStackItem>
    {
        private readonly Stack<WorkStackItem> m_Stack;
        public string Name { get; set; }
        public string Description { get; set; }
        public WorkStackItemPriority Priority
        {            
            get 
            { 
                if (m_Stack.Any()) { return m_Stack.Max(x => x.Priority); }
                else if (CompletedItems.Any()) { return CompletedItems.Max(x => x.Priority); }
                else { return WorkStackItemPriority.Whenever; }
            }
        }
        public List<WorkStackItem> CompletedItems { get; }
        /// <summary>
        /// Holds a bunch of structs which track 2 timestamps and a timespan. the timestamps should mark times at which this object was
        /// activated and de-activated. The timespan should hold the diff and is mainly there to prevent calculating that stuff a million times.
        /// </summary>
        public List<ActiveTimeSpan> PeriodsWhenActivated { get; set; }
        public bool IsActive { get; set; }
        public DateTimeOffset Opened { get; }
        public DateTimeOffset? Closed { get; set; }
        #region Interface

        public IEnumerator<WorkStackItem> GetEnumerator()
        {
            return ((IEnumerable<WorkStackItem>)m_Stack).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<WorkStackItem>)m_Stack).GetEnumerator();
        }

        //
        // Summary:
        //     Gets the number of elements contained in the System.Collections.Generic.Stack`1.
        //
        // Returns:
        //     The number of elements contained in the System.Collections.Generic.Stack`1.
        public int Count { get { return m_Stack.Count; } }

        //
        // Summary:
        //     Removes all objects from the System.Collections.Generic.Stack`1.
        public void Clear()
        {
            m_Stack.Clear();
        }
        //
        // Summary:
        //     Determines whether an element is in the System.Collections.Generic.Stack`1.
        //
        // Parameters:
        //   item:
        //     The object to locate in the System.Collections.Generic.Stack`1. The value can
        //     be null for reference types.
        //
        // Returns:
        //     true if item is found in the System.Collections.Generic.Stack`1; otherwise, false.
        public bool Contains(WorkStackItem item)
        {
            return m_Stack.Contains(item);
        }
        //
        // Summary:
        //     Copies the System.Collections.Generic.Stack`1 to an existing one-dimensional
        //     System.Array, starting at the specified array index.
        //
        // Parameters:
        //   array:
        //     The one-dimensional System.Array that is the destination of the elements copied
        //     from System.Collections.Generic.Stack`1. The System.Array must have zero-based
        //     indexing.
        //
        //   arrayIndex:
        //     The zero-based index in array at which copying begins.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     array is null.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     arrayIndex is less than zero.
        //
        //   T:System.ArgumentException:
        //     The number of elements in the source System.Collections.Generic.Stack`1 is greater
        //     than the available space from arrayIndex to the end of the destination array.
        public void CopyTo(WorkStackItem[] array, int arrayIndex)
        {
            m_Stack.CopyTo(array, arrayIndex);
        }

        //
        // Summary:
        //     Returns the object at the top of the System.Collections.Generic.Stack`1 without
        //     removing it.
        //
        // Returns:
        //     The object at the top of the System.Collections.Generic.Stack`1.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     The System.Collections.Generic.Stack`1 is empty.
        public WorkStackItem Peek()
        {
            return m_Stack.Peek();
        }
        //
        // Summary:
        //     Removes and returns the object at the top of the System.Collections.Generic.Stack`1.
        //
        // Returns:
        //     The object removed from the top of the System.Collections.Generic.Stack`1.
        //
        // Exceptions:
        //   T:System.InvalidOperationException:
        //     The System.Collections.Generic.Stack`1 is empty.
        public WorkStackItem Pop()
        {
            return m_Stack.Pop();
        }
        //
        // Summary:
        //     Inserts an object at the top of the System.Collections.Generic.Stack`1.
        //
        // Parameters:
        //   item:
        //     The object to push onto the System.Collections.Generic.Stack`1. The value can
        //     be null for reference types.
        public void Push(WorkStackItem item)
        {
            m_Stack.Push(item);
        }
        //
        // Summary:
        //     Copies the System.Collections.Generic.Stack`1 to a new array.
        //
        // Returns:
        //     A new array containing copies of the elements of the System.Collections.Generic.Stack`1.
        public WorkStackItem[] ToArray()
        {
            return m_Stack.ToArray();
        }
        //
        // Summary:
        //     Sets the capacity to the actual number of elements in the System.Collections.Generic.Stack`1,
        //     if that number is less than 90 percent of current capacity.
        public void TrimExcess()
        {
            m_Stack.TrimExcess();
        }
        //
        // Parameters:
        //   result:
        public bool TryPeek([MaybeNullWhen(false)] out WorkStackItem result)
        {
            return m_Stack.TryPeek(out result);
        }
        //
        // Parameters:
        //   result:
        public bool TryPop([MaybeNullWhen(false)] out WorkStackItem result)
        {
            return m_Stack.TryPop(out result);
        }



        #endregion

        #region constructors
        public WorkStack(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
            m_Stack = new Stack<WorkStackItem>();
            CompletedItems = new List<WorkStackItem>();
            PeriodsWhenActivated = new List<ActiveTimeSpan>();
            IsActive = false;
            Opened = DateTimeOffset.Now;
        }
        #endregion

        #region Public Methods

        #endregion
    }
}
