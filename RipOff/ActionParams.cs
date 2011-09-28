namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ActionParams
    {
        public ActionParams()
        {
            this.A = false;
            this.D = false;
            this.J = false;
            this.L = false;
            this.N = false;
        }

        public bool A { get; set; } // rotate left
        public bool D { get; set; } // rotate right
        public bool J { get; set; } // move backwards
        public bool L { get; set; } // move forwards
        public bool N { get; set; } // shoot
    }
}
