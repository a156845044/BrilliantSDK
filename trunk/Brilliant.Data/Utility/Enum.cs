using System;
using System.Reflection;

namespace Brilliant.Data.Utility
{
    /// <summary>
    /// 枚举描述特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
    public class DescriptionAttribute : Attribute
    {
        private string description;

        /// <summary>
        /// 描述信息
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        public DescriptionAttribute(string description)
        {
            this.description = description;
        }
    }

    /// <summary>
    /// 枚举特性操作
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    public static class Enum<T>
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
    }
}
