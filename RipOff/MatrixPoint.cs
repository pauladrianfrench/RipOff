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
        public int X { get { return (int)(Math.Round(this.Matrix.GetValue(1,1)));} }
        public int Y { get { return (int)(Math.Round(this.Matrix.GetValue(2,1)));} }

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
            return new MatrixPoint
                (
                lhs.Matrix.GetValue(1, 1) - rhs.Matrix.GetValue(1, 1),
                lhs.Matrix.GetValue(2, 1) - rhs.Matrix.GetValue(2, 1)
                );
        }
    }
}
