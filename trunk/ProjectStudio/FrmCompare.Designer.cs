namespace Brilliant.ProjectStudio
{
    partial class FrmCompare
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvwDB = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lstDB = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.lvwDB);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(607, 292);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.lstDB);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(444, 26);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(155, 252);
            this.panel2.TabIndex = 13;
            // 
            // lvwDB
            // 
            this.lvwDB.HideSelection = false;
            this.lvwDB.Location = new System.Drawing.Point(10, 26);
            this.lvwDB.Name = "lvwDB";
            this.lvwDB.Size = new System.Drawing.Size(423, 252);
            this.lvwDB.TabIndex = 12;
            this.lvwDB.UseCompatibleStateImageBehavior = false;
            this.lvwDB.View = System.Windows.Forms.View.List;
            this.lvwDB.SelectedIndexChanged += new System.EventHandler(this.lvwDB_SelectedIndexChanged);
            this.lvwDB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwDB_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "数据库：";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(524, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCompare
            // 
            this.btnCompare.Location = new System.Drawing.Point(423, 9);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 30);
            this.btnCompare.TabIndex = 2;
            this.btnCompare.Text = "比对(&P)";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(73)))), ((int)(((byte)(106)))));
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnCompare);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 292);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(607, 51);
            this.panel3.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "已选择：";
            // 
            // lstDB
            // 
            this.lstDB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstDB.FormattingEnabled = true;
            this.lstDB.ItemHeight = 12;
            this.lstDB.Location = new System.Drawing.Point(15, 28);
            this.lstDB.Name = "lstDB";
            this.lstDB.Size = new System.Drawing.Size(120, 216);
            this.lstDB.TabIndex = 1;
            // 
            // FrmCompare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.ClientSize = new System.Drawing.Size(607, 343);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCompare";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "比对数据库";
            this.Load += new System.EventHandler(this.FrmCompare_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListView lvwDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstDB;


    }
}