using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Brilliant.Data;
using Brilliant.Data.Common;

namespace Brilliant.ProjectStudio
{
    public partial class DiaExportSchemaProgress : Form
    {
        private Export export = new Export();
        private IList<string> selectedItems;
        private delegate void CallBackDelegate(int value, string msg);

        /// <summary>
        /// 构造器
        /// </summary>
        public DiaExportSchemaProgress()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="selectedItems"></param>
        public DiaExportSchemaProgress(IList<string> selectedItems)
            : this()
        {
            this.selectedItems = selectedItems;
            this.progressBar.Maximum = selectedItems.Count;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        private void DiaExportSchemaProgress_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(this.ExportToWord));
            thread.Start();
        }

        /// <summary>
        /// 导出表结构
        /// </summary>
        private void ExportToWord()
        {
            export.AddBasicInfo(DBContext.CurrentConnection.DataBase);
            int i = 1;
            foreach (string tableName in selectedItems)
            {
                SchemaTable model = new SchemaTable();
                model.TableName = tableName;
                model.TableDesc = DBContext.CurrentConnection.SchemaProvider.GetTableDesc(tableName);
                model.ColumnList = DBContext.CurrentConnection.SchemaProvider.GetColumn(tableName);
                export.ExportSchemaInfo(i, model);
                CallBackDelegate callBackDelegate = new CallBackDelegate(CallBack);
                this.BeginInvoke(callBackDelegate, new object[] { i, String.Format("正在导出表:{0}", tableName) });
                i++;
            }
        }

        /// <summary>
        /// 事件回调
        /// </summary>
        private void CallBack(int value, string msg)
        {
            this.progressBar.Value = value;
            this.lblMsg.Text = msg;
            this.lblMsg.Refresh();
            if (value == this.progressBar.Maximum)
            {
                export.ShowWord();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
