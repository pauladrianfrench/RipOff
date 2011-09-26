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
        static bool left;
        static bool right;
        static bool drive;

        public Form1()
        {
            InitializeComponent();

            DrawParams dp = new DrawParams();

            dp.Graphics = this.CreateGraphics();
            dp.Graphics.Clear(Color.White);
            dp.Trans = new Trans { XScale = 1, YScale = 1, Origin = new Point(500, 250) };
            dp.Pen = new Pen(Color.Blue);
            dp.FillBrush = new SolidBrush(Color.GreenYellow);

            time = new Timer();
            time.Enabled = false;
            time.Interval = 25;
            time.Tick += UpdateGameArea;

            gameArea = new GameArea(dp);
            gameArea.Draw();

            left = false;
            right = false;
            drive = false;
            this.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gameArea.Draw();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            gameArea.Draw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            time.Enabled = (time.Enabled) ? false : true;
        }

        static void UpdateGameArea(Object sender, EventArgs e)
        {
            if (drive)
            {
                gameArea.Vehicle.Figure.Drive(10);
            }
            if (left)
            {
                gameArea.Vehicle.Figure.Rotate(0.05);
            }

            if (right)
            {
                gameArea.Vehicle.Figure.Rotate(-0.05);
            }
            gameArea.DrawParam.Graphics.Clear(Color.White);
            gameArea.Draw();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'a')
            {
                left = true;
                right = false;
            }
            if (e.KeyChar == 'd')
            {
                right = true;
                left = false;
            }
            if (e.KeyChar == 'l')
            {
                drive = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.A)
            {
                left = false;
            }
            if (e.KeyData == Keys.D)
            {
                right = false;
            }
            if (e.KeyData == Keys.L)
            {
                drive = false;
            }
        }
    }
}
