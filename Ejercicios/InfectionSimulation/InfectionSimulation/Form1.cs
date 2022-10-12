using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InfectionSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const int scale = 2;
        private World world = new World();
        private long frameCount = 0;
        private long frameTime = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeWorld();
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint,
                true);
        }

        private void InitializeWorld()
        {
            for (int i = 0; i < 5000; i++)
            {
                Person entity = new Person();
                entity.Infected = i == 0;
                entity.Rotation = world.Random() * Math.PI * 2;
                entity.Position = world.RandomPoint();
                world.Add(entity);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(scale, scale);
            world.DrawOn(e.Graphics);
        }


        private void updateTimer_Tick(object sender, EventArgs e)
        {
            ClientSize = new Size(world.Width * scale, world.Height * scale);
            long begin = Environment.TickCount;
            world.Update();
            Refresh();
            long end = Environment.TickCount;
            RegisterFrameTime(end - begin);

            Text = string.Format("Objects: {0}, Average FPS: {1:00}, Current FPS: {2:00}",
                world.GameObjects.Count(),
                1000.0 / (frameTime / frameCount),
                1000.0 / (end - begin));
        }

        private void RegisterFrameTime(long time)
        {
            frameTime += time;
            frameCount++;
        }

        private void lifeSpawner_Tick(object sender, EventArgs e)
        {
            IEnumerable<Person> persons = world.GameObjects.Cast<Person>();
            if (persons.All(p => p.Infected))
            {
                foreach (Person p in persons)
                {
                    p.Infected = false;
                }
                persons.First().Infected = true;
            }
        }
    }
}
