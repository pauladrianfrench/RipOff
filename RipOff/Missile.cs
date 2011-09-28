using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    public class Missile : Entity, IScreenEntity
    {
        GameArea parent;

        public Missile(GameArea ga, double orientation)
            : base()
        {
            parent = ga;

            //body
            this.Outline.Add(new Line(new MatrixPoint(-1, -2), new MatrixPoint(-1, 2)));
            this.Outline.Add(new Line(new MatrixPoint(-1, 2), new MatrixPoint(1, 2)));
            this.Outline.Add(new Line(new MatrixPoint(1, 2), new MatrixPoint(1, -2)));
            this.Outline.Add(new Line(new MatrixPoint(1, -2), new MatrixPoint(-1, -2)));

            Rotate(orientation);
        }

        public override void Update()
        {
            Move(10);
        }
    }
}
