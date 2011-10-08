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
            InitializeComponent();

            DrawParams dp = new DrawParams();

            dp.Graphics = this.CreateGraphics();
            dp.Graphics.Clear(Color.White);
            dp.Trans = new Trans { XScale = 1, YScale = 1, Origin = new Point(510, 315) };
            dp.Pen = new Pen(Color.Blue);
            dp.FillBrush = new SolidBrush(Color.GreenYellow);

            time = new Timer();
            time.Enabled = true;
            time.Interval = 25;
            time.Tick += UpdateGameArea;

            gameArea = new GameArea();
            gameArea.DrawParam = dp;

            FuelCell cell1 = new FuelCell(gameArea);
            cell1.Centre = new MatrixPoint(150, 150);
            gameArea.AddGameObject(cell1);

            IMission miss1 = new Mission { Target = cell1, Complete = false, EndPoint = new MatrixPoint(0, 700) };
            gameArea.CollectMission(miss1);

            PlayerVehicle veh1 = new PlayerVehicle(gameArea);
            gameArea.AddGameObject(veh1);

            EnemyTank et1 = new EnemyTank(gameArea);
            et1.Centre = new MatrixPoint(-400, 0);
            

            gameArea.AddGameObject(et1);

          
            
            //Box box1 = new Box(gameArea);
            //box1.Centre = new MatrixPoint(-100, -100);
            //gameArea.AddGameObject(box1);

            //Box box2 = new Box(gameArea);
            //box2.Centre = new MatrixPoint(-100, 100);
            //gameArea.AddGameObject(box2);

            //Box box3 = new Box(gameArea);
            //box3.Centre = new MatrixPoint(100, 100);
            //gameArea.AddGameObject(box3);

            //Box box4 = new Box(gameArea);
            //box4.Centre = new MatrixPoint(100, -100);
            //gameArea.AddGameObject(box4);

            //Box box5 = new Box(gameArea);
            //box5.Centre = new MatrixPoint(200, -70);
            //gameArea.AddGameObject(box5);

            //Box box6 = new Box(gameArea);
            //box6.Centre = new MatrixPoint(200, -150);
            //gameArea.AddGameObject(box6);
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
            gameArea.AddGameObject(box1);

            Box box2 = new Box(gameArea);
            box2.Centre = new MatrixPoint(-100 + rand.NextDouble() * 100, 100 + rand.NextDouble() * 100);
            gameArea.AddGameObject(box2);

            Box box3 = new Box(gameArea);
            box3.Centre = new MatrixPoint(100, 100 + rand.NextDouble() * 100 + rand.NextDouble() * 100);
            gameArea.AddGameObject(box3);

            Box box4 = new Box(gameArea);
            box4.Centre = new MatrixPoint(100 + rand.NextDouble() * 100, -100 + rand.NextDouble() * 100);
            gameArea.AddGameObject(box4);

            Box box5 = new Box(gameArea);
            box5.Centre = new MatrixPoint(200, -70 + rand.NextDouble() * 100 + rand.NextDouble() * 100);
            gameArea.AddGameObject(box5);

            Box box6 = new Box(gameArea);
            box6.Centre = new MatrixPoint(200 + rand.NextDouble() * 100, -150 + rand.NextDouble() * 100);
            gameArea.AddGameObject(box6);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EnemyTank et1 = new EnemyTank(gameArea);
            et1.Centre = new MatrixPoint(-400, 0);

            gameArea.AddGameObject(et1);
        }
    }
}
