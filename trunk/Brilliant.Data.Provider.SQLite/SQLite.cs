using Brilliant.Data.Common;
using Brilliant.Data.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Brilliant.Data.Provider
{
    /// <summary>
    /// SQLite数据访问对象
    /// </summary>
    public class SQLite : DataProviderBase, ISchemaProvider
    {
        public override PagedType DataPagedType
        {
            get { return PagedType.Limit; }
        }

        public SQLite()
        {
        }

        public SQLite(string connectionString)
            : base(connectionString)
        {
        }

        protected override DbConnection GetConnection()
        {
            return new SQLiteConnection(base.ConnectionString);
        }

        protected override DbCommand GetCommand()
        {
            return new SQLiteCommand();
        }

        protected override DbDataAdapter GetDataAdapter()
        {
            return new SQLiteDataAdapter();
        }

        public override IDbDataParameter GetParameter()
        {
            return new SQLiteParameter();
        }

        public override IDataProvider GetDataProvider()
        {
            return new SQLite(this.ConnectionString);
        }

        public IList<DboBase> GetDbList()
        {
            string path = ConnectionString.Substring(0, ConnectionString.IndexOf(";"));
            path = path.Substring(0, path.LastIndexOf("\\")).Replace("Data Source=", "");
            FileInfo[] files = this.GetFiles(path, "*.db", "*.s*db");
            List<DboBase> list = new List<DboBase>();
            foreach (FileInfo file in files)
            {
                DboBase entity = new DboBase();
                entity.DboId = file.Name;
                entity.DboName = file.Name;
                list.Add(entity);
            }
            return list;
        }

        private FileInfo[] GetFiles(string dirPath, params string[] searchPatterns)
        {
            if (searchPatterns.Length <= 0)
            {
                return null;
            }
            List<FileInfo> list = new List<FileInfo>();
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            foreach (string pattern in searchPatterns)
            {
                list.AddRange(dir.GetFiles(pattern));
            }
            return list.ToArray();
        }

        public IList<DboProc> GetProcList()
        {
            return new List<DboProc>();
        }

        public IList<DboTable> GetTableList()
        {
            return Query<DboTable>.From("SELECT name AS DboId,name AS DboName FROM sqlite_master WHERE type='table' ORDER BY name ASC").ToList();
        }

        public IList<DboView> GetViewList()
        {
            return Query<DboView>.From("SELECT name AS DboId,name AS DboName FROM sqlite_master WHERE type='view' ORDER BY name ASC").ToList();
        }

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
                    string colType = Convert.ToString(dr["Type"]);
                    string colLenght = Regex.Match(colType, "\\(\\d+\\)", RegexOptions.IgnoreCase).Value;
                    model.ColumnIndex = Convert.ToInt32(dr["cid"]);
                    model.ColumnName = TypeMapper.ConvertToUpper(Convert.ToString(dr["name"]));
                    model.ColumnNameLower = TypeMapper.ConvertToLower(model.ColumnName);
                    model.ColumnType = String.IsNullOrEmpty(colLenght) ? colType : colType.Replace(colLenght, "");
                    model.ColumnDefaultValue = Convert.ToString(dr["dflt_value"]);
                    model.ColumnLength = String.IsNullOrEmpty(colLenght) ? 99 : Convert.ToInt32(colLenght.Replace("(", "").Replace(")", ""));
                    model.IsIdentity = false;// Convert.ToString(dr["Identity"]) == "T" ? true : false;
                    model.IsPK = Convert.ToString(dr["pk"]) == "1" ? true : false;
                    model.IsFK = false;//Convert.ToString(dr["pk"]) == "0" ? true : false;
                    model.FkTableName = String.Empty;// Convert.ToString(dr["ForeignKeyTable"]);
                    model.FKTableNameLower = String.Empty;// TypeMap.ConvertToLower(model.FkTableName);
                    model.IsNull = Convert.ToString(dr["notnull"]) == "99" ? true : false;
                    model.ColumnDesc = String.Empty;

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
            return String.Empty;
        }

        /// <summary>
        /// 获取表架构查询语句
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>表架构查询语句</returns>
        public string GetSchemaQuerySql(string tableName)
        {
            return String.Format("pragma table_info('{0}')", tableName);
        }
    }
}
