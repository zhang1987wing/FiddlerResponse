using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms.Design;
using System.Windows.Forms;

namespace WebRequestTools
{

    //一般选择文件保存地址都用弹出对话框来进行选择
    public class DirectorySelect : FolderNameEditor
    {
        private FolderBrowser fb = new FolderBrowser();
        private string fDescription = "Choose Directory";
        private string fReturnPath = String.Empty;

        public string Description
        {
            set { fDescription = value; }
            get { return fDescription; }
        }

        public string ReturnPath
        {
            get { return fReturnPath; }
        }

        private DialogResult RunDialog()
        {
            fb.Description = this.Description;
            fb.StartLocation = FolderBrowserFolder.MyComputer;
            fb.Style = FolderBrowserStyles.RestrictToSubfolders;
            //|FolderBrowserStyles.RestrictToDomain;
            return fb.ShowDialog();
        }

        public DialogResult ShowDialog()
        {
            DialogResult dRes = DialogResult.None;
            dRes = RunDialog();
            if (dRes == DialogResult.OK)
                this.fReturnPath = fb.DirectoryPath;
            else
                this.fReturnPath = String.Empty;
            return dRes;
        }
    }

    public class DirBrowser : FolderNameEditor
    {
        //一般选择文件保存地址都用弹出对话框来进行选择
        //调用   
        //DirBrowser   myDirBrowser=new   DirBrowser();   
        //if(myDirBrowser.ShowDialog()!=DialogResult.Cancel)   
        //MessageBox.Show(myDirBrowser.ReturnPath);  
        FolderBrowser fb = new FolderBrowser();
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }

        public string ReturnPath
        {
            get { return _returnPath; }
        }

        public DirBrowser() { }
        public DialogResult ShowDialog()
        {
            fb.Description = _description;
            fb.StartLocation = FolderBrowserFolder.MyComputer;
            DialogResult r = fb.ShowDialog();
            if (r == DialogResult.OK)
                _returnPath = fb.DirectoryPath;
            else
                _returnPath = String.Empty;

            return r;
        }

        //private   string   _description   =   "Choose   Directory";     
        //private   string   _returnPath   =   String.Empty; 
        private string _description = "请选择文件夹";
        private string _returnPath = String.Empty;
    }

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

        public void WriteValue(string section, string key, string value)
        {
            // section=配置节，key=键名，value=键值，path=路径
            WritePrivateProfileString(section, key, value, sPath);
        }
        public string ReadValue(string section, string key)
        {
            // 每次从ini中读取多少字节
            System.Text.StringBuilder temp = new System.Text.StringBuilder(1024);
            // section=配置节，key=键名，temp=上面，path=路径
            GetPrivateProfileString(section, key, "", temp, 1024, sPath);
            return temp.ToString();
        }
    }

    public class Txt
    {
        static string filePath = System.AppDomain.CurrentDomain.BaseDirectory;
        static string filename = "";

        public static void writetxtfile(string des)
        {
            filename = Coding.randomhex(16) + ".txt";
            writetxtfile(des, filePath + filename);
        }

        public static void writetxtfile(string des, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                sw.Write(des);
            }
            catch (Exception ex)
            {
                MessageBox.Show(path + "写入失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
            }
            finally
            {
                sw.Close();
                fs.Close();
            }
        }

        public static bool ReadtxtToDict(string path, ref Dictionary<int, string> dict)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                string txt;
                int i = 0;
                do
                {
                    i += 1;
                    txt = sr.ReadLine();
                    if (txt.Length > 0)
                    {
                        dict.Add(i, txt);
                    }
                } while (!sr.EndOfStream);
            }
            catch (Exception)
            {
                //MessageBox.Show(path + "\r\n读取失败\r\n" + e.Message + "\r\n" + e.StackTrace);
                return false;
            }
            return true;
        }
    }

    class Program2
    {
        static void Mainini(string[] args)
        {
            string Current;

            Current = Directory.GetCurrentDirectory();//获取当前根目录
            Console.WriteLine("Current directory {0}", Current);
            // 写入ini
            Ini ini = new Ini(Current + "/config.ini");
            ini.WriteValue("Setting", "key1", "hello word!");
            ini.WriteValue("Setting", "key2", "hello ini!");
            ini.WriteValue("SettingImg", "Path", "IMG.Path");
            // 读取ini
            string stemp = ini.ReadValue("Setting", "key2");
            Console.WriteLine(stemp);



            Console.ReadKey();
        }

    }
}


