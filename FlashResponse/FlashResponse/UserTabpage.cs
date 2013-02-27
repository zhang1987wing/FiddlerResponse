using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Fiddler;

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

        public UserTabpage()
        {
            InitializeComponent();

            this.groupBox2.Enabled = false;
            this.groupBox3.Enabled = false;
            this.groupBox4.Enabled = false;
        }

        public string getResponseTextBoxValue()
        {
            return this.response_ta.Text;
        }

        public string getUrlTextBoxValue()
        {
            return this.url_tb.Text;
        }

        public List<Para> getPara_list()
        {
            return paraName_list;
        }

        public bool getCheckBox1()
        {
            return this.checkBox1.Checked;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                this.groupBox2.Enabled = true;
                this.groupBox3.Enabled = true;
                this.groupBox4.Enabled = true;
            }
            else
            {
                this.groupBox2.Enabled = false;
                this.groupBox3.Enabled = false;
                this.groupBox4.Enabled = false;
            }
        }

        private void add_Button_Click(object sender, EventArgs e)
        {
            paraName_tb = new TextBox();
            paraValue_tb = new TextBox();
            paraType_tb = new ComboBox();

            this.draw_Para_Component(paraName_tb, paraValue_tb, paraType_tb, f);

            para = new Para(paraName_tb, paraValue_tb, paraType_tb);
            paraName_list.Add(para);
            paraName_list.TrimExcess();

            f += 1;
        }

        private void draw_Para_Component(TextBox paraName_tb, TextBox paraValue_tb, ComboBox paraType_tb, int f)
        {
            paraName_tb.Location = new System.Drawing.Point(7, 10 * (f + 1) + 21 * (f + 1));
            paraName_tb.Name = "paraName_tb" + f;
            paraName_tb.Size = new System.Drawing.Size(100, 21);

            paraValue_tb.Location = new System.Drawing.Point(149, 10 * (f + 1) + 21 * (f + 1));
            paraValue_tb.Name = "paraValue_tb" + f;
            paraValue_tb.Size = new System.Drawing.Size(100, 21);

            paraType_tb.FormattingEnabled = true;
            paraType_tb.Items.AddRange(new object[] {
            "MD5",
            "请求值",
            "响应值"});
            paraType_tb.Location = new System.Drawing.Point(291, 10 * (f + 1) + 21 * (f + 1));
            paraType_tb.Name = "paraType_tb" + f;
            paraType_tb.Size = new System.Drawing.Size(86, 21);

            this.groupBox3.Size = new System.Drawing.Size(this.groupBox3.Size.Width, this.groupBox3.Size.Height + 10 + 21);
            this.groupBox3.Controls.Add(paraName_tb);
            this.groupBox3.Controls.Add(paraValue_tb);
            this.groupBox3.Controls.Add(paraType_tb);

            foreach (Control btn in this.groupBox3.Controls)
            {
                if (btn is Button)
                {
                    btn.Location = new Point(btn.Location.X, btn.Location.Y + 10 + 21);
                }
            }

            this.groupBox4.Location = new Point(this.groupBox4.Location.X, this.groupBox4.Location.Y + 10 + 21);

            this.AutoScroll = true;
            this.AutoScrollPosition = new Point(0, 60 + 21);
        }

        private void minus_Button_Click(object sender, EventArgs e)
        {
            if (paraName_list.Count > 0)
            {
                this.groupBox3.Controls.Remove(paraName_list[paraName_list.Count - 1].getParaNameTextBox());
                this.groupBox3.Controls.Remove(paraName_list[paraName_list.Count - 1].getParaTypeComboBox());
                this.groupBox3.Controls.Remove(paraName_list[paraName_list.Count - 1].getParaValueTextBox());

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

                this.AutoScroll = true;
                this.AutoScrollPosition = new Point(0, 60 + 21);

                f -= 1;
            }
            else
            {
                MessageBox.Show("没有参数了");
            }
        }
    }
}
