namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class GameAreaBorder : Entity
    {
        public GameAreaBorder(GameController ga)
            : base(ga)
        {
            Outline.Add(new Line(new MatrixPoint(-500, -280), new MatrixPoint(-500, 300)));
            Outline.Add(new Line(new MatrixPoint(-500, 300), new MatrixPoint(500, 300)));
            Outline.Add(new Line(new MatrixPoint(500, 300), new MatrixPoint(500, -280)));
            Outline.Add(new Line(new MatrixPoint(500, -280), new MatrixPoint(-500, -280)));
        }

        public override void Destroy()
        {
            // do nothing, you can't destroy the border
        }
    }
}
