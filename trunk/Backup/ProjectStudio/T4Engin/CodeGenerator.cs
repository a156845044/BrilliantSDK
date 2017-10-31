using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualStudio.TextTemplating;
using Brilliant.Data.Common;
using System.Runtime.InteropServices;

namespace Brilliant.ProjectStudio
{
    public class CodeGenerator
    {
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        public static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);
        private Engine engine;

        /// <summary>
        /// 构造器
        /// </summary>
        public CodeGenerator()
        {
            this.engine = new Engine();
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="templateFile">模版路径</param>
        /// <param name="table">数据表信息</param>
        /// <returns>代码</returns>
        public string Generate(string templateFile, SchemaTable table)
        {
            SchemaHost host = new SchemaHost
            {
                TemplateFile = templateFile,
                Table = table
            };
            string content = File.ReadAllText(host.TemplateFile);
            return engine.ProcessTemplate(content, host);
        }

        /// <summary>
        /// 关闭引擎
        /// </summary>
        public void Close()
        {
            //强制回收内存
            this.engine = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            }
        }
    }
}
