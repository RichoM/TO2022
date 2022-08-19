using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class EnemyBehavior
    {
        public abstract void Update(EnemyShip ship, float deltaTime);

        protected virtual IEnumerable<EnemyShip> Flock(EnemyShip ship)
        {
            return ship.AllObjects
                .Select(obj => obj as EnemyShip)
                .Where(obj => obj != null)
                .Where(obj => obj.Behavior.Equals(ship.Behavior));
        }
    }
}
