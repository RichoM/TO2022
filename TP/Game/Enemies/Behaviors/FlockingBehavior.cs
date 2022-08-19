using Engine;
using Engine.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class FlockingBehavior : EnemyBehavior
    {
        float speed;

        public FlockingBehavior(float speed = 200)
        {
            this.speed = speed;
        }

        public override void Update(EnemyShip ship, float deltaTime)
        {
            ship.CenterX -= speed * deltaTime;

            GameObject target = null;
            if (IndexInFlock(ship) == 0)
            {
                target = ship.Player;
            }
            else
            {
                target = Flock(ship).ElementAtOrDefault(IndexInFlock(ship) - 1);
            }

            if (target != null)
            {
                PointF targetPos = target.Center;
                if (IndexInFlock(ship) > 0)
                {
                    float increment = IndexInFlock(ship) * 100 * (IndexInFlock(ship) % 2 == 0 ? -1 : 1);
                    targetPos = new PointF(targetPos.X, targetPos.Y + increment);
                }

                float diff = targetPos.Y - ship.CenterY;
                ship.CenterY += (speed * 5 / ship.Root.Height) * diff * deltaTime;
            }
        }

        protected override IEnumerable<EnemyShip> Flock (EnemyShip ship)
        {
            return base.Flock(ship)
                .OrderBy(s => s.X)
                .Where(s => s.Left > (s.Player != null ? s.Player.Left : 0));
        }

        private int IndexInFlock(EnemyShip ship)
        {
            int index = -1;
            for (int i = 0; i < Flock(ship).Count(); i++)
            {
                if (ship.Equals(Flock(ship).ElementAt(i)))
                {
                    index = i;
                } 
            }
            return index;
        }
    }
}
