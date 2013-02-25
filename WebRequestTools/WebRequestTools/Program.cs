using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace WebRequestTools
{

    //public struct config
    //{
    //    public static string proxyip;
    //}

    public struct browser
    {
        public static string text;
        public static Stream stream;
    }
    public struct key
    {
        public static string login_pwd;
        public static string md5;
        public static string AES;
        public static string AuthAES;
    }
    public struct proxyinfo
    {
        public static string filename = "proxylist.txt";
        public static string filepath = System.AppDomain.CurrentDomain.BaseDirectory;
    }
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Webrequset());

        }
    }
}
