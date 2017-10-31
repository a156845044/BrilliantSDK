using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Brilliant.ORM;

namespace Brilliant.DemoForm
{
    [Table(Name = "Person")]
    public class PersonInfo : EntityBase
    {
        /// <summary>
        /// 个人编号
        /// </summary>
        [Column(Name = "Id")]
        public string Id
        {
            get { return GetProperty<string>("Id"); }
            set { SetProperty("Id", value); }
        }

        /// <summary>
        /// 姓名
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
        /// 个人备注
        /// </summary>
        public int Age
        {
            get { return GetProperty<int>("Age"); }
            set { SetProperty("Age", value); }
        }
    }
}
