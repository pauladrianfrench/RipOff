using System.Collections.Generic;
using System.Drawing;

namespace RipOff
{
    public class Missile : MovingEntity
    {
        double distanceTravelled;

        double Range { get; set; }

        MatrixPoint trace;

        public Missile(GameController ga, Angle orientation, MatrixPoint centre, double range)
            : base(ga)
        {
            //body
            this.Outline.Add(new Line(new MatrixPoint(0, -2), new MatrixPoint(0, 2)));
           
            Rotate(orientation.Radians);
            this.Centre = centre;
            this.Move(5); // we make an initial move so we don't destroy whatever ever fired us

            this.Range = range;
            this.trace = this.Centre;

            distanceTravelled = 0;
        }

        public override MatrixPoint Centre
        {
            get { return base.Centre; }
            set
            {
                trace = new MatrixPoint(this.Centre.Xd, this.Centre.Yd);
                base.Centre = value;
            }
        }

        public override void Update()
        {
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
            per.Add(new Line(trace, this.Centre));
            return per;
        }

        public override ProximityResult DetectProximity(IEntity other)
        {
            if (other is MovingEntity && !(other is Missile))
            {
               
                ProximityResult res = base.DetectProximity(other);
                if (other is PlayerVehicle && res.Distance < 20)
                {
                    double val = res.Distance;
                }

                if (res.Collision)
                {
                    this.Destroy();
                    other.Destroy();
                    return res;
                }
                return res;
            }
            return new ProximityResult { Collision = false };
        }
    }
}
