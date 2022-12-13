using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor.Search;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    struct Coord
    {
        public static Coord Zero = new Coord(0, 0);
        public static Coord Error = new Coord(-1, -1);
        public int Y, X;

        public Coord(int y, int x)
        {
            Y = y;
            X = x;
        }

        public static bool operator ==(Coord op1, Coord op2)
            => (op1.X == op2.X) && (op1.Y == op2.Y);

        public static bool operator !=(Coord op1, Coord op2)
            => (op1 == op2);
    }

    enum NodeType
    {
        None,
        Path,
        Obstacle
    }

    public enum FindingOption
    {
        FixedPoints,
        DFS,
        BFS
    }
    [SerializeField] private FindingOption _option;

    struct MapNode
    {
        public Transform Point;
        public Coord Coord;
        public NodeType Type;
    }

    private static MapNode[,] _map;

    public bool TryFindOptimizedPath(Transform start, Transform end, out List<Transform> optimizedPath)
    {
        bool found = false;
        optimizedPath = null;

        switch (_option)
        {
            case FindingOption.FixedPoints:
                break;
            case FindingOption.DFS:
                break;
            case FindingOption.BFS:
                break;
            default:
                break;
        }

        return found;
    }
}
