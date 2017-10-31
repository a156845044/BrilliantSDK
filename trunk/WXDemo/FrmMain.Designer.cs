namespace WXDemo
{
    partial class FrmMain
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cbDept = new System.Windows.Forms.ComboBox();
            this.btnGetDept = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbDept
            // 
            this.cbDept.FormattingEnabled = true;
            this.cbDept.Location = new System.Drawing.Point(54, 26);
            this.cbDept.Name = "cbDept";
            this.cbDept.Size = new System.Drawing.Size(121, 20);
            this.cbDept.TabIndex = 0;
            this.cbDept.SelectedIndexChanged += new System.EventHandler(this.cbDept_SelectedIndexChanged);
            // 
            // btnGetDept
            // 
            this.btnGetDept.Location = new System.Drawing.Point(181, 24);
            this.btnGetDept.Name = "btnGetDept";
            this.btnGetDept.Size = new System.Drawing.Size(75, 23);
            this.btnGetDept.TabIndex = 1;
            this.btnGetDept.Text = "获取单位";
            this.btnGetDept.UseVisualStyleBackColor = true;
            this.btnGetDept.Click += new System.EventHandler(this.btnGetDept_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "单位:";
            // 
            // lbUsers
            // 
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.ItemHeight = 12;
            this.lbUsers.Location = new System.Drawing.Point(15, 67);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(241, 328);
            this.lbUsers.TabIndex = 3;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(275, 144);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(257, 215);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(457, 372);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 5;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 421);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.lbUsers);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetDept);
            this.Controls.Add(this.cbDept);
            this.Name = "FrmMain";
            this.Text = "微信接口演示";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDept;
        private System.Windows.Forms.Button btnGetDept;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnSend;
    }
}

