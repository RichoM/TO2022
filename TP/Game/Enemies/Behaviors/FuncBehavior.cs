using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class FuncBehavior : EnemyBehavior
    {
        private Func<double, double> function;
        float speed;

        public FuncBehavior(Func<double, double> function, float speed = 200)
        {
            this.function = function;
            this.speed = speed;
        }
        
        public override void Update(EnemyShip ship, float deltaTime)
        {
            ship.X -= speed * deltaTime;
            ship.CenterY = (float)(function(ship.CenterX / ship.Parent.Width) / 2 + 0.5) * ship.Parent.Height;
        }
    }
}
