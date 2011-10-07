namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public interface IPlayerVehicle : IEntity
    {
        void KeyDown(ActionParams actions);
        void KeyUp(ActionParams actions);
    }
}
