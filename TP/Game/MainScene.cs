using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Engine;
using Engine.Utils;
using Engine.Profiling;

namespace Game
{
    public partial class MainScene : Form
    {
        public MainScene()
        {
            InitializeComponent();
        }

        long startTime = Environment.TickCount;

        private void Form1_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            GameObject world = scene.World;

            world.AddChild(new StarSpawner());

            EnemySpawner[] spawners = new EnemySpawner[]
            {
                new EnemySpawner(0,  500, new FuncBehavior(x => Math.Sin(x * 10) * 0.9, 175)),
                new EnemySpawner(11, 500, new FollowPlayerBehavior(200)),
                new EnemySpawner(29, 500, new FuncBehavior(x => -0.9 * (2 / Math.PI) * Math.Asin(Math.Sin(Math.PI * x * 3)), 250)),
                new EnemySpawner(20, 500, new FlockingBehavior(200)),
                new EnemySpawner(42, 500, new FuncBehavior(x => (Math.Sin(x * 10) + Math.Sin(x * 5)) * 0.5, 275)),
                new EnemySpawner(63, 500, new FuncBehavior(x => Math.Atan(x * 10 - 5) * -0.5, 200)),
                new EnemySpawner(54, 500, new FuncBehavior(x => (Math.Sin((x + 1) * 10 + 15) - Math.Sin((x + 1) * 15)) * 0.2 - 0.4, 300)),
            };
            world.AddChildren(spawners);
            world.AddChild(new EnemySpawnerDirector(spawners));

            PlayerShip player = new PlayerShip(33);
            player.CenterY = world.CenterY;
            player.Left = world.Left + 100;
            world.AddChild(player);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Tally tally = scene.Tally;
            Text = string.Format("Time: {8} ms - Frames: {6} - Update: current({0:0.00} ms), avg({1:0.00} ms), max({2:0.00} ms) - Drawing: current({3:0.00} ms), avg({4:0.00} ms), max({5:0.00} ms) - Instances: {7}",
                tally.UpdateDurations.LastOrDefault(), tally.AverageUpdate, tally.MaxUpdate,
                tally.DrawDurations.LastOrDefault(), tally.AverageDraw, tally.MaxDraw,
                tally.Count, tally.InstanceCounter.LastOrDefault(), Environment.TickCount - startTime);
        }
    }
}
