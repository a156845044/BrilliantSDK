using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EnvDTE80;
using EnvDTE;

namespace Brilliant.ProjectStudio
{
    /// <summary>
    /// 公共对象和方法类
    /// </summary>
    public static class Com
    {
        private static DTE2 dte2;
        private static OutputWindowPane owp;

        /// <summary>
        /// VS实例对象
        /// </summary>
        public static DTE2 VS
        {
            get
            {
                if (dte2 == null)
                {
                    dte2 = ProjectStudioPackage.GetGlobalService(typeof(DTE)) as DTE2;
                }
                return dte2;
            }
        }

        /// <summary>
        /// VS中当前活动项目对象
        /// </summary>
        public static Project ActiveProject
        {
            get
            {
                var items = (Array)dte2.ToolWindows.SolutionExplorer.SelectedItems;
                foreach (UIHierarchyItem item in items)
                {
                    var project = item.Object as Project;
                    if (item != null)
                    {
                        return project;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 输出到面板
        /// </summary>
        /// <param name="text">文本信息</param>
        /// <param name="args">格式化参数</param>
        public static void Output(string text, params object[] args)
        {
            if (owp == null)
            {
                owp = dte2.ToolWindows.OutputWindow.OutputWindowPanes.Add("代码生成器");
            }
            text = String.Format(text, args) + "\r\n";
            owp.OutputString(text);
        }

        /// <summary>
        /// 清除输出面板
        /// </summary>
        public static void ClearOutput()
        {
            if (owp != null)
            {
                owp.Clear();
            }
        }
    }
}
