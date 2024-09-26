using System.IO;
using TCPServerFramework.TCPServer;

namespace CustomTCPServerApp.Server
{
    public class MyServer : AbstractTCPServer
    {
        public MyServer(int port, string serverName) : base(port, serverName)
        {
        }

        protected override void TcpServerWork(StreamReader reader, StreamWriter writer)
        {
            string? s;
            while ((s = reader.ReadLine()) != null)
            {
                string response = s.ToUpper();
                Console.WriteLine($"Received: {s}");
                writer.WriteLine(response);
                Console.WriteLine($"Echoed: {response}");
            }
        }
    }
}
