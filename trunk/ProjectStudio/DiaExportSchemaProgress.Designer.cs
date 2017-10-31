namespace Brilliant.ProjectStudio
{
    partial class DiaExportSchemaProgress
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
            this.lblMsg = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.Location = new System.Drawing.Point(8, 29);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(71, 12);
            this.lblMsg.TabIndex = 8;
            this.lblMsg.Text = "正在导出表:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(8, 51);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(488, 23);
            this.progressBar.TabIndex = 7;
            // 
            // DiaExportSchemaProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 102);
            this.ControlBox = false;
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.progressBar);
            this.MaximumSize = new System.Drawing.Size(520, 140);
            this.MinimumSize = new System.Drawing.Size(520, 140);
            this.Name = "DiaExportSchemaProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "正在导出表结构...";
            this.Load += new System.EventHandler(this.DiaExportSchemaProgress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}