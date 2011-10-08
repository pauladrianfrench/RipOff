using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    public class Mission : IMission
    {
        public Entity Target { get; set; }
        public MatrixPoint EndPoint { get; set; }
        public bool Complete { get; set; }
    }
}
