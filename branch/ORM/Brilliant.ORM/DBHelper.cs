using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using System.Data;
using System.IO;
using System.Security.Cryptography;

namespace Brilliant.ORM
{
    /// <summary>
    /// DBProvider一个静态实例
    /// </summary>
    public class DBHelper
    {
        #region 成员变量
        //private static int _identity = 0;
        private static readonly Dictionary<string, ConnectionInfo> pool = new Dictionary<string, ConnectionInfo>();//先定义否则下面无法调用
        private static readonly DBHelper instance = new DBHelper();
        private ConnectionInfo currentConnection;
        #endregion

        #region 属性
        /// <summary>
        /// 当前连接
        /// </summary>
        public static ConnectionInfo CurrentConnection
        {
            get { return instance.currentConnection; }
        }

        /// <summary>
        /// 数据访问对象实例
        /// </summary>
        public static IDataProvider DataProvider
        {
            get { return CurrentConnection.DataProvider; }
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
        private DBHelper()
        {
            string sectionName = "default";
            AddConnection(sectionName);
        }
        #endregion

        #region 方法
        /// <summary>
        /// 开始事务
        /// </summary>
        public static void BeginTransaction()
        {
            DataProvider.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public static void Commit()
        {
            DataProvider.Commit();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public static void Rollback()
        {
            DataProvider.Rollback();
        }

        /// <summary>
        /// 执行事务操作
        /// </summary>
        /// <param name="handler">需要调用事务方法的句柄</param>
        /// <returns>执行结果</returns>
        public static int Transaction(TransDelegate handler)
        {
            int count = 0;
            SQL currSQL = null;
            Exception transError = new Exception("事物处理发生异常。");
            //创建Provider实例
            DataProviderBase provider = GetDataProvider() as DataProviderBase;
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
                handler(provider, transError);
                //提交事务
                transaction.Commit();
            }
            catch (Exception ex)
            {
                //回滚事务
                transaction.Rollback();
                //重置返回值为0
                count = 0;
                //记录日志
                Log.Instance.Add(LogType.Execute, ex.Message, currSQL);
                //抛出异常
               // throw ex;//dfq 2016-08-29 注释，原因：手动结束事务时会抛异常，页面上显示不友好。
            }
            finally
            {
                //取消事件订阅
                provider.TransExecute -= ted;
                //释放事务对象
                transaction.Dispose();
                //销毁对象
                currSQL = null;
                provider = null;
            }
            return count;
        }

        /// <summary>
        /// 返回一个新的DBHelper对象实例
        /// </summary>
        /// <returns>DBHelper对象实例</returns>
        [Obsolete("该方法已过时，如果调用可能会产生未知异常")]
        public static DBHelper CreateInstance()
        {
            return new DBHelper();
        }

        /// <summary>
        /// 返回一个新的DataProvider对象实例
        /// </summary>
        /// <returns>DataProvider对象实例</returns>
        public static IDataProvider GetDataProvider()
        {
            return DataProvider.GetDataProvider();
        }

        /// <summary>
        /// 连接到服务器
        /// </summary>
        /// <param name="connInfo">连接信息</param>
        /// <returns>true:连接成功 false:连接失败</returns>
        public static bool Connect(ConnectionInfo connInfo)
        {
            object obj = instance.CreateProvider(connInfo.ProviderName);
            connInfo.DataProvider = obj as IDataProvider;
            //connInfo.SchemaProvider = obj as ISchemaProvider;
            return instance.AddConnection(connInfo);
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
                DataProvider.ChangeConnectionString(instance.currentConnection, true);
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
                DataProvider.ChangeConnectionString(instance.currentConnection, true);
            }
        }

        /// <summary>
        /// 变更连接字符串节点
        /// </summary>
        /// <param name="sectionName">节点名称</param>
        public static void ChangeSection(string sectionName)
        {
            instance.AddConnection(sectionName);
            DataProvider.ChangeConnectionString(CurrentConnection, false);
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
                string namePrefix = "Brilliant.ORM.";
                string className = providerName;
                string assemblyPath = String.Empty;
                if (providerName.Contains(namePrefix))
                {
                    className = providerName.Replace(namePrefix, "");
                }
                else
                {
                    providerName = namePrefix + providerName;
                }
                if (providerName != typeof(SqlServer).FullName && providerName != typeof(Odbc).FullName && providerName != typeof(OleDb).FullName)
                {
                    assemblyPath = String.Format("Brilliant.DataProvider.{0}.dll", className);
                    if (!File.Exists(assemblyPath))
                    {
                        assemblyPath = String.Format(@"{0}\bin\Brilliant.DataProvider.{1}.dll", AppDomain.CurrentDomain.BaseDirectory, className);
                        if (!File.Exists(assemblyPath))
                        {
                            assemblyPath = String.Format(@"{0}\Brilliant.DataProvider.{1}.dll", AppDomain.CurrentDomain.BaseDirectory, className);
                            if (!File.Exists(assemblyPath))
                            {
                                Log.Instance.Add("加载外部程插件序集时出错.");
                                throw new Exception("未找到目标程序集.");
                            }
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

            //Type type = null;
            //if (!String.IsNullOrEmpty(providerName))
            //{
            //    string namePrefix = "Brilliant.Data.Provider.";
            //    if (!providerName.Contains(namePrefix))
            //    {
            //        providerName = namePrefix + providerName;
            //    }
            //    string assemblyPath = String.Empty;
            //    if (providerName != typeof(SqlServer).FullName && providerName != typeof(Odbc).FullName && providerName != typeof(OleDb).FullName)
            //    {
            //        assemblyPath = String.Format("{0}.dll", providerName);
            //        if (!File.Exists(assemblyPath))
            //        {
            //            assemblyPath = String.Format(@"{0}\bin\{1}.dll", AppDomain.CurrentDomain.BaseDirectory, providerName);
            //            if (!File.Exists(assemblyPath))
            //            {
            //                throw new Exception(String.Format("目标文件\"{0}\"不存在!", assemblyPath));
            //            }
            //        }
            //        Assembly assembly = Assembly.LoadFrom(assemblyPath);
            //        type = assembly.GetType(providerName);
            //    }
            //    else
            //    {
            //        type = Type.GetType(providerName, true);
            //    }
            //}
            //else
            //{
            //    type = typeof(SqlServer);
            //}
            //return Activator.CreateInstance(type);
        }

        /// <summary>
        /// 添加连接
        /// </summary>
        /// <param name="sectionName">节点名称</param>
        private void AddConnection(string sectionName)
        {
            ConnectionInfo connInfo = new ConnectionInfo();
            connInfo.ConnectionString = ConfigurationManager.ConnectionStrings[sectionName].ConnectionString;
            connInfo.ProviderName = ConfigurationManager.ConnectionStrings[sectionName].ProviderName;
            object obj = CreateProvider(connInfo.ProviderName);
            connInfo.DataProvider = obj as IDataProvider;
            //connInfo.SchemaProvider = obj as ISchemaProvider;
            AddConnection(connInfo);
        }

        /// <summary>
        /// 添加连接
        /// </summary>
        /// <param name="connInfo">连接字符串</param>
        /// <returns>true:添加成功 false:添加失败</returns>
        private bool AddConnection(ConnectionInfo connInfo)
        {
            if (connInfo.DataProvider.CheckConnection(connInfo))
            {
                if (pool.ContainsKey(connInfo.DataSource))
                {
                    pool[connInfo.DataSource] = connInfo;
                }
                pool.Add(connInfo.DataSource, connInfo);
                this.currentConnection = connInfo;
                return true;
            }
            return false;
        }

        ///// <summary>
        ///// 获取主键编号(长度21位)
        ///// </summary>
        ///// <returns>主键编号</returns>
        //public static string GetPrimaryKey()
        //{
        //    if (_identity >= 10000) { _identity = 0; }
        //    string id = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _identity.ToString().PadLeft(4, '0');
        //    _identity++;
        //    return id;
        //}
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
