using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Security.Cryptography;

namespace WebRequestTools
{
    class Coding
    {
        public static string MD5Hash(string str, int len, Encoding encode)
        {
            string ps = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(encode.GetBytes(str));
            for (int i = 0; i < s.Length; i++)
            {
                ps += s[i].ToString("x2");
            }
            if (len == 16)
            {
                return ps.Substring(8, 16);
            }
            return ps;
        }

        public static string MD5Hash(string str, int len)
        {
            return MD5Hash(str, len, Encoding.Default);
        }

        public static string Urlencode(string str, Encoding code)
        {
            if (code == null)
            {
                return str;
            }
            else
            {
                return HttpUtility.UrlEncode(str, code);
            }
        }

        public static string Urldecode(string str, Encoding code)
        {
            if (code == null)
            {
                return str;
            }
            else
            {
                return HttpUtility.UrlDecode(str, code);
            }
        }

        public static string Htmlencode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        public static string Htmldecode(string str)
        {
            return HttpUtility.HtmlDecode(str);
        }

        public static string Escape(string str)
        {
            return Microsoft.JScript.GlobalObject.escape(str);
        }

        public static string UnEscape(string str)
        {
            return Microsoft.JScript.GlobalObject.unescape(str);
        }

        public static string EncodeJSString(string sInput)
        {
            StringBuilder builder = new StringBuilder(sInput);
            builder.Replace(@"\", @"\\");
            builder.Replace("\r", @"\r");
            builder.Replace("\n", @"\n");
            builder.Replace("\"", "\\\"");
            sInput = builder.ToString();
            builder = new StringBuilder();
            foreach (char ch in sInput)
            {
                if ('\x007f' < ch)
                {
                    builder.AppendFormat(@"\u{0:X4}", (int)ch);
                }
                else
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }

        public static string DecodeJSString(string s)
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

        public static string ToBase64(string str, Encoding code)
        {
            byte[] temps = code.GetBytes(str);
            return Convert.ToBase64String(temps);
        }

        public static string FromBase64(string str, Encoding code)
        {
            try
            {
                byte[] temps = Convert.FromBase64String(str);
                return code.GetString(temps);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static string Hexencode(string str, Encoding code)
        {
            if (code == null)
            {
                return str;
            }
            byte[] byStr = code.GetBytes(str);
            string strBy = "";
            string strall = "";
            for (int i = 0; i < byStr.Length; i++)
            {
                //strBy += "%" + Convert.ToChar(byStr[i]).ToString();
                strBy = Convert.ToString(byStr[i], 16);
                if (strBy.Length >= 2)
                {
                    strall += strBy;
                }
                else
                {
                    strall += "0" + strBy;
                }
                strall += " ";

                if ((i + 1) % 24 == 0)
                {
                    strall += "\r\n";
                }
            }
            return strall;
        }

        public static string Hexdecode(string str, Encoding code)
        {
            if (code == null)
            {
                return str;
            }
            byte[] byStr = code.GetBytes(str);
            string newstr = "";
            string strtochar = HexCharClear(byStr);
            for (int i = 0; i < strtochar.Length; i = i + 2)
            {
                if (i == strtochar.Length - 1)
                {
                    newstr += "%" + strtochar.Substring(i, 1);
                }
                else
                {
                    newstr += "%" + strtochar.Substring(i, 2);
                }
            }
            return Urldecode(newstr, code);
        }

        public static string HexCharClear(byte[] byStr)
        {
            System.Text.RegularExpressions.Regex regKey = new System.Text.RegularExpressions.Regex("^[A-Fa-f0-9]+$");
            string strbyte = "";
            string strtochar = "";
            for (int i = 0; i < byStr.Length; i++)
            {
                strbyte = Convert.ToChar(byStr[i]).ToString();
                if (regKey.IsMatch(strbyte))
                {
                    strtochar += strbyte;
                }
            }
            return strtochar;
        }

        public static string randomhex(int count)
        {
            Random rd = new Random();
            int num;
            string str = "";
            for (int i = 0; i < count; i++)
            {
                num = rd.Next(1, count + 1);
                str += Convert.ToString(num, 16);
            }
            return str;
        }

        public static string get_timestamp()
        {
            DateTime DateTime1970 = new DateTime(1970, 1, 1);
            TimeSpan t = DateTime.Now - DateTime1970;
            return ((int)t.TotalSeconds - TimeZoneInfo.Local.BaseUtcOffset.TotalSeconds).ToString();
        }

    }

    public class Encrypte
    {
        public static string s52s = key.login_pwd; //"23qYZaijQlmnkvwxyIN60GHOPopbcdRSWBCDKLM789AghEFTUVrstuefXJz145";
        //public static string s52s = "60GHINOPopqRSWX45JKLM789ABCDEFYZabcdTUVrstuefghijQlmnkvwxyz123";
        static bool s52t = true;
        static int N, N2;
        static int[] s52r = new int[128];
        static void s52f()
        {
            s52s = key.login_pwd;
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
            s52s = key.login_pwd;
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

        public static string strPwdMD5(string p_strPwd)
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

        public static string MD5Hash(string str, int len, Encoding encode)
        {
            string ps = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(encode.GetBytes(str));
            for (int i = 0; i < s.Length; i++)
            {
                ps += s[i].ToString("x2");
            }
            return ps;
        }

        public static string MD5Hash(string str, int len)
        {
            return MD5Hash(str, len, Encoding.Default);
        }
    }

    class AESEncryption
    {                
        //默认密钥向量 
        private static byte[] _key1 = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF, 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        /// <summary>
        /// AES加密算法
        /// </summary>
        /// <param name="plainText">明文字符串</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回加密后的密文字节数组</returns>
        public static byte[] AESEncryptbyte(string plainText, string strKey)
        {
            //分组加密算法
            SymmetricAlgorithm des = Rijndael.Create();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(plainText);//得到需要加密的字节数组
            //设置密钥及密钥向量
            des.Key = Encoding.UTF8.GetBytes(strKey);
            des.IV = _key1;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            byte[] cipherBytes = ms.ToArray();//得到加密后的字节数组
            cs.Close();
            ms.Close();
            return cipherBytes;
        }

        public static string AESEncrypt(string plainText, string strKey)
        {
            return Encoding.UTF8.GetString(AESEncryptbyte(plainText, strKey));
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">密文字符串</param>
        /// <param name="strKey">密钥</param>
        /// <returns>返回解密后的字符串</returns>
        public static byte[] AESDecryptbyte(string cipherText, string strKey)
        {
            SymmetricAlgorithm des = Rijndael.Create();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(cipherText);
            des.Key = Encoding.UTF8.GetBytes(strKey);
            des.IV = _key1;
            byte[] decryptBytes = new byte[inputByteArray.Length];
            MemoryStream ms = new MemoryStream(inputByteArray);
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Read);
            cs.Read(decryptBytes, 0, decryptBytes.Length);
            cs.Close();
            ms.Close();
            return decryptBytes;
        }

        public static string AESDecrypt(string cipherText, string strKey)
        {
            return Encoding.UTF8.GetString(AESDecryptbyte(cipherText, strKey));
        }
    }


    public sealed class AuthAESCrypt
    {
        // Fields
        private static readonly byte[] _IV = new byte[] { 0x6a, 0xfd, 200, 0x33, 0xb9, 0xde, 180, 170, 0x5e, 0x5d, 0x49, 0x33, 0x52, 0xd5, 0xfb, 8 };

        // Methods
        public static string Decrypt(string p_Decrypt, string p_Key)
        {
            return Decrypt(PaddingMode.PKCS7, p_Decrypt, p_Key);
        }

        public static string Decrypt(CipherMode p_CMode, string p_Decrypt, string p_Key)
        {
            return Decrypt(PaddingMode.PKCS7, p_CMode, p_Decrypt, p_Key);
        }

        public static string Decrypt(PaddingMode p_PMode, string p_Decrypt, string p_Key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(p_Key);
            byte[] inputBuffer = Convert.FromBase64String(p_Decrypt);
            RijndaelManaged managed = new RijndaelManaged
            {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = p_PMode
            };
            byte[] buffer3 = managed.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Encoding.UTF8.GetString(buffer3);
        }

        public static string Decrypt(PaddingMode p_PMode, CipherMode p_CMode, string p_Decrypt, string p_Key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(p_Key);
            byte[] inputBuffer = Convert.FromBase64String(p_Decrypt);
            RijndaelManaged managed = new RijndaelManaged
            {
                Key = bytes,
                Mode = p_CMode,
                Padding = p_PMode,
                IV = _IV
            };
            byte[] buffer3 = managed.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Encoding.UTF8.GetString(buffer3);
        }

        public static string Encrypt(string p_Encrypt, string p_Key)
        {
            return Encrypt(PaddingMode.PKCS7, p_Encrypt, p_Key);
        }

        public static string Encrypt(CipherMode p_CMode, string p_Encrypt, string p_Key)
        {
            return Encrypt(PaddingMode.PKCS7, p_CMode, p_Encrypt, p_Key);
        }

        public static string Encrypt(PaddingMode p_PMode, string p_Encrypt, string p_Key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(p_Key);
            byte[] inputBuffer = Encoding.UTF8.GetBytes(p_Encrypt);
            RijndaelManaged managed = new RijndaelManaged
            {
                Key = bytes,
                Mode = CipherMode.ECB,
                Padding = p_PMode
            };
            byte[] inArray = managed.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }

        public static string Encrypt(PaddingMode p_PMode, CipherMode p_CMode, string p_Encrypt, string p_Key)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(p_Key);
            byte[] inputBuffer = Encoding.UTF8.GetBytes(p_Encrypt);
            RijndaelManaged managed = new RijndaelManaged
            {
                Key = bytes,
                Mode = p_CMode,
                Padding = p_PMode,
                IV = _IV
            };
            byte[] inArray = managed.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }
    }
}
