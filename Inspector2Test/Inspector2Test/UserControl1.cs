using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Web;
using System.Net;

namespace Inspector2Test
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public void TextDisplay(string str)
        {
            if (null != str && "" != str)
                this.richTextBox.Text = HttpUtility.UrlDecode(str);
            else
                this.richTextBox.Text = "";
        }

        public RichTextBox getRichTextBox()
        {
            return this.richTextBox;
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
                    sb.Append(strBy);
                }
                else
                {
                    sb.Append(@"%" + Convert.ToString(byStr[i], 16).ToUpper());
                }
            }
            return sb.ToString().Replace("%D%A", "%0A");
        }

    }
}
