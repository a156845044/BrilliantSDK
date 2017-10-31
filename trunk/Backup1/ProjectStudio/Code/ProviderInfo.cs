using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliant.ProjectStudio
{
    /// <summary>
    /// 数据库驱动程序信息
    /// </summary>
    public class ProviderInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 驱动DLL文件名
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// 连接字符串格式
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
