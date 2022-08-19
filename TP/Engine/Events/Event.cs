using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Events
{
    public abstract class Event
    {
        private bool handled = false;

        public bool Handled
        {
            get { return handled; }
            set { handled = value; }
        }

        internal abstract void HandledBy(EventHandler evtHandler);
        public abstract bool Accepts(GameObject gameObj);
    }
}
