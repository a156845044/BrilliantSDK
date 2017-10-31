namespace Brilliant.ProjectStudio
{
    partial class FrmDBMgr
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
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.itemConnect = new System.Windows.Forms.ToolStripButton();
            this.itemDisconnect = new System.Windows.Forms.ToolStripButton();
            this.itemCompare = new System.Windows.Forms.ToolStripButton();
            this.tvwDbList = new System.Windows.Forms.TreeView();
            this.cmsConnection = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.连接CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.断开连接DToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.新建查询QToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.新建数据库NToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.附加AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.还原数据库RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.刷新FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.属性PToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsDB = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemExportToWord = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemViewSchema = new System.Windows.Forms.ToolStripMenuItem();
            this.itemViewData = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCreateCode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.cmsConnection.SuspendLayout();
            this.cmsDB.SuspendLayout();
            this.cmsTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemConnect,
            this.itemDisconnect,
            this.itemCompare});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(251, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // itemConnect
            // 
            this.itemConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itemConnect.Image = global::Brilliant.ProjectStudio.Resources.dbm_connect;
            this.itemConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itemConnect.Name = "itemConnect";
            this.itemConnect.Size = new System.Drawing.Size(23, 22);
            this.itemConnect.Text = "连接对象资源管理器";
            this.itemConnect.Click += new System.EventHandler(this.itemConnect_Click);
            // 
            // itemDisconnect
            // 
            this.itemDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itemDisconnect.Image = global::Brilliant.ProjectStudio.Resources.dbm_disconnect;
            this.itemDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itemDisconnect.Name = "itemDisconnect";
            this.itemDisconnect.Size = new System.Drawing.Size(23, 22);
            this.itemDisconnect.Text = "断开连接";
            this.itemDisconnect.Click += new System.EventHandler(this.itemDisconnect_Click);
            // 
            // itemCompare
            // 
            this.itemCompare.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.itemCompare.Image = global::Brilliant.ProjectStudio.Resources.dbq_message;
            this.itemCompare.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.itemCompare.Name = "itemCompare";
            this.itemCompare.Size = new System.Drawing.Size(23, 22);
            this.itemCompare.Text = "比对数据库";
            this.itemCompare.Click += new System.EventHandler(this.itemCompare_Click);
            // 
            // tvwDbList
            // 
            this.tvwDbList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvwDbList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvwDbList.Location = new System.Drawing.Point(0, 25);
            this.tvwDbList.Name = "tvwDbList";
            this.tvwDbList.PathSeparator = "|";
            this.tvwDbList.Size = new System.Drawing.Size(251, 389);
            this.tvwDbList.TabIndex = 1;
            this.tvwDbList.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvwDbList_AfterCollapse);
            this.tvwDbList.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwDbList_BeforeExpand);
            this.tvwDbList.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvwDbList_AfterExpand);
            this.tvwDbList.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvwDbList_BeforeSelect);
            this.tvwDbList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvwDbList_AfterSelect);
            this.tvwDbList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.tvwDbList_MouseUp);
            this.tvwDbList.Validated += new System.EventHandler(this.tvwDbList_Validated);
            // 
            // cmsConnection
            // 
            this.cmsConnection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.连接CToolStripMenuItem,
            this.断开连接DToolStripMenuItem,
            this.toolStripSeparator1,
            this.新建查询QToolStripMenuItem,
            this.toolStripSeparator2,
            this.新建数据库NToolStripMenuItem,
            this.附加AToolStripMenuItem,
            this.还原数据库RToolStripMenuItem,
            this.toolStripSeparator3,
            this.刷新FToolStripMenuItem,
            this.属性PToolStripMenuItem});
            this.cmsConnection.Name = "cmsConnection";
            this.cmsConnection.Size = new System.Drawing.Size(164, 198);
            // 
            // 连接CToolStripMenuItem
            // 
            this.连接CToolStripMenuItem.Name = "连接CToolStripMenuItem";
            this.连接CToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.连接CToolStripMenuItem.Text = "连接(&C)...";
            // 
            // 断开连接DToolStripMenuItem
            // 
            this.断开连接DToolStripMenuItem.Name = "断开连接DToolStripMenuItem";
            this.断开连接DToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.断开连接DToolStripMenuItem.Text = "断开连接(&D)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(160, 6);
            // 
            // 新建查询QToolStripMenuItem
            // 
            this.新建查询QToolStripMenuItem.Name = "新建查询QToolStripMenuItem";
            this.新建查询QToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.新建查询QToolStripMenuItem.Text = "新建查询(&Q)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(160, 6);
            // 
            // 新建数据库NToolStripMenuItem
            // 
            this.新建数据库NToolStripMenuItem.Name = "新建数据库NToolStripMenuItem";
            this.新建数据库NToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.新建数据库NToolStripMenuItem.Text = "新建数据库(&N)...";
            // 
            // 附加AToolStripMenuItem
            // 
            this.附加AToolStripMenuItem.Name = "附加AToolStripMenuItem";
            this.附加AToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.附加AToolStripMenuItem.Text = "附加(&A)...";
            // 
            // 还原数据库RToolStripMenuItem
            // 
            this.还原数据库RToolStripMenuItem.Name = "还原数据库RToolStripMenuItem";
            this.还原数据库RToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.还原数据库RToolStripMenuItem.Text = "还原数据库(&R)...";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(160, 6);
            // 
            // 刷新FToolStripMenuItem
            // 
            this.刷新FToolStripMenuItem.Name = "刷新FToolStripMenuItem";
            this.刷新FToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.刷新FToolStripMenuItem.Text = "刷新(&F)";
            // 
            // 属性PToolStripMenuItem
            // 
            this.属性PToolStripMenuItem.Name = "属性PToolStripMenuItem";
            this.属性PToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.属性PToolStripMenuItem.Text = "属性(&P)";
            // 
            // cmsDB
            // 
            this.cmsDB.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemExportToWord});
            this.cmsDB.Name = "cmsDB";
            this.cmsDB.Size = new System.Drawing.Size(146, 26);
            // 
            // itemExportToWord
            // 
            this.itemExportToWord.Image = global::Brilliant.ProjectStudio.Resources.com_word;
            this.itemExportToWord.Name = "itemExportToWord";
            this.itemExportToWord.Size = new System.Drawing.Size(145, 22);
            this.itemExportToWord.Text = "导出到Word";
            this.itemExportToWord.Click += new System.EventHandler(this.itemExportToWord_Click);
            // 
            // cmsTable
            // 
            this.cmsTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemViewSchema,
            this.itemViewData,
            this.itemCreateCode});
            this.cmsTable.Name = "cmsTable";
            this.cmsTable.Size = new System.Drawing.Size(137, 70);
            // 
            // itemViewSchema
            // 
            this.itemViewSchema.Name = "itemViewSchema";
            this.itemViewSchema.Size = new System.Drawing.Size(136, 22);
            this.itemViewSchema.Text = "查看表结构";
            this.itemViewSchema.Click += new System.EventHandler(this.itemViewSchema_Click);
            // 
            // itemViewData
            // 
            this.itemViewData.Name = "itemViewData";
            this.itemViewData.Size = new System.Drawing.Size(136, 22);
            this.itemViewData.Text = "浏览表数据";
            this.itemViewData.Click += new System.EventHandler(this.itemViewData_Click);
            // 
            // itemCreateCode
            // 
            this.itemCreateCode.Name = "itemCreateCode";
            this.itemCreateCode.Size = new System.Drawing.Size(136, 22);
            this.itemCreateCode.Text = "生成代码";
            // 
            // FrmDBMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(188)))), ((int)(((byte)(199)))), ((int)(((byte)(216)))));
            this.ClientSize = new System.Drawing.Size(251, 414);
            this.Controls.Add(this.tvwDbList);
            this.Controls.Add(this.toolStrip1);
            this.Name = "FrmDBMgr";
            this.Text = "对象资源管理器";
            this.Load += new System.EventHandler(this.FrmDBMgr_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.cmsConnection.ResumeLayout(false);
            this.cmsDB.ResumeLayout(false);
            this.cmsTable.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.TreeView tvwDbList;
        private System.Windows.Forms.ToolStripButton itemConnect;
        private System.Windows.Forms.ToolStripButton itemDisconnect;
        private System.Windows.Forms.ContextMenuStrip cmsConnection;
        private System.Windows.Forms.ToolStripMenuItem 连接CToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 断开连接DToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem 新建查询QToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 新建数据库NToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 附加AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 还原数据库RToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 刷新FToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 属性PToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsDB;
        private System.Windows.Forms.ToolStripMenuItem itemExportToWord;
        private System.Windows.Forms.ContextMenuStrip cmsTable;
        private System.Windows.Forms.ToolStripMenuItem itemViewSchema;
        private System.Windows.Forms.ToolStripMenuItem itemViewData;
        private System.Windows.Forms.ToolStripMenuItem itemCreateCode;
        private System.Windows.Forms.ToolStripButton itemCompare;

    }
}