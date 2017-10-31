using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Brilliant.ORM;

namespace DB_Test.Entity
{
    /// <summary>
    /// 实体映射：
    /// </summary>
    public class RolesEntity : EntityBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RolesEntity()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fkObject">外键实体对象</param>
        public RolesEntity(EntityBase fkObject)
            : base(fkObject)
        {
        }

        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleId
        {
            get { return GetProperty<string>("RoleId"); }
            set { SetProperty("RoleId", value); }
        }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName
        {
            get { return GetProperty<string>("RoleName"); }
            set { SetProperty("RoleName", value); }
        }

    }
}