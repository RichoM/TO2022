using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingRoutesSimulation
{
    public class GridWalker
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public double R { get; private set; }

        public Point Position { get { return new Point(X, Y); } }

        public GridWalker(int x, int y, double r = 0)
        {
            X = x;
            Y = y;
            R = r;
        }

        public GridWalker(Point position, double r = 0) : this(position.X, position.Y, r)
        {}

        public void Turn(double angle)
        {
            R += angle;
        }

        public void Forward(double dist = 1)
        {
            X = (int)Math.Round(Math.Cos(R) * dist + X);
            Y = (int)Math.Round(Math.Sin(R) * dist + Y);
        }

        public void SpiralUntil(Func<bool> condition)
        {
            if (condition()) return;
            var dist = 1;
            while (true)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < dist; j++)
                    {
                        Forward(1);
                        if (condition()) return;
                    }
                    Turn(Math.PI / 2); // 90 deg
                }
                dist++;
            }
        }
    }
}
