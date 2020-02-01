using System;
using System.Collections.Generic;

namespace StackFlow.Models
{
    [Serializable]
    public class WorkStackItem
    {
        #region Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Notes { get; private set; }
        public WorkStackItemPriority Priority { get; set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? ClosedDate { get; set; }
        //todo: property that links to another stack in true Thread.Join() fashion
        //todo: parent container property (for the above)

        /// <summary>
        /// Holds a bunch of structs which track 2 timestamps and a timespan. the timestamps should mark times at which this object was
        /// activated and de-activated. The timespan should hold the diff and is mainly there to prevent calculating that stuff a million times.
        /// </summary>
        public List<ActiveTimeSpan> PeriodsWhenActivated { get; set; }
        #endregion

        #region Constructors
        //used for new objects
        public WorkStackItem(string Name) : this(Name, "")
        {
        }
        public WorkStackItem(string Name, string Description)
        {
            this.Name = Name;
            this.Description = Description;
            this.Notes = new List<string>();
            this.Priority = WorkStackItemPriority.Whenever;
            CreatedDate = DateTime.Now;
            PeriodsWhenActivated = new List<ActiveTimeSpan>();
        }
        public WorkStackItem(string Name, WorkStackItemPriority Priority) : this(Name, "")
        {
            this.Priority = Priority;
        }
        public WorkStackItem(string Name, string Description, WorkStackItemPriority Priority) : this(Name, Description)
        {
            this.Priority = Priority;
        }

        //used for cloning
        public WorkStackItem(string Name, string Description, IList<string> Notes, WorkStackItemPriority Priority, DateTime CreatedDate, DateTime? ClosedDate, List<ActiveTimeSpan> PeriodsWhenActivated)
        {
            this.Name = Name;
            this.Description = Description;
            this.Notes = new List<string>();
            this.Priority = Priority;
            this.Notes.AddRange(Notes);
            this.CreatedDate = CreatedDate;
            this.ClosedDate = ClosedDate;
            this.PeriodsWhenActivated = PeriodsWhenActivated;
        }
        #endregion
    }
}
