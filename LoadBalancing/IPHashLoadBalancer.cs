using System.Text;
using System.Security.Cryptography;

namespace LoadBalancing
{
    public class IPHashLoadBalancer
    {
        private readonly List<string> servers;

        public IPHashLoadBalancer(List<string> servers)
        {
            this.servers = servers;
        }

        public string? RouteRequest(string clientIp)
        {
            try
            {
                byte[] hashBytes = MD5.HashData(Encoding.UTF8.GetBytes(clientIp));
                var ipHash = BitConverter.ToInt32(hashBytes, 0);
                var selectedServerIndex = Math.Abs(ipHash) % servers.Count;
                return servers[selectedServerIndex];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error routing request: {ex.Message}");
                return null;
            }
        }
    }
}
