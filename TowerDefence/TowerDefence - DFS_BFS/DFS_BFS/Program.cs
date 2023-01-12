using System;
using System.Collections.Generic;

namespace TEST_DFS_BFS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PathfindingAlgos.Node Node1 = new PathfindingAlgos.Node(1);
            PathfindingAlgos.Node Node2 = new PathfindingAlgos.Node(2);
            PathfindingAlgos.Node Node3 = new PathfindingAlgos.Node(3);
            PathfindingAlgos.Node Node4 = new PathfindingAlgos.Node(4);
            PathfindingAlgos.Node Node5 = new PathfindingAlgos.Node(5);
            PathfindingAlgos.Node Node6 = new PathfindingAlgos.Node(6);
            PathfindingAlgos.Node Node7 = new PathfindingAlgos.Node(7);
            PathfindingAlgos.Node Node8 = new PathfindingAlgos.Node(8);
            PathfindingAlgos.Node Node9 = new PathfindingAlgos.Node(9);

            Node1.AddChild(Node2)
                 .AddChild(Node8);

            Node2.AddChild(Node3)
                 .AddChild(Node4);

            Node4.AddChild(Node5)
                 .AddChild(Node6)
                 .AddChild(Node7);

            Node8.AddChild(Node9);

            bool resultDFS = PathfindingAlgos.DFS(Node1, 6);
            bool resultBFS = PathfindingAlgos.BFS(Node1, 6);
            Console.WriteLine(resultDFS);
            Console.WriteLine(resultBFS);

        }
    }

    class PathfindingAlgos
    {
        public class Node
        {
            public int Value;
            public List<Node> Children = new List<Node>();

            public Node(int value)
            {
                Value = value;
            }

            public Node AddChild(Node child)
            {
                Children.Add(child);
                return this;
            }
        }
        Node Root;
        private static int s_count;
        public static bool DFS(Node node, int targetValue)
        {
            bool success = false;
            foreach (Node child in node.Children)
            {
                if (child.Value == targetValue)
                    return true;
                else
                {
                    success = DFS(child, targetValue);
                    if (success)
                        break;
                }
            }

            return success;
        }

        public static bool BFS(Node node, int targetValue)
        {
            List<int> results = new List<int>();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(node);

            int count = 0;
            while (queue.Count > 0)
            {
                count++;
                Node next = queue.Dequeue();

                foreach (Node child in next.Children)
                {
                    if (child.Value == targetValue)
                        results.Add(count);
                    else
                        queue.Enqueue(child);
                }
            }

            return results.Count > 0;
        }
    }
}