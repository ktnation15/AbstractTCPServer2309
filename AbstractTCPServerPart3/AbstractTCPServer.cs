using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TCPServerFramework.TCPServer
{
    /// <summary>
    /// Abstract base class for creating a TCP server.
    /// </summary>
    public abstract class AbstractTCPServer
    {
        private readonly int _port;
        private readonly int _shutdownPort;
        private readonly string _serverName;
        private readonly string _debugLevel;
        private TcpListener? _listener;
        private TcpListener? _stopListener;
        private bool _running;

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractTCPServer"/> class.
        /// </summary>
        /// <param name="port">The port number on which the server listens.</param>
        /// <param name="shutdownPort">The port number for shutdown commands.</param>
        /// <param name="serverName">The name of the server.</param>
        /// <param name="debugLevel">The debug level (info, warning, error).</param>
        protected AbstractTCPServer(int port, int shutdownPort, string serverName, string debugLevel)
        {
            _port = port;
            _shutdownPort = shutdownPort;
            _serverName = serverName;
            _debugLevel = debugLevel;
            _running = true;
        }

        /// <summary>
        /// Starts the TCP server.
        /// </summary>
        public void Start()
        {
            _listener = new TcpListener(IPAddress.Any, _port);
            _listener.Start();
            Console.WriteLine($"{_serverName} started at port {_port}");

            Task.Run(() => ListenForStopCommand(_shutdownPort));

            while (_running)
            {
                if (_listener.Pending())
                {
                    var client = _listener.AcceptTcpClient();
                    Console.WriteLine("Client incoming");
                    Console.WriteLine($"Remote (IP, Port) = ({client.Client.RemoteEndPoint})");

                    Task.Run(() => HandleClientAsync(client));
                }
                else
                {
                    Thread.Sleep(2000); // Wait for 2 seconds
                }
            }

            _listener.Stop();
            Console.WriteLine($"{_serverName} stopped.");
        }

        /// <summary>
        /// Stops the TCP server gracefully.
        /// </summary>
        public void Stop()
        {
            _running = false;
        }

        /// <summary>
        /// Listens for stop commands on a specified port.
        /// </summary>
        /// <param name="stopPort">The port number to listen for stop commands.</param>
        private void ListenForStopCommand(int stopPort)
        {
            _stopListener = new TcpListener(IPAddress.Any, stopPort);
            _stopListener.Start();
            Console.WriteLine($"Stop server listening on port {stopPort}");

            while (_running)
            {
                if (_stopListener.Pending())
                {
                    var client = _stopListener.AcceptTcpClient();
                    using (var sr = new StreamReader(client.GetStream()))
                    {
                        var command = sr.ReadLine();
                        if (command == "STOP")
                        {
                            Console.WriteLine("Stop command received.");
                            Stop();
                        }
                    }
                }
                else
                {
                    Thread.Sleep(2000); // Wait for 2 seconds
                }
            }

            _stopListener.Stop();
        }

        /// <summary>
        /// Handles a single client connection.
        /// </summary>
        /// <param name="client">The TCP client.</param>
        private async Task HandleClientAsync(TcpClient client)
        {
            using (client)
            {
                using (StreamReader sr = new StreamReader(client.GetStream()))
                using (StreamWriter sw = new StreamWriter(client.GetStream()))
                {
                    sw.AutoFlush = true;
                    Console.WriteLine("Handle one client");

                    await TcpServerWorkAsync(sr, sw);
                }

                Console.WriteLine("Client disconnected.");
            }
        }

        /// <summary>
        /// Abstract method to be implemented by derived classes to handle client communication.
        /// </summary>
        /// <param name="reader">The stream reader for reading client data.</param>
        /// <param name="writer">The stream writer for sending data to the client.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        protected abstract Task TcpServerWorkAsync(StreamReader reader, StreamWriter writer);
    }
}

