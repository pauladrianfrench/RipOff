namespace RipOff
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.IO;
    using System.Threading;

    public static class GameServer
    {
        public static GameController GameController { get; set; }
        
        public static void Server()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 51111);
            listener.Start();

            using (TcpClient c = listener.AcceptTcpClient())
            using (NetworkStream n = c.GetStream())
            {
                string msg = new BinaryReader(n).ReadString();

                if (msg == "a" || msg == "A")
                {
                    ActionParams ap = new ActionParams();

                    ap.A = true;
                    ap.D = false;
                    ap.J = false;
                    ap.L = false;
                    ap.N = false;

                    GameController.KeyDown(ap);
                }
                else if (msg == "d" || msg == "D")
                {
                    ActionParams ap = new ActionParams();

                    ap.A = false;
                    ap.D = true;
                    ap.J = false;
                    ap.L = false;
                    ap.N = false;

                    GameController.KeyDown(ap);
                }
            }
            listener.Stop();
        }
    }
}
