using System.Windows.Forms;
using System.Drawing;

namespace FlashResponse
{
    partial class UserTabpage
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
        	this.request_switch = new System.Windows.Forms.CheckBox();
        	this.response_switch = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.response_ta = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.minus_Button = new System.Windows.Forms.Button();
            this.add_Button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.url_tb = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.preview_response = new System.Windows.Forms.TextBox();
            this.sign_groupbox = new System.Windows.Forms.GroupBox();
            this.signValue_text = new System.Windows.Forms.TextBox();
            this.json_groupbox = new System.Windows.Forms.GroupBox();
            this.requestType_cb = new System.Windows.Forms.ComboBox();
            this.save_btn = new System.Windows.Forms.Button();
            this.load_btn = new System.Windows.Forms.Button();
            this.key_textbox = new System.Windows.Forms.TextBox();
            this.response_label1 = new System.Windows.Forms.TextBox();
            this.response_label2 = new System.Windows.Forms.TextBox();
            this.sign_groupbox.SuspendLayout();
            this.json_groupbox.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();

            // 
            // tabPage1
            // 
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.preview_response);
            this.Controls.Add(this.sign_groupbox);
            this.Controls.Add(this.signValue_text);
            this.Controls.Add(this.json_groupbox);
            this.Location = new System.Drawing.Point(4, 22);
            this.Name = "FlashResponse";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(421, 600);
            this.TabIndex = 0;
            this.Text = "FlashResponse";
            this.UseVisualStyleBackColor = true;
            this.AutoScroll = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.response_ta);
            this.groupBox4.Controls.Add(this.response_label1);
            this.groupBox4.Controls.Add(this.response_label2);
            this.groupBox4.Location = new System.Drawing.Point(7, 570);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(450, 180);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "返回值格式";
            //this.groupBox4.Enabled = false;
            // 
            // response_ta
            // 
            this.response_ta.Location = new System.Drawing.Point(7, 20);
            this.response_ta.Multiline = true;
            this.response_ta.Name = "response_ta";
            this.response_ta.Size = new System.Drawing.Size(370, 74);
            this.response_ta.TabIndex = 0;
            this.response_ta.Text = "{json}|{sign}";
            this.response_ta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.response_ta_Keyup);
            // 
            // response_label1
            //
			this.response_label1.BorderStyle = BorderStyle.None;
			this.response_label1.ReadOnly = true;
			this.response_label1.BackColor = Color.WhiteSmoke;			
            this.response_label1.Location = new System.Drawing.Point(7, 120);
            this.response_label1.Name = "response_label1";
            this.response_label1.Size = new System.Drawing.Size(200, 20);
            this.response_label1.Text = "默认格式1：{json}|{sign}";
            // 
            // response_label2
            // 
            this.response_label2.BorderStyle = BorderStyle.None;
			this.response_label2.ReadOnly = true;
			this.response_label2.BackColor = Color.WhiteSmoke;	
            this.response_label2.Location = new System.Drawing.Point(7, 150);
            this.response_label2.Name = "response_label1";
            this.response_label2.Size = new System.Drawing.Size(300, 20);
            this.response_label2.Text = "默认格式2：{\"datas\":\"{json}\",\"sign\":\"{sign}\"}";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.minus_Button);
            this.groupBox3.Controls.Add(this.add_Button);
            //this.groupBox3.Location = new System.Drawing.Point(7, 370);
            this.groupBox3.Location = new System.Drawing.Point(8, 250);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(500, 82);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "请求值修改和读取";
            // 
            // sign_groupbox
            // 
            this.sign_groupbox.Controls.Add(this.signValue_text);
            this.sign_groupbox.Location = new System.Drawing.Point(7, 470);
            this.sign_groupbox.Name = "sign";
            this.sign_groupbox.Size = new System.Drawing.Size(382, 90);
            this.sign_groupbox.Text = "sign";
            this.sign_groupbox.ForeColor = System.Drawing.Color.Blue;
           // this.sign_groupbox.Enabled = false;
			// 
            // signValue_text
            // 
            this.signValue_text.Location = new System.Drawing.Point(7, 30);
            this.signValue_text.Name = "signValue_text";
            this.signValue_text.Multiline = true;
            this.signValue_text.Size = new System.Drawing.Size(350, 40);  
			// 
            // key_text
            // 
            this.key_textbox.Location = new System.Drawing.Point(75, 23);
            this.key_textbox.Name = "key_text";
            this.key_textbox.Multiline = true;
            this.key_textbox.Text = "请设置你的Flash Key";
            this.key_textbox.Size = new System.Drawing.Size(150, 21);            
            // 
            // minus_Button
            // 
            this.minus_Button.Location = new System.Drawing.Point(200, 53);
            this.minus_Button.Name = "minus_Button";
            this.minus_Button.Size = new System.Drawing.Size(75, 23);
            this.minus_Button.TabIndex = 1;
            this.minus_Button.Text = "取消";
            this.minus_Button.UseVisualStyleBackColor = true;
            this.minus_Button.Click += new System.EventHandler(this.minus_Button_Click);
            // 
            // add_Button
            // 
            this.add_Button.Location = new System.Drawing.Point(32, 53);
            this.add_Button.Name = "add_Button";
            this.add_Button.Size = new System.Drawing.Size(75, 23);
            this.add_Button.TabIndex = 0;
            this.add_Button.Text = "添加";
            this.add_Button.UseVisualStyleBackColor = true;
            this.add_Button.Click += new System.EventHandler(this.add_Button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.url_tb);
            this.groupBox2.Controls.Add(this.requestType_cb);
            this.groupBox2.Controls.Add(this.request_switch);
            this.groupBox2.Controls.Add(this.response_switch);
            this.groupBox2.Location = new System.Drawing.Point(8, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(500, 120);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "请求地址";
            // 
            // json_groupbox
            // 
            //this.json_groupbox.Location = new System.Drawing.Point(8, 250);
            this.json_groupbox.Location = new System.Drawing.Point(7, 370);
            this.json_groupbox.Controls.Add(this.preview_response);
            this.json_groupbox.Name = "json_label";
            this.json_groupbox.Size = new System.Drawing.Size(500, 100);
            this.json_groupbox.Text = "json返回值";
            this.json_groupbox.ForeColor = System.Drawing.Color.Black;
            //this.json_groupbox.Enabled = false;
            // 
            // url_tb
            // 
            this.url_tb.Location = new System.Drawing.Point(6, 34);
            this.url_tb.Name = "url_tb";
            this.url_tb.Size = new System.Drawing.Size(200, 21);
            this.url_tb.TabIndex = 0;
            this.url_tb.Text = "默认的";
            this.url_tb.KeyUp += new System.Windows.Forms.KeyEventHandler(this.url_tb_KeyUp);
            // 
            // requestType_cb
            // 
            requestType_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            requestType_cb.FormattingEnabled = true;
            requestType_cb.Items.AddRange(new object[] {
            "GET",
            "POST"});
            requestType_cb.SelectedIndex = 0;
            requestType_cb.SelectedValueChanged += new System.EventHandler(this.requestType_cb_SelectedIndexChanged);
            requestType_cb.Location = new System.Drawing.Point(256, 34);
            requestType_cb.Name = "requestType_cb";
            requestType_cb.Size = new System.Drawing.Size(86, 21);
            // 
            // request_switch
            // 
            this.request_switch.AutoSize = true;
            this.request_switch.Location = new System.Drawing.Point(256, 80);
            this.request_switch.Name = "request_switch";
            this.request_switch.Size = new System.Drawing.Size(48, 16);
            this.request_switch.Visible = false;
            this.request_switch.Checked = true;
            this.request_switch.Text = "request";
            this.request_switch.UseVisualStyleBackColor = true;
            this.request_switch.CheckedChanged += new System.EventHandler(this.request_switch_CheckedChanged);
            // 
            // request_switch
            // 
            this.response_switch.AutoSize = true;
            this.response_switch.Location = new System.Drawing.Point(400, 34);
            this.response_switch.Name = "response_switch";
            this.response_switch.Size = new System.Drawing.Size(48, 16);
            this.response_switch.Text = "response";
            this.response_switch.UseVisualStyleBackColor = true;
            this.response_switch.CheckedChanged += new System.EventHandler(this.response_switch_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox1);
            //this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.key_textbox);
            this.groupBox1.Controls.Add(this.load_btn);
            this.groupBox1.Controls.Add(this.save_btn);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(500, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Use FlashResponse";
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(241, 23);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(75, 23);
            this.save_btn.TabIndex = 1;
            this.save_btn.Text = "保存";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // load_btn
            // 
            this.load_btn.Location = new System.Drawing.Point(342, 23);
            this.load_btn.Name = "load_btn";
            this.load_btn.Size = new System.Drawing.Size(75, 23);
            this.load_btn.TabIndex = 2;
            this.load_btn.Text = "读取";
            this.load_btn.UseVisualStyleBackColor = true;
            this.load_btn.Click += new System.EventHandler(this.load_btn_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 30);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "启用";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            //
            // checkBox2
            // 
            /*this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(80, 30);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "字符串转换";
            this.checkBox2.UseVisualStyleBackColor = true;*/
            
            //preiview_response
            
            this.preview_response.Location = new System.Drawing.Point(6, 30);
            this.preview_response.Multiline = true;
            this.preview_response.Name = "preview_response";
            this.preview_response.Size = new System.Drawing.Size(450, 50);
            //this.preview_response.TextChanged += new System.EventHandler(this.response_tb_TextChanged);
            // 
            // Form1
            //
            this.json_groupbox.ResumeLayout(false);
            this.json_groupbox.PerformLayout();
            this.sign_groupbox.ResumeLayout(false);
            this.sign_groupbox.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox response_ta;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button minus_Button;
        private System.Windows.Forms.Button add_Button;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox url_tb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TextBox preview_response;
        private System.Windows.Forms.GroupBox sign_groupbox;
        private System.Windows.Forms.GroupBox json_groupbox;
        private System.Windows.Forms.TextBox signValue_text;
        private System.Windows.Forms.ComboBox requestType_cb;
        private System.Windows.Forms.Button load_btn;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.TextBox key_textbox;
        private System.Windows.Forms.CheckBox request_switch;
        private System.Windows.Forms.CheckBox response_switch;
        private System.Windows.Forms.TextBox response_label1;
        private System.Windows.Forms.TextBox response_label2;
    }
}
