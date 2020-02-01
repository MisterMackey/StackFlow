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
        public static implicit operator ActiveTimeSpan(DateTimeOffset startTime)
        {
            ActiveTimeSpan span = new ActiveTimeSpan();
            span.ActivatedAbsoluteTime = startTime;
            return span;
        }
        public static bool operator==(ActiveTimeSpan a, ActiveTimeSpan b)
        {
            if (a == null || b == null)
            {
                return false;
            }
            else
            {
                return (a.ActiveTime == b.ActiveTime);
            }
        }
        public static bool operator !=(ActiveTimeSpan a, ActiveTimeSpan b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            if (obj is ActiveTimeSpan)
            {
                ActiveTimeSpan b = (ActiveTimeSpan)obj;
                return (this == b);
            }
            else return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
