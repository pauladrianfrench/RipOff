using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    public class Mission : IMission
    {
        public IEntity Target { get; set; }
        public IEntity EndPoint { get; set; }
        public bool Complete { get; set; }
    }
}
