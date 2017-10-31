using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Data.Common;

namespace Brilliant.ORM
{
    /// <summary>
    /// 数据访问对象
    /// </summary>
    public class Oracle : DataProviderBase, IDataProvider
    {
        public override PagedType DataPagedType
        {
            get { return PagedType.RowId; }
        }

        public Oracle(string connectionString)
            : base(connectionString)
        {
        }

        protected override DbConnection GetConnection()
        {
            return new OracleConnection(base.ConnectionString);
        }

        protected override DbCommand GetCommand()
        {
            return new OracleCommand();
        }

        protected override DbDataAdapter GetDataAdapter()
        {
            return new OracleDataAdapter();
        }

        public override IDbDataParameter GetParameter()
        {
            return new OracleParameter();
        }

        public override IDataProvider GetDataProvider()
        {
            return new Oracle(base.ConnectionString);
        }
    }
}
