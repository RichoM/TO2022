using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine.Events;

namespace Engine
{
    public class EventHandler
    {
        public event Action<MouseDownEvent> MouseDown;
        public event Action<MouseUpEvent> MouseUp;
        public event Action<MouseMoveEvent> MouseMove;
        public event Action<KeyDownEvent> KeyDown;
        public event Action<KeyUpEvent> KeyUp;
        
        internal void TryToHandle(Event evt)
        {
            evt.HandledBy(this);
        }

        public virtual void HandleMouseDown(MouseDownEvent evt)
        {
            if (MouseDown != null)
            {
                evt.Handled = true;
                MouseDown(evt);
            }
        }

        public virtual void HandleMouseUp(MouseUpEvent evt)
        {
            if (MouseUp != null)
            {
                evt.Handled = true;
                MouseUp(evt);
            }
        }

        public virtual void HandleMouseMove(MouseMoveEvent evt)
        {
            if (MouseMove != null)
            {
                evt.Handled = true;
                MouseMove(evt);
            }
        }

        public virtual void HandleKeyDown(KeyDownEvent evt)
        {
            if (KeyDown != null)
            {
                evt.Handled = true;
                KeyDown(evt);
            }
        }

        public virtual void HandleKeyUp(KeyUpEvent evt)
        {
            if (KeyUp != null)
            {
                evt.Handled = true;
                KeyUp(evt);
            }
        }
    }
}
