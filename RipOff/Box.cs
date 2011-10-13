using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    public class Box : Entity
    {
        public Box(GameController ga)
            : base(ga)
        {
            //box
            this.Outline.Add(new Line(new MatrixPoint(-10, 0), new MatrixPoint(0, 10)));
            this.Outline.Add(new Line(new MatrixPoint(0, 10), new MatrixPoint(10, 0)));
            this.Outline.Add(new Line(new MatrixPoint(10, 0), new MatrixPoint(0, -10)));
            this.Outline.Add(new Line(new MatrixPoint(0, -10), new MatrixPoint(-10, 0)));  
        }

        public override void Destroy()
        {
            Explosion exp = new Explosion(parent, 50);
            exp.Centre = this.Centre;
            parent.AddGameObject(exp);
            base.Destroy();
        }
    }
}
