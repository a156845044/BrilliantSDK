using Brilliant.Data.Common;
using Brilliant.Data.Provider;
using Brilliant.Data.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Brilliant.Data
{
    /// <summary>
    /// DBHelper
    /// </summary>
    public class DBContext
    {
        #region 成员变量
        private static int _identity = 0;
        private static readonly Dictionary<string, ConnectionInfo> pool = new Dictionary<string, ConnectionInfo>();
        private static readonly DBContext instance = new DBContext();
        private Dictionary<string, ConnectionStringSettings> settings;
        private ConnectionInfo currentConnection;
        private string currentKey;

        #endregion

        #region 属性
        /// <summary>
        /// 当前配置文件中所配置连接字符串名称
        /// </summary>
        public static string[] ConnectionStringNames
        {
            get
            {
                return instance.settings.Keys.ToArray();
            }
        }

        /// <summary>
        /// 当前连接
        /// </summary>
        public static ConnectionInfo CurrentConnection
        {
            get { return instance.currentConnection; }
        }

        /// <summary>
        /// 当前连接的键
        /// </summary>
        public static string CurrentKey
        {
            get { return instance.currentKey; }
        }

        /// <summary>
        /// 数据访问对象实例
        /// </summary>
        public static IDataProvider DataProvider
        {
            get { return CurrentConnection.DataProvider; }
        }

        /// <summary>
        /// 数据表架构访问对象实例
        /// </summary>
        public static ISchemaProvider SchemaProvider
        {
            get { return CurrentConnection.SchemaProvider; }
        }

        /// <summary>
        /// 数据库分页方式
        /// </summary>
        public static PagedType DataPagedType
        {
            get { return DataProvider.DataPagedType; }
        }

        /// <summary>
        /// 返回一个新的DataParameter对象实例
        /// </summary>
        /// <returns>DataParameter对象实例</returns>
        public static IDbDataParameter GetParameter()
        {
            return DataProvider.GetParameter();
        }
        #endregion

        #region 构造器
        /// <summary>
        /// 构造器
        /// </summary>
        private DBContext()
        {
            LoadSettings();
            string defaultSectionName = "default";
            AddConnection(defaultSectionName);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 执行事务操作
        /// </summary>
        /// <param name="handler">需要调用事务方法的句柄</param>
        /// <returns>执行结果</returns>
        public static int Transaction(TransDelegate handler)
        {
            int count = 0;
            SQL currSQL = null;
            //创建Provider实例
            DataProviderBase provider = DataProvider.GetDataProvider() as DataProviderBase;
            //创建事务对象实例
            DataProviderBase.Transaction transaction = new DataProviderBase.Transaction(provider);
            //事务执行方法委托
            TransExecuteDelegate ted = new TransExecuteDelegate((sql) =>
            {
                currSQL = sql;
                int result = transaction.Execute(sql);
                count += result;
                return result;
            });
            try
            {
                //事件挂载
                provider.TransExecute += ted;
                //开始事务
                transaction.BeginTransaction();
                //执行需要调用事务的方法
                handler();
                //提交事务
                transaction.Commit();
            }
            catch (Exception ex)
            {
                //回滚事务
                transaction.Rollback();
                //记录日志
                Log.Instance.Add(LogType.Execute, ex.Message, currSQL);
                //抛出异常
                throw ex;
            }
            finally
            {
                //取消事件订阅
                provider.TransExecute -= ted;
                //释放对象
                transaction.Dispose();
                currSQL = null;
                provider = null;
            }
            return count;
        }

        /// <summary>
        /// 连接到数据源
        /// </summary>
        /// <param name="providerType">数据源类型</param>
        /// <param name="dataSource">服务器地址</param>
        /// <param name="dataBase">默认数据库名称</param>
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>true:连接成功 false:连接失败</returns>
        public static bool Connect(string providerType, string dataSource, string dataBase, string uid, string pwd)
        {
            return instance.AddConnection(providerType, dataSource, dataBase, uid, pwd);
        }

        /// <summary>
        /// 链接到数据源
        /// </summary>
        /// <param name="providerType">数据源类型</param>
        /// <param name="providerName">驱动程序名称</param>
        /// <param name="dataSource">服务器地址</param>
        /// <param name="dataBase">默认数据库名称</param>
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="connectionStringFormat">连接字符串格式</param>
        /// <returns>true:连接成功 false:连接失败</returns>
        public static bool Connect(string providerType, string providerName, string dataSource, string dataBase, string uid, string pwd, string connectionStringFormat)
        {
            return instance.AddConnection(providerType, providerName, dataSource, dataBase, uid, pwd, connectionStringFormat);
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="dataSource">数据源</param>
        public static void Disconnect(string dataSource)
        {
            if (pool.ContainsKey(dataSource))
            {
                pool.Remove(dataSource);
                if (pool.Count <= 0)
                {
                    instance.currentConnection = null;
                }
            }
        }

        /// <summary>
        /// 变更数据源
        /// </summary>
        /// <param name="dataSource">数据源</param>
        public static void ChangeDataSource(string dataSource)
        {
            if (pool.ContainsKey(dataSource))
            {
                instance.currentConnection = pool[dataSource];
                DataProvider.ChangeConnectionString(CurrentConnection.ConnectionString);
            }
        }

        /// <summary>
        /// 变更数据库
        /// </summary>
        /// <param name="dataBase">数据库</param>
        public static void ChangeDataBase(string dataBase)
        {
            if (CurrentConnection != null)
            {
                CurrentConnection.DataBase = dataBase;
                CurrentConnection.ConnectionString = String.Empty;
                DataProvider.ChangeConnectionString(CurrentConnection.ConnectionString);
            }
        }

        /// <summary>
        /// 变更连接字符串节点
        /// </summary>
        /// <param name="sectionName">节点名称</param>
        public static void ChangeSection(string sectionName)
        {
            instance.AddConnection(sectionName);
            DataProvider.ChangeConnectionString(CurrentConnection.ConnectionString);
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        private void LoadSettings()
        {
            settings = new Dictionary<string, ConnectionStringSettings>();
            foreach (ConnectionStringSettings item in ConfigurationManager.ConnectionStrings)
            {
                string name = item.Name;
                if (name.ToLower().Contains("local"))
                {
                    continue;
                }
                if (!settings.ContainsKey(item.Name))
                {
                    settings.Add(item.Name, item);
                }
            }
        }

        /// <summary>
        /// 创建Provider对象
        /// </summary>
        /// <param name="providerName">provider名称</param>
        /// <returns>Provider对象</returns>
        private object CreateProvider(string providerName)
        {
            Type type = null;
            if (!String.IsNullOrEmpty(providerName))
            {
                string namePrefix = "Brilliant.Data.Provider.";
                if (!providerName.Contains(namePrefix))
                {
                    providerName = namePrefix + providerName;
                }
                string assemblyPath = String.Empty;
                if (providerName != typeof(SqlServer).FullName && providerName != typeof(Odbc).FullName && providerName != typeof(OleDb).FullName)
                {
                    assemblyPath = String.Format("{0}.dll", providerName);
                    if (!File.Exists(assemblyPath))
                    {
                        assemblyPath = String.Format(@"{0}\bin\{1}.dll", AppDomain.CurrentDomain.BaseDirectory, providerName);
                        if (!File.Exists(assemblyPath))
                        {
                            Log.Instance.Add("CreateProvider加载目标程序集时找不到该文件。目标程序集完整路径：" + assemblyPath);
                            throw new Exception(String.Format("目标文件\"{0}\"不存在!", assemblyPath));
                        }
                    }
                    Assembly assembly = Assembly.LoadFrom(assemblyPath);
                    type = assembly.GetType(providerName);
                }
                else
                {
                    type = Type.GetType(providerName, true);
                }
            }
            else
            {
                type = typeof(SqlServer);
            }
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// 添加连接
        /// </summary>
        /// <param name="providerType">数据源类型</param>
        /// <returns>true:连接成功 false:连接失败</returns>
        private bool AddConnection(string providerType)
        {
            if (!settings.ContainsKey(providerType))
            {
                return false;
            }
            ConnectionInfo connInfo = new ConnectionInfo();
            connInfo.ProviderType = providerType;
            connInfo.ProviderName = ConfigurationManager.ConnectionStrings[providerType].ProviderName;
            connInfo.ConnectionString = ConfigurationManager.ConnectionStrings[providerType].ConnectionString;
            object obj = this.CreateProvider(connInfo.ProviderName);
            connInfo.DataProvider = obj as IDataProvider;
            connInfo.SchemaProvider = obj as ISchemaProvider;
            return this.AddConnection(connInfo);
        }

        /// <summary>
        /// 连接到数据源
        /// </summary>
        /// <param name="providerType">数据源类型</param>
        /// <param name="dataSource">服务器地址</param>
        /// <param name="dataBase">默认数据库名称</param>
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>true:添加成功 false:添加失败</returns>
        private bool AddConnection(string providerType, string dataSource, string dataBase, string uid, string pwd)
        {
            if (!this.settings.ContainsKey(providerType))
            {
                return false;
            }
            string providerName = ConfigurationManager.ConnectionStrings[providerType].ProviderName;
            string connectionStringFormat = ConfigurationManager.ConnectionStrings[providerType].ConnectionString;
            return this.AddConnection(providerType, providerName, dataSource, dataBase, uid, pwd, connectionStringFormat);
        }

        /// <summary>
        /// 连接到数据源
        /// </summary>
        /// <param name="providerType"></param>
        /// <param name="providerName"></param>
        /// <param name="dataSource"></param>
        /// <param name="dataBase"></param>
        /// <param name="uid"></param>
        /// <param name="pwd"></param>
        /// <param name="connectionStringFormat"></param>
        /// <returns></returns>
        private bool AddConnection(string providerType, string providerName, string dataSource, string dataBase, string uid, string pwd, string connectionStringFormat)
        {
            ConnectionInfo connInfo = new ConnectionInfo();
            connInfo.ProviderType = providerType;
            connInfo.ProviderName = providerName;
            connInfo.ConnectionStringFormat = connectionStringFormat;
            connInfo.DataBase = dataBase;
            connInfo.DataSource = dataSource;
            connInfo.Uid = uid;
            connInfo.Pwd = pwd;
            object obj = this.CreateProvider(connInfo.ProviderName);
            connInfo.DataProvider = obj as IDataProvider;
            connInfo.SchemaProvider = obj as ISchemaProvider;
            return this.AddConnection(connInfo);
        }

        /// <summary>
        /// 添加连接
        /// </summary>
        /// <param name="connInfo">连接字符串</param>
        /// <returns>true:添加成功 false:添加失败</returns>
        private bool AddConnection(ConnectionInfo connInfo)
        {
            string key = String.Format("[{0}]{1}", connInfo.ProviderType, connInfo.DataSource);
            if (connInfo.DataProvider.CheckConnection(connInfo))
            {
                if (pool.ContainsKey(key))
                {
                    pool[key] = connInfo;
                }
                else
                {
                    pool.Add(key, connInfo);
                }
                this.currentConnection = connInfo;
                this.currentKey = key;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 获取主键编号(长度21位)
        /// </summary>
        /// <returns>主键编号</returns>
        public static string GetPrimaryKey()
        {
            if (_identity >= 10000) { _identity = 0; }
            string id = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _identity.ToString().PadLeft(4, '0');
            _identity++;
            return id;
        }

        /// <summary>
        /// 加密字符串(MD5算法)
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string str)
        {
            MD5 m = new MD5CryptoServiceProvider();
            byte[] s = m.ComputeHash(UnicodeEncoding.UTF8.GetBytes(str));
            return BitConverter.ToString(s).Replace("-", "");
        }
        #endregion
    }

    /// <summary>
    /// 分页类型枚举
    /// </summary>
    public enum PagedType
    {
        /// <summary>
        /// 通用数据库分页
        /// </summary>
        Common,

        /// <summary>
        /// 用于Oracle数据库分页
        /// </summary>
        RowId,

        /// <summary>
        /// 用于SQL Server2005以上数据库分页
        /// </summary>
        RowNumber,

        /// <summary>
        /// 用于MySql以及SQLite数据库分页
        /// </summary>
        Limit
    }
}
