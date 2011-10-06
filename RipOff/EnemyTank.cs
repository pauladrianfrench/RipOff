namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class EnemyTank : Entity
    {
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

        public override ProximityResult DetectProximity(IScreenEntity other)
        {
            if (other is PlayerVehicle)
            {
                
                // under construction!!!
                Angle entityHeading = MatrixPoint.OrientationBetween(this.Centre, other.Centre);

                Angle relativeOrientation = this.Orientation + entityHeading;

                this.Rotate(-relativeOrientation.Radians);
                    
                return ProximityResult.Ahead;
               
            }
            return base.DetectProximity(other);
        }
    }
}
