using System;
using System.Linq;
using System.Linq.Expressions;

namespace Brilliant.Data.Entity
{
    /// <summary>
    /// 实体查询Provider
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class EntityQueryProvider<T> : IQueryProvider
    {
        private Type typeTemp;
        private SqlBuilder sqlTemp;

        /// <summary>
        /// 解析表达式
        /// </summary>
        private void Parse(Expression expression, SqlBuilder sb)
        {
            if (expression != null)
            {
                if (expression.NodeType == ExpressionType.Call)
                {
                    var ex = ((MethodCallExpression)expression);
                    if (ex.Method.DeclaringType == typeof(string) && ex.Method.Name == "Contains")
                    {
                        Parse(ex.Object, sb);
                        sb.Append(" LIKE ");
                        sb.Append("'%'+");
                        Parse(ex.Arguments[0], sb);
                        sb.Append("+'%'");
                    }
                    else if (ex.Method.DeclaringType == typeof(Enumerable) && ex.Method.Name == "Contains")
                    {
                        if (ex.Arguments[0].Type == typeof(string))
                        {
                            Parse(ex.Arguments[0], sb);
                            sb.Append(" LIKE ");

                            sb.Append("'%'+");
                            Parse(ex.Arguments[1], sb);
                            sb.Append("+'%'");

                        }
                        else
                        {
                            Parse(ex.Arguments[1], sb);
                            sb.Append(" IN (");
                            if (ex.Arguments[0].Type.IsArray)
                            {
                                Array objAry = Expression.Lambda(ex.Arguments[0]).Compile().DynamicInvoke() as Array;
                                if (objAry != null)
                                {
                                    for (int i = 0; i < objAry.Length; i++)
                                    {
                                        sb.Append("'");
                                        Parse(Expression.Constant(objAry.GetValue(i)), sb);
                                        if (i != objAry.Length - 1)
                                        {
                                            sb.Append(",");
                                        }
                                    }
                                }

                            }
                            else
                            {
                                Parse(ex.Arguments[0], sb);
                            }
                            sb.Append(") ");
                        }
                    }
                    else if (ex.Method.DeclaringType == typeof(Queryable) && ex.Method.Name == "Where")
                    {
                        sb.Operator = SqlOperator.Where;
                        Parse(ex.Arguments[1], sb);
                    }
                    else if (ex.Method.DeclaringType == typeof(Queryable) && ex.Method.Name == "FirstOrDefault")
                    {
                        if (ex.Arguments.Count > 1)
                        {
                            sb.Take = 1;
                            sb.Operator = SqlOperator.Where;
                            for (int i = 1; i < ex.Arguments.Count; i++)
                            {
                                Parse(ex.Arguments[i], sb);
                            }
                        }
                        else
                        {
                            sb.Take = 1;
                            SqlOperator oldOperate = sb.Operator;
                            sb.Operator = SqlOperator.None;
                            Parse(ex.Arguments[0], sb);
                            sb.Operator = oldOperate;
                        }
                    }
                    else if (ex.Method.DeclaringType == typeof(Enumerable) && ex.Method.Name == "ToList")
                    {
                        if (ex.Arguments.Count > 1)
                        {
                            sb.Operator = SqlOperator.Where;
                            for (int i = 1; i < ex.Arguments.Count; i++)
                            {
                                Parse(ex.Arguments[i], sb);
                            }
                        }
                        else
                        {
                            SqlOperator oldOperate = sb.Operator;
                            sb.Operator = SqlOperator.None;
                            Parse(ex.Arguments[0], sb);
                            sb.Operator = oldOperate;
                        }
                    }
                    else if (ex.Method.DeclaringType == typeof(Queryable) && ex.Method.Name == "Take")
                    {
                        Parse(ex.Arguments[0], sb);
                        sb.Operator = SqlOperator.Take;
                        Parse(ex.Arguments[1], sb);
                    }
                    else if (ex.Method.DeclaringType == typeof(Queryable) && ex.Method.Name == "Skip")
                    {
                        Parse(ex.Arguments[0], sb);
                        sb.Operator = SqlOperator.Skip;
                        Parse(ex.Arguments[1], sb);

                    }
                    else if (ex.Method.DeclaringType == typeof(Queryable) && (ex.Method.Name == "OrderBy" || ex.Method.Name == "ThenBy"))
                    {
                        Parse(ex.Arguments[0], sb);
                        sb.Operator = SqlOperator.OrderByAsc;
                        Parse(ex.Arguments[1], sb);

                    }
                    else if (ex.Method.DeclaringType == typeof(Queryable) && (ex.Method.Name == "OrderByDescending" || ex.Method.Name == "ThenByDescending"))
                    {

                        Parse(ex.Arguments[0], sb);
                        sb.Operator = SqlOperator.OrderByDesc;

                        Parse(ex.Arguments[1], sb);

                    }
                    else if (ex.Method.DeclaringType == typeof(Queryable) && ex.Method.Name == "Select")
                    {
                        sb.Operator = SqlOperator.From;
                        Parse(ex.Arguments[0], sb);
                        sb.Operator = SqlOperator.Select;
                        Parse(ex.Arguments[1], sb);

                    }
                    else if (ex.Object != null && ex.Method.Name == "ToString" && ex.Arguments.Count == 0)
                    {
                        sb.Append("CAST(");
                        Parse(ex.Object, sb);
                        sb.Append(" AS NVARCHAR)");
                    }
                    else if (ex.Object != null && ex.Method.Name == "Equals" && ex.Arguments.Count == 1)
                    {
                        Parse(ex.Object, sb);
                        sb.Append("=");
                        Parse(ex.Arguments[0], sb);
                    }
                    else
                    {
                        Parse(ex.Object, sb);
                        foreach (var arg in ex.Arguments)
                        {
                            Parse(arg, sb);
                        }
                    }
                }
                else if (expression.NodeType == ExpressionType.Equal)
                {
                    var ex = ((BinaryExpression)expression);
                    Parse(ex.Left, sb);
                    sb.Append("=");
                    ParseValue(ex.Right, sb);
                }
                else if (expression.NodeType == ExpressionType.LessThan)
                {
                    var ex = ((BinaryExpression)expression);
                    Parse(ex.Left, sb);
                    sb.Append("<");
                    ParseValue(ex.Right, sb);
                }
                else if (expression.NodeType == ExpressionType.LessThanOrEqual)
                {
                    var ex = ((BinaryExpression)expression);
                    Parse(ex.Left, sb);
                    sb.Append("<=");
                    ParseValue(ex.Right, sb);
                }
                else if (expression.NodeType == ExpressionType.GreaterThan)
                {
                    var ex = ((BinaryExpression)expression);
                    Parse(ex.Left, sb);
                    sb.Append(">");
                    ParseValue(ex.Right, sb);
                }
                else if (expression.NodeType == ExpressionType.GreaterThanOrEqual)
                {
                    var ex = ((BinaryExpression)expression);
                    Parse(ex.Left, sb);
                    sb.Append(">=");
                    ParseValue(ex.Right, sb);
                }
                else if (expression.NodeType == ExpressionType.Conditional)
                {
                    var ex = ((ConditionalExpression)expression);
                    sb.Append("(CASE WHEN ");
                    Parse(ex.Test, sb);
                    sb.Append(" THEN ");
                    Parse(ex.IfTrue, sb);
                    if (ex.IfFalse != null)
                    {
                        sb.Append(" ELSE ");
                        Parse(ex.IfFalse, sb);
                    }
                    sb.Append(" END)");
                }
                else if (expression.NodeType == ExpressionType.MemberAccess)
                {
                    var ex = ((MemberExpression)expression);

                    if (ex.Member.DeclaringType == typeof(string) && ex.Member.Name == "Length")
                    {
                        sb.Append("LEN(");
                        Parse(ex.Expression, sb);
                        sb.Append(")");
                    }
                    else
                    {
                        sb.Append(ex.Member.Name);
                        Parse(ex.Expression, sb);
                    }
                }
                else if (expression.NodeType == ExpressionType.Constant)
                {
                    var ex = ((ConstantExpression)expression);

                    if (ex.Value == null)
                    {
                        sb.Append("null");
                    }
                    else if (ex.Value is int || ex.Value is long || ex.Value is short || ex.Value is byte || ex.Value is double || ex.Value is decimal || ex.Value is float || ex.Value is ushort || ex.Value is uint || ex.Value is ulong || ex.Value is sbyte)
                    {
                        sb.Append(ex.Value);
                    }
                    else if (ex.Value is DateTime)
                    {
                        sb.Append(ex.Value);
                    }
                    else if (ex.Value is bool)
                    {
                        sb.Append(((bool)ex.Value) ? 1 : 0);
                    }
                    else if (ex.Value is string || ex.Value is char)
                    {
                        sb.Append("'");
                        sb.Append(ex.Value);
                        sb.Append("'");
                    }
                }
                else if (expression.NodeType == ExpressionType.Quote)
                {
                    var ex = ((UnaryExpression)expression);
                    Parse(ex.Operand, sb);
                }
                else if (expression.NodeType == ExpressionType.Lambda)
                {
                    var ex = ((LambdaExpression)expression);
                    Parse(ex.Body, sb);
                }
                else if (expression.NodeType == ExpressionType.And)
                {
                    var ex = ((BinaryExpression)expression);
                    Parse(ex.Left, sb);
                    sb.Append(" & ");
                    Parse(ex.Right, sb);
                }
                else if (expression.NodeType == ExpressionType.AndAlso)
                {
                    var ex = ((BinaryExpression)expression);
                    sb.Append("(");
                    Parse(ex.Left, sb);
                    sb.Append(" AND ");
                    Parse(ex.Right, sb);
                    sb.Append(")");
                }
                else if (expression.NodeType == ExpressionType.Or)
                {
                    var ex = ((BinaryExpression)expression);
                    Parse(ex.Left, sb);
                    sb.Append(" | ");
                    Parse(ex.Right, sb);
                }
                else if (expression.NodeType == ExpressionType.OrElse)
                {
                    var ex = ((BinaryExpression)expression);
                    sb.Append("(");
                    Parse(ex.Left, sb);
                    sb.Append(" OR ");
                    Parse(ex.Right, sb);
                    sb.Append(")");
                }
                else if (expression.NodeType == ExpressionType.Add)
                {
                    var ex = ((BinaryExpression)expression);
                    typeTemp = ex.Type;
                    sb.Append("(");
                    Parse(ex.Left, sb);
                    sb.Append("+");
                    Parse(ex.Right, sb);
                    sb.Append(")");
                    typeTemp = null;
                }
                else if (expression.NodeType == ExpressionType.Convert)
                {
                    var ex = ((UnaryExpression)expression);
                    if (typeTemp == typeof(string))
                    {
                        sb.Append("'");
                        Parse(ex.Operand, sb);
                        sb.Append("'");
                    }
                    else
                    {
                        Parse(ex.Operand, sb);
                    }
                }
                else
                {
                }
            }
        }

        /// <summary>
        /// 解析表达式的值
        /// </summary>
        private void ParseValue(Expression expression, SqlBuilder sb)
        {
            if (expression.NodeType != ExpressionType.Constant)
            {
                object obj = Expression.Lambda(expression).Compile().DynamicInvoke();
                ConstantExpression ce = Expression.Constant(obj);
                Parse(ce, sb);
            }
            else
            {
                Parse(expression, sb);
            }
        }

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <typeparam name="TElement">元素类型</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>查询对象</returns>
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new EntityQuery<TElement>(expression, this);
        }

        /// <summary>
        /// 创建查询对象
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <returns>查询对象</returns>
        public IQueryable CreateQuery(Expression expression)
        {
            return CreateQuery<object>(expression);
        }

        /// <summary>
        /// 执行表达式树
        /// </summary>
        /// <typeparam name="TResult">执行结果</typeparam>
        /// <param name="expression">表达式树</param>
        /// <returns>执行结果</returns>
        public TResult Execute<TResult>(Expression expression)
        {
            TableAttribute table = (TableAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute));
            SqlBuilder sql = new SqlBuilder();
            sql.InnerSql = sqlTemp;
            sql.From = table == null ? typeof(T).Name : table.Name;
            Parse(expression, sql);
            sqlTemp = sql;
            return default(TResult);
        }

        /// <summary>
        /// 执行表达式树
        /// </summary>
        /// <param name="expression">执行结果</param>
        /// <returns>执行结果</returns>
        public object Execute(Expression expression)
        {
            return Execute<object>(expression);
        }

        /// <summary>
        /// 将表达式树解析结果序列化字符串
        /// </summary>
        /// <returns>解析结果</returns>
        public override string ToString()
        {
            return sqlTemp.ToString();
        }
    }
}
