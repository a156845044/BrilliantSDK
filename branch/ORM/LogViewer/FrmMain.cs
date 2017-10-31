using Brilliant.ORM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace LogViewer
{
    public partial class FrmMain : Form
    {
        private List<LogInfo> _logList;
        private LogInfo _selectLog;
        private StringBuilder sbParam = new StringBuilder();
        private string _filePath;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            BindType();
            BindLevel();
        }

        private void itemOpenLog_Click(object sender, EventArgs e)
        {
            if (dialogOpenFile.ShowDialog() == DialogResult.OK)
            {
                _filePath = dialogOpenFile.FileName;
                LoadFile();
            }
        }

        private void LoadFile()
        {
            if (String.IsNullOrEmpty(_filePath) || !File.Exists(_filePath))
            {
                return;
            }
            string log = String.Format("[{0}]", File.ReadAllText(_filePath));
            _logList = JsonSerializer.JSDeSerialize<List<LogInfo>>(log);
            BindData(_logList);
        }

        private void BindData(IEnumerable<LogInfo> list)
        {
            lvLogs.SelectedItems.Clear();
            lvLogs.Items.Clear();
            int i = 0;
            foreach (LogInfo log in list)
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

        private void BindType()
        {
            this.itemLogType.ComboBox.DataSource = Log.GetLogTypes();
            this.itemLogType.ComboBox.DisplayMember = "Text";
            this.itemLogType.ComboBox.ValueMember = "Value";
        }

        private void BindLevel()
        {
            this.itemLogLevel.ComboBox.DataSource = Log.GetLogLevels();
            this.itemLogLevel.ComboBox.DisplayMember = "Text";
            this.itemLogLevel.ComboBox.ValueMember = "Value";
        }

        private void lvLogs_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (_logList == null || _logList.Count <= 0)
            {
                return;
            }
            this._selectLog = _logList[e.ItemIndex];
            this.txtSQL.Text = _selectLog.CmdText;
            sbParam.Clear();
            if (_selectLog.Parameters != null)
            {
                foreach (var item in _selectLog.Parameters)
                {
                    sbParam.AppendFormat("参数：{0}\r\n", item.ParameterName);
                    sbParam.AppendFormat("    类型：{0}\r\n", item.DbType);
                    sbParam.AppendFormat("    长度：{0}\r\n", item.Size);
                    sbParam.AppendFormat("    值：{0}\r\n", item.Value);
                }
            }
            this.txtParams.Text = sbParam.ToString();
        }

        private void itemClear_Click(object sender, EventArgs e)
        {
            _logList = null;
            _selectLog = null;
            _filePath = null;
            lvLogs.SelectedItems.Clear();
            lvLogs.Items.Clear();
            txtParams.Clear();
            txtSQL.Clear();
        }

        private void itemFilter_Click(object sender, EventArgs e)
        {
            if (_logList == null || _logList.Count <= 0)
            {
                return;
            }
            EnumItem logType = this.itemLogType.SelectedItem as EnumItem;
            EnumItem logLevel = this.itemLogLevel.SelectedItem as EnumItem;
            if (logType.Value == "0" && logLevel.Value == "0")
            {
                BindData(_logList);
            }
            else if (logType.Value == "0" && logLevel.Value != "0")
            {
                var list = from item in _logList where item.LogLevel == logLevel.Text select item;
                BindData(list);
            }
            else if (logType.Value != "0" && logLevel.Value == "0")
            {
                var list = from item in _logList where item.LogType == logType.Text select item;
                BindData(list);
            }
            else
            {
                var list = from item in _logList where item.LogLevel == logLevel.Text && item.LogType == logType.Text select item;
                BindData(list);
            }
        }

        private void itemRefresh_Click(object sender, EventArgs e)
        {
            LoadFile();
        }
    }
}
