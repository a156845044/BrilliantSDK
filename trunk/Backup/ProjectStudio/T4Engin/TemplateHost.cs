using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.CodeDom.Compiler;
using Microsoft.VisualStudio.TextTemplating;
using Brilliant.Data.Common;

namespace Brilliant.ProjectStudio
{
    /// <summary>
    /// 模版宿主
    /// </summary>
    [Serializable]
    public class TemplateHost : ITextTemplatingEngineHost
    {
        #region 字段
        private CompilerErrorCollection _ErrorCollection;
        private Encoding _fileEncodingValue = Encoding.UTF8;
        private string _fileExtensionValue = ".cs";
        private string _namespace = "ProjectStudio.T4Engine";
        internal string _templateFileValue;
        #endregion

        #region 属性
        /// <summary>
        /// 编译错误对象集合
        /// </summary>
        public CompilerErrorCollection ErrorCollection
        {
            get { return this._ErrorCollection; }
        }

        /// <summary>
        /// 文件编码方式
        /// </summary>
        public Encoding FileEncoding
        {
            get { return this._fileEncodingValue; }
        }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExtension
        {
            get { return this._fileExtensionValue; }
        }

        /// <summary>
        /// 宿主所在命名空间
        /// </summary>
        public string NameSpace
        {
            get { return this._namespace; }
            set { this._namespace = value; }
        }

        /// <summary>
        /// 模版需调用的其他程序集引用
        /// </summary>
        public IList<string> StandardAssemblyReferences
        {
            get
            {
                return new string[] { 
                    typeof(Uri).Assembly.Location,
                    typeof(SchemaTable).Assembly.Location,
                    typeof(SchemaColumn).Assembly.Location,
                    typeof(SchemaHost).Assembly.Location,
                    typeof(TemplateHost).Assembly.Location 
                };
            }
        }

        /// <summary>
        /// 模版调用标准程序集引用
        /// </summary>
        public IList<string> StandardImports
        {
            get
            {
                return new string[] { 
                    "System", 
                    "System.Text", 
                    "System.Collections.Generic", 
                    "Brilliant.Data.Common",
                    "Brilliant.ProjectStudio"
                };
            }
        }

        /// <summary>
        /// 模版文件
        /// </summary>
        public string TemplateFile
        {
            get { return this._templateFileValue; }
            set { this._templateFileValue = value; }
        }
        #endregion

        #region 方法
        public object GetHostOption(string optionName)
        {
            string str;
            return (((str = optionName) != null) && (str == "CacheAssemblies"));
        }

        public bool LoadIncludeText(string requestFileName, out string content, out string location)
        {
            content = string.Empty;
            location = string.Empty;
            if (File.Exists(requestFileName))
            {
                content = File.ReadAllText(requestFileName);
                return true;
            }
            return false;
        }

        public void LogErrors(CompilerErrorCollection errors)
        {
            this._ErrorCollection = errors;
        }

        public AppDomain ProvideTemplatingAppDomain(string content)
        {
            return AppDomain.CreateDomain("Generation App Domain");
        }

        public string ResolveAssemblyReference(string assemblyReference)
        {
            if (File.Exists(assemblyReference))
            {
                return assemblyReference;
            }
            string path = Path.Combine(Path.GetDirectoryName(this.TemplateFile), assemblyReference);
            if (File.Exists(path))
            {
                return path;
            }
            return "";
        }

        public Type ResolveDirectiveProcessor(string processorName)
        {
            string.Compare(processorName, "XYZ", StringComparison.OrdinalIgnoreCase);
            throw new Exception("没有找到指令处理器");
        }

        public string ResolveParameterValue(string directiveId, string processorName, string parameterName)
        {
            if (directiveId == null)
            {
                throw new ArgumentNullException("the directiveId cannot be null");
            }
            if (processorName == null)
            {
                throw new ArgumentNullException("the processorName cannot be null");
            }
            if (parameterName == null)
            {
                throw new ArgumentNullException("the parameterName cannot be null");
            }
            return string.Empty;
        }

        public string ResolvePath(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("the file name cannot be null");
            }
            if (!File.Exists(fileName))
            {
                string path = Path.Combine(Path.GetDirectoryName(this.TemplateFile), fileName);
                if (File.Exists(path))
                {
                    return path;
                }
            }
            return fileName;
        }

        public void SetFileExtension(string extension)
        {
            this._fileExtensionValue = extension;
        }

        public void SetOutputEncoding(Encoding encoding, bool fromOutputDirective)
        {
            this._fileEncodingValue = encoding;
        }
        #endregion
    }
}
