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
using System.Threading;

namespace Brilliant.ProjectStudio
{
    public partial class FrmCompare : Form
    {
        private delegate void CallBack(string name, string value);
        private delegate void Finish(int value);

        /// <summary>
        /// 窗体构造
        /// </summary>
        public FrmCompare()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        private void FrmCompare_Load(object sender, EventArgs e)
        {
            BindDBList();
        }

        /// <summary>
        /// 绑定数据库列表
        /// </summary>
        private void BindDBList()
        {
            this.lvwDB.SmallImageList = ResManager.SysImageList;
            IList<DboBase> list = DBContext.CurrentConnection.SchemaProvider.GetDbList();
            this.lvwDB.Items.Clear();
            foreach (DboBase table in list)
            {
                ListViewItem item = new ListViewItem();
                item.Text = table.DboName;
                item.ImageIndex = ResManager.SysImageList.Images.IndexOfKey(ResImageName.DbmTable);
                this.lvwDB.Items.Add(item);
            }
            if (this.lvwDB.Items.Count > 0)
            {
                this.lvwDB.Items[0].Selected = true;
            }
        }

        /// <summary>
        /// 点击比较
        /// </summary>
        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (lstDB.Items.Count > 2)
            {
                MessageBox.Show("目前暂不支持两个以上的数据库进行比较.");
                return;
            }
            if (lstDB.Items.Count != 2)
            {
                MessageBox.Show("比较对象不能为空.");
                return;
            }
            string leftName = lstDB.Items[0].ToString();
            string rightName = lstDB.Items[1].ToString();
            // EnvDTE.Window win = Com.VS.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            EnvDTE.Window win = Com.VS.Windows.Item(EnvDTE.Constants.vsWindowKindOutput);
            win.Activate();
            Com.ClearOutput();
            Com.Output("-------------- 开始对比数据源: 对象1: {0}, 对象2: {1} --------------", leftName, rightName);

            Thread thread = new Thread(new ThreadStart(() =>
            {
                string oriDataBase = DBContext.CurrentConnection.DataBase;
                Dictionary<string, IList<DboTable>> dbTables = new Dictionary<string, IList<DboTable>>();
                foreach (object obj in lstDB.Items)
                {
                    string dataBase = obj.ToString();
                    DBContext.ChangeDataBase(dataBase);
                    IList<DboTable> tables = DBContext.CurrentConnection.SchemaProvider.GetTableList();
                    dbTables.Add(dataBase, tables);
                }

                List<List<string>> list = new List<List<string>>();
                foreach (var item in dbTables)
                {
                    DBContext.ChangeDataBase(item.Key);
                    IList<DboTable> tables = item.Value;
                    List<string> valueList = new List<string>();
                    foreach (DboTable table in tables)
                    {
                        IList<SchemaColumn> columns = DBContext.CurrentConnection.SchemaProvider.GetColumn(table.DboName);
                        foreach (SchemaColumn column in columns)
                        {
                            string str = String.Format("{0}:{1} {2} {3} {4} {5}", table.DboName, column.ColumnName, column.ColumnType, column.ColumnLength, column.ColumnDefaultValue, column.ColumnDesc);
                            valueList.Add(str);
                        }
                    }
                    list.Add(valueList);
                }
                List<string> leftDifs = GetDifValues(list[0], list[1]);
                List<string> rightDifs = GetDifValues(list[1], list[0]);
                CallBack callBack = new CallBack(CallBackFun);
                foreach (string str in leftDifs)
                {
                    this.BeginInvoke(callBack, new object[] { leftName, str });
                }
                foreach (string str in rightDifs)
                {
                    this.BeginInvoke(callBack, new object[] { rightName, str });
                }
                this.BeginInvoke(new Finish((value) =>
                {
                    DBContext.ChangeDataBase(oriDataBase);
                    Com.Output("================ 比对: 成功，一共 {0} 处不同，失败 0 个，跳过 0 个 ================", value);
                    this.Close();
                }), new object[] { leftDifs.Count + rightDifs.Count });
            }));
            thread.Start();
        }

        /// <summary>
        /// 回调
        /// </summary>
        private void CallBackFun(string name, string value)
        {
            Com.Output("  {0} -> {1}", name, value);
        }

        /// <summary>
        /// 点击取消
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 选择数据库
        /// </summary>
        private void lvwDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstDB.Items.Clear();
            foreach (ListViewItem item in this.lvwDB.SelectedItems)
            {
                //item.Selected = true;
                lstDB.Items.Add(item.Text);
            }
        }

        /// <summary>
        /// 全选（如果只比较两个，全选则无用）
        /// </summary>
        private void lvwDB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers.CompareTo(Keys.Control) == 0 && e.KeyCode == Keys.A)
            {
                foreach (ListViewItem item in this.lvwDB.Items)
                {
                    item.Selected = true;
                }
            }
        }

        /// <summary>
        /// 获取不同
        /// </summary>
        private List<string> GetDifValues(List<string> leftValues, List<string> rightValues)
        {
            List<string> difTables = new List<string>();
            bool exist = false;
            foreach (string left in leftValues)
            {
                exist = false;
                foreach (string right in rightValues)
                {
                    if (left == right)
                    {
                        exist = true;
                    }
                }
                if (exist == false)
                {
                    difTables.Add(left);
                }
            }
            return difTables;
        }
    }
}
