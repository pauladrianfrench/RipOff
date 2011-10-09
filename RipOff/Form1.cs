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
        static GameArea gameArea;
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
            time.Interval = 500;
            time.Tick += UpdateGameArea;

            gameArea = new GameArea();
            gameArea.DrawParam = dp;

            FuelCell cell1 = new FuelCell(gameArea);
            cell1.Centre = new MatrixPoint(150, 150);
           
            PlayerVehicle veh1 = new PlayerVehicle(gameArea);
           
            EnemyTank et1 = new EnemyTank(gameArea);
            et1.Centre = new MatrixPoint(-400, 0);

            WayPoint p1 = new WayPoint(gameArea);
            p1.Centre = new MatrixPoint(-150, 150);

            WayPoint p2 = new WayPoint(gameArea);
            p2.Centre = new MatrixPoint(250, -150);

            IMission miss1 = new Mission();
            miss1.Targets.Add(new MissionTarget(p1, MissionObjective.Visit));
            miss1.Targets.Add(new MissionTarget(veh1, MissionObjective.Attack));
            miss1.Targets.Add(new MissionTarget(cell1, MissionObjective.Collect));
            miss1.Targets.Add(new MissionTarget(p2, MissionObjective.Visit));

            et1.Mission = miss1;
            
            FuelCell fuelCell1 = new FuelCell(gameArea);
            fuelCell1.Centre = new MatrixPoint(-100, -100);
           
            FuelCell fuelCell2 = new FuelCell(gameArea);
            fuelCell2.Centre = new MatrixPoint(-100, 100);
            
            FuelCell fuelCell3 = new FuelCell(gameArea);
            fuelCell3.Centre = new MatrixPoint(100, 100);

            FuelCell fuelCell4 = new FuelCell(gameArea);
            fuelCell4.Centre = new MatrixPoint(100, -100);

            FuelCell fuelCell5 = new FuelCell(gameArea);
            fuelCell5.Centre = new MatrixPoint(200, -70);
           
            FuelCell fuelCell6 = new FuelCell(gameArea);
            fuelCell6.Centre = new MatrixPoint(200, -150);
              
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
