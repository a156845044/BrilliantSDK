using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Brilliant.ORM
{
    /// <summary>
    /// SqlMap类
    /// </summary>
    public class SqlMap
    {
        /// <summary>
        /// SQL对象列表
        /// </summary>
        protected List<SQL> sqlList;

        /// <summary>
        /// 默认构造器
        /// </summary>
        protected SqlMap()
        {
            this.sqlList = new List<SQL>();
        }

        /// <summary>
        /// 带参构造器
        /// </summary>
        /// <param name="sql">SQL对象</param>
        protected SqlMap(SQL sql)
        {
            this.sqlList = new List<SQL>();
            this.sqlList.Add(sql);
        }

        /// <summary>
        /// 带参构造器
        /// </summary>
        /// <param name="sqlList">SQL对象列表</param>
        protected SqlMap(List<SQL> sqlList)
        {
            this.sqlList = sqlList;
        }

        /// <summary>
        /// 解析查询语句返回当前对象实例
        /// </summary>
        /// <param name="sql">查询语句参数</param>
        /// <returns>当前对象实例</returns>
        public static SqlMap ParseSql(SQL sql)
        {
            SqlMap instance = new SqlMap(sql);
            return instance;
        }

        /// <summary>
        /// 解析查询语句返回当前对象实例
        /// </summary>
        /// <param name="sqlList">查询语句参数列表</param>
        /// <returns>当前对象实例</returns>
        public static SqlMap ParseSql(List<SQL> sqlList)
        {
            SqlMap instance = new SqlMap(sqlList);
            return instance;
        }

        /// <summary>
        /// 执行查询语句返回受影响行数
        /// </summary>
        /// <returns>受影响行数</returns>
        public int Execute()
        {
            return DBHelper.DataProvider.ExecNonQuerry(sqlList);
        }

        /// <summary>
        /// 执行查询语句返回受影响行数(用于事务过程)
        /// </summary>
        /// <param name="dataProvider">关联DataProvider对象</param>
        /// <returns>受影响行数</returns>
        public int Execute(IDataProvider dataProvider)
        {
            return dataProvider.ExecNonQuerry(sqlList);
        }

        /// <summary>
        /// 执行查询语句返回第一行第一列的值
        /// </summary>
        /// <returns>第一行第一列的值</returns>
        public object First()
        {
            return DBHelper.DataProvider.ExecScalar(sqlList[0]);
        }

        /// <summary>
        /// 执行查询语句返回第一行第一列的值
        /// </summary>
        /// <typeparam name="T">返回值的类型</typeparam>
        /// <returns>第一行第一列的值</returns>
        public T First<T>()
        {
            object obj = DBHelper.DataProvider.ExecScalar(sqlList[0]);
            return obj == null ? default(T) : (T)obj;
        }
    }

    /// <summary>
    /// SqlMap类
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class SqlMap<T> : SqlMap where T : EntityBase, new()
    {
        /// <summary>
        /// 默认构造器
        /// </summary>
        private SqlMap()
            : base()
        {
        }

        /// <summary>
        /// 带参构造器
        /// </summary>
        /// <param name="sql">SQL对象</param>
        private SqlMap(SQL sql)
            : base(sql)
        {
        }

        /// <summary>
        /// 带参构造器
        /// </summary>
        /// <param name="sqlList">SQL对象列表</param>
        private SqlMap(List<SQL> sqlList)
            : base(sqlList)
        {
        }

        /// <summary>
        /// 解析查询语句返回当前对象实例
        /// </summary>
        /// <param name="sql">查询语句参数</param>
        /// <returns>当前对象实例</returns>
        public static new SqlMap<T> ParseSql(SQL sql)
        {
            SqlMap<T> instance = new SqlMap<T>(sql);
            return instance;
        }

        /// <summary>
        /// 解析查询语句返回当前对象实例
        /// </summary>
        /// <param name="sqlList">查询语句参数列表</param>
        /// <returns>当前对象实例</returns>
        public static new SqlMap<T> ParseSql(List<SQL> sqlList)
        {
            SqlMap<T> instance = new SqlMap<T>(sqlList);
            return instance;
        }

        /// <summary>
        /// 解析Linq表达式返回当前对象实例
        /// </summary>
        /// <param name="expression">Linq表达式</param>
        /// <returns>当前对象实例</returns>
        public static SqlMap<T> ParseExp(IQueryable<T> expression)
        {
            SQL sql = SQL.Build(expression.ToString());
            SqlMap<T> instance = new SqlMap<T>(sql);
            return instance;
        }

        /// <summary>
        /// 获取查询结果
        /// </summary>
        /// <returns>查询结果</returns>
        private DataTable GetResult()
        {
            if (sqlList.Count <= 0)
            {
                Log.Instance.Add(LogType.Map, "GetResult方法执行时未找到对应需要执行的SQL语句.");
                throw new Exception("没有需要执行的SQL语句");
            }
            DataSet ds = DBHelper.DataProvider.ExecDataSet(sqlList[0]);
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
        private T GetObject(DataTable dtResult)
        {
            List<T> list = GetList(dtResult);
            if (list.Count <= 0)
            {
                return null;
            }
            if (list.Count > 1)
            {
                Log.Instance.Add(LogType.Map, "GetObject方法执行时得到的结果不唯一.");
                throw new Exception("查询的结果不唯一");
            }
            return list[0];
        }

        /// <summary>
        /// 从查询结果中获取对象集合
        /// </summary>
        /// <param name="dtResult">查询结果</param>
        /// <returns>对象集合</returns>
        private List<T> GetList(DataTable dtResult)
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
        private List<IDictionary<string, object>> GetPropertyList(DataTable dtResult)
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
                list.Add(entity.GetAllProperties());
            }
            return list;
        }

        /// <summary>
        /// 将执行结果转换为对象
        /// </summary>
        /// <returns>对象</returns>
        public T ToObject()
        {
            DataTable dtResult = GetResult();
            return GetObject(dtResult);
        }

        /// <summary>
        /// 将执行结果转换为对象集合
        /// </summary>
        /// <returns>对象集合</returns>
        public List<T> ToList()
        {
            DataTable dtResult = GetResult();
            return GetList(dtResult);
        }

        /// <summary>
        /// 将执行结果转换为Json对象
        /// </summary>
        /// <returns>Json对象</returns>
        public string ToJsonObject()
        {
            DataTable dtResult = GetResult();
            T entity = GetObject(dtResult);
            return JsonSerializer.Serialize(entity);
        }

        /// <summary>
        /// 将执行结果转换为Json对象
        /// </summary>
        /// <param name="onlyProperty">只把关联对象的属性转换为Json，不转换关联对象</param>
        /// <returns>Json对象</returns>
        /// <remarks>时间：2014-09-05 类型：新增</remarks>
        public string ToJsonObject(bool onlyProperty)
        {
            if (onlyProperty)
            {
                DataTable dtResult = GetResult();
                T entity = GetObject(dtResult);
                return JsonSerializer.JSSerialize(entity.GetAllProperties());
            }
            else
            {
                return this.ToJsonObject();
            }
        }

        /// <summary>
        /// 将执行结果转换为Json对象列表
        /// </summary>
        /// <returns>Json对象列表</returns>
        public string ToJsonList()
        {
            DataTable dtResult = GetResult();
            List<T> list = GetList(dtResult);
            return JsonSerializer.Serialize(list);
        }

        /// <summary>
        /// 将执行结果转换为Json对象列表
        /// </summary>
        /// <param name="onlyProperty">只把关联对象的属性转换为Json，不转换关联对象</param>
        /// <returns>Json对象列表</returns>
        /// <remarks>时间：2014-09-05 类型：新增</remarks>
        public string ToJsonList(bool onlyProperty)
        {
            if (onlyProperty)
            {
                DataTable dtResult = GetResult();
                List<IDictionary<string, object>> list = GetPropertyList(dtResult);
                return JsonSerializer.JSSerialize(list);
            }
            else
            {
                return this.ToJsonList();
            }
        }
    }
}
