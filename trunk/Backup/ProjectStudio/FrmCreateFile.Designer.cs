namespace Brilliant.ProjectStudio
{
    partial class FrmCreateFile
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
            this.lblTableDesc = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cmbProjPath = new System.Windows.Forms.ComboBox();
            this.cmbTemplate = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbDBList = new System.Windows.Forms.ComboBox();
            this.chkOpen = new System.Windows.Forms.CheckBox();
            this.txtNameSpace = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lvwDBTable = new System.Windows.Forms.ListView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTableDesc
            // 
            this.lblTableDesc.Location = new System.Drawing.Point(13, 35);
            this.lblTableDesc.Name = "lblTableDesc";
            this.lblTableDesc.Size = new System.Drawing.Size(142, 264);
            this.lblTableDesc.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(45, 396);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "项目路径(&P):";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "数据库列表(&D):";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.panel2.Controls.Add(this.cmbProjPath);
            this.panel2.Controls.Add(this.cmbTemplate);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cmbDBList);
            this.panel2.Controls.Add(this.chkOpen);
            this.panel2.Controls.Add(this.txtNameSpace);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lvwDBTable);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(846, 458);
            this.panel2.TabIndex = 5;
            // 
            // cmbProjPath
            // 
            this.cmbProjPath.FormattingEnabled = true;
            this.cmbProjPath.Location = new System.Drawing.Point(152, 393);
            this.cmbProjPath.Name = "cmbProjPath";
            this.cmbProjPath.Size = new System.Drawing.Size(517, 20);
            this.cmbProjPath.TabIndex = 24;
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.FormattingEnabled = true;
            this.cmbTemplate.Location = new System.Drawing.Point(152, 427);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(517, 20);
            this.cmbTemplate.TabIndex = 23;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(45, 430);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 22;
            this.label5.Text = "代码模板(&P):";
            // 
            // cmbDBList
            // 
            this.cmbDBList.FormattingEnabled = true;
            this.cmbDBList.Location = new System.Drawing.Point(107, 8);
            this.cmbDBList.Name = "cmbDBList";
            this.cmbDBList.Size = new System.Drawing.Size(121, 20);
            this.cmbDBList.TabIndex = 21;
            this.cmbDBList.SelectedIndexChanged += new System.EventHandler(this.cmbDBList_SelectedIndexChanged);
            // 
            // chkOpen
            // 
            this.chkOpen.AutoSize = true;
            this.chkOpen.Location = new System.Drawing.Point(675, 429);
            this.chkOpen.Name = "chkOpen";
            this.chkOpen.Size = new System.Drawing.Size(132, 16);
            this.chkOpen.TabIndex = 20;
            this.chkOpen.Text = "完成后是否打开文件";
            this.chkOpen.UseVisualStyleBackColor = true;
            this.chkOpen.Visible = false;
            // 
            // txtNameSpace
            // 
            this.txtNameSpace.Location = new System.Drawing.Point(152, 361);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new System.Drawing.Size(517, 21);
            this.txtNameSpace.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(45, 364);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "命名空间(&N):";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.Controls.Add(this.lblTableDesc);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(675, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(167, 315);
            this.panel3.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "表描述信息:";
            // 
            // lvwDBTable
            // 
            this.lvwDBTable.HideSelection = false;
            this.lvwDBTable.Location = new System.Drawing.Point(12, 33);
            this.lvwDBTable.Name = "lvwDBTable";
            this.lvwDBTable.Size = new System.Drawing.Size(657, 315);
            this.lvwDBTable.TabIndex = 8;
            this.lvwDBTable.UseCompatibleStateImageBehavior = false;
            this.lvwDBTable.View = System.Windows.Forms.View.List;
            this.lvwDBTable.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvwDBTable_ItemSelectionChanged);
            this.lvwDBTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwDBTable_KeyDown);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(755, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(73)))), ((int)(((byte)(106)))));
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnGenerate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 458);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(846, 42);
            this.panel1.TabIndex = 4;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(654, 6);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(75, 30);
            this.btnGenerate.TabIndex = 0;
            this.btnGenerate.Text = "生成(&G)";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // FrmCreateFile
            // 
            this.AcceptButton = this.btnGenerate;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(846, 500);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(862, 538);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(862, 538);
            this.Name = "FrmCreateFile";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "生成代码";
            this.Load += new System.EventHandler(this.FrmCreateFile_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTableDesc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox chkOpen;
        private System.Windows.Forms.TextBox txtNameSpace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView lvwDBTable;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ComboBox cmbDBList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTemplate;
        private System.Windows.Forms.ComboBox cmbProjPath;
    }
}