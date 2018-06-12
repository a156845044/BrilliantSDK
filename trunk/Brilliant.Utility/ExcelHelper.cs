/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是Excel操作工具类。
 * 
 * 作者：zwk       时间：2013-11-12
 * ========================================================================
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.OleDb;

namespace Brilliant.Utility
{
    /// <summary>
    /// Excel操作工具类
    /// </summary>
    public static class ExcelHelper
    {
        /// <summary>
        /// 取得 OLEDB 的连接字符串.
        /// 优先启动 ACE 驱动，
        /// 假如 ACE 失败，再尝试启动 JET
        /// 该方法可能用不上。
        /// 因为 在 Office 2010 上面，Jet 与 ACE 都能正常运作
        /// 唯一需要注意的是， 如果目标机器的操作系统，是64位的话。
        /// 项目需要 编译为 x86， 而不是简单的使用默认的 Any CPU.
        /// </summary>
        /// <param name="excelFilePath">Excel文件</param>
        /// <returns>连接字符串</returns>
        private static string GetOleDbConnectionString(string excelFilePath)
        {
            // Office 2007 以及 以下版本使用.
            string jetConnString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0; HDR=Yes; IMEX=1'", excelFilePath);

            // xlsx 扩展名 使用.
            string aceConnXlsxString = String.Format("Provider=Microsoft.Ace.OLEDB.12.0;Data Source={0};Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'", excelFilePath);

 

            // 默认非 xlsx
            string aceConnString = aceConnXlsxString;
            if (excelFilePath.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase))
            {
                // 如果扩展名为 xlsx.
                // 那么需要将 驱动切换为 xlsx 扩展名 的.
                aceConnString = aceConnXlsxString;
            }
            else
            {
                aceConnString = jetConnString;
            }
            // 尝试使用 ACE. 假如不发生错误的话，使用 ACE 驱动.
            try
            {
                System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection(aceConnString);
                cn.Open();
                cn.Close();
                // 使用 ACE
                return aceConnString;
            }
            catch (Exception)
            {
                // 启动 ACE 失败.
            }
            // 尝试使用 Jet. 假如不发生错误的话，使用 Jet 驱动.
            try
            {
                System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection(jetConnString);
                cn.Open();
                cn.Close();
                // 使用 Jet
                return jetConnString;
            }
            catch (Exception)
            {
                // 启动 Jet 失败.
            }
            // 假如 ACE 与 JET 都失败了，默认使用 JET.
            return jetConnString;
        }

