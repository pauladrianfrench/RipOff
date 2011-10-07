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
            Line line1 = new Line(new MatrixPoint(0, 5), new MatrixPoint(5, 10) );
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


            Assert.AreEqual(Math.Round(Math.PI/4, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p1).Radians, 9), "Orientation check 1");
            Assert.AreEqual(Math.Round(3 * Math.PI / 4, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p2).Radians, 9), "Orientation check 2");
            Assert.AreEqual(Math.Round(5 * Math.PI / 4, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p3).Radians, 9), "Orientation check 3");
            Assert.AreEqual(Math.Round(7 * Math.PI / 4, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p4).Radians, 9), "Orientation check 4");

            Assert.AreEqual(Math.Round(0.0, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p5).Radians,9), "Orientation check 5");
            Assert.AreEqual(Math.Round(Math.PI/2, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p6).Radians,9), "Orientation check 6");
            Assert.AreEqual(Math.Round(Math.PI, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p7).Radians,9), "Orientation check 7");
            Assert.AreEqual(Math.Round(Math.PI + Math.PI/2, 9), Math.Round(MatrixPoint.OrientationBetween(centre, p8).Radians, 9), "Orientation check 8");
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
            Assert.AreEqual(Math.Round(Math.PI + 1.5 , 9), Math.Round(a6.Radians, 9), "Angle 6 test");

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

    }
}
