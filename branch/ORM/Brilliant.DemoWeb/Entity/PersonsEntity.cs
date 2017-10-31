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
    public class PersonsEntity : EntityBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public PersonsEntity()
        {
            this.RolesModel = new RolesEntity(this);

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fkObject">外键实体对象</param>
        public PersonsEntity(EntityBase fkObject)
            : base(fkObject)
        {
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Id
        {
            get { return GetProperty<string>("Id"); }
            set { SetProperty("Id", value); }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return GetProperty<string>("Name"); }
            set { SetProperty("Name", value); }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return GetProperty<string>("Sex"); }
            set { SetProperty("Sex", value); }
        }

        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get { return GetProperty<int>("Age"); }
            set { SetProperty("Age", value); }
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
        /// 外键表实体
        /// </summary>
        public RolesEntity RolesModel { get; set; }

    }
}