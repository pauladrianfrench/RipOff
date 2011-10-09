
namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using PaulMath;

    public class MovingEntity : Entity, IMovingEntity
    {
        public MovingEntity(GameArea ga)
            : base(ga)
        {
            this.Orientation = new Angle(0.0);
            this.CentreLine = new Line(new MatrixPoint(0, 0), new MatrixPoint(0, 1));
        }

        public Angle Orientation { get; private set; }
        public Line CentreLine { get; private set; }

        public override MatrixPoint Centre
        {
            get { return base.Centre; }
            set
            {
                MatrixPoint diff = value - centre;
                
                CentreLine.Point1 += diff;
                CentreLine.Point2 += diff;

                base.Centre = value;
            }
        }

        public override void Update()
        {
            base.Update();
        }

        public virtual void Rotate(double rad)
        {
            int len = this.Outline.Count;

            for (int i = 0; i < len; ++i)
            {
                Rotate(Outline[i], rad);
            }

            Rotate(CentreLine, rad);

            // finally we'll keep track of our orientation so we don't have to calculate it
            this.Orientation += rad;
        }
        private void Rotate(Line l, double rad)
        {
            // this rotates points about (0,0)
            // quick and nasty hack, shift item centre from current location to (0,0)
            // rotate and then relocate back to original location.
            // Will sort this when i get time
            double[] rot = { Math.Cos(rad), -Math.Sin(rad), Math.Sin(rad), Math.Cos(rad) };

            Matrix m = new Matrix(rot, 2, 2);

            l.Point1.Matrix = (m * (l.Point1 - Centre).Matrix);
            l.Point1 = l.Point1 + Centre;

            l.Point2.Matrix = (m * (l.Point2 - Centre).Matrix);
            l.Point2 = l.Point2 + Centre;
        }

        public virtual void Move(double speed)
        {
            // we want the speed to be constant whatever direction the item is travelling
            // so when direction is non-orthogonal make the distance driven the length of the hypotenuse
            // and determine the x,y values accordingly

            double p1y = CentreLine.Point1.Matrix.GetValue(2, 1);
            double p1x = CentreLine.Point1.Matrix.GetValue(1, 1);

            double p2y = CentreLine.Point2.Matrix.GetValue(2, 1);
            double p2x = CentreLine.Point2.Matrix.GetValue(1, 1);

            double rise = p1y - p2y;
            double run = p1x - p2x;

            double h = Math.Sqrt((rise * rise) + (run * run));
            double factor = speed / h;

            Centre -= new MatrixPoint(factor * run, factor * rise);
        }

        public virtual ProximityResult DetectProximity(IEntity other)
        {
            ProximityResult res = new ProximityResult();
            res.Bearing = MatrixPoint.OrientationBetween(this.Centre, other.Centre);
            res.Distance = MatrixPoint.DistanceBetween(this.Centre, other.Centre);
            res.Collision = false;

            List<Line> myOutline = this.GetPerimeter();
            List<Line> otherOutline = other.GetPerimeter();

            int myCount = myOutline.Count;
            int otherCount = otherOutline.Count;

            for (int i = 0; i < myCount; i++)
            {
                for (int j = 0; j < otherCount; j++)
                {
                    if (myOutline[i].Intersects(otherOutline[j]))
                    {
                        res.Collision = true;
                        return res;
                    }
                }
            }
            return res;
        }
    }
}
