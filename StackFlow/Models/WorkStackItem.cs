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
        public WorkStackItem(string Name, string Description, IList<string> Notes, WorkStackItemPriority Priority, DateTime CreatedDate, DateTime? ClosedDate)
        {
            this.Name = Name;
            this.Description = Description;
            this.Notes = new List<string>();
            this.Priority = Priority;
            this.Notes.AddRange(Notes);
            this.CreatedDate = CreatedDate;
            this.ClosedDate = ClosedDate;
        }
        #endregion
    }
}
