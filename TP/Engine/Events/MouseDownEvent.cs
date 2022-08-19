using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Engine.Events
{
    public class MouseDownEvent : MouseEvent
    {
        public MouseDownEvent(PointF position) : base(position) { }

        internal override void HandledBy(EventHandler evtHandler)
        {
            evtHandler.HandleMouseDown(this);
        }
    }
}
