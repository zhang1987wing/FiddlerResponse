using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebRequestTools
{
    public partial class inputkey : Form
    {
        public inputkey()
        {
            InitializeComponent();
        }

        int keytype = 1;

        public void paramin(TabControl ParamControl)
        {
            //int aa = ParamControl.Controls.Count;
            //ParamControl.Controls[0].Controls.Find("name_",true)
        }

        public void setkeytype(int type)
        {
            keytype = type;
        }

        private string getkey()
        {
            string returnkey = "";
            switch (keytype)
            {
                case 1: returnkey = key.md5; 
                    break;
                case 2: returnkey = key.AES;
                    break;
                case 3: returnkey = key.AuthAES;
                    break;
                default: returnkey = null;
                    break;
            }
            return returnkey;
        }

        private void savekey(string savestr)
        {
            switch (keytype)
            {
                case 1: key.md5 = savestr;
                    break;
                case 2: key.AES = savestr;
                    break;
                case 3: key.AuthAES = savestr;
                    break;
                default: break;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            savekey(inputTextBox.Text);
            this.Close();
        }

        private void inputkey_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (inputTextBox.Text != getkey() && inputTextBox.Text != "")
            {
                if (MessageBox.Show("KEY已修改，是否需要保存？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    savekey(inputTextBox.Text);
                }
                else
                {
                    //e.Cancel = true;
                }
            }
        }

        private void inputkey_Load(object sender, EventArgs e)
        {
            inputTextBox.Text = getkey();
            if (getkey() == null || getkey().Length == 0)
            {
                label1.Text = "请输入KEY：";
            }
            else if (keytype == 1)
            {
                label1.Text = "已加载上一次的KEY，可修改此次MD5 KEY，不影响上一次的值";
            }
            else
            {
                label1.Text = "已加载上一次的KEY";
            }
        }

        private void inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
             //if(e.KeyCode == Keys.);
        }

        private void inputTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '{' || e.KeyChar == '}')
            {
                e.Handled = true;
            }
        }
    }
}
