using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Security.Cryptography;
using System.Threading;
using System.Diagnostics;

namespace WebRequestTools
{
    public partial class Webrequset : Form
    {
        static int tab_rowmax = 50;
        static int tab_colmax = 5;
        private int tab_url_rowcount = 0;
        private int tab_body_rowcount = 0;
        private int tab_header_rowcount = 0;
        private int file_count = 0;
        private string[,] tab_url_data;
        private string[,] tab_body_data;
        private string[,] tab_header_data;
        private string[] default_col = new string[] { "", "name_", "value_", "type_" };

        static string RequestMethod;
        private string Contentline;
        private int LoginControlSelectedIndex = 0;
        Dictionary<int, string> cookie_login_list = new Dictionary<int, string>();
        Dictionary<int, string> cookie_other_list = new Dictionary<int, string>();
        Dictionary<int, string> userinfo = new Dictionary<int, string>();
        Dictionary<int, string> usercookie = new Dictionary<int, string>();
        private bool import_users_flag = false;
        private bool import_cookies_flag = false;
        private int submitstate = 0;

        Point config_panel_location = new Point(839, 288);
        //private int continue_time_value ;
        //private int continue_speed_value ;
        private int continue_timeunit = 2;     //0.无限制//1.秒//2.分钟//3.小时
        private int continue_speedunit = 5;    //0.无限制//1.秒//2.分钟//3.小时//4.毫秒/次//5.秒/次//6.分钟/次//7.小时/次
        private TimeSpan continue_times_value;
        private int continue_interval_millisecond = 0;

        private bool RequestEncoding_autoheader;
        private Encoding RequestEncoding;
        private Encoding ResponseEncoding;
        //private string Login_type;

        //private int thread_count = 0;
        List<Thread> listThread = new List<Thread>();
        private delegate void MyDelegate(int Delegate_no);
        private delegate void MyInvoke(string msg);
        private delegate void MyInvokeBt(string msg);

        //proxy参数
        private Dictionary<int, string> proxylist = new Dictionary<int, string>();
        private bool proxyopen = false;
        private int proxytype = 0;
        private int proxyid = 0;

        //static byte[] last_Response_Buffer;
        //static Stream laststream;
        //string[,] tab_url_data = new string[5, 50];
        //string[,] tab_body_data = new string[5, 50];
        //string[,] tab_header_data = new string[5, 50];

        public Webrequset()
        {
            InitializeComponent();
        }

        private void Webrequset_Load(object sender, EventArgs e)
        {
#if DEBUG
            testcode.Visible = true;
            testcode2.Visible = true;
            testcode3.Visible = true;
            textBox1.Visible = true;
            http_url.Text = "http://www.baidu.com/";
#endif
            Webrequset.CheckForIllegalCrossThreadCalls = false;
            this.Size = new Size(1050, 700);
            http_Method.SelectedItem = "自动";
            http_Version.SelectedItem = "HTTP/1.1";
            tiancity_login_server.SelectedIndex = 0;
            Config_Load();
            proxy_load();
            //Login_type = "tiancity";
            continue_numbers_input.Text = "5";
            continue_times_input.Text = "1";
            continue_speed_input.Text = "1";
            continue_times_unit.SelectedIndex = 2;
            continue_speed_unit.SelectedIndex = 5;
            //continue_numbers.Checked = true;
        }

        public void Config_Load()
        {
            //Proxyip = Settings.Default.http_proxyip;
            RequestEncoding_autoheader = Settings.Default.RequestEncoding_autoheader;
            try
            {
                if (Settings.Default.RequestEncoding.Length == 0)
                {
                    RequestEncoding = null;
                }
                else
                {
                    RequestEncoding = Encoding.GetEncoding(Settings.Default.RequestEncoding);
                }
            }
            catch (Exception e)
            {
                RequestEncoding = Encoding.Default;
                MessageBox.Show(e.Message);
            }
            try
            {
                ResponseEncoding = Encoding.GetEncoding(Settings.Default.ResponseEncoding);
            }
            catch (Exception e)
            {
                ResponseEncoding = Encoding.Default;
                MessageBox.Show(e.Message);
            }
        }

        public void proxy_load()
        {
            proxytype = Settings.Default.proxy_type;
            if (proxytype == 0)
            { return; }
            proxyopen = true;
            string proxylists = "";
            string msg = "";
            int i = 0;
            if (proxytype == 1 || proxytype == 2) { proxylists = Settings.Default.proxy_all; }
            else if (proxytype == 3 || proxytype == 4) { proxylists = Settings.Default.proxy_use; }
            if (proxytype == 1 || proxytype == 3) { msg = "循环"; }
            else { msg = "随机"; }
            if (proxylists.Length > 0)
            {
                proxylist.Clear();
                output_text.Text = "";
                foreach (string str in proxylists.Split(';', ','))
                {
                    if (str.Length > 0)
                    {
                        i++;
                        proxylist.Add(i, str);
                        UpdateMsgTextBoxAdd(str);
                    }
                }
                UpdateMsgTextBoxAdd("已设置以上代理（" + msg + "）" + i + "个");
            }
            else
            {
                proxyopen = false;
            }
            //string filename = proxyinfo.filename;
            //string filepath = proxyinfo.filepath;
            //int i = 0;
            //if (File.Exists(@filepath + @filename))
            //{
            //    try
            //    {
            //        FileStream fs = new FileStream(@filepath + @filename, FileMode.Open);
            //        StreamReader sr = new StreamReader(fs);
            //        string linetxt = "";
            //        do
            //        {
            //            linetxt = sr.ReadLine();
            //            if (linetxt == null) { linetxt = ""; }
            //            if (linetxt.Length > 0)
            //            {
            //                proxylist.Add(i, linetxt);
            //                if (proxytype == 1 && i == 0)
            //                { break; }
            //                i++;
            //            }
            //        } while (!sr.EndOfStream);
            //        sr.Close();
            //        fs.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        UpdateMsgTextBoxAdd(filename + "读取失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
            //    }
            //}
        }
        public void proxy_load(Dictionary<int, string> proxytmplist)
        {
            //if (proxylist != null) { proxylist.Clear(); }
            //proxytype = Settings.Default.proxy_type;
            //if (proxytype > 0)
            //{
            //    proxyopen = true;
            //    if (proxytype == 1)
            //    {
            //    if (selecteditem.Length == 0)
            //    {
            //        proxyopen = false;
            //        proxytype = 0;
            //        Settings.Default.proxy_type = 0;
            //        UpdateMsgTextBoxAdd("未选择代理，已取消代理");
            //    }
            //    else
            //    {
            //        proxylist.Add(0, selecteditem);
            //        UpdateMsgTextBoxAdd("已设置代理：" + selecteditem);
            //    }
            //    }
            //    else
            //    {
            //        proxylist = proxytmplist;
            //        UpdateMsgTextBoxAdd("已设置批量代理");
            //    }
            //}
            //else
            //{
            //    proxyopen = false;
            //}
        }
        public string getproxy()
        {
            //使用顺序代理13
            //使用随机代理24
            string proxystring = "";
            Random rd = new Random();
            switch (proxytype)
            {
                case 1:
                    proxyid += 1;
                    proxystring = proxylist[proxyid];
                    if (proxyid == proxylist.Count) { proxyid = 0; }
                    break;
                case 2:
                    proxystring = proxylist[rd.Next(1, proxylist.Count + 1)];
                    break;
                case 3:
                    proxyid += 1;
                    proxystring = proxylist[proxyid];
                    if (proxyid == proxylist.Count) { proxyid = 0; }
                    break;
                case 4:
                    proxystring = proxylist[rd.Next(1, proxylist.Count + 1)];
                    break;
            }
            if (Settings.Default.debug_msg)
            {
                MyInvoke UpdateMsgAdd = new MyInvoke(UpdateMsgTextBoxAdd);
                output_text.BeginInvoke(UpdateMsgAdd, new object[] { "线程：" + Thread.CurrentThread.Name + ",获取代理IP：" + proxystring });
            }
            return proxystring;
        }
        public void setproxy(HttpWebRequest req)
        {
            if (proxyopen)
            {
                string proxy = getproxy();
                if (proxy.Length != 0)
                {
                    req.Proxy = new WebProxy(proxy);
                }
            }

        }

        private void param_Change(object sender, EventArgs e)
        {
            CheckBox obj = (CheckBox)sender;
            //int id = Convert.ToInt32(obj.Name, 10);
            Control tabpage = obj.Parent;
            string[] keyarray = new string[] { "name_", "value_", "type_" }; ;
            bool checking = true;
            //MessageBox.Show(obj.Parent.Name);
            if (obj.Checked)
            {
                checking = true;
            }
            else
            {
                checking = false;
            }
            foreach (string key in keyarray)
            {
                foreach (Control ctl in tabpage.Controls)
                {
                    if (ctl.Name == key + obj.Name)
                    {
                        ctl.Enabled = checking;
                        break;
                    }
                }
            }
        }
        private void param_Check_value(object sender, CancelEventArgs e)
        {
            TextBox obj = (TextBox)sender;
            if (!GetParamCheck(obj))
            {
                MessageBox.Show("无限循环检查：不可将自身参数名称编入加密的参数值中");
                e.Cancel = true;
            }
        }
        private void update_file_count(object obj)
        {
            Control ctl = (Control)obj;
            //param_isfile_Count(ctl);
            param_type_selectfile_Count(ctl);
        }
        private void param_isfile_Change(object sender, EventArgs e)
        {
            CheckBox obj = (CheckBox)sender;
            param_isfile_Count(obj.Parent);
        }
        private void param_isfile_Count(Control obj)
        {
            string key = "isfile_";
            int count = 0;
            foreach (Control ctl in obj.Controls)
            {
                for (int i = 0; i < tab_body_rowcount; i++)
                {
                    if (ctl.Name == key + i.ToString())
                    {
                        if (((CheckBox)ctl).Checked && ((CheckBox)ctl).Enabled)
                        {
                            count += 1;
                            break;
                        }
                    }
                }
            }
            file_count = count;
        }
        private void param_type_selectfile_Count(Control obj)
        {
            string key = "type_";
            int count = 0;
            foreach (Control ctl in obj.Controls)
            {
                for (int i = 0; i < tab_body_rowcount; i++)
                {
                    if (ctl.Name == key + i.ToString())
                    {
                        if (((ComboBox)ctl).SelectedItem.ToString() == "file" && ((ComboBox)ctl).Enabled)
                        {
                            count += 1;
                            break;
                        }
                    }
                }
            }
            file_count = count;
        }

