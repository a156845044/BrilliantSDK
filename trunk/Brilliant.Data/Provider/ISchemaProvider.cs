using Brilliant.Data.Common;
using System.Collections.Generic;

namespace Brilliant.Data.Provider
{
    /// <summary>
    /// 数据库架构访问对象接口
    /// </summary>
    public interface ISchemaProvider
    {
        /// <summary>
        /// 获取数据库列表
        /// </summary>
        /// <returns>数据库列表</returns>
        IList<DboBase> GetDbList();

        /// <summary>
        /// 获取存储过程列表
        /// </summary>
        /// <returns>存储过程列表</returns>
        IList<DboProc> GetProcList();

        /// <summary>
        /// 获取数据表列表
        /// </summary>
        /// <returns>数据表列表</returns>
        IList<DboTable> GetTableList();

        /// <summary>
        /// 获取视图列表
        /// </summary>
        /// <returns>视图列表</returns>
        IList<DboView> GetViewList();

        /// <summary>
        /// 获取数据表架构信息
        /// </summary>
        /// <param name="tableName">数据表名称</param>
        /// <returns>数据表架构信息</returns>
        SchemaTable GetSchemaInfo(string tableName);

        /// <summary>
        /// 获取表字段信息列表
        /// </summary>
        /// <param name="tableName">表明称</param>
        /// <returns>表字段信息列表</returns>
        IList<SchemaColumn> GetColumn(string tableName);

        /// <summary>
        /// 获取表架构查询语句
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>表架构查询语句</returns>
        string GetSchemaQuerySql(string tableName);

        /// <summary>
        /// 获取表描述信息
        /// </summary>
        /// <param name="tableName">表名称</param>
        /// <returns>表描述信息</returns>
        string GetTableDesc(string tableName);
    }
}
