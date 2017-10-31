namespace Brilliant.ProjectStudio
{
    partial class DiaCreateFileProgress
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
            this.lblFileInfo = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // lblFileInfo
            // 
            this.lblFileInfo.AutoSize = true;
            this.lblFileInfo.Location = new System.Drawing.Point(8, 29);
            this.lblFileInfo.Name = "lblFileInfo";
            this.lblFileInfo.Size = new System.Drawing.Size(83, 12);
            this.lblFileInfo.TabIndex = 6;
            this.lblFileInfo.Text = "正在创建文件:";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(8, 51);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(488, 23);
            this.progressBar.TabIndex = 5;
            // 
            // DiaCreateFileProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 102);
            this.ControlBox = false;
            this.Controls.Add(this.lblFileInfo);
            this.Controls.Add(this.progressBar);
            this.MaximumSize = new System.Drawing.Size(520, 140);
            this.MinimumSize = new System.Drawing.Size(520, 140);
            this.Name = "DiaCreateFileProgress";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "正在创建代码...";
            this.Load += new System.EventHandler(this.DiaCreateFileProgress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFileInfo;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}