using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine.Extensions;
using Engine;

namespace Game
{
    public class StarSpawner : GameObject
    {
        private Random rnd = new Random();
        private bool firstFrame = true;

        public override void Update(float deltaTime)
        {
            if (firstFrame)
            {
                firstFrame = false;
                FillSpace(1000);
            }

            Left = Parent.Right;
            for (int i = 0; i < 200 * deltaTime; i++)
            {
                SpawnStar();
            }
        }

        private void FillSpace(int numberOfStars)
        {
            for (int i = 0; i < numberOfStars; i++)
            {
                CenterX = rnd.Next(Parent.Left.RoundedToInt(), Parent.Right.RoundedToInt());
                SpawnStar();
            }
        }

        public void SpawnStar()
        {
            Star star = new Star(Properties.Resources.star, rnd.Next(300));
            star.Center = Center;
            Parent.AddChildBack(star);

            CenterY = rnd.Next(Parent.Top.RoundedToInt(), Parent.Bottom.RoundedToInt());
        }

    }
}
