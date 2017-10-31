/*
 * ========================================================================
 * Copyright(c) 2013-2020 Brilliant, All Rights Reserved.
 * ========================================================================
 * 类说明：
 *     该类是文件下载工具类。
 * 
 * 作者：zwk       时间：2013-11-12
 * ========================================================================
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace Brilliant.Utility
{
    /// <summary>
    /// 文件下载工具类
    /// </summary>
    public static class DownloadHelper
    {
        private const int CHUNK_SIZE = 102400;

        /// <summary>
        /// Http文件下载
        /// </summary>
        /// <param name="filePath">文件虚拟路径 如：~/Upload/Test.zip</param>
        public static void DownloadFile(string filePath)
        {
            filePath = HttpContext.Current.Server.MapPath(filePath); //转换为物理路径
            if (!File.Exists(filePath))
            {
                return;
            }
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(System.IO.Path.GetFileName(filePath), System.Text.Encoding.UTF8));
            HttpContext.Current.Response.TransmitFile(filePath);
        }

        /// <summary>
        /// 流下载
        /// </summary>
        /// <param name="filePath">相对路径+文件名（如：/uploadfiles/att/1.doc）</param>
        /// <param name="newFileName">新文件名称</param>
        public static void DownloadFile(string filePath, string newFileName)
        {
            #region 为了服务器压力，限制每次下载的大小，故注释
            string phyFilePath = HttpContext.Current.Server.MapPath(String.Format("~{0}", filePath));
            if (!File.Exists(phyFilePath))
            {
                HttpContext.Current.Response.Write("<script>alert(\"您当前下载的文件不存在！\");</script>");
                return;
            }

            FileInfo fi = new FileInfo(phyFilePath);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = false;
            string fileName = Path.GetFileName(phyFilePath);
            if (string.IsNullOrEmpty(newFileName))
            {
                newFileName = fileName;
            }
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = false;

            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(newFileName, System.Text.Encoding.UTF8));
            HttpContext.Current.Response.TransmitFile(phyFilePath);
            #endregion

            //string phyFilePath = HttpContext.Current.Server.MapPath(String.Format("~{0}", filePath));
            //if (!File.Exists(phyFilePath))
            //{
            //    HttpContext.Current.Response.Write(" 您当前下载的文件不存在！");
            //    return;
            //}

            //FileInfo fileInfo = new FileInfo(phyFilePath);
            //if (fileInfo.Exists)
            //{
            //    int ChunkSize = CHUNK_SIZE;//100K 每次读取文件，只读取100K，缓解服务器的压力。
            //    byte[] buffer = new byte[ChunkSize];
            //    HttpContext.Current.Response.Clear();
            //    System.IO.FileStream iStream = System.IO.File.OpenRead(fileInfo.FullName);
            //    long dataLengthToRead = iStream.Length;//获取下载的文件总大小
            //    string fileName = Path.GetFileName(phyFilePath);
            //    if (string.IsNullOrEmpty(newFileName))
            //    {
            //        newFileName = fileName;
            //    }
            //    HttpContext.Current.Response.ContentType = "application/octet-stream";
            //    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(newFileName, System.Text.Encoding.UTF8));
            //    while (dataLengthToRead > 0 && HttpContext.Current.Response.IsClientConnected)
            //    {
            //        int lenthRead = iStream.Read(buffer, 0, ChunkSize);//读取大小
            //        HttpContext.Current.Response.OutputStream.Write(buffer, 0, lenthRead);
            //        HttpContext.Current.Response.Flush();
            //        dataLengthToRead = dataLengthToRead - lenthRead;
            //    }
            //    iStream.Close();
            //    HttpContext.Current.Response.Close();
            //}
        }

    }
}
