using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingRoutesSimulation
{
    public class Merchant
    {
        public readonly Point Town;
        public readonly Point CapitalCity;

        public Point Position;

        public bool GoingToCapital = true;

        public Merchant(Point position, Point capital)
        {
            Town = Position = position;
            CapitalCity = capital;
        }

        public void UpdateOn(TerrainType[,] map)
        {
            Point target = GoingToCapital ? CapitalCity : Town;
            var pathfinder = new Pathfinding(map);
            var path = pathfinder.GetPath(Position, target).Skip(1);
            if (path.Any())
            {
                Position = path.First();
            }
            else
            {
                GoingToCapital = !GoingToCapital;
            }
        }
    }
}
