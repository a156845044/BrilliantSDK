using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell;
using System.IO;

namespace Brilliant.ProjectStudio
{
    public class Utility
    {
        /// <summary>
        /// 从文件物理路径中获取文件名
        /// </summary>
        /// <param name="filePhyPath">文件物理路径 如：D:\MyDoc\Test.doc</param>
        /// <returns>文件名 如：Test.doc</returns>
        public static string GetFileNamePhy(string filePhyPath)
        {
            int i = filePhyPath.LastIndexOf("\\") + 1;
            return filePhyPath.Substring(i);
        }

        /// <summary>
        /// 获取文件扩展名
        /// </summary>
        /// <param name="fileName">文件名 如：MyDoc.doc</param>
        /// <returns>文件扩展名 如：doc</returns>
        public static string GetFileExtension(string fileName)
        {
            int i = fileName.LastIndexOf(".") + 1;
            if (i == 0)
            {
                return String.Empty;
            }
            else
            {
                return fileName.Substring(i);
            }
        }

        /// <summary>
        /// 去除文件扩展名
        /// </summary>
        /// <param name="fileName">文件名 如：MyDoc.doc</param>
        /// <returns>文件名 如：MyDoc</returns>
        public static string RemoveFileExtension(string fileName)
        {
            int i = fileName.LastIndexOf(".");
            return fileName.Substring(0, i);
        }

        /// <summary>
        /// 获取文件名称不包含扩展名
        /// </summary>
        /// <param name="phyPath">文件物理路径</param>
        /// <returns>文件名称不包含扩展名</returns>
        public static string GetFileNameWithoutExt(string phyPath)
        {
            return RemoveFileExtension(GetFileNamePhy(phyPath));
        }

        /// <summary>
        /// 从全路径中获取目录路径（不含文件）
        /// </summary>
        /// <param name="fullPath">全路径</param>
        /// <returns>目录路径</returns>
        public static string GetPath(string fullPath)
        {
            return fullPath.Substring(0, fullPath.LastIndexOf("\\"));
        }

        /// <summary>
        /// 获取指定目录下所有子目录
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>所有子目录(包含该指定路径)</returns>
        public static List<string> GetSubFolders(string path)
        {
            string[] strs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
            List<string> list = new List<string>();
            for (int i = 0; i < strs.Length - 1; i++)
            {
                string[] folders = strs[i].Split('\\');
                bool flag = true;
                foreach (string folder in folders)
                {
                    if (folder == "bin" || folder == "obj" || folder == "Properties")
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    list.Add(strs[i]);
                }
            }
            list.Insert(0, path);
            return list;
        }
    }
}
