using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Brilliant.ORM
{
    /// <summary>
    /// 实体查询
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class EntityQuery<T> : IQueryable<T>, IOrderedQueryable<T>
    {
        private Expression expression; //表达式树
        private IQueryProvider provider; //QueryProvider对象

        /// <summary>
        /// 获取TEntity的类型
        /// </summary>
        public Type ElementType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// 获取当前执行的Expression
        /// </summary>
        public Expression Expression
        {
            get { return expression; }
        }

        /// <summary>
        /// 获取QueryProvider对象
        /// </summary>
        public IQueryProvider Provider
        {
            get { return provider; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public EntityQuery()
        {
            expression = Expression.Constant(this);
            this.provider = new EntityQueryProvider<T>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="expression">表达式树</param>
        /// <param name="provider">QueryProvider对象</param>
        public EntityQuery(Expression expression, IQueryProvider provider)
        {
            this.expression = expression;
            this.provider = provider;
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        public IEnumerator<T> GetEnumerator()
        {
            var enumModel = this.Provider.Execute<IEnumerable<T>>(this.Expression);
            if (enumModel != null)
            {
                foreach (var m in enumModel)
                {
                    yield return m;
                }
            }
        }

        /// <summary>
        /// 返回一个循环访问集合的枚举器
        /// </summary>
        /// <returns>枚举器</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return GetEnumerator();
        }

        /// <summary>
        /// 返回表达式解析后的SQL语句
        /// </summary>
        /// <returns>SQL语句</returns>
        public override string ToString()
        {
            this.Provider.Execute<IEnumerable<T>>(this.Expression);
            return this.provider.ToString();
        }
    }
}
