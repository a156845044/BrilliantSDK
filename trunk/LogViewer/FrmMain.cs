using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace LogViewer
{
    public partial class FrmMain : Form
    {
        private List<LogInfo> _logList;
        private LogInfo _selectLog;
        private StringBuilder sbParam = new StringBuilder();

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        private void itemOpenLog_Click(object sender, EventArgs e)
        {
            if (dialogOpenFile.ShowDialog() == DialogResult.OK)
            {
                string log = String.Format("[{0}]", File.ReadAllText(dialogOpenFile.FileName));
                _logList = JsonSerializer.JSDeSerialize<List<LogInfo>>(log);
                BindData();
            }
        }

        private void BindData()
        {
            lvLogs.SelectedItems.Clear();
            lvLogs.Items.Clear();
            int i = 0;
            foreach (LogInfo log in _logList)
            {
                ListViewItem item = new ListViewItem(new string[] { log.Date, log.LogType, log.LogLevel, log.Message });
                item.ToolTipText = log.Message;
                lvLogs.Items.Add(item);
                if (i == 0)
                {
                    item.Selected = true;
                }
                i++;
            }
        }

        private void lvLogs_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            this._selectLog = _logList[e.ItemIndex];
            this.txtSQL.Text = _selectLog.CmdText;
            if (_selectLog.Parameters != null)
            {
                sbParam.Clear();
                foreach (var item in _selectLog.Parameters)
                {
                    sbParam.AppendFormat("参数：{0}\r\n", item.ParameterName);
                    sbParam.AppendFormat("    类型：{0}\r\n", item.DbType);
                    sbParam.AppendFormat("    长度：{0}\r\n", item.Size);
                    sbParam.AppendFormat("    值：{0}\r\n", item.Value);
                }
                this.txtParams.Text = sbParam.ToString();
            }
        }
    }

    /// <summary>
    /// Json数据序列化/反序列化
    /// </summary>
    /// <remarks>作者：dfq 时间：2016-08-29 原因：每次要带个dll文件，太麻烦，故写在本类中,原调用Brlliant.Data.dll方法</remarks>
    public static class JsonSerializer
    {
        /// <summary>
        /// 将对象/对象集合转换成Json数据
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json数据</returns>
        public static string Serialize(object obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Encoding.UTF8.GetString(stream.ToArray());
                return szJson;
            }
        }

        /// <summary>
        /// 将Json数据转换成对象/对象集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">Json数据</param>
        /// <returns>对象/对象集合</returns>
        public static T DeSerialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// 将对象/对象集合转换成Json数据（JavaScriptSerializer）
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json数据</returns>
        public static string JSSerialize(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }

        /// <summary>
        /// 将Json数据转换成对象/对象集合（JavaScriptSerializer）
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">Json数据</param>
        /// <returns>对象/对象集合</returns>
        public static T JSDeSerialize<T>(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }
    }

    /// <summary>
    /// 日志信息
    /// </summary>
    /// <remarks>作者：dfq 时间：2016-08-29 原因：每次要带个dll文件，太麻烦，故写在本类中,原调用Brlliant.Data.dll方法</remarks>
    public class LogInfo
    {
        public LogInfo()
        {
            this.Parameters = new List<LogCmdParam>();
        }

        /// <summary>
        /// 日期
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string LogLevel { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string LogType { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 查询指令
        /// </summary>
        public string CmdText { get; set; }

        /// <summary>
        /// 查询参数列表
        /// </summary>
        public List<LogCmdParam> Parameters { get; set; }

    }

    /// <summary>
    /// SQL查询参数
    /// </summary>
    /// <remarks>作者：dfq 时间：2016-08-29 原因：每次要带个dll文件，太麻烦，故写在本类中,原调用Brlliant.Data.dll方法</remarks>
    public class LogCmdParam
    {
        public string ParameterName { get; set; }

        public string Size { get; set; }

        public string Value { get; set; }

        public string DbType { get; set; }
    }
}
