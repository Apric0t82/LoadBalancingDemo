# Load Balancing Demo

## Description

This solution provides a load balancing mechanisms using the Round Robin, Least Connections, and IP Hash approaches. It includes also a thread-safe implementation of Round Robin to handle concurrent requests.

- **Round Robin Approach**: Distributes requests evenly across a list of servers, ensuring that each server gets an equal share of the load.
- **Least Connections Approach**: Routes each new request to the server with the fewest current connections. This helps to ensure an even distribution of load, especially when servers have different processing capacities.
- **IP Hash Approach**: Uses a hash of the client's IP address to determine which server should handle the request. This method ensures that the same client IP will always be routed to the same server, providing session persistence.

## Interesting Techniques

- **Round Robin Algorithm**: The core of the load balancing mechanism is based on the Round Robin algorithm, which cycles through a list of servers to distribute requests evenly.
- **Least Connections Algorithm**: The `LeastConnectionsLoadBalancer` class selects the server with the fewest active connections to handle the next request.
- **IP Hash Algorithm**: The `IPHashLoadBalancer` class uses the client's IP address to compute a hash, which is then used to select a server. This ensures consistent routing for clients with the same IP.
- **Thread Safety**: The `ThreadSafeRoundRobinLoadBalancer` class ensures that the load balancing mechanism works correctly even when accessed by multiple threads concurrently. This is achieved using locking mechanisms to prevent race conditions.
- **Unit Testing with Concurrent Tasks**: The unit tests include scenarios where multiple threads access the load balancer simultaneously, ensuring that the thread safety mechanisms are effective.
