using System.IO;
using System.Threading.Tasks;
using TCPServerFramework.TCPServer;

namespace CustomTCPServerApp.Server
{
    public class MyServer : AbstractTCPServer
    {
        public MyServer(int port, string serverName) : base(port, serverName)
        {
        }

        protected override async Task TcpServerWorkAsync(StreamReader reader, StreamWriter writer)
        {
            string? s;
            while ((s = await reader.ReadLineAsync()) != null)
            {
                string response = s.ToUpper();
                Console.WriteLine($"Received: {s}");
                await writer.WriteLineAsync(response);
                Console.WriteLine($"Echoed: {response}");
            }
        }
    }
}
