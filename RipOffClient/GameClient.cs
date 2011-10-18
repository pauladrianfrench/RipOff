
namespace RipOffClient
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.IO;
    using System.Threading;

    public class GameClient
    {
        public static void Client()
        {
            using (TcpClient client = new TcpClient("localhost", 51111))
            using (NetworkStream n = client.GetStream())
            {
                BinaryWriter w = new BinaryWriter(n);
                w.Write("Hello shitbag");
                w.Flush();

                BinaryReader read = new BinaryReader(n);
                string res = read.ReadString();
            }
        }
    }
}
