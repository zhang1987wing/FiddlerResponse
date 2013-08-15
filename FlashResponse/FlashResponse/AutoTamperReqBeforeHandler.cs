/*
 * Created by SharpDevelop.
 * User: yi.zhang
 * Date: 2013-7-26
 * Time: 17:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
	/// <summary>
	/// Description of AutoTamperReqBeforeHandler.
	/// </summary>
	public class AutoTamperReqBeforeHandler
	{
		private Session oSession;
		private UserTabpage oPage;
		private static string rbody;
		
		public AutoTamperReqBeforeHandler(Session oSession, UserTabpage oPage)
		{
			this.oSession = oSession;
            this.oPage = oPage;
		}
		
		public void updateRequest(UserTabpage oPage)
        {
            if (this.oSession.HTTPMethodIs("POST") && oPage.getRequestType_cb().Text == "POST" && this.oSession.uriContains(oPage.getUrlTextBox().Text) && this.oSession.GetRequestBodyAsString().Contains(oPage.getRequestbody_tb().Text))
            {
            	oSession.utilSetRequestBody(this.setRequestBody(oSession.GetRequestBodyAsString(), oPage));
            	
            	string ori_url = oSession.url;
            	string new_url = this.setRequestUrl(ori_url, oPage);
            	oSession.url = new_url;
            	
            }
            else if (this.oSession.HTTPMethodIs("GET") && oPage.getRequestType_cb().Text == "GET" && this.oSession.uriContains(oPage.getUrlTextBox().Text))
            {
            	string ori_url = oSession.url;
            	string new_url = this.setRequestUrl(ori_url, oPage);
            	oSession.url = new_url;
            }
        }
		
		public string setRequestUrl(String ori_url, UserTabpage oPage)
		{
			string new_url;
			string[] requestPar = null;
			List<Para> paraValue_list = oPage.getPara_list();
			
			requestPar = ori_url.Split(new char[2] { '?', '&' });
			
			foreach (Para para in paraValue_list)
            {
                if (para.getParaTypeComboBox().Text == "修改")
                {
                    foreach (string ii in requestPar)
                    {
                    	if ((ii.ToString().Substring(0, para.getParaName().Length)) == para.getParaName())
                        { 
                    		if(para.getParaName() == "sign")
                    		{
                    			string sign = this.Sign(para.getParaValue(), paraValue_list, oPage);
                    			ori_url = ori_url.Replace(ii.ToString().Substring(ii.ToString().IndexOf("=") + 1), sign);
                    		}
                    		else
                    		{
                    			ori_url = ori_url.Replace(ii.ToString().Substring(ii.ToString().IndexOf("=") + 1), para.getParaValue());	
                    		}
                        }
                    }
                }
            }
			
			new_url = ori_url;
			
			return new_url;
		}
		
		public string setRequestBody(string old_requestBody, UserTabpage oPage)
		{
			string requestBody;
			string[] requestBodyPar = null;
			List<Para> paraValue_list = oPage.getPara_list();
			
			requestBodyPar = old_requestBody.Split(new char[1] { '&' });
			
			foreach (Para para in paraValue_list)
            {
                if (para.getParaTypeComboBox().Text == "修改")
                {
                    foreach (string ii in requestBodyPar)
                    {
                    	if ((ii.ToString().Substring(0, para.getParaName().Length)) == para.getParaName())
                        { 
                        	old_requestBody = old_requestBody.Replace(ii.ToString().Substring(ii.ToString().IndexOf("=") + 1), para.getParaValue());
                        }
                    }
                }
            }
			
			requestBody = old_requestBody;
			AutoTamperReqBeforeHandler.rbody = old_requestBody;
			
			return requestBody;
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
                    	}else if(m.Value == "reqBody")
                    	{
                    		paraValue = paraValue.Replace("{" + m.Value + "}", AutoTamperReqBeforeHandler.rbody);
                    	}else if(m.Value == "fKey")
                    	{
                    		paraValue = paraValue.Replace("{" + m.Value + "}", oPage.getKeyText());
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
	}
}
