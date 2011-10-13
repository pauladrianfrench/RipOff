using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RipOff
{
    class Explosion : Entity
    {
        int lifeSpan;
        int ageTicks;
        bool spawn;

        public Explosion(GameController ga, int lifeSpanTicks)
            : base(ga)
        {
            // bang
            this.Outline.Add(new Line(new MatrixPoint(-5, -5), new MatrixPoint(-6, 2)));
            this.Outline.Add(new Line(new MatrixPoint(-6, 2), new MatrixPoint(-3, 7)));
            this.Outline.Add(new Line(new MatrixPoint(-3, 7), new MatrixPoint(-2, 8)));
            this.Outline.Add(new Line(new MatrixPoint(-2, 8), new MatrixPoint(4, 7)));
            this.Outline.Add(new Line(new MatrixPoint(4, 7), new MatrixPoint(6, 2)));
            this.Outline.Add(new Line(new MatrixPoint(6, 2), new MatrixPoint(7, -4)));
            this.Outline.Add(new Line(new MatrixPoint(7, -4), new MatrixPoint(4, -5)));
            this.Outline.Add(new Line(new MatrixPoint(4, -5), new MatrixPoint(-1, -6)));
            this.Outline.Add(new Line(new MatrixPoint(-1, -6), new MatrixPoint(-5, -5)));

            this.lifeSpan = lifeSpanTicks;
            this.ageTicks = 0;
            spawn = true;
        }

        public override void Update()
        {
            base.Update();
            ageTicks++;

            if (ageTicks > lifeSpan)
            {
                this.Expired = true;
            }
            if (spawn)
            {
                Random rand = new Random();
                Explosion exp1 = new Explosion(parent, 5);
                exp1.Centre = this.Centre + new MatrixPoint(rand.NextDouble()*15, rand.NextDouble()*15);
                exp1.spawn = false;
                parent.AddGameObject(exp1);
            }
        }
    }
}
