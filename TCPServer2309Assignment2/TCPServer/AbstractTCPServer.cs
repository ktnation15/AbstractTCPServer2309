using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TCPServerFramework.TCPServer
{
    public abstract class AbstractTCPServer
    {
        private readonly int _port;
        private readonly string _serverName;

        protected AbstractTCPServer(int port, string serverName)
        {
            _port = port;
            _serverName = serverName;
        }

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, _port);
            listener.Start();
            Console.WriteLine($"{_serverName} started at port {_port}");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Client incoming");
                Console.WriteLine($"Remote (IP, Port) = ({client.Client.RemoteEndPoint})");

                Task.Run(() =>
                {
                    TcpClient tmpClient = client;
                    DoOneClient(tmpClient);
                });
            }
        }

        private void DoOneClient(TcpClient sock)
        {
            using (StreamReader sr = new StreamReader(sock.GetStream()))
            using (StreamWriter sw = new StreamWriter(sock.GetStream()))
            {
                sw.AutoFlush = true;
                Console.WriteLine("Handle one client");

                TcpServerWork(sr, sw);
            }

            Console.WriteLine("Client disconnected.");
        }

        protected abstract void TcpServerWork(StreamReader reader, StreamWriter writer);
    }
}
