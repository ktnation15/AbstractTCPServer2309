using System;
using System.Xml;
using System.IO;

class Program : AbstractServer
{
    static void Main(string[] args)
    {
        string configFilePath = "ServerConfig.xml";
        XmlDocument configDoc = new XmlDocument();

        try
        {
            configDoc.Load(configFilePath);
        }
        catch (FileNotFoundException ex)
        {
            LogError($"File not found: {configFilePath}");
            return;
        }

        int serverPort = ReadIntFromConfig(configDoc, "ServerPort");
        int shutdownPort = ReadIntFromConfig(configDoc, "ShutDownPort");
        string serverName = ReadStringFromConfig(configDoc, "ServerName");
        string debugLevel = ReadStringFromConfig(configDoc, "DebugLevel");

        MyServer server = new MyServer(serverPort, shutdownPort, serverName, debugLevel);
        server.Start();

        LogInformation("Server started successfully.");

        Console.WriteLine("Press Enter to stop the server...");
        Console.ReadLine();

        server.Stop();
        LogInformation("Server stopped.");
    }

    static int ReadIntFromConfig(XmlDocument configDoc, string key)
    {
        // Implementation for reading an integer from the config
        return int.Parse(configDoc.SelectSingleNode($"//{key}").InnerText);
    }

    static string ReadStringFromConfig(XmlDocument configDoc, string key)
    {
        // Implementation for reading a string from the config
        return configDoc.SelectSingleNode($"//{key}").InnerText;
    }
}
