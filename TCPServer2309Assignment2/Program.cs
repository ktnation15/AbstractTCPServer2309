using System;
using TCPServerFramework;

namespace TCPServerTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            EchoTCPServer server = new EchoTCPServer(7007, "EchoServer");
            server.Start();
        }
    }
}
