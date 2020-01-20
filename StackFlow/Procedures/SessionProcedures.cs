using StackFlow.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StackFlow.Procedures
{
    public static class SessionProcedures
    {
        public static void SaveSession(string filepath, StackFlowSession session)
        {
            Stream stream;
            if (File.Exists(filepath))
            {
                stream = File.OpenWrite(filepath);
            }
            else
            {
                stream = File.Create(filepath);
            }
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, session);
            stream.Close();
        }
        public static StackFlowSession LoadSession(string filepath)
        {
            Stream stream;
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException("File not found");
            }
            stream = File.OpenRead(filepath);
            BinaryFormatter formatter = new BinaryFormatter();
            StackFlowSession session = (StackFlowSession)formatter.Deserialize(stream);
            stream.Close();
            return session;
        }
    }
}
