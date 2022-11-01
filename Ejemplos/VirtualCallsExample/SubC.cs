using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualCallsExample
{
    public class SubC : Base, IBase
    {
        public override void UpdateVirtual()
        {
            Name = "SubC";
            Counter++;
        }
    }
}
