using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Brilliant.Data.Common;
using Brilliant.Data;
using EnvDTE;
using System.IO;

namespace Brilliant.ProjectStudio
{
    public partial class FrmDBMgr : Form
    {
        private static readonly FrmDBMgr instance = new FrmDBMgr(); //当前窗体实例
        private List<TemplateInfo> templateList;

        /// <summary>
        /// 对象实例
        /// </summary>
        public static FrmDBMgr Instance
        {
            get { return FrmDBMgr.instance; }
        }

        /// <summary>
        /// 获取当前选中的数据库名称
        /// </summary>
        public string SelectedDb
        {
            get
            {
                TreeNode node = this.tvwDbList.SelectedNode;
                int level = node.Level;
                if (level == 0)
                {
                    return String.Empty;
                }
                else if (level == 1)
                {
                    return node.Text;
                }
                else
                {
                    string str = node.FullPath.Substring(node.FullPath.IndexOf("|") + 1);
                    string dataBase = str.Substring(0, str.IndexOf("|"));
                    return dataBase;
                }
            }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public FrmDBMgr()
        {
            InitializeComponent();
            this.tvwDbList.ImageList = ResManager.SysImageList;
            BindTemplateList();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        private void FrmDBMgr_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 绑定数据库列表
        /// </summary>
        public void BindDbList()
        {
            Com.Output("  {0} -> 开始获取数据库架构.", DBContext.CurrentConnection.DataSource);
            string imaKey = ResImageName.DbmServer;
            TreeNode root = new TreeNode();
            root.Text = DBContext.CurrentKey;
            root.ImageIndex = ResManager.SysImageList.Images.IndexOfKey(imaKey);
            root.SelectedImageIndex = ResManager.SysImageList.Images.IndexOfKey(imaKey);
            root.ContextMenuStrip = this.cmsConnection;
            IList<DboBase> list = DBContext.SchemaProvider.GetDbList();
            int imgIndex = ResManager.SysImageList.Images.IndexOfKey(ResImageName.DbmDb);
            int count = 0;
            foreach (DboBase db in list)
            {
                TreeNode node = new TreeNode();
                node.Text = db.DboName;
                node.ImageIndex = imgIndex;
                node.SelectedImageIndex = imgIndex;
                node.ContextMenuStrip = cmsDB;
                root.Nodes.Add(node);
                GetCommonChildNode(ref node);
                count++;
            }
            this.tvwDbList.Nodes.Add(root);
            this.tvwDbList.SelectedNode = root;
            root.Expand();
            Com.Output("======== 登录: 成功，获取到 {0} 条，失败 0 个，跳过 0 个 ========", count);
        }

        /// <summary>
        /// 获取数据库架构列表
        /// </summary>
        private void GetCommonChildNode(ref TreeNode node)
        {
            int imgIndex = ResManager.SysImageList.Images.IndexOfKey(ResImageName.ComFolderClose);
            TreeNode nodeTable = new TreeNode();
            nodeTable.Text = ResDescText.Table;
            nodeTable.ImageIndex = imgIndex;
            nodeTable.SelectedImageIndex = imgIndex;
            nodeTable.Nodes.Add("");
            node.Nodes.Add(nodeTable);
            TreeNode nodeView = new TreeNode();
            nodeView.Text = ResDescText.View;
            nodeView.ImageIndex = imgIndex;
            nodeView.SelectedImageIndex = imgIndex;
            nodeView.Nodes.Add("");
            node.Nodes.Add(nodeView);
            TreeNode nodeProc = new TreeNode();
            nodeProc.Text = ResDescText.Proc;
            nodeProc.ImageIndex = imgIndex;
            nodeProc.SelectedImageIndex = imgIndex;
            nodeProc.Nodes.Add("");
            node.Nodes.Add(nodeProc);
        }

        /// <summary>
        /// 绑定模板列表
        /// </summary>
        private void BindTemplateList()
        {
            templateList = ConfigMgr.GetTemplates();
            List<ToolStripMenuItem> itemList = new List<ToolStripMenuItem>();
            foreach (TemplateInfo temp in templateList)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = temp.Name;
                item.Tag = temp.Path;
                item.Click += new EventHandler(template_Click);
                itemList.Add(item);
            }
            this.itemCreateCode.DropDownItems.AddRange(itemList.ToArray());
        }

        /// <summary>
        /// 连接对象资源管理器
        /// </summary>
        private void itemConnect_Click(object sender, EventArgs e)
        {
            FrmDBLogin.Instance.ShowDialog();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        private void itemDisconnect_Click(object sender, EventArgs e)
        {
            if (this.tvwDbList.SelectedNode == null)
            {
                return;
            }
            TreeNode selRoot = null;
            if (this.tvwDbList.SelectedNode.Level > 0)
            {
                TreeNode par = this.tvwDbList.SelectedNode.Parent;
                while (par.Level != 0)
                {
                    par = par.Parent;
                }
                selRoot = par;
            }
            else
            {
                selRoot = this.tvwDbList.SelectedNode;
            }
            DBContext.Disconnect(selRoot.Text);
            selRoot.Remove();
        }

        /// <summary>
        /// 折叠变更节点图标
        /// </summary>
        private void tvwDbList_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 2)
            {
                e.Node.ImageIndex = ResManager.SysImageList.Images.IndexOfKey(ResImageName.ComFolderClose);
                e.Node.SelectedImageIndex = e.Node.ImageIndex;
            }
        }

