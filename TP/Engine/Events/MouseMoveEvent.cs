using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Engine.Events
{
    public class MouseMoveEvent : MouseEvent
    {
        private PointF previousPosition;

        public MouseMoveEvent(PointF previousPosition, PointF currentPosition) : base(currentPosition)
        {
            this.previousPosition = previousPosition;
        }

        public PointF PreviousPosition
        {
            get { return previousPosition; }
        }

        public PointF CurrentPosition
        {
            get { return Position; }
        }

        internal override void HandledBy(EventHandler evtHandler)
        {
            evtHandler.HandleMouseMove(this);
        }
    }
}
