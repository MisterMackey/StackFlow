using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Models
{
    [Serializable]
    public struct ActiveTimeSpan
    {
        public DateTimeOffset ActivatedAbsoluteTime {get;set;}
        public DateTimeOffset? ClosedAbsoluteTime { get; set; }
        public TimeSpan? ActiveTime { get; set; }
        public static implicit operator TimeSpan?(ActiveTimeSpan a)
        {
            return a.ActiveTime;
        }
    }
}