        /// <summary>
        /// 展开变更节点图标
        /// </summary>
        private void tvwDbList_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 2)
            {
                e.Node.ImageIndex = ResManager.SysImageList.Images.IndexOfKey(ResImageName.ComFolderOpen);
                e.Node.SelectedImageIndex = e.Node.ImageIndex;
            }
        }

        /// <summary>
        /// 选中后变更当前连接
        /// </summary>
        private void tvwDbList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string currentSource = DBContext.CurrentConnection.DataSource;
            //是否是根节点
            if (e.Node.Level == 0)
            {
                //判断选择前和选择后的数据源是否相同
                if (e.Node.Text != currentSource)
                {
                    //不同则变更为选中的数据源
                    DBContext.ChangeDataSource(e.Node.Text);
                }
            }
            //二级节点
            else if (e.Node.Level == 1)
            {
                if (e.Node.Parent.Text != currentSource)
                {
                    DBContext.ChangeDataSource(e.Node.Parent.Text);
                }
                if (e.Node.Text != currentSource)
                {
                    DBContext.ChangeDataBase(e.Node.Text);
                }
            }
            else
            {
                string dataSource = e.Node.FullPath.Substring(0, e.Node.FullPath.IndexOf("|"));
                string str = e.Node.FullPath.Substring(e.Node.FullPath.IndexOf("|") + 1);
                string dataBase = str.Substring(0, str.IndexOf("|"));
                if (dataSource != currentSource)
                {
                    DBContext.ChangeDataSource(dataSource);
                }
                if (dataBase != currentSource)
                {
                    DBContext.ChangeDataBase(dataBase);
                }
            }
        }

        /// <summary>
        /// 展开数据表、视图、存储过程文件夹时加载数据
        /// </summary>
        private void tvwDbList_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Level == 2)
            {
                this.tvwDbList.SelectedNode = e.Node;
                if (e.Node.Nodes.Count <= 1 && String.IsNullOrEmpty(e.Node.Nodes[0].Text))
                {
                    e.Node.Nodes.Clear();
                }
                else
                {
                    return;
                }

                int imgIndex = ResManager.SysImageList.Images.IndexOfKey(ResImageName.DbmTable);
                if (e.Node.Text == ResDescText.Table)
                {
                    if (e.Node.Nodes.Count > 0)
                    {
                        return;
                    }
                    DBContext.ChangeDataBase(e.Node.Parent.Text);
                    IList<DboTable> list = DBContext.SchemaProvider.GetTableList();
                    foreach (DboTable table in list)
                    {
                        TreeNode node = new TreeNode();
                        node.Text = table.DboName;
                        node.ToolTipText = table.DboName;
                        node.ImageIndex = imgIndex;
                        node.SelectedImageIndex = imgIndex;
                        node.ContextMenuStrip = this.cmsTable;
                        e.Node.Nodes.Add(node);
                    }
                }
                if (e.Node.Text == ResDescText.View)
                {
                    if (e.Node.Nodes.Count > 0)
                    {
                        return;
                    }
                    DBContext.ChangeDataBase(e.Node.Parent.Text);
                    IList<DboView> list = DBContext.SchemaProvider.GetViewList();
                    foreach (DboView view in list)
                    {
                        TreeNode node = new TreeNode();
                        node.Text = view.DboName;
                        node.ToolTipText = view.DboName;
                        node.ImageIndex = imgIndex;
                        node.SelectedImageIndex = imgIndex;
                        //node.ContextMenuStrip = this.dbMenuStrip;
                        e.Node.Nodes.Add(node);
                    }
                }
                if (e.Node.Text == ResDescText.Proc)
                {
                    if (e.Node.Nodes.Count > 0)
                    {
                        return;
                    }
                    DBContext.ChangeDataBase(e.Node.Parent.Text);
                    IList<DboProc> list = DBContext.SchemaProvider.GetProcList();
                    foreach (DboProc proc in list)
                    {
                        TreeNode node = new TreeNode();
                        node.Text = proc.DboName;
                        node.ToolTipText = proc.DboName;
                        node.ImageIndex = imgIndex;
                        node.SelectedImageIndex = imgIndex;
                        //node.ContextMenuStrip = this.dbMenuStrip;
                        e.Node.Nodes.Add(node);
                    }
                }
            }
        }

        /// <summary>
        /// 恢复因失去焦点时变灰的背景色
        /// </summary>
        private void tvwDbList_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (this.tvwDbList.SelectedNode != null)
            {
                this.tvwDbList.SelectedNode.BackColor = SystemColors.Window;
            }
        }

        /// <summary>
        /// 右击时选中
        /// </summary>
        private void tvwDbList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode tn = this.tvwDbList.GetNodeAt(e.X, e.Y);
                if (tn != null)
                {
                    this.tvwDbList.SelectedNode = tn;
                }
            }
        }

        /// <summary>
        /// 失去焦点时变灰
        /// </summary>
        private void tvwDbList_Validated(object sender, EventArgs e)
        {
            if (this.tvwDbList.SelectedNode != null)
            {
                this.tvwDbList.SelectedNode.BackColor = Color.LightGray;
            }
        }

        /// <summary>
        /// 导出到Word
        /// </summary>
        private void itemExportToWord_Click(object sender, EventArgs e)
        {
            FrmExportSchema frmExportSchema = new FrmExportSchema(this.tvwDbList.SelectedNode.Text);
            frmExportSchema.Show();
        }

        /// <summary>
        /// 点击模板创建代码
        /// </summary>
        private void template_Click(object sender, EventArgs e)
        {
            if (this.tvwDbList.SelectedNode != null)
            {
                ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
                string templatePath = tsmi.Tag.ToString();

                if (!File.Exists(templatePath))
                {
                    MessageBox.Show("模版不存在或已删除!", "系统提示");
                    return;
                }
                var result = (from item in templateList where item.Name == tsmi.Text select item).ToList();
                if (result != null && result.Count > 0)
                {
                    TemplateInfo temp = result[0];
                    string tableName = this.tvwDbList.SelectedNode.Text;
                    string fileName = tableName + temp.Output;
                    SchemaTable table = DBContext.SchemaProvider.GetSchemaInfo(tableName);
                    if (string.IsNullOrWhiteSpace(table.TableDesc))
                    {
                        table.TableDesc = " ";
                    }
                    CodeGenerator codeGenerator = new CodeGenerator();
                    string code = codeGenerator.Generate(templatePath, table);

                    Window win = Com.VS.ItemOperations.NewFile("常规\\Visual C# 类", fileName);
                    TextDocument objTextDoc = Com.VS.ActiveDocument.Object("TextDocument") as TextDocument;
                    objTextDoc.Selection.SelectAll();
                    objTextDoc.Selection.Delete();
                    EditPoint objEP = objTextDoc.StartPoint.CreateEditPoint();
                    objEP.Insert(code);
                    code = String.Empty;
                    codeGenerator.Close();
                    codeGenerator = null;
                }
                else
                {
                    MessageBox.Show("未找到对应的模板!", "系统提示");
                }
            }
        }

        /// <summary>
        /// 查看表数据
        /// </summary>
        private void itemViewData_Click(object sender, EventArgs e)
        {
            string sql = String.Format("USE {0}\r\nGO\r\n\r\nSELECT * FROM {1}", DBContext.CurrentConnection.DataBase, this.tvwDbList.SelectedNode.Text);
            Window win = Com.VS.ItemOperations.NewFile("常规\\Sql 文件", "SQLQuery");
            TextDocument objTextDoc = Com.VS.ActiveDocument.Object("TextDocument") as TextDocument;
            objTextDoc.Selection.SelectAll();
            objTextDoc.Selection.Delete();
            EditPoint objEP = objTextDoc.StartPoint.CreateEditPoint();
            objEP.Insert(sql);
        }

        /// <summary>
        /// 查看表结构
        /// </summary>
        private void itemViewSchema_Click(object sender, EventArgs e)
        {
            string tableName = this.tvwDbList.SelectedNode.Text;
            string sql = DBContext.CurrentConnection.SchemaProvider.GetSchemaQuerySql(tableName);
            Window win = Com.VS.ItemOperations.NewFile("常规\\Sql 文件", "SQLQuery");
            TextDocument objTextDoc = Com.VS.ActiveDocument.Object("TextDocument") as TextDocument;
            objTextDoc.Selection.SelectAll();
            objTextDoc.Selection.Delete();
            EditPoint objEP = objTextDoc.StartPoint.CreateEditPoint();
            objEP.Insert(String.Format("USE {0}\r\nGO\r\n\r\n", DBContext.CurrentConnection.DataBase));
            objEP.Insert(sql);
        }

        /// <summary>
        /// 比对数据库
        /// </summary>
        private void itemCompare_Click(object sender, EventArgs e)
        {
            if (DBContext.CurrentConnection == null)
            {
                FrmDBLogin.Instance.ShowDialog();
                return;
            }
            FrmCompare frmCompare = new FrmCompare();
            frmCompare.ShowDialog();
        }
    }
}
