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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Script.Serialization;

namespace Brilliant.Utility
{
    /// <summary>
    /// Json数据处理工具类
    /// </summary>
    public static class JsonHelper
    {
        #region 注释
        //统一换成 Newtonsoft.json
        ///// <summary>
        ///// 将对象/对象集合转换成Json数据
        ///// </summary>
        ///// <param name="obj">对象</param>
        ///// <returns>Json数据</returns>
        //public static string Serialize(object obj)
        //{
        //    DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
        //    using (MemoryStream stream = new MemoryStream())
        //    {
        //        json.WriteObject(stream, obj);
        //        string szJson = Encoding.UTF8.GetString(stream.ToArray());
        //        return szJson;
        //    }
        //}

        ///// <summary>
        ///// 将Json数据转换成对象/对象集合
        ///// </summary>
        ///// <typeparam name="T">对象类型</typeparam>
        ///// <param name="json">Json数据</param>
        ///// <returns>对象/对象集合</returns>
        //public static T DeSerialize<T>(string json)
        //{
        //    T obj = Activator.CreateInstance<T>();
        //    using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
        //    {
        //        DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
        //        return (T)serializer.ReadObject(ms);
        //    }
        //}

        ///// <summary>
        ///// 将对象/对象集合转换成Json数据（JavaScriptSerializer）
        ///// </summary>
        ///// <param name="obj">对象</param>
        ///// <returns>Json数据</returns>
        //public static string JSSerialize(object obj)
        //{
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //    return jss.Serialize(obj);
        //}

        ///// <summary>
        ///// 将Json数据转换成对象/对象集合（JavaScriptSerializer）
        ///// </summary>
        ///// <typeparam name="T">对象类型</typeparam>
        ///// <param name="json">Json数据</param>
        ///// <returns>对象/对象集合</returns>
        //public static T JSDeSerialize<T>(string json)
        //{
        //    JavaScriptSerializer jss = new JavaScriptSerializer();
        //    return jss.Deserialize<T>(json);
        //} 
        #endregion

        /// <summary>
        /// 将对象/对象集合转换成Json数据
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="dateFormat">是否日期格式化</param>
        /// <returns>Json数据</returns>
        /// <remarks>作者：dfq 时间：2017.03.27</remarks>
        public static string Serialize(object obj, bool dateFormat = true)
        {
            return SerializeObject(obj, dateFormat);
        }

        /// <summary>
        /// 将Json数据转换成对象/对象集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">Json数据</param>
        /// <param name="dateFormat">是否日期格式化</param>
        /// <returns>对象/对象集合</returns>
        /// <remarks>作者：dfq 时间：2017.03.27</remarks>
        public static T DeSerialize<T>(string json, bool dateFormat = true)
        {
            return DeSerializeObject<T>(json, dateFormat);
        }

        /// <summary>
        /// 将对象/对象集合转换成Json数据（JavaScriptSerializer）
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="dateFormat">是否日期格式化</param>
        /// <returns>Json数据</returns>
        /// <remarks>作者：dfq 时间：2017.03.27</remarks>
        public static string JSSerialize(object obj, bool dateFormat = true)
        {
            return SerializeObject(obj, dateFormat);
        }

        /// <summary>
        /// 将Json数据转换成对象/对象集合（JavaScriptSerializer）
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">Json数据</param>
        /// <param name="dateFormat">是否日期格式化</param>
        /// <returns>对象/对象集合</returns>
        /// <remarks>作者：dfq 时间：2017.03.27</remarks>
        public static T JSDeSerialize<T>(string json, bool dateFormat = true)
        {
            return DeSerializeObject<T>(json, dateFormat);
        }

        /// <summary>
        /// Newtonsoft.json-将对象/对象集合转换成Json数据
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="dateFormat">是否日期格式化</param>
        /// <returns>Json数据</returns>
        /// <remarks>作者：dfq 时间：2017.03.27</remarks>
        public static string SerializeObject(object obj, bool dateFormat = true)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;//空值处理

            if (dateFormat)
            {
                //日期类型默认格式化处理
                settings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }

            return JsonConvert.SerializeObject(obj, settings);
        }

        /// <summary>
        ///  Newtonsoft.json-将Json数据转换成对象/对象集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">Json数据</param>
        /// <param name="dateFormat">是否日期格式化</param>m>
        /// <returns>对象/对象集合</returns>
        /// <remarks>作者：dfq 时间：2017.03.27</remarks>
        public static T DeSerializeObject<T>(string json, bool dateFormat = true)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;//空值处理
            if (dateFormat)
            {
                //日期类型默认格式化处理
                settings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }

            return JsonConvert.DeserializeObject<T>(json, settings);
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="reader">JsonReader</param>
        /// <param name="objectType">Type</param>
        ///<param name="dateFormat">是否日期格式化</param>
        /// <returns>object</returns>
        ///  <remarks>作者：dfq 时间：2017.03.30</remarks>
        public static object Deserialize(JsonReader reader, Type objectType, bool dateFormat = true)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;//空值处理
            if (dateFormat)
            {
                //日期类型默认格式化处理
                settings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
                settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }
            JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
            return jsonSerializer.Deserialize(reader, objectType);
        }

    }
}
