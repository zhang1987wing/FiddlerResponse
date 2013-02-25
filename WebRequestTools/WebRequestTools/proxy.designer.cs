namespace WebRequestTools
{
    partial class proxy
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
            this.proxymodel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.proxytxt = new System.Windows.Forms.TextBox();
            this.proxyadd = new System.Windows.Forms.Button();
            this.proxyimport = new System.Windows.Forms.Button();
            this.proxydelete = new System.Windows.Forms.Button();
            this.proxyclear = new System.Windows.Forms.Button();
            this.proxycheck = new System.Windows.Forms.Button();
            this.proxylistbox = new System.Windows.Forms.ListBox();
            this.proxytimeoutseconds = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.proxylistdata = new System.Windows.Forms.DataGridView();
            this.proxyip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.proxyport = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.proxytimelimit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.Saveastxt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.proxylistdata)).BeginInit();
            this.SuspendLayout();
            // 
            // proxymodel
            // 
            this.proxymodel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.proxymodel.FormattingEnabled = true;
            this.proxymodel.Items.AddRange(new object[] {
            "不使用代理",
            "循环使用代理（列表全部）",
            "随机使用代理（列表全部）",
            "循环使用代理（列表多选）",
            "随机使用代理（列表多选）"});
            this.proxymodel.Location = new System.Drawing.Point(139, 28);
            this.proxymodel.Name = "proxymodel";
            this.proxymodel.Size = new System.Drawing.Size(170, 20);
            this.proxymodel.TabIndex = 0;
            this.proxymodel.SelectedIndexChanged += new System.EventHandler(this.proxymodel_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "代理模式";
            // 
            // proxytxt
            // 
            this.proxytxt.Location = new System.Drawing.Point(27, 148);
            this.proxytxt.Name = "proxytxt";
            this.proxytxt.Size = new System.Drawing.Size(100, 21);
            this.proxytxt.TabIndex = 4;
            this.proxytxt.Visible = false;
            this.proxytxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.proxytxt_KeyDown);
            // 
            // proxyadd
            // 
            this.proxyadd.Location = new System.Drawing.Point(159, 146);
            this.proxyadd.Name = "proxyadd";
            this.proxyadd.Size = new System.Drawing.Size(92, 23);
            this.proxyadd.TabIndex = 5;
            this.proxyadd.Text = "添加代理  →";
            this.proxyadd.UseVisualStyleBackColor = true;
            this.proxyadd.Visible = false;
            this.proxyadd.Click += new System.EventHandler(this.addproxy_Click);
            // 
            // proxyimport
            // 
            this.proxyimport.Location = new System.Drawing.Point(440, 70);
            this.proxyimport.Name = "proxyimport";
            this.proxyimport.Size = new System.Drawing.Size(75, 23);
            this.proxyimport.TabIndex = 6;
            this.proxyimport.Text = "导入代理";
            this.proxyimport.UseVisualStyleBackColor = true;
            this.proxyimport.Click += new System.EventHandler(this.proxyimport_Click);
            // 
            // proxydelete
            // 
            this.proxydelete.Location = new System.Drawing.Point(440, 190);
            this.proxydelete.Name = "proxydelete";
            this.proxydelete.Size = new System.Drawing.Size(75, 23);
            this.proxydelete.TabIndex = 6;
            this.proxydelete.Text = "删除选中";
            this.proxydelete.UseVisualStyleBackColor = true;
            this.proxydelete.Click += new System.EventHandler(this.proxydelete_Click);
            // 
            // proxyclear
            // 
            this.proxyclear.Location = new System.Drawing.Point(440, 250);
            this.proxyclear.Name = "proxyclear";
            this.proxyclear.Size = new System.Drawing.Size(75, 23);
            this.proxyclear.TabIndex = 6;
            this.proxyclear.Text = "清空列表";
            this.proxyclear.UseVisualStyleBackColor = true;
            this.proxyclear.Click += new System.EventHandler(this.proxyclear_Click);
            // 
            // proxycheck
            // 
            this.proxycheck.Location = new System.Drawing.Point(440, 130);
            this.proxycheck.Name = "proxycheck";
            this.proxycheck.Size = new System.Drawing.Size(75, 23);
            this.proxycheck.TabIndex = 7;
            this.proxycheck.Text = "代理检查";
            this.proxycheck.UseVisualStyleBackColor = true;
            this.proxycheck.Click += new System.EventHandler(this.proxycheck_Click);
            // 
            // proxylistbox
            // 
            this.proxylistbox.FormattingEnabled = true;
            this.proxylistbox.ItemHeight = 12;
            this.proxylistbox.Location = new System.Drawing.Point(289, 94);
            this.proxylistbox.Name = "proxylistbox";
            this.proxylistbox.Size = new System.Drawing.Size(128, 208);
            this.proxylistbox.TabIndex = 8;
            this.proxylistbox.Visible = false;
            this.proxylistbox.SelectedIndexChanged += new System.EventHandler(this.proxylist_SelectedIndexChanged);
            this.proxylistbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.proxylist_KeyDown);
            // 
            // proxytimeoutseconds
            // 
            this.proxytimeoutseconds.Location = new System.Drawing.Point(440, 28);
            this.proxytimeoutseconds.MaxLength = 9;
            this.proxytimeoutseconds.Name = "proxytimeoutseconds";
            this.proxytimeoutseconds.Size = new System.Drawing.Size(54, 21);
            this.proxytimeoutseconds.TabIndex = 9;
            this.proxytimeoutseconds.Text = "2000";
            this.proxytimeoutseconds.TextChanged += new System.EventHandler(this.proxytimeoutseconds_TextChanged);
            this.proxytimeoutseconds.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numbers_check_input_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(502, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "毫秒";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(390, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "超时：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "例：192.168.10.10:8080";
            this.label4.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 321);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "label5";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(289, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "代理列表：";
            this.label6.Visible = false;
            // 
            // proxylistdata
            // 
            this.proxylistdata.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.proxylistdata.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.proxyip,
            this.proxyport,
            this.proxytimelimit});
            this.proxylistdata.Location = new System.Drawing.Point(27, 70);
            this.proxylistdata.Name = "proxylistdata";
            this.proxylistdata.RowTemplate.Height = 23;
            this.proxylistdata.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.proxylistdata.Size = new System.Drawing.Size(398, 232);
            this.proxylistdata.TabIndex = 16;
            this.proxylistdata.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.proxylistdata_CellValidating);
            this.proxylistdata.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.proxylistdata_EditingControlShowing);
            // 
            // proxyip
            // 
            this.proxyip.HeaderText = "代理IP";
            this.proxyip.MaxInputLength = 21;
            this.proxyip.Name = "proxyip";
            this.proxyip.ToolTipText = "代理IP地址：例 192.168.5.1";
            this.proxyip.Width = 120;
            // 
            // proxyport
            // 
            this.proxyport.HeaderText = "代理端口";
            this.proxyport.MaxInputLength = 5;
            this.proxyport.Name = "proxyport";
            this.proxyport.ToolTipText = "代理IP端口（1~65535）";
            this.proxyport.Width = 80;
            // 
            // proxytimelimit
            // 
            this.proxytimelimit.HeaderText = "响应时间(ms)";
            this.proxytimelimit.MaxInputLength = 10;
            this.proxytimelimit.Name = "proxytimelimit";
            this.proxytimelimit.ReadOnly = true;
            this.proxytimelimit.ToolTipText = "代理的连接响应时间（毫秒），超过10秒自动放弃";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(557, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 17;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(557, 190);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "button1";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(557, 129);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "button1";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(557, 190);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 17;
            this.Cancel.Text = "取消";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Save
            // 
            this.Save.Location = new System.Drawing.Point(557, 130);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(75, 23);
            this.Save.TabIndex = 17;
            this.Save.Text = "保存";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // Saveastxt
            // 
            this.Saveastxt.Location = new System.Drawing.Point(557, 250);
            this.Saveastxt.Name = "Saveastxt";
            this.Saveastxt.Size = new System.Drawing.Size(75, 23);
            this.Saveastxt.TabIndex = 17;
            this.Saveastxt.Text = "另存为文本";
            this.Saveastxt.UseVisualStyleBackColor = true;
            this.Saveastxt.Visible = false;
            // 
            // proxy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 362);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Saveastxt);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.proxylistdata);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.proxytimeoutseconds);
            this.Controls.Add(this.proxylistbox);
            this.Controls.Add(this.proxycheck);
            this.Controls.Add(this.proxyclear);
            this.Controls.Add(this.proxydelete);
            this.Controls.Add(this.proxyimport);
            this.Controls.Add(this.proxyadd);
            this.Controls.Add(this.proxytxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.proxymodel);
            this.Name = "proxy";
            this.Text = "proxy";
            this.Load += new System.EventHandler(this.proxy_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.proxy_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.proxylistdata)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox proxymodel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox proxytxt;
        private System.Windows.Forms.Button proxyadd;
        private System.Windows.Forms.Button proxyimport;
        private System.Windows.Forms.Button proxydelete;
        private System.Windows.Forms.Button proxyclear;
        private System.Windows.Forms.Button proxycheck;
        private System.Windows.Forms.ListBox proxylistbox;
        private System.Windows.Forms.TextBox proxytimeoutseconds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView proxylistdata;
        private System.Windows.Forms.DataGridViewTextBoxColumn proxyip;
        private System.Windows.Forms.DataGridViewTextBoxColumn proxyport;
        private System.Windows.Forms.DataGridViewTextBoxColumn proxytimelimit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button Saveastxt;
    }
}