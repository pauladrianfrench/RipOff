namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EnemyTank : MovingEntity
    {
        public IMission Mission { get; set; }
        double nextMove;
        double nextRotate;
        IEntity towedObject;

        public EnemyTank(GameArea ga)
            : base(ga)
        {
            //body
            this.Outline.Add(new Line(new MatrixPoint(-15, -15), new MatrixPoint(0, 15)));
            this.Outline.Add(new Line(new MatrixPoint(0, 15), new MatrixPoint(15, -15)));
            this.Outline.Add(new Line(new MatrixPoint(15, -15), new MatrixPoint(0, -2)));
            this.Outline.Add(new Line(new MatrixPoint(0, -2), new MatrixPoint(-15, -15)));

            this.nextMove = 0.0;
            this.nextRotate = 0.0;

            this.Mission = ga.GetNextMission();
        }

        public override MatrixPoint Centre
        {
            get { return base.Centre; }
            set
            {
                if (towedObject != null)
                {
                    MatrixPoint initialCentre = this.Centre;
                    double initialDistance = MatrixPoint.DistanceBetween(this.Centre, towedObject.Centre);

                    base.Centre = value;
                    Angle newOrientation = MatrixPoint.OrientationBetween(this.Centre, towedObject.Centre);
                    MatrixPoint diff = new MatrixPoint(0,0);

                    if (newOrientation.Radians == 0.0)
                    {
                        diff = new MatrixPoint(0, initialDistance);
                    }
                    else if (newOrientation.Radians < Math.PI / 2)
                    {
                        diff = new MatrixPoint(Math.Cos(newOrientation.Radians) * initialDistance, Math.Sin(newOrientation.Radians) * initialDistance);
                    }
                    else if (newOrientation.Radians == Math.PI/2)
                    {
                        diff = new MatrixPoint(initialDistance, 0);
                    }
                    else if (newOrientation.Radians > Math.PI / 2 && newOrientation.Radians < Math.PI)
                    {
                        diff = new MatrixPoint((Math.Cos(newOrientation.Radians - Math.PI/2)) * initialDistance, -Math.Sin(newOrientation.Radians - Math.PI/2) * initialDistance);
                    }
                    else if (newOrientation.Radians == Math.PI)
                    {
                        diff = new MatrixPoint(0, -initialDistance);
                    }
                    else if (newOrientation.Radians > Math.PI && newOrientation.Radians < 3* Math.PI / 2)
                    {
                        diff = new MatrixPoint(-Math.Cos(newOrientation.Radians - Math.PI) * initialDistance, -Math.Sin(newOrientation.Radians - Math.PI) * initialDistance);
                    }
                    else if (newOrientation.Radians == 3 * Math.PI / 2)
                    {
                        diff = new MatrixPoint(-initialDistance, 0);
                    }
                    else if (newOrientation.Radians > 3* Math.PI / 2 && newOrientation.Radians < 2 * Math.PI)
                    {
                        diff = new MatrixPoint(-Math.Cos(newOrientation.Radians - 3*Math.PI/2) * initialDistance, Math.Sin(newOrientation.Radians - 3*Math.PI/2) * initialDistance);
                    }
                    
                    towedObject.Centre = this.Centre + diff;
                }
                else
                {
                    base.Centre = value;
                }
            }
        }

        public override void Destroy()
        {
            parent.CollectMission(this.Mission);
            Explosion exp = new Explosion(parent, 50);
            exp.Centre = this.Centre;
            parent.AddGameObject(exp);
            base.Destroy();
        }

        public override void Update()
        {
            if (Mission.Complete)
            {
                Mission = parent.GetNextMission();
                if (Mission == null)
                {
                    this.Expired = true;
                }
            }
            
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
            IEntity target = Mission.GetNextUncompletedTarget().Target;

            if (res.Collision)
            {
                this.Destroy();
                other.Destroy();
                return res;
            }
            else
            {
                // collision avoidance
                if (other != target)
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
                else
                {
                    SeekTarget(res);
                }
            }

            return res;
        }

        private void SeekTarget(ProximityResult target)
        {
            Angle relativeOrientation = this.Orientation + target.Bearing;

            if (Math.Round(relativeOrientation.Radians, 1) != Math.Round(0.0, 1))
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

            if (target.Distance > 150)
            {
                nextMove = 4;
            }
            else if (target.Distance > 120)
            {
                nextMove = 3;
            }
            else if (target.Distance > 35)
            {
                nextMove = 1;
            }
            else
            {
                nextMove = 0.0;

                if (nextMove == 0.0)
                {
                    CollectTarget(Mission.GetNextUncompletedTarget().Target);
                }
            }
        }

        public void CollectTarget(IEntity t)
        {
            this.Move(-10);
            this.Rotate(Math.PI);
            this.towedObject = t;
        }
    }
}
