using System;
using System.Windows.Forms;
using Fiddler;
using System.Web.Security;

[assembly: Fiddler.RequiredVersion("2.3.5.0")]

namespace FlashResponse
{
    public class Main : IAutoTamper
    {
        TabPage oPage;
        TextBox prize;
        string guid;

        public Main()
        {
            /* NOTE: It's possible that Fiddler UI isn't fully loaded yet, so don't add any UI in the constructor.

               But it's also possible that AutoTamper* methods are called before OnLoad (below), so be
               sure any needed data structures are initialized to safe values here in this constructor */

            
        }

        public void OnLoad() 
        { 
            /* Load your UI here */
            oPage = new TabPage("FlashResponse");
            prize = new TextBox();

            oPage.Controls.Add(prize);
            FiddlerApplication.UI.tabsViews.TabPages.Add(oPage); 
        }
        public void OnBeforeUnload() { }

        public void AutoTamperRequestBefore(Session oSession){}
        public void AutoTamperRequestAfter(Session oSession) { }

        public void AutoTamperResponseBefore(Session oSession)
        {
            if (oSession.uriContains("luoqi/fun/option.php"))
            {
                string xx = oSession.url;
                string[] requestPar = xx.Split(new char[2] {'?','&'});

                foreach (string i in requestPar)
                {
                    if(i.ToString().Contains("guid="))
                    {
                        guid = i.ToString().Substring(i.ToString().IndexOf("=") + 1);
                    }
                }
                oSession.utilSetResponseBody("irv=200|sign=" + this.Sign(oSession, prize.Text, guid) + "|couponw=asdafas" + "|prize=" + prize.Text);
            } 
        }
        public void AutoTamperResponseAfter(Session oSession) { }
        public void OnBeforeReturningError(Session oSession) { }

        public string Sign(Session oSession, string prize, string guid)
        {
            string original = "";
            string sign = "";

            MessageBox.Show(guid);
            original = "200" + guid + "asdafas" + prize + "@#a^9s87dW$%";
            MessageBox.Show(original);
            sign = FormsAuthentication.HashPasswordForStoringInConfigFile(original, "MD5").ToLower();

            return sign;

        }
    }
}
