using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Brilliant.Data.Utility
{
    /// <summary>
    /// 类型映射
    /// </summary>
    public static class TypeMapper
    {
        private static XmlDocument xmlDoc = new XmlDocument();
        private const string DEFAULT_DATETIME = "1900-01-01";

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static TypeMapper()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("Brilliant.Data.TypeMapper.xml"))
            {
                xmlDoc.Load(stream);
            }
        }

        /// <summary>
        /// 默认日期
        /// </summary>
        public static DateTime DefaultDateTime
        {
            get { return Convert.ToDateTime(DEFAULT_DATETIME); }
        }

        /// <summary>
        /// 数据库类型映射到特定语言类型
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="languageType">语言类别</param>
        /// <returns>语言类型</returns>
        public static string MapLanType(string dbType, LanguageType languageType)
        {
            string type = GetNodeValue(String.Format("/TypeMap/Type[contains(@name,',{0},')]", dbType.ToLower()), languageType.ToString().ToLower());
            if (String.IsNullOrEmpty(type))
            {
                dbType = "default";
                type = GetNodeValue(String.Format("/TypeMap/Type[contains(@name,',{0},')]", dbType.ToLower()), languageType.ToString().ToLower());
            }
            return type;
        }

        /// <summary>
        /// 数据库类型映射到特定语言类型解析器
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="languageType">语言类别</param>
        /// <returns>语言类型解析器</returns>
        public static string MapLanTypeParser(string dbType, LanguageType languageType)
        {
            string typeParser = GetNodeValue(String.Format("/TypeMap/Type[contains(@name,',{0},')]", dbType.ToLower()), languageType.ToString().ToLower() + "parser");
            if (String.IsNullOrEmpty(typeParser))
            {
                dbType = "default";
                typeParser = GetNodeValue(String.Format("/TypeMap/Type[contains(@name,',{0},')]", dbType.ToLower()), languageType.ToString().ToLower() + "parser");
            }
            return typeParser;
        }

        /// <summary>
        /// 获取Xml文档节点值/属性值
        /// </summary>
        /// <param name="xpath">xpath查询语句</param>
        /// <param name="attribute">节点属性</param>
        /// <returns>节点值/属性值</returns>
        private static string GetNodeValue(string xpath, string attribute)
        {
            XmlNode xn = xmlDoc.SelectSingleNode(xpath);
            if (xn == null)
            {
                return String.Empty;
            }
            if (xn.Attributes == null)
            {
                return String.Empty;
            }
            if (!String.IsNullOrEmpty(attribute))
            {
                return xn.Attributes[attribute].Value;
            }
            return xn.InnerText;
        }

        /// <summary>
        /// 映射C#表示的SQL类型
        /// </summary>
        /// <param name="dbTypeName">SQL类型</param>
        /// <param name="length">长度</param>
        /// <returns>C#表示的SQL类型</returns>
        public static string MapSQLType(string dbTypeName, int length)
        {
            string result = String.Format("SqlDbType.VarChar, {0}", length);
            dbTypeName = dbTypeName.ToLower();
            switch (dbTypeName)
            {
                case "bigint": result = "SqlDbType.BigInt"; break;
                case "binary": result = "SqlDbType.Binary"; break;
                case "bit": result = "SqlDbType.Bit"; break;
                case "char": result = String.Format("SqlDbType.Char, {0}", length); break;
                case "date": result = "SqlDbType.Date"; break;
                case "datetime": result = "SqlDbType.DateTime"; break;
                case "datetime2": result = "SqlDbType.DateTime2"; break;
                case "datetimeoffset": result = "SqlDbType.DateTimeOffset"; break;
                case "decimal": result = "SqlDbType.Decimal"; break;
                case "float": result = "SqlDbType.Float"; break;
                case "image": result = "SqlDbType.Image"; break;
                case "int": result = "SqlDbType.Int"; break;
                case "money": result = "SqlDbType.Money"; break;
                case "nchar": result = String.Format("SqlDbType.NChar, {0}", length); break;
                case "ntext": result = "SqlDbType.NText"; break;
                case "nvarchar": result = String.Format("SqlDbType.NVarChar, {0}", length); break;
                case "real": result = "SqlDbType.Real"; break;
                case "smalldatetime": result = "SqlDbType.SmallDateTime"; break;
                case "smallint": result = "SqlDbType.SmallInt"; break;
                case "smallmoney": result = "SqlDbType.SmallMoney"; break;
                case "sql_variant": result = "SqlDbType.Variant"; break;
                case "text": result = "SqlDbType.Text"; break;
                case "time": result = "SqlDbType.Time"; break;
                case "timestamp": result = "SqlDbType.Timestamp"; break;
                case "tinyint": result = "SqlDbType.TinyInt"; break;
                case "uniqueidentifier": result = "SqlDbType.UniqueIdentifier"; break;
                case "varbinary": result = "SqlDbType.VarBinary"; break;
                case "varchar": result = String.Format("SqlDbType.VarChar, {0}", length); break;
                case "xml": result = "SqlDbType.Xml"; break;
            }
            return result;
        }

        /// <summary>
        /// 数据库类型映射到C#类型的默认值
        /// </summary>
        /// <param name="dbTypeName">数据库类型</param>
        /// <param name="dbDefault">数据库默认值</param>
        /// <returns>C#类型的默认值</returns>
        public static string MapDefaultValue(string dbTypeName, string dbDefault)
        {
            string dbfaultValue = string.Empty;
            if (!String.IsNullOrEmpty(dbDefault))
            {
                dbfaultValue = dbDefault.Replace("(", "").Replace(")", "").Replace("'", "\"");
                if (dbDefault == "getdate")
                {
                    dbfaultValue = "DateTime.Now";
                }
                return dbfaultValue;
            }
            dbTypeName = dbTypeName.ToLower();
            switch (dbTypeName)
            {
                case "nvarchar":
                    dbfaultValue = "String.Empty";
                    break;
                case "varchar":
                    dbfaultValue = "String.Empty";
                    break;
                case "int":
                    dbfaultValue = "0";
                    break;
                case "smalldatetime":
                    dbfaultValue = "Convert.ToDateTime(\"1900-01-01\")";
                    break;
                case "money":
                    dbfaultValue = "0";
                    break;
            }
            return dbfaultValue;
        }

        /// <summary>
        /// 获取类型默认值
        /// </summary>
        /// <param name="dbDefault">数据库类型默认值</param>
        /// <returns>数据库类型默认值</returns>
        public static string MapDefaultValue(string dbDefault)
        {
            string dbfaultValue = string.Empty;
            if (!String.IsNullOrEmpty(dbDefault))
            {
                dbfaultValue = dbDefault.Replace("(", "").Replace(")", "").Replace("'", "\"");
            }
            return dbfaultValue;
        }

        /// <summary>
        /// 首字母大写（将指定字符串的首字母转换为大写）
        /// </summary>
        public static string ConvertToUpper(string fieldName)
        {
            if (String.IsNullOrEmpty(fieldName)) return fieldName;
            string firstWord = fieldName.Substring(0, 1);
            string strTemp = fieldName.Substring(1, fieldName.Length - 1);
            return String.Format("{0}{1}", firstWord.ToUpper(), strTemp);
        }

        /// <summary>
        /// 首字母小写（将指定字符串的首字母转换为小写）
        /// </summary>
        public static string ConvertToLower(string fieldName)
        {
            if (String.IsNullOrEmpty(fieldName)) return fieldName;
            string firstWord = fieldName.Substring(0, 1);
            string strTemp = fieldName.Substring(1, fieldName.Length - 1);
            return String.Format("{0}{1}", firstWord.ToLower(), strTemp);
        }
    }

    /// <summary>
    /// 语言类型
    /// </summary>
    public enum LanguageType
    {
        /// <summary>
        /// Java语言
        /// </summary>
        Java,
        /// <summary>
        /// C#语言
        /// </summary>
        CS
    }
}
