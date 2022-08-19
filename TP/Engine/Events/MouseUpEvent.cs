using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Engine.Events
{
    public class MouseUpEvent : MouseEvent
    {
        public MouseUpEvent(PointF position) : base(position) { }

        internal override void HandledBy(EventHandler evtHandler)
        {
            evtHandler.HandleMouseUp(this);
        }
    }
}
