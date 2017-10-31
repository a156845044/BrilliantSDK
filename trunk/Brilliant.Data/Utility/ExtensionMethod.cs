using Brilliant.Data.Entity;
using System.Collections;
using System.Web.UI;

namespace Brilliant.Data.Utility
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtensionMethod
    {
        /// <summary>
        /// 获取当前容器中实体的属性值
        /// </summary>
        /// <param name="dataItem">扩展类</param>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public static object GetProperty(this IDataItemContainer dataItem, string propertyName)
        {
            return ((EntityBase)dataItem.DataItem)[propertyName];
        }

        /// <summary>
        /// 将List集合转化为Json数据
        /// </summary>
        /// <param name="list">List集合</param>
        /// <returns>Json数据</returns>
        public static string ToJson(this IList list)
        {
            return JsonSerializer.JSSerialize(list);
        }
    }
}
