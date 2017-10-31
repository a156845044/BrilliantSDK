using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliant.ORM
{
    /// <summary>
    /// 将某个类指定为与数据库表相关联的实体类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 表名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public TableAttribute() { }
    }

    /// <summary>
    /// 将类与数据库表中的列相关联
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public ColumnAttribute() { }

        /// <summary>
        /// 获取或设置列名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 AutoSync 枚举
        /// </summary>
        public AutoSync AutoSync { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示列是否可包含 null 值
        /// </summary>
        public bool CanBeNull { get; set; }

        /// <summary>
        /// 获取或设置数据库列的类型
        /// </summary>
        public string DbType { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示列是否为数据库中的计算列
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示列是否包含数据库自动生成的值
        /// </summary>
        public bool IsDbGenerated { get; set; }

        /// <summary>
        /// 获取或设置一个值，该值指示该类成员是否表示作为表的整个主键或部分主键的列
        /// </summary>
        public bool IsPrimaryKey { get; set; }
    }

    /// <summary>
    /// 指示运行时如何在执行插入或更新操作后检索值
    /// </summary>
    public enum AutoSync
    {
        /// <summary>
        /// 自动选择值
        /// </summary>
        Default = 0,
        /// <summary>
        /// 始终返回值
        /// </summary>
        Always = 1,
        /// <summary>
        /// 从不返回值
        /// </summary>
        Never = 2,
        /// <summary>
        /// 仅在执行插入操作后返回值
        /// </summary>
        OnInsert = 3,
        /// <summary>
        /// 仅在执行更新操作后返回值
        /// </summary>
        OnUpdate = 4,
    }
}
