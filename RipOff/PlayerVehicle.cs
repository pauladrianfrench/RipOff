

namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;
    using PaulMath;

    public class PlayerVehicle : MovingEntity, IPlayerVehicle
    {
        bool left;
        bool right;
        bool driveForward;
        bool driveBackward;
        bool shoot;
        Line gunTip;
        List<Line> perimeter;
        IEntity towedObject;

        public PlayerVehicle(GameArea ga)
            : base(ga)
        {
            //body
            this.Outline.Add(new Line(new MatrixPoint(-5, -5), new MatrixPoint(-5, 5)));
            this.Outline.Add(new Line(new MatrixPoint(-5, 5), new MatrixPoint(5, 5)));
            this.Outline.Add(new Line(new MatrixPoint(5, 5), new MatrixPoint(5, -5)));
            this.Outline.Add(new Line(new MatrixPoint(5, -5), new MatrixPoint(-5, -5)));

            this.Outline.Add(new Line(new MatrixPoint(-5, 0), new MatrixPoint(-7, -7)));
            this.Outline.Add(new Line(new MatrixPoint(-7, -7), new MatrixPoint(-5, -5)));
            this.Outline.Add(new Line(new MatrixPoint(5, 0), new MatrixPoint(7, -7)));
            this.Outline.Add(new Line(new MatrixPoint(7, -7), new MatrixPoint(5, -5)));

            perimeter = new List<Line>();

            for (int i = 0; i < Outline.Count; i++)
            {
                perimeter.Add(Outline[i]);
            }

            //gun tip, we'll use a zero length line to track the position of the gun tip
            this.gunTip = new Line(new MatrixPoint(0, 15), new MatrixPoint(0, 15));
            this.Outline.Add(gunTip);

            //gun
            this.Outline.Add(new Line(new MatrixPoint(-1, 5), new MatrixPoint(-1, 8)));
            this.Outline.Add(new Line(new MatrixPoint(-1, 8), new MatrixPoint(1, 8)));
            this.Outline.Add(new Line(new MatrixPoint(1, 8), new MatrixPoint(1, 5)));

            this.Centre = new MatrixPoint(0, 0);

            left = false;
            right = false;
            driveForward = false;
            driveBackward = false;
            shoot = false;
        }

        public override MatrixPoint Centre
        {
            get { return base.Centre; }
            set
            {
                if (towedObject != null)
                {
                    base.Centre = value;
                    Angle newOrientation = MatrixPoint.OrientationBetween(this.Centre, towedObject.Centre);

                    MatrixPoint p = new MatrixPoint(0, 35);
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
        }

        public override void Update()
        {
            if (shoot)
            {
                new Missile(parent, this.Orientation, this.gunTip.Point1, 1000);
                shoot = false;
            }

            if (driveForward)
            {
                Move(4);
            }

            if (driveBackward)
            {
                Move(-4);
            }

            if (left)
            {
                Rotate(0.05);
            }

            if (right)
            {
                Rotate(-0.05);
            }
        }

        public override void Destroy()
        {
            Explosion exp = new Explosion(parent, 10);
            exp.Centre = this.Centre;
            parent.AddGameObject(exp);
            base.Destroy();
        }

        public override List<Line> GetPerimeter()
        {
            return perimeter;
        }

        public void KeyDown(ActionParams actions)
        {
            if (actions.A)
            {
                left = true;
            }
            if (actions.D)
            {
                right = true;
            }
            if (actions.L)
            {
                driveForward = true;
            }

            if (actions.J)
            {
                driveBackward = true;
            }

            if (actions.N)
            {
                shoot = true;
            }
        }

        public void KeyUp(ActionParams actions)
        {
            if (actions.A)
            {
                left = false;
            }
            if (actions.D)
            {
                right = false;
            }
            if (actions.L)
            {
                driveForward = false;
            }

            if (actions.J)
            {
                driveBackward = false;
            }

            if (actions.N)
            {
                shoot = false;
            }
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
            return res;
        }
    }
}
