using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms.Design;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows;

namespace WebRequestTools
{
    public partial class proxy : Form
    {
        public proxy()
        {
            InitializeComponent();
        }

        private Dictionary<int, string> proxylistinfo = new Dictionary<int, string>();
        private Dictionary<string, Thread> ConnecetThreads = new Dictionary<string, Thread>();
        private Dictionary<string, Thread> TimeoutThreads = new Dictionary<string, Thread>();
        private Dictionary<string, IPEndPoint> iplist = new Dictionary<string, IPEndPoint>();
        private string proxyselect = "";
        private string filename = proxyinfo.filename;
        private string filepath = proxyinfo.filepath;
        private string proxymatch = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\:\d{1,5}$";
        private string ipmatch = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
        private string portmatch = @"\d{1,5}$";

        private delegate void DataGridTextInvoke(string msg, int row, int col);

        private void proxy_Load(object sender, EventArgs e)
        {
            proxymodel.SelectedIndex = Settings.Default.proxy_type;
            onloadproxy();
            //onloadreadfile(filepath + filename);
        }

        private void proxylist_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private bool updateproxylist()
        {
            string proxystr = "";
            string listip = "";
            string listport = "";
            int n = 0;
            bool s = true;
            if (proxylistinfo != null) { proxylistinfo.Clear(); }
            if (proxymodel.SelectedIndex == 0)
            { return true; }
            if (proxymodel.SelectedIndex == 1 || proxymodel.SelectedIndex == 2)
            {
                for (int i = 0; i < proxylistdata.Rows.Count; i++)
                {
                    proxylistdata.Rows[i].ErrorText = "";
                    if (!proxylistdata.Rows[i].IsNewRow)
                    {
                        if (proxylistdata.Rows[i].Cells["proxyip"].Value != null) { listip = proxylistdata.Rows[i].Cells["proxyip"].Value.ToString().Trim(); }
                        if (proxylistdata.Rows[i].Cells["proxyport"].Value != null) { listport = proxylistdata.Rows[i].Cells["proxyport"].Value.ToString().Trim(); }
                        proxystr = listip + ":" + listport;
                        if (Regex.IsMatch(proxystr, proxymatch))
                        {
                            proxylistinfo.Add(n, proxystr);
                            n++;
                        }
                        else
                        {
                            proxylistdata.Rows[i].ErrorText = "格式不正确";
                            s = false;
                        }
                    }
                }
            }
            if (proxymodel.SelectedIndex == 3 || proxymodel.SelectedIndex == 4)
            {
                for (int i = 0; i < proxylistdata.SelectedRows.Count; i++)
                {
                    proxylistdata.SelectedRows[i].ErrorText = "";
                    if (!proxylistdata.SelectedRows[i].IsNewRow)
                    {
                        if (proxylistdata.SelectedRows[i].Cells["proxyip"].Value != null) { listip = proxylistdata.SelectedRows[i].Cells["proxyip"].Value.ToString().Trim(); }
                        if (proxylistdata.SelectedRows[i].Cells["proxyport"].Value != null) { listport = proxylistdata.SelectedRows[i].Cells["proxyport"].Value.ToString().Trim(); }
                        proxystr = listip + ":" + listport;
                        if (Regex.IsMatch(proxystr, proxymatch))
                        {
                            proxylistinfo.Add(n, proxystr);
                            n++;
                        }
                        else
                        {
                            proxylistdata.SelectedRows[i].ErrorText = "格式不正确";
                            s = false;
                        }
                    }
                }
            }

            if (s) return true;
            else return false;
            //int i = 1;
            //foreach (string proxystr in proxylistbox.Items)
            //{
            //    proxylistinfo.Add(i, proxystr);
            //    i++;
            //}
        }

        private void proxy_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void addproxy_Click(object sender, EventArgs e)
        {
            addproxy();
        }

