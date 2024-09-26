using System;
using CustomTCPServerApp.Server;

namespace CustomTCPServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            MyServer server = new MyServer(7007, "MyCustomEchoServer");
            server.Start();
        }
    }
}
