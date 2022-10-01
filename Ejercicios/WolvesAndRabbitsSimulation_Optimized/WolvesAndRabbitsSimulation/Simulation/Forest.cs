using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolvesAndRabbitsSimulation.Engine;

namespace WolvesAndRabbitsSimulation.Simulation
{
    internal class Forest : World
    {
        public const int PATCH_SIZE = 2;
        private int[,] grass;
        private int ticks = 0;

        private static Dictionary<int, Brush> brushes = new Dictionary<int, Brush>();

        public Forest()
        {
            grass = new int[Width / PATCH_SIZE + 1, Height / PATCH_SIZE + 1];
        }

        private static Brush GetBrush(int growth)
        {
            if (!brushes.ContainsKey(growth))
            {
                brushes.Add(growth, new Pen(Color.FromArgb(growth, 0, 255, 0)).Brush);
            }
            return brushes[growth];
        }


        public override void Update()
        {
            if (++ticks > 10)
            {
                ticks = 0;
                for (int x = 0; x < grass.GetLength(0); x++)
                {
                    for (int y = 0; y < grass.GetLength(1); y++)
                    {
                        var growth = grass[x, y];
                        growth += 10;
                        if (growth > 255) { growth = 255; }
                        else if (growth < 0) { growth = 0; }
                        grass[x, y] = growth;
                    }
                }
            }
            base.Update();
        }

        public override void DrawOn(Graphics graphics)
        {
            for (int x = 0; x < grass.GetLength(0); x++)
            {
                for (int y = 0; y < grass.GetLength(1); y++)
                {
                    var brush = GetBrush(grass[x, y]);
                    var rect = new Rectangle(x * PATCH_SIZE, y * PATCH_SIZE, PATCH_SIZE, PATCH_SIZE);
                    graphics.FillRectangle(brush, rect);
                }
            }
            base.DrawOn(graphics);
        }

        public void SetGrassAt(Point pos, int growth)
        {
            if (growth > 255) { growth = 255; }
            else if (growth < 0) { growth = 0; }
            grass[pos.X / PATCH_SIZE, pos.Y / PATCH_SIZE] = growth;
        }

        public int GetGrassAt(Point pos)
        {
            return grass[pos.X / PATCH_SIZE, pos.Y / PATCH_SIZE];
        }
    }
}
