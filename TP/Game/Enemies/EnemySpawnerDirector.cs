using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;

namespace Game
{
    public class EnemySpawnerDirector : GameObject
    {
        private EnemySpawner[] spawners;

        private int iteration = 0;
        private int lastStep = 0;

        public EnemySpawnerDirector(EnemySpawner[] spawners)
        {
            this.spawners = spawners;
            lastStep = Environment.TickCount;
        }

        public override void Update(float deltaTime)
        {
            int now = Environment.TickCount;
            if (now - lastStep > 4000)
            {
                DeactivateAll();
            }
            if (now - lastStep > 5000)
            {
                lastStep = now;
                NextIteration();
            }
        }

        private void DeactivateAll()
        {
            foreach (EnemySpawner spawner in spawners)
            {
                spawner.Active = false;
            }
        }

        private void NextIteration()
        {
            iteration++;
            if (iteration % (1 << spawners.Length) == 0)
            {
                // Overflow
                foreach (EnemySpawner spawner in spawners)
                {
                    spawner.EmmisionInterval -= 50;
                    if (spawner.EmmisionInterval < 100)
                    {
                        spawner.EmmisionInterval = 100;
                    }
                }
            }

            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].Active = (iteration & (1 << i)) > 0;
            }
        }
    }
}
