using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace Brilliant.ORM
{
    /// <summary>
    /// SqlServer数据库访问对象
    /// </summary>
    public class SqlServer : DataProviderBase
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
    }
}
