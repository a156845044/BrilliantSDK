using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;

namespace Brilliant.ORM
{
    /// <summary>
    /// 查询指令构建类
    /// </summary>
    public class SQL
    {
        #region 成员变量
        private List<IDbDataParameter> parameters;
        private string cmdTextTemp;
        private string cmdText;
        private CommandType cmdType;
        private int recordCount;
        #endregion

        #region 属性
        /// <summary>
        /// 查询指令
        /// </summary>
        public string CmdText
        {
            get { return cmdText; }
            set { cmdText = value; }
        }

        /// <summary>
        /// 指令类型（默认为Text）
        /// </summary>
        public CommandType CmdType
        {
            get { return cmdType; }
            set { cmdType = value; }
        }

        /// <summary>
        /// 查询语句参数列表
        /// </summary>
        public IDbDataParameter[] Parameters
        {
            get
            {
                if (parameters != null)
                {
                    return parameters.ToArray();
                }
                return null;
            }
        }

        /// <summary>
        /// 总记录条数
        /// </summary>
        public int RecordCount
        {
            get { return recordCount; }
        }
        #endregion

        #region 构造器
        /// <summary>
        /// 构造函数
        /// </summary>
        public SQL()
        {
            this.cmdType = CommandType.Text;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cmdText">查询指令</param>
        public SQL(string cmdText)
            : this()
        {
            this.CmdText = cmdText;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cmdText">查询指令</param>
        /// <param name="cmdType">指令类型</param>
        public SQL(string cmdText, CommandType cmdType)
            : this(cmdText)
        {
            this.cmdType = cmdType;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 构建查询指令参数
        /// </summary>
        /// <param name="cmdText">指令文本</param>
        /// <returns>查询指令参数</returns>
        public static SQL Build(string cmdText)
        {
            SQL sql = new SQL(cmdText);
            return sql;
        }

        /// <summary>
        /// 构建查询指令参数
        /// </summary>
        /// <param name="cmdText">指令文本</param>
        /// <param name="isProc">是否是存储过程</param>
        /// <returns>查询指令参数</returns>
        public static SQL Build(string cmdText, bool isProc)
        {
            SQL sql = new SQL(cmdText, isProc ? CommandType.StoredProcedure : CommandType.Text);
            return sql;
        }

        /// <summary>
        /// 构建查询指令参数
        /// </summary>
        /// <param name="cmdText">指令文本</param>
        /// <param name="parameters">指令参数</param>
        /// <returns>查询指令参数</returns>
        public static SQL Build(string cmdText, params object[] parameters)
        {
            SQL sql = new SQL();
            MatchCollection mc = Regex.Matches(cmdText, @"\?", RegexOptions.IgnoreCase);
            if (parameters.Length != mc.Count)
            {
                throw new Exception("参数长度不符");
            }
            sql.parameters = new List<IDbDataParameter>();
            StringBuilder sb = new StringBuilder(cmdText);
            string param = "@P";
            int i = 0;
            foreach (Match match in mc)
            {
                string str = param + i.ToString().PadLeft(2, '0');
                sb.Replace(match.Value, str, match.Index + i * 3, match.Length);
                sql.AddParameter(str, parameters[i]);
                i++;
            }
            sql.cmdText = sb.ToString();
            return sql;
        }

        /// <summary>
        /// 构建查询指令参数（存储过程）
        /// </summary>
        /// <param name="cmdText">指令文本</param>
        /// <param name="parameterNames">指令参数名称</param>
        /// <param name="parameters">指令参数</param>
        /// <returns>查询指令参数</returns>
        public static SQL Build(string cmdText, string[] parameterNames, params object[] parameters)
        {
            SQL sql = new SQL(cmdText, CommandType.StoredProcedure);
            if (parameterNames.Length != parameters.Length)
            {
                throw new Exception("参数长度与参数名不符");
            }
            sql.parameters = new List<IDbDataParameter>();
            for (int i = 0; i < parameterNames.Length; i++)
            {
                sql.AddParameter(parameterNames[i], parameters[i]);
            }
            return sql;
        }

        /// <summary>
        /// 格式化查询指令
        /// </summary>
        /// <param name="cmdText">指令文本</param>
        /// <param name="parameters">指令参数</param>
        /// <returns>查询指令参数</returns>
        /// <remarks>时间：2014-09-05</remarks>
        public static SQL Format(string cmdText, params object[] parameters)
        {
            SQL sql = new SQL(String.Format(cmdText, parameters));
            return sql;
        }

        /// <summary>
        /// 限制返回的结果记录条数
        /// </summary>
        /// <param name="pageSize">每页显示的记录条数</param>
        /// <param name="pageNumber">当前页码</param>
        /// <returns>查询指令参数</returns>
        public SQL Limit(int pageSize, int pageNumber)
        {
            int startIndex = 0;
            int endIndex = 0;
            this.cmdTextTemp = cmdText; //将查询指令拷贝副本
            switch (DBHelper.DataPagedType)
            {
                case PagedType.Limit:
                    SetRecordCount();
                    startIndex = (pageNumber - 1) * pageSize;
                    endIndex = pageSize * pageNumber;
                    cmdText = String.Format("{0} limit {1},{2}", cmdText, startIndex, endIndex);
                    break;
                case PagedType.RowId:
                    SetRecordCount();
                    startIndex = (pageNumber - 1) * pageSize + 1;
                    endIndex = pageSize * pageNumber;
                    cmdText = String.Format("SELECT * FROM(SELECT ROWNUM RN,PT1.* FROM({0})PT1)PT2 WHERE PT2.RN BETWEEN {1} AND {2}", cmdText, startIndex, endIndex);
                    break;
                case PagedType.RowNumber:
                    startIndex = (pageNumber - 1) * pageSize + 1;
                    endIndex = pageSize * pageNumber;
                    Match match = Regex.Match(cmdText, @"SELECT\s*(DISTINCT)?", RegexOptions.IgnoreCase);
                    StringBuilder sb = new StringBuilder(cmdText);
                    cmdTextTemp = sb.Replace(match.Value, match.Value + " TOP 10000 ", match.Index, match.Length).ToString();
                    sb.Clear();
                    sb.Append(cmdText);
                    sb.Replace(match.Value, match.Value + " TOP 100 PERCENT ", match.Index, match.Length);
                    cmdText = sb.ToString();
                    SetRecordCount();
                    cmdText = String.Format("SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY (SELECT 1)) RN,PT1.* FROM({0})PT1)PT2 WHERE PT2.RN BETWEEN {1} AND {2}", cmdText, startIndex, endIndex);
                    break;
            }
            return this;
        }

        /// <summary>
        /// 设置总记录条数
        /// </summary>
        private void SetRecordCount()
        {
            this.cmdText = String.Format("SELECT COUNT(*) FROM({0})PT", this.cmdText);
            this.recordCount = Convert.ToInt32(DBHelper.DataProvider.ExecScalar(this));
            this.cmdText = this.cmdTextTemp; //重新启用副本
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">参数值</param>
        public void AddParameter(string parameterName, object value)
        {
            IDbDataParameter parameter = DBHelper.GetParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value == null ? DBNull.Value : value;
            if (this.parameters == null)
            {
                this.parameters = new List<IDbDataParameter>();
            }
            this.parameters.Add(parameter);
        }

        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="dbType">参数类型</param>
        /// <param name="size">参数大小</param>
        /// <param name="value">参数值</param>
        public void AddParameter(string parameterName, DbType dbType, int size, object value)
        {
            IDbDataParameter parameter = DBHelper.GetParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value;
            parameter.DbType = dbType;
            parameter.Size = size;
            parameter.Value = value == null ? DBNull.Value : value;
            if (this.parameters == null)
            {
                this.parameters = new List<IDbDataParameter>();
            }
            this.parameters.Add(parameter);
        }

        /// <summary>
        /// 显示查询指令语句
        /// </summary>
        /// <returns>查询指令语句</returns>
        public override string ToString()
        {
            if (!String.IsNullOrEmpty(this.cmdTextTemp))
            {
                return cmdTextTemp;
            }
            return cmdText;
        }
        #endregion
    }

    /// <summary>
    /// SQL关键词
    /// </summary>
    internal class SqlBuilder
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SqlBuilder()
        {
            this.Take = -1;
            this.Skip = -1;
        }

        /// <summary>
        /// 取符合条件的数据
        /// </summary>
        public int Take { get; set; }

        /// <summary>
        /// 跳过指定数据
        /// </summary>
        public int Skip { get; set; }

        /// <summary>
        /// Where条件
        /// </summary>
        public string Where { get; set; }

        /// <summary>
        /// 分组条件
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 选取字段
        /// </summary>
        public string[] Select { get; set; }

        /// <summary>
        /// 连接语句
        /// </summary>
        public SqlBuilder InnerSql { get; set; }

        /// <summary>
        /// 查询表名
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 排序条件
        /// </summary>
        public string OderBy { get; set; }

        /// <summary>
        /// 操作符
        /// </summary>
        public SqlOperator Operator { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// 追加SQL语句段
        /// </summary>
        /// <param name="value">字段值</param>
        public void Append(object value)
        {
            switch (Operator)
            {
                case SqlOperator.Where:
                    if (this.Where == null)
                        this.Where = "";
                    this.Where += string.Format("{0}", value);
                    break;
                case SqlOperator.Take:
                    this.Take = (int)value;
                    break;
                case SqlOperator.Skip:
                    this.Skip = (int)value;
                    break;
                case SqlOperator.Select:
                    this.Select = value as string[];
                    break;
                case SqlOperator.InnerSql:
                    this.InnerSql = (SqlBuilder)value;
                    break;
                case SqlOperator.Group:
                    break;
                case SqlOperator.From:
                    this.From = (string)value;
                    break;
                case SqlOperator.OrderByAsc:
                    if (this.Where == null)
                        this.Where = "";
                    this.OderBy = string.Format("{0}{1},", this.OderBy, value);
                    break;
                case SqlOperator.OrderByDesc:
                    if (this.Where == null)
                        this.Where = "";
                    this.OderBy = string.Format("{0}{1} desc,", this.OderBy, value);
                    break;
            }
        }

        /// <summary>
        /// 返回当前创建的SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT");
            if (this.Take >= 0)
                sb.AppendFormat(" TOP({0})", this.Take);

            if (this.Select != null && this.Select.Length > 0)
                sb.AppendFormat(" {0}", string.Join(",", this.Select));
            else
                sb.Append(" *");

            if (string.IsNullOrWhiteSpace(this.From) && this.InnerSql != null)
            {
                sb.Append("(");
                sb.Append(this.InnerSql.ToString());
                sb.Append(")");
            }
            else
            {
                sb.AppendFormat(" FROM {0}", this.From);
            }

            if (!string.IsNullOrWhiteSpace(Where))
            {
                sb.AppendFormat(" WHERE {0}", Where);
            }

            if (!string.IsNullOrWhiteSpace(this.OderBy))
            {
                sb.AppendFormat(" ORDER BY {0}", this.OderBy.TrimEnd(','));
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// SQL操作符
    /// </summary>
    internal enum SqlOperator
    {
        /// <summary>
        /// 默认值
        /// </summary>
        None,
        /// <summary>
        /// 取符合条件的数据
        /// </summary>
        Take,
        /// <summary>
        /// 跳过指定数据
        /// </summary>
        Skip,
        /// <summary>
        /// Where条件
        /// </summary>
        Where,
        /// <summary>
        /// 分组条件
        /// </summary>
        Group,
        /// <summary>
        /// 选取字段
        /// </summary>
        Select,
        /// <summary>
        /// 连接语句
        /// </summary>
        InnerSql,
        /// <summary>
        /// 查询表名
        /// </summary>
        From,
        /// <summary>
        /// 正序
        /// </summary>
        OrderByAsc,
        /// <summary>
        /// 倒序
        /// </summary>
        OrderByDesc
    }
}
