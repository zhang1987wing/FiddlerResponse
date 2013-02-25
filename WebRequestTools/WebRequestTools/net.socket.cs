using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace WebRequestTools
{
    class net
    {
        private Socket newclient;
        //private TcpClient tcpclient = new TcpClient();
        //private Thread newThread = null;


        public bool Connect(IPEndPoint ip)
        {
            try
            {
                //byte[] data = new byte[1024];
                newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                newclient.Connect(ip);
            }
            catch (SocketException)
            {
                //MessageBox.Show("连接服务器失败  " + e.Message);
                return false;
            }

            return true;
        }

        public bool Connect(string ip, string port)
        {
            try
            {
                //byte[] data = new byte[1024];
                newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                string ipadd = ip.Trim();
                int portadd = Convert.ToInt32(port);
                IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ipadd), portadd);
                newclient.Connect(ie);
            }
            catch (SocketException)
            {
                //MessageBox.Show("连接服务器失败  " + e.Message);
                return false;
            }

            return true;
        }
    }
}
