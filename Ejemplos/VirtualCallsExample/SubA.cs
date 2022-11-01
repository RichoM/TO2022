using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCallsExample
{
    public class SubA : Base
    {
        public override void UpdateVirtual()
        {
            Name = "SubA";
            Counter++;
        }
    }
}
