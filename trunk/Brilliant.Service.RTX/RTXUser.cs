using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RTXSAPILib;

namespace Brilliant.Service.RTX
{
    /// <summary>
    /// RTX用户
    /// </summary>
    public class RTXUser
    {
        private RTXSAPIUserManager userMgr;

        /// <summary>
        /// 构造函数
        /// </summary>
        public RTXUser(RTXSAPIRootObj rootObj)
        {
            userMgr = rootObj.UserManager;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="entity">用户实体</param>
        /// <returns>true:操作成功 false:操作失败</returns>
        /// <remarks>作者:zwk 时间:2015-09-14</remarks>
        public bool Add(RTXUserInfo entity)
        {
            try
            {
                userMgr.AddUser(entity.UserName, entity.AuthType);
                Update(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 更新用户信息（Name不能修改）
        /// </summary>
        /// <param name="entity">用户实体</param>
        /// <returns>true:操作成功 false:操作失败</returns>
        /// <remarks>作者:zwk 时间:2015-09-14</remarks>
        public bool Update(RTXUserInfo entity)
        {
            try
            {
                userMgr.SetUserBasicInfo(entity.UserName, entity.Name, entity.Gender, entity.Mobile, entity.EMail, entity.Phone, entity.AuthType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <returns>true:操作成功 false:操作失败</returns>
        /// <remarks>作者:zwk 时间:2015-09-14</remarks>
        public bool Delete(string userName)
        {
            try
            {
                userMgr.DeleteUser(userName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户实体</returns>
        /// <remarks>作者:zwk 时间:2015-09-11</remarks>
        public RTXUserInfo GetUserInfo(string userName)
        {

            RTXUserInfo entity = new RTXUserInfo();
            try
            {
                string name = String.Empty;
                int gender = 0;
                string mobile = String.Empty;
                string email = String.Empty;
                string phone = String.Empty;
                int authType = 0;
                userMgr.GetUserBasicInfo(userName, out name, out gender, out mobile, out email, out phone, out authType);

                entity.Name = name;
                entity.Gender = gender;
                entity.Mobile = mobile;
                entity.EMail = email;
                entity.Phone = phone;
                entity.AuthType = authType;
            }
            catch { }
            return entity;
        }
    }
}
