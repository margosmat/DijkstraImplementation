using System;
using System.Collections.Generic;

namespace DijkstraImplementation
{
    class Program
    {
        public enum Node
        {
            Start,
            A,
            B,
            C,
            D,
            Finish,
            None
        }

        static void Main(string[] args)
        {
            Dijkstra1();
            Dijkstra2();
        }

        public static void Dijkstra1()
        {
            var graph = new Dictionary<Node, Dictionary<Node, int>>()
            {
                { Node.Start, new Dictionary<Node, int>() { {Node.A, 5}, {Node.B, 2} } },
                { Node.A, new Dictionary<Node, int>() { {Node.C, 4}, {Node.D, 2} } },
                { Node.B, new Dictionary<Node, int>() { {Node.A, 8}, {Node.D, 7} } },
                { Node.C, new Dictionary<Node, int>() { {Node.D, 6}, {Node.Finish, 3} } },
                { Node.D, new Dictionary<Node, int>() { {Node.Finish, 1} } },
                { Node.Finish, new Dictionary<Node, int>() }
            };

            var costs = new Dictionary<Node, int>()
            {
                {Node.A, 5},
                {Node.B, 2},
                {Node.C, Int32.MaxValue},
                {Node.D, Int32.MaxValue},
                {Node.Finish, Int32.MaxValue}
            };

            var parents = new Dictionary<Node, Node>()
            {
                {Node.A, Node.Start},
                {Node.B, Node.Start},
                {Node.C, Node.None},
                {Node.D, Node.None},
                {Node.Finish, Node.None}
            };

            var processed = new List<Node>();

            var node = FindLowestCostNode(costs, processed);
            while (node != Node.None)
            {
                var cost = costs[node];
                var neighbours = graph[node];
                foreach (var n in neighbours.Keys)
                {
                    var newCost = cost + neighbours[n];
                    if (costs[n] > newCost)
                    {
                        costs[n] = newCost;
                        parents[n] = node;
                    }
                }
                processed.Add(node);
                node = FindLowestCostNode(costs, processed);
            }

            Console.WriteLine("Dijkstra 1: " +  costs[Node.Finish]);
        }

        public static void Dijkstra2()
        {
            var graph = new Dictionary<Node, Dictionary<Node, int>>()
            {
                { Node.Start, new Dictionary<Node, int>() { {Node.A, 10} } },
                { Node.A, new Dictionary<Node, int>() { {Node.B, 20} } },
                { Node.B, new Dictionary<Node, int>() { {Node.Finish, 30}, {Node.C, 1} } },
                { Node.C, new Dictionary<Node, int>() { {Node.A, 1} } },
                { Node.Finish, new Dictionary<Node, int>() }
            };

            var costs = new Dictionary<Node, int>()
            {
                {Node.A, 10},
                {Node.B, Int32.MaxValue},
                {Node.C, Int32.MaxValue},
                {Node.Finish, Int32.MaxValue}
            };

            var parents = new Dictionary<Node, Node>()
            {
                {Node.A, Node.Start},
                {Node.B, Node.None},
                {Node.C, Node.None},
                {Node.Finish, Node.None}
            };

            var processed = new List<Node>();

            var node = FindLowestCostNode(costs, processed);
            while (node != Node.None)
            {
                var cost = costs[node];
                var neighbours = graph[node];
                foreach (var n in neighbours.Keys)
                {
                    var newCost = cost + neighbours[n];
                    if (costs[n] > newCost)
                    {
                        costs[n] = newCost;
                        parents[n] = node;
                    }
                }
                processed.Add(node);
                node = FindLowestCostNode(costs, processed);
            }

            Console.WriteLine("Dijkstra 2: " +  costs[Node.Finish]);
        }

        private static Node FindLowestCostNode(Dictionary<Node, int> costs, List<Node> processedNodes)
        {
            var lowestCost = Int32.MaxValue;
            var lowestCostNode = Node.None;

            foreach (var node in costs.Keys)
            {
                var cost = costs[node];
                if (cost < lowestCost && !processedNodes.Contains(node))
                {
                    lowestCost = cost;
                    lowestCostNode = node;
                }
            }

            return lowestCostNode;
        }
    }
}
