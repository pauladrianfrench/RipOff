

namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;
    using PaulMath;
    
    
    public class Entity : IScreenEntity
    {
        MatrixPoint centre;
        protected GameArea parent;

        public Entity(GameArea ga)
        {
            parent = ga;
            this.Outline = new List<Line>();
            this.centre = new MatrixPoint(0, 0);
            this.Expired = false;
        }

        public List<Line> Outline { get; set; }
        
        public MatrixPoint Centre
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
        public double Orientation { get; private set; }

        public virtual void Update()
        {
        }

        public virtual void Rotate(double rad)
        {
            // this rotates points about (0,0)
            // quick and nasty hack, shift item centre from current location to (0,0)
            // rotate and then relocate back to original location.
            // Will sort this when i get time
            double [] rot = {Math.Cos(rad) , -Math.Sin(rad), Math.Sin(rad), Math.Cos(rad)};
            
            Matrix m = new Matrix(rot, 2, 2);
            
            int len = this.Outline.Count;
            
            for (int i = 0; i < len; ++i)
            {
                Outline[i].Point1.Matrix = (m * (Outline[i].Point1 - Centre).Matrix);
                Outline[i].Point1 = Outline[i].Point1 + Centre;

                Outline[i].Point2.Matrix = (m * (Outline[i].Point2 - Centre).Matrix);
                Outline[i].Point2 = Outline[i].Point2 + Centre;
            }

            // finally we'll keep track of our orientation so we don't have to calculate it
            this.Orientation += rad;
        }

        public virtual void Move(double speed)
        {
            // we want the speed to be constant whatever direction the item is travelling
            // so when direction is non-orthogonal make the distance driven the length of the hypotenuse
            // and determine the x,y values accordingly
            // Each entities outline should be created such that Outline[0] is oriented front to back
            // and items move by shifting the centre point in line with this.
            double p1y = Outline[0].Point1.Matrix.GetValue(2, 1);
            double p1x = Outline[0].Point1.Matrix.GetValue(1, 1);

            double p2y = Outline[0].Point2.Matrix.GetValue(2, 1);
            double p2x = Outline[0].Point2.Matrix.GetValue(1, 1);

            double rise = p1y - p2y;
            double run = p1x - p2x;

            double h = Math.Sqrt((rise * rise) + (run * run));
            double factor = speed /h;

            Centre -= new MatrixPoint(factor*run, factor*rise);
         }

        public virtual bool DetectCollision(IScreenEntity other)
        {
            if (this is Explosion || other is Explosion)
            {
                return false;
            }

            List<Line> myOutline = this.GetPerimeter();
            List<Line> otherOutline = other.GetPerimeter();

            int myCount = myOutline.Count;
            int otherCount = otherOutline.Count;

            for (int i = 0; i < myCount; i++)
            {
                for (int j = 0; j < otherCount; j++)
                {
                    if (myOutline[i].Intersects(otherOutline[j]))
                    {
                        this.Destroy();
                        other.Destroy();
                        return true;
                    }
                }
            }
            return false;
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
