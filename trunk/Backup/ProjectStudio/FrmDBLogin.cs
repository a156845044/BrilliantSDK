using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Brilliant.Data;
using Microsoft.VisualStudio.Shell;

namespace Brilliant.ProjectStudio
{
    public partial class FrmDBLogin : Form
    {
        private readonly static FrmDBLogin instance = new FrmDBLogin(); //窗体实例
        private List<LoginInfo> loginInfos;
        private LoginInfo loginInfo;

        /// <summary>
        /// 窗体实例
        /// </summary>
        public static FrmDBLogin Instance
        {
            get { return FrmDBLogin.instance; }
        }

        /// <summary>
        /// 窗体构造器
        /// </summary>
        public FrmDBLogin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        private void FrmDBLogin_Load(object sender, EventArgs e)
        {
            BindProviderList();
            BindLoginInfo();
        }

        /// <summary>
        /// 绑定数据源类型列表
        /// </summary>
        private void BindProviderList()
        {
            this.cmbDataProvider.DataSource = ConfigMgr.GetProviders();
            this.cmbDataProvider.DisplayMember = "Name";
            this.cmbDataProvider.ValueMember = "ConnectionString";
        }

        /// <summary>
        /// 绑定登录信息
        /// </summary>
        private void BindLoginInfo()
        {
            loginInfos = ConfigMgr.GetLogins();
            this.cmbDataSource.DataSource = loginInfos;
            this.cmbDataSource.DisplayMember = "ServerName";
            this.cmbDataSource.ValueMember = "ServerType";
        }

        /// <summary>
        /// 点击确定
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            //登录信息实体
            LoginInfo loginInfo = new LoginInfo();
            loginInfo.ServerType = this.cmbDataProvider.Text;
            loginInfo.ServerName = this.cmbDataSource.Text;
            loginInfo.DefaultDB = this.cmbDataBase.Text;
            loginInfo.Uid = this.txtUid.Text;
            loginInfo.Pwd = this.txtPwd.Text;
            ProviderInfo info = this.cmbDataProvider.SelectedItem as ProviderInfo;

            Com.ClearOutput();
            Com.Output("------ 开始连接数据源: 目标: {0}, 对象: {1} ------", loginInfo.ServerName, loginInfo.DefaultDB);
            Com.Output("  {0} -> {1}", loginInfo.ServerName, loginInfo.ServerType);
            Com.Output("  {0} -> {1}", loginInfo.ServerName, loginInfo.DefaultDB);
            Com.Output("  {0} -> {1}", loginInfo.ServerName, loginInfo.Uid);
            //测试连接
            if (DBContext.Connect(loginInfo.ServerType, info.ProviderName, loginInfo.ServerName, loginInfo.DefaultDB, loginInfo.Uid, loginInfo.Pwd, info.ConnectionString))
            {
                FrmDBMgr.Instance.BindDbList();
                SaveLoginInfo(loginInfo); //保存登录信息
                this.Close();
            }
            else
            {
                MessageBox.Show("连接无效!");
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
        /// 选择数据源
        /// </summary>
        private void cmbDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            loginInfo = this.cmbDataSource.SelectedItem as LoginInfo;
            this.cmbDataBase.Text = loginInfo.DefaultDB;
            this.cmbDataProvider.Text = loginInfo.ServerType;
            this.txtUid.Text = loginInfo.Uid;
            this.txtPwd.Text = loginInfo.Pwd;
        }

        /// <summary>
        /// 保存登录信息
        /// </summary>
        private void SaveLoginInfo(LoginInfo loginInfo)
        {
            var result = (from item in loginInfos where item.ServerType == loginInfo.ServerType && item.ServerName == loginInfo.ServerName select item).ToList();
            if (result == null || result.Count <= 0)
            {
                loginInfos.Add(loginInfo);
                ConfigMgr.SetLogins(loginInfos);
            }
        }
    }
}
