using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Events
{
    public abstract class MouseEvent : Event
    {
        private PointF position;

        public PointF Position { get { return position; } }

        public MouseEvent(PointF position)
        {
            this.position = position;
        }

        public override bool Accepts(GameObject gameObj)
        {
            return gameObj.Bounds.Contains(position);
        }
    }
}
