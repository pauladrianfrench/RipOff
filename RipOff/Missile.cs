using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    public class Missile : MovingEntity
    {
        double distanceTravelled;

        double Range { get; set; }

        MatrixPoint trace;

        public Missile(GameArea ga, Angle orientation, MatrixPoint centre, double range)
            : base(ga)
        {
            //body
            this.Outline.Add(new Line(new MatrixPoint(0, -2), new MatrixPoint(0, 2)));
           
            Rotate(orientation.Radians);
            Centre = centre;

            this.Range = range;

            this.trace = new MatrixPoint(Outline[0].Point2.Xd, Outline[0].Point2.Yd);

            distanceTravelled = 0;
        }

        public override void Update()
        {
            double traceX = Outline[0].Point2.Xd;
            double traceY = Outline[0].Point2.Yd;
            
            this.trace = new MatrixPoint(traceX, traceY);

            double distance = 10;
            Move(distance);
            
            distanceTravelled += distance;
            if (distanceTravelled > Range)
            {
                Expired = true;
            }
        }

        public override List<Line> GetPerimeter()
        {
            List<Line> per = new List<Line>();
            per.Add(new Line(trace, Outline[0].Point2));
            return per;
        }

        public override ProximityResult DetectProximity(IEntity other)
        {
            if (!(other is Missile) && !(other is Explosion))
            {
                ProximityResult res = base.DetectProximity(other);
                if (res.Collision)
                {
                    this.Destroy();
                    other.Destroy();
                    return res;
                }
                return res;
            }
            return new ProximityResult { Collision = false, Entity = other };
        }
    }
}
