/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是对关键字类型方法进行扩展
 * 
 * 作者：dfq       时间：2016-06-16
 * ========================================================================
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brilliant.Utility
{
    /// <summary>
    /// 关键字方法扩展
    /// </summary>
    public static class MethodExtension
    {
        /// <summary>
        /// 将字符串转换为Int(安全转换)
        /// </summary>
        /// <param name="str">待转换字符串</param>
        /// <returns>当转换失败时返回0</returns>
        /// <remarks>作者：dfq 时间：2016.06.16</remarks>
        public static int ToInt(this string str)
        {
            int id = 0;
            int.TryParse(str, out id);
            return id;
        }

        /// <summary>
        /// 将对象转换为double(安全转换)
        /// </summary>
        /// <param name="obj">待转换对象</param>
        /// <returns>当转换失败时返回0</returns>
        /// <remarks>作者：dfq 时间：2016.09.21</remarks>
        public static double ToDouble(this object obj)
        {
            double result = 0;
            double.TryParse(obj.ToString(), out result);
            return result;
        }

        /// <summary>
        /// 将字符串转换为double(安全转换)
        /// </summary>
        /// <param name="obj">待转换对象</param>
        /// <returns>当转换失败时返回0</returns>
        /// <remarks>作者：dfq 时间：2016.09.22</remarks>
        public static double ToDouble(this string obj)
        {
            double result = 0;
            double.TryParse(obj, out result);
            return result;
        }

        /// <summary>
        /// 将对象转换为Int(安全转换)
        /// </summary>
        /// <param name="str">待转换字符串</param>
        /// <returns>当转换失败时返回0</returns>
        /// <remarks>作者：dfq 时间：2016.09.27</remarks>
        public static int ToInt(this object str)
        {
            int id = 0;
            int.TryParse(str.ToString(), out id);
            return id;
        }

        /// <summary>
        /// 将字符串转换为上整型(安全转换)
        /// </summary>
        /// <param name="str">待转换字符串</param>
        /// <returns>当转换失败时返回0</returns>
        /// <remarks>作者：dfq 时间：2016.12.8</remarks>
        public static long ToLong(this string str)
        {
            long id = 0;
            long.TryParse(str, out id);
            return id;
        }
    }
}
