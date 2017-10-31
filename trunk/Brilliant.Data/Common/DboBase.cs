using Brilliant.Data.Entity;
using System;

namespace Brilliant.Data.Common
{
    /// <summary>
    /// 数据库对象信息
    /// </summary>
    [Serializable]
    public class DboBase : EntityBase
    {
        /// <summary>
        /// 对象编号
        /// </summary>
        public string DboId
        {
            get { return GetProperty<string>("DboId"); }
            set { SetProperty("DboId", value); }
        }

        /// <summary>
        /// 对象名称
        /// </summary>
        public string DboName
        {
            get { return GetProperty<string>("DboName"); }
            set { SetProperty("DboName", value); }
        }

        /// <summary>
        /// 对象描述
        /// </summary>
        public string DboDesc
        {
            get { return GetProperty<string>("DboDesc"); }
            set { SetProperty("DboDesc", value); }
        }

        /// <summary>
        /// 对象类型
        /// </summary>
        public string DboType
        {
            get { return GetProperty<string>("DboType"); }
            set { SetProperty("DboType", value); }
        }
    }
}
