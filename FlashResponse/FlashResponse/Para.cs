﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashResponse
{
    public class Para
    {
        private TextBox paraName_tb;
        private TextBox paraValue_tb;
        private ComboBox paraType_cb;
        private ComboBox paraDataType_cb;

        public Para(TextBox paraName_tb, TextBox paraValue_tb, ComboBox paraType_cb, ComboBox paraDataType_cb)
        {
            this.paraName_tb = paraName_tb;
            this.paraValue_tb = paraValue_tb;
            this.paraType_cb = paraType_cb;
            this.paraDataType_cb = paraDataType_cb;
            this.paraType_cb.SelectedValueChanged += new EventHandler(this.paraType_tb_SelectedIndexChanged);
        }

        public string getParaName()
        {
            return paraName_tb.Text;
        }

        public string getParaValue()
        {
            return paraValue_tb.Text;
        }

        public void setParaValue(string paraValue)
        {
            paraValue_tb.Text = paraValue;
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
        
        public ComboBox getParaDataTypeComboBox()
        {
            return this.paraDataType_cb;
        }

        public void setParaDataType(ComboBox paraDataType_cb)
        {
            this.paraDataType_cb = paraDataType_cb;
        }
        
        private void paraType_tb_SelectedIndexChanged(object sender, EventArgs e)
        {
        	if (this.paraType_cb.Text == "读取")
        	{
        		this.paraValue_tb.ReadOnly = true;
        	}else
        	{
        		this.paraValue_tb.ReadOnly = false;
        	}
        }
    }
}
