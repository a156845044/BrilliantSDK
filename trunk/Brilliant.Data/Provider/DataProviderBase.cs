using Brilliant.Data.Common;
using Brilliant.Data.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;

namespace Brilliant.Data.Provider
{
    /// <summary>
    /// 事务方法委托
    /// </summary>
    public delegate void TransDelegate();

    /// <summary>
    /// 事务执行委托
    /// </summary>
    /// <param name="sql">执行参数</param>
    /// <returns>执行结果</returns>
    public delegate int TransExecuteDelegate(SQL sql);

    /// <summary>
    /// DataProvider基类
    /// </summary>
    public abstract class DataProviderBase : IDataProvider
    {
        #region 事件
        /// <summary>
        /// 事务执行事件
        /// </summary>
        public event TransExecuteDelegate TransExecute;
        #endregion

        #region 成员变量
        protected ConnectionInfo connInfo;
        private string connectionString;
        #endregion

        #region 属性
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return this.connectionString;
            }
        }

        /// <summary>
        /// 获取当前分页类型
        /// </summary>
        public abstract PagedType DataPagedType
        {
            get;
        }
        #endregion

        #region 构造器
        /// <summary>
        /// 构造器
        /// </summary>
        public DataProviderBase()
        {

        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public DataProviderBase(string connectionString)
        {
            this.connectionString = connectionString;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 变更连接字符串
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public void ChangeConnectionString(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// 检测连接是否可用
        /// </summary>
        /// <param name="connInfo">连接信息</param>
        /// <returns>true:可用 false:不可用</returns>
        public bool CheckConnection(ConnectionInfo connInfo)
        {
            if (String.IsNullOrEmpty(connInfo.ConnectionString))
            {
                return false;
            }
            this.connectionString = connInfo.ConnectionString;
            using (DbConnection conn = GetConnection())
            {
                try
                {
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    connInfo.DataSource = String.IsNullOrEmpty(connInfo.DataSource) ? conn.DataSource : connInfo.DataSource;
                    connInfo.DataBase = String.IsNullOrEmpty(connInfo.DataBase) ? conn.Database : connInfo.DataBase;
                    this.connInfo = connInfo;
                    return true;
                }
                catch (Exception ex)
                {
                    Log.Instance.Add(ex.Message);
                    return false;
                }
            }
        }

        /// <summary>
        /// 返回Command对象
        /// </summary>
        private DbCommand GetCommand(DbConnection conn, SQL sql)
        {
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            DbCommand cmd = GetCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql.CmdText;
            cmd.CommandType = sql.CmdType;
            if (sql.Parameters != null)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(sql.Parameters);
            }
            return cmd;
        }

        /// <summary>
        /// 执行查询指令返回DataSet对象
        /// </summary>
        /// <param name="sql">查询指令</param>
        /// <returns>DataSet对象</returns>
        public DataSet ExecDataSet(SQL sql)
        {
            try
            {
                using (DbConnection conn = GetConnection())
                {
                    DbCommand cmd = GetCommand(conn, sql);
                    DbDataAdapter da = GetDataAdapter();
                    da.SelectCommand = cmd;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    cmd.Parameters.Clear();
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Add(LogType.Execute, ex.Message, sql);
                throw ex;
            }
        }

        /// <summary>
        /// 执行查询指令返回DataReader对象
        /// </summary>
        /// <param name="sql">查询指令</param>
        /// <returns>DataReader对象</returns>
        public IDataReader ExecDataReader(SQL sql)
        {
            DbConnection conn = GetConnection();
            DbCommand cmd = GetCommand(conn, sql);
            IDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            cmd.Parameters.Clear();
            return dataReader;
        }

        /// <summary>
        /// 执行查询指令返回第一行第一列的值
        /// </summary>
        /// <param name="sql">查询指令</param>
        /// <returns>第一行第一列的值</returns>
        public object ExecScalar(SQL sql)
        {
            using (DbConnection conn = GetConnection())
            {
                DbCommand cmd = GetCommand(conn, sql);
                object result = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                //新增对于DBNull的判定，将DBnull转换为null以供逻辑判断（2014-09-05）
                return result == DBNull.Value ? null : result;
            }
        }

        /// <summary>
        /// 执行查询指令返回受影响行数
        /// </summary>
        /// <param name="sql">查询指令</param>
        /// <returns>受影响行数</returns>
        public int ExecNonQuerry(SQL sql)
        {
            List<SQL> sqlList = new List<SQL>();
            sqlList.Add(sql);
            return ExecNonQuerry(sqlList);
        }

        /// <summary>
        /// 执行查询指令列表返回受影响行数
        /// </summary>
        /// <param name="sqlList">查询指令列表</param>
        /// <returns>受影响行数</returns>
        public int ExecNonQuerry(List<SQL> sqlList)
        {
            if (TransExecute != null)
            {
                int count = 0;
                foreach (SQL sql in sqlList)
                {
                    count += TransExecute(sql);
                }
                return count;
            }
            return ExecTrans(sqlList);
        }

        /// <summary>
        /// 批量执行查询指令返回受影响行数(事务)
        /// </summary>
        /// <param name="sqlList">查询指令列表</param>
        /// <returns>受影响行数</returns>
        private int ExecTrans(List<SQL> sqlList)
        {
            using (DbConnection conn = GetConnection())
            {
                conn.Open();
                DbCommand cmd = GetCommand();
                cmd.Connection = conn;
                DbTransaction trans = conn.BeginTransaction();
                cmd.Transaction = trans;
                SQL sql = null;
                try
                {
                    int count = 0;
                    for (int i = 0; i < sqlList.Count; i++)
                    {
                        sql = sqlList[i];
                        cmd.CommandText = sql.CmdText;
                        cmd.CommandType = sql.CmdType;
                        if (sql.Parameters != null)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(sql.Parameters);
                        }
                        count += cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    Log.Instance.Add(LogType.Execute, ex.Message, sql);
                    return 0;
                }
            }
        }
        #endregion

        #region 虚方法
        /// <summary>
        /// 返回一个新的Connection实例
        /// </summary>
        /// <returns>Connection实例</returns>
        protected abstract DbConnection GetConnection();

        /// <summary>
        /// 返回一个新的Command实例
        /// </summary>
        /// <returns>Command实例</returns>
        protected abstract DbCommand GetCommand();

        /// <summary>
        /// 返回一个新的DataAdapter实例
        /// </summary>
        /// <returns>DataAdapter实例</returns>
        protected abstract DbDataAdapter GetDataAdapter();

        /// <summary>
        /// 返回一个新的IDbDataParameter实例
        /// </summary>
        /// <returns>IDbDataParameter实例</returns>
        public abstract IDbDataParameter GetParameter();

        /// <summary>
        /// 返回一个新的IDataProvider实例
        /// </summary>
        /// <returns>IDataProvider实例</returns>
        public abstract IDataProvider GetDataProvider();
        #endregion

        #region 内部类
        /// <summary>
        /// 事务操作对象
        /// </summary>
        internal class Transaction : IDisposable
        {
            private DbCommand _cmd;
            private DbConnection _conn;
            private DbTransaction _trans;
            private DataProviderBase _provider;

            public string CommandText
            {
                get { return _cmd.CommandText; }
                set { _cmd.CommandText = value; }
            }

            public CommandType CommandType
            {
                get { return _cmd.CommandType; }
                set { _cmd.CommandType = value; }
            }

            public DbParameterCollection Parameters
            {
                get { return _cmd.Parameters; }
            }

            public Transaction(DataProviderBase provider)
            {
                this._provider = provider;
            }

            public int Execute(SQL sql)
            {
                this.CommandText = sql.CmdText;
                this.CommandType = sql.CmdType;
                if (sql.Parameters != null)
                {
                    this.Parameters.Clear();
                    this.Parameters.AddRange(sql.Parameters);
                }
                int result = _cmd.ExecuteNonQuery();
                this.Parameters.Clear();
                return result;
            }

            public void BeginTransaction()
            {
                _conn = _provider.GetConnection();
                _conn.Open();
                _cmd = _provider.GetCommand();
                _cmd.Connection = _conn;
                _trans = _conn.BeginTransaction();
                _cmd.Transaction = _trans;
            }

            public void Commit()
            {
                _trans.Commit();
            }

            public void Rollback()
            {
                _trans.Rollback();
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _conn.Close();
                    _conn.Dispose();
                    _trans.Dispose();
                    _cmd.Parameters.Clear();
                    _cmd.Dispose();
                }
            }

            ~Transaction()
            {
                Dispose(false);
            }
        }
        #endregion
    }
}
