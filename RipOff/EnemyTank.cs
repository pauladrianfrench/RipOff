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

            if (Mission == null)
            {
                return res;
            }

            if (!(other is Explosion) && res.Collision)
            {
                this.Destroy();
                other.Destroy();
                return res;
            }
            else
            {
                if (!Mission.Complete && Mission.GetNextUncompletedTarget().Target == other)
                {
                    SeekTarget(res);
                }
                else if (!(other is Missile) && !(other is WayPoint)) 
                {
                    // collision avoidance checks
                    if (res.Distance < 150)
                    {
                        if (res.GetHeading(this.Orientation) == Heading.Ahead)
                        {
                            nextRotate = -0.1;
                            nextMove = 0;
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.FineAheadLeft)
                        {
                            if ((tickCount % 5 == 0) && (other is PlayerVehicle))
                            {
                                new Missile(parent, this.Orientation, this.gunTip.Point1, 1000);
                            }

                            nextRotate = -0.3;
                            nextMove = 2;
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.FineAheadRight)
                        {
                            if ((tickCount % 5 == 0) && (other is PlayerVehicle))
                            {
                                new Missile(parent, this.Orientation, this.gunTip.Point1, 1000);
                            }

                            nextRotate = 0.3;
                            nextMove = 2;
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.AheadLeft)
                        {
                            nextRotate = -0.2;
                            nextMove = 4;
                        }
                        else if (res.GetHeading(this.Orientation) == Heading.AheadRight)
                        {
                            nextRotate = 0.2;
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
            }

            return res;
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

            if (prox.Distance > 200 )
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
                //if (nextMove == 0.0)
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
                            target.Complete = AttackTarget(target.Target);
                            break;
                        default:
                            target.Complete = true;
                            break;
                    }
                }
            }
        }

        public void CollectTarget(IEntity t)
        {
            this.Move(-5);
            this.Rotate(Math.PI);
            this.towedObject = t;
        }

        public bool AttackTarget(IEntity t)
        {
            Missile m = new Missile(parent, this.Orientation, this.gunTip.Point1, 1000);
            return t.Expired;
        }
    }
}
