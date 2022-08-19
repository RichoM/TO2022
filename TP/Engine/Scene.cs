using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Engine.Events;
using Engine.Profiling;

namespace Engine
{
    public partial class Scene : UserControl
    {
        public Scene()
        {
            InitializeComponent();
        }

        private Tally tally = new Tally();
        private GameObject world = new GameObject();
        private float lastStep = -1;
        private PointF cursorPosition;

        public Tally Tally { get { return tally; } }
        public GameObject World { get { return world; } }

        public PointF CursorPosition
        {
            get { return cursorPosition; }
        }

        private void Scene_Load(object sender, EventArgs e)
        {
            ResizeWorld();
            steppingTimer.Enabled = true;
            lastStep = Environment.TickCount;
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.UserPaint
                | ControlStyles.AllPaintingInWmPaint,
                true);
        }

        private void Scene_Paint(object sender, PaintEventArgs e)
        {
            tally.RegisterDraw();
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            world.FullDrawOn(e.Graphics);
        }

        private void steppingTimer_Tick(object sender, EventArgs e)
        {
            float now = Environment.TickCount;
            float delta = (now - lastStep) / 1000;
            if (delta > 0)
            {
                tally.RegisterUpdate();
                tally.RegisterInstances(world.AllChildren.LongCount());
                world.FullUpdate(delta, true);
                lastStep = now;
                Refresh();
            }
        }

        private void Scene_Resize(object sender, EventArgs e)
        {
            ResizeWorld();
        }

        private void ResizeWorld()
        {
            world.Bounds = new Rectangle(0, 0, Width, Height);
        }

        private void Scene_MouseDown(object sender, MouseEventArgs e)
        {
            world.HandleEvent(new MouseDownEvent(e.Location));
        }

        private void Scene_MouseUp(object sender, MouseEventArgs e)
        {
            world.HandleEvent(new MouseUpEvent(e.Location));
        }

        private void Scene_MouseMove(object sender, MouseEventArgs e)
        {
            world.HandleEvent(new MouseMoveEvent(cursorPosition, e.Location));
            cursorPosition = e.Location;
        }

        private void Scene_KeyDown(object sender, KeyEventArgs e)
        {
            world.HandleEvent(new KeyDownEvent(e.KeyData));
        }

        private void Scene_KeyUp(object sender, KeyEventArgs e)
        {
            world.HandleEvent(new KeyUpEvent(e.KeyData));
        }

        /**
        HACK(Richo):
        This code was taken from: http://stackoverflow.com/a/5606692
        For some reason, the Arrow keys didn't fire the KeyDown/KeyUp events.
        */
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }
    }
}
