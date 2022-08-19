using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Engine.Events
{
    public abstract class KeyboardEvent : Event
    {
        private Keys keyData;

        public KeyboardEvent(Keys keyData)
        {
            this.keyData = keyData;
        }

        public Keys KeyData
        {
            get { return keyData; }
        }

        public override bool Accepts(GameObject gameObj)
        {
            return true;
        }
    }
}
