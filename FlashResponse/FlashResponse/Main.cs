using System;
using System.Windows.Forms;
using Fiddler;
using System.Web.Security;
using System.Text.RegularExpressions;

[assembly: Fiddler.RequiredVersion("2.3.5.0")]

namespace FlashResponse
{
    public class Main : IAutoTamper
    {
        private UserTabpage oPage;
        private AutoTamperRepBeforeHandler atrbh;

        public Main()
        {
            /* NOTE: It's possible that Fiddler UI isn't fully loaded yet, so don't add any UI in the constructor.

               But it's also possible that AutoTamper* methods are called before OnLoad (below), so be
               sure any needed data structures are initialized to safe values here in this constructor */

            
        }

        public void OnLoad() 
        { 
            /* Load your UI here */
            oPage = new UserTabpage();
            oPage.Name = "FlashResponse";
           
            FiddlerApplication.UI.tabsViews.TabPages.Add(oPage);
        }
        public void OnBeforeUnload() { }

        public void AutoTamperRequestBefore(Session oSession){}
        public void AutoTamperRequestAfter(Session oSession) { }

        public void AutoTamperResponseBefore(Session oSession)
        {

            atrbh = new AutoTamperRepBeforeHandler(oSession, oPage);

            atrbh.updateResponseBody();
        }
        public void AutoTamperResponseAfter(Session oSession) { }
        public void OnBeforeReturningError(Session oSession) { }
    }
}
