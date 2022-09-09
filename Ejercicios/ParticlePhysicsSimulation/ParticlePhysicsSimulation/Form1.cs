using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParticlePhysicsSimulation
{
    public partial class Form1 : Form
    {
        enum ParticleType
        {
            SAND, WATER, WOOD
        }

        const int scale = 3;
        Simulation sim = new Simulation();
        Mouse mouse = new Mouse();
        Stopwatch stopwatch = new Stopwatch();
        ParticleType selectedParticle = ParticleType.SAND;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(Simulation.WIDTH * scale, Simulation.HEIGHT * scale);
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint,
                true);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.ScaleTransform(scale, scale);
            sim.DrawOn(e.Graphics);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            mouse.position = e.Location;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouse.leftClick = e.Button == MouseButtons.Left;
            mouse.rightClick = e.Button == MouseButtons.Right;
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouse.leftClick = mouse.rightClick = false;
        }

        struct Mouse
        {
            public bool leftClick;
            public bool rightClick;
            public Point position;
        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            if (mouse.leftClick)
            {
                AddPatch(mouse.position, selectedParticle);
            }
            else if (mouse.rightClick)
            {
                RemovePatch(mouse.position);
            }
            stopwatch.Restart();
            sim.Step();
            Refresh();
            stopwatch.Stop();
            Text = $"{selectedParticle}, {stopwatch.ElapsedMilliseconds} ms";
        }

        private void AddPatch(Point pos, ParticleType type)
        {
            int amount = 10;
            for (int x = pos.X - amount; x < pos.X + amount; x++)
            {
                for (int y = pos.Y - amount; y < pos.Y + amount; y++)
                {
                    Particle particle = null;
                    switch (type)
                    {
                        case ParticleType.SAND: particle = new SandParticle(); break;
                        case ParticleType.WATER: particle = new WaterParticle(); break;
                        case ParticleType.WOOD: particle = new WoodParticle(); break;
                    }
                    sim.AddParticle(x / scale, y / scale, particle);
                }
            }
        }


        private void RemovePatch(Point pos)
        {
            int amount = 10;
            for (int x = pos.X - amount; x < pos.X + amount; x++)
            {
                for (int y = pos.Y - amount; y < pos.Y + amount; y++)
                {
                    sim.RemoveParticle(x / scale, y / scale);
                }
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '1')
            {
                selectedParticle = ParticleType.SAND;
            }
            else if (e.KeyChar == '2')
            {
                selectedParticle = ParticleType.WATER;
            }
            else if (e.KeyChar == '3')
            {
                selectedParticle = ParticleType.WOOD;
            }
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            sim.Clear();
        }
    }
}
