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
            this.Size = new System.Drawing.Size(421, 522);
            this.TabIndex = 0;
            this.Text = "FlashResponse";
            this.UseVisualStyleBackColor = true;
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
            this.response_ta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.response_ta_Keyup);
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
            // sign_groupbox
            // 
            this.sign_groupbox.Controls.Add(this.signValue_text);
            this.sign_groupbox.Location = new System.Drawing.Point(7, 470);
            this.sign_groupbox.Name = "sign";
            this.sign_groupbox.Size = new System.Drawing.Size(382, 90);
            this.sign_groupbox.Text = "sign";
            this.sign_groupbox.ForeColor = System.Drawing.Color.Blue;
			// 
            // signValue_text
            // 
            this.signValue_text.Location = new System.Drawing.Point(60, 350);
            this.signValue_text.Name = "signValue_text";
            this.signValue_text.Multiline = true;
            this.signValue_text.Size = new System.Drawing.Size(200, 40);   
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
            this.groupBox2.Text = "请求地址";
            // 
            // json_groupbox
            // 
            this.json_groupbox.Location = new System.Drawing.Point(8, 250);
            this.json_groupbox.Controls.Add(this.preview_response);
            this.json_groupbox.Name = "json_label";
            this.json_groupbox.Size = new System.Drawing.Size(382, 100);
            this.json_groupbox.Text = "json返回值";
            this.json_groupbox.ForeColor = System.Drawing.Color.Black;
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
            // groupBox1
            // 
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
            this.checkBox2.Size = new System.Drawing.Size(48, 16);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "字符串转换";
            this.checkBox2.UseVisualStyleBackColor = true;
            
            //preiview_response
            
            this.preview_response.Location = new System.Drawing.Point(6, 30);
            this.preview_response.Multiline = true;
            this.preview_response.Name = "preview_response";
            this.preview_response.Size = new System.Drawing.Size(310, 50);
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
    }
}
