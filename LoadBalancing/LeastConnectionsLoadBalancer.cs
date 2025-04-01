namespace LoadBalancing;

public class LeastConnectionsLoadBalancer
{
    private readonly List<LeastConnectionServer> servers;

    public LeastConnectionsLoadBalancer(List<LeastConnectionServer> servers)
    {
        this.servers = servers;
    }

    private LeastConnectionServer GetLeastConnectionsServer()
    {
        var leastConnections = this.servers.MinBy(s => s.ConnectionsCount);
        return leastConnections;
    }

    public LeastConnectionServer RouteRequest()
    {
        var selectedServer = GetLeastConnectionsServer();
        selectedServer.ConnectionsCount++;
        return selectedServer;
    }
}

public record LeastConnectionServer
{
    public string? Name { get; set; }
    public int ConnectionsCount { get; set; }
}
