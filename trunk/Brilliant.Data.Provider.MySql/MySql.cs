using Brilliant.Data.Common;
using Brilliant.Data.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace Brilliant.Data.Provider
{
    /// <summary>
    /// MySql数据访问对象
    /// </summary>
    public class MySql : DataProviderBase, ISchemaProvider
    {
        public override PagedType DataPagedType
        {
            get { return PagedType.Limit; }
        }

        public MySql()
        {
        }

        public MySql(string connectionString)
            : base(connectionString)
        {
        }

        protected override DbConnection GetConnection()
        {
            return new MySqlConnection(base.ConnectionString);
        }

        protected override DbCommand GetCommand()
        {
            return new MySqlCommand();
        }

        protected override DbDataAdapter GetDataAdapter()
        {
            return new MySqlDataAdapter();
        }

        public override IDbDataParameter GetParameter()
        {
            return new MySqlParameter();
        }

        public override IDataProvider GetDataProvider()
        {
            return new MySql(this.ConnectionString);
        }

        public IList<DboBase> GetDbList()
        {
            SQL sql = new SQL("show databases");
            List<DboBase> list = new List<DboBase>();
            DataTable dt = this.ExecDataSet(sql).Tables[0];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string dboName = dt.Rows[i][0].ToString();
                if (dboName == "mysql" || dboName == "information_schema" || dboName == "performance_schema")
                {
                    continue;
                }
                DboBase dboBase = new DboBase();
                dboBase.DboId = i.ToString();
                dboBase.DboName = dboName;
                list.Add(dboBase);
            }
            return list;
        }

        public IList<DboProc> GetProcList()
        {
            return Query<DboProc>.From("SELECT name AS DboId,name AS DboName FROM mysql.proc WHERE db='{0}' AND type='PROCEDURE' ORDER BY DboId ASC", connInfo.DataBase).ToList();
        }

        public IList<DboTable> GetTableList()
        {
            SQL sql = new SQL("show tables");
            List<DboTable> list = new List<DboTable>();
            DataTable dt = this.ExecDataSet(sql).Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                DboTable dboBase = new DboTable();
                dboBase.DboId = Convert.ToString(dr[0]);
                dboBase.DboName = Convert.ToString(dr[0]);
                list.Add(dboBase);
            }
            //list.OrderBy(m => m.DboName);
            return list;
        }

        public IList<DboView> GetViewList()
        {
            return Query<DboView>.From("SELECT TABLE_NAME AS DboId,TABLE_NAME AS DboName FROM information_schema.views WHERE TABLE_SCHEMA='{0}' ORDER BY DboId ASC", connInfo.DataBase).ToList();
        }

        /// <summary>
        /// 获取表字段列表
        /// </summary>
        public IList<Brilliant.Data.Common.SchemaColumn> GetColumn(string tableName)
        {
            SQL sql = new SQL(GetSchemaQuerySql(tableName));
            IList<Brilliant.Data.Common.SchemaColumn> list = new List<Brilliant.Data.Common.SchemaColumn>();
            using (IDataReader dr = this.ExecDataReader(sql))
            {
                int i = 0;
                while (dr.Read())
                {
                    Brilliant.Data.Common.SchemaColumn model = new Brilliant.Data.Common.SchemaColumn();
                    string colType = Convert.ToString(dr["Type"]);
                    string colExt = Regex.Match(colType, "\\(\\d+\\)\\s*\\w*", RegexOptions.IgnoreCase).Value;
                    string colLenght = Regex.Match(colType, "\\(\\d+\\)", RegexOptions.IgnoreCase).Value;
                    model.ColumnIndex = i;
                    model.ColumnName = TypeMapper.ConvertToUpper(Convert.ToString(dr["Field"]));
                    model.ColumnNameLower = TypeMapper.ConvertToLower(model.ColumnName);
                    model.ColumnType = String.IsNullOrEmpty(colExt) ? colType : colType.Replace(colExt, "");
                    model.ColumnDefaultValue = Convert.ToString(dr["Default"]);
                    model.ColumnLength = String.IsNullOrEmpty(colLenght) ? 14 : Convert.ToInt32(colLenght.Replace("(", "").Replace(")", ""));
                    model.IsIdentity = false;// Convert.ToString(dr["Identity"]) == "T" ? true : false;
                    model.IsPK = Convert.ToString(dr["Key"]) == "PRI" ? true : false;
                    model.IsFK = Convert.ToString(dr["Key"]) == "MUL" ? true : false;
                    model.FkTableName = String.Empty;// Convert.ToString(dr["ForeignKeyTable"]);
                    model.FKTableNameLower = String.Empty;// TypeMap.ConvertToLower(model.FkTableName);
                    model.IsNull = Convert.ToString(dr["Null"]).ToUpper() == "YES" ? true : false;
                    model.ColumnDesc = Convert.ToString(dr["Comment"]);

                    model.CSharpTypeParser = TypeMapper.MapLanTypeParser(model.ColumnType, LanguageType.CS);
                    model.CSharpType = TypeMapper.MapLanType(model.ColumnType, LanguageType.CS);
                    model.JavaTypeParser = TypeMapper.MapLanTypeParser(model.ColumnType, LanguageType.Java);
                    model.JavaType = TypeMapper.MapLanType(model.ColumnType, LanguageType.Java);
                    model.SqlDbType = TypeMapper.MapSQLType(model.ColumnType, model.ColumnLength);
                    list.Add(model);
                    i++;
                }
            }
            return list;
        }

        /// <summary>
        /// 获取表架构信息
        /// </summary>
        public SchemaTable GetSchemaInfo(string tableName)
        {
            SchemaTable schemaTable = new SchemaTable();
            schemaTable.TableIndex = 0;
            schemaTable.TableName = TypeMapper.ConvertToUpper(tableName);
            schemaTable.TableNameLower = TypeMapper.ConvertToLower(tableName);
            schemaTable.TableDesc = this.GetTableDesc(tableName);
            schemaTable.TableNameSpace = tableName;
            schemaTable.ColumnList = this.GetColumn(tableName);
            schemaTable.PKList = (from item in schemaTable.ColumnList where item.IsPK select item).ToList();
            schemaTable.FKList = (from item in schemaTable.ColumnList where item.IsFK select item).ToList();
            return schemaTable;
        }

        /// <summary>
        /// 获取表描述信息
        /// </summary>
        public string GetTableDesc(string tableName)
        {
            return Query<DboBase>.From("show table status where name='{0}'", tableName).ToObject()["Comment"].ToString();
        }

        /// <summary>
        /// 获取表架构查询语句
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>表架构查询语句</returns>
        public string GetSchemaQuerySql(string tableName)
        {
            return String.Format("show full fields from {0}", tableName);
        }
    }
}
