using System;
using System.Collections.Generic;
using System.Reflection;

namespace Brilliant.ORM
{
    /// <summary>
    /// 枚举描述特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    internal class DescriptionAttribute : Attribute
    {
        private string description;

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="description">描述信息</param>
        public DescriptionAttribute(string description)
        {
            this.description = description;
        }
    }

    /// <summary>
    /// 枚举特性操作
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    internal static class Enum<T>
    {
        /// <summary>
        /// 获取描述信息
        /// </summary>
        /// <param name="value">枚举项</param>
        /// <returns>描述信息</returns>
        public static string GetDesc(T value)
        {
            Type type = typeof(T);
            FieldInfo info = type.GetField(value.ToString());
            object[] obj = info.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (obj == null)
            {
                return String.Empty;
            }
            DescriptionAttribute da = obj[0] as DescriptionAttribute;
            if (da != null)
            {
                return da.Description;
            }
            return String.Empty;
        }

        /// <summary>
        /// 将枚举转换为List集合
        /// </summary>
        /// <returns>List集合</returns>
        public static List<EnumItem> ToList()
        {
            Type type = typeof(T);
            List<EnumItem> list = new List<EnumItem>();
            Array array = Enum.GetValues(type);
            foreach (int value in array)
            {
                EnumItem item = new EnumItem();
                item.Text = GetDesc((T)Enum.ToObject(type, value));
                item.Value = value.ToString();
                list.Add(item);
            }
            return list;
        }
    }

    /// <summary>
    /// 枚举列表项
    /// </summary>
    public class EnumItem
    {
        /// <summary>
        /// 显示文本
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 显示文本对应的值
        /// </summary>
        public string Value { get; set; }
    }
}
