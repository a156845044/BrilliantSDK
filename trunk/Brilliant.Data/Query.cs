using Brilliant.Data.Entity;
using Brilliant.Data.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Brilliant.Data
{
    /// <summary>
    /// 查询器
    /// </summary>
    public class Query
    {
        private List<SQL> _sqlList;

        private int _recordCount;

        /// <summary>
        /// SQL对象列表
        /// </summary>
        public List<SQL> SqlList
        {
            get { return _sqlList; }
        }

        /// <summary>
        /// 总记录条数
        /// </summary>
        public int RecordCount
        {
            get { return _recordCount; }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        protected Query()
        {
            this._sqlList = new List<SQL>();
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="sql">SQL对象</param>
        protected Query(SQL sql)
            : this()
        {
            this._sqlList.Add(sql);
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="sqlList">SQL对象集合</param>
        protected Query(List<SQL> sqlList)
        {
            this._sqlList = sqlList;
        }

        /// <summary>
        /// 创建Query对象
        /// </summary>
        /// <returns>Query对象</returns>
        public static Query From()
        {
            Query q = new Query(new SQL());
            return q;
        }

        /// <summary>
        /// 通过Sql字符串创建Query对象
        /// </summary>
        /// <param name="sql">Sql字符串</param>
        /// <returns>Query对象</returns>
        public static Query From(string sql)
        {
            Query q = new Query();
            q.Append(sql);
            return q;
        }

        /// <summary>
        /// 通过Sql字符串创建Query对象
        /// </summary>
        /// <param name="sql">Sql字符串</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>Query对象</returns>
        public static Query From(string sql, params object[] parameters)
        {
            Query q = new Query();
            q.Append(sql, parameters);
            return q;
        }

        /// <summary>
        /// 通过SQL对象创建Query对象
        /// </summary>
        /// <param name="sql">SQL对象</param>
        /// <returns>Query对象</returns>
        public static Query From(SQL sql)
        {
            Query q = new Query(sql);
            return q;
        }

        /// <summary>
        /// 通过SQL对象集合创建Query对象
        /// </summary>
        /// <param name="sqlList">SQL对象集合</param>
        /// <returns>Query对象</returns>
        public static Query From(List<SQL> sqlList)
        {
            Query q = new Query(sqlList);
            return q;
        }

        /// <summary>
        /// 通过Linq表达式创建Query对象
        /// </summary>
        /// <param name="expression">Linq表达式</param>
        /// <returns>Query对象</returns>
        public static Query From(IQueryable expression)
        {
            Query q = new Query();
            q.Append(expression.ToString());
            return q;
        }

        /// <summary>
        /// 追加Sql字符串
        /// </summary>
        /// <param name="sql">sql字符串</param>
        /// <returns>当前对象实例</returns>
        public Query Append(string sql)
        {
            return Append(sql, null);
        }

        /// <summary>
        /// 追加Sql字符串
        /// </summary>
        /// <param name="sql">sql字符串</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>当前对象实例</returns>
        public Query Append(string sql, params object[] parameters)
        {
            int sqlCount = this._sqlList.Count;
            if (sqlCount <= 0)
            {
                this._sqlList.Add(new SQL());
                Append(sql, parameters);
            }
            else if (sqlCount == 1)
            {
                if (parameters == null)
                {
                    this._sqlList[0].Append(sql);
                }
                else
                {
                    this._sqlList[0].Append(sql, parameters);
                }
            }
            else
            {
                Log.Instance.Add(LogType.Parse, "Append方法追加SQL字符串时，SQL对象实例不唯一。");
                throw new Exception("实例对象不唯一，无法追加字符串实例。");
            }
            return this;
        }

        /// <summary>
        /// 限制返回的结果记录条数
        /// </summary>
        /// <param name="pageSize">每页显示的记录条数</param>
        /// <param name="pageNumber">当前页码</param>
        /// <returns>当前对象实例</returns>
        public Query Limit(int pageSize, int pageNumber)
        {
            SQL sql = _sqlList[0];
            string cmdText = sql.CmdText;
            int startIndex = 0;
            int endIndex = 0;
            switch (DBContext.DataProvider.DataPagedType)
            {
                case PagedType.Limit:
                    SetRecordCount(cmdText);
                    startIndex = (pageNumber - 1) * pageSize;
                    endIndex = pageSize * pageNumber;
                    sql.CmdText = String.Format("{0} limit {1},{2}", cmdText, startIndex, endIndex);
                    break;
                case PagedType.RowId:
                    SetRecordCount(cmdText);
                    startIndex = (pageNumber - 1) * pageSize + 1;
                    endIndex = pageSize * pageNumber;
                    sql.CmdText = String.Format("SELECT * FROM(SELECT ROWNUM RN,PT1.* FROM({0})PT1)PT2 WHERE PT2.RN BETWEEN {1} AND {2}", cmdText, startIndex, endIndex);
                    break;
                case PagedType.RowNumber:
                    startIndex = (pageNumber - 1) * pageSize + 1;
                    endIndex = pageSize * pageNumber;
                    Match match = Regex.Match(cmdText, @"SELECT", RegexOptions.IgnoreCase);
                    StringBuilder sb = new StringBuilder(cmdText);
                    //总记录条数查询语句
                    sb.Replace(match.Value, "SELECT TOP 100 PERCENT ", match.Index, match.Length);
                    SetRecordCount(sb.ToString());
                    //结果查询语句
                    sb.Clear();
                    sb.Append(cmdText);
                    string realCmdText = sb.Replace(match.Value, "SELECT TOP 10000 ", match.Index, match.Length).ToString();
                    sql.CmdText = String.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) RN,PT1.* FROM({0})PT1)PT2 WHERE PT2.RN BETWEEN {1} AND {2}", realCmdText, startIndex, endIndex);
                    break;
            }
            return this;
        }

        /// <summary>
        /// 限制返回的结果记录条数
        /// </summary>
        /// <param name="pageSize">每页显示的记录条数</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="recordCount">返回参数：总记录条数</param>
        /// <returns>当前对象实例</returns>
        public Query Limit(int pageSize, int pageNumber, out int recordCount)
        {
            Limit(pageSize, pageNumber);
            recordCount = _recordCount;
            return this;
        }

        /// <summary>
        /// 执行查询返回受影响行数
        /// </summary>
        /// <returns>受影响行数</returns>
        public int Execute()
        {
            return DBContext.DataProvider.ExecNonQuerry(_sqlList);
        }

        /// <summary>
        /// 将执行结果转换为对象集合
        /// </summary>
        /// <returns>对象集合</returns>
        public List<T> ToList<T>() where T : EntityBase, new()
        {
            DataTable dtResult = GetResult();
            return GetList<T>(dtResult);
        }

        /// <summary>
        /// 将执行结果转换为对象
        /// </summary>
        /// <returns>对象</returns>
        public T ToObject<T>() where T : EntityBase, new()
        {
            DataTable dtResult = GetResult();
            return GetObject<T>(dtResult);
        }

        /// <summary>
        /// 执行查询语句返回第一行第一列的值
        /// </summary>
        /// <returns>第一行第一列的值</returns>
        public T First<T>()
        {
            if (_sqlList.Count <= 0)
            {
                Log.Instance.Add(LogType.Map, "First<T>方法没有找到需要执行的SQL语句。");
                throw new Exception("没有需要执行的SQL语句.");
            }
            object obj = DBContext.DataProvider.ExecScalar(_sqlList[0]);
            if (obj == DBNull.Value)
            {
                return default(T);
            }
            return (T)obj;
        }

        /// <summary>
        /// 设置总记录条数
        /// </summary>
        private void SetRecordCount(string cmdText)
        {
            SQL sql = new SQL();
            sql.Append("SELECT COUNT(*) FROM({0})PT", cmdText);
            this._recordCount = DBContext.DataProvider.ExecNonQuerry(sql);
        }

        /// <summary>
        /// 获取查询结果
        /// </summary>
        /// <returns>查询结果</returns>
        private DataTable GetResult()
        {
            if (_sqlList.Count <= 0)
            {
                Log.Instance.Add(LogType.Map, "GetResult方法没有找到需要执行的SQL语句。");
                throw new Exception("没有需要执行的SQL语句。");
            }
            DataSet ds = DBContext.DataProvider.ExecDataSet(_sqlList[0]);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 从查询结果中获取对象
        /// </summary>
        /// <param name="dtResult">查询结果</param>
        /// <returns>对象</returns>
        private T GetObject<T>(DataTable dtResult) where T : EntityBase, new()
        {
            List<T> list = GetList<T>(dtResult);
            if (list.Count <= 0)
            {
                return null;
            }
            if (list.Count > 1)
            {
                Log.Instance.Add(LogType.Map, "GetObject<T>获取对象时，返回的结果记录条数不唯一。", _sqlList[0]);
                throw new Exception("查询的结果不唯一。");
            }
            return list[0];
        }

        /// <summary>
        /// 从查询结果中获取对象集合
        /// </summary>
        /// <param name="dtResult">查询结果</param>
        /// <returns>对象集合</returns>
        private List<T> GetList<T>(DataTable dtResult) where T : EntityBase, new()
        {
            List<T> list = new List<T>();
            if (dtResult == null || dtResult.Rows.Count <= 0)
            {
                return list;
            }
            foreach (DataRow row in dtResult.Rows)
            {
                T entity = new T();
                foreach (DataColumn col in dtResult.Columns)
                {
                    entity.SetProperty(col.ColumnName, row[col.ColumnName]);
                }
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// 从查询结果中获取对象属性列表
        /// </summary>
        /// <param name="dtResult">查询结果</param>
        /// <returns>对象属性列表</returns>
        private List<IDictionary<string, object>> GetPropertyList<T>(DataTable dtResult) where T : EntityBase, new()
        {
            List<IDictionary<string, object>> list = new List<IDictionary<string, object>>();
            if (dtResult == null || dtResult.Rows.Count <= 0)
            {
                return list;
            }
            foreach (DataRow row in dtResult.Rows)
            {
                T entity = new T();
                foreach (DataColumn col in dtResult.Columns)
                {
                    entity.SetProperty(col.ColumnName, row[col.ColumnName]);
                }
                list.Add(entity.GetProperties());
            }
            return list;
        }
    }

    /// <summary>
    /// 查询器
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class Query<T> : Query where T : EntityBase, new()
    {
        private EntityMapper<T> _em;

        /// <summary>
        /// 构造器
        /// </summary>
        private Query()
            : base() { }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="sql">SQL对象</param>
        private Query(SQL sql)
            : base(sql) { }

        private Query(List<SQL> sqlList)
            : base(sqlList) { }

        /// <summary>
        /// 返回一个新的Query对象
        /// </summary>
        /// <returns>Query对象</returns>
        public new static Query<T> From()
        {
            Query<T> q = new Query<T>(new SQL());
            return q;
        }

        /// <summary>
        /// 通过Sql语句创建Query对象
        /// </summary>
        /// <param name="sql">SQL对象</param>
        /// <returns>Query对象</returns>
        public new static Query<T> From(string sql)
        {
            Query<T> q = new Query<T>();
            q.Append(sql);
            return q;
        }

        /// <summary>
        /// 通过Sql语句创建Query对象
        /// </summary>
        /// <param name="sql">Sql语句</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>Query对象</returns>
        public new static Query<T> From(string sql, params object[] parameters)
        {
            Query<T> q = new Query<T>();
            q.Append(sql, parameters);
            return q;
        }

        /// <summary>
        /// 通过SQL对象创建Query对象
        /// </summary>
        /// <param name="sql">SQL对象</param>
        /// <returns>Query对象</returns>
        public new static Query<T> From(SQL sql)
        {
            Query<T> q = new Query<T>(sql);
            return q;
        }

        /// <summary>
        /// 通过SQL对象集合创建Query对象
        /// </summary>
        /// <param name="sql">SQL对象集合</param>
        /// <returns>Query对象</returns>
        public new static Query<T> From(List<SQL> sqlList)
        {
            Query<T> q = new Query<T>(sqlList);
            return q;
        }

        /// <summary>
        /// 通过Linq语句创建Query对象
        /// </summary>
        /// <param name="expression">Linq查询表达式</param>
        /// <returns>Query对象</returns>
        public new static Query<T> From(IQueryable expression)
        {
            Query<T> q = new Query<T>();
            q.Append(expression.ToString());
            return q;
        }

        /// <summary>
        /// 通过实体对象创建Query对象
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>Query对象</returns>
        public static Query<T> From(T entity)
        {
            Query<T> q = new Query<T>();
            q._em = new EntityMapper<T>(entity);
            return q;
        }

        /// <summary>
        /// 通过实体对象列表创Query对象
        /// </summary>
        /// <param name="entities">实体对象列表</param>
        /// <returns>Query对象</returns>
        public static Query<T> From(List<T> entities)
        {
            Query<T> q = new Query<T>();
            q._em = new EntityMapper<T>(entities);
            return q;
        }

        /// <summary>
        /// 追加Sql字符串实例
        /// </summary>
        /// <param name="sql">Sql字符串实例</param>
        /// <returns>当前对象实例</returns>
        public new Query<T> Append(string sql)
        {
            base.Append(sql);
            return this;
        }

        /// <summary>
        /// 追加Sql字符串实例
        /// </summary>
        /// <param name="sql">Sql字符串实例</param>
        /// <param name="parameters">查询参数</param>
        /// <returns>当前对象实例</returns>
        public new Query<T> Append(string sql, params object[] parameters)
        {
            base.Append(sql, parameters);
            return this;
        }

        /// <summary>
        /// 限制返回的结果记录条数
        /// </summary>
        /// <param name="pageSize">每页显示的记录条数</param>
        /// <param name="pageNumber">当前页码</param>
        /// <returns>当前对象实例</returns>
        public new Query<T> Limit(int pageSize, int pageNumber)
        {
            base.Limit(pageSize, pageNumber);
            return this;
        }

        /// <summary>
        /// 限制返回的结果记录条数
        /// </summary>
        /// <param name="pageSize">每页显示的记录条数</param>
        /// <param name="pageNumber">当前页码</param>
        /// <param name="recordCount">返回参数：总记录条数</param>
        /// <returns>当前对象实例</returns>
        public new Query<T> Limit(int pageSize, int pageNumber, out int recordCount)
        {
            base.Limit(pageSize, pageNumber, out recordCount);
            return this;
        }

        /// <summary>
        /// 判断实在是否已经存在
        /// </summary>
        /// <returns>执行结果</returns>
        public bool Has()
        {
            this.SqlList.Clear();
            this.SqlList.AddRange(_em.Has);
            return this.First<int>() > 0;
        }

        /// <summary>
        /// 保存实体对象
        /// </summary>
        /// <returns>执行结果</returns>
        public bool Save()
        {
            this.SqlList.Clear();
            this.SqlList.AddRange(_em.Save);
            return this.Execute() > 0;
        }

        /// <summary>
        /// 移除实体对象
        /// </summary>
        /// <returns>执行结果</returns>
        public bool Remove()
        {
            this.SqlList.Clear();
            this.SqlList.AddRange(_em.Remove);
            return this.Execute() > 0;
        }

        /// <summary>
        /// 保存对实体的修改
        /// </summary>
        /// <returns>执行结果</returns>
        public bool SaveChanges()
        {
            this.SqlList.Clear();
            this.SqlList.AddRange(_em.SaveChanges);
            return this.Execute() > 0;
        }

        /// <summary>
        /// 将查询结果转化为对象集合
        /// </summary>
        /// <returns>对象集合</returns>
        public List<T> ToList()
        {
            return base.ToList<T>();
        }

        /// <summary>
        /// 将查询结果转化为实体对象
        /// </summary>
        /// <returns>实体对象</returns>
        public T ToObject()
        {
            return base.ToObject<T>();
        }
    }
}
