﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Brilliant.ProjectStudio {
    using System;
    
    
    /// <summary>
    ///   一个强类型的资源类，用于查找本地化的字符串等。
    /// </summary>
    // 此类是由 StronglyTypedResourceBuilder
    // 类通过类似于 ResGen 或 Visual Studio 的工具自动生成的。
    // 若要添加或移除成员，请编辑 .ResX 文件，然后重新运行 ResGen
    // (以 /str 作为命令选项)，或重新生成 VS 项目。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ResTemplate {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResTemplate() {
        }
        
        /// <summary>
        ///   返回此类使用的缓存的 ResourceManager 实例。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Brilliant.ProjectStudio.ResTemplate", typeof(ResTemplate).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   使用此强类型资源类，为所有资源查找
        ///   重写当前线程的 CurrentUICulture 属性。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;#@ template language=&quot;c#&quot; HostSpecific=&quot;True&quot; #&gt;
        ///&lt;#@ output extension= &quot;.cs&quot; #&gt;
        ///&lt;#
        ///	SchemaHost host = Host as SchemaHost;
        ///	int i = 0;
        ///	StringBuilder sbFields = new StringBuilder();
        ///	StringBuilder sbParams = new StringBuilder();
        ///	StringBuilder sbUpdParam = new StringBuilder();
        ///
        ///	StringBuilder sbFieldFmt = new StringBuilder();
        ///	StringBuilder sbFieldFmtParam = new StringBuilder();
        ///	StringBuilder sbUpdFmt = new StringBuilder();
        ///	StringBuilder sbUpdFmtParam = new StringBuilder();
        ///	StringBuilder sbS [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string ComBLL {
            get {
                return ResourceManager.GetString("ComBLL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;#@ template language=&quot;c#&quot; HostSpecific=&quot;True&quot; #&gt;
        ///&lt;#@ output extension= &quot;.cs&quot; #&gt;
        ///&lt;#
        ///	SchemaHost host = Host as SchemaHost;
        ///#&gt;
        ///using System;
        ///using System.Collections.Generic;
        ///using System.Linq;
        ///using System.Text;
        ///using System.Data;
        ///
        ///using Brilliant.ORM;
        ///using &lt;#= host.Table.TableNameSpace #&gt;.Entity;
        ///
        ///namespace &lt;#= host.Table.TableNameSpace #&gt;.BLL
        ///{
        ///    /// &lt;summary&gt;
        ///    /// &lt;#= host.Table.TableDesc#&gt;业务逻辑
        ///    /// &lt;/summary&gt;
        ///    public class &lt;#= host.Table.TableName #&gt;Biz : &lt;#= host.Table.Ta [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string ComBLLExtend {
            get {
                return ResourceManager.GetString("ComBLLExtend", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;#@ template language=&quot;c#&quot; HostSpecific=&quot;True&quot; #&gt;
        ///&lt;#@ output extension= &quot;.cs&quot; #&gt;
        ///&lt;#
        ///	SchemaHost host = Host as SchemaHost;
        ///	StringBuilder sb = new StringBuilder();
        ///	foreach(SchemaColumn field in host.Table.ColumnList)
        ///	{
        ///		sb.AppendFormat(&quot; \&quot;{0}\&quot;,&quot;, field.ColumnName);
        ///	}
        ///#&gt;
        ///using System;
        ///using System.Collections.Generic;
        ///using System.Linq;
        ///using System.Text;
        ///
        ///using Brilliant.ORM;
        ///
        ///namespace &lt;#= host.Table.TableNameSpace #&gt;.Entity
        ///{
        ///    /// &lt;summary&gt;
        ///    /// 实体映射：&lt;#= host.Table.TableDes [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string ComEntity {
            get {
                return ResourceManager.GetString("ComEntity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;#@ template language=&quot;c#&quot; HostSpecific=&quot;True&quot; #&gt;
        ///&lt;#@ output extension= &quot;.cs&quot; #&gt;
        ///&lt;#
        ///	SchemaHost host = Host as SchemaHost;
        ///	int i = 0;
        ///	StringBuilder sbFields = new StringBuilder();
        ///	StringBuilder sbParams = new StringBuilder();
        ///	StringBuilder sbUpdParam = new StringBuilder();
        ///
        ///	StringBuilder sbFieldFmt = new StringBuilder();
        ///	StringBuilder sbFieldFmtParam = new StringBuilder();
        ///	StringBuilder sbUpdFmt = new StringBuilder();
        ///	StringBuilder sbUpdFmtParam = new StringBuilder();
        ///	StringBuilder sbS [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string OABLL {
            get {
                return ResourceManager.GetString("OABLL", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;#@ template language=&quot;c#&quot; HostSpecific=&quot;True&quot; #&gt;
        ///&lt;#@ output extension= &quot;.cs&quot; #&gt;
        ///&lt;#
        ///	SchemaHost host = Host as SchemaHost;
        ///#&gt;
        ///using System;
        ///using System.Collections.Generic;
        ///using System.Linq;
        ///using System.Text;
        ///using System.Data;
        ///
        ///using Brilliant.ORM;
        ///using &lt;#= host.Table.TableNameSpace #&gt;.Entity;
        ///
        ///namespace &lt;#= host.Table.TableNameSpace #&gt;.BLL
        ///{
        ///    /// &lt;summary&gt;
        ///    /// &lt;#= host.Table.TableDesc#&gt;业务逻辑
        ///    /// &lt;/summary&gt;
        ///    public partial class &lt;#= host.Table.TableName #&gt;Biz
        ///    {
        ///    [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string OABLLExtend {
            get {
                return ResourceManager.GetString("OABLLExtend", resourceCulture);
            }
        }
        
        /// <summary>
        ///   查找类似 &lt;#@ template language=&quot;c#&quot; HostSpecific=&quot;True&quot; #&gt;
        ///&lt;#@ output extension= &quot;.cs&quot; #&gt;
        ///&lt;#
        ///	SchemaHost host = Host as SchemaHost;
        ///	StringBuilder sb = new StringBuilder();
        ///	foreach(SchemaColumn field in host.Table.ColumnList)
        ///	{
        ///		sb.AppendFormat(&quot; \&quot;{0}\&quot;,&quot;, field.ColumnName);
        ///	}
        ///#&gt;
        ///using System;
        ///using System.Collections.Generic;
        ///using System.Linq;
        ///using System.Text;
        ///
        ///using Brilliant.ORM;
        ///
        ///namespace &lt;#= host.Table.TableNameSpace #&gt;.Entity
        ///{
        ///    /// &lt;summary&gt;
        ///    /// 实体映射：&lt;#= host.Table.TableDes [字符串的其余部分被截断]&quot;; 的本地化字符串。
        /// </summary>
        internal static string OAEntity {
            get {
                return ResourceManager.GetString("OAEntity", resourceCulture);
            }
        }
    }
}
