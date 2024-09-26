public class MyServer : AbstractServer
{
    private int serverPort;
    private int shutdownPort;
    private string serverName;
    private string debugLevel;

    public MyServer(int serverPort, int shutdownPort, string serverName, string debugLevel)
    {
        this.serverPort = serverPort;
        this.shutdownPort = shutdownPort;
        this.serverName = serverName;
        this.debugLevel = debugLevel;
    }

    public void Start()
    {
        LogInformation($"Starting server {serverName} on port {serverPort} with debug level {debugLevel}.");
        // Server start logic...
    }

    public void Stop()
    {
        LogInformation($"Stopping server {serverName}.");
        // Server stop logic...
    }
}
