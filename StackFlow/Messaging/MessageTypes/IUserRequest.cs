using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace StackFlow.Messaging.MessageTypes
{
    public interface IUserRequest
    {
        string UserName { get; set; }
        SecureString Password { get; set; }
    }
}
