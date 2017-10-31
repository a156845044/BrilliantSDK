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

namespace Brilliant.ProjectStudio
{
    public partial class FrmCreateFile : Form
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public FrmCreateFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        private void FrmCreateFile_Load(object sender, EventArgs e)
        {
            Project project = Com.ActiveProject; //获取当前项目工程
            if (project != null)
            {
                this.txtNameSpace.Text = project.Name;
                string path = Utility.GetPath(project.FullName);
                List<string> pathList = Utility.GetSubFolders(path);
                this.cmbProjPath.DataSource = pathList;
            }

            BindDBList();
            BindTemplateList();
        }

        /// <summary>
        /// 绑定数据库列表
        /// </summary>
        private void BindDBList()
        {
            IList<DboBase> list = DBContext.CurrentConnection.SchemaProvider.GetDbList();
            this.cmbDBList.DataSource = list;
            this.cmbDBList.DisplayMember = "DboName";
            this.cmbDBList.ValueMember = "DboId";
            string selectedDB = FrmDBMgr.Instance.SelectedDb;
            if (!String.IsNullOrEmpty(selectedDB))
            {
                this.cmbDBList.Text = selectedDB;
            }
        }

        /// <summary>
        /// 绑定数据表列表
        /// </summary>
        private void BindTableList()
        {
            this.lvwDBTable.SmallImageList = ResManager.SysImageList;
            IList<DboTable> list = DBContext.CurrentConnection.SchemaProvider.GetTableList();
            this.lvwDBTable.Items.Clear();
            foreach (DboTable table in list)
            {
                ListViewItem item = new ListViewItem();
                item.Text = table.DboName;
                item.ImageIndex = ResManager.SysImageList.Images.IndexOfKey(ResImageName.DbmTable);
                this.lvwDBTable.Items.Add(item);
            }
            if (this.lvwDBTable.Items.Count > 0)
            {
                this.lvwDBTable.Items[0].Selected = true;
            }
        }

        /// <summary>
        /// 绑定模板列表
        /// </summary>
        private void BindTemplateList()
        {
            List<TemplateInfo> list = ConfigMgr.GetTemplates();
            this.cmbTemplate.DataSource = list;
            this.cmbTemplate.DisplayMember = "Path";
            this.cmbTemplate.ValueMember = "Name";
        }

        /// <summary>
        /// 点击生成
        /// </summary>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string nameSpace = this.txtNameSpace.Text; //命名空间
            string projPath = this.cmbProjPath.Text; //文件路径
            //获取选中的数据表名称
            IList<string> selectedItems = new List<string>();
            foreach (ListViewItem item in this.lvwDBTable.SelectedItems)
            {
                selectedItems.Add(item.Text);
            }
            //获取当前选中的模板
            TemplateInfo template = this.cmbTemplate.SelectedItem as TemplateInfo;
            //创建进度条
            DiaCreateFileProgress dcfp = new DiaCreateFileProgress(nameSpace, projPath, selectedItems, this.chkOpen.Checked, template);
            dcfp.ShowDialog();
            this.Close();
        }

        /// <summary>
        /// 点击取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 选中数据表名称时获取到数据表的描述信息
        /// </summary>
        private void lvwDBTable_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                this.lblTableDesc.Text = DBContext.SchemaProvider.GetTableDesc(e.Item.Text);
            }
        }

        /// <summary>
        /// Ctrl+A全选
        /// </summary>
        private void lvwDBTable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.A)
            {
                foreach (ListViewItem item in this.lvwDBTable.Items)
                {
                    item.Selected = true;
                }
            }
        }

        /// <summary>
        /// 变更数据库重新获取数据表
        /// </summary>
        private void cmbDBList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dataBase = (this.cmbDBList.SelectedItem as DboBase).DboName;
            DBContext.ChangeDataBase(dataBase);
            BindTableList();
        }
    }
}
