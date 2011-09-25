namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class Trans
    {
        public int XScale { get; set; }
        public int YScale { get; set; }
        public Point Origin { get; set; }

        public int ScaleX(int x)
        {
            return x * XScale;
        }

        public int ScaleY(int y)
        {
            return y * YScale;
        }

        public Point TransPoint(Point p)
        {
            p.X *= XScale;
            p.Y *= YScale;

            p.X += Origin.X;
            p.Y = Origin.Y - p.Y;

            return p;
        }
    }
}
