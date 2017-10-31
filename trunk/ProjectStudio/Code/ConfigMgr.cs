using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Brilliant.ProjectStudio
{
    /// <summary>
    /// 配置管理
    /// </summary>
    public static class ConfigMgr
    {
        #region 成员变量
        private const string PROVIDER_FILENAME = "Provider.json";
        private const string TEMPLATE_FILENAME = "Template.json";
        private const string LOGIN_FILENAME = "Login.json";
        private static string configPath;
        private static string templatePath;
        #endregion

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string ConfigPath
        {
            get { return ConfigMgr.configPath; }
        }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ConfigMgr()
        {
            configPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ProjectStudio";
            if (!Directory.Exists(configPath))
            {
                Directory.CreateDirectory(configPath);
            }
            templatePath = configPath + "\\Template";
            if (!Directory.Exists(templatePath))
            {
                Directory.CreateDirectory(templatePath);
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="fileFullName">全路径</param>
        /// <param name="content">内容</param>
        public static void Save(string fileFullName, string content)
        {
            using (FileStream fs = new FileStream(fileFullName, FileMode.Create, FileAccess.Write, FileShare.Write, 1024, FileOptions.Asynchronous))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(content);
                IAsyncResult writeResult = fs.BeginWrite(buffer, 0, buffer.Length, (asyncResult) =>
                {
                    FileStream stream = (FileStream)asyncResult.AsyncState;
                    stream.EndWrite(asyncResult);
                },
                    fs);
                fs.Flush();
            }
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="fileFullName">全路径</param>
        /// <returns>配置</returns>
        public static T Load<T>(string fileFullName) where T : new()
        {
            if (!File.Exists(fileFullName))
            {
                return default(T);
            }
            string config = File.ReadAllText(fileFullName);
            return JsonSerializer.JSDeSerialize<T>(config);
        }

        /// <summary>
        /// 获取数据库驱动程序列表
        /// </summary>
        public static List<ProviderInfo> GetProviders()
        {
            string fullName = configPath + "\\" + PROVIDER_FILENAME;
            if (!File.Exists(fullName))
            {
                List<ProviderInfo> list = new List<ProviderInfo>();
                ProviderInfo p1 = new ProviderInfo();
                p1.Name = "SqlServer";
                p1.ProviderName = "Brilliant.Data.Provider.SqlServer";
                p1.ConnectionString = "Data Source={0};Database={1};uid={2};pwd={3}";
                list.Add(p1);

                ProviderInfo p2 = new ProviderInfo();
                p2.Name = "MySql";
                p2.ProviderName = "Brilliant.Data.Provider.MySql";
                p2.ConnectionString = "Data Source={0};Database={1};User Id={2};Password={3};charset=utf8;";
                list.Add(p2);

                ProviderInfo p3 = new ProviderInfo();
                p3.Name = "Oracle";
                p3.ProviderName = "Brilliant.Data.Provider.Oracle";
                p3.ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT=1521))(CONNECT_DATA=(SERVICE_NAME={1})));Persist Security Info=True;User ID={2};Password={3};";
                list.Add(p3);

                ProviderInfo p4 = new ProviderInfo();
                p4.Name = "SQLite";
                p4.ProviderName = "Brilliant.Data.Provider.SQLite";
                p4.ConnectionString = @"Data Source={0}\{1};New=False;Compress=True;Synchronous=Off;UTF8Encoding=True;Version=3;";
                list.Add(p4);
                string content = JsonSerializer.JSSerialize(list);
                Save(fullName, content);
                return list;
            }
            else
            {
                return Load<List<ProviderInfo>>(fullName);
            }
        }

        /// <summary>
        /// 保存数据库驱动程序列表
        /// </summary>
        public static void SetProviders(List<ProviderInfo> list)
        {
            string fullName = configPath + "\\" + PROVIDER_FILENAME;
            string content = JsonSerializer.JSSerialize(list);
            Save(fullName, content);
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        public static List<TemplateInfo> GetTemplates()
        {
            string fullName = configPath + "\\" + TEMPLATE_FILENAME;
            if (!File.Exists(fullName))
            {
                List<TemplateInfo> list = new List<TemplateInfo>();
                TemplateInfo t1 = new TemplateInfo();
                t1.Name = "ComEntity";
                t1.Path = String.Format("{0}\\{1}.tt", templatePath, t1.Name);
                t1.Output = ".cs";
                t1.FileType = "Entity";
                list.Add(t1);
                File.WriteAllText(t1.Path, ResTemplate.ComEntity);

                TemplateInfo t2 = new TemplateInfo();
                t2.Name = "ComBLL";
                t2.Path = String.Format("{0}\\{1}.tt", templatePath, t2.Name);
                t2.Output = ".cs";
                t2.FileType = "BizBase";
                list.Add(t2);
                File.WriteAllText(t2.Path, ResTemplate.ComBLL);

                TemplateInfo t3 = new TemplateInfo();
                t3.Name = "ComBLLExtend";
                t3.Path = String.Format("{0}\\{1}.tt", templatePath, t3.Name);
                t3.Output = ".cs";
                t3.FileType = "Biz";
                list.Add(t3);
                File.WriteAllText(t3.Path, ResTemplate.ComBLLExtend);

                TemplateInfo t4 = new TemplateInfo();
                t4.Name = "OAEntity";
                t4.Path = String.Format("{0}\\{1}.tt", templatePath, t4.Name);
                t4.Output = ".cs";
                t4.FileType = "Entity";
                list.Add(t4);
                File.WriteAllText(t4.Path, ResTemplate.OAEntity);

                TemplateInfo t5 = new TemplateInfo();
                t5.Name = "OABLL";
                t5.Path = String.Format("{0}\\{1}.tt", templatePath, t5.Name);
                t5.Output = ".cs";
                t5.FileType = "Biz";
                list.Add(t5);
                File.WriteAllText(t5.Path, ResTemplate.OABLL);

                TemplateInfo t6 = new TemplateInfo();
                t6.Name = "OABLLExtend";
                t6.Path = String.Format("{0}\\{1}.tt", templatePath, t6.Name);
                t6.Output = ".cs";
                t6.FileType = "Biz";
                list.Add(t6);
                File.WriteAllText(t6.Path, ResTemplate.OABLLExtend);

                string content = JsonSerializer.JSSerialize(list);
                Save(fullName, content);
                return list;
            }
            else
            {
                return Load<List<TemplateInfo>>(fullName);
            }
        }

        /// <summary>
        /// 获取登录信息列表
        /// </summary>
        public static List<LoginInfo> GetLogins()
        {
            string fullName = configPath + "\\" + LOGIN_FILENAME;
            if (!File.Exists(fullName))
            {
                List<LoginInfo> list = new List<LoginInfo>();
                LoginInfo info = new LoginInfo();
                info.DefaultDB = "master";
                info.Pwd = "123456";
                info.ServerName = "127.0.0.1";
                info.ServerType = "SqlServer";
                info.Uid = "sa";
                list.Add(info);
                string content = JsonSerializer.JSSerialize(list);
                Save(fullName, content);
                return list;
            }
            else
            {
                return Load<List<LoginInfo>>(fullName);
            }
        }

        /// <summary>
        /// 保存登录信息
        /// </summary>
        public static void SetLogins(List<LoginInfo> list)
        {
            string fullName = configPath + "\\" + LOGIN_FILENAME;
            string content = JsonSerializer.JSSerialize(list);
            Save(fullName, content);
        }
    }
}
