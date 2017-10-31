using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data.Common;

namespace Brilliant.ORM
{
    /// <summary>
    /// OleDb数据访问对象
    /// </summary>
    public class OleDb : DataProviderBase
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
        public OleDb()
        {

        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="connectionString">连接字符串</param>
        public OleDb(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// 返回一个新的Connection实例
        /// </summary>
        /// <returns>Connection实例</returns>
        protected override DbConnection GetConnection()
        {
            return new OleDbConnection(base.ConnectionString);
        }

        /// <summary>
        /// 返回一个新的Command实例
        /// </summary>
        /// <returns>Command实例</returns>
        protected override DbCommand GetCommand()
        {
            return new OleDbCommand();
        }

        /// <summary>
        /// 返回一个新的DataAdapter实例
        /// </summary>
        /// <returns>DataAdapter实例</returns>
        protected override DbDataAdapter GetDataAdapter()
        {
            return new OleDbDataAdapter();
        }

        /// <summary>
        /// 返回一个新的DataParameter对象实例
        /// </summary>
        /// <returns>DataParameter实例</returns>
        public override IDbDataParameter GetParameter()
        {
            return new OleDbParameter();
        }

        /// <summary>
        /// 返回一个新的IDataProvider实例
        /// </summary>
        /// <returns>DataProvider实例</returns>
        public override IDataProvider GetDataProvider()
        {
            return new OleDb(this.ConnectionString);
        }
    }
}
