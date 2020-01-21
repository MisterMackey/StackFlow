using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Messaging
{
    public interface IMessage<T>
    {
        public T PayLoad { get; set; }
        public object Sender { get; set; }
        public DateTime TimeOfOrigin { get; set; }
    }
}
