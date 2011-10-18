namespace RipOffClient
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using RipOff;

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ActionParams ap = new ActionParams();

            ap.A = e.KeyChar == 'a';
            ap.D = e.KeyChar == 'd';
            ap.J = e.KeyChar == 'j';
            ap.L = e.KeyChar == 'l';
            ap.N = e.KeyChar == 'n';

            GameClient.Client();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            ActionParams ap = new ActionParams();

            ap.A = e.KeyData == Keys.A;
            ap.D = e.KeyData == Keys.D;
            ap.L = e.KeyData == Keys.L;
            ap.J = e.KeyData == Keys.J;
            ap.N = e.KeyData == Keys.N;
        }
    }
}
