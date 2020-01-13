using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace StackFlow.Models
{
    public class StackFlowSession
    {
        public List<WorkStack> Session { get; set; }

    }
}
