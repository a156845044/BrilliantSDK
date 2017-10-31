using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Brilliant.ORM
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtensionMethod
    {
        /// <summary>
        /// 获取当前容器中实体的属性值
        /// </summary>
        /// <param name="container">扩展类</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static object GetProperty(this IDataItemContainer container, string propertyName)
        {
            return ((EntityBase)container.DataItem)[propertyName];
        }

        /// <summary>
        /// 获取当前容器中实体的属性值
        /// </summary>
        /// <typeparam name="T">值类型</typeparam>
        /// <param name="container">扩展类</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static T GetProperty<T>(this IDataItemContainer container, string propertyName)
        {
            return (T)container.GetProperty(propertyName);
        }

        /// <summary>
        /// 获取当前容器中实体的属性值
        /// </summary>
        /// <param name="container">扩展类</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static int GetInt(this IDataItemContainer container, string propertyName)
        {
            return Convert.ToInt32(container.GetProperty(propertyName));
        }

        /// <summary>
        /// 获取当前容器中实体的属性值
        /// </summary>
        /// <param name="container">扩展类</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static bool GetBool(this IDataItemContainer container, string propertyName)
        {
            return Convert.ToBoolean(container.GetProperty(propertyName));
        }

        /// <summary>
        /// 获取当前容器中实体的属性值
        /// </summary>
        /// <param name="container">扩展类</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static string GetString(this IDataItemContainer container, string propertyName)
        {
            return Convert.ToString(container.GetProperty(propertyName));
        }

        /// <summary>
        /// 获取当前容器中实体的属性值
        /// </summary>
        /// <param name="container">扩展类</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="format">要对结果应用的格式字符串</param>
        /// <returns>属性值</returns>
        public static string GetString(this IDataItemContainer container, string propertyName, string format)
        {
            return String.Format(format, container.GetProperty(propertyName));
        }
    }
}
