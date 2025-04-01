namespace LoadBalancing;

public class RoundRobinLoadBalancer
{
    private readonly List<string> servers;
    private int currentIndex;

    public RoundRobinLoadBalancer(List<string> servers)
    {
        this.servers = servers;
        currentIndex = 0;
    }

    public string GetNextServer()
    {
        string server = servers[currentIndex];
        currentIndex = (currentIndex + 1) % servers.Count;
        return server;
    }
}