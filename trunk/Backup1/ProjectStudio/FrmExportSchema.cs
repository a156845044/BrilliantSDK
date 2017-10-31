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

namespace Brilliant.ProjectStudio
{
    public partial class FrmExportSchema : Form
    {
        private string tableName;

        /// <summary>
        /// 构造器
        /// </summary>
        public FrmExportSchema()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public FrmExportSchema(string tableName)
            : this()
        {
            this.tableName = tableName;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        private void FrmExportSchema_Load(object sender, EventArgs e)
        {
            BindDBList();
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
            this.cmbDBList.Text = tableName;
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
        /// 变更数据库
        /// </summary>
        private void cmbDBList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string dataBase = (this.cmbDBList.SelectedItem as DboBase).DboName;
            DBContext.ChangeDataBase(dataBase);
            BindTableList();
        }

        /// <summary>
        /// 点击导出
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            IList<string> list = new List<string>();
            foreach (ListViewItem item in this.lvwDBTable.SelectedItems)
            {
                list.Add(item.Text);
            }
            if (list.Count == 0)
            {
                MessageBox.Show("请选择需要导出的表");
                return;
            }
            DiaExportSchemaProgress diaExportSchemaProgress = new DiaExportSchemaProgress(list);
            if (diaExportSchemaProgress.ShowDialog() == DialogResult.OK)
            {
                this.Close();
            }
        }

        /// <summary>
        /// 点击取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
