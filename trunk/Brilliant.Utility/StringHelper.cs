/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是字符串操作工具类。
 * 
 * 作者：zwk       时间：2013-11-12
 * ========================================================================
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Brilliant.Utility
{
    /// <summary>
    /// 字符串操作工具类
    /// </summary>
    public static class StringHelper
    {
        //自增变量
        private static int _identity = 0;

        /// <summary>
        /// 将指定文本转化为汉语拼音缩写
        /// </summary>
        /// <param name="text">文本 如：赵文凯</param>
        /// <returns>汉语拼音缩写 如：zwk</returns>
        public static string ConvertToChineseSpellingAbbr(string text)
        {
            char spelling;
            byte[] array;
            StringBuilder sb = new StringBuilder(text.Length);
            foreach (char c in text)
            {
                spelling = c;
                array = Encoding.Default.GetBytes(new char[] { c });

                if (array.Length == 2)
                {
                    int i = array[0] * 0x100 + array[1];

                    if (i < 0xB0A1) spelling = c;
                    else
                        if (i < 0xB0C5) spelling = 'a';
                    else
                            if (i < 0xB2C1) spelling = 'b';
                    else
                                if (i < 0xB4EE) spelling = 'c';
                    else
                                    if (i < 0xB6EA) spelling = 'd';
                    else
                                        if (i < 0xB7A2) spelling = 'e';
                    else
                                            if (i < 0xB8C1) spelling = 'f';
                    else
                                                if (i < 0xB9FE) spelling = 'g';
                    else
                                                    if (i < 0xBBF7) spelling = 'h';
                    else
                                                        if (i < 0xBFA6) spelling = 'g';
                    else
                                                            if (i < 0xC0AC) spelling = 'k';
                    else
                                                                if (i < 0xC2E8) spelling = 'l';
                    else
                                                                    if (i < 0xC4C3) spelling = 'm';
                    else
                                                                        if (i < 0xC5B6) spelling = 'n';
                    else
                                                                            if (i < 0xC5BE) spelling = 'o';
                    else
                                                                                if (i < 0xC6DA) spelling = 'p';
                    else
                                                                                    if (i < 0xC8BB) spelling = 'q';
                    else
                                                                                        if (i < 0xC8F6) spelling = 'r';
                    else
                                                                                            if (i < 0xCBFA) spelling = 's';
                    else
                                                                                                if (i < 0xCDDA) spelling = 't';
                    else
                                                                                                    if (i < 0xCEF4) spelling = 'w';
                    else
                                                                                                        if (i < 0xD1B9) spelling = 'x';
                    else
                                                                                                            if (i < 0xD4D1) spelling = 'y';
                    else
                                                                                                                if (i < 0xD7FA) spelling = 'z';
                }
                sb.Append(spelling);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 根据名称获取字母缩写
        /// </summary>
        /// <param name="ChineseStr">待输入的中文名称</param>
        /// <returns>字母缩写</returns>
        /// <remarks>作者：dfq 时间：2014.04.02</remarks>
        public static string ConvertToChineseCap(string ChineseStr)
        {
            string Capstr = "";
            byte[] ZW = new byte[2];
            long ChineseStr_int;
            string CharStr = "";
            string ChinaStr = "";
            for (int i = 0; i < ChineseStr.Length; i++)
            {
                CharStr = ChineseStr.Substring(i, 1).ToString();
                ZW = System.Text.Encoding.Default.GetBytes(CharStr);
                // 得到汉字符的字节数组
                if (ZW.Length == 2)
                {
                    int i1 = (short)(ZW[0]);
                    int i2 = (short)(ZW[1]);
                    ChineseStr_int = i1 * 256 + i2;
                    if ((ChineseStr_int >= 45217) && (ChineseStr_int <= 45252))
                    {
                        ChinaStr = "a";
                    }
                    else if ((ChineseStr_int >= 45253) && (ChineseStr_int <= 45760))
                    {
                        ChinaStr = "b";
                    }
                    else if ((ChineseStr_int >= 45761) && (ChineseStr_int <= 46317))
                    {
                        ChinaStr = "c";

                    }
                    else if ((ChineseStr_int >= 46318) && (ChineseStr_int <= 46825))
                    {
                        ChinaStr = "d";
                    }
                    else if ((ChineseStr_int >= 46826) && (ChineseStr_int <= 47009))
                    {
                        ChinaStr = "e";
                    }
                    else if ((ChineseStr_int >= 47010) && (ChineseStr_int <= 47296))
                    {
                        ChinaStr = "f";
                    }
                    else if ((ChineseStr_int >= 47297) && (ChineseStr_int <= 47613))
                    {
                        ChinaStr = "g";
                    }
                    else if ((ChineseStr_int >= 47614) && (ChineseStr_int <= 48118))
                    {

                        ChinaStr = "h";
                    }

                    else if ((ChineseStr_int >= 48119) && (ChineseStr_int <= 49061))
                    {
                        ChinaStr = "j";
                    }
                    else if ((ChineseStr_int >= 49062) && (ChineseStr_int <= 49323))
                    {
                        ChinaStr = "k";
                    }
                    else if ((ChineseStr_int >= 49324) && (ChineseStr_int <= 49895))
                    {
                        ChinaStr = "l";
                    }
                    else if ((ChineseStr_int >= 49896) && (ChineseStr_int <= 50370))
                    {
                        ChinaStr = "m";
                    }

                    else if ((ChineseStr_int >= 50371) && (ChineseStr_int <= 50613))
                    {
                        ChinaStr = "n";

                    }
                    else if ((ChineseStr_int >= 50614) && (ChineseStr_int <= 50621))
                    {
                        ChinaStr = "o";
                    }
                    else if ((ChineseStr_int >= 50622) && (ChineseStr_int <= 50905))
                    {
                        ChinaStr = "p";

                    }
                    else if ((ChineseStr_int >= 50906) && (ChineseStr_int <= 51386))
                    {
                        ChinaStr = "q";

                    }

                    else if ((ChineseStr_int >= 51387) && (ChineseStr_int <= 51445))
                    {
                        ChinaStr = "r";
                    }
                    else if ((ChineseStr_int >= 51446) && (ChineseStr_int <= 52217))
                    {
                        ChinaStr = "s";
                    }
                    else if ((ChineseStr_int >= 52218) && (ChineseStr_int <= 52697))
                    {
                        ChinaStr = "t";
                    }
                    else if ((ChineseStr_int >= 52698) && (ChineseStr_int <= 52979))
                    {
                        ChinaStr = "w";
                    }
                    else if ((ChineseStr_int >= 52980) && (ChineseStr_int <= 53640))
                    {
                        ChinaStr = "x";
                    }
                    else if ((ChineseStr_int >= 53689) && (ChineseStr_int <= 54480))
                    {
                        ChinaStr = "y";
                    }
                    else if ((ChineseStr_int >= 54481) && (ChineseStr_int <= 55289))
                    {
                        ChinaStr = "z";
                    }
                }
                else
                {
                    //Capstr = ChineseStr;
                    Capstr = Capstr + CharStr;
                    // break;
                }
                //Capstr = ChineseStr;
                Capstr = Capstr + ChinaStr;
            }

            return Capstr.ToLower();

        }

        /// <summary>
        /// 获取主键编号(长度21位)
        /// </summary>
        /// <returns>主键编号</returns>
        public static string GetPrimaryKey()
        {
            if (_identity >= 10000) { _identity = 0; }
            string id = DateTime.Now.ToString("yyyyMMddHHmmssfff") + _identity.ToString().PadLeft(4, '0');
            _identity++;
            return id;
        }

        /// <summary>
        /// 去除字符串中的空格、制表符、换行、新行
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>新字符串</returns>
        public static string TrimString(string str)
        {
            str = Regex.Replace(str, @"\s", "");
            return str;
        }

        /// <summary>
        /// 去掉字符串中的非数字
        /// </summary>
        /// <param name="key">值</param>
        /// <returns>数字</returns>
        /// <remarks>作者：dfq 时间：2016.08.12</remarks>
        public static string RemoveNotNumber(string key)
        {
            return Regex.Replace(key, @"[^\d]*", "");
        }

        /// <summary>
        /// 去掉字符串中的数字
        /// </summary>
        /// <param name="key">值</param>
        /// <returns>非数字</returns>
        /// <remarks>作者：dfq 时间：2016.08.12</remarks>
        public static string RemoveNumber(string key)
        {
            return Regex.Replace(key, @"\d", "");
        }

        /// <summary>
        /// 创建指定长度的字符串
        /// </summary>
        /// <param name="strLong">长度</param>
        /// <param name="str">字符串</param>
        /// <returns>指定长度的字符串</returns>
        /// <remarks>作者:dfq 时间:2016-08-12</remarks>
        public static string StringOfChar(int strLong, string str)
        {
            string ReturnStr = "";
            for (int i = 0; i < strLong; i++)
            {
                ReturnStr += str;
            }
            return ReturnStr;
        }

        /// <summary>
        /// 获取GUID
        /// </summary>
        /// <returns>GUID</returns>
        /// <remarks>作者：dfq 时间：2017.03.27</remarks>
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>  
        /// 截取字符串长度  
        /// </summary>  
        /// <param name="input">要截取的字符串对象</param>  
        /// <param name="length">要保留的字符个数</param>  
        /// <param name="suffix">后缀(用以替换超出长度部分)</param>  
        /// <returns></returns>  
        public static string SubStringCustom(string input, int length, string suffix)
        {
            Encoding encode = Encoding.GetEncoding("gb2312");
            byte[] byteArr = encode.GetBytes(input);
            if (byteArr.Length <= length) return input;

            int m = 0, n = 0;
            foreach (byte b in byteArr)
            {
                if (n >= length) break;
                if (b > 127) m++; //重要一步：对前p个字节中的值大于127的字符进行统计  
                n++;
            }
            if (m % 2 != 0) n = length + 1; //如果非偶：则说明末尾为双字节字符，截取位数加1  

            return encode.GetString(byteArr, 0, n) + suffix;
        }
    }
}
