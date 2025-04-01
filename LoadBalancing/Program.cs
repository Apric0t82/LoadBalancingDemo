using LoadBalancing;

static void ProcessRoundRobin()
{
    var servers = new List<string> { "server1", "server2", "server3" };
    var loadBalancer = new RoundRobinLoadBalancer(servers);

    Console.WriteLine("Round Robin Load Balancer");
    for (int i = 0; i < 10; i++)
    {
        var selectedServer = loadBalancer.GetNextServer();
        Console.WriteLine("Request sent to server: {0}", selectedServer);
    }
    Console.WriteLine();
}

static void ProcessLeastConnections()
{
    var servers = new List<LeastConnectionServer>
    {
        new() { Name = "server1", ConnectionsCount = 6 },
        new() { Name = "server2", ConnectionsCount = 7 },
        new() { Name = "server3", ConnectionsCount = 8 }
    };

    var loadBalancer = new LeastConnectionsLoadBalancer(servers);

    Console.WriteLine("Least Connections Load Balancer");
    for (int i = 0; i < 10; i++)
    {
        var selectedServer = loadBalancer.RouteRequest();
        Console.WriteLine("Request sent to server: {0}", selectedServer.Name);
    }
    Console.WriteLine();
}

static void ProcessIPHash()
{
    var servers = new List<string> { "server1", "server2", "server3" };
    var loadBalancer = new IPHashLoadBalancer(servers);

    string[] clientIps = ["192.168.1.1", "192.168.1.2", "192.168.1.3"];

    Console.WriteLine("IP Hash Load Balancer");
    foreach (var clientIp in clientIps)
    {
        var selectedServer = loadBalancer.RouteRequest(clientIp);
        Console.WriteLine($"Request from client with IP {clientIp} sent to server: {selectedServer}");
    }
}


static async Task ProcessThreadSafeRoundRobin()
{
    var servers = new List<Server>();
    for (int i = 1; i <= 100; i++)
    {
        servers.Add(new Server(i));
    }

    var loadBalancer = new ThreadSafeRoundRobinLoadBalancer(servers);
    const int iterations = 10_000;
    int numberOfTasks = Environment.ProcessorCount;

    Console.WriteLine("Thread-Safe Round Robin Load Balancer");
    Console.WriteLine();

    var tasks = Enumerable.Range(1, numberOfTasks).Select(i => Task.Run(() =>
        {
            for (int j = 0; j < iterations; j++)
            {
                var selectedServer = loadBalancer.GetNextServer();
                Console.WriteLine($"Got instance: {selectedServer.Id}, iteration {j} of thread {i}");
            }
        }))
        .ToArray();
    await Task.WhenAll(tasks);

    Console.WriteLine();
}

await ProcessThreadSafeRoundRobin();


ProcessRoundRobin();
ProcessLeastConnections();
ProcessIPHash();