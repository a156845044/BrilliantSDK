/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是字符串加解密工具类。
 * 
 * 作者：zwk       时间：2013-11-12
 * ========================================================================
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace Brilliant.Utility
{
    /// <summary>
    /// 字符串加解密工具类
    /// </summary>
    public static class EncryptHelper
    {
        /// <summary>
        /// DES加密算法
        /// </summary>
        /// <param name="originalString">待加密的字符串</param>
        /// <param name="sKey">加密Key</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(string originalString, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            // 把字符串放到byte数组中
            byte[] inputByteArray = Encoding.Default.GetBytes(originalString);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey); //建立加密对象的密钥和偏移量
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);  //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            MemoryStream ms = new MemoryStream();         //使得输入密码必须输入英文文本
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }

        /// <summary>
        /// DES解密算法
        /// </summary>
        /// <param name="originalString">待解密的字符串</param>
        /// <param name="sKey">解密Key</param>
        /// <returns>解密后的字符串</returns>
        public static string Decrypt(string originalString, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[originalString.Length / 2];
            for (int x = 0; x < originalString.Length / 2; x++)
            {
                int i = (Convert.ToInt32(originalString.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            //建立加密对象的密钥和偏移量，此值重要，不能修改
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());

        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">待加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Md5(string str)
        {
            MD5 m = new MD5CryptoServiceProvider();
            byte[] s = m.ComputeHash(UnicodeEncoding.UTF8.GetBytes(str));
            return BitConverter.ToString(s).Replace("-", "");
        }
    }
}
