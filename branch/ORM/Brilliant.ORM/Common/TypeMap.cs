using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Brilliant.ORM
{
    /// <summary>
    /// 类型映射
    /// </summary>
    public class TypeMap
    {
        private const string DEFAULT_DATETIME = "1900-01-01";

        /// <summary>
        /// 默认日期
        /// </summary>
        public static DateTime DefaultDateTime
        {
            get { return Convert.ToDateTime(DEFAULT_DATETIME); }
        }

        /// <summary>
        /// 过滤Null值
        /// </summary>
        /// <param name="obj">原始值</param>
        /// <returns>过滤之后的值</returns>
        [Obsolete("该方法已过时，不在返回有意义的值")]
        public static object FilterNull(object obj)
        {
            if (obj == null)
            {
                return String.Empty;
            }
            return obj;
        }
    }
}
