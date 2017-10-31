namespace Brilliant.ProjectStudio
{
    partial class FrmExportSchema
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
            this.btnExport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbDBList = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvwDBTable = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(499, 331);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 30);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "导出(&E)";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(586, 331);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbDBList);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(651, 29);
            this.panel1.TabIndex = 13;
            // 
            // cmbDBList
            // 
            this.cmbDBList.FormattingEnabled = true;
            this.cmbDBList.Location = new System.Drawing.Point(99, 3);
            this.cmbDBList.Name = "cmbDBList";
            this.cmbDBList.Size = new System.Drawing.Size(121, 20);
            this.cmbDBList.TabIndex = 23;
            this.cmbDBList.SelectedIndexChanged += new System.EventHandler(this.cmbDBList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "数据库列表(&D):";
            // 
            // lvwDBTable
            // 
            this.lvwDBTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvwDBTable.HideSelection = false;
            this.lvwDBTable.Location = new System.Drawing.Point(10, 39);
            this.lvwDBTable.Name = "lvwDBTable";
            this.lvwDBTable.Size = new System.Drawing.Size(651, 286);
            this.lvwDBTable.TabIndex = 14;
            this.lvwDBTable.UseCompatibleStateImageBehavior = false;
            this.lvwDBTable.View = System.Windows.Forms.View.List;
            this.lvwDBTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwDBTable_KeyDown);
            // 
            // FrmExportSchema
            // 
            this.AcceptButton = this.btnExport;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.ClientSize = new System.Drawing.Size(671, 365);
            this.Controls.Add(this.lvwDBTable);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnCancel);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(687, 403);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(687, 403);
            this.Name = "FrmExportSchema";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导出表结构";
            this.Load += new System.EventHandler(this.FrmExportSchema_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbDBList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvwDBTable;
    }
}