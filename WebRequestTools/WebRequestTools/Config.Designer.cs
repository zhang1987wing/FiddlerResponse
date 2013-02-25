namespace WebRequestTools
{
    partial class Config
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.http_proxyip = new System.Windows.Forms.ComboBox();
            this.save = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ResponseEncode = new System.Windows.Forms.ComboBox();
            this.RequestEncode = new System.Windows.Forms.ComboBox();
            this.Cancel = new System.Windows.Forms.Button();
            this.RequestEncode_autoheader = new System.Windows.Forms.CheckBox();
            this.Login_url = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ResponseOutputLen = new System.Windows.Forms.TextBox();
            this.debug_msg = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "代理IP：";
            this.label1.Visible = false;
            // 
            // http_proxyip
            // 
            this.http_proxyip.FormattingEnabled = true;
            this.http_proxyip.Items.AddRange(new object[] {
            "127.0.0.1:8888",
            "127.0.0.1:9999"});
            this.http_proxyip.Location = new System.Drawing.Point(95, 32);
            this.http_proxyip.Name = "http_proxyip";
            this.http_proxyip.Size = new System.Drawing.Size(134, 20);
            this.http_proxyip.TabIndex = 8;
            this.http_proxyip.Visible = false;
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(280, 70);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(75, 23);
            this.save.TabIndex = 9;
            this.save.Text = "保存设置";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "返回显示编码";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 115);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "请求参数编码";
            // 
            // ResponseEncode
            // 
            this.ResponseEncode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ResponseEncode.FormattingEnabled = true;
            this.ResponseEncode.Items.AddRange(new object[] {
            "GB2312",
            "UTF-8",
            "ASCII",
            "UNICODE",
            "UTF-7",
            "UTF-16",
            "UTF-32"});
            this.ResponseEncode.Location = new System.Drawing.Point(145, 152);
            this.ResponseEncode.Name = "ResponseEncode";
            this.ResponseEncode.Size = new System.Drawing.Size(84, 20);
            this.ResponseEncode.TabIndex = 18;
            // 
            // RequestEncode
            // 
            this.RequestEncode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RequestEncode.FormattingEnabled = true;
            this.RequestEncode.Items.AddRange(new object[] {
            "不编码",
            "GB2312",
            "UTF-8",
            "ASCII",
            "UNICODE",
            "UTF-7",
            "UTF-16",
            "UTF-32"});
            this.RequestEncode.Location = new System.Drawing.Point(145, 112);
            this.RequestEncode.Name = "RequestEncode";
            this.RequestEncode.Size = new System.Drawing.Size(84, 20);
            this.RequestEncode.TabIndex = 17;
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(280, 114);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 21;
            this.Cancel.Text = "取消保存";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // RequestEncode_autoheader
            // 
            this.RequestEncode_autoheader.AutoSize = true;
            this.RequestEncode_autoheader.Location = new System.Drawing.Point(35, 195);
            this.RequestEncode_autoheader.Name = "RequestEncode_autoheader";
            this.RequestEncode_autoheader.Size = new System.Drawing.Size(156, 16);
            this.RequestEncode_autoheader.TabIndex = 23;
            this.RequestEncode_autoheader.Text = "自动添加请求编码头信息";
            this.RequestEncode_autoheader.UseVisualStyleBackColor = true;
            // 
            // Login_url
            // 
            this.Login_url.DetectUrls = false;
            this.Login_url.Location = new System.Drawing.Point(35, 300);
            this.Login_url.Name = "Login_url";
            this.Login_url.Size = new System.Drawing.Size(320, 50);
            this.Login_url.TabIndex = 24;
            this.Login_url.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 275);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(299, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "登录请求地址（0、1、2必须对应账号、密码、验证码）";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 26;
            this.label4.Text = "输出信息显示长度";
            // 
            // ResponseOutputLen
            // 
            this.ResponseOutputLen.Location = new System.Drawing.Point(145, 70);
            this.ResponseOutputLen.MaxLength = 5;
            this.ResponseOutputLen.Name = "ResponseOutputLen";
            this.ResponseOutputLen.Size = new System.Drawing.Size(84, 21);
            this.ResponseOutputLen.TabIndex = 27;
            this.ResponseOutputLen.Text = "200";
            this.ResponseOutputLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ResponseOutputLen.TextChanged += new System.EventHandler(this.ResponseOutputLen_TextChanged);
            this.ResponseOutputLen.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numbers_check_input_KeyPress);
            // 
            // debug_msg
            // 
            this.debug_msg.AutoSize = true;
            this.debug_msg.Location = new System.Drawing.Point(35, 235);
            this.debug_msg.Name = "debug_msg";
            this.debug_msg.Size = new System.Drawing.Size(96, 16);
            this.debug_msg.TabIndex = 29;
            this.debug_msg.Text = "调试信息输出";
            this.debug_msg.UseVisualStyleBackColor = true;
            // 
            // Config
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 462);
            this.Controls.Add(this.debug_msg);
            this.Controls.Add(this.ResponseOutputLen);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Login_url);
            this.Controls.Add(this.RequestEncode_autoheader);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ResponseEncode);
            this.Controls.Add(this.RequestEncode);
            this.Controls.Add(this.save);
            this.Controls.Add(this.http_proxyip);
            this.Controls.Add(this.label1);
            this.Name = "Config";
            this.Text = "Config";
            this.Load += new System.EventHandler(this.Config_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Config_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox http_proxyip;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ResponseEncode;
        private System.Windows.Forms.ComboBox RequestEncode;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.CheckBox RequestEncode_autoheader;
        private System.Windows.Forms.RichTextBox Login_url;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ResponseOutputLen;
        private System.Windows.Forms.CheckBox debug_msg;
    }
}