using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Brilliant.Web.UI
{
    public class ScriptBlock
    {
        private Control _control;

        /// <summary>
        /// 要注册脚本的控件
        /// </summary>
        public Control Control
        {
            get { return _control; }
            set { _control = value; }
        }

        private string _script;

        /// <summary>
        /// 脚本
        /// </summary>
        public string Script
        {
            get { return _script; }
            set { _script = value; }
        }
    }
}
