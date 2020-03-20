using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace studio
{
    class TcpServer
    {
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> Connections = new List<Socket>();
        public void startup()
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            server.Bind(ep);
            server.Listen(10);

            Thread listernThread = new Thread(listenClient);
            listernThread.IsBackground = true;
            listernThread.Start();
            Console.WriteLine("开始监听...");
        }

        public void send(String s)
        {
            if (Connections.Count == 0)
            {
                return;
            }
            byte[] bytes = System.Text.Encoding.Default.GetBytes(s);
            Connections[0].Send(bytes);
        }

        void listenClient()
        {
            Socket connection = null;
            while(true)
            {
                try
                {
                    connection = server.Accept();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
                IPAddress clientIp = (connection.RemoteEndPoint as IPEndPoint).Address;
                int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;
                Console.WriteLine("新的客户端接入：" + clientIp + ":" + clientPort);

                Thread connecttionRecThread = new Thread(Recv);
                connecttionRecThread.IsBackground = true;
                connecttionRecThread.Start(connection);
                Connections.Add(connection);
            }
        }

        void Recv(object o)
        {
            Socket connection = o as Socket;
            while(true)
            {
                byte[] buffer = new byte[1024 * 1024 * 3];
                try
                {
                    int len = connection.Receive(buffer);
                    if (len == 0)
                    {
                        break;
                    }
                    string str = Encoding.UTF8.GetString(buffer, 0, len);
                    Console.WriteLine("接收到的客户端数据：" + connection.RemoteEndPoint + ":" + str);
                }
                catch(Exception e)
                {
                    Connections.Remove(connection);
                    break;
                }

            }
        }
    }
}
