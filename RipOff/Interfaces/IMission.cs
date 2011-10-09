namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IMission
    {
        List<IMissionTarget> Targets { get; set; }
        bool Complete { get; }
        IMissionTarget GetNextUncompletedTarget();
    }
}
