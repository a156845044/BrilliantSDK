using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Odbc;
using System.Data.Common;

namespace Brilliant.ORM
{
    /// <summary>
    /// Odbc数据访问对象
    /// </summary>
    public class Odbc : DataProviderBase
    {
        /// <summary>
        /// 数据库分页类型
        /// </summary>
        public override PagedType DataPagedType
        {
            get { return PagedType.Common; }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public Odbc()
        {

        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public Odbc(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// 返回一个新的Connection实例
        /// </summary>
        /// <returns>Connection实例</returns>
        protected override DbConnection GetConnection()
        {
            return new OdbcConnection(base.ConnectionString);
        }

        /// <summary>
        /// 返回一个新的Command实例
        /// </summary>
        /// <returns>Command实例</returns>
        protected override DbCommand GetCommand()
        {
            return new OdbcCommand();
        }

        /// <summary>
        /// 返回一个新的DataAdapter实例
        /// </summary>
        /// <returns>DataAdapter实例</returns>
        protected override DbDataAdapter GetDataAdapter()
        {
            return new OdbcDataAdapter();
        }

        /// <summary>
        /// 返回一个新的DataParameter对象实例
        /// </summary>
        /// <returns>DataParameter对象</returns>
        public override IDbDataParameter GetParameter()
        {
            return new OdbcParameter();
        }

        /// <summary>
        /// 返回一个新的IDataProvider实例
        /// </summary>
        /// <returns>DataProvider实例</returns>
        public override IDataProvider GetDataProvider()
        {
            return new Odbc(this.ConnectionString);
        }
    }
}
