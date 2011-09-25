using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RipOff
{
    public class Vehicle
    {
        
        public Vehicle()
        {
            this.Figure = new Figure();
        }

        public Point Location { get; set; }
        public Figure Figure { get; private set; }
    }
}
