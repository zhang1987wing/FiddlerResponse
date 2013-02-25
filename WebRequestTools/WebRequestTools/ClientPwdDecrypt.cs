using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;

namespace Passport
{
    /// <summary>
    /// 对客户端密码进行解密处理
    /// </summary>
    public class ClientPwdDecrypt
    {
        public static string s52s = "23qYZaijQlmnkvwxyIN60GHOPopbcdRSWBCDKLM789AghEFTUVrstuefXJz1451";
        //public static string s52s = "60GHINOPopqRSWX45JKLM789ABCDEFYZabcdTUVrstuefghijQlmnkvwxyz123";
        static bool s52t = true;
        static int N, N2;
        static int[] s52r = new int[128];
        
        static void s52f()
        {
            N = s52s.Length;
            N2 = N * N;
            for (var x = 0; x < s52s.Length; x++)
            {
                s52r[(int)s52s[x]] = x;
            }
            s52t = false;
        }


        public static string s52e(string n)
        {
            if (s52t) s52f();
            int l = n.Length, a, x = 0;
            List<char> t = new List<char>(l * 3);
            for (; x < l; x++)
            {
                a = (int)n[x];
                if (a < N2)
                {
                    t.Add(s52s[a / N]);
                    t.Add(s52s[a % N]);
                }
                else
                {
                    t.Add(s52s[a / N2 + 5]);
                    t.Add(s52s[(a / N) % N]);
                    t.Add(s52s[a % N]);
                }
            }
            string s = new string(t.ToArray());
            return s.Length.ToString().Length + s.Length.ToString() + s;
        }

        public static string s52d(string n)
        {
            if (s52t) s52f();
            int c;
            if (!int.TryParse(n[0].ToString(), out c)) return "";
            if (!int.TryParse(n.Substring(1, c), out c)) return "";
            int x = c.ToString().Length + 1;
            if (n.Length != c + x) return "";
            int nl = n.Length, a;
            List<char> t = new List<char>(nl * 3);
            for (; x < nl; x++)
            {
                a = s52r[(int)n[x]];
                x++;
                if (a < 5)
                {
                    c = a * N + s52r[(int)n[x]];
                }
                else
                {
                    c = (a - 5) * N2 + s52r[(int)n[x]] * N;
                    x++;
                    c += s52r[(int)n[x]];
                }
                t.Add((char)c);
            }
            return new string(t.ToArray());
        }

        public static string MD5(string p_strPwd)
        {
            string strLongMd5 = s52d(p_strPwd);
            if (strLongMd5.Length != 32)
            {
                return "";
            }
            else
            {
                return strLongMd5.Substring(8, 16);
            }
        }

        public string key
        {
            set { s52s = value; }
            get { return s52s; }
        }

    }
}
