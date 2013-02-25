using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebRequestTools
{
    public partial class BrowserForm : Form
    {
        public BrowserForm()
        {
            InitializeComponent();
        }

        private void browserForm_Load(object sender, EventArgs e)
        {
            //webBrowser1.DocumentStream = browser.stream;
            webBrowser1.DocumentText = browser.text;
        }
    }
}
