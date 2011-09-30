using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RipOff
{
    public class PlayerVehicle : Entity, IPlayerVehicle
    {
        GameArea parent;
        bool left;
        bool right;
        bool driveForward;
        bool driveBackward;
        bool shoot;
        Line gunTip;

        public PlayerVehicle(GameArea ga) 
            : base()
        {
            parent = ga;

            //body
            this.Outline.Add(new Line(new MatrixPoint(-10, -10), new MatrixPoint(-10, 10)));
            this.Outline.Add(new Line(new MatrixPoint(-10, 10), new MatrixPoint(10, 10)));
            this.Outline.Add(new Line(new MatrixPoint(10, 10), new MatrixPoint(10, -10)));
            this.Outline.Add(new Line(new MatrixPoint(10, -10), new MatrixPoint(-10, -10)));

            //gun tip, we'll use a zero length line to track the position of the gun tip
            this.gunTip = new Line(new MatrixPoint(0, 15), new MatrixPoint(0, 15));
            this.Outline.Add(gunTip);

            //gun
            this.Outline.Add(new Line(new MatrixPoint(-1, 10), new MatrixPoint(-1, 15)));
            this.Outline.Add(new Line(new MatrixPoint(-1, 15), new MatrixPoint(1, 15)));
            this.Outline.Add(new Line(new MatrixPoint(1, 15), new MatrixPoint(1, 10)));

            this.Centre = new MatrixPoint(0, 0);
           
            left = false;
            right = false;
            driveForward = false;
            driveBackward = false;
            shoot = false;
        }

        public override void Update()
        {
            if (shoot)
            {
                Missile m = new Missile(parent, this.Orientation, 1000);
                m.Centre = this.gunTip.Point1;
                parent.AddGameObject(m);
            }

            if (driveForward)
            {
                Move(5);
            }

            if (driveBackward)
            {
                Move(-5);
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
    }
}
