using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fiddler;

[assembly: Fiddler.RequiredVersion("2.3.0.0")]

namespace Inspector2Test
{
    public class WebViewer : Inspector2, IResponseInspector2
    {
        UserControl1 testControl;
        private byte[] m_entityBody;
        HTTPResponseHeaders m_Headers;
        private bool m_bDirty;

        private bool m_bReadOnly;

        public bool bReadOnly
        {
            get
            {
                return m_bReadOnly;
            }
            set
            {
                m_bReadOnly = value;   // TODO: You probably also want to turn your visible control GRAY (false) or WHITE (true) here depending on the value being passed in.   
            }
        }

        public HTTPResponseHeaders headers
        {
            get
            {
                return null;    // Return null if your control doesn't allow header editing.
            }
            set
            {
                m_Headers = value;
            }
        }

        public byte[] body
        {
            get
            {
                return m_entityBody;
            }
            set
            {
                m_entityBody = value;
                string content_type = "";
                if (m_Headers != null)
                    content_type = m_Headers["Content-Type"];
                testControl.TextDisplay(value); // TODO: Use correct encoding based on content header?
                m_bDirty = false;   // Note: Be sure to have an OnTextChanged handler for the textbox which sets m_bDirty to true!
            }
        }

        public bool bDirty
        {
            get
            {
                return m_bDirty;
            }
        }

        public override void AddToTab(System.Windows.Forms.TabPage o)
        {
            testControl = new UserControl1();
            o.Text = "TiancityView";
            o.Controls.Add(testControl);
            o.Controls[0].Dock = DockStyle.Fill;
        }

        public override int GetOrder()
        {
            return 0;
        }
        
        

        public void Clear()
        {
            m_entityBody = null;
            testControl.getRichTextBox().Clear();
        }
    }
}
