using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using System.Net;

namespace WebRequestTools
{
    class HTTP
    {
        //static string ReqHeadName = "FCQ";
        //static string ResHeadName = "FCR";

        //public static void SetReqHeadName(string name)
        //{
        //    ReqHeadName = name;
        //}
        //public static void SetResHeadName(string name)
        //{
        //    ResHeadName = name;
        //}

        //public static string Request(string Url, string Head)
        //{
        //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
        //    request.Headers.Set(ReqHeadName, Head);
        //    HttpWebResponse wResp = (HttpWebResponse)request.GetResponse();
        //    //Stream respStream = wResp.GetResponseStream();
        //    return wResp.GetResponseHeader(ResHeadName);
        //}


        //url  //body  //headlist[x,y]  //WebProxy
        public static string Request(string Url)
        {
            return "";
        }

        public static string Request(string Url, string Postdata)
        {
            return "";
        }

        public static string Request(string Url, ref string aa)
        {
            aa = "2";
            return "";
        }

        public static string test()
        {
            string aa = "1";
            return Request("asd", ref aa);
        }

    }
}
