using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    public class FuelCell : Entity
    {
        public FuelCell(GameArea ga)
            : base(ga)
	    {
            this.Outline.Add(new Line(new MatrixPoint(-3, -1), new MatrixPoint(-3, 2)));
            this.Outline.Add(new Line(new MatrixPoint(-3, 1), new MatrixPoint(0, 4)));
            this.Outline.Add(new Line(new MatrixPoint(0, 4), new MatrixPoint(3, 1)));
            this.Outline.Add(new Line(new MatrixPoint(3, 1), new MatrixPoint(3, -1)));
            this.Outline.Add(new Line(new MatrixPoint(3, -1), new MatrixPoint(0, -4)));
            this.Outline.Add(new Line(new MatrixPoint(0, -4), new MatrixPoint(-3, -1)));

            this.Outline.Add(new Line(new MatrixPoint(-5, -2), new MatrixPoint(-5, 2)));
            this.Outline.Add(new Line(new MatrixPoint(-5, 2), new MatrixPoint(0, 6)));
            this.Outline.Add(new Line(new MatrixPoint(0, 6), new MatrixPoint(5, 2)));
            this.Outline.Add(new Line(new MatrixPoint(5, 2), new MatrixPoint(5, -2)));
            this.Outline.Add(new Line(new MatrixPoint(5, -2), new MatrixPoint(0, -6)));
            this.Outline.Add(new Line(new MatrixPoint(0, -6), new MatrixPoint(-5, -2)));
	    }        
    }
}
