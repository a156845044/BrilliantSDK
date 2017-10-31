using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Brilliant.Data
{
    /// <summary>
    /// SQL类
    /// </summary>
    public class SQL
    {
        private StringBuilder _cmdText;
        private CommandType _cmdType;
        private IList<IDbDataParameter> _parameters;

        /// <summary>
        /// 查询指令
        /// </summary>
        public string CmdText
        {
            get { return _cmdText.ToString(); }
            set
            {
                _cmdText.Clear();
                _cmdText.Append(value);
            }
        }

        /// <summary>
        /// 查询指令类型
        /// </summary>
        public CommandType CmdType
        {
            get { return _cmdType; }
            set { _cmdType = value; }
        }

        /// <summary>
        /// 参数列表
        /// </summary>
        public IDbDataParameter[] Parameters
        {
            get { return _parameters.ToArray(); }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        public SQL()
        {
            this._cmdText = new StringBuilder();
            this._cmdType = CommandType.Text;
            this._parameters = new List<IDbDataParameter>();
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="cmdText">查询指令</param>
        public SQL(string cmdText)
            : this()
        {
            CmdText = cmdText;
        }

        /// <summary>
        /// 追加指定的查询指令
        /// </summary>
        /// <param name="cmdText">查询指令</param>
        public void Append(string cmdText)
        {
            this._cmdText.Append(cmdText);
        }

        /// <summary>
        /// 追加指定的查询指令
        /// </summary>
        /// <param name="cmdText">查询指令</param>
        /// <param name="parameters">查询参数（格式化参数在前，查询参数在后）</param>
        /// <example>
        /// Append("SELECT * FROM {0} WHERE Name=? ORDER BY {1} ASC","Person","Id","张三")
        /// 此案例中{0}格式化参数要全部放在?查询参数前面
        /// </example>
        public void Append(string cmdText, params object[] parameters)
        {
            int fmtCount = Regex.Matches(cmdText, @"{\d+}", RegexOptions.IgnoreCase).Count;
            this._cmdText.AppendFormat(cmdText, parameters);
            MatchCollection mc = Regex.Matches(CmdText, @"\?", RegexOptions.IgnoreCase);
            if (parameters.Length - fmtCount != mc.Count)
            {
                throw new Exception("参数长度不符");
            }
            string param = "@P";
            int i = 0;
            foreach (Match match in mc)
            {
                string str = param + i.ToString().PadLeft(2, '0');
                this._cmdText.Replace(match.Value, str, match.Index + i * 3, match.Length);
                this.AddParameter(str, parameters[i + fmtCount]);
                i++;
            }
        }

        /// <summary>
        /// 添加参数参数
        /// </summary>
        /// <param name="parameter">查询参数</param>
        public void AddParameter(IDbDataParameter parameter)
        {
            this._parameters.Add(parameter);
        }

        /// <summary>
        /// 添加查询参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">参数值</param>
        public void AddParameter(string parameterName, object value)
        {
            AddParameter(parameterName, value, DbType.String, 0);
        }

        /// <summary>
        /// 添加查询参数
        /// </summary>
        /// <param name="parameterName">参数名称</param>
        /// <param name="value">参数值</param>
        /// <param name="dbType">对应类型</param>
        /// <param name="size">参数大小</param>
        public void AddParameter(string parameterName, object value, DbType dbType, int size)
        {
            IDbDataParameter parameter = DBContext.DataProvider.GetParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = value == null ? DBNull.Value : value;
            parameter.DbType = dbType;
            parameter.Size = size;

            AddParameter(parameter);
        }

        /// <summary>
        /// 将此实例的值转换为 System.String
        /// </summary>
        /// <returns>其值与此实例相同的字符串</returns>
        public override string ToString()
        {
            return this.CmdText;
        }
    }
}
