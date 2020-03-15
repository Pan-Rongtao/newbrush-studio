using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace studio
{
    class TcpServer
    {
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Tcp);
        public TcpServer()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            server.Bind(ep);
            server.Listen(100);
        }
    }
}
