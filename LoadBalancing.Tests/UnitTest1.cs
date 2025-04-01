using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace LoadBalancing.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Should_Start_Back_At_The_Top_Of_The_List()
        {
            // Act
            var servers = new List<string> { "server1", "server2", "server3", "server4", "server5" };
            var loadBalancer = new RoundRobinLoadBalancer(servers);

            // Assert
            Assert.Equal(servers[0], loadBalancer.GetNextServer());
            Assert.Equal(servers[1], loadBalancer.GetNextServer());
            Assert.Equal(servers[2], loadBalancer.GetNextServer());
            Assert.Equal(servers[3], loadBalancer.GetNextServer());
            Assert.Equal(servers[4], loadBalancer.GetNextServer());

            Assert.Equal(servers[0], loadBalancer.GetNextServer());
            Assert.Equal(servers[1], loadBalancer.GetNextServer());
            Assert.Equal(servers[2], loadBalancer.GetNextServer());
            Assert.Equal(servers[3], loadBalancer.GetNextServer());
            Assert.Equal(servers[4], loadBalancer.GetNextServer());
        }

        private async Task<List<Server>> MultiThreadCalc(ThreadSafeRoundRobinLoadBalancer rb)
        {
            var tasks = new Task[10];
            var result = new List<Server>();
            for (var i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() =>
                {
                    result.Add(rb.GetNextServer());
                });
            }
            await Task.WhenAll(tasks);
            return result.ToList();
        }

        [Fact]
        public async Task Should_Return_Same_Items_When_Called_From_Concurrent_Threads()
        {
            // Arrange
            var servers = new List<Server>();
            for (int i = 1; i <= 3; i++)
            {
                servers.Add(new Server(i));
            }
            var loadBalancer = new ThreadSafeRoundRobinLoadBalancer(servers);
            
            // Act
            var result = await MultiThreadCalc(loadBalancer);
            var mustBe = new List<Server> { servers[0], servers[1], servers[2], servers[0], servers[1], servers[2], servers[0], servers[1], servers[2], servers[0] };

            // Assert
            Assert.Equal(mustBe, result);
        }
    }
}
