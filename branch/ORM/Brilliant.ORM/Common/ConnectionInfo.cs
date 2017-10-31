using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliant.ORM
{
    /// <summary>
    /// 连接信息
    /// </summary>
    public class ConnectionInfo
    {
        /// <summary>
        /// 数据源
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 数据库
        /// </summary>
        public string DataBase { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }

        /// <summary>
        /// 连接字符串格式
        /// </summary>
        public string ConnectionStringFormat { get; set; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 数据库访问对象类型
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// 数据库访问对象
        /// </summary>
        public IDataProvider DataProvider { get; set; }

        ///// <summary>
        ///// 数据库架构访问对象
        ///// </summary>
        //public ISchemaProvider SchemaProvider { get; set; }
    }
}
