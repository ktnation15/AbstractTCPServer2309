using System.Diagnostics;

public abstract class AbstractServer
{
    private static readonly TraceSource traceSource = new TraceSource("AbstractServerTraceSource")
    {
        Switch = new SourceSwitch("SourceSwitch", "All")
    };

    static AbstractServer()
    {
        // Add Console Listener
        traceSource.Listeners.Add(new ConsoleTraceListener());

        // Add Text Writer Listener
        traceSource.Listeners.Add(new TextWriterTraceListener("server.log"));

        // Add EventLog Listener
        if (!EventLog.SourceExists("AbstractServerSource"))
        {
            EventLog.CreateEventSource("AbstractServerSource", "Application");
        }
        traceSource.Listeners.Add(new EventLogTraceListener("AbstractServerSource"));
    }

    protected void LogVerbose(string message)
    {
        traceSource.TraceEvent(TraceEventType.Verbose, 0, message);
    }

    protected void LogInformation(string message)
    {
        traceSource.TraceEvent(TraceEventType.Information, 0, message);
    }

    protected void LogWarning(string message)
    {
        traceSource.TraceEvent(TraceEventType.Warning, 0, message);
    }

    protected void LogError(string message)
    {
        traceSource.TraceEvent(TraceEventType.Error, 0, message);
    }

    // Other server methods...
}
