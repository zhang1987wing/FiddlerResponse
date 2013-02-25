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
    public partial class TextForm : Form
    {
        public TextForm()
        {
            InitializeComponent();
        }

        int textevent = 1;

        public void typeset(int type)
        {
            textevent = type;
        }

        private void TextForm_Load(object sender, EventArgs e)
        {

        }

        private void TextForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Webrequset Ownerfrm;
            Ownerfrm = (Webrequset)this.Owner;
            if (richTextBox1.Text.Trim().Length == 0)
            {
                return;
            }
            switch (textevent)
            {
                case 1:
                    Ownerfrm.StripMenu_load_file_fromtext(richTextBox1.Text);
                    break;
                default: break;
            }
        }
    }
}
