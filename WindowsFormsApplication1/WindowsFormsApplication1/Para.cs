using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class Para
    {
        private TextBox paraName_tb;
        private TextBox paraValue_tb;
        private ComboBox paraType_cb;

        public Para(TextBox paraName_tb, TextBox paraValue_tb, ComboBox paraType_cb)
        {
            this.paraName_tb = paraName_tb;
            this.paraValue_tb = paraValue_tb;
            this.paraType_cb = paraType_cb;
        }

        public string getParaName()
        {
            return paraName_tb.Text;
        }

        public string getParaValue()
        {
            return paraValue_tb.Text;
        }

        public string getParaType()
        {
            return paraType_cb.Text;
        }

        public TextBox getParaNameTextBox()
        {
            return paraName_tb;
        }

        public void getParaNameTextBox(TextBox paraName_tb)
        {
            this.paraName_tb = paraName_tb;
        }

        public TextBox getParaValueTextBox()
        {
            return paraValue_tb;
        }

        public void setParaValue(TextBox paraValue_tb)
        {
            this.paraValue_tb = paraValue_tb;
        }

        public ComboBox getParaTypeComboBox()
        {
            return paraType_cb;
        }

        public void setParaType(ComboBox paraType_cb)
        {
            this.paraType_cb = paraType_cb;
        }
    }
}
