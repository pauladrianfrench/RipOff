namespace UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using RipOff;

    [TestFixture]
    public class Tests
    {
        [Test]
        public void LineTests()
        {
            Line line1 = new Line(new MatrixPoint(0, 5), new MatrixPoint(5, 10));
            Line line2 = new Line(new MatrixPoint(0, 10), new MatrixPoint(5, 0));
            Line line3 = new Line(new MatrixPoint(0, 4), new MatrixPoint(5, 0));
            Line line4 = new Line(new MatrixPoint(0, 0), new MatrixPoint(5, 5));
            Line line5 = new Line(new MatrixPoint(0, 5), new MatrixPoint(5, 0));

            Line line6 = new Line(new MatrixPoint(1, 5), new MatrixPoint(1, 15));
            Line line7 = new Line(new MatrixPoint(-4, 3), new MatrixPoint(5, 7));

            Assert.True(line1.Intersects(line2), "line1.Intersects(line2)");
            Assert.True(line2.Intersects(line1), "line2.Intersects(line1)");

            Assert.False(line1.Intersects(line3), "line1.Intersects(line3)");
            Assert.False(line3.Intersects(line1), "line3.Intersects(line1)");

            Assert.True(line4.Intersects(line5), "line4.Intersects(line5)");
            Assert.True(line5.Intersects(line4), "line5.Intersects(line4)");

            Assert.True(line6.Intersects(line7), "line6.Intersects(line7)");
            Assert.True(line7.Intersects(line6), "line7.Intersects(line6)");
        }

        [Test]
        public void TestCollisionDetection()
        {
            ////GameArea gameArea = new GameArea();

            ////PlayerVehicle veh1 = new PlayerVehicle(gameArea);
            ////gameArea.AddGameObject(veh1);

            ////Box box = new Box(gameArea);

            ////gameArea.AddGameObject(box);
            ////Assert.AreEqual(Heading.Hit, veh1.DetectProximity(box), "Collision detection false negative");

            ////box.Centre = new MatrixPoint(-150, -150);
            ////Assert.AreEqual(Heading.Missed, veh1.DetectProximity(box), "Collision detection false positive");
        }

        [Test]
        public void TestOrientationCalcs1()
        {
            MatrixPoint centre = new MatrixPoint(0, 0);

            MatrixPoint p1 = new MatrixPoint(5, 5);
            MatrixPoint p2 = new MatrixPoint(5, -5);
            MatrixPoint p3 = new MatrixPoint(-5, -5);
            MatrixPoint p4 = new MatrixPoint(-5, 5);

            MatrixPoint p5 = new MatrixPoint(0, 5);
            MatrixPoint p6 = new MatrixPoint(5, 0);
            MatrixPoint p7 = new MatrixPoint(0, -5);
            MatrixPoint p8 = new MatrixPoint(-5, 0);

            MatrixPoint p9 = new MatrixPoint(9, -28);

            Assert.AreEqual(Math.Round(Math.PI / 4, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p1).Radians, 9), "Orientation check 1");
            Assert.AreEqual(Math.Round(3 * Math.PI / 4, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p2).Radians, 9), "Orientation check 2");
            Assert.AreEqual(Math.Round(5 * Math.PI / 4, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p3).Radians, 9), "Orientation check 3");
            Assert.AreEqual(Math.Round(7 * Math.PI / 4, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p4).Radians, 9), "Orientation check 4");

            Assert.AreEqual(Math.Round(0.0, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p5).Radians, 9), "Orientation check 5");
            Assert.AreEqual(Math.Round(Math.PI / 2, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p6).Radians, 9), "Orientation check 6");
            Assert.AreEqual(Math.Round(Math.PI, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p7).Radians, 9), "Orientation check 7");
            Assert.AreEqual(Math.Round(Math.PI + Math.PI / 2, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p8).Radians, 9), "Orientation check 8");
        }

        [Test]
        public void TestOrientationCalcs2()
        {
            MatrixPoint centre = new MatrixPoint(1, 1);

            MatrixPoint p1 = new MatrixPoint(6, 6);
            MatrixPoint p2 = new MatrixPoint(6, -4);
            MatrixPoint p3 = new MatrixPoint(-4, -4);
            MatrixPoint p4 = new MatrixPoint(-4, 6);


            Assert.AreEqual(Math.Round(Math.PI / 4, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p1).Radians, 9), "Orientation check 1");
            Assert.AreEqual(Math.Round(3 * Math.PI / 4, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p2).Radians, 9), "Orientation check 2");
            Assert.AreEqual(Math.Round(5 * Math.PI / 4, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p3).Radians, 9), "Orientation check 3");
            Assert.AreEqual(Math.Round(7 * Math.PI / 4, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p4).Radians, 9), "Orientation check 4");
        }

        [Test]
        public void TestAngles()
        {
            Angle a1 = new Angle(6 * Math.PI);
            Assert.AreEqual(Math.Round(0.0, 9), Math.Round(a1.Radians, 9), "Angle 1 test");

            Angle a2 = new Angle(5 * Math.PI);
            Assert.AreEqual(Math.Round(Math.PI, 9), Math.Round(a2.Radians, 9), "Angle 2 test");

            Angle a3 = a1 + a2;
            Assert.AreEqual(Math.Round(Math.PI, 9), Math.Round(a3.Radians, 9), "Angle 3 test");

            Angle a4 = new Angle(4);
            Assert.AreEqual(Math.Round(4.0, 9), Math.Round(a4.Radians, 9), "Angle 4 test");

            Angle a5 = new Angle((Math.PI * 2) + 1.33);
            Assert.AreEqual(Math.Round(1.33, 9), Math.Round(a5.Radians, 9), "Angle 5 test");

            Angle a6 = new Angle(-(Math.PI - 1.5));
            Assert.AreEqual(Math.Round(Math.PI + 1.5, 9), Math.Round(a6.Radians, 9), "Angle 6 test");

            Angle a7 = new Angle(-(5 * Math.PI - 1.5));
            Assert.AreEqual(Math.Round(Math.PI + 1.5, 9), Math.Round(a7.Radians, 9), "Angle 7 test");

            Angle a8 = new Angle(-5 * Math.PI) - 1.5;
            Assert.AreEqual(Math.Round(Math.PI - 1.5, 9), Math.Round(a8.Radians, 9), "Angle 8 test");

            Angle a9 = new Angle(2.3);
            Angle a10 = new Angle(0.5);
            a9 += a10;
            Assert.AreEqual(Math.Round(2.8, 9), Math.Round(a9.Radians, 9), "Angle 9 test");

            a9 -= a10;
            Assert.AreEqual(Math.Round(2.3, 9), Math.Round(a9.Radians, 9), "Angle 9 test");

            a9 -= 0.3;
            Assert.AreEqual(Math.Round(2.0, 9), Math.Round(a9.Radians, 9), "Angle 9 test");

            a9 += 0.5;
            Assert.AreEqual(Math.Round(2.5, 9), Math.Round(a9.Radians, 9), "Angle 9 test");
        }

        [Test]
        public void TestMovingEntities()
        {
            GameArea ga = new GameArea();

            MovingEntity mover = new MovingEntity(ga);

            mover.Rotate(-Math.PI / 4);
            mover.Move(10);
            Assert.AreEqual(Math.Round(Math.Sqrt(50), 7), Math.Round(mover.Centre.Xd, 7), "Test move x");
            Assert.AreEqual(Math.Round(Math.Sqrt(50), 7), Math.Round(mover.Centre.Yd, 7), "Test move y");

            Missile m = new Missile(ga, mover.Orientation, new MatrixPoint(0, 0), 1000);
            m.Move(10);

            Assert.AreEqual(Math.Round(Math.Sqrt(50), 7), Math.Round(m.Centre.Xd, 7), "Missile move x");
            Assert.AreEqual(Math.Round(Math.Sqrt(50), 7), Math.Round(m.Centre.Yd, 7), "Missile move y");

            Line trace = m.GetPerimeter()[0];
            Assert.AreEqual(Math.Round(0.0, 7), Math.Round(trace.Point1.Xd, 7), "Trace move x");
            Assert.AreEqual(Math.Round(0.0, 7), Math.Round(trace.Point1.Yd, 7), "Trace move y");
            Assert.AreEqual(Math.Round(Math.Sqrt(50), 7), Math.Round(trace.Point2.Xd, 7), "Trace move x");
            Assert.AreEqual(Math.Round(Math.Sqrt(50), 7), Math.Round(trace.Point2.Yd, 7), "Trace move y");

            m.Move(10);
            trace = m.GetPerimeter()[0];
            Assert.AreEqual(Math.Round(Math.Sqrt(50), 7), Math.Round(trace.Point1.Xd, 7), "Trace move x");
            Assert.AreEqual(Math.Round(Math.Sqrt(50), 7), Math.Round(trace.Point1.Yd, 7), "Trace move y");
            Assert.AreEqual(Math.Round(2 * Math.Sqrt(50), 7), Math.Round(trace.Point2.Xd, 7), "Trace move x");
            Assert.AreEqual(Math.Round(2 * Math.Sqrt(50), 7), Math.Round(trace.Point2.Yd, 7), "Trace move y");

            m.Move(10);
            trace = m.GetPerimeter()[0];
            Assert.AreEqual(Math.Round(2 * Math.Sqrt(50), 7), Math.Round(trace.Point1.Xd, 7), "Trace move x");
            Assert.AreEqual(Math.Round(2 * Math.Sqrt(50), 7), Math.Round(trace.Point1.Yd, 7), "Trace move y");
            Assert.AreEqual(Math.Round(3 * Math.Sqrt(50), 7), Math.Round(trace.Point2.Xd, 7), "Trace move x");
            Assert.AreEqual(Math.Round(3 * Math.Sqrt(50), 7), Math.Round(trace.Point2.Yd, 7), "Trace move y");

            PlayerVehicle veh1 = new PlayerVehicle(ga);
            Missile miss = new Missile(ga, new Angle(-3.0499999999999972), new MatrixPoint(-7.2369182872595275, 99.435069414788984), 1000);

            trace = miss.GetPerimeter()[0];
            Assert.AreEqual(Math.Round(-7.2369182872595275, 7), Math.Round(trace.Point1.Xd, 7), "Trace2 move x");
            Assert.AreEqual(Math.Round(99.435069414788984, 7), Math.Round(trace.Point1.Yd, 7), "Trace2 move y");
            Assert.AreEqual(Math.Round(-7.2369182872595275, 7), Math.Round(trace.Point2.Xd, 7), "Trace2 move x");
            Assert.AreEqual(Math.Round(99.435069414788984, 7), Math.Round(trace.Point2.Yd, 7), "Trace2 move y");
            Assert.False(miss.DetectProximity(veh1).Collision, "Collison check 1");

            miss.Move(10);
            trace = miss.GetPerimeter()[0];
            Assert.AreEqual(Math.Round(-7.2369182872595275, 7), Math.Round(trace.Point1.Xd, 7), "Trace2 move x");
            Assert.AreEqual(Math.Round(99.435069414788984, 7), Math.Round(trace.Point1.Yd, 7), "Trace2 move y");
            Assert.AreEqual(Math.Round(-6.3222718999999996, 7), Math.Round(trace.Point2.Xd, 7), "Trace2 move x");
            Assert.AreEqual(Math.Round(89.476986199999999, 7), Math.Round(trace.Point2.Yd, 7), "Trace2 move y");
            Assert.False(miss.DetectProximity(veh1).Collision, "Collison check 2");

            miss.Move(10);
            trace = miss.GetPerimeter()[0];
            Assert.AreEqual(Math.Round(-6.3222718999999996, 7), Math.Round(trace.Point1.Xd, 7), "Trace2 move x1");
            Assert.AreEqual(Math.Round(89.476986199999999, 7), Math.Round(trace.Point1.Yd, 7), "Trace2 move y1");
            Assert.AreEqual(Math.Round(-5.4076253999999997, 7), Math.Round(trace.Point2.Xd, 7), "Trace2 move x2");
            Assert.AreEqual(Math.Round(79.5189029, 7), Math.Round(trace.Point2.Yd, 7), "Trace2 move y2");
            Assert.False(miss.DetectProximity(veh1).Collision, "Collison check 3");

            miss.Move(10);
            trace = miss.GetPerimeter()[0];
            Assert.AreEqual(Math.Round(-5.4076253999999997, 7), Math.Round(trace.Point1.Xd, 7), "Trace2 move x1");
            Assert.AreEqual(Math.Round(79.5189029, 7), Math.Round(trace.Point1.Yd, 7), "Trace2 move y1");
            Assert.AreEqual(Math.Round(-4.4929790000000001, 7), Math.Round(trace.Point2.Xd, 7), "Trace2 move x2");
            Assert.AreEqual(Math.Round(69.560819699999996, 7), Math.Round(trace.Point2.Yd, 7), "Trace2 move y2");
            Assert.False(miss.DetectProximity(veh1).Collision, "Collison check 4");

            miss.Move(10);
            miss.Move(10);
            miss.Move(10);
            miss.Move(10);
            miss.Move(10);

            miss.Move(10);
            trace = miss.GetPerimeter()[0];
            Assert.AreEqual(Math.Round(0.080253099999999994, 7), Math.Round(trace.Point1.Xd, 7), "Trace2 move x1");
            Assert.AreEqual(Math.Round(19.7704035, 7), Math.Round(trace.Point1.Yd, 7), "Trace2 move y1");
            Assert.AreEqual(Math.Round(0.99489950000000005, 7), Math.Round(trace.Point2.Xd, 7), "Trace2 move x2");
            Assert.AreEqual(Math.Round(9.8123202000000003, 7), Math.Round(trace.Point2.Yd, 7), "Trace2 move y2");
            Assert.True(miss.DetectProximity(veh1).Collision, "Collison check 5");

            miss.Move(10);
            trace = miss.GetPerimeter()[0];
            Assert.AreEqual(Math.Round(0.99489950000000005, 7), Math.Round(trace.Point1.Xd, 7), "Trace2 move x1");
            Assert.AreEqual(Math.Round(9.8123202000000003, 7), Math.Round(trace.Point1.Yd, 7), "Trace2 move y1");
            Assert.AreEqual(Math.Round(1.9095458999999999, 7), Math.Round(trace.Point2.Xd, 7), "Trace2 move x2");
            Assert.AreEqual(Math.Round(-0.145763, 7), Math.Round(trace.Point2.Yd, 7), "Trace2 move y2");
            Assert.False(miss.DetectProximity(veh1).Collision, "Collison check 6");

            miss.Move(10);
            trace = miss.GetPerimeter()[0];
            Assert.AreEqual(Math.Round(1.9095458999999999, 7), Math.Round(trace.Point1.Xd, 7), "Trace2 move x1");
            Assert.AreEqual(Math.Round(-0.145763, 7), Math.Round(trace.Point1.Yd, 7), "Trace2 move y1");
            Assert.AreEqual(Math.Round(2.8241923999999998, 7), Math.Round(trace.Point2.Xd, 7), "Trace2 move x2");
            Assert.AreEqual(Math.Round(-10.103846300000001, 7), Math.Round(trace.Point2.Yd, 7), "Trace2 move y2");
            Assert.True(miss.DetectProximity(veh1).Collision, "Collison check 7");


        }
    }
}
