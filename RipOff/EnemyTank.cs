namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using PaulMath;

    public class EnemyTank : MovingEntity
    {
        public IMission Mission { get; set; }
        double nextMove;
        double nextRotate;
        IEntity towedObject;
        Line gunTip;
        uint tickCount;
        bool abortTarget;
        ProximityResult nearestObject;

        public EnemyTank(GameArea ga)
            : base(ga)
        {
            //body
            this.Outline.Add(new Line(new MatrixPoint(-7, -7), new MatrixPoint(0, 7)));
            this.Outline.Add(new Line(new MatrixPoint(0, 7), new MatrixPoint(7, -7)));
            this.Outline.Add(new Line(new MatrixPoint(7, -7), new MatrixPoint(0, -2)));
            this.Outline.Add(new Line(new MatrixPoint(0, -2), new MatrixPoint(-7, -7)));

            //gun tip, we'll use a zero length line to track the position of the gun tip
            this.gunTip = new Line(new MatrixPoint(0, 10), new MatrixPoint(0, 10));
            this.Outline.Add(gunTip);

            this.nextMove = 0.0;
            this.nextRotate = 0.0;

            this.Mission = ga.GetNextMission();
            tickCount = 0;

            abortTarget = false;
        }

        public override MatrixPoint Centre
        {
            get { return base.Centre; }
            set
            {
                if (towedObject != null)
                {
                    if (towedObject != null)
                    {
                        base.Centre = value;
                        Angle newOrientation = MatrixPoint.OrientationBetween(this.Centre, towedObject.Centre);

                        MatrixPoint p = new MatrixPoint(0, 20);
                        double[] rot = { Math.Cos(-newOrientation.Radians), -Math.Sin(-newOrientation.Radians), Math.Sin(-newOrientation.Radians), Math.Cos(-newOrientation.Radians) };
                        Matrix m = new Matrix(rot, 2, 2);
                        p.Matrix = m * p.Matrix;

                        towedObject.Centre = this.Centre + p;
                    }
                    else
                    {
                        base.Centre = value;
                    }
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
            tickCount++;
            if (Mission != null && Mission.Complete)
            {
                if (towedObject != null)
                {
                    towedObject.Expired = true;
                }
                towedObject = null;
                Mission = parent.GetNextMission();
                if (Mission == null)
                {
                    this.Expired = true;
                }
            }

            base.Update();
            IEntity currentTarget = Mission.GetNextUncompletedTarget().Target;
            double targetDistance = MatrixPoint.DistanceBetween(this.Centre, currentTarget.Centre);

            if (nearestObject != null && nearestObject.Distance < 100 && nearestObject.Distance < targetDistance)
            {
                AvoidTarget(nearestObject);
            }
            else
            {
                SeekTarget(base.DetectProximity(currentTarget));
            }

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

            if (!(other is Explosion) && res.Collision)
            {
                // we're dead!
                this.Destroy();
                other.Destroy();
                return res;
            }

            if (Mission == null && !Mission.Complete)
            {
                return res;
            }

            IEntity currentTarget = Mission.GetNextUncompletedTarget().Target;

            if (res.Entity != currentTarget)
            {
                if (nearestObject == null)
                {
                    nearestObject = res;
                }
                else if (res.Distance < nearestObject.Distance && IsAhead(res) && !(res.Entity is Missile) && !(res.Entity is WayPoint))
                {
                    nearestObject = res;
                }
            }

            return res;
        }

        private bool IsAhead(ProximityResult prox)
        {
            Heading relativeHeading = prox.GetHeading(this.Orientation);
            switch (relativeHeading)
            {
                case Heading.Ahead:
                case Heading.AheadLeft:
                case Heading.AheadRight:
                case Heading.FineAheadLeft:
                case Heading.FineAheadRight:
                    return true;
                default:
                    return false;
            }
        }

        private void SeekTarget(ProximityResult prox)
        {
            IMissionTarget target = Mission.GetNextUncompletedTarget();
            Angle relativeOrientation = this.Orientation + prox.Bearing;

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

            if (prox.Distance > 200)
            {
                nextMove = 4;
            }
            else if (prox.Distance > 150)
            {
                if (tickCount % 10 == 0 && target.Objective == MissionObjective.Attack)
                {
                    new Missile(parent, this.Orientation, this.gunTip.Point1, 1000);
                    target.Complete = target.Target.Expired;
                }
                nextMove = 4;
            }
            else if (prox.Distance > 30)
            {
                if (tickCount % 5 == 0 && target.Objective == MissionObjective.Attack)
                {
                    new Missile(parent, this.Orientation, this.gunTip.Point1, 1000);
                    target.Complete = target.Target.Expired;
                }
                nextMove = 4;
            }
            else
            {
                switch (target.Objective)
                {
                    case MissionObjective.Visit:
                        target.Complete = true;
                        break;
                    case MissionObjective.Collect:
                        CollectTarget(target.Target);
                        target.Complete = true;
                        break;
                    case MissionObjective.LayMine:
                        target.Complete = true;
                        break;
                    case MissionObjective.Attack:
                        FireMissile();
                        target.Complete = target.Target.Expired;
                        break;
                    default:
                        target.Complete = true;
                        break;
                }
            }
        }

        private void AvoidTarget(ProximityResult prox)
        {
            // collision avoidance checks
            if (prox.Distance < 100)
            {
                if (prox.GetHeading(this.Orientation) == Heading.Ahead)
                {
                    if ((tickCount % 5 == 0) && (prox.Entity is PlayerVehicle)) // if it's the player, might as well have a pop
                    {
                        FireMissile();
                    }
                    nextRotate = -0.1;
                    nextMove = 0;
                }
                else if (prox.GetHeading(this.Orientation) == Heading.FineAheadLeft)
                {
                    if ((tickCount % 5 == 0) && (prox.Entity is PlayerVehicle)) // if it's the player, might as well have a pop
                    {
                        FireMissile();
                    }

                    nextRotate = -0.3;
                    nextMove = 2;
                }
                else if (prox.GetHeading(this.Orientation) == Heading.FineAheadRight)
                {
                    if ((tickCount % 5 == 0) && (prox.Entity is PlayerVehicle)) // if it's the player, might as well have a pop
                    {
                        FireMissile();
                    }

                    nextRotate = 0.3;
                    nextMove = 2;
                }
                else if (prox.GetHeading(this.Orientation) == Heading.AheadLeft)
                {
                    nextRotate = -0.2;
                    nextMove = 4;
                }
                else if (prox.GetHeading(this.Orientation) == Heading.AheadRight)
                {
                    nextRotate = 0.2;
                    nextMove = 4;
                }
                else if (prox.GetHeading(this.Orientation) == Heading.Right && prox.Distance < 75)
                {
                    nextRotate = 0.05;
                    nextMove = 4;
                }
                else if (prox.GetHeading(this.Orientation) == Heading.Left && prox.Distance < 75)
                {
                    nextRotate = -0.05;
                    nextMove = 4;
                }
            }
        }

        public void CollectTarget(IEntity t)
        {
            this.Move(-5);
            this.Rotate(Math.PI);
            this.towedObject = t;
        }

        public void FireMissile()
        {
            Missile m = new Missile(parent, this.Orientation, this.gunTip.Point1, 1000);
        }
    }
}
