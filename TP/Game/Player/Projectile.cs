using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Engine;
using Engine.Extensions;

namespace Game
{
    public class Projectile : GameObject
    {
        private Image img;
        private float speed = 700;

        public Projectile()
        {
            img = Properties.Resources.projectile;

            Extent = img.Size;
        }

        public override void Update(float deltaTime)
        {
            X += speed * deltaTime;
            
            CheckForCollision();
        }

        private void CheckForCollision()
        {
            IEnumerable<EnemyShip> collisions = AllObjects
                .Where((m) => CollidesWith(m))
                .Select((m) => m as EnemyShip);
            foreach (EnemyShip enemy in collisions)
            {
                if (enemy != null)
                {
                    enemy.Explode();
                    Delete();
                }
            }
        }

        public override void DrawOn(Graphics graphics)
        {
            graphics.DrawImage(img, Position);
        }
    }
}
