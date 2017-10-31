using Brilliant.Data.Common;
using Brilliant.Data.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Brilliant.Data.Provider
{
    /// <summary>
    /// SqlServer数据库访问对象
    /// </summary>
    public class SqlServer : DataProviderBase, ISchemaProvider
    {
        /// <summary>
        /// 数据库分页类型
        /// </summary>
        public override PagedType DataPagedType
        {
            get { return PagedType.RowNumber; }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public SqlServer()
        {

        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public SqlServer(string connectionString)
            : base(connectionString)
        {

        }

        /// <summary>
        /// 返回一个新的Connection实例
        /// </summary>
        /// <returns>Connection实例</returns>
        protected override DbConnection GetConnection()
        {
            return new SqlConnection(base.ConnectionString);
        }

        /// <summary>
        /// 返回一个新的Command实例
        /// </summary>
        /// <returns>Command实例</returns>
        protected override DbCommand GetCommand()
        {
            return new SqlCommand();
        }

        /// <summary>
        /// 返回一个新的DataAdapter实例
        /// </summary>
        /// <returns>DataAdapter实例</returns>
        protected override DbDataAdapter GetDataAdapter()
        {
            return new SqlDataAdapter();
        }

        /// <summary>
        /// 返回一个新的DataParameter对象实例
        /// </summary>
        /// <returns>DataParameter对象</returns>
        public override IDbDataParameter GetParameter()
        {
            return new SqlParameter();
        }

        /// <summary>
        /// 返回一个新的IDataProvider实例
        /// </summary>
        /// <returns>DataProvider实例</returns>
        public override IDataProvider GetDataProvider()
        {
            return new SqlServer(this.ConnectionString);
        }

        /// <summary>
        /// 获取数据库列表
        /// </summary>
        public IList<DboBase> GetDbList()
        {
            return Query.From("SELECT dbid AS DboId,name AS DboName FROM master..SysDatabases WHERE name NOT IN('master','tempdb','model','msdb','ReportServer','ReportServerTempDB') ORDER BY name ASC")
                .ToList<DboBase>();
        }

        /// <summary>
        /// 获取存储过程列表
        /// </summary>
        public IList<DboProc> GetProcList()
        {
            return Query.From("SELECT object_id AS DboId,name AS DboName,type AS DboType FROM sys.objects WHERE type='p' AND name NOT IN('sp_alterdiagram','sp_creatediagram','sp_dropdiagram','sp_helpdiagramdefinition','sp_helpdiagrams','sp_renamediagram','sp_upgraddiagrams') ORDER BY name ASC")
                .ToList<DboProc>();
        }

        /// <summary>
        /// 获取数据表列表
        /// </summary>
        public IList<DboTable> GetTableList()
        {
            return Query.From()
                .Append("SELECT [DboId]=O.object_id,[DboName]=O.name,[DboDesc]=ISNULL(EP.value,N'') ")
                .Append("FROM sys.objects O ")
                .Append("LEFT JOIN sys.extended_properties EP ON O.object_id=EP.major_id AND minor_id=0 AND class=1 ")
                .Append("WHERE O.type='U' AND O.name NOT IN('sysdiagrams','dtproperities') ORDER BY O.name ASC")
                .ToList<DboTable>();
        }

        /// <summary>
        /// 获取视图列表
        /// </summary>
        public IList<DboView> GetViewList()
        {
            return Query.From()
                .Append("SELECT object_id AS DboId,name AS DboName,type AS DboType FROM sys.objects WHERE type='v' ORDER BY name ASC")
                .ToList<DboView>();
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
                    model.ColumnIndex = Convert.ToInt32(dr["Id"]);
                    model.ColumnName = TypeMapper.ConvertToUpper(Convert.ToString(dr["Name"]));
                    model.ColumnNameLower = TypeMapper.ConvertToLower(model.ColumnName);
                    model.ColumnType = Convert.ToString(dr["Type"]);
                    model.ColumnDefaultValue = TypeMapper.MapDefaultValue(Convert.ToString(dr["Default"]));
                    model.ColumnLength = Convert.ToInt32(dr["Length"]);
                    model.IsIdentity = Convert.ToString(dr["Identity"]) == "T" ? true : false;
                    model.IsPK = Convert.ToString(dr["PrimaryKey"]) == "T" ? true : false;
                    model.IsFK = Convert.ToString(dr["ForeignKey"]) == "T" ? true : false;
                    model.FkTableName = TypeMapper.ConvertToUpper(Convert.ToString(dr["ForeignKeyTable"]));
                    model.FKTableNameLower = TypeMapper.ConvertToLower(model.FkTableName);
                    model.IsNull = Convert.ToString(dr["IsNull"]) == "T" ? true : false;
                    model.ColumnDesc = Convert.ToString(dr["ColumnDesc"]);

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
            return Query.From()
            .Append("SELECT [Desc]=ISNULL(EP.value,N'') ")
            .Append("FROM sys.extended_properties EP INNER JOIN sys.objects O ON EP.major_id=O.object_id ")
            .Append("WHERE minor_id=0 AND class=1 AND O.name='{0}'", tableName)
            .First<string>();
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
            sbSQL.AppendLine("    [Id]=C.column_id,");
            sbSQL.AppendLine("    [Name]=C.name,");
            sbSQL.AppendLine("    [Type]=T.name,");
            sbSQL.AppendLine("    [Length]=C.max_length,");
            sbSQL.AppendLine("    [Identity]=CASE WHEN C.is_identity=1 THEN N'T'ELSE N'' END,");
            sbSQL.AppendLine("    [PrimaryKey]=ISNULL(PKInfo.PrimaryKey,N''),");
            sbSQL.AppendLine("    [ForeignKey]=CASE WHEN FKInfo.parent_column_id>0 THEN N'T'ELSE N'' END,");
            sbSQL.AppendLine("    [ForeignKeyTable]=ISNULL(FKInfo.name,N''),");
            sbSQL.AppendLine("    [IsNull]=CASE WHEN C.is_nullable=1 THEN N'T'ELSE N'' END,");
            sbSQL.AppendLine("    [Default]=ISNULL(DC.definition,N''),");
            sbSQL.AppendLine("    [ColumnDesc]=ISNULL(EP.value,N'') ");
            sbSQL.AppendLine("FROM sys.columns C ");
            sbSQL.AppendLine("INNER JOIN sys.objects O ON C.object_id=o.object_id AND O.type='U' AND O.is_ms_shipped=0 ");
            sbSQL.AppendLine("INNER JOIN sys.types T ON C.user_type_id=T.user_type_id ");
            sbSQL.AppendLine("LEFT JOIN sys.default_constraints DC ON C.object_id=DC.parent_object_id AND C.column_id=DC.parent_column_id AND C.default_object_id=DC.object_id ");
            sbSQL.AppendLine("LEFT JOIN sys.extended_properties EP ON EP.class=1 AND C.object_id=EP.major_id AND C.column_id=EP.minor_id ");
            sbSQL.AppendLine("LEFT JOIN (SELECT IC.object_id,IC.column_id,PrimaryKey=CASE WHEN I.is_primary_key=1 THEN N'T'ELSE N'' END FROM sys.indexes I INNER JOIN sys.index_columns IC ON I.[object_id]=IC.[object_id] AND I.index_id=IC.index_id)PKInfo ON PKInfo.object_id=C.object_id AND PKInfo.column_id=C.column_id ");
            sbSQL.AppendLine("LEFT JOIN (SELECT FKC.parent_object_id,FKC.parent_column_id,O.name FROM sys.foreign_key_columns FKC INNER JOIN sys.objects O ON FKC.referenced_object_id=O.object_id)FKInfo ON C.object_id=FKInfo.parent_object_id AND C.column_id=FKInfo.parent_column_id ");
            sbSQL.AppendFormat("WHERE O.name='{0}' ORDER BY Id ASC", tableName);
            return sbSQL.ToString();
        }
    }
}
