using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine.Events
{
    public class KeyUpEvent : KeyboardEvent
    {
        public KeyUpEvent(Keys keyData) : base(keyData) { }

        internal override void HandledBy(EventHandler evtHandler)
        {
            evtHandler.HandleKeyUp(this);
        }
    }
}
