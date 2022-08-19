using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine.Events
{
    public class KeyDownEvent : KeyboardEvent
    {
        public KeyDownEvent(Keys keyData) : base(keyData) {}

        internal override void HandledBy(EventHandler evtHandler)
        {
            evtHandler.HandleKeyDown(this);
        }
    }
}
