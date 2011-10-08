namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IMission
    {
        IEntity Target { get; set; }
        IEntity EndPoint { get; set; }
        bool Complete { get; set; }
    }
}
