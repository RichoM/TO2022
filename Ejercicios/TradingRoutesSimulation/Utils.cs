using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingRoutesSimulation
{
    public static class Utils
    {

        // http://stackoverflow.com/a/10065670/4357302
        public static float Mod(this float a, float n)
        {
            float result = a % n;
            if ((a < 0 && n > 0) || (a > 0 && n < 0))
                result += n;
            return result;
        }

        public static int Mod(this int a, int n)
        {
            return (int)Mod(a, (float)n);
        }

        public static Point Mod(this Point p, int w, int h)
        {
            return new Point(Mod(p.X, w), Mod(p.Y, h));
        }

        public static Point Mod(this Point p, Size s)
        {
            return Mod(p, s.Width, s.Height);
        }


        public static double Dist(float x1, float y1, float x2, float y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public static double Dist(this PointF p1, PointF p2)
        {
            return Dist(p1.X, p1.Y, p2.X, p2.Y);
        }

        public static double Dist(this PointF p, float x, float y)
        {
            return Dist(p.X, p.Y, x, y);
        }

        public static double Dist(this Point p1, Point p2)
        {
            return Dist(p1.X, p1.Y, p2.X, p2.Y);
        }

        public static double Dist(this Point p, int x, int y)
        {
            return Dist(p.X, p.Y, x, y);
        }
    }
}
