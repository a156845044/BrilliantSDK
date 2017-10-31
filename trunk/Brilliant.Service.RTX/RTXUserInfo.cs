using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brilliant.Service.RTX
{
    public class RTXUserInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string EMail { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 认证类型
        /// </summary>
        public int AuthType { get; set; }
    }

    public class RTXUserDetailInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string Brithday { get; set; }

        /// <summary>
        /// 血型
        /// </summary>
        public string BloodType { get; set; }

        /// <summary>
        /// 星座
        /// </summary>
        public string Constellation { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public string College { get; set; }

        /// <summary>
        /// 主页
        /// </summary>
        public string HomePage { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }
    }
}
