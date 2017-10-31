/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是Html文本操作工具类。
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
    /// Html文本操作工具类
    /// </summary>
    public static class HtmlHelper
    {
        /// <summary>
        /// 去除Html文本中的空格和换行
        /// </summary>
        /// <param name="htmlText">HTML源码</param>
        /// <returns>去除空格和换行后的Html文本</returns>
        public static string HtmlEncodeFilter(string htmlText)
        {
            return htmlText.Replace("\n", "<br/>").Replace(" ", "&nbsp;");
        }

        /// <summary>
        /// 获取指定ID的标签内容
        /// </summary>
        /// <param name="htmlText">HTML源码</param>
        /// <param name="id">标签ID</param>
        /// <returns>标签内容</returns>
        public static string GetElementById(string htmlText, string id)
        {
            string pattern = @"<([a-z]+)(?:(?!id)[^<>])*id=([""']?){0}\2[^>]*>(?>(?<o><\1[^>]*>)|(?<-o></\1>)|(?:(?!</?\1).))*(?(o)(?!))</\1>";
            pattern = string.Format(pattern, Regex.Escape(id));
            Match match = Regex.Match(htmlText, pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            return match.Success ? match.Value : "";
        }

        /// <summary>
        /// 通过class属性获取对应标签集合
        /// </summary>
        /// <param name="htmlText">HTML源码</param>
        /// <param name="className">class值</param>
        /// <returns>标签集合</returns>
        public static string[] GetElementsByClass(string htmlText, string className)
        {
            return GetElements(htmlText, "", className);
        }

        /// <summary>
        /// 通过标签名获取标签集合
        /// </summary>
        /// <param name="htmlText">HTML源码</param>
        /// <param name="tagName">标签名(如div)</param>
        /// <returns>标签集合</returns>
        public static string[] GetElementsByTagName(string htmlText, string tagName)
        {
            return GetElements(htmlText, tagName, "");
        }

        /// <summary>
        /// 通过同时指定标签名+class值获取标签集合
        /// </summary>
        /// <param name="htmlText">HTML源码</param>
        /// <param name="tagName">标签名</param>
        /// <param name="className">class值</param>
        /// <returns>标签集合</returns>
        public static string[] GetElementsByTagAndClass(string htmlText, string tagName, string className)
        {
            return GetElements(htmlText, tagName, className);
        }

        /// <summary>
        /// 通过同时指定标签名+class值获取标签集合
        /// </summary>
        /// <param name="htmlText">HTML源码</param>
        /// <param name="tagName">标签名</param>
        /// <param name="className">class值</param>
        /// <returns>标签集合</returns>
        private static string[] GetElements(string htmlText, string tagName, string className)
        {
            string pattern = "";
            if (tagName != "" && className != "")
            {
                pattern = @"<({0})(?:(?!class)[^<>])*class=([""']?){1}\2[^>]*>(?>(?<o><\1[^>]*>)|(?<-o></\1>)|(?:(?!</?\1).))*(?(o)(?!))</\1>";
                pattern = string.Format(pattern, Regex.Escape(tagName), Regex.Escape(className));
            }
            else if (tagName != "")
            {
                pattern = @"<({0})(?:[^<>])*>(?>(?<o><\1[^>]*>)|(?<-o></\1>)|(?:(?!</?\1).))*(?(o)(?!))</\1>";
                pattern = string.Format(pattern, Regex.Escape(tagName));
            }
            else if (className != "")
            {
                pattern = @"<([a-z]+)(?:(?!class)[^<>])*class=([""']?){0}\2[^>]*>(?>(?<o><\1[^>]*>)|(?<-o></\1>)|(?:(?!</?\1).))*(?(o)(?!))</\1>";
                pattern = string.Format(pattern, Regex.Escape(className));
            }
            if (pattern == "")
            {
                return new string[] { };
            }
            List<string> list = new List<string>();
            Regex reg = new Regex(pattern, RegexOptions.Singleline | RegexOptions.IgnoreCase);
            Match match = reg.Match(htmlText);
            while (match.Success)
            {
                list.Add(match.Value);
                match = reg.Match(htmlText, match.Index + match.Length);
            }
            return list.ToArray();
        }

        /// <summary>
        /// 根据表达式获取内容
        /// </summary>
        /// <param name="htmlText">HTML源码</param>
        /// <param name="expression">正则表达式</param>
        /// <returns>内容</returns>
        public static string[] GetListByHtml(string htmlText, string expression)
        {
            List<string> list = new List<string>();
            Regex r = new Regex(expression, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Match m = r.Match(htmlText);
            //int matchCount = 0;
            while (m.Success)
            {
                list.Add(m.Value);
                m = m.NextMatch();
            }
            return list.ToArray();
        }

        /// <summary>
        /// 去除html标签
        /// </summary>
        /// <param name="htmlText">HTML源码</param>
        /// <returns>移除标签后的文本</returns>
        public static string RemoveHtmlTags(string htmlText)
        {
            //删除脚本
            htmlText = Regex.Replace(htmlText, @"<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
            //删除HTML
            htmlText = Regex.Replace(htmlText, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"([\r\n])[\s]+", "", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"-->", "", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"<!--.*", "", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&(nbsp|#160);", "   ", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            htmlText = Regex.Replace(htmlText, @"&#(\d+);", "", RegexOptions.IgnoreCase);
            htmlText.Replace("<", "");
            htmlText.Replace(">", "");
            htmlText.Replace("\r\n", "");
            htmlText = htmlText.Trim();
            return htmlText;
        }

        ///// <summary>
        ///// 移除Html中的标记对
        ///// </summary>
        ///// <param name="htmlText">html文本</param>
        ///// <returns>移除标记对后的文本</returns>
        //public static string RemoveHtmlTags(string htmlText)
        //{
        //    return Regex.Replace(htmlText, "<[^>]*>", "");
        //}

        /// <summary>
        /// 取得HTML中所有图片的URL
        /// </summary>
        /// <param name="htmlText">HTML代码</param>
        /// <returns>图片的URL列表</returns>
        public static string[] GetHtmlImageUrlList(string htmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(htmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }

        /// <summary>
        /// 获取img的alt标签
        /// </summary>
        /// <param name="htmlText">HTML代码</param>
        /// <returns>Alt标签列表</returns>
        public static string[] GetHtmlAltList(string htmlText)
        {
            // 定义正则表达式用来匹配 img 标签
            Regex regImg = new Regex(@"<img\b[^<>]*?\balt[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgAlt>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串
            MatchCollection matches = regImg.Matches(htmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgAlt"].Value;
            return sUrlList;
        }

        /// <summary>
        /// 获取a标签的href属性列表
        /// </summary>
        /// <param name="htmlText">HTML代码</param>
        /// <returns>href属性列表</returns>
        public static string[] GetHtmlLinkList(string htmlText)
        {
            Regex regLink = new Regex("<a[^>]*?href=(['\"\"]?)(?<url>[^'\"\"\\s>]+)\\1[^>]*>");
            MatchCollection matches = regLink.Matches(htmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["url"].Value;
            return sUrlList;
        }
    }
}