        /// <summary>
        /// 创建Excel文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="cmdText">待执行的创建语句</param>
        /// <returns>受影响行数</returns>
        public static int CreateExcel(string filePath, string cmdText)
        {
            if (File.Exists(filePath)) // 如果文件已存在，那么删除.
            {
                File.Delete(filePath);
            }
            string connString = GetOleDbConnectionString(filePath);
            using (OleDbConnection conn = new OleDbConnection(connString))// 定义 Oledb 的数据库联接.
            {
                // 打开连接.
                conn.Open();
                // 创建 Excel Sheet的 命令.
                OleDbCommand cmd = new OleDbCommand(cmdText, conn);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 插入数据到Excel文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="cmdText">待执行的INSERT语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>受影响行数</returns>
        public static int InsertExcel(string filePath, string cmdText, List<OleDbParameter[]> parameters)
        {
            int result = 0;
            string connString = GetOleDbConnectionString(filePath);// 取得连接字符串.
            using (OleDbConnection conn = new OleDbConnection(connString))// 定义 Oledb 的数据库联接.
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(cmdText, conn);
                foreach (OleDbParameter[] param in parameters)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(param);
                    result += cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        /// <summary>
        /// 修改Excel中的数据
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="cmdText">UPDATE或DELETE语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>受影响行数</returns>
        public static int ModifyExcel(string filePath, string cmdText, OleDbParameter[] parameters)
        {
            string connString = GetOleDbConnectionString(filePath);// 取得连接字符串.
            using (OleDbConnection conn = new OleDbConnection(connString))// 定义 Oledb 的数据库联接.
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand(cmdText, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 读取Excel文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="cmdText">SELECT语句</param>
        /// <returns>DataReader对象</returns>
        public static OleDbDataReader ReadExcel(string filePath, string cmdText)
        {
            string connString = GetOleDbConnectionString(filePath);// 取得连接字符串.
            OleDbConnection conn = new OleDbConnection(connString);
            conn.Open();
            OleDbCommand cmd = new OleDbCommand(cmdText, conn);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        ///// <summary>
        ///// 取得 OLEDB 的连接字符串.
        ///// 优先启动 ACE 驱动，
        ///// 假如 ACE 失败，再尝试启动 JET
        ///// 该方法可能用不上。
        ///// 因为 在 Office 2010 上面，Jet 与 ACE 都能正常运作
        ///// 唯一需要注意的是， 如果目标机器的操作系统，是64位的话。
        ///// 项目需要 编译为 x86， 而不是简单的使用默认的 Any CPU.
        ///// </summary>
        ///// <param name="excelFilePath">Excel文件</param>
        ///// <returns>连接字符串</returns>
        //private static string GetOleDbConnectionString(string excelFilePath)
        //{
        //    // Office 2007 以及 以下版本使用.
        //    string jetConnString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;", excelFilePath);

        //    // xlsx 扩展名 使用.
        //    string aceConnXlsxString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\"", excelFilePath);

        //    // xls 扩展名 使用.
        //    string aceConnXlsString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=YES\"", excelFilePath);

        //    // 默认非 xlsx
        //    string aceConnString = aceConnXlsString;
        //    if (excelFilePath.EndsWith(".xlsx", StringComparison.CurrentCultureIgnoreCase))
        //    {
        //        // 如果扩展名为 xlsx.
        //        // 那么需要将 驱动切换为 xlsx 扩展名 的.
        //        aceConnString = aceConnXlsxString;
        //    }
        //    // 尝试使用 ACE. 假如不发生错误的话，使用 ACE 驱动.
        //    try
        //    {
        //        System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection(aceConnString);
        //        cn.Open();
        //        cn.Close();
        //        // 使用 ACE
        //        return aceConnString;
        //    }
        //    catch (Exception)
        //    {
        //        // 启动 ACE 失败.
        //    }
        //    // 尝试使用 Jet. 假如不发生错误的话，使用 Jet 驱动.
        //    try
        //    {
        //        System.Data.OleDb.OleDbConnection cn = new System.Data.OleDb.OleDbConnection(jetConnString);
        //        cn.Open();
        //        cn.Close();
        //        // 使用 Jet
        //        return jetConnString;
        //    }
        //    catch (Exception)
        //    {
        //        // 启动 Jet 失败.
        //    }
        //    // 假如 ACE 与 JET 都失败了，默认使用 JET.
        //    return jetConnString;
        //}

        /// <summary>
        /// 获取Excel导出存放的目录
        /// </summary>
        /// <returns>存放的目录</returns>
        /// <remarks>作者：dfq 时间：2015-01-22</remarks>
        public static string GetExcelPathRoot()
        {
            string excelPathVir = String.Format("/Downloads/Phy/{0}", DateTime.Now.ToString("yyyyMMdd"));
            string excelPathPhy = URIHelper.GetMapPath("~" + excelPathVir);
            if (!Directory.Exists(excelPathPhy))
            {
                Directory.CreateDirectory(excelPathPhy);
            }
            return excelPathVir;
        }

        /// <summary>
        /// 插入数据到Excel文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="cmdTextList">待执行的SQL语句列表</param>
        /// <returns>受影响行数</returns>
        /// <remarks>作者：dfq 时间：2015-01-22</remarks>
        public static int InsertExcel(string filePath, List<String> cmdTextList)
        {
            int result = 0;
            string connString = GetOleDbConnectionString(filePath);// 取得连接字符串.
            using (OleDbConnection conn = new OleDbConnection(connString))// 定义 Oledb 的数据库联接.
            {
                conn.Open();
                foreach (string cmdText in cmdTextList)
                {
                    OleDbCommand cmd = new OleDbCommand(cmdText, conn);
                    result += cmd.ExecuteNonQuery();
                }

            }
            return result;
        }
    }
}
