using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using System.Data.Common;

namespace Brilliant.ORM
{
    /// <summary>
    /// SQLite数据访问对象
    /// </summary>
    public class SQLite : DataProviderBase, IDataProvider
    {
        public override PagedType DataPagedType
        {
            get { return PagedType.Limit; }
        }

        public SQLite(string connectionString)
            : base(connectionString)
        {
        }

        protected override DbConnection GetConnection()
        {
            return new SQLiteConnection(base.ConnectionString);
        }

        protected override DbCommand GetCommand()
        {
            return new SQLiteCommand();
        }

        protected override DbDataAdapter GetDataAdapter()
        {
            return new SQLiteDataAdapter();
        }

        public override IDbDataParameter GetParameter()
        {
            return new SQLiteParameter();
        }

        public override IDataProvider GetDataProvider()
        {
            return new SQLite(base.ConnectionString);
        }
    }
}
