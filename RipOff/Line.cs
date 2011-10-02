
namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;

    public class Line
    {
        double rise;
        double run;
        MatrixPoint point1;
        MatrixPoint point2;
        
        public Line(MatrixPoint p1, MatrixPoint p2)
        {
            point1 = p1;
            point2 = p2;

            rise = RightPoint.Yd - LeftPoint.Yd; 
            run = RightPoint.Xd - LeftPoint.Xd;
        }

        public Line(Line l)
        {
            this.point1 = l.point1;
            this.point2 = l.point2;

            rise = RightPoint.Yd - LeftPoint.Yd;
            run = RightPoint.Xd - LeftPoint.Xd;
        }

        public MatrixPoint Point1 { get { return point1; } set { point1 = value; rise = RightPoint.Yd - LeftPoint.Yd; run = RightPoint.Xd - LeftPoint.Xd; } }
        public MatrixPoint Point2 { get { return point2; } set { point2 = value; rise = RightPoint.Yd - LeftPoint.Yd; run = RightPoint.Xd - LeftPoint.Xd; } }
        
        public MatrixPoint LeftPoint
        {
            get
            {
                return (point1.Xd <= point2.Xd) ? point1 : point2; 
            }
        }
        public MatrixPoint RightPoint
        {
            get
            {
                return (point1.Xd > point2.Xd) ? point1 : point2;
            }
        }
        public MatrixPoint HighPoint
        {
            get
            {
                return (point1.Yd > point2.Yd) ? point1 : point2;
            }
        }
        public MatrixPoint LowPoint
        {
            get
            {
                return (point1.Yd <= point2.Yd) ? point1 : point2;
            }
        }

        public bool IsVertical { get { return run == 0; } }
        public bool IsHorizontal { get { return rise == 0; } }
 

        public double Length { get { return Math.Sqrt(rise * rise + run * run); } }

        public bool Intersects(Line line)
        {
            if (this.IsVertical && line.IsVertical)
            {
                return false;
            }
            else if (this.IsVertical)
            {
                return Intersects(this, line);
            }
            else if (line.IsVertical)
            {
                return Intersects(line, this);
            }
            else
            {
                double  this_m = this.rise / this.run;
                double  line_m = line.rise / line.run;
            
                if (this_m == line_m)
                {
                    return false;
                }

                double this_c = this.point1.Yd - this_m * this.point1.Xd;
                double line_c = line.point1.Yd - line_m * line.point1.Xd;

                double intersect_x = (this_c - line_c) / (line_m - this_m);
           
                if(intersect_x >= this.LeftPoint.Xd && intersect_x <= this.RightPoint.Xd &&
                    intersect_x >= line.LeftPoint.Xd && intersect_x <= line.RightPoint.Xd)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool Intersects(Line vert, Line line)
        {
            double line_m = line.rise / line.run;
            double line_c = line.point1.Yd - line_m * line.point1.Xd;
            double line_y = line_m * vert.point1.Xd + line_c;

            if (line_y >= vert.LowPoint.Yd && line_y <= vert.HighPoint.Yd)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
