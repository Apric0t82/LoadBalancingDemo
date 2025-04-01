# Load Balancing Demo

## Description

This solution provides a load balancing mechanisms using the Round Robin, Least Connections and IP Hash algorithms. It includes also a thread-safe implementation of Round Robin to handle concurrent requests efficiently. 
Round Robin approach is designed to distribute requests evenly across a list of servers, ensuring that each server gets an equal share of the load.

## Interesting Techniques

- **Round Robin Algorithm**: The core of the load balancing mechanism is based on the Round Robin algorithm, which cycles through a list of servers to distribute requests evenly.
- **Thread Safety**: The `ThreadSafeRoundRobinLoadBalancer` class ensures that the load balancing mechanism works correctly even when accessed by multiple threads concurrently. This is achieved using synchronization techniques to manage the state of the load balancer.
- **Unit Testing with Concurrent Tasks**: The unit tests include scenarios where multiple threads access the load balancer simultaneously, ensuring that the thread safety mechanisms are effective.
