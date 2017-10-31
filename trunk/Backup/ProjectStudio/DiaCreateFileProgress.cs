using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Brilliant.Data.Common;
using System.IO;
using Brilliant.Data;

namespace Brilliant.ProjectStudio
{
    public partial class DiaCreateFileProgress : Form
    {
        #region 成员变量
        private string templatePath;
        private string templateType;
        private string output;
        private string nameSpace;
        private string projPath;
        private IList<string> selectedItems;
        private delegate void CallBackDelegate(int value, string msg, string filePath);
        private CodeGenerator codeGenerator = new CodeGenerator();
        private bool open;
        #endregion

        /// <summary>
        /// 构造器
        /// </summary>
        private DiaCreateFileProgress()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public DiaCreateFileProgress(string nameSpace, string projPath, IList<string> selectedItems, bool open, TemplateInfo template)
            : this()
        {
            this.templatePath = template.Path;
            this.templateType = template.FileType;
            this.output = template.Output;
            this.nameSpace = nameSpace;
            this.projPath = projPath;
            this.selectedItems = selectedItems;
            this.progressBar.Maximum = selectedItems.Count;
            this.open = open;
        }

        /// <summary>
        /// 加载时启动线程
        /// </summary>
        private void DiaCreateFileProgress_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(this.CreateCodeFile));
            thread.Start();
        }

        /// <summary>
        /// 事件回调
        /// </summary>
        private void CallBack(int value, string msg, string filePath)
        {
            //添加文件到当前项目工程
            Com.ActiveProject.ProjectItems.AddFromFile(filePath);
            this.progressBar.Value = value;
            this.lblFileInfo.Text = msg;
            this.lblFileInfo.Refresh();
            //是否打开文件
            if (open)
            {
            }
            if (value == this.progressBar.Maximum)
            {
                codeGenerator.Close();//关闭引擎(会释放内存资源)
                this.Close();
            }
        }

        /// <summary>
        /// 创建代码
        /// </summary>
        private void CreateCodeFile()
        {
            int i = 1;
            string fileName = String.Empty;
            string destPath = String.Empty;
            string code = String.Empty;
            foreach (string tableName in selectedItems)
            {
                //根据表名称获取表结构信息
                SchemaTable table = DBContext.SchemaProvider.GetSchemaInfo(tableName);
                table.TableNameSpace = nameSpace;
                fileName = String.Format("{0}{1}{2}", table.TableName, templateType, output);
                destPath = String.Format("{0}\\{1}", projPath, fileName);
                //使用T4引擎生成代码
                code = codeGenerator.Generate(templatePath, table);
                File.WriteAllText(destPath, code, Encoding.UTF8);
                CallBackDelegate callBackDelegate = new CallBackDelegate(CallBack);
                this.BeginInvoke(callBackDelegate, new object[] { i, String.Format("正在创建文件:{0}", fileName), destPath });
                i++;
            }
        }
    }
}
