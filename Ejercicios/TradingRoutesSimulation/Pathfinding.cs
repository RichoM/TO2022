using Priority_Queue;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingRoutesSimulation
{
    // NOTE(Richo): A* algorithm from https://www.redblobgames.com/pathfinding/a-star/introduction.html
    public class Pathfinding
    {
        readonly TerrainType[,] map;
        readonly int width;
        readonly int height;

        public Pathfinding(TerrainType[,] map)
        {
            this.map = map;
            width = map.GetUpperBound(0) + 1;
            height = map.GetUpperBound(1) + 1;
        }

        public TravelMap CalculateMap(Point start, Point goal)
        {
            var frontier = new SimplePriorityQueue<Point, double>();
            frontier.Enqueue(start, 0);

            var travelMap = new TravelMap(width, height, start);

            while (frontier.Count > 0)
            {
                var current = frontier.Dequeue();
                if (current == goal) break; // Early exit

                foreach (var next in GetNeighbours(current))
                {
                    var cost = travelMap.Get(current).Cost + GetTravelCost(next);
                    if (!travelMap.Contains(next))
                    {
                        Enqueue(frontier, next, goal, cost);
                        travelMap.Set(next, new TravelMap.Node()
                        {
                            Previous = current,
                            Cost = cost
                        });
                    }
                    else
                    {
                        var node = travelMap.Get(next);
                        if (cost < node.Cost)
                        {
                            node.Previous = current;
                            node.Cost = cost;
                            Enqueue(frontier, next, goal, cost);
                        }
                    }
                }
            }
            return travelMap;
        }

        public IEnumerable<Point> GetPath(Point start, Point goal)
        {
            return CalculateMap(start, goal).GetPath(goal);
        }

        private void Enqueue(SimplePriorityQueue<Point, double> frontier, Point next, Point goal, double cost)
        {
            if (frontier.Contains(next))
            {
                frontier.UpdatePriority(next, cost + GetHeuristic(next, goal));
            }
            else
            {
                frontier.Enqueue(next, cost + GetHeuristic(next, goal));
            }
        }

        private IEnumerable<Point> GetNeighbours(Point point)
        {
            Func<int, int, Point> p = (x, y) => new Point(x, y);
            var offsets = new[]
            {
                p(0, -1),
                p(1,  0),
                p(0,  1),
                p(-1,  0),

                p(-1, -1),
                p(1, -1),
                p(-1,  1),
                p(1,  1),
            };
            return offsets
                .Select(o => p(o.X + point.X, o.Y + point.Y))
                .Where(pt => pt.X >= 0 && pt.X < width
                            && pt.Y >= 0 && pt.Y < height)
                .Where(pt => map[pt.X, pt.Y] != TerrainType.WaterDeep)
                .Where(pt => map[pt.X, pt.Y] != TerrainType.Snow);
        }

        private int GetTravelCost(Point destination)
        {
            switch (map[destination.X, destination.Y])
            {
                case TerrainType.WaterShallow: return 25;
                case TerrainType.Sand: return 5;
                case TerrainType.Forest: return 1;
                case TerrainType.Mountain: return 100;

                default: return 1000;
            }
        }

        private double GetHeuristic(Point p1, Point p2)
        {
            return p1.Dist(p2);
        }
    }
}
