

namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;
    
    public class Entity : IEntity
    {
        protected MatrixPoint centre;
        protected GameArea parent;

        public Entity(GameArea ga)
        {
            this.parent = ga;
            this.Outline = new List<Line>();
            this.centre = new MatrixPoint(0, 0);
            this.Expired = false;
        }

        public List<Line> Outline { get; set; }
        

        public virtual MatrixPoint Centre
        {
            get { return centre; }
            set
            {
                MatrixPoint diff = value - centre;
                centre = value;

                // our centre point has moved, update all lines accordingly.
                foreach (Line l in Outline)
                {
                    l.Point1 += diff;
                    l.Point2 += diff;
                }
            }
        }
        public bool Expired { get; set; }
        

        public virtual void Update()
        {
        }

        public virtual List<Line> GetPerimeter()
        {
            return Outline;
        }

        public virtual void Destroy()
        {
            this.Expired = true;
        }

        public void Draw(DrawParams dp)
        {
            foreach (Line l in this.Outline)
            {
                dp.Graphics.DrawLine(dp.Pen, dp.Trans.TransPoint(new Point(l.Point1.X, l.Point1.Y)), dp.Trans.TransPoint(new Point(l.Point2.X, l.Point2.Y)));
            }
        }
    }
}
