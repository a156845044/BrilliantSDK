/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是XML文件操作类。
 * 
 * 作者：zwk       时间：2013-11-19
 * ========================================================================
 */
using System;
using System.Data;
using System.Xml;
using System.Collections.Generic;

namespace Brilliant.Utility
{
    /// <summary>
    /// XML文件操作类
    /// </summary>
    public class XmlHelper
    {
        private static XmlDocument doc = new XmlDocument();
        private static string prefix = "pf";
        private static string xmlNamespace = "http://schemas.microsoft.com/developer/msbuild/2003";

        /// <summary>
        /// 命名空间前缀
        /// </summary>
        public static string Prefix
        {
            get { return XmlHelper.prefix; }
            set { XmlHelper.prefix = value; }
        }

        /// <summary>
        /// 命名空间
        /// </summary>
        public static string XmlNamespace
        {
            get { return XmlHelper.xmlNamespace; }
            set { XmlHelper.xmlNamespace = value; }
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="docPath">Xml文件路径</param>
        /// <param name="xpath">xpath查询语句</param>
        /// <returns>节点</returns>
        public static XmlNode GetNode(string docPath, string xpath)
        {
            doc.Load(docPath);
            return doc.SelectSingleNode(xpath);
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="docPath">Xml文件路径</param>
        /// <param name="xpath">xpath查询语句</param>
        /// <param name="xmlNamespace">Xml引用空间（为空使用默认引用空间）</param>
        /// <returns>节点</returns>
        public static XmlNode GetNode(string docPath, string xpath, string xmlNamespace)
        {
            doc.Load(docPath);
            if (!String.IsNullOrEmpty(xmlNamespace))
            {
                XmlHelper.xmlNamespace = xmlNamespace;
            }
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace(prefix, XmlHelper.xmlNamespace);
            return doc.SelectSingleNode(xpath, nsmgr);
        }

        /// <summary>
        /// 获取节点列表
        /// </summary>
        /// <param name="docPath">Xml文件路径</param>
        /// <param name="xpath">xpath查询语句</param>
        /// <returns>节点列表</returns>
        public static XmlNodeList GetNodes(string docPath, string xpath)
        {
            doc.Load(docPath);
            return doc.SelectNodes(xpath);
        }

        /// <summary>
        /// 获取节点列表
        /// </summary>
        /// <param name="docPath">Xml文件路径</param>
        /// <param name="xpath">xpath查询语句</param>
        /// <param name="xmlNamespace">Xml引用空间（为空使用默认引用空间）</param>
        /// <returns>节点列表</returns>
        public static XmlNodeList GetNodes(string docPath, string xpath, string xmlNamespace)
        {
            doc.Load(docPath);
            if (!String.IsNullOrEmpty(xmlNamespace))
            {
                XmlHelper.xmlNamespace = xmlNamespace;
            }
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace(prefix, XmlHelper.xmlNamespace);
            return doc.SelectNodes(xpath, nsmgr);
        }

        /// <summary>
        /// 获取节点值/节点属性值
        /// </summary>
        /// <param name="docPath">Xml文件路径</param>
        /// <param name="xpath">xpath查询语句</param>
        /// <param name="attribute">节点属性（为空返回节点值，否则返回节点属性值）</param>
        /// <returns>节点值/节点属性值</returns>
        public static string GetNodeValue(string docPath, string xpath, string attribute)
        {
            XmlNode xn = GetNode(docPath, xpath);
            if (xn == null)
            {
                return String.Empty;
            }
            if (xn.Attributes == null)
            {
                return String.Empty;
            }
            if (!String.IsNullOrEmpty(attribute))
            {
                return xn.Attributes[attribute].Value;
            }
            return xn.InnerText;
        }

        /// <summary>
        /// 获取节点值/节点属性值
        /// </summary>
        /// <param name="docPath">Xml文件路径</param>
        /// <param name="xpath">xpath查询语句</param>
        /// <param name="attribute">节点属性（为空返回节点值，否则返回节点属性值）</param>
        /// <param name="xmlNamespace">Xml引用空间（为空使用默认引用空间）</param>
        /// <returns>节点值/节点属性值</returns>
        public static string GetNodeValue(string docPath, string xpath, string attribute, string xmlNamespace)
        {
            XmlNode xn = GetNode(docPath, xpath, xmlNamespace);
            if (xn == null)
            {
                return String.Empty;
            }
            if (xn.Attributes == null)
            {
                return String.Empty;
            }
            if (!String.IsNullOrEmpty(attribute))
            {
                return xn.Attributes[attribute].Value;
            }
            return xn.InnerText;
        }

        /// <summary>
        /// 获取节点值列表/节点属性列表
        /// </summary>
        /// <param name="docPath">Xml文件路径</param>
        /// <param name="xpath">xpath查询语句</param>
        /// <param name="attribute">节点属性（为空返回节点值，否则返回节点属性值）</param>
        /// <returns>节点值/节点属性值列表</returns>
        public static IList<string> GetNodesValue(string docPath, string xpath, string attribute)
        {
            XmlNodeList xnl = GetNodes(docPath, xpath);
            IList<string> list = new List<string>();
            if (xnl != null)
            {
                foreach (XmlNode xn in xnl)
                {
                    if (xn.Attributes == null)
                    {
                        continue;
                    }
                    if (!String.IsNullOrEmpty(attribute))
                    {
                        list.Add(xn.Attributes[attribute].Value);
                    }
                    else
                    {
                        list.Add(xn.InnerText);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取节点值列表/节点属性列表
        /// </summary>
        /// <param name="docPath">Xml文件路径</param>
        /// <param name="xpath">xpath查询语句</param>
        /// <param name="attribute">节点属性（为空返回节点值，否则返回节点属性值）</param>
        /// <param name="xmlNamespace">Xml引用空间（为空使用默认引用空间）</param>
        /// <returns>节点值/节点属性值列表</returns>
        public static IList<string> GetNodesValue(string docPath, string xpath, string attribute, string xmlNamespace)
        {
            XmlNodeList xnl = GetNodes(docPath, xpath, xmlNamespace);
            IList<string> list = new List<string>();
            if (xnl != null)
            {
                foreach (XmlNode xn in xnl)
                {
                    if (xn.Attributes == null)
                    {
                        continue;
                    }
                    if (!String.IsNullOrEmpty(attribute))
                    {
                        list.Add(xn.Attributes[attribute].Value);
                    }
                    else
                    {
                        list.Add(xn.InnerText);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 设置节点的属性
        /// </summary>
        /// <param name="docPath">Xml文件路径</param>
        /// <param name="xpath">xpath查询语句</param>
        /// <param name="attribute">属性名</param>
        /// <param name="value">属性值</param>
        public static void SetNodeValue(string docPath, string xpath, string attribute, string value)
        {
            XmlNode xn = XmlHelper.GetNode(docPath, xpath);
            if (xn != null)
            {
                XmlElement xe = xn as XmlElement;
                xe.SetAttribute(attribute, value);
            }
        }

        /// <summary>
        /// 设置节点的属性
        /// </summary>
        /// <param name="docPath">Xml文件路径</param>
        /// <param name="xpath">xpath查询语句</param>
        /// <param name="attribute">属性名</param>
        /// <param name="value">属性值</param>
        /// <param name="xmlNamespace">Xml引用空间（为空使用默认引用空间）</param>
        public static void SetNodeValue(string docPath, string xpath, string attribute, string value, string xmlNamespace)
        {
            XmlNode xn = XmlHelper.GetNode(docPath, xpath, xmlNamespace);
            if (xn != null)
            {
                XmlElement xe = xn as XmlElement;
                xe.SetAttribute(attribute, value);
            }
        }
    }
}
