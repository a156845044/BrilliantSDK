namespace Brilliant.ProjectStudio
{
    partial class FrmDBLogin
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.txtUid = new System.Windows.Forms.TextBox();
            this.cmbDataBase = new System.Windows.Forms.ComboBox();
            this.cmbDataSource = new System.Windows.Forms.ComboBox();
            this.cmbDataProvider = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(363, 349);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 54;
            this.btnCancel.Text = "取消(&C):";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(238, 349);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.TabIndex = 53;
            this.btnOK.Text = "确定(&O):";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Brilliant.ProjectStudio.Resources.dbl_line;
            this.pictureBox2.Location = new System.Drawing.Point(13, 333);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(519, 1);
            this.pictureBox2.TabIndex = 52;
            this.pictureBox2.TabStop = false;
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(184, 282);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.PasswordChar = '*';
            this.txtPwd.Size = new System.Drawing.Size(259, 21);
            this.txtPwd.TabIndex = 51;
            this.txtPwd.Text = "123qwe!@#";
            // 
            // txtUid
            // 
            this.txtUid.Location = new System.Drawing.Point(184, 247);
            this.txtUid.Name = "txtUid";
            this.txtUid.Size = new System.Drawing.Size(259, 21);
            this.txtUid.TabIndex = 50;
            this.txtUid.Text = "sa";
            // 
            // cmbDataBase
            // 
            this.cmbDataBase.FormattingEnabled = true;
            this.cmbDataBase.Location = new System.Drawing.Point(184, 216);
            this.cmbDataBase.Name = "cmbDataBase";
            this.cmbDataBase.Size = new System.Drawing.Size(259, 20);
            this.cmbDataBase.TabIndex = 49;
            this.cmbDataBase.Text = "master";
            // 
            // cmbDataSource
            // 
            this.cmbDataSource.FormattingEnabled = true;
            this.cmbDataSource.Location = new System.Drawing.Point(184, 183);
            this.cmbDataSource.Name = "cmbDataSource";
            this.cmbDataSource.Size = new System.Drawing.Size(259, 20);
            this.cmbDataSource.TabIndex = 48;
            this.cmbDataSource.Text = "192.168.0.101";
            this.cmbDataSource.SelectedIndexChanged += new System.EventHandler(this.cmbDataSource_SelectedIndexChanged);
            // 
            // cmbDataProvider
            // 
            this.cmbDataProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDataProvider.FormattingEnabled = true;
            this.cmbDataProvider.Location = new System.Drawing.Point(184, 151);
            this.cmbDataProvider.Name = "cmbDataProvider";
            this.cmbDataProvider.Size = new System.Drawing.Size(259, 20);
            this.cmbDataProvider.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(94, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 46;
            this.label5.Text = "密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(82, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 45;
            this.label4.Text = "登录名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(58, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 44;
            this.label3.Text = "默认数据库：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 43;
            this.label2.Text = "服务器名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 154);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 42;
            this.label1.Text = "服务器类型：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::Brilliant.ProjectStudio.Resources.dbl_bg;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(535, 103);
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // FrmDBLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 384);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.txtPwd);
            this.Controls.Add(this.txtUid);
            this.Controls.Add(this.cmbDataBase);
            this.Controls.Add(this.cmbDataSource);
            this.Controls.Add(this.cmbDataProvider);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(551, 422);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(551, 422);
            this.Name = "FrmDBLogin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "连接到服务器";
            this.Load += new System.EventHandler(this.FrmDBLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TextBox txtUid;
        private System.Windows.Forms.ComboBox cmbDataBase;
        private System.Windows.Forms.ComboBox cmbDataSource;
        private System.Windows.Forms.ComboBox cmbDataProvider;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}