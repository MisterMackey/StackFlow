using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace StackFlow.Messaging.MessageTypes
{
    public class LoadSessionUserRequest : IUserRequest
    {
        public string UserName { get; set; }
        public SecureString Password { get; set; }
        public string SessionName { get; set; }
    }
}
