namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using PaulMath;
    
    public class MatrixPoint
    {
        public MatrixPoint(double x, double y)
        {
            this.Matrix = new Matrix(new double[] {x,y}, 2,1);
        }

        public Matrix Matrix{ get; set; }
        public int X { get { return (int)(Math.Round(this.Matrix.GetValue(1, 1)));} }
        public int Y { get { return (int)(Math.Round(this.Matrix.GetValue(2, 1)));} }

        public double Xd { get { return this.Matrix.GetValue(1, 1);  } }
        public double Yd { get { return this.Matrix.GetValue(2, 1); } }

        public static MatrixPoint operator + (MatrixPoint lhs, MatrixPoint rhs)
        {
            return new MatrixPoint
                        (
                            lhs.Matrix.GetValue(1, 1) + rhs.Matrix.GetValue(1, 1),
                            lhs.Matrix.GetValue(2, 1) + rhs.Matrix.GetValue(2, 1)
                        );
        }

        public static MatrixPoint operator -(MatrixPoint lhs, MatrixPoint rhs)
        {
            return new MatrixPoint(lhs.Matrix.GetValue(1, 1) - rhs.Matrix.GetValue(1, 1),
                                   lhs.Matrix.GetValue(2, 1) - rhs.Matrix.GetValue(2, 1));
        }

        public static double DistanceBetween(MatrixPoint p1, MatrixPoint p2)
        {
            double rise = p2.Yd - p1.Yd;
            double run = p2.Xd - p1.Xd;
            return Math.Sqrt(rise * rise + run * run);
        }

        public static Angle OrientationBetween(MatrixPoint p1, MatrixPoint p2)
        {
            MatrixPoint p = p2 - p1;
           
            double rise = p.Yd;
            double run = p.Xd;

            if (rise != 0.0 && run != 0.0)
            {
                if (rise > 0 && run > 0)
                {
                    return new Angle(Math.Atan(run / rise));
                }
                else if (rise < 0 && run > 0)
                {
                    return new Angle(Math.PI + Math.Atan(run / rise));
                }
                else if (rise < 0 && run < 0)
                {
                    return new Angle(Math.PI + Math.Atan(run / rise));
                }
                else if (rise > 0 && run < 0)
                {
                    return new Angle((2 * Math.PI) + Math.Atan(run / rise));
                }
            }
            else
            {
                if (rise == 0 && run > 0)
                {
                    return new Angle(Math.PI/2);
                }
                else if (rise < 0 && run == 0)
                {
                    return new Angle(Math.PI);
                }
                else if (rise == 0 && run < 0)
                {
                    return new Angle((Math.PI / 2) + Math.PI);
                }
                else if (rise > 0 && run == 0)
                {
                    return new Angle(0);
                }
            }
            return new Angle();
        }
    }
}
