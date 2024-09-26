using System;
using System.Xml;
using CustomTCPServerApp.Server;

namespace CustomTCPServerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string configFilePath = "ServerConfig.xml";
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(configFilePath);

            int serverPort = ReadIntFromConfig(configDoc, "ServerPort");
            int shutdownPort = ReadIntFromConfig(configDoc, "ShutDownPort");
            string serverName = ReadStringFromConfig(configDoc, "ServerName");
            string debugLevel = ReadStringFromConfig(configDoc, "DebugLevel");

            MyServer server = new MyServer(serverPort, shutdownPort, serverName, debugLevel);
            server.Start();

            Console.WriteLine("Press Enter to stop the server...");
            Console.ReadLine();

            server.Stop();
        }

        private static int ReadIntFromConfig(XmlDocument configDoc, string tagName)
        {
            XmlNode? node = configDoc.DocumentElement?.SelectSingleNode(tagName);
            if (node != null)
            {
                string value = node.InnerText.Trim();
                return Convert.ToInt32(value);
            }
            throw new Exception($"Tag {tagName} not found in configuration file.");
        }

        private static string ReadStringFromConfig(XmlDocument configDoc, string tagName)
        {
            XmlNode? node = configDoc.DocumentElement?.SelectSingleNode(tagName);
            if (node != null)
            {
                return node.InnerText.Trim();
            }
            throw new Exception($"Tag {tagName} not found in configuration file.");
        }
    }
}

