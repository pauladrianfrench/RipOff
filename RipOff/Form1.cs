using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RipOff
{
    public partial class Form1 : Form
    {
        static GameController gameArea;
        Timer time;

        public Form1()
        {
            this.DoubleBuffered = true;

            InitializeComponent();

            DrawParams dp = new DrawParams();

            dp.Graphics = this.CreateGraphics();
            dp.Graphics.Clear(Color.White);
            dp.Trans = new Trans { XScale = 1, YScale = 1, Origin = new Point(510, 315) };
            dp.Pen = new Pen(Color.Blue);
            dp.FillBrush = new SolidBrush(Color.GreenYellow);

            time = new Timer();
            time.Enabled = true;
            time.Interval = 30;
            time.Tick += UpdateGameArea;

            gameArea = new GameController();
            gameArea.DrawParam = dp;

            PlayerVehicle veh1 = new PlayerVehicle(gameArea);
            veh1.Centre = new MatrixPoint(350, -200);

            FuelCell fuelCell1 = new FuelCell(gameArea);
            fuelCell1.Centre = new MatrixPoint(-20, 40);

            FuelCell fuelCell2 = new FuelCell(gameArea);
            fuelCell2.Centre = new MatrixPoint(20, 40);

            FuelCell fuelCell3 = new FuelCell(gameArea);
            fuelCell3.Centre = new MatrixPoint(20, -40);

            FuelCell fuelCell4 = new FuelCell(gameArea);
            fuelCell4.Centre = new MatrixPoint(-20, -40);

            FuelCell fuelCell5 = new FuelCell(gameArea);
            fuelCell5.Centre = new MatrixPoint(-40, 20);

            FuelCell fuelCell6 = new FuelCell(gameArea);
            fuelCell6.Centre = new MatrixPoint(40, 20);

            FuelCell fuelCell7 = new FuelCell(gameArea);
            fuelCell7.Centre = new MatrixPoint(40, -20);

            FuelCell fuelCell8 = new FuelCell(gameArea);
            fuelCell8.Centre = new MatrixPoint(-40, -20);

            WayPoint p1 = new WayPoint(gameArea);
            p1.Centre = new MatrixPoint(-150, 150);

            WayPoint p2 = new WayPoint(gameArea);
            p2.Centre = new MatrixPoint(250, -150);

            WayPoint p3 = new WayPoint(gameArea);
            p3.Centre = new MatrixPoint(-250, -250);

            WayPoint p4 = new WayPoint(gameArea);
            p4.Centre = new MatrixPoint(35, 85);

            WayPoint p5 = new WayPoint(gameArea);
            p5.Centre = new MatrixPoint(100, 150);

            WayPoint p6 = new WayPoint(gameArea);
            p6.Centre = new MatrixPoint(10, 150);

            IMission miss1 = new Mission();
            miss1.Targets.Add(new MissionTarget(p1, MissionObjective.Visit));
            miss1.Targets.Add(new MissionTarget(veh1, MissionObjective.Attack));
            miss1.Targets.Add(new MissionTarget(fuelCell1, MissionObjective.Collect));
            miss1.Targets.Add(new MissionTarget(p2, MissionObjective.Visit));
            miss1.Targets.Add(new MissionTarget(p4, MissionObjective.Visit));

            IMission miss2 = new Mission();
            miss2.Targets.Add(new MissionTarget(p2, MissionObjective.Visit));
            miss2.Targets.Add(new MissionTarget(fuelCell2, MissionObjective.Collect));
            miss2.Targets.Add(new MissionTarget(p3, MissionObjective.Visit));
            miss2.Targets.Add(new MissionTarget(p4, MissionObjective.Visit));

            IMission miss3 = new Mission();
            miss3.Targets.Add(new MissionTarget(p3, MissionObjective.Visit));
            miss3.Targets.Add(new MissionTarget(veh1, MissionObjective.Attack));
            miss3.Targets.Add(new MissionTarget(fuelCell3, MissionObjective.Collect));
            miss3.Targets.Add(new MissionTarget(p4, MissionObjective.Visit));
            miss3.Targets.Add(new MissionTarget(p5, MissionObjective.Visit));

            IMission miss4 = new Mission();
            miss4.Targets.Add(new MissionTarget(p4, MissionObjective.Visit));
            miss4.Targets.Add(new MissionTarget(veh1, MissionObjective.Attack));
            miss4.Targets.Add(new MissionTarget(fuelCell4, MissionObjective.Collect));
            miss4.Targets.Add(new MissionTarget(p5, MissionObjective.Visit));
            miss4.Targets.Add(new MissionTarget(p6, MissionObjective.Visit));

            IMission miss5 = new Mission();
            miss5.Targets.Add(new MissionTarget(p4, MissionObjective.Visit));
            miss5.Targets.Add(new MissionTarget(veh1, MissionObjective.Attack));
            miss5.Targets.Add(new MissionTarget(fuelCell5, MissionObjective.Collect));
            miss5.Targets.Add(new MissionTarget(p3, MissionObjective.Visit));
            miss5.Targets.Add(new MissionTarget(p2, MissionObjective.Visit));

            IMission miss6 = new Mission();
            miss6.Targets.Add(new MissionTarget(p6, MissionObjective.Visit));
            miss6.Targets.Add(new MissionTarget(veh1, MissionObjective.Attack));
            miss6.Targets.Add(new MissionTarget(fuelCell6, MissionObjective.Collect));
            miss6.Targets.Add(new MissionTarget(p2, MissionObjective.Visit));
            miss6.Targets.Add(new MissionTarget(p1, MissionObjective.Visit));

            IMission miss7 = new Mission();
            miss7.Targets.Add(new MissionTarget(p5, MissionObjective.Visit));
            miss7.Targets.Add(new MissionTarget(fuelCell7, MissionObjective.Collect));
            miss7.Targets.Add(new MissionTarget(p3, MissionObjective.Visit));
            miss7.Targets.Add(new MissionTarget(p4, MissionObjective.Visit));

            IMission miss8 = new Mission();
            miss8.Targets.Add(new MissionTarget(p3, MissionObjective.Visit));
            miss8.Targets.Add(new MissionTarget(veh1, MissionObjective.Attack));
            miss8.Targets.Add(new MissionTarget(fuelCell8, MissionObjective.Collect));
            miss8.Targets.Add(new MissionTarget(p6, MissionObjective.Visit));
            miss8.Targets.Add(new MissionTarget(p1, MissionObjective.Visit));

            gameArea.CollectMission(miss1);
            gameArea.CollectMission(miss2);
            gameArea.CollectMission(miss3);
            gameArea.CollectMission(miss4);
            gameArea.CollectMission(miss5);
            gameArea.CollectMission(miss6);
            gameArea.CollectMission(miss7);
            gameArea.CollectMission(miss8);

            new EnemyTank(gameArea).Centre = new MatrixPoint(-300, 900);
            new EnemyTank(gameArea).Centre = new MatrixPoint(400, 400);
            new EnemyTank(gameArea).Centre = new MatrixPoint(700, 80);
            new EnemyTank(gameArea).Centre = new MatrixPoint(-900, 900);
            new EnemyTank(gameArea).Centre = new MatrixPoint(1500, 1700);
            new EnemyTank(gameArea).Centre = new MatrixPoint(-2500, 3000);
            new EnemyTank(gameArea).Centre = new MatrixPoint(300, 600);
            new EnemyTank(gameArea).Centre = new MatrixPoint(2000, 200);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gameArea.Draw();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            gameArea.Draw();
        }

        static void UpdateGameArea(Object sender, EventArgs e)
        {
            gameArea.Update();
            gameArea.DrawParam.Graphics.Clear(Color.White);
            gameArea.Draw();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ActionParams ap = new ActionParams();

            ap.A = e.KeyChar == 'a';
            ap.D = e.KeyChar == 'd';
            ap.J = e.KeyChar == 'j';
            ap.L = e.KeyChar == 'l';
            ap.N = e.KeyChar == 'n';

            gameArea.KeyDown(ap);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            ActionParams ap = new ActionParams();

            ap.A = e.KeyData == Keys.A;
            ap.D = e.KeyData == Keys.D;
            ap.L = e.KeyData == Keys.L;
            ap.J = e.KeyData == Keys.J;
            ap.N = e.KeyData == Keys.N;

            gameArea.KeyUp(ap);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PlayerVehicle veh1 = new PlayerVehicle(gameArea);
            gameArea.AddGameObject(veh1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random rand = new Random();

            Box box1 = new Box(gameArea);
            box1.Centre = new MatrixPoint(-100 + rand.NextDouble() * 100, -100 + rand.NextDouble() * 100);


            Box box2 = new Box(gameArea);
            box2.Centre = new MatrixPoint(-100 + rand.NextDouble() * 100, 100 + rand.NextDouble() * 100);


            Box box3 = new Box(gameArea);
            box3.Centre = new MatrixPoint(100, 100 + rand.NextDouble() * 100 + rand.NextDouble() * 100);


            Box box4 = new Box(gameArea);
            box4.Centre = new MatrixPoint(100 + rand.NextDouble() * 100, -100 + rand.NextDouble() * 100);


            Box box5 = new Box(gameArea);
            box5.Centre = new MatrixPoint(200, -70 + rand.NextDouble() * 100 + rand.NextDouble() * 100);


            Box box6 = new Box(gameArea);
            box6.Centre = new MatrixPoint(200 + rand.NextDouble() * 100, -150 + rand.NextDouble() * 100);

        }

        private void button3_Click(object sender, EventArgs e)
        {

            EnemyTank et1 = new EnemyTank(gameArea);
            et1.Centre = new MatrixPoint(-400, 0);

        }
    }
}
