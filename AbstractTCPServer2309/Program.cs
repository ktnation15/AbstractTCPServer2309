using System;

namespace TCPEchoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            EchoServer server = new EchoServer();
            server.Start();
        }
    }
}
