using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;
using System.Drawing;

namespace Game
{
    public class RotatingCannon : GameObject
    {
        private int duration;
        private int startTime;

        private Cannon cannon;

        private float angle;

        public RotatingCannon(int duration = 2500)
        {
            this.duration = duration;
            startTime = Environment.TickCount;
            angle = 0;

            cannon = new Cannon();
            cannon.ShotInterval = 150;
            AddChild(cannon);
        }

        public override void Update(float deltaTime)
        {
            angle += -1 * deltaTime;
            MoveCannon(cannon, angle);

            if (Environment.TickCount - startTime > duration)
            {
                Delete();
            }
        }

        private void MoveCannon(Cannon cannon, float angle)
        {
            float o = (float)Math.Sin(angle) * 75;
            float a = (float)Math.Cos(angle) * 75;
            cannon.Center = new PointF(CenterX + a, CenterY + o);
        }
    }
}
