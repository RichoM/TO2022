using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine.Extensions;

namespace Game
{
    public class FollowPlayerBehavior : EnemyBehavior
    {
        private static Random rnd = new Random();
        float speed;

        public FollowPlayerBehavior(float speed = 200)
        {
            this.speed = speed;
        }

        public override void Update(EnemyShip ship, float deltaTime)
        {
            if (!ship.Visible)
            {
                ship.CenterY = rnd.Next(ship.Root.Top.FloorToInt(), ship.Root.Bottom.FloorToInt());
            }

            ship.CenterX -= speed * deltaTime;

            if (ship.Player != null)
            {
                double multiplier = Math.Abs(ship.Player.X - ship.X) / (ship.Root.Width);
                if (ship.Player.Top > ship.CenterY)
                {
                    ship.CenterY += ((float)(speed * multiplier * deltaTime));
                }
                else if (ship.Player.Bottom < ship.CenterY)
                {
                    ship.CenterY -= ((float)(speed * multiplier * deltaTime));
                }
            }
        }
    }
}
