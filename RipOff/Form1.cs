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
            dp.Trans = new Trans { XScale = 1, YScale = 1, Origin = new Point(500, 250) };
            dp.Pen = new Pen(Color.Blue);
            dp.FillBrush = new SolidBrush(Color.GreenYellow);

            time = new Timer();
            time.Enabled = false;
            time.Interval = 1;
            time.Tick += UpdateGameArea;

            gameArea = new GameArea(dp);
            gameArea.Draw();
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
            gameArea.Vehicle.Figure.Drive(5);
            gameArea.Vehicle.Figure.Rotate(0.05);
            gameArea.DrawParam.Graphics.Clear(Color.White);
            gameArea.Draw();
        }

    }
}
