using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;

namespace Game
{
    public class RapidFire : GameObject
    {
        private bool firstFrame = true;
        private int duration;
        private int startTime;

        public RapidFire(int duration = 2500)
        {
            this.duration = duration;
            startTime = Environment.TickCount;
        }

        public override void Update(float deltaTime)
        {
            PlayerShip player = Parent as PlayerShip;
            if (firstFrame)
            {
                firstFrame = false;
                if (player != null) { player.Cannon.ShotInterval = 0; }
            }
            else if (Environment.TickCount - startTime > duration)
            {
                if (player != null) { player.Cannon.DefaultShotInterval(); }
                Delete();
            }
        }
    }
}
