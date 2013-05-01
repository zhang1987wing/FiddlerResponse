namespace WindowsFormsApplication1
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.response_ta = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.minus_Button = new System.Windows.Forms.Button();
            this.add_Button = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.url_tb = new System.Windows.Forms.TextBox();
            this.requestType_cb = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.preview_response = new System.Windows.Forms.TextBox();
            this.sign_groupbox = new System.Windows.Forms.GroupBox();
            this.signValue_text = new System.Windows.Forms.TextBox();
            this.json_groupbox = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.save_btn = new System.Windows.Forms.Button();
            this.load_btn = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.json_groupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(429, 548);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.sign_groupbox);
            this.tabPage1.Controls.Add(this.signValue_text);
            this.tabPage1.Controls.Add(this.json_groupbox);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(421, 522);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.response_ta);
            this.groupBox4.Location = new System.Drawing.Point(7, 570);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(383, 100);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "返回值格式";
            // 
            // response_ta
            // 
            this.response_ta.Location = new System.Drawing.Point(7, 20);
            this.response_ta.Multiline = true;
            this.response_ta.Name = "response_ta";
            this.response_ta.Size = new System.Drawing.Size(370, 74);
            this.response_ta.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.minus_Button);
            this.groupBox3.Controls.Add(this.add_Button);
            this.groupBox3.Location = new System.Drawing.Point(7, 370);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(500, 82);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
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
            this.groupBox2.Location = new System.Drawing.Point(8, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(382, 120);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "请求地址和方式";
            // 
            // url_tb
            // 
            this.url_tb.Location = new System.Drawing.Point(6, 34);
            this.url_tb.Name = "url_tb";
            this.url_tb.Size = new System.Drawing.Size(100, 21);
            this.url_tb.TabIndex = 0;
            // 
            // requestType_cb
            // 
            this.requestType_cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.requestType_cb.FormattingEnabled = true;
            this.requestType_cb.Items.AddRange(new object[] {
            "GET",
            "POST"});
            this.requestType_cb.Location = new System.Drawing.Point(150, 34);
            this.requestType_cb.Name = "requestType_cb";
            this.requestType_cb.Size = new System.Drawing.Size(86, 20);
            this.requestType_cb.TabIndex = 1;
            this.requestType_cb.SelectedValueChanged += new System.EventHandler(this.requestType_cb_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.load_btn);
            this.groupBox1.Controls.Add(this.save_btn);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Location = new System.Drawing.Point(7, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(383, 67);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
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
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(80, 30);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(84, 16);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "字符串转换";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // preview_response
            // 
            this.preview_response.Location = new System.Drawing.Point(6, 30);
            this.preview_response.Multiline = true;
            this.preview_response.Name = "preview_response";
            this.preview_response.Size = new System.Drawing.Size(310, 50);
            this.preview_response.TabIndex = 4;
            this.preview_response.TextChanged += new System.EventHandler(this.response_tb_TextChanged);
            // 
            // sign_groupbox
            // 
            this.sign_groupbox.ForeColor = System.Drawing.Color.Blue;
            this.sign_groupbox.Location = new System.Drawing.Point(7, 470);
            this.sign_groupbox.Name = "sign_groupbox";
            this.sign_groupbox.Size = new System.Drawing.Size(382, 90);
            this.sign_groupbox.TabIndex = 5;
            this.sign_groupbox.TabStop = false;
            this.sign_groupbox.Text = "sign";
            // 
            // signValue_text
            // 
            this.signValue_text.Location = new System.Drawing.Point(6, 30);
            this.signValue_text.Multiline = true;
            this.signValue_text.Name = "signValue_text";
            this.signValue_text.Size = new System.Drawing.Size(200, 40);
            this.signValue_text.TabIndex = 0;
            // 
            // json_groupbox
            // 
            this.json_groupbox.Controls.Add(this.preview_response);
            this.json_groupbox.ForeColor = System.Drawing.Color.Blue;
            this.json_groupbox.Location = new System.Drawing.Point(8, 250);
            this.json_groupbox.Name = "json_groupbox";
            this.json_groupbox.Size = new System.Drawing.Size(382, 100);
            this.json_groupbox.TabIndex = 6;
            this.json_groupbox.TabStop = false;
            this.json_groupbox.Text = "json返回值";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(421, 522);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(181, 23);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(75, 23);
            this.save_btn.TabIndex = 1;
            this.save_btn.Text = "保存";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // load_btn
            // 
            this.load_btn.Location = new System.Drawing.Point(282, 22);
            this.load_btn.Name = "load_btn";
            this.load_btn.Size = new System.Drawing.Size(75, 23);
            this.load_btn.TabIndex = 2;
            this.load_btn.Text = "读取";
            this.load_btn.UseVisualStyleBackColor = true;
            this.load_btn.Click += new System.EventHandler(this.load_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 552);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.json_groupbox.ResumeLayout(false);
            this.json_groupbox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
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
    }
}