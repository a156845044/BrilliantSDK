using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Brilliant.Data.Common;

namespace Brilliant.ProjectStudio
{
    /// <summary>
    /// 表信息宿主
    /// </summary>
    [Serializable]
    public class SchemaHost : TemplateHost
    {
        public SchemaTable Table { get; set; }
    }
}
