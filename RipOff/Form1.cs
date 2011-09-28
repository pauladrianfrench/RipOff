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
            time.Enabled = true;
            time.Interval = 25;
            time.Tick += UpdateGameArea;

            gameArea = new GameArea(dp);
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
    }
}
