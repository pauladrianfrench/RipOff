namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EnemyTank : MovingEntity
    {
        public IEntity Target { get; set; }
        double nextMove;
        double nextRotate;

        public EnemyTank(GameArea ga)
            : base(ga)
        {
            //body
            this.Outline.Add(new Line(new MatrixPoint(-15, -15), new MatrixPoint(0, 15)));
            this.Outline.Add(new Line(new MatrixPoint(0, 15), new MatrixPoint(15, -15)));
            this.Outline.Add(new Line(new MatrixPoint(15, -15), new MatrixPoint(0, -2)));
            this.Outline.Add(new Line(new MatrixPoint(0, -2), new MatrixPoint(-15, -15)));

            nextMove = 0.0;
            nextRotate = 0.0;
        }

        public override void Destroy()
        {
            Explosion exp = new Explosion(parent, 50);
            exp.Centre = this.Centre;
            parent.AddGameObject(exp);
            base.Destroy();
        }

        public override void Update()
        {
            base.Update();

            if (nextMove != 0.0)
            {
                this.Move(nextMove);
            }

            if (nextRotate != 0.0)
            {
                this.Rotate(nextRotate);
            }

            nextMove = 0.0;
            nextRotate = 0.0;
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
                // collision avoidance
                if (other is Box || other is EnemyTank || other is Explosion)
                {
                    if (res.Distance < 130)
                    {
                        if (res.GetHeading(this.Orientation) == Heading.Ahead)
                        {
                          nextRotate = -0.1;
                          nextMove = 0;
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.FineAheadLeft)
                        {
                            nextRotate = -0.2;
                            nextMove = 1;
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.FineAheadRight)
                        {
                            nextRotate = 0.2;
                            nextMove = 1;
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.AheadLeft)
                        {
                            nextRotate = -0.05;
                            nextMove = 4;
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.AheadRight)
                        {
                            nextRotate = 0.05;
                            nextMove = 4;
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.Right && res.Distance < 75)
                        {
                            nextRotate = 0.05;
                            nextMove = 4;
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.Left && res.Distance < 75)
                        {
                            nextRotate = -0.05;
                            nextMove = 4;
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
                    nextRotate = 0.05;
                    
                }
                else
                {
                    nextRotate = -0.05;
                }
            }

            nextMove = 4;
            
        }
    }
}
