namespace RipOff
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ProximityResult
    {
        public IEntity Entity { get; set; }
        public bool Collision { get; set; }
        public Angle Bearing { get; set; }
        public double Distance { get; set; }

        public Heading GetHeading(Angle yourOrientation)
        {
            double rad = (yourOrientation + Bearing).Radians;
            double twoPi = Math.PI * 2;

            if (Math.Round(rad, 5) == 0.0)
            {
                return Heading.Ahead;
            }
            else if (rad < twoPi / 16)
            {
                return Heading.FineAheadRight;
            }
            else if (rad >= 15 * twoPi / 16)
            {
                return Heading.FineAheadLeft;
            }
            else if (rad >= twoPi / 16 && rad < 3 * twoPi / 16)
            {
                return Heading.AheadRight;
            }
            else if (rad >= 3*twoPi / 16 && rad < 5 * twoPi / 16)
            {
                return Heading.Right;
            }
            else if (rad >= 5 * twoPi / 16 && rad < 7 * twoPi / 16)
            {
                return Heading.BehindRight;
            }
            else if (rad >= 7 * twoPi / 16 && rad < 9 * twoPi / 16)
            {
                return Heading.Behind;
            }
            else if (rad >= 9 * twoPi / 16 && rad < 11 * twoPi / 16)
            {
                return Heading.BehindLeft;
            }
            else if (rad >= 11 * twoPi / 16 && rad < 13 * twoPi / 16)
            {
                return Heading.Left;
            }
            else if (rad >= 13 * twoPi / 16 && rad < 15 * twoPi / 16)
            {
                return Heading.AheadLeft;
            }
            return Heading.Unknown;
        }
    }
}
