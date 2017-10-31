/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是资源路径转换工具类。
 * 
 * 作者：zwk       时间：2015-10-13
 * ========================================================================
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Brilliant.Utility
{
    /// <summary>
    /// 资源路径转换工具类
    /// </summary>
    public static class URIHelper
    {
        /// <summary>
        /// 取得网站的根目录的URL
        /// </summary>
        /// <returns>网站的根目录的URL</returns>
        /// <remarks>作者:dfq 时间:2014-09-19</remarks>
        public static string GetRootURL()
        {
            HttpRequest request = HttpContext.Current.Request;
            string AppPath = "";
            if (request != null)
            {
                string UrlAuthority = request.Url.GetLeftPart(UriPartial.Authority);
                if (request.ApplicationPath == null || request.ApplicationPath == "/")
                    AppPath = UrlAuthority;
                else
                    AppPath = UrlAuthority + request.ApplicationPath;
            }
            return AppPath;
        }


        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        ///  <remarks>作者：dfq 时间：2014.04.02</remarks>
        public static string GetMapPath(string strPath)
        {
            if (strPath.ToLower().StartsWith("http://"))
            {
                return strPath;
            }
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        /// <summary>
        /// URL字符编码
        /// </summary>
        /// <param name="str">待编码的字符</param>
        /// <returns>编码后的字符串</returns>
        /// <remarks>作者：dfq 时间：2015-11-24</remarks>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }

        /// <summary>
        ///  URL字符解码
        /// </summary>
        /// <param name="str">待解码的参数</param>
        /// <returns>解码后的字符串</returns>
        /// <remarks>作者：dfq 时间：2015-11-24</remarks>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(str);
        }
    }
}
