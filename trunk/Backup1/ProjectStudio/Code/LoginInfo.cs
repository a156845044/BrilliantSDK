using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliant.ProjectStudio
{
    /// <summary>
    /// 登录信息实体
    /// </summary>
    public class LoginInfo
    {
        /// <summary>
        /// 数据源类型
        /// </summary>
        public string ServerType { get; set; }

        /// <summary>
        /// 数据源名称
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// 默认数据库
        /// </summary>
        public string DefaultDB { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
    }
}
