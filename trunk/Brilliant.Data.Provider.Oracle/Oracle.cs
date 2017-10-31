using Brilliant.Data.Common;
using Brilliant.Data.Utility;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Brilliant.Data.Provider
{
    /// <summary>
    /// Oracle数据访问对象
    /// </summary>
    public class Oracle : DataProviderBase, ISchemaProvider
    {
        public override PagedType DataPagedType
        {
            get { return PagedType.RowId; }
        }

        public Oracle()
        {
        }

        public Oracle(string connectionString)
            : base(connectionString)
        {
        }

        protected override DbConnection GetConnection()
        {
            return new OracleConnection(base.ConnectionString);
        }

        protected override DbCommand GetCommand()
        {
            return new OracleCommand();
        }

        protected override DbDataAdapter GetDataAdapter()
        {
            return new OracleDataAdapter();
        }

        public override IDbDataParameter GetParameter()
        {
            return new OracleParameter();
        }

        public override IDataProvider GetDataProvider()
        {
            return new Oracle(this.ConnectionString);
        }

        /// <summary>
        /// 获取表空间服务列表
        /// </summary>
        /// <returns>表空间服务列表</returns>
        public IList<DboBase> GetDbList()
        {
            //1.通过注册表获取本机Service列表（无法获取远程主机）
            //RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE").OpenSubKey("ORACLE");
            //string oracleHome = Convert.ToString(key.GetValue("ORACLE_HOME"));
            //string path = oracleHome + @"\network\ADMIN\tnsnames.ora";
            //string[] lines = File.ReadAllLines(path);
            //List<DboBase> list = new List<DboBase>();
            //for (int i = 0; i < lines.Length; i++)
            //{
            //    string line = lines[i].Trim();
            //    if (line != String.Empty)
            //    {
            //        char c = line[0];
            //        if (c >= 'A' && c <= 'Z')
            //        {
            //            DboBase dboBase = new DboBase();
            //            dboBase.DboId = i.ToString();
            //            dboBase.DboName = line.Substring(0, line.IndexOf(' '));
            //            list.Add(dboBase);
            //        }
            //    }
            //}
            //2.通过连接字符串获取当前服务
            //List<DboBase> list = new List<DboBase>();
            //string result = Regex.Match(this.ConnectionString, "SERVICE_NAME=\\w+", RegexOptions.IgnoreCase).Value;
            //if (!String.IsNullOrEmpty(result))
            //{
            //    DboBase dboBase = new DboBase();
            //    dboBase.DboId = "1";
            //    dboBase.DboName = result.Replace("SERVICE_NAME=", "");
            //    list.Add(dboBase);
            //}
            //return list;
            //3.通过SQL语句获取服务列表
            SQL sql = new SQL("SELECT GLOBAL_NAME FROM GLOBAL_NAME");
            List<DboBase> list = new List<DboBase>();
            using (IDataReader dr = this.ExecDataReader(sql))
            {
                while (dr.Read())
                {
                    DboBase entity = new DboBase();
                    entity.DboName = TypeMapper.ConvertToUpper(Convert.ToString(dr["GLOBAL_NAME"]).ToLower());
                    entity.DboId = entity.DboName;
                    list.Add(entity);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取存储过程列表
        /// </summary>
        /// <returns>存储过程列表</returns>
        public IList<DboProc> GetProcList()
        {
            return Query<DboProc>.From("SELECT OBJECT_NAME AS DboId,OBJECT_NAME AS DboName FROM USER_PROCEDURES").ToList();
        }

        /// <summary>
        /// 获取数据表列表
        /// </summary>
        /// <returns>数据表列表</returns>
        public IList<DboTable> GetTableList()
        {
            SQL sql = new SQL("SELECT TABLE_NAME AS DboId,TABLE_NAME AS DboName FROM User_TABLES ORDER BY DboId ASC");
            List<DboTable> list = new List<DboTable>();
            using (IDataReader dr = this.ExecDataReader(sql))
            {
                while (dr.Read())
                {
                    DboTable entity = new DboTable();
                    entity.DboName = TypeMapper.ConvertToUpper(Convert.ToString(dr["DboName"]).ToLower());
                    entity.DboId = entity.DboName;
                    list.Add(entity);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取视图列表
        /// </summary>
        /// <returns>视图列表</returns>
        public IList<DboView> GetViewList()
        {
            return Query<DboView>.From("SELECT VIEW_NAME AS DboId,VIEW_NAME AS DboName FROM USER_VIEWS").ToList();
        }

        /// <summary>
        /// 获取表架构信息
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>表架构信息</returns>
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
        /// 获取表字段列表
        /// </summary>
        public IList<SchemaColumn> GetColumn(string tableName)
        {
            SQL sql = new SQL(GetSchemaQuerySql(tableName));
            IList<SchemaColumn> list = new List<SchemaColumn>();
            using (IDataReader dr = this.ExecDataReader(sql))
            {
                while (dr.Read())
                {
                    SchemaColumn model = new SchemaColumn();
                    model.ColumnIndex = Convert.ToInt32(dr["COLUMN_ID"]);
                    model.ColumnName = TypeMapper.ConvertToUpper(Convert.ToString(dr["COLUMN_NAME"]).ToLower());
                    model.ColumnNameLower = TypeMapper.ConvertToLower(model.ColumnName);
                    model.ColumnType = Convert.ToString(dr["DATA_TYPE"]);
                    model.ColumnDefaultValue = TypeMapper.MapDefaultValue(Convert.ToString(dr["DATA_DEFAULT"]));
                    model.ColumnLength = Convert.ToInt32(dr["DATA_LENGTH"]);
                    model.IsIdentity = false; // Convert.ToString(dr["Identity"]) == "T" ? true : false;
                    model.IsPK = Convert.ToString(dr["CONSTRAINT_TYPE"]) == "P" ? true : false;
                    model.IsFK = Convert.ToString(dr["CONSTRAINT_TYPE"]) == "R" ? true : false;
                    model.FkTableName = String.Empty; //TypeMapper.ConvertToUpper(Convert.ToString(dr["ForeignKeyTable"]));
                    model.FKTableNameLower = String.Empty; //TypeMapper.ConvertToLower(model.FkTableName);
                    model.IsNull = Convert.ToString(dr["NULLABLE"]) == "Y" ? true : false;
                    model.ColumnDesc = Convert.ToString(dr["COMMENTS"]);

                    model.CSharpTypeParser = TypeMapper.MapLanTypeParser(model.ColumnType, LanguageType.CS);
                    model.CSharpType = TypeMapper.MapLanType(model.ColumnType, LanguageType.CS);
                    model.JavaTypeParser = TypeMapper.MapLanTypeParser(model.ColumnType, LanguageType.Java);
                    model.JavaType = TypeMapper.MapLanType(model.ColumnType, LanguageType.Java);
                    model.SqlDbType = TypeMapper.MapSQLType(model.ColumnType, model.ColumnLength);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// 获取表描述信息
        /// </summary>
        public string GetTableDesc(string tableName)
        {
            return Query.From("SELECT COMMENTS FROM USER_TAB_COMMENTS WHERE TABLE_NAME='{0}'", tableName).First<string>();
        }

        /// <summary>
        /// 获取表架构查询语句
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>表架构查询语句</returns>
        public string GetSchemaQuerySql(string tableName)
        {
            StringBuilder sbSQL = new StringBuilder();
            sbSQL.AppendLine("SELECT ");
            sbSQL.AppendLine("    UTC.TABLE_NAME,");
            sbSQL.AppendLine("    UTC.COLUMN_ID,");
            sbSQL.AppendLine("    UTC.COLUMN_NAME,");
            sbSQL.AppendLine("    UTC.DATA_TYPE,");
            sbSQL.AppendLine("    UTC.DATA_LENGTH,");
            sbSQL.AppendLine("    UTC.DATA_DEFAULT,");
            sbSQL.AppendLine("    UTC.NULLABLE,");
            sbSQL.AppendLine("    UCC.COMMENTS,");
            sbSQL.AppendLine("    TB_CON.CONSTRAINT_TYPE ");
            sbSQL.AppendLine("FROM USER_TAB_COLUMNS UTC ");
            sbSQL.AppendLine("INNER JOIN USER_COL_COMMENTS UCC ON UTC.COLUMN_NAME=UCC.COLUMN_NAME ");
            sbSQL.AppendLine("LEFT JOIN( ");
            sbSQL.AppendLine("    SELECT ");
            sbSQL.AppendLine("        UCC.COLUMN_NAME,");
            sbSQL.AppendLine("        UC.CONSTRAINT_NAME,");
            sbSQL.AppendLine("        UC.CONSTRAINT_TYPE ");
            sbSQL.AppendLine("    FROM USER_CONS_COLUMNS UCC ");
            sbSQL.AppendLine("    INNER JOIN USER_CONSTRAINTS UC ON UCC.CONSTRAINT_NAME=UC.CONSTRAINT_NAME ");
            sbSQL.AppendFormat("    WHERE UC.TABLE_NAME='{0}' AND (UC.CONSTRAINT_TYPE='R' OR UC.CONSTRAINT_TYPE='P') \r\n", tableName.ToUpper());
            sbSQL.AppendLine(")TB_CON ON UTC.COLUMN_NAME=TB_CON.COLUMN_NAME ");
            sbSQL.AppendFormat("WHERE UTC.TABLE_NAME='{0}' AND UCC.TABLE_NAME='{0}'\r\n", tableName.ToUpper());
            sbSQL.AppendLine("ORDER BY UTC.COLUMN_ID ASC");
            return sbSQL.ToString();
        }
    }
}
