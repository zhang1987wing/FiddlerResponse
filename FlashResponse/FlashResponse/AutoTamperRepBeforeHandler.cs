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
        
		/*构造函数，初始化*/
        public AutoTamperRepBeforeHandler(Session oSession, UserTabpage oPage)
        {
            this.oSession = oSession;
            this.oPage = oPage;
        }

        /*更新返回值和json预览*/
        public void updateResponseBody(UserTabpage oPage)
        {
            if (this.oSession.HTTPMethodIs("POST") && oPage.getRequestType_cb().Text == "POST" && this.oSession.uriContains(oPage.getUrlTextBox().Text) && this.oSession.GetRequestBodyAsString().Contains(oPage.getRequestbody_tb().Text))
            {
            	oPage.updatepPreview_response1();
                oSession.utilSetResponseBody(setResponseBody(oPage.getResponseTextBoxValue().Text, oPage.getPara_list(), oSession, oPage));
            }
            else if (this.oSession.HTTPMethodIs("GET") && oPage.getRequestType_cb().Text == "GET" && this.oSession.uriContains(oPage.getUrlTextBox().Text))
            {
                oPage.updatepPreview_response1();
                oSession.utilSetResponseBody(setResponseBody(oPage.getResponseTextBoxValue().Text, oPage.getPara_list(), oSession, oPage));
            }
        }

        /*设置返回值*/
        public string setResponseBody(string response_ta_value, List<Para> paraValue_list, Session oSession, UserTabpage oPage)
        {
        	string[] requestPar = null;

            oSession["ui-color"] = "brown";
            oSession["ui-bold"] = "true";
        	
        	if (oPage.getRequestType_cb().Text == "GET")
        	{
        		string url = oSession.url;
            	requestPar = url.Split(new char[2] { '?', '&' });
        	}
        	else if(oPage.getRequestType_cb().Text == "POST")
        	{
        		string url = oSession.GetRequestBodyAsString();
            	requestPar = url.Split(new char[1] { '&' });
        	}


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

            /*将大括号中符合规定的值替换*/
            Regex reg = new Regex(@"(?<={)[^{}]+(?=})");
            MatchCollection mc = reg.Matches(response_ta_value);

            if (paraValue_list.Count == 0)
            {
            	foreach (Match m in mc)
            	{
            		if (m.Value == "json")
            		{
            			response_ta_value = response_ta_value.Replace("{" + m.Value + "}", this.jsonTOurlencode(oPage.getPreviewTextbox().Text));
            		}
            		else if (m.Value == "sign")
            		{
            			response_ta_value = response_ta_value.Replace("{" + m.Value + "}", this.Sign(oPage.getSignValue_text().Text, paraValue_list, oPage));
            		}
            	}
            }
            else
            {
            	foreach (Match m in mc)
            	{
                	foreach(Para para in paraValue_list)
                	{
                    	if(m.Value == para.getParaName())
                    	{
                        	if (para.getParaTypeComboBox().Text == "MD5")
                        	{
                            	response_ta_value = response_ta_value.Replace("{" + m.Value + "}", this.Sign(para.getParaValue(), paraValue_list, oPage));
                        	}
                        	else
                        	{
                            	response_ta_value = response_ta_value.Replace("{" + m.Value + "}", para.getParaValue());
                        	}
                    	}
                    	else if(m.Value == "json")
                    	{
                    		response_ta_value = response_ta_value.Replace("{" + m.Value + "}", this.jsonTOurlencode(oPage.getPreviewTextbox().Text));
                    	}
                    	else if(m.Value == "sign")
                    	{
                    		response_ta_value = response_ta_value.Replace("{" + m.Value + "}", this.Sign(oPage.getSignValue_text().Text, paraValue_list, oPage));
                    	}
               	 	}
            	}
            }
            
            if(oPage.getCheckBox2().Checked)
            {
                oPage.getResponseTextBoxValue().Text += "|" + oPage.getPreviewTextbox().Text; 
            }

            return response_ta_value;
        }

        /*MD5加密函数*/
        public string Sign(string paraValue, List<Para> paraValue_list, UserTabpage oPage)
        {
            string sign = "";

            Regex reg = new Regex(@"(?<={)[^{}]+(?=})");
            MatchCollection mc = reg.Matches(paraValue);
			
            if(paraValue_list.Count == 0)
            {
            	foreach (Match m in mc)
            	{
            		if(m.Value == "json")
            		{
            			paraValue = paraValue.Replace("{" + m.Value + "}", this.jsonTOurlencode(oPage.getPreviewTextbox().Text));
            		}
            	}
            }
            else
            {
            	 foreach (Match m in mc)
            	{
                	foreach (Para para in paraValue_list)
                	{
                    	if (m.Value == para.getParaName())
                    	{
                        	paraValue = paraValue.Replace("{" + m.Value + "}", para.getParaValue());
                    	}else if(m.Value == "json")
                    	{
                    		paraValue = paraValue.Replace("{" + m.Value + "}", this.jsonTOurlencode(oPage.getPreviewTextbox().Text));
                    	}
                	}
            	}
            }
           

            sign = FormsAuthentication.HashPasswordForStoringInConfigFile(paraValue, "MD5").ToLower();

            return sign;
        }
        
        /*JSON转urlencode*/
        private string jsonTOurlencode(String jsonText)
        {
        	return AutoTamperRepBeforeHandler.UrlEncode2(jsonText, Encoding.UTF8);
        }
        
        /*Urlencode转换*/
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
