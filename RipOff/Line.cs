
namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;

    public class Line
    {
        public Line(MatrixPoint p1, MatrixPoint p2)
        {
            Point1 = p1;
            Point2 = p2;
        }
        public Line(Line l)
        {
            this.Point1 = l.Point1; this.Point2 = l.Point2;
        }
        public MatrixPoint Point1 { get; set; }
        public MatrixPoint Point2 { get; set; }

        public bool CrossesOrTouches(Line line)
        {
            
            return false;
        }
    }
}
