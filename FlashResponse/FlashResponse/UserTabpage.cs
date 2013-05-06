using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fiddler;
using Newtonsoft.Json;
using System.IO;
using System.Xml;

namespace FlashResponse
{
    public partial class UserTabpage : TabPage
    {
        private int f = 0;
        private List<Para> paraName_list = new List<Para>(4);
        private Para para;
        private TextBox paraName_tb;
        private TextBox paraValue_tb;
        private ComboBox paraType_tb;
        private ComboBox paraDataType_cb;
        private ToolTip toolTip1;
        private TextBox requestbody_tb;
        String jsonText = "";
        StringWriter sw;
        JsonTextWriter writer;

        public UserTabpage()
        {
            InitializeComponent();

            this.groupBox2.Enabled = false;
            this.groupBox3.Enabled = false;
            this.groupBox4.Enabled = false;
            this.sign_groupbox.Enabled = false;
            this.json_groupbox.Enabled = false;
            
            toolTip1 = new ToolTip();
 
			// Set up the delays for the ToolTip.
			toolTip1.AutoPopDelay = 3000;
			toolTip1.InitialDelay = 1000;
			toolTip1.ReshowDelay = 500;
			// Force the ToolTip text to be displayed whether or not the form is active.
			toolTip1.ShowAlways = true;
			    
			toolTip1.SetToolTip(this.checkBox2, "启用该选项后，请求的response最后会添加urlencode编码后的值");
        }

        public TextBox getResponseTextBoxValue()
        {
        	return this.response_ta;
        }

        public string getUrlTextBoxValue()
        {
            return this.url_tb.Text;
        }

        public TextBox getUrlTextBox()
        {
            return this.url_tb;
        }

        public List<Para> getPara_list()
        {
            return paraName_list;
        }

        public bool getCheckBox1()
        {
            return this.checkBox1.Checked;
        }
        
        public CheckBox getCheckBox2()
        {
        	return this.checkBox2;
        }
        
        public ComboBox getRequestType_cb()
        {
        	return this.requestType_cb;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.groupBox2.Enabled = true;
                this.groupBox3.Enabled = true;
                this.groupBox4.Enabled = true;
                this.sign_groupbox.Enabled = true;
                this.json_groupbox.Enabled = true;
            }
            else
            {
                this.groupBox2.Enabled = false;
                this.groupBox3.Enabled = false;
                this.groupBox4.Enabled = false;
                this.sign_groupbox.Enabled = false;
                this.json_groupbox.Enabled = false;
            }
        }

        private void url_tb_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        private void response_ta_Keyup(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
        }

        /*添加按钮事件，并且将各个参数作为para的一个属性，将para添加值list中*/
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

        public void draw_Para_Component(TextBox paraName_tb, TextBox paraValue_tb, ComboBox paraType_tb, ComboBox paraDataType_cb, int f)
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
            this.sign_groupbox.Location = new Point(this.sign_groupbox.Location.X, this.sign_groupbox.Location.Y + 10 + 21);
            
