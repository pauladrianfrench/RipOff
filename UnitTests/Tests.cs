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
            GameArea gameArea = new GameArea();

            PlayerVehicle veh1 = new PlayerVehicle(gameArea);
            gameArea.AddGameObject(veh1);

            Box box = new Box(gameArea);
            
            gameArea.AddGameObject(box);
            Assert.AreEqual(box.DetectProximity(veh1), ProximityResult.Hit, "Collision detection false negative");

            box.Centre = new MatrixPoint(-150, -150);
            Assert.AreEqual(box.DetectProximity(veh1), ProximityResult.Missed,"Collision detection false positive");
        }

        [Test]
        public void TestOrientationCalcs()
        {
            MatrixPoint centre = new MatrixPoint(0, 0);

            MatrixPoint p1 = new MatrixPoint(4, 5);
            MatrixPoint p2 = new MatrixPoint(5, -4);
            MatrixPoint p3 = new MatrixPoint(-4, -5);
            MatrixPoint p4 = new MatrixPoint(-5, 4);

            MatrixPoint p5 = new MatrixPoint(0.1, 4);
            MatrixPoint p6 = new MatrixPoint(0.1, -4);
            MatrixPoint p7 = new MatrixPoint(-0.1, -4);
            MatrixPoint p8 = new MatrixPoint(-0.1, 4);


            double res1 = MatrixPoint.OrientationBetween(centre, p1);
            double res2 = MatrixPoint.OrientationBetween(centre, p2);
            double res3 = MatrixPoint.OrientationBetween(centre, p3);
            double res4 = MatrixPoint.OrientationBetween(centre, p4);

            double res5 = MatrixPoint.OrientationBetween(centre, p5);
            double res6 = MatrixPoint.OrientationBetween(centre, p6);
            double res7 = MatrixPoint.OrientationBetween(centre, p7);
            double res8 = MatrixPoint.OrientationBetween(centre, p8);
        }
    }
}
