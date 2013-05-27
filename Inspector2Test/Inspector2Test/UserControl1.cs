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

        public void TextDisplay(byte[] str)
        {
            if (null != str && str.Length > 0)
            	this.richTextBox.Text = this.DecodeJSString(this.UrlDecode(System.Text.Encoding.UTF8.GetString(str)));
            else
                this.richTextBox.Text = "";
        }
        
        public string DecodeJSString(string s)
        {
            if (string.IsNullOrEmpty(s) || !s.Contains(@"\"))
            {
                return s;
            }
            StringBuilder builder = new StringBuilder();
            int length = s.Length;
            for (int i = 0; i < length; i++)
            {
                char ch = s[i];
                if (ch == '\\')
                {
                    if ((i < (length - 5)) && (s[i + 1] == 'u'))
                    {
                        int num3 = HexToInt(s[i + 2]);
                        int num4 = HexToInt(s[i + 3]);
                        int num5 = HexToInt(s[i + 4]);
                        int num6 = HexToInt(s[i + 5]);
                        if (((num3 < 0) || (num4 < 0)) || ((num5 < 0) || (num6 < 0)))
                        {
                            goto Label_0188;
                        }
                        ch = (char)((((num3 << 12) | (num4 << 8)) | (num5 << 4)) | num6);
                        i += 5;
                        builder.Append(ch);
                        continue;
                    }
                    if ((i < (length - 3)) && (s[i + 1] == 'x'))
                    {
                        int num7 = HexToInt(s[i + 2]);
                        int num8 = HexToInt(s[i + 3]);
                        if ((num7 < 0) || (num8 < 0))
                        {
                            goto Label_0188;
                        }
                        ch = (char)((num7 << 4) | num8);
                        i += 3;
                        builder.Append(ch);
                        continue;
                    }
                    if (i < (length - 1))
                    {
                        switch (s[i + 1])
                        {
                            case '\\':
                                {
                                    builder.Append(@"\");
                                    i++;
                                    continue;
                                }
                            case 'n':
                                {
                                    builder.Append("\n");
                                    i++;
                                    continue;
                                }
                            case 't':
                                {
                                    builder.Append("\t");
                                    i++;
                                    continue;
                                }
                        }
                    }
                }
            Label_0188:
                builder.Append(ch);
            }
            return builder.ToString();
        }
        
        public string UrlDecode(string str)
        {
        	return HttpUtility.UrlDecode(str);
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
        
        private static int HexToInt(char h)
        {
            if ((h >= '0') && (h <= '9'))
            {
                return (h - '0');
            }
            if ((h >= 'a') && (h <= 'f'))
            {
                return ((h - 'a') + 10);
            }
            if ((h >= 'A') && (h <= 'F'))
            {
                return ((h - 'A') + 10);
            }
            return -1;
        }
    }
}
