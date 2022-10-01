using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolvesAndRabbitsSimulation.Engine
{
    class World
    {
        private Random rnd = new Random();

        private const int width = 255;
        private const int height = 255;
        private Size size = new Size(width, height);

        private List<GameObject>[,] grid = new List<GameObject>[width, height];
        private List<GameObject> removed = new List<GameObject>();

        public IEnumerable<GameObject> GameObjects
        {
            get
            {
                var objects = new List<GameObject>();
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < width; y++)
                    {
                        if (grid[x, y] != null)
                        {
                            objects.AddRange(grid[x, y]);
                        }
                    }
                }
                return objects;
            }
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public float Random()
        {
            return (float)rnd.NextDouble();
        }

        public Point RandomPoint()
        {
            return new Point(rnd.Next(width), rnd.Next(height));
        }

        public int Random(int min, int max)
        {
            return rnd.Next(min, max);
        }


        public void Add(GameObject obj)
        {
            var bucket = GetBucketAt(obj.Position);
            if (bucket == null)
            {
                bucket = InitBucketAt(obj.Position);
            }
            bucket.Add(obj);
        }
        public void Remove(GameObject obj)
        {
            removed.Add(obj);
            var bucket = GetBucketAt(obj.Position);
            if (bucket != null)
            {
                bucket.Remove(obj);
            }
        }

        private List<GameObject> GetBucketAt(Point pos)
        {
            var x = PositiveMod(pos.X, width);
            var y = PositiveMod(pos.Y, height);

            return grid[x, y];
        }

        private List<GameObject> InitBucketAt(Point pos)
        {
            var bucket = new List<GameObject>();
            var x = PositiveMod(pos.X, width);
            var y = PositiveMod(pos.Y, height);
            grid[x, y] = bucket;
            return bucket;
        }


        public virtual void Update()
        {
            foreach (GameObject obj in GameObjects)
            {
                Point old_pos = obj.Position;
                obj.UpdateOn(this);
                obj.Position = PositiveMod(obj.Position, size);
                if (!old_pos.Equals(obj.Position))
                {
                    GetBucketAt(old_pos).Remove(obj);
                    if (removed.Contains(obj))
                    {
                        removed.Remove(obj);
                    }
                    else
                    {
                        Add(obj);
                    }
                }
            }
        }

        public virtual void DrawOn(Graphics graphics)
        {
            foreach (GameObject obj in GameObjects)
            {
                graphics.FillRectangle(new Pen(obj.Color).Brush, obj.Bounds);
            }
        }

        // http://stackoverflow.com/a/10065670/4357302
        private static int PositiveMod(int a, int n)
        {
            int result = a % n;
            if ((a < 0 && n > 0) || (a > 0 && n < 0))
                result += n;
            return result;
        }
        private static Point PositiveMod(Point p, Size s)
        {
            return new Point(PositiveMod(p.X, s.Width), PositiveMod(p.Y, s.Height));
        }

        public double Dist(PointF a, PointF b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        public IEnumerable<GameObject> ObjectsAt(Point pos)
        {
            return grid[pos.X, pos.Y];
        }

    }
}
