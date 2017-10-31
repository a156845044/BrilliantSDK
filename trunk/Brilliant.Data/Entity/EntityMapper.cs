using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Brilliant.Data.Entity
{
    /// <summary>
    /// 实体解析
    /// </summary>
    public class EntityMapper<T> where T : EntityBase
    {
        private Type _type;
        private List<T> _entities;

        /// <summary>
        /// 构造器
        /// </summary>
        private EntityMapper()
        {
            _type = typeof(T);
            _entities = new List<T>();
            GetTableName();
            GetFields();
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="entity">实体</param>
        public EntityMapper(T entity)
            : this()
        {
            _entities.Add(entity);
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="entities">实体集合</param>
        public EntityMapper(List<T> entities)
            : this()
        {
            _entities.AddRange(entities);
        }

        /// <summary>
        /// 表明称
        /// </summary>
        public string TableName { get; private set; }

        /// <summary>
        /// 字段列表
        /// </summary>
        public string[] Fields { get; private set; }

        /// <summary>
        /// 主键(单主键)
        /// </summary>
        public string PKName { get; private set; }

        /// <summary>
        /// 添加SQL语句
        /// </summary>
        public List<SQL> Save
        {
            get
            {
                List<SQL> sqlList = new List<SQL>();
                string strField = String.Join(",", this.Fields);
                string fmt = String.Format("INSERT INTO {0}({1}) VALUES({2})", TableName, strField, GetParam(Fields.Length));
                foreach (T entity in _entities)
                {
                    sqlList.Add(GetSql(fmt, GetValues(entity, Fields)));
                }
                return sqlList;
            }
        }

        /// <summary>
        /// 删除SQL语句
        /// </summary>
        public List<SQL> Remove
        {
            get
            {
                List<SQL> sqlList = new List<SQL>();
                string fmt = String.Format("DELETE FROM {0} WHERE {1}=?", TableName, PKName);
                foreach (T entity in _entities)
                {
                    sqlList.Add(GetSql(fmt, entity[PKName]));
                }
                return sqlList;
            }
        }

        /// <summary>
        /// 更新SQL语句
        /// </summary>
        public List<SQL> SaveChanges
        {
            get
            {
                List<SQL> sqlList = new List<SQL>();
                string fmt = String.Format("UPDATE {0} SET {1} WHERE {2}=?", TableName, GetParam(Fields), PKName);
                foreach (T entity in _entities)
                {
                    sqlList.Add(GetSql(fmt, GetValues(entity, Fields, true)));
                }
                return sqlList;
            }
        }

        /// <summary>
        /// 判断是否存在SQL语句
        /// </summary>
        public List<SQL> Has
        {
            get
            {
                if (_entities.Count > 1)
                {
                    throw new Exception("Has方法无法判定多个对象在数据库中是否存在。");
                }
                List<SQL> sqlList = new List<SQL>();
                string fmt = String.Format("SELECT COUNT(*) FROM {0} WHERE {1}=?", TableName, PKName);
                foreach (T entity in _entities)
                {
                    sqlList.Add(GetSql(fmt, entity[PKName]));
                }
                return sqlList;
            }
        }

        /// <summary>
        /// 获取表明称
        /// </summary>
        private void GetTableName()
        {
            object[] objs = _type.GetCustomAttributes(true);
            if (objs == null || objs.Length == 0)
            {
                return;
            }
            TableAttribute tableAttr = objs[0] as TableAttribute;
            TableName = tableAttr.Name;
        }

        /// <summary>
        /// 获取字段列表
        /// </summary>
        private void GetFields()
        {
            object[] objs = null;
            List<string> fields = new List<string>();
            PropertyInfo[] properties = _type.GetProperties();
            foreach (PropertyInfo ropertyInfo in properties)
            {
                objs = ropertyInfo.GetCustomAttributes(true);
                if (objs == null || objs.Length == 0) continue;
                ColumnAttribute colAttr = objs[0] as ColumnAttribute;
                if (colAttr.IsForeignTable) continue;
                string colName = colAttr.Name;
                if (colAttr.IsPrimaryKey)
                {
                    PKName = colName;
                }
                fields.Add(colName);
            }
            this.Fields = fields.ToArray();
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="length">参数个数</param>
        /// <returns>返回格式：?,?,?,?</returns>
        private string GetParam(int length)
        {
            string[] strs = new string[length];
            for (int i = 0; i < length; i++)
            {
                strs[i] = "?";
            }
            return String.Join(",", strs);
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="fields">参数名称列表</param>
        /// <returns>返回格式：Id=?,Name=?</returns>
        private string GetParam(string[] fields)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string field in fields)
            {
                if (field == PKName)
                {
                    continue;
                }
                sb.AppendFormat("{0}=?,", field);
            }
            return sb.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 获取SQL对象
        /// </summary>
        /// <param name="format">格式</param>
        /// <param name="values">参数值</param>
        /// <returns>SQL对象</returns>
        private SQL GetSql(string format, params object[] values)
        {
            SQL sql = new SQL();
            sql.Append(format, values);
            return sql;
        }

        /// <summary>
        /// 获取实体属性值
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fields">字段名称列表</param>
        /// <returns>实体属性值</returns>
        private object[] GetValues(T entity, string[] fields)
        {
            return GetValues(entity, fields, false);
        }

        /// <summary>
        /// 获取实体属性值
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fields">字段名称列表</param>
        /// <param name="resetPK">是否重置主键位置</param>
        /// <returns>实体属性值</returns>
        private object[] GetValues(T entity, string[] fields, bool resetPK)
        {
            List<object> values = new List<object>();
            foreach (string field in fields)
            {
                if (resetPK && field == PKName)
                {
                    continue;
                }
                values.Add(entity[field]);
            }
            if (resetPK) values.Add(entity[PKName]);
            return values.ToArray();
        }
    }
}
