using StackFlow.Messaging.MessageTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackFlow.Messaging
{
    public class LoadSessionMessage : IMessage<LoadSessionUserRequest>
    {
        public LoadSessionUserRequest PayLoad { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public object Sender { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime TimeOfOrigin { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
