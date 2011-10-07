using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    public interface IMovingEntity : IEntity
    {
        ProximityResult DetectProximity(IEntity other);
        Line CentreLine { get; }
        Angle Orientation { get; }
    }
}
