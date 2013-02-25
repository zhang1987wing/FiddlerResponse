using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WebRequestTools
{
    public partial class Config : Form
    {
        public Config()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, EventArgs e)
        {
            //string ipmatch = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\:\d{1,5}$";
            //if (Regex.IsMatch(http_proxyip.Text, ipmatch) || http_proxyip.Text.Trim().Length == 0)
            //{
            //    Settings.Default.http_proxyip = http_proxyip.Text;
            //}
            //else
            //{
            //    MessageBox.Show("代理IP 设置有误");
            //    return;
            //}
            if (RequestEncode.SelectedIndex == 0)
            {
                Settings.Default.RequestEncoding = "";
            }
            else
            {
                Settings.Default.RequestEncoding = RequestEncode.Text;
            }
            Settings.Default.ResponseEncoding = ResponseEncode.Text;
            Settings.Default.output_len = Convert.ToInt32(ResponseOutputLen.Text);
            Settings.Default.RequestEncoding_autoheader = RequestEncode_autoheader.Checked;
            Settings.Default.debug_msg = debug_msg.Checked;
            Settings.Default.Login_tiancity_url = Login_url.Text;
            Settings.Default.Save();
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            Webrequset Ownerfrm;
            Ownerfrm = (Webrequset)this.Owner;
            Ownerfrm.Config_Load();
        }

        private void Config_Load(object sender, EventArgs e)
        {
            //http_proxyip.Text = Settings.Default.http_proxyip;
            ResponseOutputLen.Text = Settings.Default.output_len.ToString();
            RequestEncode.Text = Settings.Default.RequestEncoding;
            ResponseEncode.Text = Settings.Default.ResponseEncoding;
            RequestEncode_autoheader.Checked = Settings.Default.RequestEncoding_autoheader;
            debug_msg.Checked = Settings.Default.debug_msg;
            Login_url.Text = Settings.Default.Login_tiancity_url;
            if (RequestEncode.Text == "")
            {
                RequestEncode.SelectedIndex = 0;
            }
            if (ResponseEncode.Text == "")
            {
                ResponseEncode.SelectedIndex = 0;
            }
        }

        private void ResponseOutputLen_TextChanged(object sender, EventArgs e)
        {
            if (ResponseOutputLen.TextLength == 0) { ResponseOutputLen.Text = "100"; }
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

    }
}
