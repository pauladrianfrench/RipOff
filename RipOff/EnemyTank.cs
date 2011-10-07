namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EnemyTank : MovingEntity
    {
        public IEntity Target { get; set; }

        public EnemyTank(GameArea ga)
            : base(ga)
        {
            //body
            this.Outline.Add(new Line(new MatrixPoint(-15, -15), new MatrixPoint(0, 15)));
            this.Outline.Add(new Line(new MatrixPoint(0, 15), new MatrixPoint(15, -15)));
            this.Outline.Add(new Line(new MatrixPoint(15, -15), new MatrixPoint(0, -2)));
            this.Outline.Add(new Line(new MatrixPoint(0, -2), new MatrixPoint(-15, -15)));
        }

        public override void Destroy()
        {
            Explosion exp = new Explosion(parent, 50);
            exp.Centre = this.Centre;
            parent.AddGameObject(exp);
            base.Destroy();
        }

        public override ProximityResult DetectProximity(IEntity other)
        {
            ProximityResult res = base.DetectProximity(other);

            if (res.Collision)
            {
                this.Destroy();
                other.Destroy();
                return res;
            }
            else
            {
                if (other is Box)
                {
                    if (res.Distance < 150)
                    {
                        if (res.GetHeading(this.Orientation) == Heading.Ahead)
                        {
                            this.Rotate(-0.05);
                            this.Move(2);
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.FineAheadLeft)
                        {
                            this.Rotate(-0.05);
                            this.Move(3);
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.FineAheadRight)
                        {
                            this.Rotate(0.05);
                            this.Move(3);
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.AheadLeft)
                        {
                            this.Rotate(-0.05);
                            this.Move(4);
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.AheadRight)
                        {
                            this.Rotate(0.05);
                            this.Move(4);
                        }
                    }
                }
                else if (Target != null && other == Target)
                {
                    SeekTarget(res);
                }
            }

            return res;
        }

        private void SeekTarget(ProximityResult target)
        {
            Angle relativeOrientation = this.Orientation + target.Bearing;

            if (Math.Round(relativeOrientation.Radians) != Math.Round(0.0, 9))
            {
                if (relativeOrientation.Radians > Math.PI)
                {
                    this.Rotate(0.05);
                }
                else
                {
                    this.Rotate(-0.05);
                }
            }

            this.Move(4);
        }
    }
}
