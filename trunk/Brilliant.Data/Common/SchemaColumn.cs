using System;

namespace Brilliant.Data.Common
{
    /// <summary>
    /// 表字段
    /// </summary>
    [Serializable]
    public class SchemaColumn
    {
        /// <summary>
        /// 字段序号
        /// </summary>
        public int ColumnIndex { get; set; }

        /// <summary>
        /// 字段名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段名称（首字母小写）
        /// </summary>
        public string ColumnNameLower { get; set; }

        /// <summary>
        /// 字段长度
        /// </summary>
        public int ColumnLength { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public string ColumnDesc { get; set; }

        /// <summary>
        /// 字段类型
        /// </summary>
        public string ColumnType { get; set; }

        /// <summary>
        /// 字段默认值
        /// </summary>
        public string ColumnDefaultValue { get; set; }

        /// <summary>
        /// 是否为空
        /// </summary>
        public bool IsNull { get; set; }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPK { get; set; }

        /// <summary>
        /// 是否是外键
        /// </summary>
        public bool IsFK { get; set; }

        /// <summary>
        /// 外键表明称
        /// </summary>
        public string FkTableName { get; set; }

        /// <summary>
        /// 外键表名称（首字母小写）
        /// </summary>
        public string FKTableNameLower { get; set; }

        /// <summary>
        /// CSharp类型
        /// </summary>
        public string CSharpType { get; set; }

        /// <summary>
        /// CSharp类型解析器
        /// </summary>
        public string CSharpTypeParser { get; set; }

        /// <summary>
        /// Java类型
        /// </summary>
        public string JavaType { get; set; }

        /// <summary>
        /// Java类型解析器
        /// </summary>
        public string JavaTypeParser { get; set; }

        /// <summary>
        /// SqlDbType
        /// </summary>
        public string SqlDbType { get; set; }
    }
}