        private void addproxy()
        {
            if (Regex.IsMatch(proxyselect, proxymatch))
            {
                MessageBox.Show(proxytxt.Text + "格式不正确");
                return;
            }
            if (!proxylistbox.Items.Contains(proxytxt.Text))
            {
                proxylistbox.Items.Add(proxytxt.Text);
                //dataGridView1[0, dataGridView1.NewRowIndex].Value = proxytxt.Text;
                //dataGridView1.Rows.Add("1", "2");
            }
            else
            {
                MessageBox.Show(proxytxt.Text + "已存在");
            }
        }

        private void proxymodel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //不使用代理
            //使用单一代理（选中）
            //使用顺序代理（列表）
            //使用随机代理（列表）
            if (proxymodel.SelectedIndex == 0)
            {
                proxytxt.Enabled = false;
                proxyadd.Enabled = false;
                proxyimport.Enabled = false;
                proxycheck.Enabled = false;
                proxydelete.Enabled = false;
                proxyclear.Enabled = false;
            }
            else if (proxymodel.SelectedIndex > 0)
            {
                proxytxt.Enabled = true;
                proxyadd.Enabled = true;
                proxyimport.Enabled = true;
                proxycheck.Enabled = true;
                proxydelete.Enabled = true;
                proxyclear.Enabled = true;
            }
            Settings.Default.proxy_type = proxymodel.SelectedIndex;
        }

