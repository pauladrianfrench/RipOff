using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    public class Missile : Entity, IScreenEntity
    {
        GameArea parent;
        double distanceTravelled;

        double Range { get; set; }

        public Line Trace { get; set; }

        public Missile(GameArea ga, double orientation, double range)
            : base()
        {
            parent = ga;

            this.Trace = new Line(new MatrixPoint(0, -2), new MatrixPoint(0, 2));

            //body
            this.Outline.Add(new Line(new MatrixPoint(0, -2), new MatrixPoint(0, 2)));
            //this.Outline.Add(new Line(new MatrixPoint(-1, 2), new MatrixPoint(1, 2)));
            //this.Outline.Add(new Line(new MatrixPoint(1, 2), new MatrixPoint(1, -2)));
            //this.Outline.Add(new Line(new MatrixPoint(1, -2), new MatrixPoint(-1, -2)));
            
            Rotate(orientation);

            this.Range = range;

            distanceTravelled = 0;
        }

        public override void Update()
        {
            double traceX = Trace.Point1.Xd;
            double traceY = Trace.Point1.Yd;
            double distance = 10;

            // we add trace in so it gets moved with all the other points
            this.Outline.Add(Trace);
            
            Move(distance);
            
            // then we take it out again because we don't want to draw it
            this.Outline.Remove(Trace);

            Trace.Point2 = new MatrixPoint(traceX, traceY);

            distanceTravelled += distance;
            if (distanceTravelled > Range)
            {
                Expired = true;
            }
        }
    }
}
