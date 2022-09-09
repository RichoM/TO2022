using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticlePhysicsSimulation
{
    public class Simulation
    {
        public const int WIDTH = 200;
        public const int HEIGHT = 200;

        List<Particle> particles = new List<Particle>();
        int frames = 0;

        public void AddParticle(int x, int y, Particle particle)
        {
            if (IsInside(x, y))
            {
                if (IsEmpty(x, y))
                {
                    particle.X = x;
                    particle.Y = y;
                    particles.Add(particle);
                }
            }
        }

        public void RemoveParticle(int x, int y)
        {
            if (IsInside(x, y))
            {
                var particle = GetParticle(x, y);
                if (particle != null)
                {
                    particles.Remove(particle);
                }
            }
        }

        public bool IsInside(int x, int y)
        {
            return x > 0 && x < WIDTH && y > 0 && y < HEIGHT;
        }
        public bool IsEmpty(int x, int y)
        {
            return GetParticle(x, y) == null;
        }

        public void Step()
        {
            frames++;

            for (int y = HEIGHT-1; y >= 0; y--)
            {
                if (frames % 2 == 0)
                {
                    for (int x = WIDTH - 1; x >= 0; x--)
                    {
                        ParticleStep(x, y);
                    }
                } 
                else
                {
                    for (int x = 0; x < WIDTH; x++)
                    {
                        ParticleStep(x, y);
                    }
                }
            }
        }

        private void ParticleStep(int x, int y)
        {
            GetParticle(x, y)?.Step(this);
        }

        public bool TryToMove(int x1, int y1, int x2, int y2)
        {
            if (IsInside(x2, y2) && IsEmpty(x2, y2))
            {
                var particle = GetParticle(x1, y1);
                particle.X = x2;
                particle.Y = y2;
                return true;
            }
            return false;
        }

        private Particle GetParticle(int x, int y)
        {
            return particles.FirstOrDefault(p => p.X == x && p.Y == y);
        }

        public void Clear()
        {
            particles.Clear();
        }

        public void DrawOn(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Black, 0, 0, WIDTH, HEIGHT);
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    var p = GetParticle(x, y);
                    if (p != null)
                    {
                        graphics.FillRectangle(new SolidBrush(p.Color), x, y, 1, 1);
                    }
                }
            }
        }
    }
}
