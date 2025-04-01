using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoadBalancing
{
    public class ThreadSafeRoundRobinLoadBalancer
    {
        private readonly List<Server> servers;
        private readonly int size;
        private int position = -1;

        public ThreadSafeRoundRobinLoadBalancer(List<Server> servers)
        {
            if (servers.Count == 0)
                throw new NullReferenceException("Servers list is empty");

            this.servers = servers;
            this.size = servers.Count;
        }

        public Server GetNextServer()
        {
            if (size == 1)
            {
                return servers[0];
            }

            Interlocked.Increment(ref position);

            // Interlocked.Increment will reach int.MaxValue in very high load scenarios.
            // In that case, it handles the overflow condition and returns int.MinValue,
            // and System.ArgumentOutOfRangeException will be thrown while accessing the array because of the negative index.
            if (position == int.MinValue)
            {
                Interlocked.Exchange(ref position, 0);
            }

            int candidateIndex = position % size;

            Server candidate = servers[candidateIndex];
            return candidate;
        }
    }

    public class Server(int id)
    {
        public int Id { get; } = id;
        public string Name { get; } = "server" + id;
    }
}
