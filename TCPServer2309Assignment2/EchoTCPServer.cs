using System.IO;
using TCPServerFramework.TCPServer;

namespace TCPServerFramework
{
    public class EchoTCPServer : AbstractTCPServer
    {
        public EchoTCPServer(int port, string serverName) : base(port, serverName)
        {
        }

        protected override void TcpServerWork(StreamReader reader, StreamWriter writer)
        {
            string? s;
            while ((s = reader.ReadLine()) != null)
            {
                Console.WriteLine($"Received: {s}");
                writer.WriteLine(s);
                Console.WriteLine($"Echoed: {s}");
            }
        }
    }
}
