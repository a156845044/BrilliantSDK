using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Brilliant.Utility
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HttpPost(string url, string data)
        {
            //发出
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //request.CookieContainer = cookie;
            using (Stream requestStream = request.GetRequestStream())
            {
                using (StreamWriter sw = new StreamWriter(requestStream, Encoding.GetEncoding("utf-8")))
                {
                    sw.Write(data);
                }
            }

            //接受
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //response.Cookies = cookie.GetCookies(response.ResponseUri);
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                {
                    string result = sr.ReadToEnd();
                    return result;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader sr = new StreamReader(responseStream, Encoding.GetEncoding("utf-8")))
                {
                    string result = sr.ReadToEnd();
                    return result;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public static T HttpGet<T>(string url)
        {
            string json = HttpGet(url);
            try
            {
                return JsonHelper.JSDeSerialize<T>(json);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
