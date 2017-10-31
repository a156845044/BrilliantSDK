/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是Json数据处理工具类。
 * 
 * 作者：zwk       时间：2013-11-12
 * ========================================================================
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace Brilliant.ORM
{
    /// <summary>
    /// Json数据序列化/反序列化
    /// </summary>
    public static class JsonSerializer
    {
        /// <summary>
        /// 将对象/对象集合转换成Json数据
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json数据</returns>
        public static string Serialize(object obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Encoding.UTF8.GetString(stream.ToArray());
                return szJson;
            }
        }

        /// <summary>
        /// 将Json数据转换成对象/对象集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">Json数据</param>
        /// <returns>对象/对象集合</returns>
        public static T DeSerialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// 将对象/对象集合转换成Json数据（JavaScriptSerializer）
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns>Json数据</returns>
        public static string JSSerialize(object obj)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Serialize(obj);
        }

        /// <summary>
        /// 将Json数据转换成对象/对象集合（JavaScriptSerializer）
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">Json数据</param>
        /// <returns>对象/对象集合</returns>
        public static T JSDeSerialize<T>(string json)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<T>(json);
        }
    }
}
