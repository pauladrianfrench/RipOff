using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RipOff
{
    public class GameArea
    {
        public GameArea(DrawParams dp)
        {
            this.DrawParam = dp;
            this.Vehicle = new Vehicle();
        }

        public void Draw()
        {
            DrawVehicle();
            
            //Point p1 = new Point(0, 0);
            //Point p2 = new Point(0, 100);
            //Point p3 = new Point(100, 100);
            //Point p4 = new Point(100, 0);
            //DrawParam.Graphics.DrawLine(DrawParam.Pen, DrawParam.Trans.TransPoint(p1), DrawParam.Trans.TransPoint(p2));
            //DrawParam.Graphics.DrawLine(DrawParam.Pen, DrawParam.Trans.TransPoint(p2), DrawParam.Trans.TransPoint(p3));
            //DrawParam.Graphics.DrawLine(DrawParam.Pen, DrawParam.Trans.TransPoint(p3), DrawParam.Trans.TransPoint(p4));
            //DrawParam.Graphics.DrawLine(DrawParam.Pen, DrawParam.Trans.TransPoint(p4), DrawParam.Trans.TransPoint(p1));
        }

        public void DrawVehicle()
        {
            foreach (Line l in this.Vehicle.Figure.Outline)
            {
                DrawParam.Graphics.DrawLine(DrawParam.Pen, DrawParam.Trans.TransPoint(new Point(l.Point1.X, l.Point1.Y)), DrawParam.Trans.TransPoint(new Point(l.Point2.X,l.Point2.Y)));
            }
        }

        public DrawParams DrawParam { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
