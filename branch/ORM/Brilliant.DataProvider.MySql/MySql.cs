using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace Brilliant.ORM
{
    /// <summary>
    /// MySql数据访问对象
    /// </summary>
    public class MySql : DataProviderBase, IDataProvider
    {
        public override PagedType DataPagedType
        {
            get { return PagedType.Limit; }
        }

        public MySql(string connectionString)
            : base(connectionString)
        {
        }

        protected override DbConnection GetConnection()
        {
            return new MySqlConnection(base.ConnectionString);
        }

        protected override DbCommand GetCommand()
        {
            return new MySqlCommand();
        }

        protected override DbDataAdapter GetDataAdapter()
        {
            return new MySqlDataAdapter();
        }

        public override IDbDataParameter GetParameter()
        {
            return new MySqlParameter();
        }

        public override IDataProvider GetDataProvider()
        {
            return new MySql(base.ConnectionString);
        }
    }
}
