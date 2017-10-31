using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliant.ProjectStudio
{
    /// <summary>
    /// 模板信息实体
    /// </summary>
    public class TemplateInfo
    {
        /// <summary>
        /// 模板存放路径
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 后缀
        /// </summary>
        public string Output { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public string FileType { get; set; }
    }
}
