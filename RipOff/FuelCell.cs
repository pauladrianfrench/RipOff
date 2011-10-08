using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    public class FuelCell : MovingEntity
    {
        public FuelCell(GameArea ga)
            : base(ga)
	    {
            this.Outline.Add(new Line(new MatrixPoint(-5, -2), new MatrixPoint(-5, 2)));
            this.Outline.Add(new Line(new MatrixPoint(-5, 2), new MatrixPoint(0, 6)));
            this.Outline.Add(new Line(new MatrixPoint(0, 6), new MatrixPoint(5, 2)));
            this.Outline.Add(new Line(new MatrixPoint(5, 2), new MatrixPoint(5, -2)));
            this.Outline.Add(new Line(new MatrixPoint(5, -2), new MatrixPoint(0, -6)));
            this.Outline.Add(new Line(new MatrixPoint(0, -6), new MatrixPoint(-5, -2)));

            this.Outline.Add(new Line(new MatrixPoint(-10, -4), new MatrixPoint(-10, 4)));
            this.Outline.Add(new Line(new MatrixPoint(-10, 4), new MatrixPoint(0, 12)));
            this.Outline.Add(new Line(new MatrixPoint(0, 12), new MatrixPoint(10, 4)));
            this.Outline.Add(new Line(new MatrixPoint(10, 4), new MatrixPoint(10, -4)));
            this.Outline.Add(new Line(new MatrixPoint(10, -4), new MatrixPoint(0, -12)));
            this.Outline.Add(new Line(new MatrixPoint(0, -12), new MatrixPoint(-10, -4)));
	    }        
    }
}
