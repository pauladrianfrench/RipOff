namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IMission
    {
        Entity Target { get; set; }
        MatrixPoint EndPoint { get; set; }
        bool Complete { get; set; }
    }
}
