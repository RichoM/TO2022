using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;
using Engine.Extensions;

namespace Game
{
    public class Explosion : GameObject
    {
        private static Random rnd = new Random();

        public static void Burst(GameObject world, PointF point, 
            int amount = 2000, 
            int minMagnitude = 50,
            int maxMagnitude = 300,
            float initialSize = 2,
            float deltaSize = 2,
            float deltaAlpha = -2)
        {
            for (int i = 0; i < amount; i++)
            {
                float angle = rnd.Next(0, 360);
                float magnitude = rnd.Next(minMagnitude, maxMagnitude);
                float o = (float)Math.Sin(angle) * magnitude;
                float a = (float)Math.Cos(angle) * magnitude;
                GameObject explosion = new Explosion(new PointF(a, o), initialSize, deltaSize, deltaAlpha);
                explosion.Center = point;
                world.AddChild(explosion);
            }
            world.Play(rnd.NextDouble() > 0.5 ?
                Properties.Resources.explosion1 :
                Properties.Resources.explosion2);
        }

        private float alpha = 1;

        private PointF speed;
        private float deltaSize;
        private float deltaAlpha;

        private Explosion(PointF speed, 
            float initialSize = 2, 
            float deltaSize = 2, 
            float deltaAlpha = -2)
        {
            this.speed = speed;
            this.deltaSize = deltaSize;
            this.deltaAlpha = deltaAlpha;

            Extent = new SizeF(initialSize, initialSize);
        }

        public override void Update(float deltaTime)
        {
            PointF center = Center;
            Width += deltaSize * deltaTime;
            Height += deltaSize * deltaTime;
            Center = center;
            
            alpha += deltaAlpha * deltaTime;
            X += speed.X * deltaTime;
            Y += speed.Y * deltaTime;
        }

        public override void DrawOn(Graphics graphics)
        {
            int a = (alpha * 255).RoundedToInt().MinMax(255, 0);
            int g = (255 - a).MinMax(200, 50);
            Pen pen = new Pen(Color.FromArgb(a, 255, g, 0));
            graphics.FillRectangle(pen.Brush, Bounds);
        }
    }
}
