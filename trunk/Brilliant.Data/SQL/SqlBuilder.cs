using System.Text;

namespace Brilliant.Data
{
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
