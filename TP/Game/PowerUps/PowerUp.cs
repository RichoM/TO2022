using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;
using System.Drawing;

namespace Game
{
    public class PowerUp : GameObject
    {
        private static Random rnd = new Random();
        private Image img;
        private List<Func<GameObject>> powerUps = new List<Func<GameObject>>();

        public PowerUp()
        {
            img = Properties.Resources.powerup;
            Extent = img.Size;

            powerUps.Add(() => new RotatingCannon(5000));
            powerUps.Add(() => new SideCannons(5000));
            powerUps.Add(() => new Shield(5000));
            powerUps.Add(() => new SpeedUp(5000));
            powerUps.Add(() => new RapidFire(5000));
        }

        public override void Update(float deltaTime)
        {
            X += -150 * deltaTime;
        }

        public void ApplyOn(PlayerShip ship)
        {
            GameObject powerUp = ChoosePowerUp(ship);
            powerUp.Center = ship.Center;
            ship.AddChild(powerUp);
            Play(Properties.Resources.start);
            Delete();
        }

        private GameObject ChoosePowerUp(PlayerShip ship)
        {
            int attempts = 0;
            GameObject result = null;
            do
            {
                attempts++;
                result = powerUps[rnd.Next(powerUps.Count)]();
                if (ship.AllChildren.Any((m) => m.GetType().Equals(result.GetType())))
                {
                    result = null;
                }
            } while (result == null && attempts < 100);
            if (result == null)
            {
                // If we reach here, we probably have all the powerups,
                // in which case, simply add the first power up on the list
                result = powerUps[0]();
            }
            return result;
        }

        public override void DrawOn(Graphics graphics)
        {
            graphics.DrawImage(img, Position);
        }
    }
}
