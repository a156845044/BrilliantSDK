/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是正则验证工具类。
 * 
 * 作者：zwk       时间：2013-11-12
 * ========================================================================
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Brilliant.Utility
{
    /// <summary>
    /// 正则验证工具类
    /// </summary>
    public static class ValidateHelper
    {
        /// <summary>
        /// 是否非负整数
        /// </summary>
        /// <param name="str">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsNonnegativeInteger(string str)
        {
            return Regex.IsMatch(str, @"^\d+$");
        }

        /// <summary>
        /// 是否整数
        /// </summary>
        /// <param name="str">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsInteger(string str)
        {
            return Regex.IsMatch(str, @"^-?\d+$");
        }

        /// <summary>
        /// 是否非负浮点数
        /// </summary>
        /// <param name="str">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsNonnegativeFloat(string str)
        {
            return Regex.IsMatch(str, @"^\d+(\.\d+)?$");
        }

        /// <summary>
        /// 是否浮点数
        /// </summary>
        /// <param name="str">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsFloat(string str)
        {
            return Regex.IsMatch(str, @"^(-?\d+)(\.\d+)?$");
        }

        /// <summary>
        /// 是否英文字母
        /// </summary>
        /// <param name="str">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsLetter(string str)
        {
            return Regex.IsMatch(str, @"^[A-Za-z]+$");
        }

        /// <summary>
        /// 是否大写字母
        /// </summary>
        /// <param name="str">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsUpperLetter(string str)
        {
            return Regex.IsMatch(str, @"^[A-Z]+$");
        }

        /// <summary>
        /// 是否小写字母
        /// </summary>
        /// <param name="str">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsLowerLetter(string str)
        {
            return Regex.IsMatch(str, @"^[a-z]+$");
        }

        /// <summary>
        /// 是否邮箱
        /// </summary>
        /// <param name="str">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsEmail(string str)
        {
            return Regex.IsMatch(str, @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$");
        }

        /// <summary>
        /// 是否链接地址
        /// </summary>
        /// <param name="str">待验证字符串</param>
        /// <returns>验证结果</returns>
        public static bool IsUrlAddress(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-z]+://(\w+(-\w+)*)(\.(\w+(-\w+)*))*(\?\S*)?$");
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        /// <remarks>作者：dfq 时间：2016-09-23</remarks>
        public static bool IsSafeSqlString(string str)
        {
            // return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
            //return !Regex.IsMatch(str, @"[;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
            return !Regex.IsMatch(str, @"[;|,|\/|\(|\)|\[|\]|\}|\{|%|!|\']");
        }

        /// <summary>
        /// 检查危险字符
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        /// <remarks>作者：dfq 时间：2016-09-23</remarks>
        public static string Filter(string sInput)
        {
            if (sInput == null || sInput == "")
                return null;
            string sInput1 = sInput.ToLower();
            string output = sInput;
            string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                throw new Exception("字符串中含有非法字符!");
            }
            else
            {
                output = output.Replace("'", "''");
            }
            return output;
        }

        /// <summary> 
        /// 检查过滤设定的危险字符
        /// </summary> 
        /// <param name="word">设置的危险字符</param>
        /// <param name="InText">要过滤的字符串 </param> 
        /// <returns>如果参数存在不安全字符，则返回true </returns> 
        /// <remarks>作者：dfq 时间：2016-09-23</remarks>
        public static bool SqlFilter(string word, string InText)
        {
            if (InText == null)
                return false;
            foreach (string i in word.Split('|'))
            {
                if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
