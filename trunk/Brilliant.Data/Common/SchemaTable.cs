using System;
using System.Collections.Generic;

namespace Brilliant.Data.Common
{
    /// <summary>
    /// 表架构
    /// </summary>
    [Serializable]
    public class SchemaTable
    {
        /// <summary>
        /// 表序号
        /// </summary>
        public int TableIndex { get; set; }

        /// <summary>
        /// 表明称
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表明称（首字母小写）
        /// </summary>
        public string TableNameLower { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>
        public string TableDesc { get; set; }

        /// <summary>
        /// 表命名空间
        /// </summary>
        public string TableNameSpace { get; set; }

        /// <summary>
        /// 主键（多个主键只返回第一个）
        /// </summary>
        public SchemaColumn PK
        {
            get
            {
                if (PKList.Count > 0)
                {
                    return PKList[0];
                }
                return null;
            }
        }

        /// <summary>
        /// 主键列表
        /// </summary>
        public IList<SchemaColumn> PKList { get; set; }

        /// <summary>
        /// 外键列表
        /// </summary>
        public IList<SchemaColumn> FKList { get; set; }

        /// <summary>
        /// 字段列表
        /// </summary>
        public IList<SchemaColumn> ColumnList { get; set; }
    }
}