            this.AutoScroll = true;
            this.AutoScrollPosition = new Point(0, 60 + 21);
        }
        
        /*取消按钮事件，删除多余的参数值*/
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
                this.sign_groupbox.Location = new Point(this.sign_groupbox.Location.X, this.sign_groupbox.Location.Y - 10 - 21);
                
                this.AutoScroll = true;
                this.AutoScrollPosition = new Point(0, 60 + 21);

                f -= 1;
            }
            else
            {
                MessageBox.Show("没有参数了");
            }
        }
        
        /*更新json原始值*/
        private void paraType_tb_SelectedIndexChanged(object sender, EventArgs e)
        {
        	this.updatepPreview_response1();
        }
        
        private void requestType_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.createTooltip(this.toolTip1);
            if(this.requestbody_tb == null)
            	this.requestbody_tb = new TextBox();

            if (this.requestType_cb.Text == "GET")
            {
                groupBox2.Controls.Remove(this.requestbody_tb);
            }
            else if (this.requestType_cb.Text == "POST")
            {
                this.requestbody_tb.Location = new System.Drawing.Point(7, 80);
                this.requestbody_tb.Name = "requestbody_tb";
                this.requestbody_tb.Size = new System.Drawing.Size(200, 21);

                groupBox2.Controls.Add(this.requestbody_tb);
            }
        }

        public void add_requestbody_tb(string requestbody_tb_text)
        {
            //this.createTooltip(this.toolTip1);
            if (this.requestbody_tb == null)
				this.requestbody_tb = new TextBox();
            
            this.requestbody_tb.Location = new System.Drawing.Point(7, 80);
            this.requestbody_tb.Name = "requestbody_tb";
            this.requestbody_tb.Size = new System.Drawing.Size(200, 21);
            this.requestbody_tb.Text = requestbody_tb_text;

            this.groupBox2.Controls.Add(this.requestbody_tb);
        }

        public void remove_requestbody_tb()
        {
            this.groupBox2.Controls.Remove(this.requestbody_tb);
        }
        
        /*更新json原始值方法，使用newtonsoft.json第三方控件编写json格式*/
        public void updatepPreview_response()
        {
        	if (paraName_list.Count != 0)
        	{
        		this.preview_response.Text = "";
        		sw = new StringWriter();
        		writer = new Newtonsoft.Json.JsonTextWriter(sw);	
        		writer.WriteStartObject();
        	       		
        		foreach(Para para in paraName_list)
        		{
        			if(para.getParaType() != "MD5")
        			{
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
        	}
        	else
        	{
        		this.preview_response.Text = this.preview_response.Text;
        	}
        	
        }

        public void updatepPreview_response1()
        {
            if (paraName_list.Count != 0 && this.preview_response.Text != "")
            {
                string[] requestPar = this.preview_response.Text.Split(new char[5] { ',', '{', '}', '[', ']' });

                foreach (Para para in paraName_list)
                {
                    if (para.getParaTypeComboBox().Text == "请求值")
                    {
                        for (int i = 0; i < requestPar.Length; i++)
                        {
                            string ii = requestPar[i];
                            if (ii.Contains(para.getParaName()))
                            {
                                ii = ii.Replace(ii.Split(new char[1] { '"' })[3], para.getParaValue());
                                this.preview_response.Text = this.preview_response.Text.Replace(requestPar[i], ii);

                                break;
                            }
                        }
                    }
                }
            }
           this.preview_response.Text = this.preview_response.Text;
           
           //return this.preview_response.Text;
        }
        
        public TextBox getPreviewTextbox()
        {
        	return this.preview_response;
        }
        
        public TextBox getSignValue_text()
        {
        	return this.signValue_text;
        }

        public void createTooltip(ToolTip toolTip)
        {
            if (toolTip != null)
            {
                toolTip.Dispose();
            }
            toolTip = new ToolTip();
            toolTip.AutoPopDelay = 3000;
            toolTip.InitialDelay = 1;
            toolTip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;

            toolTip.SetToolTip(this.groupBox2, "你选择的是" + this.requestType_cb.Text + "方式");
        }

        public TextBox getRequestbody_tb()
        {
            return this.requestbody_tb;
        }

        public TextBox getResponse_ta()
        {
            return this.response_ta;
        }

        private void save_btn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog=new SaveFileDialog();
            saveFileDialog.Filter = "xml files (*.xml)|*.xml*";　
            saveFileDialog.FilterIndex=2;　
            saveFileDialog.RestoreDirectory=true;
            string fName = "";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fName = saveFileDialog.FileName;
            }
            else
            {
                return;
            }
            XmlDocument myXmlDoc = new XmlDocument();

            OperateXML operateXML = new OperateXML(fName, myXmlDoc, this);
            operateXML.GenerateXMLFile();
        }

        public void removeAllPara()
        {
            if (paraName_list.Count > 0)
            {
                for (int i = 0; i < paraName_list.Count; i++)
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
                    this.sign_groupbox.Location = new Point(this.sign_groupbox.Location.X, this.sign_groupbox.Location.Y - 10 - 21);

                    this.AutoScroll = true;
                    this.AutoScrollPosition = new Point(0, 60 + 21);

                    f -= 1;
                }
            }
        }

        private void load_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "选择文件";
            fileDialog.Filter = "xml files (*.xml)|*.xml";
            fileDialog.FilterIndex = 1;
            fileDialog.RestoreDirectory = true;
            String fileName = "";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = fileDialog.FileName;

            }
            else
            {
                return;
            } 

            XmlDocument myXmlDoc = new XmlDocument();
            OperateXML operateXML = new OperateXML(fileName, myXmlDoc, this);

            paraName_tb = new TextBox();
            paraValue_tb = new TextBox();
            paraType_tb = new ComboBox();
            paraDataType_cb = new ComboBox();

            this.removeAllPara();

            operateXML.loadXmlFile();
        }
    }
}
