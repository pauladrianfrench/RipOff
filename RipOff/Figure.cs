﻿

namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;
    using PaulMath;
    
    
    public class Figure
    {
        MatrixPoint centre;
        public Figure()
        {
            this.Outline = new List<Line>();
            
            //body
            this.Outline.Add(new Line(new MatrixPoint(-10, -10), new MatrixPoint(-10, 10)));
            this.Outline.Add(new Line(new MatrixPoint(-10, 10), new MatrixPoint(10, 10)));
            this.Outline.Add(new Line(new MatrixPoint(10, 10), new MatrixPoint(10, -10)));
            this.Outline.Add(new Line(new MatrixPoint(10, -10), new MatrixPoint(-10, -10)));

            //gun
            this.Outline.Add(new Line(new MatrixPoint(-1, 10), new MatrixPoint(-1, 15)));
            this.Outline.Add(new Line(new MatrixPoint(-1, 15), new MatrixPoint(1, 15)));
            this.Outline.Add(new Line(new MatrixPoint(1, 15), new MatrixPoint(1, 10)));

            this.centre = new MatrixPoint(0, 0);

        }

        public List<Line> Outline { get; private set; }
        public MatrixPoint Centre
        {
            get { return centre; }
            set
            {
                MatrixPoint diff = value - centre;
                centre = value;
                foreach (Line l in Outline)
                {
                    l.Point1 += diff;
                    l.Point2 += diff;
                }
            }
        }

        public void Rotate(double rad)
        {
            double [] rot = {Math.Cos(rad) , -Math.Sin(rad), Math.Sin(rad), Math.Cos(rad)};
            Matrix m = new Matrix(rot, 2, 2);
            int len = this.Outline.Count;
            for (int i = 0; i < len; ++i)
            {
                Outline[i].Point1.Matrix = (m * (Outline[i].Point1 - Centre).Matrix);
                Outline[i].Point1 = Outline[i].Point1 + Centre;

                Outline[i].Point2.Matrix = (m * (Outline[i].Point2 - Centre).Matrix);
                Outline[i].Point2 = Outline[i].Point2 + Centre;
            }
        }

        public void Drive(double speed)
        {

            double p1y = Outline[0].Point1.Matrix.GetValue(2, 1);
            double p1x = Outline[0].Point1.Matrix.GetValue(1, 1);

            double rise = p1y - Outline[0].Point2.Matrix.GetValue(2,1);
            double run = p1x - Outline[0].Point2.Matrix.GetValue(1,1);

            double h = Math.Sqrt((rise * rise) + (run * run));
            double factor = speed /h;

            Centre -= new MatrixPoint(factor*run, factor*rise);
         }
    }
}