        private void proxyimport_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            OpenFile.Title = "选择文件";
            OpenFile.Filter = "TXT|*.txt|All files|*.*";
            OpenFile.ValidateNames = true;
            OpenFile.CheckFileExists = true;
            OpenFile.CheckPathExists = true;
            if (OpenFile.ShowDialog() == DialogResult.OK)
            {
                readproxyfile(OpenFile.FileName);
            }
        }

        private void readproxyfile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            try
            {
                string linetxt = "";
                int newindex = proxylistdata.NewRowIndex;
                do
                {
                    linetxt = sr.ReadLine();
                    if (linetxt == null) { linetxt = ""; }
                    if (linetxt.Length > 0)
                    {
                        if (linetxt.IndexOf(':') == -1) { linetxt += ":"; }
                        if (proxylistcheckexist(newindex, linetxt) == -1)
                        {
                            newindex = proxylistdata.Rows.Add();
                            proxylistdata.Rows[newindex].SetValues(linetxt.Split(':')[0], linetxt.Split(':')[1], "");
                        }
                    }
                    //if (linetxt.Length > 0 && !proxylistbox.Items.Contains(linetxt))
                    //{
                    //    proxylistbox.Items.Add(linetxt);
                    //}
                } while (!sr.EndOfStream);
            }
            catch (Exception ex)
            {
                MessageBox.Show(path + "读取失败\r\n" + ex.StackTrace + "\r\n" + ex.Message);
            }
            finally
            {
                sr.Close();
                fs.Close();
            }
        }

        private void writeproxyfile(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                string wstr;
                for (int i = 0; i < proxylistdata.Rows.Count; i++)
                {
                    if (proxylistdata.Rows[i].IsNewRow) { break; }
                    if (proxylistdata.Rows[i].Cells[0].Value != null)
                    {
                        wstr = proxylistdata.Rows[i].Cells[0].Value.ToString();
                        if (proxylistdata.Rows[i].Cells[1].Value != null)
                        {
                            wstr += ":" + proxylistdata.Rows[i].Cells[1].Value.ToString();
                        }
                        sw.WriteLine(wstr);
                    }
                }
                //foreach (string str in proxylistbox.Items)
                //{
                //    sw.WriteLine(str);
                //}
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

        private void onloadreadfile(string path)
        {
            //"proxylist.txt";
            if (File.Exists(@path))
            {
                readproxyfile(path);
                label5.Visible = true;
                label5.Text = "已加载文件" + path;
            }
        }

        private List<string> loadproxysetting(int Range)
        {
            List<string> proxylists = new List<string>();
            string proxylist = "";

            if (Range == 1)
            { proxylist = Settings.Default.proxy_all; }
            else if (Range == 2)
            { proxylist = Settings.Default.proxy_use; }

            if (proxylist.Length == 0)
            {
                return proxylists;
            }
            else
            {
                foreach (string str in proxylist.Split(';', ','))
                {
                    proxylists.Add(str);
                }
            }
            return proxylists;
        }

        private void writeproxysetting(int Range)
        {
            string str = "";
            if (Range == 1)
            {
                for (int i = 0; i < proxylistdata.Rows.Count; i++)
                {
                    if (!proxylistdata.Rows[i].IsNewRow)
                    {
                        if (proxylistdata.Rows[i].Cells[0].Value != null)
                        {
                            str += proxylistdata.Rows[i].Cells[0].Value.ToString();
                            if (proxylistdata.Rows[i].Cells[1].Value != null)
                            {
                                str += ":" + proxylistdata.Rows[i].Cells[1].Value.ToString() + ",";
                            }
                            else
                            {
                                str += ":" + ",";
                            }
                        }
                    }
                }
                Settings.Default.proxy_all = str;
            }
            else if (Range == 2)
            {
                for (int i = 0; i < proxylistdata.SelectedRows.Count; i++)
                {
                    if (!proxylistdata.SelectedRows[i].IsNewRow)
                    {
                        if (proxylistdata.SelectedRows[i].Cells[0].Value != null)
                        {
                            str += proxylistdata.SelectedRows[i].Cells[0].Value.ToString();
                            if (proxylistdata.SelectedRows[i].Cells[1].Value != null)
                            {
                                str += ":" + proxylistdata.SelectedRows[i].Cells[1].Value.ToString() + ",";
                            }
                            else
                            {
                                str += ":" + ",";
                            }
                        }
                    }
                }
                Settings.Default.proxy_use = str;
            }            
        }

        private void onloadproxy()
        {//1.all,2.selected
            List<string> proxylists = new List<string>();
            proxylists = loadproxysetting(1);
            int index = proxylistdata.NewRowIndex;
            for (int i = 0; i < proxylists.Count; i++)
            {
                if (proxylists[i].Length > 0)
                {
                    if (proxylists[i].IndexOf(':') == -1) { proxylists[i] += ":"; }
                    if (proxylistcheckexist(index, proxylists[i]) == -1)
                    {
                        index = proxylistdata.Rows.Add();
                        proxylistdata.Rows[index].SetValues(proxylists[i].Split(':')[0], proxylists[i].Split(':')[1], "");
                    }
                }
            }
            proxylists.Clear();
            if (proxylistdata.Rows[0].Selected) { proxylistdata.Rows[0].Selected = false; }
            proxylists = loadproxysetting(2);
            for (int i = 0; i < proxylists.Count; i++)
            {
                if (proxylists[i].Length > 0)
                {
                    index = proxylistcheckexist(-1, proxylists[i]);
                    if (index >= 0 )
                    {
                        proxylistdata.Rows[index].Selected = true;
                    }
                }
            }
        }

        private int proxylistcheckexist(int checkindex, string proxy)
        {
            //-1不存在，返回存在的行数
            if (proxy == null || proxy.Length == 0 || proxy.IndexOf(':') == -1)
            { return -1; }
            string ip = proxy.Split(':')[0].Trim();
            string port = proxy.Split(':')[1].Trim();
            string listip = "";
            string listport = "";
            for (int i = 0; i < proxylistdata.Rows.Count; i++)
            {
                if (i != checkindex)
                {
                    if (proxylistdata.Rows[i].Cells["proxyip"].Value != null) { listip = proxylistdata.Rows[i].Cells["proxyip"].Value.ToString().Trim(); }
                    if (proxylistdata.Rows[i].Cells["proxyport"].Value != null) { listport = proxylistdata.Rows[i].Cells["proxyport"].Value.ToString().Trim(); }
                    if (listip == ip && listport == port)
                    {
                        return i;
                    }
                }
            }
            return -1; 
        }

        private void proxycheck_Click(object sender, EventArgs e)
        {
            if (ConnecetThreads.Count != 0 || TimeoutThreads.Count != 0)
            {
                MessageBox.Show("代理检测中稍后再试");
                return;
            }
            if (proxylistdata.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择需要检查的代理（选择行）");
                return;
            }
            string tmpip = "", tmpport = "";
            string n = "";
            iplist.Clear();
            for (int i = 0; i < proxylistdata.SelectedRows.Count; i++)
            {
                if (!proxylistdata.SelectedRows[i].IsNewRow)
                {
                    proxylistdata.SelectedRows[i].ErrorText = "";
                    proxylistdata.SelectedRows[i].Cells["proxytimelimit"].Value = "";
                    if (proxylistdata.SelectedRows[i].Cells["proxyip"].Value != null) { tmpip = proxylistdata.SelectedRows[i].Cells["proxyip"].Value.ToString().Trim(); }
                    if (proxylistdata.SelectedRows[i].Cells["proxyport"].Value != null) { tmpport = proxylistdata.SelectedRows[i].Cells["proxyport"].Value.ToString().Trim(); }
                    n = proxylistdata.SelectedRows[i].Index.ToString();
                    if (Regex.IsMatch(tmpip, ipmatch) && Regex.IsMatch(tmpport, portmatch))
                    {
                        Thread ConnecetThread = new Thread(new ThreadStart(proxynetconnect));
                        ConnecetThread.Name = n;
                        Thread TimeoutThread = new Thread(new ThreadStart(proxytimeout));
                        TimeoutThread.Name = n;
                        iplist.Add(n, new IPEndPoint(IPAddress.Parse(tmpip), int.Parse(tmpport)));
                        ConnecetThreads.Add(ConnecetThread.Name, ConnecetThread);
                        TimeoutThreads.Add(TimeoutThread.Name, TimeoutThread);
                        ConnecetThread.Start();// Thread.Sleep(2000);
                        TimeoutThread.Start();
                    }
                    else
                    {
                        proxylistdata.SelectedRows[i].ErrorText = "格式不正确";
                    }
                }
            }
            return;

            //proxylistdata.SelectedRows[1].SetValues("1", "2", 12);

            //if (proxylistbox.SelectedItem == null)
            //{
            //    MessageBox.Show("请先选择需要测试的代理");
            //    return;
            //}
            //proxyselect = proxylistbox.SelectedItem.ToString();

            //if (Regex.IsMatch(proxyselect, ipmatch))
            //{
            //    if (TimeoutThread != null && TimeoutThread.IsAlive) { MessageBox.Show(proxyselect + " 检测中稍后再试"); return; }
            //    ConnecetThread = new Thread(new ThreadStart(proxynetconnect));
            //    ConnecetThread.Start();
            //    TimeoutThread = new Thread(new ThreadStart(proxytimeout));
            //    TimeoutThread.Start();
            //    //proxytxt.Text = test.Connect("219.94.232.243", "80").ToString();
            //}
            //else
            //{
            //    MessageBox.Show("代理IP：" + proxyselect + " 格式有误");
            //    return;
            //}
        }

        private void proxynetconnect()
        {
            string n = Thread.CurrentThread.Name;
            try
            {
                DataGridTextInvoke UpdateDataTextGrid = new DataGridTextInvoke(UpdateDataTextGridMsg);
                net test = new net();
                bool t;
                TimeSpan tsbegin = new TimeSpan(DateTime.Now.Ticks);
                if (test.Connect(iplist[Thread.CurrentThread.Name]))
                {
                    t = true;
                }
                else
                {
                    t = false;
                }
                TimeSpan tsend = new TimeSpan(DateTime.Now.Ticks);
                //UpdateDataTextGrid.Invoke(TimeoutThreads[n].ThreadState.ToString(), int.Parse(n));
                if (TimeoutThreads[n] != null && TimeoutThreads[n].IsAlive)
                {
                    if (TimeoutThreads[n].ThreadState == ThreadState.Running)
                    {
                        TimeoutThreads[n].Abort();
                        if (TimeoutThreads.ContainsKey(n)) { TimeoutThreads.Remove(n); }
                        if (ConnecetThreads.ContainsKey(n)) { ConnecetThreads.Remove(n); }
                    }
                }
                if (t)
                {
                    int ms = Convert.ToInt32(tsend.TotalMilliseconds - tsbegin.TotalMilliseconds);
                    string msg;
                    if (ms == 0) { msg = "< 1"; }
                    else { msg = ms.ToString(); }
                    UpdateDataTextGrid.Invoke(msg, Convert.ToInt32(n), 2);
                    //UpdateDataTextGrid.Invoke((tsend.TotalMilliseconds - tsbegin.TotalMilliseconds).ToString(), int.Parse(n), 2);
                    //UpdateDataTextGrid.Invoke("连接成功", int.Parse(n), 2);
                    //MessageBox.Show("连接成功");
                }
                else
                {
                    UpdateDataTextGrid.Invoke("连接失败", int.Parse(n), 2);
                    //MessageBox.Show("连接失败");
                }
            }
            catch
            { }
            finally
            {
                if (TimeoutThreads.ContainsKey(n)) { TimeoutThreads.Remove(n); }
                if (ConnecetThreads.ContainsKey(n)) { ConnecetThreads.Remove(n); }
            }
            //MessageBox.Show(test.Connect(proxytxt.Text, "80").ToString());
        }

        private void proxytimeout()
        {
            string n = Thread.CurrentThread.Name;
            try
            {
                DataGridTextInvoke UpdateDataTextGrid = new DataGridTextInvoke(UpdateDataTextGridMsg);
                Thread.Sleep(Convert.ToInt32(proxytimeoutseconds.Text));
                if (ConnecetThreads[n] != null && ConnecetThreads[n].IsAlive)
                {
                    if (ConnecetThreads[n].ThreadState == ThreadState.Running)
                    {
                        ConnecetThreads[n].Abort();
                        if (TimeoutThreads.ContainsKey(n)) { TimeoutThreads.Remove(n); }
                        if (ConnecetThreads.ContainsKey(n)) { ConnecetThreads.Remove(n); }
                    }
                    //MessageBox.Show("连接超时");                    
                }
                UpdateDataTextGrid.Invoke("连接超时", int.Parse(n), 2);
            }
            catch
            { }
            finally
            {
                if (TimeoutThreads.ContainsKey(n)) { TimeoutThreads.Remove(n); }
                if (ConnecetThreads.ContainsKey(n)) { ConnecetThreads.Remove(n); }
            }
            //TimeoutThreads.Remove(n);
            //if (ConnecetThread != null && ConnecetThread.IsAlive)
            //{
            //    ConnecetThread.Abort();
            //    MessageBox.Show(proxytxt.Text + "   连接超时");
            //}
        }

        private void UpdateDataTextGridMsg(string msg, int row, int col)
        {
            proxylistdata.Rows[row].Cells[col].Value = msg;
        }

        private void proxydelete_Click(object sender, EventArgs e)
        {
            if (proxylistdata.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先选择需要删除的代理（选择行）");
                return;
            }

            List<int> listindex = new List<int>();
            for (int i = 0; i < proxylistdata.SelectedRows.Count; i++)
            {
                listindex.Add(proxylistdata.SelectedRows[i].Index);
            }
            listindex.Sort();
            listindex.Reverse();
            foreach (int i in listindex)
            {
                if (!proxylistdata.Rows[i].IsNewRow)
                {
                    proxylistdata.Rows.RemoveAt(i);
                }
            }

            //proxylistdata.SelectedRows[0].
            //if (proxylistbox.SelectedIndex != -1)
            //{
            //    proxylistbox.Items.RemoveAt(proxylistbox.SelectedIndex);
            //}
        }

        private void proxyclear_Click(object sender, EventArgs e)
        {
            //while (this.dataGridView.Rows.Count != 0)
            //{
            //    this.dataGridView.Rows.RemoveAt(0);
            //}

            //proxylistbox.Items.Clear();
            proxylistdata.Rows.Clear();
        }

        private void proxytimeoutseconds_TextChanged(object sender, EventArgs e)
        {
            if (proxytimeoutseconds.TextLength == 0)
            {
                proxytimeoutseconds.Text = "1";
            }
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

        private void proxylist_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (proxylistbox.SelectedIndex != -1)
                {
                    proxylistbox.Items.RemoveAt(proxylistbox.SelectedIndex);
                }
            }
        }

        private void proxytxt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addproxy();
            }
        }

        private void proxylistdata_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //proxylistdata.Rows[e.RowIndex].ErrorText= "";
            //if (e.FormattedValue != null)
            //{
            //    Regex.IsMatch(e.FormattedValue.ToString(), ipmatch);
            //    e.Cancel = true;
            //}


        }

        private void proxylistdata_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewTextBoxEditingControl CellEdit = null;
            CellEdit = (DataGridViewTextBoxEditingControl)e.Control;
            CellEdit.SelectAll();
            //CellEdit.KeyDown += Cells_KeyDown;
            CellEdit.KeyPress += Cells_KeyPress;
        }

        private void Cells_KeyDown(object sender, KeyEventArgs e)
        {
            if (proxylistdata.CurrentCell.ColumnIndex == 0)
            {
                if ((e.KeyValue < 48 || e.KeyValue > 57) && e.KeyValue != 8 && e.KeyValue != 46 && e.KeyValue != 13)
                { e.Handled = true; }
                else
                { e.Handled = false; }
            }
            else if (proxylistdata.CurrentCell.ColumnIndex == 1)
            {
                if ((e.KeyValue < 48 || e.KeyValue > 57) && e.KeyValue != 8 && e.KeyValue != 13)
                { e.Handled = true; }
                else
                { e.Handled = false; }
            }
        }

        private void Cells_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (proxylistdata.CurrentCell.ColumnIndex == proxylistdata.Columns["proxyip"].Index)
            {
                if ((Convert.ToInt32(e.KeyChar) < 48 || Convert.ToInt32(e.KeyChar) > 57) && Convert.ToInt32(e.KeyChar) != 46 && Convert.ToInt32(e.KeyChar) != 8 && Convert.ToInt32(e.KeyChar) != 13)
                {
                    e.Handled = true;  // 输入非法就屏蔽
                    return;
                }
            }
            else if (proxylistdata.CurrentCell.ColumnIndex == proxylistdata.Columns["proxyport"].Index)
            {
                if ((Convert.ToInt32(e.KeyChar) < 48 || Convert.ToInt32(e.KeyChar) > 57) && Convert.ToInt32(e.KeyChar) != 46 && Convert.ToInt32(e.KeyChar) != 8 && Convert.ToInt32(e.KeyChar) != 13 && Convert.ToInt32(e.KeyChar) == 46)
                {
                    e.Handled = true;  // 输入非法就屏蔽
                    return;
                }

            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if ((proxymodel.SelectedIndex == 3 || proxymodel.SelectedIndex == 4) && proxylistdata.SelectedRows.Count == 0)
            {
                MessageBox.Show("请在列表中选择需要使用的代理");
                return;
            }
            if (!updateproxylist())
            {
                MessageBox.Show("使用的代理中格式有误");
                return;
            }
            writeproxysetting(1);
            writeproxysetting(2);
            //writeproxyfile(filepath + filename);
            Settings.Default.Save();
            //if (proxymodel.SelectedIndex == 1 && proxylist.SelectedItem == null)
            //{
            //    MessageBox.Show("请在列表中选择代理");
            //    return;
            //}           
            //if (proxylistbox.SelectedItem == null) { proxystr = ""; }
            //else proxystr = proxylistbox.SelectedItem.ToString();
            Webrequset Ownerfrm;
            Ownerfrm = (Webrequset)this.Owner;
            Ownerfrm.proxy_load();
            Close();
        }

        //private void Saveastxt_Click(object sender, EventArgs e)
        //{
        //    writeproxyfile(filepath + filename);
        //}

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
