using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Profiling
{
    public class TallyGraph : GameObject
    {
        private Tally tally;
        private PointF mousePos;
        private int selected;
        
        public TallyGraph(Tally tally)
        {
            this.tally = tally;

            EventHandler.MouseMove += evt =>
            {
                mousePos = evt.Position;
            };
        }

        public int Selected
        {
            get
            {
                return selected;
            }
        }

        public override void DrawOn(Graphics graphics)
        {
            base.DrawOn(graphics);
            graphics.FillRectangle(Brushes.White, Bounds);
            if (tally.InstanceCounter.Count == 0) return;

            float instanceMultiplier = Height / tally.InstanceCounter.Max();
            float maxUpdate = tally.MaxUpdate;
            float maxDraw = tally.MaxDraw;
            float updateMultiplier = Height / (maxUpdate > maxDraw ? maxUpdate : maxDraw);
            float drawMultiplier = Height / (maxUpdate > maxDraw ? maxUpdate : maxDraw);
            List<PointF> instancePoints = new List<PointF>();
            List<PointF> updatePoints = new List<PointF>();
            List<PointF> drawPoints = new List<PointF>();
            for (int i = 0; i < tally.Count; i++)
            {
                float x = i / ((float)tally.Count - 1) * Width;
                graphics.DrawLine(Pens.LightGray, x, 0, x, Height);
                if (i > 0)
                {
                    float x1 = (i - 1) / ((float)tally.Count - 1) * Width;
                    if (Math.Abs(mousePos.X - x1) < (x - x1) / 2)
                    {
                        graphics.DrawLine(Pens.Black, x1, 0, x1, Height);
                        selected = i - 1;
                    }
                }
                
                float instanceY = Height - tally.InstanceCounter.ElementAtOrDefault(i) * instanceMultiplier;
                instancePoints.Add(new PointF(x, instanceY));
                
                float updateY = Height - tally.UpdateDurations.ElementAtOrDefault(i) * updateMultiplier;
                updatePoints.Add(new PointF(x, updateY));

                float drawY = Height - tally.DrawDurations.ElementAtOrDefault(i) * drawMultiplier;
                drawPoints.Add(new PointF(x, drawY));
            }
            graphics.DrawLines(Pens.Green, instancePoints.ToArray());
            graphics.DrawLines(Pens.Red, updatePoints.ToArray());
            graphics.DrawLines(Pens.Blue, drawPoints.ToArray());

            Font font = new Font(FontFamily.GenericSansSerif, 10);
            graphics.FillRectangle(Brushes.Red, new Rectangle(10, 10, 10, 10));
            graphics.DrawString("Update", font, Brushes.Black, new Point(25, 8));
            graphics.FillRectangle(Brushes.Blue, new Rectangle(10, 30, 10, 10));
            graphics.DrawString("Draw", font, Brushes.Black, new Point(25, 28));
            graphics.FillRectangle(Brushes.Green, new Rectangle(10, 50, 10, 10));
            graphics.DrawString("Instances", font, Brushes.Black, new Point(25, 48));
        }
    }
}
