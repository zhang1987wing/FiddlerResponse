using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private int f = 0;
        private List<Para> paraName_list = new List<Para>(4);
        private Para para;
        private TextBox paraName_tb;
        private TextBox paraValue_tb;
        private ComboBox paraType_tb;
        private ComboBox paraDataType_cb;
        private ToolTip toolTip1;
        String jsonText = "";
        StringWriter sw;
        JsonTextWriter writer;

        public Form1()
        {
            InitializeComponent();
            toolTip1 = new ToolTip();
 
			// Set up the delays for the ToolTip.
			toolTip1.AutoPopDelay = 5000;
			toolTip1.InitialDelay = 1000;
			toolTip1.ReshowDelay = 500;
			// Force the ToolTip text to be displayed whether or not the form is active.
			toolTip1.ShowAlways = true;
			    
			toolTip1.SetToolTip(this.checkBox2, "启用该选项后，请求的response最后会添加urlencode编码后的值");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void add_Button_Click(object sender, EventArgs e)
        {
            paraName_tb = new TextBox();
            paraValue_tb = new TextBox();
            paraType_tb = new ComboBox();
            paraDataType_cb = new ComboBox();

            this.draw_Para_Component(paraName_tb, paraValue_tb, paraType_tb, paraDataType_cb, f);

            para = new Para(paraName_tb, paraValue_tb, paraType_tb, paraDataType_cb);
            paraName_list.Add(para);
            paraName_list.TrimExcess();

            f += 1;
        }

        private void draw_Para_Component(TextBox paraName_tb, TextBox paraValue_tb, ComboBox paraType_tb, ComboBox paraDataType_cb, int f)
        {
            paraName_tb.Location = new System.Drawing.Point(7, 10 * (f + 1) + 21 * (f + 1));
            paraName_tb.Name = "paraName_tb" + f;
            paraName_tb.Size = new System.Drawing.Size(100, 21);

            paraValue_tb.Location = new System.Drawing.Point(149, 10 * (f + 1) + 21 * (f + 1));
            paraValue_tb.Name = "paraValue_tb" + f;
            paraValue_tb.Size = new System.Drawing.Size(100, 21);

            paraDataType_cb.FormattingEnabled = true;
            paraDataType_cb.Items.AddRange(new object[] {
            "Int",
            "Text"});
            paraDataType_cb.Location = new System.Drawing.Point(291, 10 * (f + 1) + 21 * (f + 1));
            paraDataType_cb.Name = "paraDataType_cb" + f;
            paraDataType_cb.Size = new System.Drawing.Size(86, 21);
            
            paraType_tb.FormattingEnabled = true;
            paraType_tb.Items.AddRange(new object[] {
            "MD5",
            "请求值",
            "响应值"});
            paraType_tb.SelectedValueChanged += new System.EventHandler(this.paraType_tb_SelectedIndexChanged);
            paraType_tb.Location = new System.Drawing.Point(411, 10 * (f + 1) + 21 * (f + 1));
            paraType_tb.Name = "paraType_tb" + f;
            paraType_tb.Size = new System.Drawing.Size(86, 21);           

            this.groupBox3.Size = new System.Drawing.Size(this.groupBox3.Size.Width, this.groupBox3.Size.Height + 10 + 21);
            this.groupBox3.Controls.Add(paraName_tb);
            this.groupBox3.Controls.Add(paraValue_tb);
            this.groupBox3.Controls.Add(paraType_tb);
            this.groupBox3.Controls.Add(paraDataType_cb);

            foreach (Control btn in this.groupBox3.Controls)
            {
                if (btn is Button)
                {
                    btn.Location = new Point(btn.Location.X, btn.Location.Y + 10 + 21);
                }
            }

            this.groupBox4.Location = new Point(this.groupBox4.Location.X, this.groupBox4.Location.Y + 10 + 21);
            this.sign_label.Location = new Point(this.sign_label.Location.X, this.sign_label.Location.Y + 10 + 21);
            this.signValue_text.Location = new Point(this.signValue_text.Location.X, this.signValue_text.Location.Y + 10 + 21);
            this.tabPage1.AutoScroll = true;
            this.tabPage1.AutoScrollPosition = new Point(0, 60 + 21);
        }

        private void minus_Button_Click(object sender, EventArgs e)
        {
            if (paraName_list.Count > 0)
            {
                this.groupBox3.Controls.Remove(paraName_list[paraName_list.Count - 1].getParaNameTextBox());
                this.groupBox3.Controls.Remove(paraName_list[paraName_list.Count - 1].getParaTypeComboBox());
                this.groupBox3.Controls.Remove(paraName_list[paraName_list.Count - 1].getParaValueTextBox());
               
                this.groupBox3.Controls.Remove(paraName_list[paraName_list.Count - 1].getParaDataTypeComboBox());

                paraName_list.RemoveAt(paraName_list.Count - 1);

                this.groupBox3.Size = new System.Drawing.Size(this.groupBox3.Size.Width, this.groupBox3.Size.Height - 10 - 21);

                foreach (Control btn in this.groupBox3.Controls)
                {
                    if (btn is Button)
                    {
                        btn.Location = new Point(btn.Location.X, btn.Location.Y - 10 - 21);
                    }
                }

                this.groupBox4.Location = new Point(this.groupBox4.Location.X, this.groupBox4.Location.Y - 10 - 21);
                this.sign_label.Location = new Point(this.sign_label.Location.X, this.sign_label.Location.Y - 10 - 21);
                this.signValue_text.Location = new Point(this.signValue_text.Location.X, this.signValue_text.Location.Y - 10 - 21);
                

                this.tabPage1.AutoScroll = true;
                this.tabPage1.AutoScrollPosition = new Point(0, 60 - 21);

                f -= 1;
            }
            else
            {
                MessageBox.Show("没有参数了");
            }
        }
        
        private void response_tb_TextChanged(object sender, EventArgs e)
        {
        	this.jsonTOurlencode(this.preview_response.Text);
        }
        
        private void paraType_tb_SelectedIndexChanged(object sender, EventArgs e)
        {
        	this.preview_response.Text = "";
        	sw = new StringWriter();
        	writer = new Newtonsoft.Json.JsonTextWriter(sw);	
        	writer.WriteStartObject();
        	       		
        		foreach(Para para in paraName_list)
        		{
        			if(para.getParaType() != "MD5")
        			{
        				MessageBox.Show(para.getParaTypeComboBox().Text);
        				if(para.getParaDataTypeComboBox().Text == "Int")
        				{
        					writer.WritePropertyName(para.getParaName());
        					writer.WriteValue(Int32.Parse(para.getParaValue()));
        				}
        				else if(para.getParaDataTypeComboBox().Text == "Text")
        				{
        					writer.WritePropertyName(para.getParaName());
        					writer.WriteValue(para.getParaValue());
        				}
        			}
        		}
        	
        	writer.WriteEndObject();
        	writer.Flush();
        	jsonText = sw.GetStringBuilder().ToString();
        	this.preview_response.Text = jsonText;
        	//jsonText = sw.GetStringBuilder().ToString();
        	MessageBox.Show(jsonText);
        	this.preview_response.Text = jsonText;
        }
        
        private void jsonTOurlencode(String jsonText)
        {
        	
        	//response_ta.Text = System.Web.HttpUtility.UrlEncode(jsonText, System.Text.Encoding.UTF8);
        	response_ta.Text = Form1.UrlEncode2(preview_response.Text, Encoding.UTF8);
        }
        
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
        
        private void requestType_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
        	if(this.toolTip1 != null)
        	{
        		this.toolTip1.Dispose();
        	}
        	
        	toolTip1 = new ToolTip();
        	toolTip1.AutoPopDelay = 3000;
			toolTip1.InitialDelay = 1;
			toolTip1.ReshowDelay = 500;
			// Force the ToolTip text to be displayed whether or not the form is active.
			toolTip1.ShowAlways = true;
			    
			toolTip1.SetToolTip(this.groupBox2, "你选择的是" + this.requestType_cb.Text + "方式");
        }
    }
}
