using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Brilliant.ORM
{
    /// <summary>
    /// 数据访问对象接口
    /// </summary>
    public interface IDataProvider
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        string ConnectionString { get; }

        ///// <summary>
        ///// 是否开启事务处理
        ///// </summary>
        //bool Transaction { get; }

        /// <summary>
        /// 数据库分页类型
        /// </summary>
        PagedType DataPagedType { get; }

        /// <summary>
        /// 变更连接字符串
        /// </summary>
        /// <param name="connInfo">连接信息</param>
        /// <param name="formated">是否启用格式</param>
        /// <exception cref="System.Exception">formated为true时，连接字符串格式为空，则引发异常。</exception>
        void ChangeConnectionString(ConnectionInfo connInfo, bool formated);

        /// <summary>
        /// 检测连接是否可用
        /// </summary>
        /// <param name="connInfo">连接信息</param>
        /// <returns>true:可用 false:不可用</returns>
        bool CheckConnection(ConnectionInfo connInfo);

        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();

        /// <summary>
        /// 执行查询指令返回DataSet对象
        /// </summary>
        /// <param name="sql">查询指令</param>
        /// <returns>DataSet对象</returns>
        DataSet ExecDataSet(SQL sql);

        /// <summary>
        /// 执行查询指令返回DataReader对象
        /// </summary>
        /// <param name="sql">查询指令</param>
        /// <returns>DataReader对象</returns>
        IDataReader ExecDataReader(SQL sql);

        /// <summary>
        /// 执行查询指令返回第一行第一列的值
        /// </summary>
        /// <param name="sql">查询指令</param>
        /// <returns>第一行第一列的值</returns>
        object ExecScalar(SQL sql);

        /// <summary>
        /// 执行查询指令返回受影响行数
        /// </summary>
        /// <param name="sql">查询指令</param>
        /// <returns>受影响行数</returns>
        int ExecNonQuerry(SQL sql);

        /// <summary>
        /// 批量执行查询指令返回受影响行数(事务)
        /// </summary>
        /// <param name="sqlList">查询指令列表</param>
        /// <returns>受影响行数</returns>
        int ExecNonQuerry(List<SQL> sqlList);

        /// <summary>
        /// 返回一个新的DataParameter对象实例
        /// </summary>
        /// <returns>DataParameter实例</returns>
        IDbDataParameter GetParameter();

        /// <summary>
        /// 返回一个新的IDataProvider实例
        /// </summary>
        /// <returns>DataProvider实例</returns>
        IDataProvider GetDataProvider();
    }
}
