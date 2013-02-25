using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace 读写ini文件
{
    public class Ini
    {
        // 声明INI文件的写操作函数 WritePrivateProfileString()

        [System.Runtime.InteropServices.DllImport("kernel32")]

        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        // 声明INI文件的读操作函数 GetPrivateProfileString()

        [System.Runtime.InteropServices.DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section, string key, string def, System.Text.StringBuilder retVal, int size, string filePath);


        private string sPath = null;
        public Ini(string path)
        {
            this.sPath = path;
        }

        public void Writue(string section, string key, string value)
        {

            // section=配置节，key=键名，value=键值，path=路径

            WritePrivateProfileString(section, key, value, sPath);

        }
        public string ReadValue(string section, string key)
        {

            // 每次从ini中读取多少字节

            System.Text.StringBuilder temp = new System.Text.StringBuilder(255);

            // section=配置节，key=键名，temp=上面，path=路径

            GetPrivateProfileString(section, key, "", temp, 255, sPath);

            return temp.ToString();

        }




    }
    class Program
    {
        static void Mainini(string[] args)
        {
            string Current;

            Current = Directory.GetCurrentDirectory();//获取当前根目录
            Console.WriteLine("Current directory {0}", Current);
            // 写入ini
            Ini ini = new Ini(Current + "/config.ini");
            ini.Writue("Setting", "key1", "hello word!");
            ini.Writue("Setting", "key2", "hello ini!");
            ini.Writue("SettingImg", "Path", "IMG.Path");
            // 读取ini
            string stemp = ini.ReadValue("Setting", "key2");
            Console.WriteLine(stemp);



            Console.ReadKey();
        }

    }
}


