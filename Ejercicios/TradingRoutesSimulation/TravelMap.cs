using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingRoutesSimulation
{
    public class TravelMap
    {
        public class Node
        {
            public Point Previous;
            public double Cost;
        }

        readonly int width;
        readonly int height;
        readonly Node[,] map;
        readonly Point start;

        public TravelMap(int width, int height, Point start)
        {
            this.width = width;
            this.height = height;
            this.start = start;

            map = new Node[width, height];
            map[start.X, start.Y] = new Node() { Previous = start, Cost = 0 };
        }


        public Point[] GetPath(Point target)
        {
            var result = new List<Point>();
            var current = target;
            result.Add(current);
            while (current != start)
            {
                if (!Contains(current)) break;
                var node = Get(current);
                current = node.Previous;
                result.Add(current);
            }

            result.Reverse();
            return result.ToArray();
        }

        public Node Get(Point p) { return map[p.X, p.Y]; }
        public void Set(Point p, Node n) { map[p.X, p.Y] = n; }

        public bool Contains(Point p)
        {
            return p.X >= 0 && p.X < width
                && p.Y >= 0 && p.Y < height
                && map[p.X, p.Y] != null;
        }
    }
}
