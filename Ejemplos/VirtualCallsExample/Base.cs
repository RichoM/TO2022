using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCallsExample
{
    public abstract class Base
    {
        public int Counter = 0;
        public string Name = "";

        public void Update() 
        {
            Name = "Base";
            Counter++;
        }

        public abstract void UpdateVirtual();
    }
}
