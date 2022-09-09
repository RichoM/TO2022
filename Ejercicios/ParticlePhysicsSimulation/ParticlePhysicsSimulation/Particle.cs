using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticlePhysicsSimulation
{
    public abstract class Particle
    {
        public int X { get; set; }
        public int Y { get; set; }

        public abstract Color Color { get; }

        public virtual void Step(Simulation sim) { /* Do nothing */ }
    }

    public class WoodParticle : Particle
    {
        public override Color Color => Color.Brown;
    }

    public class SandParticle : Particle
    {
        public override Color Color => Color.Yellow;

        public override void Step(Simulation sim)
        {
            if (sim.TryToMove(X, Y, X, Y + 1)) return;
            if (sim.TryToMove(X, Y, X - 1, Y + 1)) return;
            if (sim.TryToMove(X, Y, X + 1, Y + 1)) return;
        }
    }


    public class WaterParticle : Particle
    {
        public override Color Color => Color.Blue;

        public override void Step(Simulation sim)
        {
            if (sim.TryToMove(X, Y, X, Y + 1)) return;
            if (sim.TryToMove(X, Y, X - 1, Y + 1)) return;
            if (sim.TryToMove(X, Y, X + 1, Y + 1)) return;

            if (sim.TryToMove(X, Y, X - 1, Y)) return;
            if (sim.TryToMove(X, Y, X + 1, Y)) return;
        }
    }
}
