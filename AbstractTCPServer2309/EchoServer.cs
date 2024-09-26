using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace TCPEchoServer
{
    public class EchoServer
    {
        private const int PORT = 7007; // Changed port to 7007 as per your requirement

        public EchoServer()
        {
        }

        public void Start() // Changed from static to instance method
        {
            TcpListener listener = new TcpListener(IPAddress.Any, PORT); // Changed from IPAddress.Loopback to IPAddress.Any
            listener.Start();// Start listening
            Console.WriteLine("Server started on port " + PORT);// Print server started message

            while (true)// Infinite loop to keep server running
            {
                TcpClient client = listener.AcceptTcpClient();// Accept incoming client
                Console.WriteLine("Client incoming");// Print client incoming message
                Console.WriteLine($"Remote (IP, Port) = ({client.Client.RemoteEndPoint})");// Print client IP and port
                // Handle client in a separate task
                Task.Run(() =>
                {
                    TcpClient tmpClient = client;// Assign client to a temporary variable
                    DoOneClient(tmpClient);// Handle client
                });
            }
        }
        // Handle one client
        private void DoOneClient(TcpClient sock)
        {
            // Use using to ensure that the resources are released
            using (StreamReader sr = new StreamReader(sock.GetStream()))// Read from client
            using (StreamWriter sw = new StreamWriter(sock.GetStream()))//
            {
                // Set AutoFlush to true to ensure that the data is sent immediately
                sw.AutoFlush = true;
                Console.WriteLine("Handle one client");

                // Simple echo
                string? s;
                while ((s = sr.ReadLine()) != null)
                {
                    // Print received message
                    Console.WriteLine($"Received: {s}");
                    sw.WriteLine(s);
                    Console.WriteLine($"Echoed: {s}");
                }
            }
            // Client disconnected
            Console.WriteLine("Client disconnected.");
        }
    }
}
