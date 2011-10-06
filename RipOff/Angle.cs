using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    public class Angle
    {
        double rad;
        public Angle()
        {
            rad = 0;
        }

        public Angle(double rad)
        {
            this.rad = rad;
        }

        public double Radians 
        { 
            get 
            {
                if (rad < 0)
                {
                    return (Math.PI * 2) + (rad % (Math.PI * 2));
                }
                else
                {
                    return rad % (Math.PI * 2);
                }
            } 
        }

        public static Angle operator + (Angle a1, Angle a2)
        {
            return new Angle(a1.rad + a2.rad);
        }

        public static Angle operator +(Angle a1, double a2)
        {
            return new Angle(a1.rad + a2);
        }

        public static Angle operator +(double a1, Angle a2)
        {
            return new Angle(a1 + a2.rad);
        }

        public static Angle operator -(Angle a1, Angle a2)
        {
            return new Angle(a1.rad - a2.rad);
        }

        public static Angle operator -(Angle a1, double a2)
        {
            return new Angle(a1.rad - a2);
        }

        public static Angle operator -(double a1, Angle a2)
        {
            return new Angle(a1 - a2.rad);
        }
    }
}
