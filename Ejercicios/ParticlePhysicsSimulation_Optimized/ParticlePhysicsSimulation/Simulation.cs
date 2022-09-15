using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticlePhysicsSimulation
{
    public enum ParticleType
    {
        EMPTY,
        SAND,
        WATER,
        WOOD,

        OUT
    }

    public class Simulation
    {
        public const int WIDTH = 200;
        public const int HEIGHT = 200;
        
        ParticleType[,] particles = new ParticleType[WIDTH, HEIGHT];
        int frames = 0;

        public void AddParticle(int x, int y, ParticleType type = ParticleType.WATER)
        {
            if (x > 0 && x < WIDTH && y > 0 && y < HEIGHT)
            {
                particles[x, y] = type;
            }
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
            var p = GetParticle(x, y);
            if (p == ParticleType.EMPTY) return;
            else if (p == ParticleType.WOOD) return;
            else if (p == ParticleType.SAND)
            {
                if (TryToMove(x, y, x, y + 1)) return;
                if (TryToMove(x, y, x - 1, y + 1)) return;
                if (TryToMove(x, y, x + 1, y + 1)) return;
            }
            else if (p == ParticleType.WATER)
            {
                if (TryToMove(x, y, x, y + 1)) return;
                if (TryToMove(x, y, x - 1, y + 1)) return;
                if (TryToMove(x, y, x + 1, y + 1)) return;

                if (TryToMove(x, y, x - 1, y)) return;
                if (TryToMove(x, y, x + 1, y)) return;
            }
        }

        private bool TryToMove(int x1, int y1, int x2, int y2)
        {
            if (GetParticle(x2, y2) == ParticleType.EMPTY)
            {
                particles[x2, y2] = GetParticle(x1, y1);
                particles[x1, y1] = ParticleType.EMPTY;
                return true;
            }
            return false;
        }

        private ParticleType GetParticle(int x, int y)
        {
            if (x < 0 || y < 0 || x >= WIDTH || y >= HEIGHT)
            {
                return ParticleType.OUT;
            }
            return particles[x, y];
        }

        public void Clear()
        {
            particles = new ParticleType[WIDTH, HEIGHT];
        }

        public void DrawOn(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.Black, 0, 0, WIDTH, HEIGHT);
            for (int y = 0; y < HEIGHT; y++)
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    var p = GetParticle(x, y);
                    switch (p)
                    {
                        case ParticleType.SAND:
                            graphics.FillRectangle(Brushes.Yellow, x, y, 1, 1);
                            break;
                        case ParticleType.WATER:
                            graphics.FillRectangle(Brushes.Blue, x, y, 1, 1);
                            break;
                        case ParticleType.WOOD:
                            graphics.FillRectangle(Brushes.Brown, x, y, 1, 1);
                            break;
                        default: break;
                    }
                }
            }
        }
    }
}
