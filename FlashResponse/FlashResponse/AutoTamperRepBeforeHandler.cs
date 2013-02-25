using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fiddler;
using System.Text.RegularExpressions;
using System.Web.Security;
using System.Windows.Forms;

namespace FlashResponse
{
    class AutoTamperRepBeforeHandler
    {
        private Session oSession;
        private UserTabpage oPage;

        public AutoTamperRepBeforeHandler(Session oSession, UserTabpage oPage)
        {
            this.oSession = oSession;
            this.oPage = oPage;
        }

        public void updateResponseBody()
        {
            if (this.oSession.uriContains(this.oPage.getUrlTextBoxValue()))
            {
                oSession.utilSetResponseBody(setResponseBody(oPage.getResponseTextBoxValue(), oPage.getPara_list(), oSession));
            } 
        }

        public string setResponseBody(string response_ta_value, List<Para> paraValue_list, Session oSession)
        {
            string url = oSession.url;
            string[] requestPar = url.Split(new char[2] { '?', '&' });

            foreach (Para para in paraValue_list)
            {
                if (para.getParaTypeComboBox().Text == "请求值")
                {
                    foreach (string ii in requestPar)
                    {
                        if (ii.Contains(para.getParaName()))
                        {
                            para.setParaValue(ii.ToString().Substring(ii.ToString().IndexOf("=") + 1));
                        }
                    }
                }
            }

            Regex reg = new Regex(@"(?<={)[^{}]+(?=})");
            MatchCollection mc = reg.Matches(response_ta_value);

            foreach (Match m in mc)
            {
                foreach(Para para in paraValue_list)
                {
                    if(m.Value == para.getParaName())
                    {
                        if (para.getParaTypeComboBox().Text == "MD5")
                        {
                            response_ta_value = response_ta_value.Replace("{" + m.Value + "}", this.Sign(para.getParaValue(), paraValue_list));
                        }
                        else
                        {
                            response_ta_value = response_ta_value.Replace("{" + m.Value + "}", para.getParaValue());
                        }
                    }
                }
            }

            return response_ta_value;
        }

        public string Sign(string paraValue, List<Para> paraValue_list)
        {
            string sign = "";

            Regex reg = new Regex(@"(?<={)[^{}]+(?=})");
            MatchCollection mc = reg.Matches(paraValue);

            foreach (Match m in mc)
            {
                foreach (Para para in paraValue_list)
                {
                    if (m.Value == para.getParaName())
                    {
                        paraValue = paraValue.Replace("{" + m.Value + "}", para.getParaValue());
                    }
                }
            }

            sign = FormsAuthentication.HashPasswordForStoringInConfigFile(paraValue, "MD5").ToLower();

            return sign;
        }
    }
}