        private void param_type_Change(object sender, EventArgs e)
        {
            ComboBox obj = (ComboBox)sender;
            int rowcount = 0;
            inputkey inputkey;
            if (obj.Parent.Name.IndexOf("url") >= 0)
            {
                rowcount = tab_url_rowcount;
            }
            else if (obj.Parent.Name.IndexOf("body") >= 0)
            {
                update_file_count(obj.Parent);
                rowcount = tab_body_rowcount;
            }
            else if (obj.Parent.Name.IndexOf("header") >= 0)
            {
                rowcount = tab_header_rowcount;
            }
            if (obj.Enabled)
            {
                int i = 0;
                for (i = 0; i < rowcount; i++)
                {
                    if (((ComboBox)(obj.Parent.Controls.Find("type_" + i.ToString(), false)[0])).Name == obj.Name)
                    {
                        break;
                    }
                }
                switch (obj.SelectedItem.ToString())
                {
                    case "timestamp":
                        ((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).Text = Coding.get_timestamp();
                        break;
                    case "timeticks":
                        ((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).Text = DateTime.Now.Ticks.ToString();
                        break;
                    case "GUID":
                        ((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).Text = Guid.NewGuid().ToString("N");
                        break;
                    case "MD5":
                        inputkey = new inputkey();
                        //inputkey.paramin(ParamControl);
                        inputkey.setkeytype(1);
                        inputkey.ShowDialog(this);
                        if (((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).TextLength == 0)
                            ((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).Text = "{参数名1}" + key.md5 + "{参数名2}";
                        break;
                    case "MD5_16":
                        inputkey = new inputkey();
                        //inputkey.paramin(ParamControl);
                        inputkey.setkeytype(1);
                        inputkey.ShowDialog(this);
                        if (((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).TextLength == 0)
                            ((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).Text = "{参数名1}" + key.md5 + "{参数名2}";
                        break;
                    case "AES":
                        inputkey = new inputkey();
                        inputkey.setkeytype(2);
                        inputkey.ShowDialog(this);
                        if (((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).TextLength == 0)
                            ((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).Text = "格式：字符+{参数名}";
                        //((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).AccessibleDescription = key.AES;
                        break;
                    case "AuthAES":
                        inputkey = new inputkey();
                        inputkey.setkeytype(3);
                        inputkey.ShowDialog(this);
                        if (((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).TextLength == 0)
                            ((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).Text = "格式：字符+{参数名}";
                        //((TextBox)(obj.Parent.Controls.Find("value_" + i.ToString(), false)[0])).AccessibleDescription = key.AuthAES;
                        break;
                    default: break;
                }
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            int f = -99;
            string str = "";
            Button obj = (Button)sender;
            //int bb = Convert.ToInt32(pb.Name, 10);
            if (obj.Name.IndexOf("url") >= 0)
            {
                str = "url";
                f = add_Click_action(tab_url, tab_url_rowcount);
                if (f == 0)
                {
                    tab_url_rowcount += 1;
                    //urladd.Location = new Point(40, 60 + 40 * tab_url_rowcount);
                    //urldel.Location = new Point(150, 60 + 40 * tab_url_rowcount);
                }
            }
            else if (obj.Name.IndexOf("body") >= 0)
            {
                str = "body";
                f = add_Click_action(tab_body, tab_body_rowcount);
                if (f == 0)
                {
                    tab_body_rowcount += 1;
                    //bodyadd.Location = new Point(40, 60 + 40 * tab_body_rowcount);
                    //bodydel.Location = new Point(150, 60 + 40 * tab_body_rowcount);
                }
            }
            else if (obj.Name.IndexOf("header") >= 0)
            {
                str = "header";
                f = add_Click_action(tab_header, tab_header_rowcount);
                if (f == 0)
                {
                    tab_header_rowcount += 1;
                    //headeradd.Location = new Point(40, 60 + 40 * tab_header_rowcount);
                    //headerdel.Location = new Point(150, 60 + 40 * tab_header_rowcount);
                }
            }
            if (f == -1)
            {
                MessageBox.Show(str + "参数已添加到最大行数");
                return;
            }
            else if (f == -99)
            {
                MessageBox.Show("调用错误");
                return;
            }
        }
        private int add_Click_action(TabPage tabpage, int tabrow)
        {
            return add_Click_action(tabpage, tabrow, true);
        }
        private int add_Click_action(TabPage tabpage, int tabrow, bool autoscroll)
        {
            if (tabrow >= tab_rowmax)
            {
                return -1;
            }
            if (autoscroll)
            {
                //tabpage.VerticalScroll.Value = 0;
                tabpage.AutoScroll = false;
            }
            CheckBox[] CheckBox_param = new CheckBox[tab_rowmax];
            CheckBox_param[tabrow] = new CheckBox();
            CheckBox_param[tabrow].Size = new Size(20, 20);
            CheckBox_param[tabrow].Location = new Point(25, 50 + 40 * tabrow);
            CheckBox_param[tabrow].Name = tabrow.ToString();
            CheckBox_param[tabrow].AccessibleName = tabrow.ToString();
            CheckBox_param[tabrow].Checked = true;
            CheckBox_param[tabrow].CheckedChanged += new EventHandler(param_Change);

            TextBox[] TextBox_param_name = new TextBox[tab_rowmax];
            TextBox_param_name[tabrow] = new TextBox();
            TextBox_param_name[tabrow].Size = new Size(70, 20);
            TextBox_param_name[tabrow].Location = new Point(70, 50 + 40 * tabrow);
            TextBox_param_name[tabrow].Name = "name_" + tabrow.ToString();
            TextBox_param_name[tabrow].AccessibleName = tabrow.ToString();

            TextBox[] TextBox_param_value = new TextBox[tab_rowmax];
            TextBox_param_value[tabrow] = new TextBox();
            TextBox_param_value[tabrow].Size = new Size(240, 20);
            TextBox_param_value[tabrow].Location = new Point(170, 50 + 40 * tabrow);
            TextBox_param_value[tabrow].Name = "value_" + tabrow.ToString();
            TextBox_param_value[tabrow].AccessibleName = tabrow.ToString();
            TextBox_param_value[tabrow].Validating += new CancelEventHandler(param_Check_value);

            tabpage.Controls.Add(CheckBox_param[tabrow]);
            tabpage.Controls.Add(TextBox_param_name[tabrow]);
            tabpage.Controls.Add(TextBox_param_value[tabrow]);

            ComboBox[] ComboBox_param_type = new ComboBox[tab_rowmax];
            ComboBox_param_type[tabrow] = new ComboBox();
            ComboBox_param_type[tabrow].Size = new Size(100, 25);
            ComboBox_param_type[tabrow].Location = new Point(450, 50 + 40 * tabrow);
            ComboBox_param_type[tabrow].Name = "type_" + tabrow.ToString();
            ComboBox_param_type[tabrow].AccessibleName = tabrow.ToString();
            ComboBox_param_type[tabrow].SelectedIndexChanged += new EventHandler(param_type_Change);
            ComboBox_param_type[tabrow].EnabledChanged += new EventHandler(param_type_Change);
            ComboBox_param_type[tabrow].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            if (tabpage.Name.IndexOf("url") > 0)
            {
                ComboBox_param_type[tabrow].Items.AddRange(new object[] { "txt", "timestamp", "timeticks", "GUID", "MD5", "MD5_16", "escape_%u", "escape_\\u", "base64", "AuthAES", "ShareMemory" });// "AES",
            }
            else if (tabpage.Name.IndexOf("body") > 0)
            {
                ComboBox_param_type[tabrow].Items.AddRange(new object[] { "txt", "file", "urlencode", "timestamp", "timeticks", "GUID", "MD5", "MD5_16", "escape_%u", "escape_\\u", "base64", "AuthAES", "ShareMemory" });
            }
            else if (tabpage.Name.IndexOf("header") > 0)
            {
                ComboBox_param_type[tabrow].Items.AddRange(new object[] { "txt", "urlencode", "timestamp", "timeticks", "GUID", "MD5", "MD5_16", "escape_%u", "escape_\\u", "base64", "AuthAES", "ShareMemory" });
            }
            tabpage.Controls.Add(ComboBox_param_type[tabrow]);
            ComboBox_param_type[tabrow].SelectedItem = "txt";

            foreach (Control btn in tabpage.Controls)
            {
                if (btn is Button)
                {
                    btn.Location = new Point(btn.Location.X, btn.Location.Y + 40);
                }
            }
            if (autoscroll)
            {
                tabpage.AutoScroll = true;
                tabpage.AutoScrollPosition = new Point(0, 60 + 40 * (tabrow + 1));
            }
            return 0;
        }
        private void del_Click(object sender, EventArgs e)
        {

            int f = -99;
            string str = "";
            Button obj = (Button)sender;
            if (obj.Name.IndexOf("url") >= 0)
            {
                str = "url";
                f = del_Click_action(tab_url, tab_url_rowcount);
                if (f == 0)
                {
                    tab_url_rowcount -= 1;
                    //urladd.Location = new Point(40, 60 + 40 * tab_url_rowcount);
                    //urldel.Location = new Point(150, 60 + 40 * tab_url_rowcount);
                }
            }
            else if (obj.Name.IndexOf("body") >= 0)
            {
                str = "body";
                f = del_Click_action(tab_body, tab_body_rowcount);
                if (f == 0)
                {
                    tab_body_rowcount -= 1;
                    //更新文件数
                    update_file_count(obj.Parent);
                    //bodyadd.Location = new Point(40, 60 + 40 * tab_body_rowcount);
                    //bodydel.Location = new Point(150, 60 + 40 * tab_body_rowcount);
                }
            }
            else if (obj.Name.IndexOf("header") >= 0)
            {
                str = "header";
                f = del_Click_action(tab_header, tab_header_rowcount);
                if (f == 0)
                {
                    tab_header_rowcount -= 1;
                    //headeradd.Location = new Point(40, 60 + 40 * tab_header_rowcount);
                    //headerdel.Location = new Point(150, 60 + 40 * tab_header_rowcount);
                }
            }

            if (f == -2)
            {
                MessageBox.Show(str + "参数已全部删除");
                return;
            }
            else if (f == -99)
            {
                MessageBox.Show("调用错误");
                return;
            }

        }
        private int del_Click_action(TabPage tabpage, int tabrow)
        {
            if (tabrow <= 0)
            {
                return -2;
            }

            tabrow -= 1;
            tabpage.AutoScroll = false;

            string[] keyarray = default_col;
            foreach (string key in keyarray)
            {
                tabpage.Controls.RemoveByKey(key + tabrow.ToString());
            }
            foreach (Control btn in tabpage.Controls)
            {
                if (btn is Button)
                {
                    btn.Location = new Point(btn.Location.X, btn.Location.Y - 40);
                }
            }

            tabpage.AutoScroll = true;
            tabpage.AutoScrollPosition = new Point(0, 60 + 40 * (tabrow));

            return 0;
        }
        private void Clear_TabPage_All()
        {
            Clear_TabPage(tab_url, tab_url_rowcount);
            Clear_TabPage(tab_body, tab_body_rowcount);
            Clear_TabPage(tab_header, tab_header_rowcount);
            tab_url_rowcount = 0;
            tab_body_rowcount = 0;
            tab_header_rowcount = 0;

        }
        private void Clear_TabPage(TabPage tabpage, int tabrow)
        {
            for (int i = tabrow; i > 0; i--)
            {
                del_Click_action(tabpage, i);
            }
        }

        private string password_Encrypt(string password)
        {
            return Encrypte.s52e(Encrypte.MD5Hash(password, 32));
        }

        private void http_Method_SelectedValueChanged(object sender, EventArgs e)
        {
            if (http_Method.SelectedIndex == 0)
            {
                RequestMethod = null;
            }
            else
            {
                RequestMethod = http_Method.Text;
            }
        }
        private void login_key_SelectedValueChanged(object sender, EventArgs e)
        {
            if (tiancity_login_server.SelectedIndex == 0)
            {
                key.login_pwd = Settings.Default.Login_tiancity_key_test;
            }
            else if (tiancity_login_server.SelectedIndex == 1)
            {
                key.login_pwd = Settings.Default.Login_tiancity_key;
            }
        }

        private void tiancity_login_CheckedChanged(object sender, EventArgs e)
        {
            tiancity_userid.Enabled = tiancity_login.Checked;
            tiancity_password.Enabled = tiancity_login.Checked;
            tiancity_login_server.Enabled = tiancity_login.Checked;
            tiancity_users_import.Enabled = tiancity_login.Checked;
            tiancity_cookies_import.Enabled = tiancity_login.Checked;
        }

        private void LoginControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoginControlSelectedIndex = LoginControl.SelectedIndex;
        }

        private void tiancity_users_import_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Title = "选择需要读取的文件";
            OpenFile.Filter = "TXT|*.txt|All files|*.*";
            OpenFile.ValidateNames = true;
            OpenFile.CheckFileExists = true;
            OpenFile.CheckPathExists = true;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                userinfo.Clear();
                if (Txt.ReadtxtToDict(OpenFile.FileName, ref userinfo))
                {
                    lable_tiancity_users_file.Visible = true;
                    lable_tiancity_users_file.Text = OpenFile.SafeFileName;
                    tiancity_users_importcount.Text = userinfo.Count.ToString();
                    import_users_flag = true;
                }
                else
                {
                    MessageBox.Show(OpenFile.FileName + "\r\n读取失败\r\n");
                }
            }
        }

        private void tiancity_cookies_import_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Title = "选择需要读取的文件";
            OpenFile.Filter = "TXT|*.txt|All files|*.*";
            OpenFile.ValidateNames = true;
            OpenFile.CheckFileExists = true;
            OpenFile.CheckPathExists = true;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                usercookie.Clear();
                if (Txt.ReadtxtToDict(OpenFile.FileName, ref usercookie))
                {
                    lable_tiancity_cookies_file.Visible = true;
                    lable_tiancity_cookies_file.Text = OpenFile.SafeFileName;
                    tiancity_cookies_importcount.Text = usercookie.Count.ToString();
                    import_cookies_flag = true;
                }
                else
                {
                    MessageBox.Show(OpenFile.FileName + "\r\n读取失败\r\n");
                }
            }
        }

        private void Resquest_data_read()
        {
            tab_url_data = tabdata_read(tab_url, default_col, tab_url_rowcount);
            tab_body_data = tabdata_read(tab_body, default_col, tab_body_rowcount);
            //tab_body_data = tabdata_read(tab_body, new string[] { "", "name_", "value_", "type_", "isfile_" }, tab_body_rowcount);
            tab_header_data = tabdata_read(tab_header, default_col, tab_header_rowcount);
        }

        private string[,] tabdata_read(Control tabpage, string[] keyarray, int rowcount)
        {
            int keyx = 0;
            string[,] data;
            //string[] keyarray;
            //keyarray = new string[]{ "", "name_", "value_"};
            data = new string[rowcount, keyarray.Length];
            foreach (Control ctl in tabpage.Controls)
            {
                for (int i = 0; i <= rowcount; i++)
                {
                    keyx = 0;
                    foreach (string key in keyarray)
                    {
                        if (key + i.ToString() == ctl.Name)
                        {
                            if (ctl is TextBox)
                                data[i, keyx] = ((TextBox)ctl).Text;
                            else if (ctl is ComboBox)
                                data[i, keyx] = ((ComboBox)ctl).Text;
                            else if (ctl is CheckBox)
                                data[i, keyx] = ((CheckBox)ctl).Checked.ToString();
                            break;
                        }
                        keyx += 1;
                    }
                }
            }
            return data;

        }

        private string GetFullUrl()
        {
            string url = http_url.Text;
            if (url.Length < 1)
            {
                return "";
            }
            string FullUrl = http_url.Text;
            //最后一位如果是？则去掉
            if (url.Substring(url.Length - 1) == "?")
            {
                url = url.Substring(0, url.Length - 1);
            }
            if (url.IndexOf("?") > 0)
            {
                FullUrl = url + "&";
            }
            else if (tab_url_rowcount > 0)
            {
                FullUrl = url + "?";
            }
            for (int i = 0; i < tab_url_rowcount; i++)
            {
                //tab_url_data[i,0]  checkbox
                //tab_url_data[i,1]  name
                //tab_url_data[i,2]  value
                if (Convert.ToBoolean(tab_url_data[i, 0]) && tab_url_data[i, 1] != "")
                {
                    //FullUrl += tab_url_data[i, 1].Trim() + "=" + GetParamValue(tab_url, tab_url_rowcount, tab_url_data[i, 2], tab_url_data[i, 3]) + "&";
                    FullUrl += tab_url_data[i, 1].Trim() + "=" + GetParamValue(tab_url_data[i, 2], tab_url_data[i, 3]) + "&";
                }
            }
            while (FullUrl.Substring(FullUrl.Length - 1) == "&")
            {
                FullUrl = FullUrl.Substring(0, FullUrl.Length - 1);
            }
            return FullUrl;
        }
        private string GetBodyText()
        {
            if (tab_body_rowcount == 0)
            {
                return "";
            }
            string body = "";
            for (int i = 0; i < tab_body_rowcount; i++)
            {
                if (Convert.ToBoolean(tab_body_data[i, 0]))
                {
                    if (tab_body_data[i, 1] != "")
                    {
                        //body += tab_body_data[i, 1].Trim() + "=" + GetParamValue(tab_body, tab_body_rowcount, tab_body_data[i, 2], tab_body_data[i, 3]) + "&";
                        body += tab_body_data[i, 1].Trim() + "=" + GetParamValue(tab_body_data[i, 2], tab_body_data[i, 3]) + "&";
                    }
                    else
                    {
                        body += GetParamValue(tab_body_data[i, 2], tab_body_data[i, 3]);
                    }
                }
            }
            while (body.Length > 1 && body.Substring(body.Length - 1) == "&")
            {
                body = body.Substring(0, body.Length - 1);
            }
            return body;
        }
        private MemoryStream GetBodyStream()
        {
            if (tab_body_rowcount == 0)
            {
                return null;
            }
            MemoryStream postStream = new MemoryStream();
            Contentline = "-----------------------------" + Coding.randomhex(13);
            string Dispositionstr = "Content-Disposition: form-data; name=";
            string textField = "";
            byte[] buffer;
            bool OnlyFileStream = false;
            //byte[] filebuffer;
            int count = 0;

            for (int i = 0; i < tab_body_rowcount; i++)
            {
                if (Convert.ToBoolean(tab_body_data[i, 0]))
                {
                    if (tab_body_data[i, 1] != "")
                    {
                        textField = "--" + Contentline + "\r\n";
                        textField += Dispositionstr + "\"" + tab_body_data[i, 1].Trim() + "\"";
                        //textField += Dispositionstr + "\"" + tab_body_data[i, 1].Substring(tab_body_data[i, 1].LastIndexOf("\\") + 1) + "\"";
                        //Convert.ToBoolean(tab_body_data[i, 4])
                        if (tab_body_data[i, 3] == "file")
                        {
                            textField += "; filename=\"" + tab_body_data[i, 2] + "\"\r\n";
                            textField += "Content-Type: image/jpeg\r\n\r\n";
                            buffer = RequestEncoding.GetBytes(textField);
                            postStream.Write(buffer, 0, buffer.Length);
                            try
                            {
                                FileStream fs = new FileStream(tab_body_data[i, 2], FileMode.Open, FileAccess.Read);
                                buffer = new byte[fs.Length];
                                do
                                {
                                    count = fs.Read(buffer, 0, buffer.Length);
                                    postStream.Write(buffer, 0, count);

                                } while (count > 0);
                                fs.Close();
                                fs.Dispose();
                                fs = null;
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("文件读取出错\r\n" + tab_body_data[i, 2]);
                            }
                            buffer = RequestEncoding.GetBytes("\r\n");
                            postStream.Write(buffer, 0, buffer.Length);
                        }
                        else
                        {
                            //textField += "\r\n\r\n" + GetParamValue(tab_body, tab_body_rowcount, tab_body_data[i, 2], tab_body_data[i, 3]) + "\r\n";
                            textField += "\r\n\r\n" + GetParamValue(tab_body_data[i, 2], tab_body_data[i, 3]) + "\r\n";
                            buffer = RequestEncoding.GetBytes(textField);
                            postStream.Write(buffer, 0, buffer.Length);
                        }
                    }
                    else
                    {
                        OnlyFileStream = true;
                        if (tab_body_data[i, 3] == "file")
                        {
                            try
                            {
                                FileStream fs = new FileStream(tab_body_data[i, 2], FileMode.Open, FileAccess.Read);
                                buffer = new byte[fs.Length];
                                do
                                {
                                    count = fs.Read(buffer, 0, buffer.Length);
                                    postStream.Write(buffer, 0, count);
                                } while (count > 0);
                                fs.Close();
                                fs.Dispose();
                                fs = null;
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("文件读取出错\r\n" + tab_body_data[i, 2]);
                            }
                        }
                        else
                        {
                            //textField += GetParamValue(tab_body, tab_body_rowcount, tab_body_data[i, 2], tab_body_data[i, 3]);
                            textField += GetParamValue(tab_body_data[i, 2], tab_body_data[i, 3]);
                            buffer = RequestEncoding.GetBytes(textField);
                            postStream.Write(buffer, 0, buffer.Length);
                        }
                    }
                }
            }

            if (!OnlyFileStream)
            {
                buffer = RequestEncoding.GetBytes("--" + Contentline + "\r\n");
                postStream.Write(buffer, 0, buffer.Length);
            }

            return postStream;
        }

        private void RequestHeaderSet(HttpWebRequest request)
        {
            int RequestNo = 0;
            //try
            //{
            //    RequestNo = Convert.ToInt32(Thread.CurrentThread.Name);
            //}
            //catch(Exception)
            //{
            //    MessageBox.Show("Get Thread No Fail");
            //}
            RequestNo = Convert.ToInt32(Thread.CurrentThread.Name);
            RequestHeaderSet(request, RequestNo);
        }

        private void RequestHeaderSet(HttpWebRequest request, int CookieListNo)
        {
            //if (headerset.ContainsKey(CookieListNo) && headerset[CookieListNo])
            //{
            //    return;
            //}

            string data = null;
            request.UserAgent = "WebRequest";
            request.Accept = "*/*";
            //Cookie cookie = new Cookie();
            //request.CookieContainer.Add(
            //    System.Net.CookieCollection ccl = new System.Net.CookieCollection();
            //System.Net.CookieContainer cc = new CookieContainer();
            //System.Net.Cookie c = new Cookie("UserInfo","UserName=" + System.Web.HttpUtility.UrlEncode("孟宪会") + "&UserId=net_lover");
            //c.Domain = "mengxianhui.com";
            //ccl.Add(c);
            //c = new Cookie("Url","http://dotnet.aspx.cc");
            //c.Domain = "mengxianhui.com";
            //ccl.Add(c);
            //cc.Add(ccl);
            //System.Net.HttpWebRequest r = System.Net.HttpWebRequest.Create("http://www.mengxianhui.com:8081/b.aspx") as System.Net.HttpWebRequest;
            //r.CookieContainer = cc;
            if (RequestMethod != null)
            {
                request.Method = RequestMethod;
            }
            if (cookie_login_list.Count > 0)
            {
                //request.Headers.Add("cookie", cookie_login);
                if (cookie_login_list.ContainsKey(CookieListNo))
                {
                    RequestCookieAdd(request, cookie_login_list[CookieListNo]);
                }
            }
            if (cookie_other_list.Count > 0)
            {
                //request.Headers.Add("cookie", cookie_other);
                if (cookie_other_list.ContainsKey(CookieListNo))
                {
                    RequestCookieAdd(request, cookie_other_list[CookieListNo]);
                }
            }
            for (int i = 0; i < tab_header_rowcount; i++)
            {
                if (Convert.ToBoolean(tab_header_data[i, 0]) && tab_header_data[i, 1].Trim() != "")
                {
                    //data = GetParamValue(tab_header, tab_header_rowcount, tab_header_data[i, 2], tab_header_data[i, 3]);
                    data = GetParamValue(tab_header_data[i, 2], tab_header_data[i, 3]);
                    switch (tab_header_data[i, 1].ToLower())
                    {
                        //case "cookie": request.Headers.Add(tab_header_data[i, 1].Trim(), data); break;
                        case "cookie": RequestCookieAdd(request, data); break;
                        case "accept": request.Accept = data; break;
                        case "referer": request.Referer = data; break;
                        case "user-agent": request.UserAgent = data; break;
                        case "content-length": request.ContentLength = Convert.ToInt32(data); break;
                        case "content-type": request.ContentType = data; break;
                        case "connection":
                            if (tab_header_data[i, 2].ToLower() == "keep-alive")
                            {
                                request.KeepAlive = true;
                            }
                            else if (tab_header_data[i, 2].ToLower() == "close")
                            {
                                request.KeepAlive = false;
                            }
                            break;
                        default: request.Headers.Set(tab_header_data[i, 1], data); break;
                    }
                }
            }
            //headerset.Add(CookieListNo, true);
        }

        private void RequestCookieAdd(HttpWebRequest request, string cookievalue)
        {
            if (request.Headers["cookie"] == null)
            {
                request.Headers["cookie"] += cookievalue;
            }
            else
            {
                request.Headers["cookie"] += ";" + cookievalue;
            }
        }

        private bool GetParamCheck(object obj)
        {
            TextBox valuebox = (TextBox)obj;
            MatchCollection MatchAll = Regex.Matches(valuebox.Text, @"{.*?[^\\]}");
            for (int i = 0; i < MatchAll.Count; i++)
            {
                if (MatchAll[i].ToString().Trim().Substring(1, MatchAll[i].ToString().Trim().Length - 2) == ((TextBox)valuebox.Parent.Controls.Find("name_" + valuebox.AccessibleName, false)[0]).Text.Trim())
                {
                    return false;
                }
            }
            return true;
        }

        private string GetParamValue(string value, string type)
        {
            // "txt", "urlencode", "timestamp", "GUID", "MD5", "escape_%u", "escape_\\u", "base64", "AES", "AuthAES", "ShareMemory"
            string returnvalue;
            string pattern;
            string str = null;
            //MyInvoke UpdateMsgAdd = new MyInvoke(UpdateMsgTextBoxAdd);
            switch (type)
            {
                case "txt": returnvalue = value;
                    break;
                case "urlencode": returnvalue = Coding.Urlencode(value, RequestEncoding);
                    break;
                case "timestamp": returnvalue = Coding.get_timestamp();
                    break;
                case "timeticks": returnvalue = DateTime.Now.Ticks.ToString();
                    break;
                case "GUID": returnvalue = value;//Guid.NewGuid().ToString("N")
                    break;
                case "MD5":
                    pattern = @"{.+?}";
                    str = GetValue(value, pattern);
                    //string[] Matches = value.Split(',');
                    //foreach (string n in Matches)
                    //{
                    //    //if (n.Trim() == "{#key#}")
                    //    //{
                    //    //    md5str += key.md5;
                    //    //}
                    //    //else
                    //    if (Regex.IsMatch(n.Trim(), pattern))
                    //    {
                    //        str += GetParamValueFromName(n.Trim().Substring(1, n.Trim().Length - 2));
                    //    }
                    //    else
                    //    {
                    //        str += n.Trim();
                    //    }
                    //}
                    returnvalue = Encrypte.MD5Hash(str, 32);
                    if (Settings.Default.debug_msg)
                    {
                        UpdateMsgTextBoxAdd("MD5原文： " + str);
                        UpdateMsgTextBoxAdd("MD5密文： " + returnvalue);
                        //output_text.BeginInvoke(UpdateMsgAdd, new object[] { "MD5: " + str });
                    }
                    break;
                case "MD5_16":
                    pattern = @"{.+?}";
                    str = GetValue(value, pattern);
                    returnvalue = Encrypte.MD5Hash(str, 16);
                    if (Settings.Default.debug_msg)
                    {
                        UpdateMsgTextBoxAdd("MD5原文： " + str);
                        UpdateMsgTextBoxAdd("MD5密文： " + returnvalue);
                        //output_text.BeginInvoke(UpdateMsgAdd, new object[] { "MD5: " + str });
                    }
                    break;
                case "escape_%u": returnvalue = Coding.Escape(value);
                    break;
                case "escape_\\u": returnvalue = Coding.EncodeJSString(value);
                    break;
                case "base64": returnvalue = Coding.ToBase64(value, RequestEncoding);
                    break;
                case "AES":
                    pattern = @"{.+?}";
                    str = GetValue(value, pattern);
                    if (Settings.Default.debug_msg)
                    {
                        UpdateMsgTextBoxAdd("AES原文： " + str);
                        //output_text.BeginInvoke(UpdateMsgAdd, new object[] { "AES: " + str });
                    }
                    try { str = AESEncryption.AESEncrypt(str, key.AES); }
                    catch { str = ""; UpdateMsgTextBoxAdd("AES加密出错"); }
                    if (Settings.Default.debug_msg)
                    {
                        UpdateMsgTextBoxAdd("AuthAES密文： " + str);
                    }
                    returnvalue = str;
                    break;
                case "AuthAES":
                    pattern = @"{.+?}";
                    str = GetValue(value, pattern);
                    if (Settings.Default.debug_msg)
                    {
                        UpdateMsgTextBoxAdd("AuthAES原文： " + str);
                        //output_text.BeginInvoke(UpdateMsgAdd, new object[] { "AuthAES: " + str });
                    }
                    try { str = AuthAESCrypt.Encrypt(PaddingMode.PKCS7, CipherMode.CBC, str, key.AuthAES); }
                    catch { str = ""; UpdateMsgTextBoxAdd("AuthAES加密出错"); }
                    if (Settings.Default.debug_msg)
                    {
                        UpdateMsgTextBoxAdd("AuthAES密文： " + str);
                    }
                    returnvalue = str;
                    break;
                case "ShareMemory":
                    ShareMemory.init(value, 1024);
                    returnvalue = ShareMemory.getdata_string(Encoding.Default);
                    break;
                default: returnvalue = null;
                    break;
            }
            return returnvalue;
        }

        private string GetValue(string value, string pattern)
        {
            string str = value;
            MatchCollection Matches = Regex.Matches(value, pattern);
            List<string> strlist = new List<string>();
            for (int i = 0; i < Matches.Count; i++)
            {
                strlist.Add(GetParamValueFromName(Matches[i].ToString().Trim().Substring(1, Matches[i].ToString().Trim().Length - 2)));
                str = str.Replace(Matches[i].ToString(), strlist[i]);
            }
            return str;
        }

        private string GetParamValueFromName(string name)
        {
            string returnvalue;
            returnvalue = GetParamValueSearch(tab_url, tab_url_rowcount, name);
            if (returnvalue != name)
            {
                return returnvalue;
            }
            returnvalue = GetParamValueSearch(tab_body, tab_body_rowcount, name);
            if (returnvalue != name)
            {
                return returnvalue;
            }
            returnvalue = GetParamValueSearch(tab_header, tab_header_rowcount, name);
            if (returnvalue != name)
            {
                return returnvalue;
            }
            return name;
        }

        private string GetParamValueSearch(TabPage tab, int rowcount, string name)
        {
            int i = 0;
            for (i = 0; i < rowcount; i++)
            {
                if (((TextBox)tab.Controls.Find("name_" + i.ToString(), false)[0]).Text.Trim() == name)
                {
                    break;
                }
            }
            if (i >= rowcount)
            {
                return name;
            }
            string value = ((TextBox)tab.Controls.Find("value_" + i.ToString(), false)[0]).Text;
            string type = ((ComboBox)tab.Controls.Find("type_" + i.ToString(), false)[0]).Text;
            string returnvalue;
            //returnvalue = GetParamValue(tab, rowcount, value, type);
            returnvalue = GetParamValue(value, type);
            if (returnvalue == null)
            {
                return name;
            }
            else
            {
                return returnvalue;
            }
        }

        public bool tiancity_autologin(string userid, string password)
        {
            if (Login_HTTPRequest())
            {
                return true;
            }
            return false;
        }


        public static string UrlEncode2(string str, Encoding Encoding)
        {
            if (Encoding == null)
            {
                return str;
            }
            StringBuilder sb = new StringBuilder();
            byte[] byStr = Encoding.GetBytes(str); ;
            System.Text.RegularExpressions.Regex regKey = new System.Text.RegularExpressions.Regex("^[A-Za-z0-9]+$");
            for (int i = 0; i < byStr.Length; i++)
            {
                string strBy = Convert.ToChar(byStr[i]).ToString();
                if (regKey.IsMatch(strBy))
                {
                    //是字母或者数字则不进行转换    
                    sb.Append(strBy);
                }
                else
                {
                    sb.Append(@"%" + Convert.ToString(byStr[i], 16));
                }
            }
            return sb.ToString().Replace("%D%A", "%0A");
        }

        public bool Login_HTTPRequest()
        {
            string userid = tiancity_userid.Text;
            string password = tiancity_password.Text;
            return Login_HTTPRequest(userid, password, 0);
        }

        public bool Login_HTTPRequest(int RequestNo)
        {
            string userid = tiancity_userid.Text;
            string password = tiancity_password.Text;
            return Login_HTTPRequest(userid, password, RequestNo);
        }

        public bool Login_HTTPRequest(string userid, string password, int RequestNo)
        {
            string checkcode = "111111";
            string responsetext;
            if (password.Length == 0)
            {
                return false;
            }
            string EncryptPassword = password_Encrypt(password);
            try
            {
                //string url = "http://passport.tiancity.com/login/login_check_json.aspx?username=" + userid + "&cipherword=" + EncryptPassword + "&checkcode=&op=checkuser";
                string url = string.Format(Settings.Default.Login_tiancity_url, userid, EncryptPassword, checkcode);

                // 与指定URL创建HTTP请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                setproxy(request);
                //RequestHeaderSet(request);
                HttpWebResponse wResp = (HttpWebResponse)request.GetResponse();
                Stream respStream = wResp.GetResponseStream();
                using (StreamReader reader = new StreamReader(respStream, ResponseEncoding))
                {
                    responsetext = reader.ReadToEnd();
                }
                if (responsetext.IndexOf(Coding.Urlencode("登录成功", Encoding.UTF8).ToLower()) >= 0)
                {
                    string setcookie = wResp.GetResponseHeader("Set-Cookie");
                    string[] loginkeystrings = new string[] { "TCSPUBLICJAUTHM=", "TCSPUBLICJAUTHMV=" };
                    string cookie_login = GetCookie(setcookie, loginkeystrings);
                    if (cookie_login.Length > 0)
                    {
                        cookie_login_list.Add(RequestNo, cookie_login);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GetCookie(string cookie, string[] cookiename)
        {
            string cookiestr = "";
            for (int i = 0; i < cookiename.Length; i++)
            {
                foreach (string c in cookie.Split(','))
                {
                    if (c.Length > cookiename[i].Length && c.Substring(0, cookiename[i].Length) == cookiename[i])
                    {
                        cookiestr += c.Split(';')[0] + ";";
                    }
                }
            }
            return cookiestr;
        }

        private void Cookiesave(string cookie)
        {
            if (cookie != null && cookie.Length > 0)
            {
                cookie_other_list.Add(Convert.ToInt32(Thread.CurrentThread.Name), cookie);
            }
        }

        public string HTTPRequest(string Url)
        {
            return HTTPRequest(Url, new string[] { });
        }

        public string HTTPRequest(string Url, string[] GetResponseCookieName)
        {
            StringBuilder content = new StringBuilder();
            string cookie_other = "";
            try
            {
                // 与指定URL创建HTTP请求
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                if (RequestEncoding != null && RequestEncoding_autoheader)
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }
                setproxy(request);
                RequestHeaderSet(request);
                request.KeepAlive = true;
                // Get the response instance.
                HttpWebResponse wResp = (HttpWebResponse)request.GetResponse();
                //wResp.GetResponseHeader
                Stream respStream = wResp.GetResponseStream();
                if (GetResponseCookieName.Length != 0)
                {
                    cookie_other = GetCookie(wResp.GetResponseHeader("Set-Cookie"), GetResponseCookieName);
                }
                // Dim reader As StreamReader = New StreamReader(respStream)    
                using (StreamReader reader = new StreamReader(respStream, ResponseEncoding))
                {
                    //lastBuffer = StreamToByte(reader);
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                content = new StringBuilder("请求失败，请检查设置\r\n" + e.Message + "\r\n" + e.StackTrace);
            }
            Cookiesave(cookie_other);
            return content.ToString();
        }

        public string HTTPRequest(string Url, string Postdata)
        {
            return HTTPRequest(Url, Postdata, new string[] { });
        }

        public string HTTPRequest(string Url, string Postdata, string[] GetResponseCookieName)
        {
            if (Postdata == "")
            {
                return HTTPRequest(Url);
            }

            StringBuilder content = new StringBuilder();
            string cookie_other = "";
            try
            {
                Encoding Encoding = Encoding.Default;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                if (RequestEncoding_autoheader)
                {
                    request.ContentType = "application/x-www-form-urlencoded";
                }
                setproxy(request);
                RequestHeaderSet(request);

                byte[] buffer = Encoding.GetBytes(Postdata);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse wResp = (HttpWebResponse)request.GetResponse();
                if (GetResponseCookieName.Length != 0)
                {
                    cookie_other = GetCookie(wResp.GetResponseHeader("Set-Cookie"), GetResponseCookieName);
                }
                Stream respStream = wResp.GetResponseStream();
                using (StreamReader reader = new StreamReader(respStream, ResponseEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                content = new StringBuilder("请求失败，请检查设置\r\n" + e.Message + "\r\n" + e.StackTrace);
            }
            Cookiesave(cookie_other);
            return content.ToString();
        }

        public string HTTPRequest(string Url, MemoryStream postStream)
        {
            return HTTPRequest(Url, postStream, new string[] { });
        }

        public string HTTPRequest(string Url, MemoryStream postStream, string[] GetResponseCookieName)
        {
            if (postStream == null)
            {
                return HTTPRequest(Url);
            }

            StringBuilder content = new StringBuilder();
            string cookie_other = "";
            try
            {
                Encoding Encoding = Encoding.Default;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                if (RequestEncoding_autoheader)
                {
                    request.ContentType = "multipart/form-data; boundary=" + Contentline;
                }
                setproxy(request);
                RequestHeaderSet(request);

                byte[] buffer = new byte[postStream.Length];
                postStream.Position = 0;
                postStream.Read(buffer, 0, buffer.Length);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse wResp = (HttpWebResponse)request.GetResponse();
                if (GetResponseCookieName.Length != 0)
                {
                    cookie_other = GetCookie(wResp.GetResponseHeader("Set-Cookie"), GetResponseCookieName);
                }
                Stream respStream = wResp.GetResponseStream();
                //respStream.Write(lastBuffer, 0, lastBuffer.Length);
                using (StreamReader reader = new StreamReader(respStream, ResponseEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                content = new StringBuilder("请求失败，请检查设置\r\n" + e.Message + "\r\n" + e.StackTrace);
            }
            Cookiesave(cookie_other);
            return content.ToString();
        }

        public static byte[] StreamToByte(Stream Reader)
        {
            try
            {
                MemoryStream mem = new MemoryStream();
                byte[] buffer = new byte[1];
                int bytesRead = 0;
                int TotalByteRead = 0;

                while (true)
                {
                    bytesRead = Reader.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        break;
                    TotalByteRead += bytesRead;
                    mem.Write(buffer, 0, buffer.Length);
                }

                if (mem.Length > 0)
                {
                    return mem.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ep)
            {
                throw ep;
            }
        }

        //DateTime DateTime1970 = new DateTime(1970, 1, 1);
        //TimeSpan t = DateTime.Now - DateTime1970;

        //string test = "Testing 1-2-3";

        //// convert string to stream
        //MemoryStream stream = new MemoryStream();
        //StreamWriter writer = new StreamWriter( stream );
        //writer.Write( test );
        //writer.Flush();

        //// convert stream to string
        //stream.Position = 0;
        //StreamReader reader = new StreamReader( stream );
        //string text = reader.ReadToEnd();

        //如果方法验证网页来源就加上这一句如果不验证那就可以不写了
        //request.Referer = "http://sufei.cnblogs.com";
        //CookieContainer objcok = new CookieContainer();
        //objcok.Add(new Uri("http://sufei.cnblogs.com"), new Cookie("键", "值"));
        //objcok.Add(new Uri("http://sufei.cnblogs.com"), new Cookie("键", "值"));
        //objcok.Add(new Uri("http://sufei.cnblogs.com"), new Cookie("sidi_sessionid", "360A748941D055BEE8C960168C3D4233"));
        //request.CookieContainer = objcok;

        /*
        // 获取对应HTTP请求的响应
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        // 获取响应流
        Stream responseStream = response.GetResponseStream();
        // 对接响应流(以"GBK"字符集)
        StreamReader sReader = new StreamReader(responseStream, Encoding.GetEncoding("gb2312"));
        // 开始读取数据
        Char[] sReaderBuffer = new Char[256];
        int count = sReader.Read(sReaderBuffer, 0, 256);
        while (count > 0)
        {
            String tempStr = new String(sReaderBuffer, 0, count);
            content.Append(tempStr);
            count = sReader.Read(sReaderBuffer, 0, 256);
        }
        // 读取结束
        sReader.Close();
         */

        /* socket
        ///<summary>
        /// 请求的公共类用来向服务器发送请求
        ///</summary>
        ///<param name="strSMSRequest">发送请求的字符串</param>
        ///<returns>返回的是请求的信息</returns>
        private static string SMSrequest(string strSMSRequest)
        {
            byte[] data = new byte[1024];
            string stringData = null;
            IPHostEntry gist = Dns.GetHostByName("www.110.cn");
            IPAddress ip = gist.AddressList[0];
            //得到IP 
            IPEndPoint ipEnd = new IPEndPoint(ip, 3121);
            //默认80端口号 
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //使用tcp协议 stream类型 
            try
            {
                socket.Connect(ipEnd);
            }
            catch (SocketException ex)
            {
                return "Fail to connect server\r\n" + ex.ToString();
            }
            string path = strSMSRequest.ToString().Trim();
            StringBuilder buf = new StringBuilder();
            //buf.Append("GET ").Append(path).Append(" HTTP/1.0\r\n");
            //buf.Append("Content-Type: application/x-www-form-urlencoded\r\n");
            //buf.Append("\r\n");
            byte[] ms = System.Text.UTF8Encoding.UTF8.GetBytes(buf.ToString());
            //提交请求的信息
            socket.Send(ms);
            //接收返回 
            string strSms = "";
            int recv = 0;
            do
            {
                recv = socket.Receive(data);
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                //如果请求的页面meta中指定了页面的encoding为gb2312则需要使用对应的Encoding来对字节进行转换() 
                strSms = strSms + stringData;
                //strSms += recv.ToString();
            }
            while (recv != 0);
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            return strSms;
        }
         */

        private void StripMenu_save_file_Click(object sender, EventArgs e)
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            SaveFile.Title = "选择需要保存的文件";
            SaveFile.Filter = "WRT|*.wrt|INI|*.ini|All files|*.*";
            SaveFile.ValidateNames = true;
            SaveFile.CheckPathExists = true;
            SaveFile.OverwritePrompt = true;
            if (SaveFile.ShowDialog() == DialogResult.OK)
            {
                Save_File(SaveFile.FileName);
            }
        }

        private void StripMenu_load_file_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Title = "选择需要读取的文件";
            OpenFile.Filter = "WRT|*.wrt|INI|*.ini|All files|*.*";
            OpenFile.ValidateNames = true;
            OpenFile.CheckFileExists = true;
            OpenFile.CheckPathExists = true;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                if (Load_File(OpenFile.FileName))
                {
                    Clear_Interface();
                    Load_Interface();
                }
            }
        }

        private void StripMenu_load_file_http_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Title = "选择需要读取的文本文件";
            OpenFile.Filter = "TXT|*.txt|All files|*.*";
            OpenFile.ValidateNames = true;
            OpenFile.CheckFileExists = true;
            OpenFile.CheckPathExists = true;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                if (Load_File_Http(OpenFile.FileName))
                {
                    Clear_Interface();
                    Load_Interface();
                }
            }
        }

        private void StripMenu_load_file_http2_Click(object sender, EventArgs e)
        {
            TextForm TextForm = new TextForm();
            TextForm.typeset(1);
            TextForm.ShowDialog(this);
        }

        public void StripMenu_load_file_fromtext(string txt)
        {
            string tmpfilename;
            do
            {
                tmpfilename = System.AppDomain.CurrentDomain.BaseDirectory + Coding.randomhex(10);
            }
            while (File.Exists(tmpfilename));
            Txt.writetxtfile(txt, tmpfilename);

            Load_File_Http(tmpfilename);
            Clear_Interface();
            Load_Interface();

            if (File.Exists(tmpfilename))
            {
                File.Delete(tmpfilename);
            }
        }

        private void StripMenu_config_Click(object sender, EventArgs e)
        {
            Config ConfigForm = new Config();
            ConfigForm.Owner = this;
            ConfigForm.ShowDialog(this);
        }

        private struct Load_file
        {
            public struct http
            {
                public static string Method;
                public static string URL;
                public static string version;
            }
            public struct Login
            {
                public static bool Used = false;
                public static string Type;
                public static int Server;
                public static string Userid;
                public static string Password;
            }
            public struct Param
            {
                public static int url_count = 0;
                public static int body_count = 0;
                public static int header_count = 0;
                public static string[,] url = new string[tab_rowmax, tab_colmax];
                public static string[,] body = new string[tab_rowmax, tab_colmax];
                public static string[,] header = new string[tab_rowmax, tab_colmax];
            }
            public struct Config
            {
                public static bool Used = false;
                //public static string proxyip;
                public static Encoding Request;
                public static Encoding Response;
            }
        }

        private void ClearLoad()
        {
            Load_file.http.Method = "";
            Load_file.http.URL = "";
            Load_file.http.version = "";

            Load_file.Login.Type = "";
            Load_file.Login.Used = false;
            Load_file.Login.Server = -1;
            Load_file.Login.Userid = "";
            Load_file.Login.Password = "";

            Load_file.Param.url_count = 0;
            Load_file.Param.body_count = 0;
            Load_file.Param.header_count = 0;
            if (Load_file.Param.url != null)
            {
                Array.Clear(Load_file.Param.url, 0, Load_file.Param.url.Length);
            }
            if (Load_file.Param.body != null)
            {
                Array.Clear(Load_file.Param.body, 0, Load_file.Param.body.Length);
            }
            if (Load_file.Param.header != null)
            {
                Array.Clear(Load_file.Param.header, 0, Load_file.Param.header.Length);
            }
            Load_file.Config.Used = false;
            //Load_file.Config.proxyip = "";
            Load_file.Config.Request = Encoding.Default;
            Load_file.Config.Response = Encoding.Default;
        }

        private void Save_File(string path)
        {
            Ini ini = new Ini(path);
            ini.WriteValue("HTTP", "Method", http_Method.SelectedIndex.ToString());
            ini.WriteValue("HTTP", "URL", http_url.Text);
            ini.WriteValue("HTTP", "Version", http_Version.SelectedIndex.ToString());

            //ini.WriteValue("Config", "Used", "false");
            //ini.WriteValue("Config", "Proxy", Proxyip);
            //ini.WriteValue("Config", "RequestEncoding", RequestEncoding.ToString());
            //ini.WriteValue("Config", "ResponseEncoding", ResponseEncoding.ToString());

            //ini.WriteValue("Login", "Type", Login_type);
            //ini.WriteValue("Login", "Used", tiancity_login.Checked.ToString());
            //ini.WriteValue("Login", "Server", tiancity_login_server.SelectedIndex.ToString());
            //ini.WriteValue("Login", "Userid", tiancity_userid.Text);
            //ini.WriteValue("Login", "Password", tiancity_password.Text);

            ini.WriteValue("KEY", "MD5", key.md5);
            ini.WriteValue("KEY", "AES", key.AES);
            ini.WriteValue("KEY", "AuthAES", key.AuthAES);

            Resquest_data_read();
            string[] savestrings = default_col;
            //data[i, keyx]

            ini.WriteValue("Param_url", "Count", tab_url_rowcount.ToString());
            for (int i = 0; i < tab_url_rowcount; i++)
            {
                for (int j = 0; j < savestrings.Length; j++)
                {
                    ini.WriteValue("Param_url", savestrings[j] + i.ToString(), tab_url_data[i, j]);
                }
            }

            ini.WriteValue("Param_body", "Count", tab_body_rowcount.ToString());
            for (int i = 0; i < tab_body_rowcount; i++)
            {
                for (int j = 0; j < savestrings.Length; j++)
                {
                    ini.WriteValue("Param_body", savestrings[j] + i.ToString(), tab_body_data[i, j]);
                }
            }

            ini.WriteValue("Param_header", "Count", tab_header_rowcount.ToString());
            for (int i = 0; i < tab_header_rowcount; i++)
            {
                for (int j = 0; j < savestrings.Length; j++)
                {
                    ini.WriteValue("Param_header", savestrings[j] + i.ToString(), tab_header_data[i, j]);
                }
            }

        }

        private void Save_ToDB()
        {

        }

        private bool Load_File(string path)
        {
            Ini ini = new Ini(path);
            ClearLoad();
            try
            {
                Load_file.http.Method = ini.ReadValue("HTTP", "Method");
                Load_file.http.URL = ini.ReadValue("HTTP", "URL");
                Load_file.http.version = ini.ReadValue("HTTP", "Version");

                //Load_file.Config.Used = Convert.ToBoolean(ini.ReadValue("Config", "Used"));
                //Load_file.Config.proxyip = ini.ReadValue("Config", "Proxy");
                //Load_file.Config.Request = Encoding.GetEncoding(ini.ReadValue("Config", "RequestEncoding"));
                //Load_file.Config.Response = Encoding.GetEncoding(ini.ReadValue("Config", "ResponseEncoding"));

                //Load_file.Login.Type = ini.ReadValue("Login", "Type");
                //Load_file.Login.Used = Convert.ToBoolean(ini.ReadValue("Login", "Used"));
                //Load_file.Login.Server = Convert.ToInt32(ini.ReadValue("Login", "Server"));
                //Load_file.Login.Userid = ini.ReadValue("Login", "Userid");
                //Load_file.Login.Password = ini.ReadValue("Login", "Password");

                key.md5 = ini.ReadValue("KEY", "MD5");
                key.AES = ini.ReadValue("KEY", "AES");
                key.AuthAES = ini.ReadValue("KEY", "AuthAES");

                string[] loadstrings = default_col;

                Load_file.Param.url_count = Convert.ToInt32(ini.ReadValue("Param_url", "Count"));
                for (int i = 0; i < Load_file.Param.url_count; i++)
                {
                    for (int j = 0; j < loadstrings.Length; j++)
                    {
                        Load_file.Param.url[i, j] = ini.ReadValue("Param_url", loadstrings[j] + i.ToString());
                    }
                }

                Load_file.Param.body_count = Convert.ToInt32(ini.ReadValue("Param_body", "Count"));
                for (int i = 0; i < Load_file.Param.body_count; i++)
                {
                    for (int j = 0; j < loadstrings.Length; j++)
                    {
                        Load_file.Param.body[i, j] = ini.ReadValue("Param_body", loadstrings[j] + i.ToString());
                    }
                }

                Load_file.Param.header_count = Convert.ToInt32(ini.ReadValue("Param_header", "Count"));
                for (int i = 0; i < Load_file.Param.header_count; i++)
                {
                    for (int j = 0; j < loadstrings.Length; j++)
                    {
                        Load_file.Param.header[i, j] = ini.ReadValue("Param_header", loadstrings[j] + i.ToString());
                    }
                }
                //Param_Type = "Param_url";
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(path + "\r\n读取失败\r\n" + e.Message + "\r\n" + e.StackTrace);
                return false;
            }
        }

        private bool Load_File_Http(string path)
        {
            ClearLoad();
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            bool suceess = false;
            try
            {
                string linestring;
                bool bodyf = false;
                int line = 0;
                string url;
                string cookie;
                bool urlencode = false;
                do
                {
                    line += 1;
                    if (line == 1)
                    {
                        linestring = sr.ReadLine();
                        Load_file.http.Method = linestring.Split(' ')[0];
                        url = linestring.Split(' ')[1];
                        if (url.IndexOf('?') > 0)    //url
                        {
                            Load_file.http.URL = url.Split('?')[0];
                            url = url.Split('?')[1];
                            Load_file.Param.url_count += url.Split('&').Length;
                            Load_file.Param.url = ParamStringToArray(url, Load_file.Param.url_count);
                        }
                        else
                        {
                            Load_file.http.URL = url;
                        }
                        Load_file.http.version = linestring.Split(' ')[2];
                    }
                    else
                    {
                        if (bodyf)   //body
                        {
                            linestring = sr.ReadLine();
                            Load_file.Param.body_count += linestring.Split('&').Length;
                            Load_file.Param.body = ParamStringToArray(linestring, Load_file.Param.body_count);
                            if (urlencode)
                            {
                                for (int i = 0; i < Load_file.Param.body_count; i++)
                                {
                                    if (!(RequestEncoding == null || Load_file.Param.body == null))
                                    {
                                        Load_file.Param.body[i, 2] = Coding.Urldecode(Load_file.Param.body[i, 2], RequestEncoding);
                                    }
                                }
                            }
                            return true;
                        }
                        else
                        {
                            //header
                            do
                            {
                                linestring = sr.ReadLine();
                                if (linestring.Length > 0)
                                {
                                    switch (linestring.ToLower().Substring(0, linestring.IndexOf(':')))
                                    {
                                        case "connection":
                                            break;
                                        case "accept-encoding":
                                            break;
                                        case "content-length":
                                            break;
                                        case "content-type":
                                            if (linestring.Substring(linestring.IndexOf(':') + 1).Trim() == "application/x-www-form-urlencoded")
                                            {
                                                urlencode = true;
                                                for (int i = 0; i < Load_file.Param.url_count; i++)
                                                {
                                                    if (!(RequestEncoding == null || Load_file.Param.url == null))
                                                    {
                                                        Load_file.Param.url[i, 2] = Coding.Urldecode(Load_file.Param.url[i, 2], RequestEncoding);
                                                    }
                                                }
                                            }
                                            break;
                                        case "host":
                                            if (Load_file.http.URL.IndexOf("http") != 0)
                                            { Load_file.http.URL = "http://" + linestring.Split(':')[1].Trim() + Load_file.http.URL; }
                                            break;
                                        case "cookie":
                                            cookie = linestring.Substring(8).Trim();
                                            foreach (string cookie_n in cookie.Split(';'))
                                            {
                                                Load_file.Param.header[Load_file.Param.header_count, 0] = "true";
                                                Load_file.Param.header[Load_file.Param.header_count, 1] = "cookie";
                                                Load_file.Param.header[Load_file.Param.header_count, 2] = cookie_n.Trim();
                                                Load_file.Param.header[Load_file.Param.header_count, 3] = "txt";
                                                Load_file.Param.header_count += 1;
                                            }
                                            break;
                                        default:
                                            Load_file.Param.header[Load_file.Param.header_count, 0] = "true";
                                            if (linestring.IndexOf(':') > 0)
                                            {
                                                Load_file.Param.header[Load_file.Param.header_count, 1] = linestring.Split(':')[0].Trim();
                                                Load_file.Param.header[Load_file.Param.header_count, 2] = linestring.Substring(linestring.IndexOf(':') + 1).Trim();

                                            }
                                            Load_file.Param.header[Load_file.Param.header_count, 3] = "txt";
                                            Load_file.Param.header_count += 1;
                                            break;
                                    }
                                }
                            } while (linestring.Length > 0);
                            bodyf = true;
                        }
                    }
                } while (!sr.EndOfStream);
                suceess = true;
                //fs.Position = 0;
                //fs.Seek(0, SeekOrigin.Begin);
            }
            catch (Exception e)
            {
                MessageBox.Show(path + "\r\n读取失败\r\n" + e.Message + "\r\n" + e.StackTrace);
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
            return suceess;
        }

        private bool Load_FromDB()
        {
            return true;
        }

        private void Clear_Interface()
        {
            Clear_TabPage_All();
        }

        private void Load_Interface()
        {
            try
            {
                http_Method.SelectedItem = Load_file.http.Method;
                http_url.Text = Load_file.http.URL;
                http_Version.SelectedItem = Load_file.http.version;
                string[] load_col = default_col;

                if (Load_file.Param.url != null)
                {
                    tab_url_rowcount = Load_file.Param.url_count;
                    if (tab_url_rowcount > 0)
                    {
                        ParamControl.SelectTab(tab_url);
                    }
                    for (int i = 0; i < tab_url_rowcount; i++)
                    {
                        add_Click_action(tab_url, i, false);
                        for (int j = 0; j < load_col.Length; j++)
                        {
                            if (tab_url.Controls.Find(load_col[j] + i.ToString(), false)[0] is CheckBox)
                            {
                                ((CheckBox)(tab_url.Controls.Find(load_col[j] + i.ToString(), false)[0])).Checked = Convert.ToBoolean(Load_file.Param.url[i, j]);
                            }
                            else if (tab_url.Controls.Find(load_col[j] + i.ToString(), false)[0] is TextBox)
                            {
                                ((TextBox)(tab_url.Controls.Find(load_col[j] + i.ToString(), false)[0])).Text = Load_file.Param.url[i, j];
                            }
                            else if (tab_url.Controls.Find(load_col[j] + i.ToString(), false)[0] is ComboBox)
                            {
                                ((ComboBox)(tab_url.Controls.Find(load_col[j] + i.ToString(), false)[0])).SelectedIndexChanged -= new EventHandler(param_type_Change);
                                ((ComboBox)(tab_url.Controls.Find(load_col[j] + i.ToString(), false)[0])).SelectedItem = Load_file.Param.url[i, j];
                                ((ComboBox)(tab_url.Controls.Find(load_col[j] + i.ToString(), false)[0])).SelectedIndexChanged += new EventHandler(param_type_Change);
                            }
                        }
                    }
                }

                if (Load_file.Param.body != null)
                {
                    tab_body_rowcount = Load_file.Param.body_count;
                    if (tab_body_rowcount > 0)
                    {
                        ParamControl.SelectTab(tab_body);
                    }
                    for (int i = 0; i < tab_body_rowcount; i++)
                    {
                        add_Click_action(tab_body, i, false);
                        for (int j = 0; j < load_col.Length; j++)
                        {
                            if (tab_body.Controls.Find(load_col[j] + i.ToString(), false)[0] is CheckBox)
                            {
                                ((CheckBox)(tab_body.Controls.Find(load_col[j] + i.ToString(), false)[0])).Checked = Convert.ToBoolean(Load_file.Param.body[i, j]);
                            }
                            else if (tab_body.Controls.Find(load_col[j] + i.ToString(), false)[0] is TextBox)
                            {
                                ((TextBox)(tab_body.Controls.Find(load_col[j] + i.ToString(), false)[0])).Text = Load_file.Param.body[i, j];
                            }
                            else if (tab_body.Controls.Find(load_col[j] + i.ToString(), false)[0] is ComboBox)
                            {
                                ((ComboBox)(tab_body.Controls.Find(load_col[j] + i.ToString(), false)[0])).SelectedIndexChanged -= new EventHandler(param_type_Change);
                                ((ComboBox)(tab_body.Controls.Find(load_col[j] + i.ToString(), false)[0])).SelectedItem = Load_file.Param.body[i, j];
                                ((ComboBox)(tab_body.Controls.Find(load_col[j] + i.ToString(), false)[0])).SelectedIndexChanged += new EventHandler(param_type_Change);
                            }
                        }
                    }
                }

                if (Load_file.Param.header != null)
                {
                    tab_header_rowcount = Load_file.Param.header_count;
                    if (tab_header_rowcount > 0)
                    {
                        ParamControl.SelectTab(tab_header);
                    }
                    for (int i = 0; i < tab_header_rowcount; i++)
                    {
                        add_Click_action(tab_header, i, false);
                        for (int j = 0; j < load_col.Length; j++)
                        {
                            if (tab_header.Controls.Find(load_col[j] + i.ToString(), false)[0] is CheckBox)
                            {
                                ((CheckBox)(tab_header.Controls.Find(load_col[j] + i.ToString(), false)[0])).Checked = Convert.ToBoolean(Load_file.Param.header[i, j]);
                            }
                            else if (tab_header.Controls.Find(load_col[j] + i.ToString(), false)[0] is TextBox)
                            {
                                ((TextBox)(tab_header.Controls.Find(load_col[j] + i.ToString(), false)[0])).Text = Load_file.Param.header[i, j];
                            }
                            else if (tab_header.Controls.Find(load_col[j] + i.ToString(), false)[0] is ComboBox)
                            {
                                ((ComboBox)(tab_header.Controls.Find(load_col[j] + i.ToString(), false)[0])).SelectedIndexChanged -= new EventHandler(param_type_Change);
                                ((ComboBox)(tab_header.Controls.Find(load_col[j] + i.ToString(), false)[0])).SelectedItem = Load_file.Param.header[i, j];
                                ((ComboBox)(tab_header.Controls.Find(load_col[j] + i.ToString(), false)[0])).SelectedIndexChanged += new EventHandler(param_type_Change);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                Clear_TabPage_All();
            }
            //tiancity_login.Checked = Load_file.Login.Used;
            //tiancity_login_server.SelectedIndex = Load_file.Login.Server;
            //tiancity_userid.Text = Load_file.Login.Userid;
            //tiancity_password.Text = Load_file.Login.Password;
        }

        private void ParamIniToArray(string Param_Type)
        {
            string[] loadstrings = default_col;
        }

        private string[,] ParamStringToArray(string Param, int count)
        {
            if (Param.Length <= 0 || count == 0)
                return null;

            string[,] ReturnArray = new string[count, tab_colmax];
            string[] name_value = Param.Split('&');
            for (int i = 0; i < count; i++)
            {
                ReturnArray[i, 0] = "true";
                if (name_value[i].IndexOf('=') >= 0)
                {
                    ReturnArray[i, 1] = name_value[i].Split('=')[0];
                    ReturnArray[i, 2] = Coding.Urldecode(name_value[i].Split('=')[1], RequestEncoding);
                    //UrlDecode(name_value[i].Split('=')[1], RequestEncoding);
                }
                ReturnArray[i, 3] = "txt";
            }
            return ReturnArray;
        }



        private void submit_Click(object sender, EventArgs e)
        {
            //单个 backgroundWorker
            //多个
            //List<Thread> listThread = new List<Thread>(5);
            //Thread thread = null;
            //thread = new Thread(new ThreadStart(submit));
            //thread.name =;
            //thread.Start();
            //listThread.Add(thread);
            ////关闭指定线程
            //foreach (Thread tempThread in listThread)
            //{
            //    if (tempThread.Name == "Thread3")
            //    {
            //        Console.WriteLine(tempThread.Name + " 线程已关闭");
            //        tempThread.Abort();
            //    }
            //}
            if (submitstate == 1)
            {
                Thread exitThread = new Thread(stopThread);
                exitThread.Start();
                return;
            }
            cookie_login_list.Clear();
            cookie_other_list.Clear();
            output_text.Text = "";

            Resquest_data_read();

            Thread thread = null;

            if (listThread.Count == 0)
            {
                int usecount = 0;
                int importcount = 0;
                submit_state.Visible = true;
                submit_button_change(1);
                if (LoginControlSelectedIndex == 0)
                {
                    thread = new Thread(new ThreadStart(submit));
                    thread.Name = "0";
                    thread.Start();
                    listThread.Add(thread);
                    submit_state.Text = "请求提交中...";
                    //backgroundWorker.WorkerSupportsCancellation = true;
                    //backgroundWorker.RunWorkerAsync();
                }
                else if (LoginControlSelectedIndex == 1)
                {
                    if (import_users_flag || !tiancity_login.Checked)
                    {
                        usecount = Convert.ToInt32(tiancity_users_usecount.Text);
                        importcount = Convert.ToInt32(tiancity_users_importcount.Text);
                    }
                    else
                    {
                        MessageBox.Show("请先导入账号文本文件，文件格式：  账号,密码"); return;
                    }
                }
                else if (LoginControlSelectedIndex == 2)
                {
                    if (import_cookies_flag || !tiancity_login.Checked)
                    {
                        usecount = Convert.ToInt32(tiancity_cookies_usecount.Text);
                        importcount = Convert.ToInt32(tiancity_cookies_importcount.Text);
                    }
                    else
                    {
                        MessageBox.Show("请先导入Cookies文本文件"); return;
                    }
                }
                if (LoginControlSelectedIndex > 0)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(tiancity_users_usecount.Text, "^\\d+$") || !tiancity_login.Checked)
                    {
                        if ((usecount > 0 && usecount <= importcount) || !tiancity_login.Checked)
                        {
                            for (int i = 1; i <= usecount; i++)
                            {
                                thread = new Thread(new ThreadStart(submit));
                                thread.Name = i.ToString();
                                listThread.Add(thread);
                                thread.Start();
                            }
                        }
                        else
                        {
                            MessageBox.Show("请求数应输入小于等于总账号数量的正整数");
                        }
                    }
                    else
                    {
                        MessageBox.Show("请求数输入有误");
                    }
                }
            }
            else
            {
                MessageBox.Show("请求发送中，请稍后再试");
            }
        }

        private void browser_Click(object sender, EventArgs e)
        {
            BrowserForm browserForm = new BrowserForm();
            browserForm.ShowDialog(this);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string url = GetFullUrl();
            MyInvoke UpdateMsgAdd = new MyInvoke(UpdateMsgTextBoxAdd);
            //byte[] buffer;
            if (tiancity_login.Checked)
            {
                //bool loginsuccess = HTTPRequest(true);
                if (!Login_HTTPRequest())
                {
                    output_text.BeginInvoke(UpdateMsgAdd, new object[] { tiancity_userid.Text + "：登录失败" });
                    //MessageBox.Show("登录失败");
                    return;
                }
            }
            string responsetxt;
            int runnumber = 1;
            TimeSpan timeover = TimeSpan.Zero;

            if (continue_numbers.Checked || continue_times.Checked)
            {
                if (continue_times.Checked)
                {
                    timeover = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")) + continue_times_value;
                    if (continue_numbers.Checked) { runnumber = Convert.ToInt32(continue_numbers_input.Text); }
                    else { runnumber = -1; }
                }
                else
                {
                    timeover = TimeSpan.MaxValue;
                    runnumber = Convert.ToInt32(continue_numbers_input.Text);
                }
            }

            for (int i = 0; i != runnumber; runnumber--)
            {
                if (backgroundWorker.CancellationPending) { e.Cancel = true; return; };
                if (file_count > 0) { responsetxt = HTTPRequest(url, GetBodyStream()); }
                else { responsetxt = HTTPRequest(url, GetBodyText()); }
                browser.text = responsetxt;
                output_text.BeginInvoke(UpdateMsgAdd, new object[] { responsetxt });
                if (timeover == TimeSpan.Zero || timeover <= TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"))) { break; }
                if (continue_speed.Checked) { Thread.Sleep(continue_interval_millisecond); }
                if (runnumber < 0) { runnumber = -1; }
            }
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            submit_state.Text = "";
            submit_state.Visible = false;
            submit_button_change(0);
            if (e.Cancelled)
            {
                UpdateMsgTextBoxAdd("已取消");
            }
            //Settings.Default.RequestEncoding
            //output_text.Text = UrlDecode(browser.text, ResponseEncoding);
        }

        private void submit()
        {
            submit(Convert.ToInt32(Thread.CurrentThread.Name));
        }

        private void submit(int Tid)
        {
            MyInvoke UpdateMsgAdd = new MyInvoke(UpdateMsgTextBoxAdd);
            MyInvoke UpdateMsg = new MyInvoke(UpdateMsgBox);
            submit_state.BeginInvoke(UpdateMsg, new object[] { "当前正在提交的数量：" + listThread.Count.ToString() });
            string url = GetFullUrl();
            //byte[] buffer;

            if (tiancity_login.Checked)
            {
                if (LoginControlSelectedIndex == 0)
                {
                    if (!Login_HTTPRequest())
                    {
                        output_text.BeginInvoke(UpdateMsgAdd, new object[] { tiancity_userid.Text + "：登录失败" });
                        threadexit();
                        return;
                    }
                    else
                    {
                        if (Settings.Default.debug_msg)
                        {
                            output_text.BeginInvoke(UpdateMsgAdd, new object[] { tiancity_userid.Text + "：登录成功" });
                        }
                    }
                }
                else if (LoginControlSelectedIndex == 1)
                {
                    string[] info;
                    string strinfo = userinfo[Tid];
                    if (strinfo.IndexOf(',') > 0)
                    {
                        info = strinfo.Split(',');
                    }
                    else
                    {
                        info = new string[] { strinfo, "" };
                    }
                    //string[] info = userinfo[Tid].Split(new char[1] {','});
                    //bool loginsuccess = HTTPRequest();
                    if (!Login_HTTPRequest(info[0], info[1], Tid))
                    {
                        output_text.BeginInvoke(UpdateMsgAdd, new object[] { info[0] + "：登录失败" });
                        threadexit();
                        return;
                    }
                    else
                    {
                        if (Settings.Default.debug_msg)
                        {
                            output_text.BeginInvoke(UpdateMsgAdd, new object[] { info[0] + "：登录成功" });
                        }
                    }
                }
                else if (LoginControlSelectedIndex == 2)
                {
                    cookie_login_list.Add(Tid, usercookie[Tid]);
                }
            }

            string ResponseText;
            int runnumber = 1;
            TimeSpan timeover = TimeSpan.Zero;

            if (continue_numbers.Checked || continue_times.Checked)
            {
                if (continue_times.Checked)
                {
                    timeover = TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")) + continue_times_value;
                    if (continue_numbers.Checked) { runnumber = Convert.ToInt32(continue_numbers_input.Text); }
                    else { runnumber = -1; }
                }
                else
                {
                    timeover = TimeSpan.MaxValue;
                    runnumber = Convert.ToInt32(continue_numbers_input.Text);
                }
            }

            for (int i = 0; i != runnumber; runnumber--)
            {
                if (file_count > 0)
                {
                    ResponseText = HTTPRequest(url, GetBodyStream());
                }
                else
                {
                    ResponseText = HTTPRequest(url, GetBodyText());
                }

                ////
                string findstr = "";
                if (outputsearch_reg.Checked)
                {
                    Regex reg = new Regex(outputsearch_reg_input.Text);
                    Match regmatch = reg.Match(ResponseText);
                    findstr = regmatch.Value;
                }
                else if (outputsearch_side.Checked)
                {
                    findstr = search_string(ResponseText, outputsearch_side_L.Text, outputsearch_side_R.Text);
                }
                if (MemoryKey.Text.Length > 0 && findstr.Length > 0)
                {
                    ShareMemory.init(MemoryKey.Text, 1024);
                    ShareMemory.setdata_string(findstr, Encoding.Default);
                    if (Settings.Default.debug_msg)
                    {
                        output_text.BeginInvoke(UpdateMsgAdd, new object[] { "已在内存" + MemoryKey.Text + "中存入：" + findstr });
                    }
                }

                browser.text = ResponseText;
                output_text.BeginInvoke(UpdateMsgAdd, new object[] { ResponseText });
                if (timeover == TimeSpan.Zero || timeover <= TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss"))) { break; }
                if (continue_speed.Checked && (continue_numbers.Checked || continue_times.Checked)) { Thread.Sleep(continue_interval_millisecond); }
                if (runnumber < 0) { runnumber = -1; }
            }
            threadexit();
        }

        private void UpdateMsgBox(string msg)
        {
            //状态数据显示
            submit_state.Text = msg;
        }

        private void UpdateMsgTextBoxAdd(string msg)
        {
            UpdateMsgTextBoxAdd(msg, Settings.Default.output_len);
        }

        private void UpdateMsgTextBoxAdd(string msg, int len)
        {
            if (msg.Length == 0)
            {
                //output_text.Text = "";
                return;
            }
            if (len == -1)
            {
                output_text.AppendText(msg + "\n");
                return;
            }
            if (msg.Length > len)
            {
                msg = msg.Substring(0, len);
            }
            //接收数据显示
            if (len > 0)
            {
                msg += "\n";
            }
            output_text.AppendText(msg);
        }

        private void UpdateMsgButton(string msg)
        {
            http_submit.Text = msg;
        }

        private void submit_button_change(int state)
        {
            submitstate = state;
            string msg;
            MyInvokeBt UpdateMsgBt = new MyInvokeBt(UpdateMsgButton);
            switch (state)
            {
                case 0: msg = "提交"; break;
                case 1: msg = "终止"; break;
                default: msg = ""; break;
            }
            http_submit.BeginInvoke(UpdateMsgBt, new object[] { msg });
        }

        private void output_text_TextChanged(object sender, EventArgs e)
        {
            if (output_text.TextLength > 10000)
            {
                output_text.Clear();
            }
            //output_text.Focus();
            output_text.Select(output_text.TextLength, 0);
        }

        private void testcode_Click(object sender, EventArgs e)
        {
            //UpdateMsgTextBoxAdd(ShareMemory.getdata_Int32().ToString());
            UpdateMsgTextBoxAdd(ShareMemory.getdata_string(Encoding.Default));

            string value = @"{sda}sda{das}{aaa}aa";
            string pattern = @"{.+?}";
            MatchCollection Matches;
            Matches = Regex.Matches(value, pattern);

            HTTP.test();
            //MessageBox.Show(search_string(output_text.Text,textBox_L.Text,textBox_R.Text));

            //Random rd = new Random();
            //UpdateMsgTextBoxAdd(rd.Next(1, 3).ToString());
            //UpdateMsgTextBoxAdd(System.AppDomain.CurrentDomain.BaseDirectory);
            //UpdateMsgTextBoxAdd(new string('1', 11));
            //TimeSpan aa = new TimeSpan(DateTime.Now.Ticks);
            //UpdateMsgTextBoxAdd(aa.ToString());
            //int f = -99;
            //string str = "";
            //str = "url";
            //Button obj = (Button)sender;

            //f = add_Click_action(tab_url, tab_url_rowcount);
            //if (f == 0)
            //{
            //    tab_url_rowcount += 1;
            //}

            //Load_file.Param.url = new string[2,2];
            //Load_file.Param.url[1, 1] = "111";
            //Load_file.Param.url.Initialize();
            //MessageBox.Show(Settings.Default.http_proxyip);
            //controls find
            //output_text.Text = ((TextBox)(tab_url.Controls.Find("0", false)[1])).Text;
            //string aa  = ((TextBox)(tab_url.Controls.Find("name_0", false)[0])).Text;
            //bool bb = ((CheckBox)(tab_url.Controls.Find("0", false)[0])).Checked;

        }

        private void testcode2_Click(object sender, EventArgs e)
        {
            MatchCollection aa = Regex.Matches("{asda}dsasd{abb}{bb}{}123", @"{.*?[^\\]}");
            string aaa = aa[1].ToString();

            //ShareMemory.setdata_Int32(Convert.ToInt32(continue_numbers_input.Text));
            ShareMemory.setdata_string(http_url.Text, Encoding.UTF8);

            //Thread thread = null;
            //for (int i = 0; i < 3; i++)
            //{
            //    thread = new Thread(new ThreadStart(submit));
            //    thread.Start();
            //}

            //TimeSpan ts = new TimeSpan(0, 2, 0);
            //TimeSpan a = TimeSpan.FromMilliseconds(10000); 
            //MessageBox.Show(TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")).ToString());
            //MessageBox.Show(TimeSpan.Zero.ToString());

            //Clear_Interface();
            //MessageBox.Show(LoginControlSelectedIndex.ToString());
            //TimeSpan time = TimeSpan.Parse("01:00:00");
            //ThreadTime.TimeHelper myTime = new ThreadTime.TimeHelper(time, label1);
            //myTime.Open();
        }

        private void testcode3_Click(object sender, EventArgs e)
        {
            ShareMemory.init(MemoryKey.Text, 1000);
        }

        private void threadexit()
        {
            listThread.Remove(Thread.CurrentThread);
            MyInvoke UpdateMsg = new MyInvoke(UpdateMsgBox);
            if (listThread.Count == 0)
            {
                submit_state.BeginInvoke(UpdateMsg, new object[] { "" });
                submit_button_change(0);
            }
            else { submit_state.BeginInvoke(UpdateMsg, new object[] { "当前正在提交的数量：" + listThread.Count.ToString() }); }
            Thread.CurrentThread.Abort();
        }

        private void stopThread()
        {
            stoplistThread();
            MyInvoke UpdateMsg = new MyInvoke(UpdateMsgBox);
            submit_state.BeginInvoke(UpdateMsg, new object[] { "" });
            submit_button_change(0);
            //UpdateMsgBox("");
        }

        private void stoplistThread()
        {
            if (listThread.Count > 0)
            {
                try
                {
                    MyInvoke UpdateMsgAdd = new MyInvoke(UpdateMsgTextBoxAdd);
                    MyInvoke UpdateMsg = new MyInvoke(UpdateMsgBox);
                    int scount = 0;
                    foreach (Thread tempThread in listThread)
                    {
                        output_text.BeginInvoke(UpdateMsgAdd, new object[] { "线程" + tempThread.Name + "：已停止" });
                        submit_state.BeginInvoke(UpdateMsg, new object[] { "当前正在提交的数量：" + (listThread.Count - scount).ToString() });
                        //UpdateMsgTextBoxAdd("线程" + tempThread.Name + "：已停止");
                        tempThread.Abort();
                        scount++;
                        //listThread.Remove(tempThread);
                        //if (tempThread.Name == "Thread3")
                        //{
                        //    Console.WriteLine(tempThread.Name + " 线程已关闭");
                        //    tempThread.Abort();
                        //}
                    }
                    listThread.Clear();
                }
                catch (Exception)
                {
                    //stoplistThread(); 
                }
            }
            else
            {
                UpdateMsgTextBoxAdd("请求线程已全部停止");
            }
        }


        private void continue_config_CheckedChanged(object sender, EventArgs e)
        {
            continue_panel.Location = config_panel_location;
            continue_panel.Visible = continue_config.Checked;
        }

        private void continue_numbers_CheckedChanged(object sender, EventArgs e)
        {
            continue_numbers_input.Enabled = continue_numbers.Checked;
        }

        private void continue_times_CheckedChanged(object sender, EventArgs e)
        {
            continue_times_input.Enabled = continue_times.Checked;
        }

        private void continue_speed_CheckedChanged(object sender, EventArgs e)
        {
            continue_speed_input.Enabled = continue_speed.Checked;
        }

        private void continue_numbers_input_TextChanged(object sender, EventArgs e)
        {
            if (continue_numbers_input.TextLength == 0) { continue_numbers_input.Text = "1"; }
        }

        private void continue_times_input_TextChanged(object sender, EventArgs e)
        {
            if (continue_times_input.TextLength == 0) { continue_times_input.Text = "1"; }
            update_times_unit();
        }

        private void continue_speed_input_TextChanged(object sender, EventArgs e)
        {
            if (continue_speed_input.TextLength == 0) { continue_speed_input.Text = "1"; }
            update_speed_tsinterval();
        }

        private void continue_times_unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            continue_timeunit = continue_times_unit.SelectedIndex;
            update_times_unit();
        }

        private void continue_speed_unit_SelectedIndexChanged(object sender, EventArgs e)
        {
            continue_speedunit = continue_speed_unit.SelectedIndex;
            update_speed_tsinterval();
        }

        private void update_times_unit()
        {//1.秒//2.分钟//3.小时
            TimeSpan tsrun = TimeSpan.Zero;// = TimeSpan.Parse("00:00:00");
            switch (continue_times_unit.SelectedIndex)
            {
                case 0: tsrun = TimeSpan.Zero; break;
                case 1: tsrun = new TimeSpan(0, 0, Convert.ToInt32(continue_times_input.Text)); break;
                case 2: tsrun = new TimeSpan(0, Convert.ToInt32(continue_times_input.Text), 0); break;
                case 3: tsrun = new TimeSpan(Convert.ToInt32(continue_times_input.Text), 0, 0); break;
                default: break;
            }
            continue_times_value = tsrun;
        }

        private void update_speed_tsinterval()
        {//1.秒//2.分钟//3.小时//4.毫秒/次//5.秒/次//6.分钟/次//7.小时/次
            TimeSpan ts = TimeSpan.Zero;
            int tsinterval = 0; //毫秒
            switch (continue_speed_unit.SelectedIndex)
            {
                case 0: tsinterval = 0; break;
                case 1: ts = new TimeSpan(0, 0, 1); tsinterval = (int)ts.TotalMilliseconds / Convert.ToInt32(continue_speed_input.Text); break;
                case 2: ts = new TimeSpan(0, 1, 0); tsinterval = (int)ts.TotalMilliseconds / Convert.ToInt32(continue_speed_input.Text); break;
                case 3: ts = new TimeSpan(1, 0, 0); tsinterval = (int)ts.TotalMilliseconds / Convert.ToInt32(continue_speed_input.Text); break;
                case 4: tsinterval = Convert.ToInt32(continue_speed_input.Text); break;
                case 5: tsinterval = Convert.ToInt32(continue_speed_input.Text) * 1000; break;
                case 6: tsinterval = Convert.ToInt32(continue_speed_input.Text) * 1000 * 60; break;
                case 7: tsinterval = Convert.ToInt32(continue_speed_input.Text) * 1000 * 60 * 60; break;
            }
            continue_interval_millisecond = tsinterval;
        }


        private void outputsearch_config_CheckedChanged(object sender, EventArgs e)
        {
            output_panel.Location = config_panel_location;
            output_panel.Visible = outputsearch_config.Checked;
        }

        private void outputsearch_txt_CheckedChanged(object sender, EventArgs e)
        {
            outputsearch_txt_input.Enabled = outputsearch_txt.Checked;
        }

        private void outputsearch_reg_CheckedChanged(object sender, EventArgs e)
        {
            outputsearch_reg_input.Enabled = outputsearch_reg.Checked;
        }

        private void outputsearch_side_CheckedChanged(object sender, EventArgs e)
        {
            outputsearch_side_L.Enabled = outputsearch_side.Checked;
            outputsearch_side_R.Enabled = outputsearch_side.Checked;
        }


        private bool textnumbercheck(string text)
        {
            return textnumbercheck(text, null, "");
        }

        private bool textnumbercheck(string text, int[] exclude)
        {
            return textnumbercheck(text, exclude, "");
        }

        private bool textnumbercheck(string text, int[] exclude, string msg)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(text, "^\\d+$"))
            {
                return true;
            }
            if (msg.Length > 0)
            {
                //   MessageBox.Show(msg);
            }
            return false;
        }

        private bool numberinput(int KeyChar)
        {
            return numberinput(KeyChar, null);
        }

        private bool numberinput(int KeyChar, string input)
        {
            //判断按键是不是要输入的类型。
            if ((KeyChar < 48 || KeyChar > 57) && KeyChar != 8 && KeyChar != 46)
            {
                return false;
            }
            //小数点的处理。
            if (KeyChar == 46 && input != null)  //小数点处理
            {
                if (input.Length <= 0)
                    return false;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(input, out oldf);
                    b2 = float.TryParse(input + KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            return false;
                        else
                            return true;
                    }
                }
            }
            return true;
        }

        private bool numberinputonlyint(int KeyChar)
        {
            //判断按键是不是要输入的类型。
            if ((KeyChar < 48 || KeyChar > 57) && KeyChar != 8)
            {
                return false;
            }
            return true;
        }

        private void numbers_check_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (numberinputonlyint(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void ParamControl_indexchange(object sender, EventArgs e)
        {
            //    TabControl tabControl = (TabControl)sender;
            //    //tabControl.TabIndex
            //    //MessageBox.Show(tabControl.SelectedIndex.ToString());
            //    switch (tabControl.SelectedIndex)
            //    {
            //        case 0: tab_url.VerticalScroll.Value = 0; break;
            //        case 1: break;
            //        case 2: break;
            //        default: break;
            //    }
            //    //foreach (Control btn in tabControl.Controls)
            //    //{
            //    //    if (btn is Button)
            //    //    {
            //    //        btn.Location = new Point(btn.Location.X, btn.Location.Y - 40);
            //    //    }
            //    //}
        }

        private void StripMenu_proxy_Click(object sender, EventArgs e)
        {
            proxy ProxyForm = new proxy();
            ProxyForm.Owner = this;
            ProxyForm.ShowDialog(this);
        }

        private void output_clear_Click(object sender, EventArgs e)
        {
            output_text.Text = "";
        }


        public static string search_string(string s, string s1, string s2)  //获取搜索到的数目  
        {
            int n1, n2;
            n1 = s.IndexOf(s1, 0) + s1.Length;   //开始位置  
            n2 = s.IndexOf(s2, n1);               //结束位置    
            return s.Substring(n1, n2 - n1);   //取搜索的条数，用结束的位置-开始的位置,并返回    
        }

    }
}
