using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    public class FuelCell : Entity
    {
        List<Line> perimeter;
 
        public FuelCell(GameController ga)
            : base(ga)
	    {
            this.Outline.Add(new Line(new MatrixPoint(-3, -1), new MatrixPoint(-3, 2)));
            this.Outline.Add(new Line(new MatrixPoint(-3, 1), new MatrixPoint(0, 4)));
            this.Outline.Add(new Line(new MatrixPoint(0, 4), new MatrixPoint(3, 1)));
            this.Outline.Add(new Line(new MatrixPoint(3, 1), new MatrixPoint(3, -1)));
            this.Outline.Add(new Line(new MatrixPoint(3, -1), new MatrixPoint(0, -4)));
            this.Outline.Add(new Line(new MatrixPoint(0, -4), new MatrixPoint(-3, -1)));

            perimeter = new List<Line>();

            for (int i = 0; i < Outline.Count; i++)
            {
                perimeter.Add(Outline[i]);
            }

            this.Outline.Add(new Line(new MatrixPoint(-5, -2), new MatrixPoint(-5, 2)));
            this.Outline.Add(new Line(new MatrixPoint(-5, 2), new MatrixPoint(0, 6)));
            this.Outline.Add(new Line(new MatrixPoint(0, 6), new MatrixPoint(5, 2)));
            this.Outline.Add(new Line(new MatrixPoint(5, 2), new MatrixPoint(5, -2)));
            this.Outline.Add(new Line(new MatrixPoint(5, -2), new MatrixPoint(0, -6)));
            this.Outline.Add(new Line(new MatrixPoint(0, -6), new MatrixPoint(-5, -2)));
	    }

        public override List<Line> GetPerimeter()
        {
            return perimeter;
        }
    }
}
