using Brilliant.Data.Utility;
using System;
using System.Collections.Generic;

namespace Brilliant.Data.Entity
{
    /// <summary>
    /// 实体基类
    /// </summary>
    [Serializable]
    public class EntityBase
    {
        //外键关联对象
        private EntityBase fkObject;

        //存放原始键
        private Stack<string> keys = new Stack<string>();

        //实体属性列表（键值对）
        private IDictionary<string, object> fields = new Dictionary<string, object>();

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public EntityBase() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fkObject">外键关联对象</param>
        public EntityBase(EntityBase fkObject)
        {
            this.fkObject = fkObject;
            if (fkObject != null) //如果外键关联对象不为空
            {
                this.fields = fkObject.fields;
                this.keys = fkObject.keys;
            }
        }

        /// <summary>
        /// 设置属性的值
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <param name="value">属性值</param>
        /// <remarks>
        ///     新增原始键数据的保存。
        ///     在序列化对象属性时，可以输出为原始键。
        /// </remarks>
        public void SetProperty(string propertyName, object value)
        {
            string key = propertyName.ToLower();
            if (fields.ContainsKey(key))
            {
                fields[key] = value;
            }
            else
            {
                fields.Add(key, value);
                keys.Push(propertyName); //存放原始键数据
            }
        }

        /// <summary>
        /// 获取属性的值
        /// </summary>
        /// <typeparam name="T">用以指明属性类型</typeparam>
        /// <param name="propertyName">属性名</param>
        /// <returns>属性的值</returns>
        public T GetProperty<T>(string propertyName)
        {
            propertyName = propertyName.ToLower();
            if (fields.ContainsKey(propertyName))
            {
                if (DBNull.Value == fields[propertyName])
                {
                    return default(T);
                }
                return (T)fields[propertyName];
            }
            return default(T);
        }

        /// <summary>
        /// 属性索引器
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        /// <returns>属性值</returns>
        public object this[string propertyName]
        {
            get
            {
                string key = propertyName.ToLower();
                if (fields.ContainsKey(key))
                {
                    return fields[key];
                }
                return null;
            }
        }

        /// <summary>
        /// 获取实体所有属性列表
        /// </summary>
        /// <returns>所有属性列表</returns>
        /// <remarks>时间：2014-09-05 类型：新增</remarks>
        public IDictionary<string, object> GetProperties()
        {
            IDictionary<string, object> propertyList = new Dictionary<string, object>();
            foreach (string key in keys)
            {
                propertyList.Add(key, fields[key.ToLower()]);
            }
            return propertyList;
        }

        /// <summary>
        /// 将当前对象转化为Json字符串
        /// </summary>
        /// <returns>Json字符串</returns>
        public string ToJson()
        {
            return JsonSerializer.JSSerialize(this);
        }
    }
}
